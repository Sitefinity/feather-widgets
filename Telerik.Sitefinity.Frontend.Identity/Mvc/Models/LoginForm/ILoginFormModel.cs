﻿using System;
using System.Collections.Specialized;
using System.Linq;
using System.Web;

namespace Telerik.Sitefinity.Frontend.Identity.Mvc.Models.LoginForm
{
    /// <summary>
    /// This interface is used as a model for the <see cref="LoginFormController"/>.
    /// </summary>
    public interface ILoginFormModel
    {
        /// <summary>
        /// Gets or sets the token service URL.
        /// </summary>
        /// <value>
        /// The token service URL.
        /// </value>
        string ServiceUrl { get; set; }
        
        /// <summary>
        /// Gets or sets a value indicating whether it is allowed to reset password.
        /// </summary>
        /// <value>
        /// <c>true</c> if it is allowed to reset password; otherwise, <c>false</c>.
        /// </value>
        bool AllowResetPassword { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether Remember me checkbox is displayed.
        /// </summary>
        /// <value>
        /// <c>true</c> if Remember me checkbox will be displayed; otherwise, <c>false</c>.
        /// </value>
        bool ShowRememberMe { get; set; }

        /// <summary>
        /// Gets or sets the membership provider.
        /// </summary>
        /// <value>
        /// The membership provider.
        /// </value>
        string MembershipProvider { get; set; }

        /// <summary>
        /// Gets a value indicating whether password retrieval is enabled.
        /// </summary>
        /// <value>
        /// <c>true</c> if password retrieval is enabled; otherwise, <c>false</c>.
        /// </value>
        bool EnablePasswordRetrieval { get; }

        /// <summary>
        /// Gets a value indicating whether password reset is enabled.
        /// </summary>
        /// <value>
        /// <c>true</c> if password reset is enabled; otherwise, <c>false</c>.
        /// </value>
        bool EnablePasswordReset { get; }

        /// <summary>
        /// Gets or sets the css class.
        /// </summary>
        /// <value>
        /// The css class.
        /// </value>
        string CssClass { get; set; }

        /// <summary>
        /// Gets or sets the login redirect page identifier.
        /// </summary>
        /// <value>
        /// The login redirect page identifier.
        /// </value>
        Guid? LoginRedirectPageId { get; set; }
        
        /// <summary>
        /// Gets or sets the register redirect page identifier.
        /// </summary>
        /// <value>
        /// The register redirect page identifier.
        /// </value>
        Guid? RegisterRedirectPageId { get; set; }

        /// <summary>
        /// Initializes the login view model.
        /// </summary>
        /// <param name="viewModel">The view model.</param>
        void InitializeLoginViewModel(LoginFormViewModel viewModel);

        /// <summary>
        /// Gets the login form view model.
        /// </summary>
        /// <returns>
        /// An instance of <see cref="LoginFormViewModel"/>
        /// </returns>
        LoginFormViewModel GetLoginFormViewModel();

        /// <summary>
        /// Gets the login form view model.
        /// </summary>
        /// <returns>
        /// An instance of <see cref="ResetPasswordViewModel"/>
        /// </returns>
        ResetPasswordViewModel GetResetPasswordViewModel(string securityToken, bool resetComplete, string error);

        /// <summary>
        /// Gets the <see cref="ForgotPasswordViewModel"/>.
        /// </summary>
        /// <returns>
        /// An instance of <see cref="ForgotPasswordViewModel"/>
        /// </returns>
        ForgotPasswordViewModel GetForgotPasswordViewModel(string email, bool emailNotFound, bool emailSent, string error);
        
        /// <summary>
        /// Tries the reset user password.
        /// </summary>
        /// <param name="newPassword">The new password.</param>
        /// <param name="answer">The answer.</param>
        /// <param name="securityParams">The collection of security parameters.</param>
        /// <returns>
        /// <c>true</c> if the password reset succeeds; otherwise, <c>false</c>.
        /// </returns>
        void ResetUserPassword(string newPassword, string answer, NameValueCollection securityParams);
        
        /// <summary>
        /// Sends reset password email.
        /// </summary>
        /// <param name="userIdentifier"></param>
        ForgotPasswordViewModel SendResetPasswordEmail(string email);
        
        /// <summary>
        /// Gets the error from view model.
        /// </summary>
        /// <param name="modelStateDict">The model state dictionary.</param>
        /// <returns>
        /// The first error from the view state.
        /// </returns>
        string GetErrorFromViewModel(System.Web.Mvc.ModelStateDictionary modelStateDict);
        /// <summary>
        /// Gets the page URL.
        /// </summary>
        /// <param name="pageId">The page identifier.</param>
        /// <param name="fallbackRedirectUrl">The fallback redirect URL.</param>
        /// <returns>
        /// The page url as string.
        /// </returns>
        string GetPageUrl(Guid? pageId, string fallbackRedirectUrl = null);

        /// <summary>
        /// Authenticates a specific user.
        /// </summary>
        /// <param name="input">The input data.</param>
        /// <param name="context">The HTTP context that is to be authenticated.</param>
        /// <returns>View model that is populated depending on the outcome of the authentication.</returns>
        LoginFormViewModel Authenticate(LoginFormViewModel input, HttpContextBase context);
    }
}
