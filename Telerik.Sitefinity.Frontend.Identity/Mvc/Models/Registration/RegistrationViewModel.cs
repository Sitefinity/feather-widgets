using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Telerik.Sitefinity.Frontend.Identity.Mvc.Models.Registration
{
    /// <summary>
    /// This class represents view model for the <see cref="RegistrationController"/>.
    /// </summary>
    public class RegistrationViewModel
    {
        /// <summary>
        /// Holds the login page to be redirected, when clicking Log in
        /// </summary>
        public string LoginPageUrl { get; set; }

        /// <summary>
        /// Gets or sets the css class that will be applied on the wrapping element of the widget.
        /// </summary>
        public string CssClass { get; set; }

        /// <summary>
        /// Gets or sets the name of the membership provider.
        /// </summary>
        /// <value>
        /// The name of the membership provider.
        /// </value>
        public string MembershipProviderName { get; set; }

        /// <summary>
        /// Gets or sets the message that would be displayed on successful registration.
        /// </summary>
        /// <value>The successful registration message.</value>
        public string SuccessfulRegistrationMsg { get; set; }

        /// <summary>
        /// Gets or sets the URL of the page that will be opened on successful registration.
        /// </summary>
        /// <value>
        /// The successful registration page URL.
        /// </value>
        public string SuccessfulRegistrationPageUrl { get; set; }

        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        /// <value>
        /// The name of the user.
        /// </value>
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>
        /// The password.
        /// </value>
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>
        /// The email.
        /// </value>
        public string Email { get; set; }
    }
}
