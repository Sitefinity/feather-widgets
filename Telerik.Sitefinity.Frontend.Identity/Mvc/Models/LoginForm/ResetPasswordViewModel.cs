﻿namespace Telerik.Sitefinity.Frontend.Identity.Mvc.Models.LoginForm
{
    /// <summary>
    /// This class represents reset password view model for the <see cref="LoginFormController"/>.
    /// </summary>
    public class ResetPasswordViewModel
    {
        /// <summary>
        /// Gets or sets a value indicating whether the password is changed.
        /// </summary>
        /// <value>
        /// <c>true</c> if the password is changed; otherwise, <c>false</c>.
        /// </value>
        public bool PasswordChanged { get; set; }

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
        public string RepeatNewPassword { get; set; }
    }
}
