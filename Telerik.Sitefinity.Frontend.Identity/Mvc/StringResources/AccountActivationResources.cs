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
    public class AccountActivationResources : Resource
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
        /// Gets phrase : Error has occured
        /// </summary>
        [ResourceEntry("AccountActivationFailTitle",
            Value = "Error has occured",
            Description = "phrase : Error has occured",
            LastModified = "2024/09/16")]
        public string AccountActivationFailTitle
        {
            get
            {
                return this["AccountActivationFailTitle"];
            }
        }

        /// <summary>
        /// Gets phrase : We could not activate your account.
        /// </summary>
        [ResourceEntry("AccountActivationFailMessage",
            Value = "We could not activate your account.",
            Description = "phrase : We could not activate your account.",
            LastModified = "2024/09/16")]
        public string AccountActivationFailMessage
        {
            get
            {
                return this["AccountActivationFailMessage"];
            }
        }
        
        /// <summary>
        /// Gets phrase : Log in
        /// </summary>
        [ResourceEntry("LoginPageUrlTitle",
            Value = "Log in",
            Description = "phrase : Log in",
            LastModified = "2024/10/01")]
        public string LoginPageUrlTitle
        {
            get
            {
                return this["LoginPageUrlTitle"];
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
        /// Gets phrase : Login page
        /// </summary>
        [ResourceEntry("LoginPage",
            Value = "Login page",
            Description = "phrase : Login page",
            LastModified = "2024/10/01")]
        public string LoginPage
        {
            get
            {
                return this["LoginPage"];
            }
        }

        /// <summary>
        /// Gets phrase : This is the page where you have dropped Login widget
        /// </summary>
        [ResourceEntry("LoginPageInfo",
            Value = "This is the page where you have dropped Login widget",
            Description = "phrase : This is the page where you have dropped Login widget",
            LastModified = "2015/10/01")]
        public string LoginPageInfo
        {
            get
            {
                return this["LoginPageInfo"];
            }
        }

        /// <summary>
        /// Control name: Registration
        /// </summary>
        [ResourceEntry("AccountActivationWidgetTitle",
            Value = "Account activation",
            Description = "Control title: Account Activation",
            LastModified = "2019/06/03")]
        public string AccountActivationWidgetTitle
        {
            get
            {
                return this["AccountActivationWidgetTitle"];
            }
        }

        /// <summary>
        /// Control description: A widget that should be placed on the page that will be used to activate user accounts.
        /// </summary>
        [ResourceEntry("AccountActivationWidgetDescription",
            Value = "Put this widget on the page linked in the email for activation user account",
            Description = "Control description: A widget that should be placed on the page that will be used to activate user accounts.",
            LastModified = "2019/06/03")]
        public string AccountActivationWidgetDescription
        {
            get
            {
                return this["AccountActivationWidgetDescription"];
            }
        }

        /// <summary>
        /// Phrase: Activation link has expired.
        /// </summary>
        [ResourceEntry("ActivationLinkExpiredTitle",
            Value = "Activation link has expired",
            Description = "Activation link has expired",
            LastModified = "2024/09/13")]
        public string ActivationLinkExpiredTitle
        {
            get
            {
                return this["ActivationLinkExpiredTitle"];
            }
        }

        /// <summary>
        /// Phrase: To access your account resend activation link to {0}.
        /// </summary>
        [ResourceEntry("ActivationLinkExpiredMessage",
            Value = "To access your account resend activation link to {0}",
            Description = "To access your account resend activation link to {0}",
            LastModified = "2024/09/13")]
        public string ActivationLinkExpiredMessage
        {
            get
            {
                return this["ActivationLinkExpiredMessage"];
            }
        }

        /// <summary>
        /// Phrase: Send activation link.
        /// </summary>
        [ResourceEntry("SendActivationLink",
            Value = "Send activation link",
            Description = "Send activation link",
            LastModified = "2024/09/13")]
        public string SendActivationLink
        {
            get
            {
                return this["SendActivationLink"];
            }
        }

        /// <summary>
        /// Phrase: Please check your email.
        /// </summary>
        [ResourceEntry("ActivationLinkSentTitle",
            Value = "Please check your email",
            Description = "Please check your email",
            LastModified = "2024/09/13")]
        public string ActivationLinkSentTitle
        {
            get
            {
                return this["ActivationLinkSentTitle"];
            }
        }

        /// <summary>
        /// Phrase: An activation link has been sent to {0}.
        /// </summary>
        [ResourceEntry("ActivationLinkSentMessage",
            Value = "An activation link has been sent to {0}",
            Description = "An activation link has been sent to {0}",
            LastModified = "2024/09/13")]
        public string ActivationLinkSentMessage
        {
            get
            {
                return this["ActivationLinkSentMessage"];
            }
        }

        /// <summary>
        /// Phrase: Send again link.
        /// </summary>
        [ResourceEntry("SendAgainActivationLink",
            Value = "Send again",
            Description = "Send again",
            LastModified = "2024/09/13")]
        public string SendAgainActivationLink
        {
            get
            {
                return this["SendAgainActivationLink"];
            }
        }
    }
}
