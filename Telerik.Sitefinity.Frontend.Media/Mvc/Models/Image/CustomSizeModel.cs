using System;
using System.Linq;

namespace Telerik.Sitefinity.Frontend.Media.Mvc.Models.Image
{
    /// <summary>
    /// This class represents custom image size options.
    /// </summary>
    public class CustomSizeModel : IEquatable<CustomSizeModel>
    {
        /// <summary>
        /// Gets or sets the method for resizing the image. Probable values are ResizeFitToAreaArguments or CropCropArguments.
        /// </summary>
        /// <value>
        /// The method.
        /// </value>
        public string Method { get; set; }

        /// <summary>
        /// Gets or sets the maximum width. Relevant for ResizeFitToAreaArguments method.
        /// </summary>
        /// <value>
        /// The maximum width.
        /// </value>
        public int? MaxWidth { get; set; }

        /// <summary>
        /// Gets or sets the maximum height. Relevant for ResizeFitToAreaArguments method.
        /// </summary>
        /// <value>
        /// The maximum height.
        /// </value>
        public int? MaxHeight { get; set; }

        /// <summary>
        /// Gets or sets the width. Relevant for CropCropArguments method.
        /// </summary>
        /// <value>
        /// The width.
        /// </value>
        public int? Width { get; set; }

        /// <summary>
        /// Gets or sets the height. Relevant for CropCropArguments method.
        /// </summary>
        /// <value>
        /// The height.
        /// </value>
        public int? Height { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to resize smaller images to bigger dimensions.
        /// </summary>
        /// <value>
        ///   <c>true</c> if smaller images should be resizet to bigger dimensions; otherwise, <c>false</c>.
        /// </value>
        public bool? ScaleUp { get; set; }

        /// <summary>
        /// Gets or sets the image quality.
        /// </summary>
        /// <value>
        /// The quality.
        /// </value>
        public ImageQuality? Quality { get; set; }

        /// <summary>
        /// Equalses the specified other.
        /// </summary>
        /// <param name="other">The other.</param>
        /// <returns></returns>
        public bool Equals(CustomSizeModel other)
        {
            if (other == null)
            {
                return false;
            }
            return this.Height == other.Height &&
                   this.MaxHeight == other.MaxHeight &&
                   this.MaxWidth == other.MaxWidth &&
                   this.Method == other.Method &&
                   this.Quality == other.Quality &&
                   this.ScaleUp == other.ScaleUp &&
                   this.Width == other.Width;
        }
    }
}
