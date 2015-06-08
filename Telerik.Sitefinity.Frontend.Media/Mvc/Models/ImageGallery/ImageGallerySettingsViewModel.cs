using System;
using System.Linq;
using Telerik.Sitefinity.Frontend.Mvc.Models;

namespace Telerik.Sitefinity.Frontend.Media.Mvc.Models.ImageGallery
{
    /// <summary>
    /// View model for the settings of the image gallery model.
    /// </summary>
    public class ImageGallerySettingsViewModel : ContentListSettingsViewModel
    {
        /// <summary>
        /// Gets or sets the serialized thumbnail size model. It determines the size of the gallery's thumbnails.
        /// </summary>
        /// <value>
        /// The serialized selected parents ids.
        /// </value>
        public string SerializedThumbnailSizeModel { get; set; }

        /// <summary>
        /// Gets or sets the serialized single image size model. It determines the size of the image in the details view.
        /// </summary>
        /// <value>
        /// The serialized selected parents ids.
        /// </value>
        public string SerializedImageSizeModel { get; set; }
    }
}
