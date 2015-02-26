namespace Telerik.Sitefinity.Frontend.Identity.Mvc.Models.LoginForm
{
    /// <summary>
    /// This class represents login form view model for the <see cref="LoginFormController"/>.
    /// </summary>
    public class LoginFormViewModel
    {
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
        /// Gets or sets a value indicating whether to remember the user.
        /// </summary>
        /// <value>
        /// <c>true</c> if should remember the user; otherwise, <c>false</c>.
        /// </value>
        public bool RememberMe { get; set; }
    }
}
