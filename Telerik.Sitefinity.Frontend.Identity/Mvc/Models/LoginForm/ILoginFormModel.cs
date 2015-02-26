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
        /// Gets or sets the membership provider.
        /// </summary>
        /// <value>
        /// The membership provider.
        /// </value>
        string MembershipProvider { get; set; }

        /// <summary>
        /// Gets or sets the login redirect page identifier.
        /// </summary>
        /// <value>
        /// The login redirect page identifier.
        /// </value>
        Guid? LoginRedirectPageId { get; set; }

        /// <summary>
        /// Gets the view model.
        /// </summary>
        /// <returns></returns>
        LoginFormViewModel GetViewModel();
    }
}
