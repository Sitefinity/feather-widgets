using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Telerik.Sitefinity.Frontend.Identity.Mvc.Models.LoginForm
{
    /// <summary>
    /// This class represents reset password input model for the <see cref="LoginFormController"/>.
    /// </summary>
    public class ResetPasswordInputModel
    {
        /// <summary>
        /// Gets or sets the new password.
        /// </summary>
        /// <value>
        /// The new password.
        /// </value>
        [Required(ErrorMessage = "ResetPasswordRequiredErrorMessage")]
        public string NewPassword { get; set; }

        /// <summary>
        /// Gets or sets the repeat new password.
        /// </summary>
        /// <value>
        /// The repeat new password.
        /// </value>
        [Required(ErrorMessage = "ResetPasswordRequiredErrorMessage")]
        [Compare("NewPassword", ErrorMessage = "ResetPasswordNonMatchingPasswordsMessage")]
        public string RepeatNewPassword { get; set; }

        /// <summary>
        /// Gets or sets the reset password answer.
        /// </summary>
        /// <value>
        /// The reset password answer.
        /// </value>
        public string ResetPasswordAnswer { get; set; }

        /// <summary>
        /// Gets or sets the security token.
        /// </summary>
        /// <value>
        /// The security token.
        /// </value>
        public string SecurityToken { get; set; }
    }
}
