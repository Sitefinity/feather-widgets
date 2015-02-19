using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Localization.Data;

namespace Telerik.Sitefinity.Frontend.Media.Mvc.StringResources
{
    /// <summary>
    /// Localizable strings for the Image widget
    /// </summary>
    [ObjectInfo(typeof(ImageResources), Title = "ImageResources", Description = "ImageResources")]
    public class ImageResources : Resource
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ImageResources"/> class. 
        /// Initializes new instance of <see cref="ImageResources"/> class with the default <see cref="ResourceDataProvider"/>.
        /// </summary>
        public ImageResources()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ImageResources"/> class.
        /// </summary>
        /// <param name="dataProvider">The data provider.</param>
        public ImageResources(ResourceDataProvider dataProvider)
            : base(dataProvider)
        {
        }

        #endregion

        /// <summary>
        /// Gets Resources for Comments
        /// </summary>
        [ResourceEntry("SelectImage",
            Value = "Select image",
            Description = "The phrase that will show when the widget has no image selected.",
            LastModified = "2015/02/18")]
        public string SelectImage
        {
            get
            {
                return this["SelectImage"];
            }
        }

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

        /// <summary>
        /// Gets phrase: Template
        /// </summary>
        [ResourceEntry("Template",
            Value = "Template",
            Description = "phrase : Template",
            LastModified = "2015/02/19")]
        public string Template
        {
            get
            {
                return this["Template"];
            }
        }
    }
}
