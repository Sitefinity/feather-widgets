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
        public string RedirectUrl { get; set; }

        /// <summary>
        /// Gets or sets url of the page where user has to drop Profile widget
        /// </summary>
        public string ProfilePageUrl { get; set; }

        /// <summary>
        /// Gets or sets url of the page where user has to drop Registration widget
        /// </summary>
        public string RegistrationPageUrl { get; set; }
    }
}
