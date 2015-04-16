using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Frontend.Mvc.Models;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.Libraries;
using Telerik.Sitefinity.Services;
using SfVideo = Telerik.Sitefinity.Libraries.Model.Video;

namespace Telerik.Sitefinity.Frontend.Media.Mvc.Models.VideoGallery
{
    /// <summary>
    /// A model for the Video Gallery MVC widget.
    /// </summary>
    public class VideoGalleryModel : MediaGalleryModelBase<SfVideo>, IVideoGalleryModel
    {
            #region Initialization

        /// <summary>
        /// Initializes a new instance of the <see cref="VideoGalleryModel"/> class.
        /// </summary>
        public VideoGalleryModel() : base()
        {
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
        public Frontend.Mvc.Models.ContentDetailsViewModel CreateDetailsViewModel(IDataItem item, int? itemIndex)
        {
            var viewModel = (VideoDetailsViewModel) base.CreateDetailsViewModel(item);

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

            //viewModel.MediaUrl = this.GetSelectedSizeUrl((SfVideo)item, this.VideoSizeModel);

            return viewModel;
        }

        #endregion
    }
}

