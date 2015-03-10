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
        /// Gets phrase : Activate accounts...
        /// </summary>
        [ResourceEntry("ActivateAccounts",
            Value = "Activate accounts...",
            Description = "phrase:Activate accounts...",
            LastModified = "2015/02/27")]
        public string ActivateAccounts
        {
            get
            {
                return this["ActivateAccounts"];
            }
        }

        /// <summary>
        /// Gets phrase : Immediately
        /// </summary>
        [ResourceEntry("Immediately",
            Value = "Immediately",
            Description = "Account activation method : Immediately",
            LastModified = "2015/02/27")]
        public string Immediately
        {
            get
            {
                return this["Immediately"];
            }
        }

        /// <summary>
        /// Gets phrase : Send an email for successful registration
        /// </summary>
        [ResourceEntry("SendEmailAfterRegistration",
            Value = "Send an email for successful registration",
            Description = "phrase: Send an email for successful registration",
            LastModified = "2015/02/27")]
        public string SendEmailAfterRegistration
        {
            get
            {
                return this["SendEmailAfterRegistration"];
            }
        }

        /// <summary>
        /// Gets phrase : By confirmation link sent to user email
        /// </summary>
        [ResourceEntry("ByConfirmationEmail",
            Value = "By confirmation link sent to user email",
            Description = "Account activation method : By confirmation link sent to user email",
            LastModified = "2015/02/27")]
        public string ByConfirmationEmail
        {
            get
            {
                return this["ByConfirmationEmail"];
            }
        }

        /// <summary>
        /// Gets phrase : Activation page
        /// </summary>
        [ResourceEntry("ActivationPage",
            Value = "Activation page",
            Description = "phrase: Activation page",
            LastModified = "2015/02/27")]
        public string ActivationPage
        {
            get
            {
                return this["ActivationPage"];
            }
        }

        /// <summary>
        /// Activation page description
        /// </summary>
        [ResourceEntry("ActivationPageDescription",
            Value = "This is the page which user will open by confirmation link. There you have dropped Account activation widget",
            Description = "Activation page description",
            LastModified = "2015/02/27")]
        public string ActivationPageDescription
        {
            get
            {
                return this["ActivationPageDescription"];
            }
        }

        /// <summary>
        /// Get phrase: Confirmation email template
        /// </summary>
        [ResourceEntry("ConfirmationEmailTemplate",
            Value = "Confirmation email template",
            Description = "phrase:Confirmation email template",
            LastModified = "2015/02/27")]
        public string ConfirmationEmailTemplate
        {
            get
            {
                return this["ConfirmationEmailTemplate"];
            }
        }

        /// <summary>
        /// Get phrase: Success email template
        /// </summary>
        [ResourceEntry("SuccessEmailTemplate",
            Value = "Success email template",
            Description = "phrase: Success email template",
            LastModified = "2015/02/27")]
        public string SuccessEmailTemplate
        {
            get
            {
                return this["SuccessEmailTemplate"];
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

        /// <summary>
        /// Gets phrase: Provider
        /// </summary>
        [ResourceEntry("Provider",
            Value = "Provider",
            Description = "phrase : Provider",
            LastModified = "2015/02/27")]
        public string Provider
        {
            get
            {
                return this["Provider"];
            }
        }

        /// <summary>
        /// Gets phrase: where the user will be registered
        /// </summary>
        [ResourceEntry("ProviderDescription",
            Value = "where the user will be registered",
            Description = "phrase : where the user will be registered",
            LastModified = "2015/02/27")]
        public string ProviderDescription
        {
            get
            {
                return this["ProviderDescription"];
            }
        }

        /// <summary>
        /// phrase: Welcome
        /// </summary>
        [ResourceEntry("SuccessEmailDefaultSubject",
            Value = "Welcome",
            Description = "The default subject of the success email",
            LastModified = "2015/02/27")]
        public string SuccessEmailDefaultSubject
        {
            get
            {
                return this["SuccessEmailDefaultSubject"];
            }
        }

        /// <summary>
        /// word: Roles
        /// </summary>
        /// <value>Roles</value>
        [ResourceEntry("Roles",
            Value = "Roles",
            Description = "word: Roles",
            LastModified = "2015/03/03")]
        public string Roles
        {
            get
            {
                return this["Roles"];
            }
        }

        /// <summary>
        /// phrase: which the user will be assigned to
        /// </summary>
        /// <value>which the user will be assigned to</value>
        [ResourceEntry("RolesDescription",
            Value = "which the user will be assigned to",
            Description = "phrase: which the user will be assigned to",
            LastModified = "2015/03/03")]
        public string RolesDescription
        {
            get
            {
                return this["RolesDescription"];
            }
        }

        /// <summary>
        /// phrase: Back to Login
        /// </summary>
        [ResourceEntry("BackToLogin",
            Value = "Back to Login",
            Description = "Back to Login",
            LastModified = "2015/03/03")]
        public string BackToLogin
        {
            get
            {
                return this["BackToLogin"];
            }
        }

        /// <summary>
        /// phrase: Success! Thanks for filling out our form!
        /// </summary>
        /// <value>Success! Thanks for filling out our form!</value>
        [ResourceEntry("DefaultSuccessfulRegistrationMessage",
            Value = "Success! Thanks for filling out our form!",
            Description = "phrase: Success! Thanks for filling out our form!",
            LastModified = "2015/03/04")]
        public string DefaultSuccessfulRegistrationMessage
        {
            get
            {
                return this["DefaultSuccessfulRegistrationMessage"];
            }
        }

        /// <summary>
        /// phrase: Please, visit your email
        /// </summary>
        /// <value>Please, visit your email</value>
        [ResourceEntry("VisitYourEmail",
            Value = "Please, visit your email",
            Description = "phrase: Please, visit your email",
            LastModified = "2015/03/09")]
        public string VisitYourEmail
        {
            get
            {
                return this["VisitYourEmail"];
            }
        }

        /// <summary>
        /// phrase: An activation link has been sent to {email}
        /// </summary>
        [ResourceEntry("ActivationLinkHasBeenSent",
            Value = "An activation link has been sent to {0}",
            Description = "phrase: An activation link has been sent to {email}",
            LastModified = "2015/03/09")]
        public string ActivationLinkHasBeenSent
        {
            get
            {
                return this["ActivationLinkHasBeenSent"];
            }
        }

        /// <summary>
        /// phrase: Another activation link has been sent to {email} If you do not received an email please check your spam box
        /// </summary>
        [ResourceEntry("ActivationLinkHasBeenSentAgain",
            Value = "Another activation link has been sent to {0} If you do not received an email please check your spam box",
            Description = "phrase: Another activation link has been sent to {email} If you do not received an email please check your spam box",
            LastModified = "2015/03/09")]
        public string ActivationLinkHasBeenSentAgain
        {
            get
            {
                return this["ActivationLinkHasBeenSentAgain"];
            }
        }

        /// <summary>
        /// phrase: Send again
        /// </summary>
        [ResourceEntry("SendAgain",
            Value = "Send again",
            Description = "phrase: Send again",
            LastModified = "2015/03/09")]
        public string SendAgain
        {
            get
            {
                return this["SendAgain"];
            }
        }
    }
}
