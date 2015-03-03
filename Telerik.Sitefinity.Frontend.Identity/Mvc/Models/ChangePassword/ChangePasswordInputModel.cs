using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Web.Mvc;

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
        [Required(ErrorMessage = "ChangePasswordRequiredErrorMessage")]
        public string OldPassword { get; set; }

        /// <summary>
        /// Gets or sets the new password.
        /// </summary>
        /// <value>
        /// The new password.
        /// </value>
        [Required(ErrorMessage = "ChangePasswordRequiredErrorMessage")]
        public string NewPassword { get; set; }

        /// <summary>
        /// Gets or sets the repeat password.
        /// </summary>
        /// <value>
        /// The repeat password.
        /// </value>
        [Required(ErrorMessage = "ChangePasswordRequiredErrorMessage")]
        [Compare("NewPassword", ErrorMessage = "ChangePasswordNonMatchingPasswordsMessage")]
        public string RepeatPassword { get; set; }
    }
}
