using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Script.Serialization;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.ContentLocations;
using Telerik.Sitefinity.Frontend.Media.Mvc.Helpers;
using Telerik.Sitefinity.Frontend.Mvc.Models;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules;
using Telerik.Sitefinity.Modules.Libraries;
using Telerik.Sitefinity.Modules.Libraries.Configuration;
using Telerik.Sitefinity.Modules.Libraries.ImageProcessing;
using Telerik.Sitefinity.Modules.Libraries.ImageProcessing.Models;
using Telerik.Sitefinity.Modules.Libraries.Thumbnails;
using Telerik.Sitefinity.Modules.Libraries.Web.Services;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web;
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

        /// <inheritdoc />
        public bool Responsive { get; set; }

        #endregion

        #region Public Methods

        /// <inheritDoc/>
        public virtual ImageViewModel GetViewModel()
        {
            var viewModel = new ImageViewModel()
            {
                AlternativeText = this.AlternativeText,
                Title = this.Title,
                DisplayMode = this.DisplayMode,
                ThumbnailName = this.ThumbnailName,
                ThumbnailHeight = null,
                ThumbnailWidth = null,
                CustomSize = this.CustomSize != null ? new JavaScriptSerializer().Deserialize<CustomSizeModel>(this.CustomSize) : null,
                Responsive = this.Responsive,
                UseAsLink = this.UseAsLink,
                CssClass = this.CssClass
            };

            SfImage image;
            if (this.Id != Guid.Empty)
            {
                image = this.GetImage();
                if (image != null)
                {
                    if (this.CustomSize != null)
                    {
                        this.ThumbnailUrl = image.ResolveCustomImageSizeThumbnailUrl(this.CustomSize, this.ProviderName);
                    }
                    else
                    {
                        var urlAsAbsolute = Config.Get<SystemConfig>().SiteUrlSettings.GenerateAbsoluteUrls;
                        this.ThumbnailUrl = image.ResolveThumbnailUrl(null, urlAsAbsolute);
                    }

                    viewModel.SelectedSizeUrl = this.GetSelectedSizeUrl(image);
                    viewModel.LinkedContentUrl = GetLinkedUrl(image);
                }
            }
            else
            {
                image = new SfImage();
            }

            int width;
            int height;

            this.GetThumbnailSizes(out width, out height, image);

            viewModel.ThumbnailUrl = this.ThumbnailUrl;
            viewModel.ThumbnailHeight = height;
            viewModel.ThumbnailWidth = width;

            viewModel.Item = new ItemViewModel(image);

            return viewModel;
        }

 		/// <inheritDoc/>
        public virtual IEnumerable<IContentLocationInfo> GetLocations()
        {
            return ContentLocationHelper.GetLocations(this.Id, this.ProviderName, typeof(SfImage));
        }

        #endregion

        #region Private members


        /// <summary>
        /// Gets the image.
        /// </summary>
        /// <returns></returns>
        protected virtual SfImage GetImage()
        {
            LibrariesManager librariesManager = LibrariesManager.GetManager(this.ProviderName);
            return librariesManager.GetImages().Where(i => i.Id == this.Id).Where(PredefinedFilters.PublishedItemsFilter<Telerik.Sitefinity.Libraries.Model.Image>()).FirstOrDefault();
        }

        /// <summary>
        /// Gets the selected size URL.
        /// </summary>
        /// <param name="image">The image.</param>
        /// <returns></returns>
        protected virtual string GetSelectedSizeUrl(SfImage image)
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
                    var relativeUrl = node.GetFullUrl(Telerik.Sitefinity.Services.SystemManager.CurrentContext.Culture, false, true);
                    linkedUrl = UrlPath.ResolveUrl(relativeUrl, false);
                }
            }
            else if (this.UseAsLink && this.LinkedPageId == Guid.Empty)
            {
                linkedUrl = image.ResolveMediaUrl(false);
            }
            
            return linkedUrl;
        }

        private CultureInfo GetCurrentCulture()
        {
            if (SystemManager.CurrentContext.AppSettings.Multilingual)
            {
                var currentCulture = Telerik.Sitefinity.Services.SystemManager.CurrentContext.Culture;
                if (SystemManager.CurrentContext.AppSettings.DefinedFrontendLanguages.Contains(currentCulture))
                    return currentCulture;
            }

            return SystemManager.CurrentContext.AppSettings.DefaultFrontendLanguage;
        }

        private ThumbnailProfileConfigElement GetDefaultAlbumThumbnailProfile(string thumbnailName)
        {
            if (!string.IsNullOrWhiteSpace(thumbnailName))
            {
                var librariesConfig = Config.Get<LibrariesConfig>();
                if (librariesConfig?.Images?.Thumbnails?.Profiles != null)
                {
                    var profiles = librariesConfig?.Images?.Thumbnails?.Profiles;

                    if (profiles.ContainsKey(thumbnailName))
                    {
                        var thumbnailProfile = profiles[thumbnailName];

                        return thumbnailProfile;
                    }
                }
            }

            return null;
        }

        private int GetThumbnailProfileSize(ThumbnailProfileConfigElement thumbnailProfile, string parameterKey)
        {
            int width = 0;
            int.TryParse(thumbnailProfile.Parameters[parameterKey], out width);

            return width;
        }

        private void GetThumbnailSizes(out int width, out int height, SfImage image)
        {
            width = 0;
            height = 0;

            var thumbnailName = this.ThumbnailName != null ? this.ThumbnailName : string.Empty;

            var thumbnailProfile = this.GetDefaultAlbumThumbnailProfile(thumbnailName);
            if (thumbnailProfile != null)
            {
                var selectedThumbnail = image.Thumbnails.Where(t => t.Name == thumbnailProfile.Name).FirstOrDefault();

                if (selectedThumbnail == null) 
                {
                    selectedThumbnail = LazyThumbnailGenerator.Instance.CreateThumbnail(image, thumbnailName);
                }

                if (selectedThumbnail != null)
                {
                    width = selectedThumbnail.Width;
                    height = selectedThumbnail.Height;
                }
            }
        }
            #endregion
    }
}