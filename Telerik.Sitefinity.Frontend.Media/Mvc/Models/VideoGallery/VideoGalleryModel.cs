using System;
using System.Linq;
using ServiceStack.Text;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Frontend.Mvc.Models;
using Telerik.Sitefinity.Libraries.Model;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.Libraries;
using Telerik.Sitefinity.Services;
using SfVideo = Telerik.Sitefinity.Libraries.Model.Video;
using Telerik.Sitefinity.Frontend.Media.Mvc.Models.Image;

namespace Telerik.Sitefinity.Frontend.Media.Mvc.Models.VideoGallery
{
    /// <summary>
    /// This class is used as a model for the <see cref="VideoGalleryController"/>.
    /// </summary>
    public class VideoGalleryModel : MediaGalleryModelBase<SfVideo>, IVideoGalleryModel
    {
        #region Initialization

        /// <summary>
        /// Initializes a new instance of the <see cref="VideoGalleryModel"/> class.
        /// </summary>
        public VideoGalleryModel() : base()
        {
            this.SerializedThumbnailSizeModel = JsonSerializer.SerializeToString(this.DefaultThumbnailSize());
            this.SerializedVideoSizeViewModel = JsonSerializer.SerializeToString(this.DefaultVideoSize());
        }

        #endregion

        #region Public methods

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
                if (this.SerializedThumbnailSizeModel != null)
                {
                    this.thumbnailSizeModel = JsonSerializer.DeserializeFromString<ImageSizeModel>(this.SerializedThumbnailSizeModel);
                }
                else
                {
                    this.thumbnailSizeModel = this.DefaultThumbnailSize();
                }

                return this.thumbnailSizeModel;
            }
        }

        /// <inheritdoc />
        public string SerializedVideoSizeViewModel { get; set; }

        /// <summary>
        /// Gets the video size view model.
        /// </summary>
        /// <value>
        /// The video size view model.
        /// </value>
        public VideoSizeViewModel VideoSizeViewModel
        {
            get
            {
                if (string.IsNullOrEmpty(this.SerializedVideoSizeViewModel))
                {
                    this.videoSizeViewModel = this.DefaultVideoSize();
                }
                else
                {
                    this.videoSizeViewModel = JsonSerializer.DeserializeFromString<VideoSizeViewModel>(this.SerializedVideoSizeViewModel);
                }

                return this.videoSizeViewModel;
            }
        }

        #endregion 

        #region Protected overriden methods

        /// <inheritdoc />
        protected override IQueryable<IDataItem> GetItemsQuery()
        {
            var manager = (LibrariesManager)this.GetManager();
            return manager.GetVideos();
        }


        /// <inheritdoc />
        protected override Frontend.Mvc.Models.ContentDetailsViewModel CreateDetailsViewModelInstance()
        {
            return new VideoDetailsViewModel();
        }

        /// <inheritdoc />
        public ContentDetailsViewModel CreateDetailsViewModel(IDataItem item, int? itemIndex)
        {
            var viewModel = (VideoDetailsViewModel)base.CreateDetailsViewModel(item);

            int? totalCount = 0;
            IDataItem next = null;
            IDataItem prev = null;

            if (itemIndex != null)
            {
                var query = this.GetItemsQuery();
                int? take = this.DisplayMode == ListDisplayMode.Limit ? this.ItemsPerPage : null;

                query = this.UpdateExpression(query, null, take, ref totalCount);

                if (this.DisplayMode == ListDisplayMode.Limit && this.ItemsPerPage < totalCount)
                {
                    totalCount = this.ItemsPerPage;
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
            viewModel.MediaUrl = ((SfVideo)item).MediaUrl;

            return viewModel;
        }

        /// <inheritdoc />
        protected override ItemViewModel CreateItemViewModelInstance(IDataItem item)
        {
            return new VideoThumbnailViewModel(item);
        }

        /// <inheritdoc />
        protected override void PopulateListViewModel(int page, IQueryable<IDataItem> query, ContentListViewModel viewModel)
        {
            base.PopulateListViewModel(page, query, viewModel);

            foreach (var item in viewModel.Items)
            {
                ((VideoThumbnailViewModel)item).ThumbnailUrl = this.GetSelectedSizeUrl((SfVideo)item.DataItem, this.ThumbnailSizeModel);
            }
        }

        #endregion

        #region Protected virtual methods

        /// <summary>
        /// Gets the thumbnail URL for the selected size.
        /// </summary>
        /// <param name="video">The video.</param>
        /// <param name="sizeModel">The size model.</param>
        /// <returns></returns>
        protected virtual string GetSelectedSizeUrl(SfVideo video, ImageSizeModel sizeModel)
        {
            if (video.Id == Guid.Empty)
                return string.Empty;
                        
            var urlAsAbsolute = Config.Get<SystemConfig>().SiteUrlSettings.GenerateAbsoluteUrls;

            string videoThumbnailUrl;
            if (sizeModel.DisplayMode == ImageDisplayMode.Thumbnail && !string.IsNullOrWhiteSpace(sizeModel.Thumbnail.Name))
            {
                videoThumbnailUrl = video.ResolveThumbnailUrl(sizeModel.Thumbnail.Name, urlAsAbsolute);
            }
            else
            {
                videoThumbnailUrl = video.ResolveThumbnailUrl("0", urlAsAbsolute);
            }

            return videoThumbnailUrl;
        }
        #endregion

        #region Private methods

        private ImageSizeModel DefaultThumbnailSize()
        {
            ImageSizeModel result;
            var defaultThumbnail = VideoGalleryModel.DefaultThumbnailProfileName<VideoLibrary>();
            if (defaultThumbnail != null)
            {
                result = new ImageSizeModel()
                {
                    DisplayMode = ImageDisplayMode.Thumbnail,
                    Thumbnail = new ThumbnailModel()
                    {
                        Name = VideoGalleryModel.DefaultThumbnailProfileName<VideoLibrary>()
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

        private VideoSizeViewModel DefaultVideoSize()
        {
            return new VideoSizeViewModel()
            {
                AspectRatio = "Auto"
            };
        }

        #endregion

        #region Private fields and constants

        private ImageSizeModel thumbnailSizeModel;
        private VideoSizeViewModel videoSizeViewModel;

        #endregion
    }
}
