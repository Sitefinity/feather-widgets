using System;
using System.Linq;
using System.Web.Script.Serialization;
using Telerik.Sitefinity.Frontend.Mvc.Models;
using Telerik.Sitefinity.Modules.Libraries;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Web.DataResolving;
using SfImage = Telerik.Sitefinity.Libraries.Model.Image;

namespace Telerik.Sitefinity.Frontend.Media.Mvc.Models.Image
{
    /// <summary>
    ///     This class is used as a model for the image controller.
    /// </summary>
    public class ImageModel : IImageModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ImageModel" /> class.
        /// </summary>
        public ImageModel()
        {
            
        }

        /// <inheritdoc />
        public Guid Id { get; set; }

        /// <inheritdoc />
        public string Markup { get; set; }

        /// <inheritdoc />
        public string ProviderName { get; set; }

        /// <inheritdoc />
        public string Title { get; set; }

        /// <inheritdoc />
        public string AlternativeText { get; set; }

        /// <inheritdoc />
        public string CssClass { get; set; }

        /// <inheritdoc />
        public bool UseAsLink { get; set; }

        /// <inheritdoc />
        public Guid PageIdToUseAsLink { get; set; }

        /// <inheritdoc />
        public ImageDisplayMode DisplayMode { get; set; }

        /// <inheritdoc />
        public string ThumbnailName { get; set; }

        /// <inheritdoc />
        public string ThumbnailUrl { get; set; }

        /// <inheritdoc />
        public string CustomSize { get; set; }

        /// <inheritDoc/>
        public ImageViewModel GetViewModel()
        {
            var viewModel = new ImageViewModel()
            {
                Markup = this.Markup,
                Title = this.Title,
                DisplayMode = this.DisplayMode,
                ThumbnailName = this.ThumbnailName,
                ThumbnailUrl = this.ThumbnailUrl,
                CustomSize = this.CustomSize != null ? new JavaScriptSerializer().Deserialize<ImageViewModel.CustomSizeModel>(this.CustomSize) : null
            };

            if (this.Id != Guid.Empty)
            {
                LibrariesManager librariesManager = LibrariesManager.GetManager(this.ProviderName);
                viewModel.Item = new ItemViewModel(librariesManager.GetImages().Where(i => i.Id == this.Id).FirstOrDefault());
            }
            else
            {
                viewModel.Item = new ItemViewModel(new SfImage());
            }

            if (this.UseAsLink && this.PageIdToUseAsLink != Guid.Empty) 
            {
                var pageManager = PageManager.GetManager();
                var node = pageManager.GetPageNode(this.PageIdToUseAsLink);
                if (node != null)
                {
                    var relativeUrl = DataResolver.Resolve(node, "URL");
                    viewModel.PageUrlUseAsLink = UrlPath.ResolveUrl(relativeUrl, true);
                }
            }

            return viewModel;
        }
    }
}