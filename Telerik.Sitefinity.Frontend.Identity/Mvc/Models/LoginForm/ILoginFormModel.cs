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
        /// Gets or sets the login form template name.
        /// </summary>
        /// <value>
        /// The login form template.
        /// </value>
        string LoginFormTemplate { get; set; }
        
        /// <summary>
        /// Gets or sets the forgot password template.
        /// </summary>
        /// <value>
        /// The forgot password template.
        /// </value>
        string ForgotPasswordTemplate { get; set; }
        
        /// <summary>
        /// Gets or sets the reset password template.
        /// </summary>
        /// <value>
        /// The reset password template.
        /// </value>
        string ResetPasswordTemplate { get; set; }
        
        /// <summary>
        /// Gets or sets a value indicating whether it is allowed to reset password.
        /// </summary>
        /// <value>
        /// <c>true</c> if it is allowed to reset password; otherwise, <c>false</c>.
        /// </value>
        bool AllowResetPassword { get; set; }

        /// <summary>
        /// Gets the view model.
        /// </summary>
        /// <returns></returns>
        LoginFormViewModel GetViewModel();
    }
}
