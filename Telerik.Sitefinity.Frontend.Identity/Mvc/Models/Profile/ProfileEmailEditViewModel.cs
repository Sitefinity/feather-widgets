using System;
using System.Web.Mvc;

namespace Telerik.Sitefinity.Frontend.Identity.Mvc.Models.Profile
{
    [Bind(Include = "Password, UserId, Email")]
    public class ProfileEmailEditViewModel
    {
        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>
        /// The password.
        /// </value>        
        [AllowHtml]
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

        /// <summary>
        /// Gets or sets the email change confirmation result.
        /// </summary>
        public ConfirmEmailChangeFailure? ConfirmEmailChangeFailure { get; set; }
    }
}