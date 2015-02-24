using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Telerik.Sitefinity.Frontend.Identity.Mvc.Models.LoginStatus
{
    public interface ILoginStatusModel
    {
        LoginStatusViewModel GetViewModel();

        /// <summary>
        /// Gets or sets the css class that will be applied on the wrapping element of the widget.
        /// </summary>
        string CssClass { get; set; }

        /// <summary>
        /// Gets or sets id of the page where the user will be redirected after logout
        /// </summary>
        Guid LoginPageId { get; set; }

        /// <summary>
        /// Gets or sets id of the page where you have dropped Login form widget
        /// </summary>
        Guid LogoutPageId { get; set; }

        /// <summary>
        /// Gets or sets id of the page where you have dropped Profile widget
        /// </summary>
        Guid ProfilePageId { get; set; }

        /// <summary>
        /// Gets or sets id of the page where you have dropped Registration widget
        /// </summary>
        Guid RegistrationPageId { get; set; }
    }
}
