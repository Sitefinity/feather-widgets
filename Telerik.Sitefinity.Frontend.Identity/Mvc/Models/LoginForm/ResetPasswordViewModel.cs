using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Telerik.Sitefinity.Frontend.Identity.Mvc.Models.LoginForm
{
    /// <summary>
    /// This class represents reset password view model for the <see cref="LoginFormController"/>.
    /// </summary>
    public class ResetPasswordViewModel
    {
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

        /// <summary>
        /// Gets or sets the css class.
        /// </summary>
        /// <value>
        /// The css class.
        /// </value>
        public string CssClass { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the reset is complete.
        /// </summary>
        /// <value>
        /// <c>true</c> if the reset is complete; otherwise, <c>false</c>.
        /// </value>
        public bool ResetComplete { get; set; }

        /// <summary>
        /// Gets or sets the error.
        /// </summary>
        /// <value>
        /// The error.
        /// </value>
        public string Error { get; set; }
    }
}
