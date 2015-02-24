using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Telerik.Sitefinity.Frontend.Identity.Mvc.Models.LoginStatus
{
    public class LoginStatusViewModel
    {
        /// <summary>
        /// Gets or sets the redirect URL.
        /// </summary>
        /// <value>
        /// The redirect URL.
        /// </value>
        public string RedirectUrl { get; set; }

        /// <summary>
        /// Holds the login page to be redirected, when clicking Log in
        /// </summary>
        public string LoginUrl { get; set; }

        /// <summary>
        /// Indicates whether to show the login name.
        /// </summary>
        public bool ShowLoginName { get; set; }

        /// <summary>
        /// Gets or sets the login name format.
        /// </summary>
        public string LoginNameFormatString { get; set; }
    }
}
