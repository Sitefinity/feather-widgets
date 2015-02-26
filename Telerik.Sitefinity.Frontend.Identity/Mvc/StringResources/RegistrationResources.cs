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
    [ObjectInfo(typeof(RegistrationResources), Title = "RegistrationResources", Description = "RegistrationResources")]
    public class RegistrationResources : Resource
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="RegistrationResources"/> class. 
        /// Initializes new instance of <see cref="RegistrationResources"/> class with the default <see cref="ResourceDataProvider"/>.
        /// </summary>
        public RegistrationResources()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RegistrationResources"/> class.
        /// </summary>
        /// <param name="dataProvider">The data provider.</param>
        public RegistrationResources(ResourceDataProvider dataProvider)
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
            LastModified = "2015/02/26")]
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
            LastModified = "2015/02/26")]
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
            LastModified = "2015/02/26")]
        public string MoreOptions
        {
            get
            {
                return this["MoreOptions"];
            }
        }

        /// <summary>
        /// Gets phrase : User profile page
        /// </summary>
        [ResourceEntry("LoginPage",
            Value = "Login page",
            Description = "phrase : Login page",
            LastModified = "2015/02/26")]
        public string LoginPage
        {
            get
            {
                return this["LoginPage"];
            }
        }

        /// <summary>
        /// Gets phrase : This is the page where you have dropped Profile widget
        /// </summary>
        [ResourceEntry("LoginPageInfo",
            Value = "This is the page where you have dropped Login form widget",
            Description = "phrase : This is the page where you have dropped Login form widget",
            LastModified = "2015/02/26")]
        public string LoginPageInfo
        {
            get
            {
                return this["LoginPageInfo"];
            }
        }

        /// <summary>
        /// Gets phrase : Account activation
        /// </summary>
        [ResourceEntry("AccountActivation",
            Value = "Account activation",
            Description = "phrase : Account activation",
            LastModified = "2015/02/26")]
        public string AccountActivation
        {
            get
            {
                return this["AccountActivation"];
            }
        }

        /// <summary>
        /// Gets phrase: General
        /// </summary>
        [ResourceEntry("General",
            Value = "General",
            Description = "phrase : General",
            LastModified = "2015/02/26")]
        public string General
        {
            get
            {
                return this["General"];
            }
        }
    }
}
