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
        public string NewPassword { get; set; }

        /// <summary>
        /// Gets or sets the repeat new password.
        /// </summary>
        /// <value>
        /// The repeat new password.
        /// </value>
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

        /// <summary>
        /// Gets or sets the login page URL.
        /// </summary>
        /// <value>
        /// The login page URL.
        /// </value>
        public string LoginPageUrl { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the membership provider settings require question and answer for reset/retrieval password functionality.
        /// </summary>
        /// <value>
        /// <c>true</c> if the membership provider requires question and answer; otherwise, <c>false</c>.
        /// </value>
        public bool RequiresQuestionAndAnswer { get; set; }

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
