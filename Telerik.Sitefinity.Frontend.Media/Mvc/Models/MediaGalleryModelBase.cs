using ServiceStack.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Telerik.Sitefinity.ContentLocations;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Frontend.Mvc.Models;
using Telerik.Sitefinity.Libraries.Model;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.Libraries;

namespace Telerik.Sitefinity.Frontend.Media.Mvc.Models
{
    /// <summary>
    /// Base class for business logic of media galleries.
    /// </summary>
    /// <typeparam name="TLibrary">The type of the library.</typeparam>
    /// <typeparam name="TMedia">The type of the media items.</typeparam>
    public abstract class MediaGalleryModelBase<TMedia> : ContentModelBase
        where TMedia : MediaContent
    {
        /// <summary>
        /// Gets or sets the type of content that is loaded.
        /// </summary>
        /// <value>
        /// The type of the content.
        /// </value>
        public override Type ContentType
        {
            get
            {
                return typeof(TMedia);
            }

            set
            {
            }
        }

        /// <summary>
        /// Gets or sets the parent filtering mode.
        /// </summary>
        /// <value>
        /// The parent filtering mode.
        /// </value>
        public ParentFilterMode ParentFilterMode { get; set; }

        /// <summary>
        /// Gets or sets the serialized selected parent ids.
        /// </summary>
        /// <value>
        /// The serialized selected parents ids.
        /// </value>
        public string SerializedSelectedParentsIds { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to include items from child libraries of the selected libraries.
        /// </summary>
        /// <value>
        /// <c>true</c> if items of child libraries should be included; otherwise, <c>false</c>.
        /// </value>
        public bool IncludeChildLibraries { get; set; }

        /// <inheritdoc />
        public virtual ContentListViewModel CreateListViewModelByParent(IFolder parentItem, int page)
        {
            if (page < 1)
                throw new ArgumentException("'page' argument has to be at least 1.", "page");

            var query = ((LibrariesManager)this.GetManager()).GetDescendants(parentItem); 
            if (query == null)
                return this.CreateListViewModelInstance();

            var viewModel = this.CreateListViewModelInstance();
            this.PopulateListViewModel(page, query, viewModel);

            return viewModel;
        }

        /// <inheritdoc />
        protected override string CompileFilterExpression()
        {
            var baseExpression = base.CompileFilterExpression();

            if (this.ParentFilterMode == ParentFilterMode.Selected && this.SerializedSelectedParentsIds.IsNullOrEmpty() == false && !this.IncludeChildLibraries)
            {
                var selectedItemIds = JsonSerializer.DeserializeFromString<IList<string>>(this.SerializedSelectedParentsIds);
                var parentFilterExpression = string.Join(" OR ", selectedItemIds.Select(id => "((Parent.Id = " + id.Trim() + " AND FolderId = null)" + " OR FolderId = " + id.Trim() + ")"));

                if (!parentFilterExpression.IsNullOrEmpty())
                {
                    if (baseExpression.IsNullOrEmpty())
                        return "({0})".Arrange(parentFilterExpression);
                    else
                        return "({0}) and ({1})".Arrange(baseExpression, parentFilterExpression);
                }
            }

            return baseExpression;
        }

        /// <inheritdoc />
        protected override IQueryable<IDataItem> UpdateExpression(IQueryable<IDataItem> query, int? skip, int? take, ref int? totalCount)
        {
            if (this.ParentFilterMode == ParentFilterMode.Selected && this.SerializedSelectedParentsIds.IsNullOrEmpty() == false && this.IncludeChildLibraries)
            {
                var selectedItemIds = JsonSerializer.DeserializeFromString<IList<string>>(this.SerializedSelectedParentsIds);
                Guid parentId;
                if (selectedItemIds.Count >= 1 && Guid.TryParse(selectedItemIds[0], out parentId))
                {
                    var parent = ((LibrariesManager)this.GetManager()).GetFolder(parentId);
                    if (parent != null)
                    {
                        var compiledFilterExpression = this.CompileFilterExpression();
                        compiledFilterExpression = this.AddLiveFilterExpression(compiledFilterExpression);
                        compiledFilterExpression = this.AdaptMultilingualFilterExpression(compiledFilterExpression);

                        var mediaQuery = (IQueryable<TMedia>)query;
                        mediaQuery = this.SetExpression(mediaQuery, compiledFilterExpression, this.SortExpression, 0, 0, ref totalCount);
                        
                        var getDescendants = typeof(LibrariesManager).GetMethod("GetDescendantsFromQuery", BindingFlags.Instance | BindingFlags.NonPublic).MakeGenericMethod(typeof(TMedia));
                        mediaQuery = (IQueryable<TMedia>)getDescendants.Invoke(this.GetManager(), new object[] { mediaQuery, parent });

                        mediaQuery = this.SetExpression(mediaQuery, null, null, skip, take, ref totalCount);

                        return mediaQuery;
                    }
                }
            }

            return base.UpdateExpression(query, skip, take, ref totalCount);
        }

        /// <inheritdoc />
        public override IEnumerable<ContentLocations.IContentLocationInfo> GetLocations()
        {
            var result = base.GetLocations();
            var firstLocation = result.FirstOrDefault() as ContentLocationInfo;

            if (firstLocation != null && this.ParentFilterMode == ParentFilterMode.Selected && this.SerializedSelectedParentsIds.IsNullOrEmpty() == false && this.IncludeChildLibraries)
            {
                var selectedItemIds = JsonSerializer.DeserializeFromString<IList<string>>(this.SerializedSelectedParentsIds);
                if (selectedItemIds.Count >= 1)
                {
                    var mediaContentParentLocationFilterType = typeof(Telerik.Sitefinity.Constants).Assembly.GetType("Telerik.Sitefinity.Modules.Libraries.Web.UI.MediaContentParentLocationFilter");
                    var filter = mediaContentParentLocationFilterType.GetConstructor(Type.EmptyTypes).Invoke(null);
                    var value = mediaContentParentLocationFilterType.GetProperty("Value", BindingFlags.Public | BindingFlags.Instance);

                    value.SetValue(filter, selectedItemIds[0], null);
                    firstLocation.Filters.Add(filter as IContentLocationFilter);
                }
            }

            return result;
        }
    }
}
