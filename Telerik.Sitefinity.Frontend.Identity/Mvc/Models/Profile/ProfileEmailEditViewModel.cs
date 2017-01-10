using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Telerik.Sitefinity.Frontend.Identity.Mvc.Models.Profile
{
    public class ProfileEmailEditViewModel
    {
        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>
        /// The password.
        /// </value>        
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the user id.
        /// </summary>
        /// <value>
        /// The user id.
        /// </value>
        public Guid UserId { get; set; }

        /// <summary>
        /// Gets or sets the user email.
        /// </summary>
        /// <value>
        /// The user email.
        /// </value>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to show profile changed message.
        /// </summary>
        /// <value>
        /// <c>true</c> if should display profile changed message; otherwise, <c>false</c>.
        /// </value>
        public bool ShowProfileChangedMsg { get; set; }
    }
}
