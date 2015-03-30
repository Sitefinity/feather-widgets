using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Localization.Data;

namespace Telerik.Sitefinity.Frontend.Identity.Mvc.StringResources
{
    /// <summary>
    /// Localizable strings for the Account Activation widget
    /// </summary>
    [ObjectInfo(typeof(AccountActivationResources), ResourceClassId = "AccountActivationResources", Title = "AccountActivationResourcesTitle", Description = "AccountActivationResourcesDescription")]
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

        #region Class Description

        /// <summary>
        /// Gets Title for the Account activation widget resources class.
        /// </summary>
        [ResourceEntry("AccountActivationResourcesTitle",
            Value = "Account activation widget resources",
            Description = "Title for the Account activation widget resources class.",
            LastModified = "2015/03/30")]
        public string AccountActivationResourcesTitle
        {
            get
            {
                return this["AccountActivationResourcesTitle"];
            }
        }

        /// <summary>
        /// Gets Description for the Account activation widget resources class.
        /// </summary>
        [ResourceEntry("AccountActivationResourcesDescription",
            Value = "Localizable strings for the Account activation widget.",
            Description = "Description for the Account activation widget resources class.",
            LastModified = "2015/03/30")]
        public string AccountActivationResourcesDescription
        {
            get
            {
                return this["AccountActivationResourcesDescription"];
            }
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
        /// Gets phrase : Your account could not be activated
        /// </summary>
        [ResourceEntry("AccountActivationFail",
            Value = "Your account could not be activated",
            Description = "phrase : Your account could not be activated",
            LastModified = "2015/03/03")]
        public string AccountActivationFail
        {
            get
            {
                return this["AccountActivationFail"];
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
