﻿namespace Telerik.Sitefinity.Frontend.Identity.Mvc.Models.LoginForm
{
    /// <summary>
    /// This class represents forgot password view model for the <see cref="LoginFormController"/>.
    /// </summary>
    public class ForgotPasswordViewModel
    {
        /// <summary>
        /// Gets or sets a value indicating whether the email is sent.
        /// </summary>
        /// <value>
        /// <c>true</c> if the email is sent; otherwise, <c>false</c>.
        /// </value>
        public bool EmailSent { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>
        /// The email.
        /// </value>
        public string Email { get; set; }
    }
}
