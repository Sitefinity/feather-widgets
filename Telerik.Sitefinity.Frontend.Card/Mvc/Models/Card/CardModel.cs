using System;
using System.Globalization;
using System.Linq;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Modules;
using Telerik.Sitefinity.Modules.GenericContent;
using Telerik.Sitefinity.Modules.Libraries;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web;
using SfImage = Telerik.Sitefinity.Libraries.Model.Image;

namespace Telerik.Sitefinity.Frontend.Card.Mvc.Models.Card
{
    /// <summary>
    /// Provides API for working with card items.
    /// </summary>
    public class CardModel : ICardModel
    {
        public CardModel()
        {
            this.IsPageSelectMode = true;
        }

        /// <inheritdoc />
        public Guid ImageId { get; set; }

        /// <inheritdoc />
        public string ImageProviderName { get; set; }

        /// <inheritdoc />
        public string CssClass { get; set; }

        /// <inheritdoc />
        [DynamicLinksContainer]
        public string Description { get; set; }

        /// <inheritdoc />
        public bool IsPageSelectMode { get; set; }

        /// <inheritdoc />
        public Guid LinkedPageId { get; set; }

        /// <inheritdoc />
        public string LinkedUrl { get; set; }

        /// <inheritdoc />
        public string ActionName { get; set; }

        /// <inheritdoc />
        public string Heading { get; set; }

        /// <inheritdoc />
        public virtual CardViewModel GetViewModel()
        {
            var viewModel = new CardViewModel()
            {
                Heading = this.Heading,
                Description = this.Description,
                ActionName = this.ActionName,
                ActionUrl = this.GetLinkedUrl(),
                CssClass = this.CssClass
            };

            SfImage image;
            if (this.ImageId != Guid.Empty)
            {
                image = this.GetImage();
                if (image != null)
                {
                    viewModel.SelectedSizeUrl = this.GetSelectedSizeUrl(image);
                    viewModel.ImageAlternativeText = image.AlternativeText;
                    viewModel.ImageTitle = image.Title;
                }
            }

            return viewModel;
        }

        /// <inheritdoc />
        public bool IsEmpty()
        {
            return (this.ImageId == Guid.Empty &&
                            this.LinkedPageId == Guid.Empty &&
                            string.IsNullOrEmpty(this.Description) &&
                            string.IsNullOrEmpty(this.Description) &&
                            string.IsNullOrEmpty(this.CssClass) &&
                            string.IsNullOrEmpty(this.ActionName) &&
                            string.IsNullOrEmpty(this.Heading) &&
                            string.IsNullOrEmpty(this.LinkedUrl) &&
                            string.IsNullOrEmpty(this.ImageProviderName));
        }

        /// <summary>
        /// Gets the image.
        /// </summary>
        /// <returns></returns>
        protected virtual SfImage GetImage()
        {
            LibrariesManager librariesManager = LibrariesManager.GetManager(this.ImageProviderName);
            return librariesManager.GetImages().Where(i => i.Id == this.ImageId).Where(PredefinedFilters.PublishedItemsFilter<Telerik.Sitefinity.Libraries.Model.Image>()).FirstOrDefault();
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
            var originalImageUrl = image.ResolveMediaUrl(urlAsAbsolute);
            imageUrl = originalImageUrl;

            return imageUrl;
        }

        /// <summary>
        /// Gets the linked page URL.
        /// </summary>
        /// <returns></returns>
        protected virtual string GetLinkedUrl()
        {
            if (this.IsPageSelectMode)
            {
                if (this.LinkedPageId == Guid.Empty)
                    return null;

                var pageManager = PageManager.GetManager();
                var node = pageManager.GetPageNode(this.LinkedPageId);
                if (node != null)
                {
                    string relativeUrl;
                    if (SystemManager.CurrentContext.AppSettings.Multilingual)
                    {
                        relativeUrl = node.GetFullUrl(CultureInfo.CurrentUICulture, false);
                    }
                    else
                    {
                        relativeUrl = node.GetFullUrl(null, false, true);
                    }
                    
                    return UrlPath.ResolveUrl(relativeUrl, Config.Get<SystemConfig>().SiteUrlSettings.GenerateAbsoluteUrls);
                }
            }
            else if (!string.IsNullOrEmpty(this.LinkedUrl))
            {
                return this.LinkedUrl;
            }

            return null;
        }
    }
}
