using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Telerik.Sitefinity.Frontend.Identity.Mvc.StringResources;

namespace Telerik.Sitefinity.Frontend.Identity.Mvc.Models.LoginForm
{
    /// <summary>
    /// This class represents login form view model for the <see cref="LoginFormController"/>.
    /// </summary>
    [Bind(Include = "UserName, Password, RememberMe, ExternalProviders, LoginError")]
    public class LoginFormViewModel
    {
        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        /// <value>
        /// The name of the user.
        /// </value>
        [Required]
        [Display(Name = "Username", ResourceType = typeof(LoginFormStaticResources))]
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>
        /// The password.
        /// </value>
        [Required]
        [Display(Name = "Password", ResourceType = typeof(LoginFormStaticResources))]
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the login settings should be persisted.
        /// </summary>
        /// <value>
        /// <c>true</c> if login settings should be persisted; otherwise, <c>false</c>.
        /// </value>
        [Display(Name = "RememberMe", ResourceType = typeof(LoginFormStaticResources))]
        public bool RememberMe
        {
            get
            {
                return this.rememberMe;
            }
            set
            {
                if (this.rememberMe != value)
                {
                    this.rememberMe = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the token service URL.
        /// </summary>
        /// <value>
        /// The token service URL.
        /// </value>
        public string ServiceUrl { get; set; }

        /// <summary>
        /// Gets or sets the membership provider.
        /// </summary>
        /// <value>
        /// The membership provider.
        /// </value>
        public string MembershipProvider { get; set; }

        /// <summary>
        /// Gets or sets the redirect URL after login.
        /// </summary>
        /// <value>
        /// The login redirect URL.
        /// </value>
        public string RedirectUrlAfterLogin { get; set; }

        /// <summary>
        /// Gets or sets the register page URL.
        /// </summary>
        /// <value>
        /// The register page URL.
        /// </value>
        public string RegisterPageUrl { get; set; }
        
        /// <summary>
        /// Gets or sets the realm.
        /// </summary>
        /// <value>
        /// The realm.
        /// </value>
        public string Realm { get; set; }

        /// <summary>
        /// Gets or sets the css class.
        /// </summary>
        /// <value>
        /// The css class.
        /// </value>
        public string CssClass { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the registration link should be shown.
        /// </summary>
        /// <value>
        //  <c>true</c> if the registration link should be showed; otherwise, <c>false</c>.
        /// </value>
        public bool ShowRegistrationLink { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the forgot password link should be shown.
        /// </summary>
        /// <value>
        /// <c>true</c> if the forgot password link should be showed; otherwise, <c>false</c>.
        /// </value>
        public bool ShowForgotPasswordLink { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether Remember me checkbox is displayed.
        /// </summary>
        /// <value>
        /// <c>true</c> if Remember me checkbox will be displayed; otherwise, <c>false</c>.
        /// </value>
        public bool ShowRememberMe { get; set; }

        /// <summary>
        /// Gets or sets the external providers.
        /// </summary>
        /// <value>
        /// External providers.
        /// </value>
        public IDictionary<string, string> ExternalProviders { get; set; }

        /// <summary>
        /// Gets or sets the login error flag.
        /// </summary>
        /// <value>
        /// Error flag.
        /// </value>
        public bool LoginError { get; set; }

        private bool rememberMe = true;
    }
}
