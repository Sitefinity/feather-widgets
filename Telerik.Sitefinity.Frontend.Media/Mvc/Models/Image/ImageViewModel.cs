using System.ComponentModel;
using Telerik.Sitefinity.Frontend.Mvc.Models;

namespace Telerik.Sitefinity.Frontend.Media.Mvc.Models.Image
{
    /// <summary>
    /// This class represents view model for Image content.
    /// </summary>
    public class ImageViewModel
    {
        /// <summary>
        /// Gets or sets the item.
        /// </summary>
        /// <value>
        /// The item.
        /// </value>
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public ItemViewModel Item { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>
        /// The title.
        /// </value>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the alternative text.
        /// </summary>
        /// <value>
        /// The alternative text.
        /// </value>
        public string AlternativeText { get; set; }

        /// <summary>
        /// Gets or sets the css class.
        /// </summary>
        /// <value>
        /// The css class.
        /// </value>
        public string CssClass { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to use image as link.
        /// </summary>
        /// <value>
        ///   <c>true</c> if should use image as link; otherwise, <c>false</c>.
        /// </value>
        public bool UseAsLink { get; set; }

        /// <summary>
        /// Gets or sets the content URL use as link.
        /// </summary>
        /// <value>
        /// The content URL use as link.
        /// </value>
        public string LinkedContentUrl { get; set; }

        /// <summary>
        /// Gets or sets whether the Image will be displayed in its original size or in a thumbnail.
        /// </summary>
        /// <value>
        /// The display mode.
        /// </value>
        public ImageDisplayMode DisplayMode { get; set; }

        /// <summary>
        /// Gets or sets the name of the thumbnail.
        /// </summary>
        /// <value>
        /// The name of the thumbnail.
        /// </value>
        public string ThumbnailName { get; set; }

        /// <summary>
        /// Gets or sets the thumbnail URL.
        /// </summary>
        /// <value>
        /// The thumbnail URL.
        /// </value>
        public string ThumbnailUrl { get; set; }

        /// <summary>
        /// Gets or sets the selected size URL.
        /// </summary>
        /// <value>
        /// The selected size URL.
        /// </value>
        public string SelectedSizeUrl { get; set; }

        /// <summary>
        /// Gets or sets the size of the custom thumbnail that is selected.
        /// </summary>
        /// <value>
        /// The size of the custom thumbnail serialized as JSON.
        /// </value>
        public CustomSizeModel CustomSize { get; set; }

        /// <summary>
        /// This class represents custom image size options.
        /// </summary>
        public class CustomSizeModel
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
        }
    }
}
