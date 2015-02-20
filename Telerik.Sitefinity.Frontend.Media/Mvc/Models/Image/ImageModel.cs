using System;
using System.Linq;
using System.Web.Script.Serialization;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Frontend.Mvc.Models;
using Telerik.Sitefinity.Modules.Libraries;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Services;
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
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="ImageModel" /> class.
        /// </summary>
        public ImageModel()
        {
            
        }

        #endregion

        #region Public Properties

        /// <inheritdoc />
        public Guid Id { get; set; }

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
        public Guid LinkedPageId { get; set; }

        /// <inheritdoc />
        public ImageDisplayMode DisplayMode { get; set; }

        /// <inheritdoc />
        public string ThumbnailName { get; set; }

        /// <inheritdoc />
        public string ThumbnailUrl { get; set; }

        /// <inheritdoc />
        public string CustomSize { get; set; }

        #endregion

        #region Public Methods

        /// <inheritDoc/>
        public ImageViewModel GetViewModel()
        {
            var viewModel = new ImageViewModel()
            {
                Title = this.Title,
                DisplayMode = this.DisplayMode,
                ThumbnailName = this.ThumbnailName,
                ThumbnailUrl = this.ThumbnailUrl,
                CustomSize = this.CustomSize != null ? new JavaScriptSerializer().Deserialize<ImageViewModel.CustomSizeModel>(this.CustomSize) : null,
                UseAsLink = this.UseAsLink
            };

            SfImage image;
            if (this.Id != Guid.Empty)
            {
                LibrariesManager librariesManager = LibrariesManager.GetManager(this.ProviderName);
                image = librariesManager.GetImages().Where(i => i.Id == this.Id).FirstOrDefault();
            }
            else
            {
                image = new SfImage();
            }


            viewModel.Item = new ItemViewModel(image);
            viewModel.SelectedSizeUrl = this.GetSelectedSizeUrl(image);
            viewModel.LinkedContentUrl = GetLinkedUrl(image);

            return viewModel;
        }

        #endregion

        #region Private members

        /// <summary>
        /// Gets the linked page URL.
        /// </summary>
        /// <returns></returns>
        private string GetLinkedUrl(SfImage image)
        {
            string linkedUrl = null;

            if (this.UseAsLink && this.LinkedPageId != Guid.Empty)
            {
                var pageManager = PageManager.GetManager();
                var node = pageManager.GetPageNode(this.LinkedPageId);
                if (node != null)
                {
                    var relativeUrl = node.GetFullUrl();
                    linkedUrl = UrlPath.ResolveUrl(relativeUrl, true);
                }
            }
            else if (this.UseAsLink && this.LinkedPageId == Guid.Empty) {
                linkedUrl = image.ResolveMediaUrl(true);
            }

            return linkedUrl;
        }

        /// <summary>
        /// Gets the selected size URL.
        /// </summary>
        /// <param name="image">The image.</param>
        /// <returns></returns>
        private string GetSelectedSizeUrl(SfImage image) 
        {
            if (image.Id == Guid.Empty)
                return string.Empty;

            string imageUrl;
            var urlAsAbsolute = Config.Get<SystemConfig>().SiteUrlSettings.GenerateAbsoluteUrls;
            if (this.DisplayMode == ImageDisplayMode.Thumbnail || !string.IsNullOrWhiteSpace(this.ThumbnailName))
            {
                imageUrl = image.ResolveThumbnailUrl(this.ThumbnailName, urlAsAbsolute);
            }
            else if (this.DisplayMode == ImageDisplayMode.Custom)
            {
                imageUrl = this.ThumbnailUrl;
            }
            else
            {
                var originalImageUrl = image.ResolveMediaUrl(urlAsAbsolute);
                imageUrl = originalImageUrl;
            }

            return imageUrl;
        }

        #endregion
    }
}