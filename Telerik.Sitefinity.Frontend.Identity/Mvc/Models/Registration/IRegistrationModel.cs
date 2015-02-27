using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Telerik.Sitefinity.Frontend.Identity.Mvc.Models.Registration
{
    /// <summary>
    /// This interface is used as a model for the <see cref="RegistrationController"/>.
    /// </summary>
    public interface IRegistrationModel
    {
        /// <summary>
        /// Gets or sets the login page identifier.
        /// </summary>
        /// <value>
        /// The login page identifier.
        /// </value>
        Guid? LoginPageId { get; set; }

        /// <summary>
        /// Gets or sets the css class that will be applied on the wrapping element of the widget.
        /// </summary>
        string CssClass { get; set; }

        /// <summary>
        /// Gets or sets the name of the provider.
        /// </summary>
        /// <value>The name of the provider.</value>
        string ProviderName { get; set; }

        /// <summary>
        /// Gets or sets the message that would be displayed on successful registration.
        /// </summary>
        /// <value>The successful registration message.</value>
        string SuccessfulRegistrationMsg { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the page that will be opened on successful registration.
        /// </summary>
        /// <value>
        /// The successful registration page identifier.
        /// </value>
        Guid? SuccessfulRegistrationPageId { get; set; }

        /// <summary>
        /// Gets the view model.
        /// </summary>
        /// <returns>
        /// A instance of <see cref="RegistrationViewModel"/> as view model
        /// </returns>
        RegistrationViewModel GetViewModel();
    }
}
