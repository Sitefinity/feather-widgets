using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Localization.Data;

namespace Telerik.Sitefinity.Frontend.Identity.Mvc.StringResources
{
    /// <summary>
    /// Localizable strings for the Profile widget
    /// </summary>
    [ObjectInfo(typeof(ProfileResources), Title = "ProfileResources", Description = "ProfileResources")]
    public class ProfileResources : Resource
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ProfileResources"/> class. 
        /// Initializes new instance of <see cref="ProfileResources"/> class with the default <see cref="ResourceDataProvider"/>.
        /// </summary>
        public ProfileResources()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProfileResources"/> class.
        /// </summary>
        /// <param name="dataProvider">The data provider.</param>
        public ProfileResources(ResourceDataProvider dataProvider)
            : base(dataProvider)
        {
        }

        #endregion

        /// <summary>
        /// Gets phrase : CSS classes
        /// </summary>
        [ResourceEntry("CssClasses",
            Value = "CSS classes",
            Description = "phrase : CSS classes",
            LastModified = "2015/03/04")]
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
            LastModified = "2015/03/04")]
        public string Template
        {
            get
            {
                return this["Template"];
            }
        }

        /// <summary>
        /// Gets phrase : More options
        /// </summary>
        [ResourceEntry("MoreOptions",
            Value = "More options",
            Description = "phrase : More options",
            LastModified = "2015/03/04")]
        public string MoreOptions
        {
            get
            {
                return this["MoreOptions"];
            }
        }
    }
}
