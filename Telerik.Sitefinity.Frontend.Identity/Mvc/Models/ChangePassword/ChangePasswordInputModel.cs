using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Telerik.Sitefinity.Frontend.Identity.Mvc.Models.ChangePassword
{
    /// <summary>
    /// DTO used for submiting a change password form for <see cref="ChangePasswordController"/>
    /// </summary>
    public class ChangePasswordInputModel
    {
        /// <summary>
        /// Gets or sets the old password.
        /// </summary>
        /// <value>
        /// The old password.
        /// </value>
        [Required]
        public string OldPassword { get; set; }

        /// <summary>
        /// Gets or sets the new password.
        /// </summary>
        /// <value>
        /// The new password.
        /// </value>
        [Required]
        public string NewPassword { get; set; }

        /// <summary>
        /// Gets or sets the repeat new password.
        /// </summary>
        /// <value>
        /// The repeat new password.
        /// </value>
        [Required]
        public string RepeatNewPassword { get; set; }
    }
}
