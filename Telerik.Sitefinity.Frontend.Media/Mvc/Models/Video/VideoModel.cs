using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telerik.Sitefinity.Modules.Libraries;

namespace Telerik.Sitefinity.Frontend.Media.Mvc.Models.Video
{
    /// <summary>
    /// Provides API for working with <see cref="Telerik.Sitefinity.Libraries.Model.Video"/> items.
    /// </summary>
    public class VideoModel: IVideoModel
    {
        #region Properties

        /// <inheritdoc />
        public Guid Id { get; set; }

        /// <inheritdoc />
        public string ProviderName { get; set; }

        /// <inheritdoc />
        public string AspectRatio { get; set; }

        /// <inheritdoc />
        public string CssClass { get; set; }

        #endregion

        #region Public methods

        /// <inheritdoc />
        public virtual VideoViewModel GetViewModel()
        {
            var viewModel = new VideoViewModel();
            viewModel.CssClass = this.CssClass;

            if (this.Id != Guid.Empty)
            {
                LibrariesManager librariesManager = LibrariesManager.GetManager(this.ProviderName);
                var videoItem = librariesManager.GetVideos()
                    .Where(i => i.Id == this.Id)
                    .SingleOrDefault();

                if (videoItem == null)
                    return viewModel;

                viewModel.MediaUrl = videoItem.ResolveMediaUrl();
                viewModel.Title = videoItem.Title;
                viewModel.FileSize = (long)Math.Ceiling(videoItem.TotalSize / 1024d);
                viewModel.Extension = videoItem.Extension.Length > 0 ? videoItem.Extension.Remove(0, 1) : string.Empty;
                viewModel.HasSelectedVideo = true;
            }

            return viewModel;
        }

        #endregion
    }
}
