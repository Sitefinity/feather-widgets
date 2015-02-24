using System;

namespace Telerik.Sitefinity.Frontend.Identity.Mvc.Models.LoginStatus
{
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
        /// Gets or sets the logoin page identifier.
        /// </summary>
        /// <value>
        /// The logoin page identifier.
        /// </value>
        Guid? LogoinPageId { get; set; }

        /// <summary>
        /// Holds the login page to be redirected, when clicking Log in
        /// </summary>
        /// <value>
        /// The login URL.
        /// </value>
        string LoginUrl { get; set; }

        /// <summary>
        /// Gets or sets the login name format.
        /// </summary>
        string LoginNameFormatString { get; set; }

        /// <summary>
        /// Indicates whether to show the login name.
        /// </summary>
        bool ShowLoginName { get; set; }

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
    }
}
