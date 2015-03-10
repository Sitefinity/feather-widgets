using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;

namespace Telerik.Sitefinity.Frontend.Identity.Mvc.Models.AccountActivation
{
    /// <summary>
    /// This interface is used as a model for the <see cref="AccountActivationController"/>.
    /// </summary>
    public interface IAccountActivationModel
    {
        /// <summary>
        /// Gets the view model.
        /// </summary>
        /// <returns>
        /// An instance of <see cref="AccountActivationViewModel"/>
        /// </returns>
        AccountActivationViewModel GetViewModel(NameValueCollection securityParams);

        /// <summary>
        /// Gets or sets the css class.
        /// </summary>
        /// <value>
        /// The css class.
        /// </value>
        string CssClass { get; set; }
        
        /// <summary>
        /// Gets or sets the membership provider.
        /// </summary>
        /// <value>
        /// The membership provider.
        /// </value>
        string MembershipProvider { get; set; }

        /// <summary>
        /// Gets or sets the profile page identifier.
        /// </summary>
        /// <value>
        /// The profile page identifier.
        /// </value>
        Guid? ProfilePageId { get; set; }
    }
}
