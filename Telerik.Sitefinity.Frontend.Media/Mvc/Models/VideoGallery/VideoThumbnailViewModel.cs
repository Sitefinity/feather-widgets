using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telerik.Sitefinity.Frontend.Mvc.Models;
using Telerik.Sitefinity.Model;

namespace Telerik.Sitefinity.Frontend.Media.Mvc.Models.VideoGallery
{
    /// <inheritdoc />
    public class VideoThumbnailViewModel : ItemViewModel
    {
        /// <inheritdoc />
        public VideoThumbnailViewModel(IDataItem item)
            : base(item)
        {
        }

        /// <summary>
        /// Gets or sets the thumbnail URL of the item.
        /// </summary>
        /// <value>The thumbnail URL.</value>
        public string ThumbnailUrl { get; set; }

        /// <summary>
        /// Gets or sets the duration of the video.
        /// </summary>
        /// <value>
        /// The duration of the video.
        /// </value>
        public int VideoDuration { get; set; }
    }
}
