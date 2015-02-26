using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Telerik.Sitefinity.Frontend.Identity.Mvc.Models.LoginForm
{
    /// <summary>
    /// This interface is used as a model for the <see cref="LoginFormController"/>.
    /// </summary>
    public interface ILoginFormModel
    {
        /// <summary>
        /// Gets or sets the token service URL.
        /// </summary>
        /// <value>
        /// The token service URL.
        /// </value>
        string ServiceUrl { get; set; }
        
        /// <summary>
        /// Gets or sets a value indicating whether it is allowed to reset password.
        /// </summary>
        /// <value>
        /// <c>true</c> if it is allowed to reset password; otherwise, <c>false</c>.
        /// </value>
        bool AllowResetPassword { get; set; }

        /// <summary>
        /// Gets or sets the membership provider.
        /// </summary>
        /// <value>
        /// The membership provider.
        /// </value>
        string MembershipProvider { get; set; }

        /// <summary>
        /// Gets or sets the css class.
        /// </summary>
        /// <value>
        /// The css class.
        /// </value>
        string CssClass { get; set; }

        /// <summary>
        /// Gets or sets the login redirect page identifier.
        /// </summary>
        /// <value>
        /// The login redirect page identifier.
        /// </value>
        Guid? LoginRedirectPageId { get; set; }

        /// <summary>
        /// Gets the login form view model.
        /// </summary>
        /// <returns>
        /// An instance of <see cref="LoginFormViewModel"/>
        /// </returns>
        LoginFormViewModel GetLoginFormViewModel();

        /// <summary>
        /// Gets the login form view model.
        /// </summary>
        /// <returns>
        /// An instance of <see cref="ResetPasswordViewModel"/>
        /// </returns>
        ResetPasswordViewModel GetResetPasswordViewModel();

        /// <summary>
        /// Gets the <see cref="ForgotPasswordViewModel"/>.
        /// </summary>
        /// <returns>
        /// An instance of <see cref="ForgotPasswordViewModel"/>
        /// </returns>
        ForgotPasswordViewModel GetForgotPasswordViewModel();
    }
}
