using ServiceStack.Text;
using System;
using System.Collections.Generic;
using System.Linq;
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
                var manager = (LibrariesManager)this.GetManager();
                var selectedItemIds = JsonSerializer.DeserializeFromString<IList<string>>(this.SerializedSelectedParentsIds);
                foreach (var stringId in selectedItemIds)
                {
                    Guid id;
                    if (Guid.TryParse(stringId, out id))
                    {
                        var folder = manager.GetFolder(id);
                        query = query.Union(manager.GetDescendants(folder));
                    }
                }
            }

            return base.UpdateExpression(query, skip, take, ref totalCount);
        }
    }
}
