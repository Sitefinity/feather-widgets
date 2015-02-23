using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Localization.Data;

namespace Telerik.Sitefinity.Frontend.Media.Mvc.StringResources
{
    /// <summary>
    /// Localizable strings for the Image Gallery widget
    /// </summary>
    [ObjectInfo(typeof(ImageGalleryResources), Title = "ImageGalleryResources", Description = "ImageGalleryResources")]
    public class ImageGalleryResources : Resource
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ImageGalleryResources"/> class. 
        /// Initializes new instance of <see cref="ImageGalleryResources"/> class with the default <see cref="ResourceDataProvider"/>.
        /// </summary>
        public ImageGalleryResources()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ImageGalleryResources"/> class.
        /// </summary>
        /// <param name="dataProvider">The data provider.</param>
        public ImageGalleryResources(ResourceDataProvider dataProvider)
            : base(dataProvider)
        {
        }

        #endregion

        /// <summary>
        /// Gets phrase : More options
        /// </summary>
        [ResourceEntry("MoreOptions",
            Value = "More options",
            Description = "phrase : More options",
            LastModified = "2015/02/19")]
        public string MoreOptions
        {
            get
            {
                return this["MoreOptions"];
            }
        }

        /// <summary>
        /// Gets phrase : CSS classes
        /// </summary>
        [ResourceEntry("CssClasses",
            Value = "CSS classes",
            Description = "phrase : CSS classes",
            LastModified = "2015/02/19")]
        public string CssClasses
        {
            get
            {
                return this["CssClasses"];
            }
        }
    }
}
