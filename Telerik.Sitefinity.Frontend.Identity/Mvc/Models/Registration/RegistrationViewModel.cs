using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Telerik.Sitefinity.Frontend.Identity.Mvc.StringResources;

namespace Telerik.Sitefinity.Frontend.Identity.Mvc.Models.Registration
{
    /// <summary>
    /// This class represents view model for the <see cref="RegistrationController"/>.
    /// </summary>
    public class RegistrationViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RegistrationViewModel"/> class.
        /// </summary>
        public RegistrationViewModel()
        {
            this.Profile = new Dictionary<string, string>();
        }

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
        /// Gets or sets the URL of the page that will be opened on successful registration.
        /// </summary>
        /// <value>
        /// The successful registration page URL.
        /// </value>
        public string SuccessfulRegistrationPageUrl { get; set; }

        /// <summary>
        /// Gets or sets the id of the page that will be used to confirm the registration.
        /// </summary>
        /// <value>The confirmation page id.</value>
        public virtual Guid? ConfirmationPageId { get; set; }

        /// <summary>
        /// Gets or sets is the username is required
        /// </summary>
        /// <value>
        /// Should the email be the username be the same
        /// </value>
        public bool EmailAddressShouldBeTheUsername { get; set; }

        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        /// <value>
        /// The name of the user.
        /// </value>
        [Required]
        [Display(Name = "Username", ResourceType = typeof(RegistrationStaticResources))]
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>
        /// The password.
        /// </value>
        [Required]
        [Display(Name = "Password", ResourceType = typeof(RegistrationStaticResources))]
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the password confirmation value.
        /// </summary>
        /// <value>
        /// The retyped password.
        /// </value>
        [System.Web.Mvc.Compare("Password")]
        [Display(Name = "ReTypePassword", ResourceType = typeof(RegistrationStaticResources))]
        public string ReTypePassword { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>
        /// The email.
        /// </value>
        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email", ResourceType = typeof(RegistrationStaticResources))]
        [EmailAddress]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the profile object.
        /// </summary>
        /// <value>
        /// The profile.
        /// </value>
        public IDictionary<string, string> Profile { get; private set; }
    }
}
