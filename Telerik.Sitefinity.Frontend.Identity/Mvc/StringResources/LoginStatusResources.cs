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

        /// <summary>
        /// Gets phrase : User profile page
        /// </summary>
        [ResourceEntry("LoginPage",
            Value = "Login page",
            Description = "phrase : Login page",
            LastModified = "2015/02/24")]
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
            LastModified = "2015/02/24")]
        public string LoginPageInfo
        {
            get
            {
                return this["LoginPageInfo"];
            }
        }

        /// <summary>
        /// Gets phrase : Registration page
        /// </summary>
        [ResourceEntry("RegistrationPage",
            Value = "Registration page",
            Description = "phrase : Registration page",
            LastModified = "2015/02/24")]
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
            LastModified = "2015/02/24")]
        public string RegistrationPageInfo
        {
            get
            {
                return this["RegistrationPageInfo"];
            }
        }

        /// <summary>
        /// Gets phrase : User profile page
        /// </summary>
        [ResourceEntry("ProfilePage",
            Value = "User profile page",
            Description = "phrase : User profile page",
            LastModified = "2015/02/24")]
        public string ProfilePage
        {
            get
            {
                return this["ProfilePage"];
            }
        }

        /// <summary>
        /// Gets phrase : This is the page where you have dropped Profile widget
        /// </summary>
        [ResourceEntry("ProfilePageInfo",
            Value = "This is the page where you have dropped Profile widget",
            Description = "phrase : This is the page where you have dropped Profile widget",
            LastModified = "2015/02/24")]
        public string ProfilePageInfo
        {
            get
            {
                return this["ProfilePageInfo"];
            }
        }

        /// <summary>
        /// Gets phrase : After logout users will be redirected to...
        /// </summary>
        [ResourceEntry("LogoutPageInfo",
            Value = "After logout users will be redirected to...",
            Description = "phrase : After logout users will be redirected to...",
            LastModified = "2015/02/24")]
        public string LogoutPageInfo
        {
            get
            {
                return this["LogoutPageInfo"];
            }
        }
    }
}
