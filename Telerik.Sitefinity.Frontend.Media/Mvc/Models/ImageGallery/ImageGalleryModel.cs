using System;
using System.Linq;
using System.Web.Script.Serialization;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Frontend.Media.Mvc.Models.Image;
using Telerik.Sitefinity.Frontend.Mvc.Models;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.Libraries;
using Telerik.Sitefinity.Services;
using SfImage = Telerik.Sitefinity.Libraries.Model.Image;

namespace Telerik.Sitefinity.Frontend.Media.Mvc.Models.ImageGallery
{
    /// <summary>
    /// A model for the Image Gallery MVC widget.
    /// </summary>
    public class ImageGalleryModel : MediaGalleryModelBase<SfImage>, IImageGalleryModel
    {
        #region Properties
        /// <inheritdoc />
        public string SerializedThumbnailSizeModel { get; set; }

        /// <summary>
        /// Gets the size model of the thumbnails in the gallery.
        /// </summary>
        /// <value>The thumbnail size model.</value>
        public ImageSizeModel ThumbnailSizeModel
        {
            get
            {
                if (this.thumbnailSizeModel == null)
                {
                    this.thumbnailSizeModel = this.SerializedThumbnailSizeModel != null ?
                        new JavaScriptSerializer().Deserialize<ImageSizeModel>(this.SerializedThumbnailSizeModel) :
                        new ImageSizeModel()
                        {
                            DisplayMode = ImageDisplayMode.Original
                        };
                }

                return this.thumbnailSizeModel;
            }
        }
        #endregion

        #region Protected overriden methods
        /// <inheritdoc />
        protected override IQueryable<IDataItem> GetItemsQuery()
        {
            return ((LibrariesManager)this.GetManager()).GetImages();
        }

        /// <inheritdoc />
        protected override ItemViewModel CreateItemViewModelInstance(IDataItem item)
        {
            return new ThumbnailViewModel(item);
        }

        /// <inheritdoc />
        protected override void PopulateListViewModel(int page, IQueryable<IDataItem> query, ContentListViewModel viewModel)
        {
            base.PopulateListViewModel(page, query, viewModel);

            foreach (var item in viewModel.Items)
            {
                ((ThumbnailViewModel)item).ThumbnailUrl = this.GetSelectedSizeUrl((SfImage)item.DataItem, this.ThumbnailSizeModel);
            }
        }
        #endregion

        /// <summary>
        /// Gets the selected size URL.
        /// </summary>
        /// <param name="image">The image.</param>
        /// <returns></returns>
        protected virtual string GetSelectedSizeUrl(SfImage image, ImageSizeModel sizeModel)
        {
            if (image.Id == Guid.Empty)
                return string.Empty;

            string imageUrl;
            var urlAsAbsolute = Config.Get<SystemConfig>().SiteUrlSettings.GenerateAbsoluteUrls;
            if (sizeModel.DisplayMode == ImageDisplayMode.Thumbnail && !string.IsNullOrWhiteSpace(sizeModel.Thumbnail.Name))
            {
                imageUrl = image.ResolveThumbnailUrl(this.ThumbnailSizeModel.Thumbnail.Name, urlAsAbsolute);
            }
            else
            {
                var originalImageUrl = image.ResolveMediaUrl(urlAsAbsolute);
                imageUrl = originalImageUrl;
            }

            return imageUrl;
        }

        #region Private fields and constants
        private ImageSizeModel thumbnailSizeModel;
        #endregion
    }
}
