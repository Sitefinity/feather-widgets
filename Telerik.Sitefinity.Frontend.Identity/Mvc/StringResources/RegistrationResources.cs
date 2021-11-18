﻿using System;
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
    [ObjectInfo(typeof(RegistrationResources), ResourceClassId = "RegistrationResources", Title = "RegistrationResourcesTitle", Description = "RegistrationResourcesDescription")]
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

        #region Class Description

        /// <summary>
        /// Gets Title for the Registration widget resources class.
        /// </summary>
        [ResourceEntry("RegistrationResourcesTitle",
            Value = "Registration widget resources",
            Description = "Title for the Registration widget resources class.",
            LastModified = "2015/03/30")]
        public string RegistrationResourcesTitle
        {
            get
            {
                return this["RegistrationResourcesTitle"];
            }
        }

        /// <summary>
        /// Gets Description for the Registration widget resources class.
        /// </summary>
        [ResourceEntry("RegistrationResourcesDescription",
            Value = "Localizable strings for the Registration widget.",
            Description = "Description for the Registration widget resources class.",
            LastModified = "2015/03/30")]
        public string RegistrationResourcesDescription
        {
            get
            {
                return this["RegistrationResourcesDescription"];
            }
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
        /// Gets phrase: Question
        /// </summary>
        [ResourceEntry("Question",
            Value = "Secret question",
            Description = "phrase : Secret question",
            LastModified = "2016/12/23")]
        public string Question
        {
            get
            {
                return this["Question"];
            }
        }

        /// <summary>
        /// Gets phrase: Answer
        /// </summary>
        [ResourceEntry("Answer",
            Value = "Secret answer",
            Description = "phrase : Secret answer",
            LastModified = "2016/12/23")]
        public string Answer
        {
            get
            {
                return this["Answer"];
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
        /// Gets phrase: Repeat password
        /// </summary>
        [ResourceEntry("ReTypePassword",
            Value = "Repeat password",
            Description = "phrase : Repeat password",
            LastModified = "2016/12/23")]
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
        /// Gets phrase: User group
        /// </summary>
        [ResourceEntry("UserGroup",
            Value = "User group",
            Description = "phrase : User group",
            LastModified = "2021/02/09")]
        public string UserGroup
        {
            get
            {
                return this["UserGroup"];
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
        /// phrase: Log in
        /// </summary>
        [ResourceEntry("BackToLogin",
            Value = "Log in",
            Description = "Log in",
            LastModified = "2016/12/23")]
        public string BackToLogin
        {
            get
            {
                return this["BackToLogin"];
            }
        }

        /// <summary>
        /// phrase: Thank you!
        /// </summary>
        /// <value>Thank you!</value>
        [ResourceEntry("ThankYou",
            Value = "Thank you!",
            Description = "Thank you!",
            LastModified = "2015/03/10")]
        public string ThankYou
        {
            get
            {
                return this["ThankYou"];
            }
        }

        /// <summary>
        /// phrase: You are successfully registered.
        /// </summary>
        /// <value>You are successfully registered.</value>
        [ResourceEntry("DefaultSuccessfulRegistrationMessage",
            Value = "You are successfully registered.",
            Description = "You are successfully registered.",
            LastModified = "2015/03/10")]
        public string DefaultSuccessfulRegistrationMessage
        {
            get
            {
                return this["DefaultSuccessfulRegistrationMessage"];
            }
        }

        /// <summary>
        /// phrase: Please check your email
        /// </summary>
        /// <value>Please check your email</value>
        [ResourceEntry("VisitYourEmail",
            Value = "Please check your email",
            Description = "phrase: Please check your email",
            LastModified = "2015/05/04")]
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

        /// <summary>
        /// Gets phrase: Allow users to register with...
        /// </summary>
        [ResourceEntry("ExternalProvidersRegiterOptions",
            Value = "Allow users to register with...",
            Description = "phrase : Allow users to register with...",
            LastModified = "2016/12/12")]
        public string ExternalProvidersRegiterOptions
        {
            get
            {
                return this["ExternalProvidersRegiterOptions"];
            }
        }

        /// <summary>
        /// phrase : Or
        /// </summary>
        [ResourceEntry("Or",
            Value = "Or",
            Description = "phrase : Or",
            LastModified = "2016/12/13")]
        public string Or
        {
            get
            {
                return this["Or"];
            }
        }

        /// <summary>
        /// Gets phrase: Registration
        /// </summary>
        [ResourceEntry("Registration",
            Value = "Registration",
            Description = "phrase : Registration",
            LastModified = "2016/12/13")]
        public string Registration
        {
            get
            {
                return this["Registration"];
            }
        }

        /// <summary>
        /// Gets phrase: Or connect with...
        /// </summary>
        [ResourceEntry("ConnectWith",
            Value = "Or connect with...",
            Description = "phrase : Or connect with...",
            LastModified = "2016/12/23")]
        public string ConnectWith
        {
            get
            {
                return this["ConnectWith"];
            }
        }

        /// <summary>
        /// Gets phrase: Send an activation link again
        /// </summary>
        [ResourceEntry("SendActivationLinkAgain",
            Value = "Send an activation link again",
            Description = "phrase : Send an activation link again",
            LastModified = "2018/09/12")]
        public string SendActivationLinkAgain
        {
            get
            {
                return this["SendActivationLinkAgain"];
            }
        }

         /// <summary>
        /// Control name: Registration
        /// </summary>
        [ResourceEntry("RegistrationWidgetTitle",
            Value = "Registration",
            Description = "Control title: Registration",
            LastModified = "2019/06/03")]
        public string RegistrationWidgetTitle
        {
            get
            {
                return this["RegistrationWidgetTitle"];
            }
        }

        /// <summary>
        /// Control description: A form used to register users.
        /// </summary>
        [ResourceEntry("RegistrationWidgetDescription",
            Value = "Form used to register users",
            Description = "Control description: A form used to register users.",
            LastModified = "2019/06/03")]
        public string RegistrationWidgetDescription
        {
            get
            {
                return this["RegistrationWidgetDescription"];
            }
        }
    }
}
