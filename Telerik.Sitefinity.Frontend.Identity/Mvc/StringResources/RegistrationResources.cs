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

        /// <summary>
        /// Gets phrase: First name
        /// </summary>
        [ResourceEntry("FirstName",
            Value = "First name",
            Description = "phrase : First name",
            LastModified = "2015/02/26")]
        public string FirstName
        {
            get
            {
                return this["FirstName"];
            }
        }

        /// <summary>
        /// Gets phrase: Last name
        /// </summary>
        [ResourceEntry("LastName",
            Value = "Last name",
            Description = "phrase : Last name",
            LastModified = "2015/02/26")]
        public string LastName
        {
            get
            {
                return this["LastName"];
            }
        }

        /// <summary>
        /// Gets phrase: Email
        /// </summary>
        [ResourceEntry("Email",
            Value = "Email",
            Description = "phrase : Email",
            LastModified = "2015/02/26")]
        public string Email
        {
            get
            {
                return this["Email"];
            }
        }

        /// <summary>
        /// Gets phrase: Username
        /// </summary>
        [ResourceEntry("Username",
            Value = "Username",
            Description = "phrase : Username",
            LastModified = "2015/02/26")]
        public string Username
        {
            get
            {
                return this["Username"];
            }
        }

        /// <summary>
        /// Gets phrase: Password
        /// </summary>
        [ResourceEntry("Password",
            Value = "Password",
            Description = "phrase : Password",
            LastModified = "2015/02/26")]
        public string Password
        {
            get
            {
                return this["Password"];
            }
        }

        /// <summary>
        /// Gets phrase: Re-type password
        /// </summary>
        [ResourceEntry("ReTypePassword",
            Value = "Re-type password",
            Description = "phrase : Re-type password",
            LastModified = "2015/02/26")]
        public string ReTypePassword
        {
            get
            {
                return this["ReTypePassword"];
            }
        }

        /// <summary>
        /// Gets phrase: Register
        /// </summary>
        [ResourceEntry("Register",
            Value = "Register",
            Description = "phrase : Register",
            LastModified = "2015/02/26")]
        public string Register
        {
            get
            {
                return this["Register"];
            }
        }

        /// <summary>
        /// Gets phrase: When the form is successfully submitted...
        /// </summary>
        [ResourceEntry("FormSuccessfullySubmited",
            Value = "When the form is successfully submitted...",
            Description = "phrase : When the form is successfully submitted...",
            LastModified = "2015/02/27")]
        public string FormSuccessfullySubmited
        {
            get
            {
                return this["FormSuccessfullySubmited"];
            }
        }

        /// <summary>
        /// Gets phrase: Show message
        /// </summary>
        [ResourceEntry("ShowMsg",
            Value = "Show message",
            Description = "phrase : Show message",
            LastModified = "2015/02/27")]
        public string ShowMsg
        {
            get
            {
                return this["ShowMsg"];
            }
        }

        /// <summary>
        /// Gets phrase: Open a specially prepared page...
        /// </summary>
        [ResourceEntry("OpenPage",
            Value = "Open a specially prepared page...",
            Description = "phrase : Open a specially prepared page...",
            LastModified = "2015/02/27")]
        public string OpenPage
        {
            get
            {
                return this["OpenPage"];
            }
        }
    }
}
