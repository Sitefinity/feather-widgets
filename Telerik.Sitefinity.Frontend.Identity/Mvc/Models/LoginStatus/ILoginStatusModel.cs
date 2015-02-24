using System;

namespace Telerik.Sitefinity.Frontend.Identity.Mvc.Models.LoginStatus
{
    /// <summary>
    /// This interface is used as a model for the LoginStatusController.
    /// </summary>
    public interface ILoginStatusModel
    {
        /// <summary>
        /// Gets or sets the id of the page where user has to be redirected, when clicking Log out
        /// </summary>
        /// <value>
        /// The the id to be redirected to as a nullable guid
        /// </value>
        Guid? LogoutRedirectPageId { get; set; }

        /// <summary>
        /// Gets or sets the url of the page where user has to be redirected, when clicking Log out
        /// </summary>
        /// <value>
        /// The the url to be redirected to as string
        /// </value>
        string LogoutRedirectUrl { get; set; }

        /// <summary>
        /// Gets or sets the login page identifier.
        /// </summary>
        /// <value>
        /// The login page identifier.
        /// </value>
        Guid? LoginPageId { get; set; }

        /// <summary>
        /// Holds the external login page to be redirected, when clicking Log in
        /// </summary>
        /// <value>
        /// The login URL.
        /// </value>
        string ExternalLoginUrl { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether if instant login is allowed.
        /// </summary>
        /// <remarks>
        /// This could be used in case using Windows authentication.
        /// </remarks>
        /// <value>
        ///   <c>true</c> if instant login is allowed; otherwise, <c>false</c>.
        /// </value>
        bool AllowInstantLogin { get; set; }

        /// <summary>
        /// Gets or sets id of the page where user has to drop Profile widget
        /// </summary>
        Guid? ProfilePageId { get; set; }

        /// <summary>
        /// Gets or sets id of the page where user has to drop Registration widget
        /// </summary>
        Guid? RegistrationPageId { get; set; }

        /// <summary>
        /// Gets or sets the css class that will be applied on the wrapping element of the widget.
        /// </summary>
        string CssClass { get; set; }

        /// <summary>
        /// Gets the view model.
        /// </summary>
        /// <returns>
        /// A instance of <see cref="LoginStatusViewModel"/> as view model
        /// </returns>
        LoginStatusViewModel GetViewModel();

        /// <summary>
        /// Gets the user status view model
        /// </summary>
        /// <returns>
        /// A instance of <see cref="StatusViewModel"/> as view model
        /// </returns>
        StatusViewModel GetStatusViewModel();

        /// <summary>
        /// Gets the redirect url to be used
        /// </summary>
        /// <returns>
        /// The redirect url as a string
        /// </returns>
        string GetRedirectUrl();

        /// <summary>
        /// Gets the login redirect URL.
        /// </summary>
        /// <returns></returns>
        string GetLoginRedirectUrl();
    }
}
