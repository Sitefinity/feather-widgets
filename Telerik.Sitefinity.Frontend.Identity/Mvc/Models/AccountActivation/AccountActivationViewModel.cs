using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Telerik.Sitefinity.Frontend.Identity.Mvc.Models.AccountActivation
{
    /// <summary>
    /// DTO used for displaying the index action of <see cref="AccountActivationController"/>
    /// </summary>
    [Bind(Include = "Email, Provider")]
    public class AccountActivationViewModel
    {
        /// <summary>
        /// Gets or sets the css class.
        /// </summary>
        /// <value>
        /// The css class.
        /// </value>
        public string CssClass { get; set; }

        /// <summary>
        /// Gets or sets the profile page URL.
        /// </summary>
        /// <value>
        /// The profile page URL.
        /// </value>
        public string LoginPageUrl { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the account is activated.
        /// </summary>
        /// <value>
        /// <c>true</c> if the account activated; otherwise, <c>false</c>.
        /// </value>
        public bool Activated { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether an account activation has been atempted.
        /// </summary>
        /// <value>
        /// <c>true</c> if the account activated; otherwise, <c>false</c>.
        /// </value>
        public bool AttemptedActivation { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether an activation error has occured.
        /// </summary>
        public bool ActivationError { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the activation link has expired.
        /// </summary>
        /// <value>
        /// <c>true</c> if the link is expired; otherwise, <c>false</c>.
        /// </value>
        public bool ExpiredActivationLink { get; set; }

        /// <summary>
        /// Gets or sets the user email.
        /// </summary>
        /// <value>
        /// The user email.
        /// </value>
        [RegularExpression(Constants.EmailAddressRegexPattern, ErrorMessage = "The Email field is not a valid e-mail address.")]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the provider.
        /// </summary>
        /// <value>
        /// The provider name.
        /// </value>
        public string Provider { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether activation request was sent.
        /// </summary>
        public bool SentActivationLink { get; set; }
    }
}
