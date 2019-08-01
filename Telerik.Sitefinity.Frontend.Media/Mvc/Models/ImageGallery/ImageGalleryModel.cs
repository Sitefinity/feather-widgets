using System;
using System.Linq;
using ServiceStack.Text;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Frontend.Media.Mvc.Helpers;
using Telerik.Sitefinity.Frontend.Media.Mvc.Models.Image;
using Telerik.Sitefinity.Frontend.Mvc.Models;
using Telerik.Sitefinity.Libraries.Model;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.Libraries;
using Telerik.Sitefinity.Modules.Libraries;
using Telerik.Sitefinity.Services;
using SfImage = Telerik.Sitefinity.Libraries.Model.Image;
using Telerik.Sitefinity.Modules.Libraries.Configuration;
using Config = Telerik.Sitefinity.Configuration.Config;

namespace Telerik.Sitefinity.Frontend.Media.Mvc.Models.ImageGallery
{
    /// <summary>
    /// A model for the Image Gallery MVC widget.
    /// </summary>
    public class ImageGalleryModel : MediaGalleryModelBase<SfImage>, IImageGalleryModel
    {
        #region Initialization

        /// <summary>
        /// Initializes a new instance of the <see cref="ImageGalleryModel"/> class.
        /// </summary>
        public ImageGalleryModel() : base()
        {
            this.SerializedThumbnailSizeModel = JsonSerializer.SerializeToString(this.DefaultThumbnailSize());
        }

        #endregion

        #region Properties
        /// <inheritdoc />
        public string SerializedThumbnailSizeModel { get; set; }

        /// <inheritdoc />
        public string SerializedImageSizeModel { get; set; }

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
                    if (this.SerializedThumbnailSizeModel != null)
                    {
                        this.thumbnailSizeModel = JsonSerializer.DeserializeFromString<ImageSizeModel>(this.SerializedThumbnailSizeModel);
                    }
                    else
                    {
                        this.thumbnailSizeModel = this.DefaultThumbnailSize();
                    }
                }

                return this.thumbnailSizeModel;
            }
        }

        /// <summary>
        /// Gets the size model of the image in the details view.
        /// </summary>
        /// <value>The thumbnail size model.</value>
        public ImageSizeModel ImageSizeModel
        {
            get
            {
                if (this.imageSizeModel == null)
                {
                    this.imageSizeModel = this.SerializedImageSizeModel != null ?
                        JsonSerializer.DeserializeFromString<ImageSizeModel>(this.SerializedImageSizeModel) :
                        new ImageSizeModel()
                        {
                            DisplayMode = ImageDisplayMode.Original
                        };
                }

                return this.imageSizeModel;
            }
        }
        #endregion

        #region Protected overriden methods
        /// <inheritdoc />
        protected override IQueryable<IDataItem> GetItemsQuery()
        {
            var manager = (LibrariesManager)this.GetManager();
            return manager.GetImages();
        }

        /// <inheritdoc />
        protected override Frontend.Mvc.Models.ContentDetailsViewModel CreateDetailsViewModelInstance()
        {
            return new ImageDetailsViewModel();
        }

        /// <inheritdoc />
        public Frontend.Mvc.Models.ContentDetailsViewModel CreateDetailsViewModel(IDataItem item, int? itemIndex)
        {
            var viewModel = (ImageDetailsViewModel) base.CreateDetailsViewModel(item);

            int? totalCount = 0;
            IDataItem next = null;
            IDataItem prev = null;

            if (itemIndex != null)
            {
                var query = this.GetItemsQuery();
                int? take = this.DisplayMode == ListDisplayMode.Limit ? this.LimitCount : null;
                
                query = this.UpdateExpression(query, null, take, ref totalCount);

                if (this.DisplayMode == ListDisplayMode.Limit && this.LimitCount < totalCount)
                {
                    totalCount = this.LimitCount;
                }

                if (totalCount > 1)
                {
                    if (itemIndex == 1)
                    {
                        next = query.Skip(itemIndex.Value).FirstOrDefault();
                        prev = query.LastOrDefault();
                    }
                    else if (itemIndex == totalCount)
                    {
                        next = query.FirstOrDefault();
                        prev = query.Skip(itemIndex.Value - 2).FirstOrDefault();
                    }
                    else
                    {
                        next = query.Skip(itemIndex.Value).FirstOrDefault();
                        prev = query.Skip(itemIndex.Value - 2).FirstOrDefault();
                    }
                }
            }

            viewModel.PreviousItem = prev != null ? new ItemViewModel(prev) : null;
            viewModel.NextItem = next != null ? new ItemViewModel(next) : null;
            viewModel.TotalItemsCount = totalCount.Value;

            viewModel.MediaUrl = this.GetSelectedSizeUrl((SfImage)item, this.ImageSizeModel);

            var sfImage = item as SfImage;
            if (sfImage != null && sfImage.IsVectorGraphics())
            {
                this.ApplyThumbnailProfileToViewModel(viewModel, this.ImageSizeModel);
            }

            return viewModel;
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

            foreach (ThumbnailViewModel item in viewModel.Items)
            {
                var sfImage = (SfImage)item.DataItem;

                if (sfImage.IsVectorGraphics())
                {
                    this.ApplyThumbnailProfileToViewModel(item, this.ThumbnailSizeModel);
                    this.ApplyImageSizesToViewModel(item, this.ImageSizeModel);
                }

                item.ThumbnailUrl = this.GetSelectedSizeUrl(sfImage, this.ThumbnailSizeModel);
                item.MediaUrl = this.GetSelectedSizeUrl(sfImage, this.ImageSizeModel);
            }
        }
        #endregion

        #region Protected virtual methods
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
                imageUrl = image.ResolveThumbnailUrl(sizeModel.Thumbnail.Name, urlAsAbsolute);
            }
            else
            {
                var originalImageUrl = image.ResolveMediaUrl(urlAsAbsolute);
                imageUrl = originalImageUrl;
            }

            return imageUrl;
        }
        #endregion

        #region Private methods

        private ImageSizeModel DefaultThumbnailSize()
        {
            ImageSizeModel result;
            var defaultThumbnail = ImageGalleryModel.DefaultThumbnailProfileName<Album>();
            if (defaultThumbnail != null)
            {
                result = new ImageSizeModel()
                {
                    DisplayMode = ImageDisplayMode.Thumbnail,
                    Thumbnail = new ThumbnailModel()
                    {
                        Name = ImageGalleryModel.DefaultThumbnailProfileName<Album>()
                    }
                };
            }
            else
            {
                result = new ImageSizeModel()
                {
                    DisplayMode = ImageDisplayMode.Original
                };
            }

            return result;
        }

        private void ApplyThumbnailProfileToViewModel(ThumbnailViewModel thumbnailViewModel, ImageSizeModel imageSizeModel)
        {
            int width;
            int height;

            this.GetThumbnailSizes(out width, out height, imageSizeModel);

            if (height > 0)
            {
                thumbnailViewModel.Height = height;
            }
            
            if (width > 0)
            {
                thumbnailViewModel.Width = width;
            }
        }

        private void ApplyImageSizesToViewModel(ThumbnailViewModel thumbnailViewModel, ImageSizeModel imageSizeModel)
        {
            int width;
            int height;

            this.GetThumbnailSizes(out width, out height, imageSizeModel);

            if (height > 0)
            {
                thumbnailViewModel.DetailsImageHeight = height;
            }

            if (width > 0)
            {
                thumbnailViewModel.DetailsImageWidth = width;
            }
        }

        private void ApplyThumbnailProfileToViewModel(ImageDetailsViewModel imageDetailsViewModel, ImageSizeModel imageSizeModel)
        {
            int width;
            int height;

            this.GetThumbnailSizes(out width, out height, imageSizeModel);

            if (height > 0)
            {
                imageDetailsViewModel.Height = height;
            }

            if (width > 0)
            {
                imageDetailsViewModel.Width = width;
            }
        }

        private void GetThumbnailSizes(out int width, out int height, ImageSizeModel imageSizeModel)
        {
            width = 0;
            height = 0;

            var thumbnailName = imageSizeModel.Thumbnail != null ? imageSizeModel.Thumbnail.Name : string.Empty;

            var thumbnailProfile = this.DefaultAlbumThumbnailProfile(thumbnailName);
            if (thumbnailProfile != null)
            {
                // Sets width - width is with higher priority if presents in parameters' collection
                if (thumbnailProfile.Parameters.Keys.Contains("Width"))
                {
                    width = this.GetThumbnailProfileSize(thumbnailProfile, "Width");
                }
                else if (thumbnailProfile.Parameters.Keys.Contains("MaxWidth"))
                {
                    width = this.GetThumbnailProfileSize(thumbnailProfile, "MaxWidth");
                }

                // Sets height - height is with higher priority if presents in parameters' collection
                if (thumbnailProfile.Parameters.Keys.Contains("Height"))
                {
                    height = this.GetThumbnailProfileSize(thumbnailProfile, "Height");
                }
                else if (thumbnailProfile.Parameters.Keys.Contains("MaxHeight"))
                {
                    height = this.GetThumbnailProfileSize(thumbnailProfile, "MaxHeight");
                }
            }
        }

        private int GetThumbnailProfileSize(ThumbnailProfileConfigElement thumbnailProfile, string parameterKey)
        {
            int width = 0;
            int.TryParse(thumbnailProfile.Parameters[parameterKey], out width);

            return width;
        }

        private ThumbnailProfileConfigElement DefaultAlbumThumbnailProfile(string thumbnailName)
        {
            if (!string.IsNullOrWhiteSpace(thumbnailName))
            {
                var profiles = Config.Get<LibrariesConfig>().Images.Thumbnails.Profiles;

                if (profiles.ContainsKey(thumbnailName))
                {
                    var thumbnailProfile = profiles[thumbnailName];

                    return thumbnailProfile;
                }
            }

            return null;
        }

        #endregion

        #region Private fields and constants
        private ImageSizeModel thumbnailSizeModel;
        private ImageSizeModel imageSizeModel;

        #endregion
    }
}
