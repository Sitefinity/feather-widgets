using ServiceStack.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.ContentLocations;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Frontend.Mvc.Models;
using Telerik.Sitefinity.Libraries.Model;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.Libraries;
using Telerik.Sitefinity.Modules.Libraries.Configuration;

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
        public string SerializedSelectedParentsIds
        {
            get
            {
                return this.serializedSelectedParentsIds;
            }
            set
            {
                if (this.serializedSelectedParentsIds != value)
                {
                    this.serializedSelectedParentsIds = value;
                    if (!this.serializedSelectedParentsIds.IsNullOrEmpty())
                    {
                        this.selectedParentsIds = JsonSerializer.DeserializeFromString<IList<string>>(this.serializedSelectedParentsIds);
                    }
                }
            }
        }

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
        protected override void PopulateListViewModel(int page, IQueryable<IDataItem> query, ContentListViewModel viewModel)
        {
            int? totalPages = null;
            if (this.ParentFilterMode == Models.ParentFilterMode.Selected && this.selectedParentsIds.Count() == 0)
            {
                viewModel.Items = Enumerable.Empty<ItemViewModel>();
            }
            else
            {
                viewModel.Items = this.ApplyListSettings(page, query, out totalPages);
            }

            this.SetViewModelProperties(viewModel, page, totalPages);
        }

        /// <inheritdoc />
        protected override string CompileFilterExpression()
        {
            var baseExpression = base.CompileFilterExpression();

            if (this.ParentFilterMode == ParentFilterMode.Selected && this.selectedParentsIds.Count() != 0)
            {
                var parentFilterExpression = string.Join(" OR ", this.selectedParentsIds.Select(id => "((Parent.Id = " + id.Trim() + " AND FolderId = null)" + " OR FolderId = " + id.Trim() + ")"));

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

        /// <summary>
        /// Gets the name of the default thumbnail profile.
        /// </summary>
        /// <typeparam name="TLibrary">The type of the library.</typeparam>
        /// <returns>The name of the default thumbnail profile.</returns>
        protected static string DefaultThumbnailProfileName<TLibrary>()
            where TLibrary : Library
        {
            ConfigElementDictionary<string, ThumbnailProfileConfigElement> profiles;
            Type libraryType = typeof(TLibrary);

            if (libraryType == typeof(Album))
                profiles = Config.Get<LibrariesConfig>().Images.Thumbnails.Profiles;
            else if (libraryType == typeof(VideoLibrary))
                profiles = Config.Get<LibrariesConfig>().Videos.Thumbnails.Profiles;
            else
                return null;

            var defaultProfile = profiles.Values.FirstOrDefault(p => p.IsDefault) ?? profiles.Values.FirstOrDefault(p => p.Name == "thumbnail") ?? profiles.Values.FirstOrDefault();
            if (defaultProfile != null)
                return defaultProfile.Name;
            else
                return null;
        }

        #region Private fields and constants
        private string serializedSelectedParentsIds;
        private IList<string> selectedParentsIds = new List<string>();
        #endregion
    }
}
