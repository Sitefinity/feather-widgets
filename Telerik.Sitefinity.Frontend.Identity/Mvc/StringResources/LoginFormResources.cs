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
    [ObjectInfo(typeof(LoginFormResources), Title = "LoginFormResources", Description = "LoginFormResources")]
    public class LoginFormResources : Resource
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="LoginFormResources"/> class. 
        /// Initializes new instance of <see cref="LoginFormResources"/> class with the default <see cref="ResourceDataProvider"/>.
        /// </summary>
        public LoginFormResources()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LoginFormResources"/> class.
        /// </summary>
        /// <param name="dataProvider">The data provider.</param>
        public LoginFormResources(ResourceDataProvider dataProvider)
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
            LastModified = "2015/02/25")]
        public string MoreOptions
        {
            get
            {
                return this["MoreOptions"];
            }
        }

        /// <summary>
        /// Gets phrase : This is the page to which user should be authenticated immediately after login.
        /// </summary>
        [ResourceEntry("LandingPageAfterLoginInfo",
            Value = "After login users will be redirected to...",
            Description = "phrase : After login users will be redirected to...",
            LastModified = "2015/02/25")]
        public string LandingPageAfterLoginInfo
        {
            get
            {
                return this["LandingPageAfterLoginInfo"];
            }
        }

        /// <summary>
        /// Gets phrase : Registration page
        /// </summary>
        [ResourceEntry("RegistrationPage",
            Value = "Registration page",
            Description = "phrase : Registration page",
            LastModified = "2015/02/25")]
        public string RegistrationPage
        {
            get
            {
                return this["RegistrationPage"];
            }
        }

        /// <summary>
        /// Gets phrase : This is the page where you have dropped Registration widget
        /// </summary>
        [ResourceEntry("RegistrationPageInfo",
            Value = "This is the page where you have dropped Registration widget",
            Description = "phrase : This is the page where you have dropped Registration widget",
            LastModified = "2015/02/25")]
        public string RegistrationPageInfo
        {
            get
            {
                return this["RegistrationPageInfo"];
            }
        }

        /// <summary>
        /// Gets phrase : Show "Remember me" checkbox
        /// </summary>
        [ResourceEntry("RememberMeInfo",
            Value = "Show \"Remember me\" checkbox",
            Description = "phrase : Show \"Remember me\" checkbox",
            LastModified = "2015/02/25")]
        public string RememberMeInfo
        {
            get
            {
                return this["RememberMeInfo"];
            }
        }
    }
}
