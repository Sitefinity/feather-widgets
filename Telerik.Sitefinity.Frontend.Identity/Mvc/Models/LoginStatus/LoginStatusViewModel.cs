using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Telerik.Sitefinity.Frontend.Identity.Mvc.Models.LoginStatus
{
    public class LoginStatusViewModel
    {
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
