using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Model;

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
        /// Gets or sets the name of the membership provider.
        /// </summary>
        /// <value>The name of the membership provider.</value>
        string MembershipProviderName { get; set; }

        /// <summary>
        /// Gets or sets the whether to send email message on successful registration confirmation.
        /// </summary>
        bool SendEmailOnSuccess { get; set; }

        /// <summary>
        /// Gets or sets the subject of the success email.
        /// </summary>
        /// <value>The subject of the email.</value>
        string SuccessEmailSubject { get; set; }

        /// <summary>
        /// Gets or sets the template id of the email template used for the success email.
        /// </summary>
        Guid? SuccessEmailTemplateId { get; set; }

        /// <summary>
        /// Gets or sets the name of the email sender that will be used to send confirmation and successful registration emails.
        /// </summary>
        /// <value>The email sender.</value>
        string EmailSenderName { get; set; }

        /// <summary>
        /// Gets or sets the activation method.
        /// </summary>
        /// <value>
        /// The activation method.
        /// </value>
        ActivationMethod ActivationMethod { get; set;}

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

        void RegisterUser(RegistrationViewModel model);
    }
}
