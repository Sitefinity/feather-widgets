using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Localization.Data;

namespace Telerik.Sitefinity.Frontend.Identity.Mvc.StringResources
{
    /// <summary>
    /// Localizable strings for the Login Form widget
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
        /// Gets phrase: Login form template
        /// </summary>
        [ResourceEntry("LoginFormTemplate",
            Value = "Login form",
            Description = "phrase : Login form template",
            LastModified = "2015/02/25")]
        public string LoginFormTemplate
        {
            get
            {
                return this["LoginFormTemplate"];
            }
        }

        /// <summary>
        /// Gets phrase: Forgot your password template
        /// </summary>
        [ResourceEntry("ForgotPasswordTemplate",
            Value = "Forgot your password",
            Description = "phrase : Forgot your password template",
            LastModified = "2015/02/25")]
        public string ForgotPasswordTemplate
        {
            get
            {
                return this["ForgotPasswordTemplate"];
            }
        }

        /// <summary>
        /// Gets phrase: Reset password template
        /// </summary>
        [ResourceEntry("ResetPasswordTemplate",
            Value = "Reset password",
            Description = "phrase : Reset password template",
            LastModified = "2015/02/25")]
        public string ResetPasswordTemplate
        {
            get
            {
                return this["ResetPasswordTemplate"];
            }
        }

        /// <summary>
        /// Gets phrase: Allow users to reset password
        /// </summary>
        [ResourceEntry("AllowResetPassword",
            Value = "Allow users to reset password",
            Description = "phrase : Allow users to reset password",
            LastModified = "2015/02/25")]
        public string AllowResetPassword
        {
            get
            {
                return this["AllowResetPassword"];
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

        /// <summary>
        /// Gets phrase : You are already logged in.
        /// </summary>
        [ResourceEntry("AlreadyLogedIn",
            Value = "You are already logged in",
            Description = "phrase : You are already logged in.",
            LastModified = "2015/02/26")]
        public string AlreadyLogedIn
        {
            get
            {
                return this["AlreadyLogedIn"];
            }
        }

        /// <summary>
        /// phrase : You must first enable password reset or password retrieval from the membership provider settings.
        /// </summary>
        [ResourceEntry("PaswordResetNotEnabled",
            Value = "Go to Administration/Settings/Advanced/Security to enable Password reset or Password retrieval from the membership provider settings.",
            Description = "phrase : You must first enable password reset or password retrieval from the membership provider settings.",
            LastModified = "2015/02/27")]
        public string PaswordResetNotEnabled
        {
            get
            {
                return this["PaswordResetNotEnabled"];
            }
        }

        /// <summary>
        /// phrase : New password
        /// </summary>
        [ResourceEntry("ResetPasswordNewPassword",
            Value = "New password",
            Description = "phrase : New password",
            LastModified = "2015/02/27")]
        public string ResetPasswordNewPassword
        {
            get
            {
                return this["ResetPasswordNewPassword"];
            }
        }

        /// <summary>
        /// phrase : Repeat new password
        /// </summary>
        [ResourceEntry("ResetPasswordRepeatNewPassword",
            Value = "Repeat new password",
            Description = "phrase : Repeat new password",
            LastModified = "2015/03/10")]
        public string ResetPasswordRepeatNewPassword
        {
            get
            {
                return this["ResetPasswordRepeatNewPassword"];
            }
        }

        /// <summary>
        /// phrase : Reset password
        /// </summary>
        [ResourceEntry("ResetPasswordHeader",
            Value = "Reset password",
            Description = "phrase : Reset password",
            LastModified = "2015/02/27")]
        public string ResetPasswordHeader
        {
            get
            {
                return this["ResetPasswordHeader"];
            }
        }

        /// <summary>
        /// phrase : Forgot your password?
        /// </summary>
        [ResourceEntry("ForgotPasswordHeader",
            Value = "Forgot your password?",
            Description = "phrase : Forgot your password?",
            LastModified = "2015/02/27")]
        public string ForgotPasswordHeader
        {
            get
            {
                return this["ForgotPasswordHeader"];
            }
        }

        /// <summary>
        /// phrase : Enter your login email address and you will receive email with a link to reset your password.
        /// </summary>
        [ResourceEntry("EnterLoginEmailAddress",
            Value = "Enter your login email address and you will receive email with a link to reset your password.",
            Description = "phrase : Enter your login email address and you will receive email with a link to reset your password.",
            LastModified = "2015/03/11")]
        public string EnterLoginEmailAddress
        {
            get
            {
                return this["EnterLoginEmailAddress"];
            }
        }

        /// <summary>
        /// phrase : Email
        /// </summary>
        [ResourceEntry("ForgotPasswordEmail",
            Value = "Email",
            Description = "phrase : Email",
            LastModified = "2015/02/27")]
        public string ForgotPasswordEmail
        {
            get
            {
                return this["ForgotPasswordEmail"];
            }
        }

        /// <summary>
        /// phrase : Answer
        /// </summary>
        [ResourceEntry("ResetPasswordAnswer",
            Value = "Answer",
            Description = "phrase : Answer",
            LastModified = "2015/03/02")]
        public string ResetPasswordAnswer
        {
            get
            {
                return this["ResetPasswordAnswer"];
            }
        }

        /// <summary>
        /// phrase : Both passwords must match.
        /// </summary>
        [ResourceEntry("ResetPasswordNonMatchingPasswordsMessage",
            Value = "Both passwords must match.",
            Description = "phrase : Both passwords must match.",
            LastModified = "2015/03/02")]
        public string ResetPasswordNonMatchingPasswordsMessage
        {
            get
            {
                return this["ResetPasswordNonMatchingPasswordsMessage"];
            }
        }

        /// <summary>
        /// phrase : Invalid data.
        /// </summary>
        [ResourceEntry("ResetPasswordGeneralErrorMessage",
            Value = "Invalid data.",
            Description = "phrase : Invalid data.",
            LastModified = "2015/03/02")]
        public string ResetPasswordGeneralErrorMessage
        {
            get
            {
                return this["ResetPasswordGeneralErrorMessage"];
            }
        }

        /// <summary>
        /// phrase : Your password is successfully changed.
        /// </summary>
        [ResourceEntry("ResetPasswordSuccess",
            Value = "Your password is successfully changed.",
            Description = "phrase : Your password is successfully changed.",
            LastModified = "2015/03/02")]
        public string ResetPasswordSuccess
        {
            get
            {
                return this["ResetPasswordSuccess"];
            }
        }

        /// <summary>
        /// phrase : Back to login
        /// </summary>
        [ResourceEntry("ResetPasswordBackToLogin",
            Value = "Back to login",
            Description = "phrase : Back to login",
            LastModified = "2015/03/02")]
        public string ResetPasswordBackToLogin
        {
            get
            {
                return this["ResetPasswordBackToLogin"];
            }
        }

        /// <summary>
        /// phrase : Save
        /// </summary>
        [ResourceEntry("ResetPasswordSaveButton",
            Value = "Save",
            Description = "phrase : Save",
            LastModified = "2015/03/02")]
        public string ResetPasswordSaveButton
        {
            get
            {
                return this["ResetPasswordSaveButton"];
            }
        }

        /// <summary>
        /// phrase : Log in
        /// </summary>
        [ResourceEntry("LoginFormLogInLegendHeader",
            Value = "Log in",
            Description = "phrase : Log in",
            LastModified = "2015/03/02")]
        public string LoginFormLogInLegendHeader
        {
            get
            {
                return this["LoginFormLogInLegendHeader"];
            }
        }

        /// <summary>
        /// phrase : Log in
        /// </summary>
        [ResourceEntry("LoginFormLogInButton",
            Value = "Log in",
            Description = "phrase : Log in",
            LastModified = "2015/03/02")]
        public string LoginFormLogInButton
        {
            get
            {
                return this["LoginFormLogInButton"];
            }
        }

        /// <summary>
        /// phrase : Register now
        /// </summary>
        [ResourceEntry("LoginFormRegisterNow",
            Value = "Register now",
            Description = "phrase : Register now",
            LastModified = "2015/03/02")]
        public string LoginFormRegisterNow
        {
            get
            {
                return this["LoginFormRegisterNow"];
            }
        }

        /// <summary>
        /// phrase : You sent a request to reset your password to
        /// </summary>
        /// <value>You sent a request to reset your password to</value>
        [ResourceEntry("ForgotPasswordRequestSent",
            Value = "You sent a request to reset your password to",
            Description = "phrase : You sent a request to reset your password to",
            LastModified = "2015/03/13")]
        public string ForgotPasswordRequestSent
        {
            get
            {
                return this["ForgotPasswordRequestSent"];
            }
        }

        /// <summary>
        /// phrase : Please use the link provided in your email to reset the password for your account.
        /// </summary>
        [ResourceEntry("ForgotPasswordRequestSentUseLink",
            Value = "Please use the link provided in your email to reset the password for your account.",
            Description = "phrase : Please use the link provided in your email to reset the password for your account.",
            LastModified = "2015/03/02")]
        public string ForgotPasswordRequestSentUseLink
        {
            get
            {
                return this["ForgotPasswordRequestSentUseLink"];
            }
        }

        /// <summary>
        /// phrase : Back to login
        /// </summary>
        [ResourceEntry("ForgotPasswordBackToLogin",
            Value = "Back to login",
            Description = "phrase : Back to login",
            LastModified = "2015/03/02")]
        public string ForgotPasswordBackToLogin
        {
            get
            {
                return this["ForgotPasswordBackToLogin"];
            }
        }

        /// <summary>
        /// phrase : Send
        /// </summary>
        [ResourceEntry("ForgotPasswordSendButton",
            Value = "Send",
            Description = "phrase : Send",
            LastModified = "2015/03/02")]
        public string ForgotPasswordSendButton
        {
            get
            {
                return this["ForgotPasswordSendButton"];
            }
        }

        /// <summary>
        /// phrase : This field is required.
        /// </summary>
        [ResourceEntry("ResetPasswordRequiredErrorMessage",
            Value = "This field is required.",
            Description = "phrase : This field is required.",
            LastModified = "2015/03/03")]
        public string ResetPasswordRequiredErrorMessage
        {
            get
            {
                return this["ResetPasswordRequiredErrorMessage"];
            }
        }

        /// <summary>
        /// Gets phrase: Password reset is not enabled
        /// </summary>
        [ResourceEntry("ResetPasswordNotEnabled",
            Value = "Password reset is not enabled",
            Description = "phrase : Password reset is not enabled",
            LastModified = "2015/02/25")]
        public string ResetPasswordNotEnabled
        {
            get
            {
                return this["ResetPasswordNotEnabled"];
            }
        }

        /// <summary>
        /// Gets phrase: Forgotten Password
        /// </summary>
        [ResourceEntry("ForgottenPasword",
            Value = "Forgotten Password",
            Description = "phrase : Forgotten Password",
            LastModified = "2015/02/25")]
        public string ForgottenPasword
        {
            get
            {
                return this["ForgottenPasword"];
            }
        }

        /// <summary>
        /// phrase: Incorrect Username/Password Combination
        /// </summary>
        /// <value>Incorrect Username/Password Combination</value>
        [ResourceEntry("IncorrectCredentialsMessage",
            Value = "Incorrect Username/Password Combination",
            Description = "phrase: Incorrect Username/Password Combination",
            LastModified = "2015/03/06")]
        public string IncorrectCredentialsMessage
        {
            get
            {
                return this["IncorrectCredentialsMessage"];
            }
        }

        /// <summary>
        /// phrase: Show \"Remember me\" checkbox
        /// </summary>
        /// <value>Show \"Remember me\" checkbox</value>
        [ResourceEntry("ShowRememberMe",
            Value = "Show \"Remember me\" checkbox",
            Description = "phrase: Show \"Remember me\" checkbox",
            LastModified = "2015/03/05")]
        public string ShowRememberMe
        {
            get
            {
                return this["ShowRememberMe"];
            }
        }

        /// <summary>
        /// phrase: Username
        /// </summary>
        /// <value>Username</value>
        [ResourceEntry("Username",
            Value = "Username",
            Description = "phrase: Username",
            LastModified = "2015/03/09")]
        public string Username
        {
            get
            {
                return this["Username"];
            }
        }

        /// <summary>
        /// phrase: Password
        /// </summary>
        /// <value>Password</value>
        [ResourceEntry("Password",
            Value = "Password",
            Description = "phrase: Password",
            LastModified = "2015/03/09")]
        public string Password
        {
            get
            {
                return this["Password"];
            }
        }

        /// <summary>
        /// phrase: RememberMe
        /// </summary>
        /// <value>RememberMe</value>
        [ResourceEntry("RememberMe",
            Value = "Remember me",
            Description = "phrase: RememberMe",
            LastModified = "2015/03/09")]
        public string RememberMe
        {
            get
            {
                return this["RememberMe"];
            }
        }

        /// <summary>
        /// phrase: Templates label
        /// </summary>
        /// <value>Templates label</value>
        [ResourceEntry("Templates",
            Value = "Templates",
            Description = "phrase: Templates",
            LastModified = "2015/03/09")]
        public string Templates
        {
            get
            {
                return this["Templates"];
            }
        }

        /// <summary>
        /// phrase: {0} does not exist
        /// </summary>
        /// <value>{0} does not exist</value>
        [ResourceEntry("DoesNotExist",
            Value = "{0} does not exist",
            Description = "phrase: {0} does not exist",
            LastModified = "2015/03/13")]
        public string DoesNotExist
        {
            get
            {
                return this["DoesNotExist"];
            }
        }
    }
}
