using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
        /// Gets or sets id of the page where you have dropped Profile widget
        /// </summary>
        Guid ProfilePageId { get; set; }

        /// <summary>
        /// Gets or sets id of the page where you have dropped Registration widget
        /// </summary>
        Guid RegistrationPageId { get; set; }

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
        StatusViewModel GetStatus();

        /// <summary>
        /// Gets the redirect url to be used
        /// </summary>
        /// <returns>
        /// The redirect url as a string
        /// </returns>
        string GetRedirectUrl();
    }
}
