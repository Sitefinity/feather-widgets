using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Telerik.Sitefinity.Frontend.Identity.Mvc.Models.ChangePassword
{
    /// <summary>
    /// DTO used for displaying the index action of <see cref="ChangePasswordController"/>
    /// </summary>
    public class ChangePasswordViewModel
    {
        /// <summary>
        /// Gets or sets the css class.
        /// </summary>
        /// <value>
        /// The css class.
        /// </value>
        public string CssClass { get; set; }

        /// <summary>
        /// Gets or sets the old password.
        /// </summary>
        /// <value>
        /// The old password.
        /// </value>
        public string OldPassword { get; set; }

        /// <summary>
        /// Gets or sets the new password.
        /// </summary>
        /// <value>
        /// The new password.
        /// </value>
        public string NewPassword { get; set; }

        /// <summary>
        /// Gets or sets the repeat new password.
        /// </summary>
        /// <value>
        /// The repeat new password.
        /// </value>
        public string RepeatPassword { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the password change was complete.
        /// </summary>
        /// <value>
        /// <c>true</c> if the password change was complete; otherwise, <c>false</c>.
        /// </value>
        public bool PasswordChanged { get; set; }
        
        /// <summary>
        /// Gets or sets the error.
        /// </summary>
        /// <value>
        /// The error.
        /// </value>
        public string Error { get; set; }
    }
}
