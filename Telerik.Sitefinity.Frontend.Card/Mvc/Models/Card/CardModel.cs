using ServiceStack.Text;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Frontend.Mvc.Models;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules;
using Telerik.Sitefinity.Modules.GenericContent;
using Telerik.Sitefinity.Modules.Libraries;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Modules.Pages;
using SfImage = Telerik.Sitefinity.Libraries.Model.Image;
using Telerik.Sitefinity.Web;

namespace Telerik.Sitefinity.Frontend.Card.Mvc.Models.Card
{
    /// <summary>
    /// Provides API for working with card items.
    /// </summary>
    public class CardModel : ICardModel
    {
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
        public Guid LinkedPageId { get; set; }

        /// <inheritdoc />
        public string LinkedUrl { get; set; }

        /// <inheritdoc />
        public string ActionName { get; set; }

        /// <inheritdoc />
        public string Heading { get; set; }

        /// <inheritdoc />
        public CardViewModel GetViewModel()
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
            string linkedUrl = null;

            if (this.LinkedPageId != Guid.Empty)
            {
                var pageManager = PageManager.GetManager();
                var node = pageManager.GetPageNode(this.LinkedPageId);
                if (node != null)
                {
                    var relativeUrl = node.GetFullUrl();
                    linkedUrl = UrlPath.ResolveUrl(relativeUrl, true);
                }
            }
            else if (!string.IsNullOrEmpty(this.LinkedUrl) && this.LinkedPageId == Guid.Empty)
            {
                linkedUrl = this.LinkedUrl;
            }

            return linkedUrl;
        }
    }
}
