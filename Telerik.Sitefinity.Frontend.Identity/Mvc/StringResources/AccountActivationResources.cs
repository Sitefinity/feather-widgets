using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Localization.Data;

namespace Telerik.Sitefinity.Frontend.Identity.Mvc.StringResources
{
    /// <summary>
    /// Localizable strings for the Change Password widget
    /// </summary>
    [ObjectInfo(typeof(LoginStatusResources), Title = "ChangePasswordResources", Description = "ChangePasswordResources")]
    class AccountActivationResources : Resource
    {
        #region Constructors

        /// <summary>
        /// Initializes new instance of <see cref="AccountActivationResources"/> class with the default <see cref="ResourceDataProvider"/>.
        /// </summary>
        public AccountActivationResources()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AccountActivationResources"/> class.
        /// </summary>
        /// <param name="dataProvider">The data provider.</param>
        public AccountActivationResources(ResourceDataProvider dataProvider)
            : base(dataProvider)
        {
        }

        #endregion
        
        /// <summary>
        /// Gets phrase : Your account is activated
        /// </summary>
        [ResourceEntry("AccountActivationSuccess",
            Value = "Your account is activated",
            Description = "phrase : Your account is activated",
            LastModified = "2015/03/03")]
        public string AccountActivationSuccess
        {
            get
            {
                return this["AccountActivationSuccess"];
            }
        }

        /// <summary>
        /// Gets phrase : Go to your profile
        /// </summary>
        [ResourceEntry("ProfilePageUrlTitle",
            Value = "Go to your profile",
            Description = "phrase : Go to your profile",
            LastModified = "2015/03/03")]
        public string ProfilePageUrlTitle
        {
            get
            {
                return this["ProfilePageUrlTitle"];
            }
        }

        /// <summary>
        /// Gets phrase : CSS classes
        /// </summary>
        [ResourceEntry("CssClasses",
            Value = "CSS classes",
            Description = "phrase : CSS classes",
            LastModified = "2015/03/02")]
        public string CssClasses
        {
            get
            {
                return this["CssClasses"];
            }
        }

        /// <summary>
        /// Gets phrase : More options
        /// </summary>
        [ResourceEntry("MoreOptions",
            Value = "More options",
            Description = "phrase : More options",
            LastModified = "2015/03/03")]
        public string MoreOptions
        {
            get
            {
                return this["MoreOptions"];
            }
        }

        /// <summary>
        /// Gets phrase : Template
        /// </summary>
        [ResourceEntry("Template",
            Value = "Template",
            Description = "phrase : Template",
            LastModified = "2015/03/03")]
        public string Template
        {
            get
            {
                return this["Template"];
            }
        }

        /// <summary>
        /// Gets phrase : Profile page
        /// </summary>
        [ResourceEntry("ProfilePage",
            Value = "Profile page",
            Description = "phrase : Profile page",
            LastModified = "2015/03/04")]
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
            LastModified = "2015/03/03")]
        public string ProfilePageInfo
        {
            get
            {
                return this["ProfilePageInfo"];
            }
        }
    }
}
