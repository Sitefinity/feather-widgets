namespace Telerik.Sitefinity.Frontend.Identity.Mvc.Models.LoginStatus
{
    /// <summary>
    /// This class represents view model for Login status.
    /// </summary>
    public class LoginStatusViewModel
    {
        /// <summary>
        /// Gets or sets the redirect url.
        /// </summary>
        /// <value>
        /// The redirect url.
        /// </value>
        public string LogoutPageUrl { get; set; }

        /// <summary>
        /// Holds the login page to be redirected, when clicking Log in
        /// </summary>
        /// <value>
        /// The redirect url.
        /// </value>
        public string LoginPageUrl { get; set; }

         /// <summary>
        /// Gets or sets url of the page where user has to drop Profile widget
        /// </summary>
        /// <value>
        /// The redirect url.
        /// </value>
        public string ProfilePageUrl { get; set; }

        /// <summary>
        /// Gets or sets url of the page where user has to drop Registration widget
        /// </summary>
        /// <value>
        /// The redirect url.
        /// </value>
        public string RegistrationPageUrl { get; set; }

        /// <summary>
        /// Gets or sets the css class that will be applied on the wrapping element of the widget.
        /// </summary>
        public string CssClass { get; set; }

        /// <summary>
        /// Gets or sets the status service URL.
        /// </summary>
        /// <value>
        /// The status service URL.
        /// </value>
        public string StatusServiceUrl { get; set; }

        /// <summary>
        /// Gets or sets the allow Windows STS login.
        /// </summary>
        ///   <c>true</c> if Windows STS login is allowed; otherwise, <c>false</c>.
        /// </value>
        public bool AllowWindowsStsLogin { get; set; }
    }
}
