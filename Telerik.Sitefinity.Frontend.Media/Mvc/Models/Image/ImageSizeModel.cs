using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Telerik.Sitefinity.Frontend.Media.Mvc.Models.Image
{
    /// <summary>
    /// This class represents the possible size options to display an image.
    /// </summary>
    public class ImageSizeModel
    {
        /// <summary>
        /// Gets or sets the name of the used thumbnail.
        /// </summary>
        /// <value>The name of the thumbnail.</value>
        public ThumbnailModel Thumbnail { get; set; }

        /// <summary>
        /// Gets or sets the display size mode.
        /// </summary>
        /// <value>The display mode.</value>
        public ImageDisplayMode DisplayMode { get; set; }

        /// <summary>
        /// Gets or sets the custom image size options.
        /// </summary>
        /// <value>The size of the custom.</value>
        public CustomSizeModel CustomSize { get; set; }
    }
}
