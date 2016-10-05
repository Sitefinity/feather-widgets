﻿using System;
using System.Linq;
using ServiceStack.Text;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Frontend.Media.Mvc.Models.Image;
using Telerik.Sitefinity.Frontend.Mvc.Models;
using Telerik.Sitefinity.Libraries.Model;
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

            foreach (var item in viewModel.Items)
            {
                ((ThumbnailViewModel)item).ThumbnailUrl = this.GetSelectedSizeUrl((SfImage)item.DataItem, this.ThumbnailSizeModel);
                ((ThumbnailViewModel)item).MediaUrl = this.GetSelectedSizeUrl((SfImage)item.DataItem, this.ImageSizeModel);
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

        #endregion

        #region Private fields and constants
        private ImageSizeModel thumbnailSizeModel;
        private ImageSizeModel imageSizeModel;

        #endregion
    }
}
