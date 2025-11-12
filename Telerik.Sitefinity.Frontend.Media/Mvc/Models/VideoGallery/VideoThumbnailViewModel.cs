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
        /// Gets or sets the thumbnail profile of the item.
        /// </summary>
        /// <value>The thumbnail URL.</value>
        public string ProfileName { get; set; }

        /// <summary>
        /// Gets or sets the thumbnail URL of the item.
        /// </summary>
        /// <value>The thumbnail URL.</value>
        public string ThumbnailUrl { get; set; }

        /// <summary>
        /// Gets or sets the thumbnail max width.
        /// </summary>
        public int? MaxWidth { get; set; }

        /// <summary>
        /// Gets or sets the thumbnail max height.
        /// </summary>
        public int? MaxHeight { get; set; }

        /// <summary>
        /// Gets or sets the thumbnail width.
        /// </summary>
        public int? Width { get; set; }

        /// <summary>
        /// Gets or sets the thumbnail height.
        /// </summary>
        public int? Height { get; set; }
    }
}
