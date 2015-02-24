using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Localization.Data;

namespace Telerik.Sitefinity.Frontend.Identity.Mvc.StringResources
{
    /// <summary>
    /// Localizable strings for the Login Status widget
    /// </summary>
    [ObjectInfo(typeof(LoginStatusResources), Title = "LoginStatusResources", Description = "LoginStatusResources")]
    public class LoginStatusResources : Resource
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="LoginStatusResources"/> class. 
        /// Initializes new instance of <see cref="LoginStatusResources"/> class with the default <see cref="ResourceDataProvider"/>.
        /// </summary>
        public LoginStatusResources()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LoginStatusResources"/> class.
        /// </summary>
        /// <param name="dataProvider">The data provider.</param>
        public LoginStatusResources(ResourceDataProvider dataProvider)
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
            LastModified = "2015/02/23")]
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
            LastModified = "2015/02/23")]
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
            LastModified = "2015/02/24")]
        public string MoreOptions
        {
            get
            {
                return this["MoreOptions"];
            }
        }
    }
}
