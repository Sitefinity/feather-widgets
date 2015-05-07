using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Security;
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
        /// Gets the option to use the email as the username
        /// </summary>
        /// <value>True or False</value>
        bool EmailAddressShouldBeTheUsername { get; set; }

        /// <summary>
        /// Gets the list of roles that will be assigned to the user when registering.
        /// </summary>
        /// <value>
        /// The selected roles items.
        /// </value>
        string SerializedSelectedRoles { get; set; }

        /// <summary>
        /// Gets or sets the action that will be executed on successful user submission.
        /// </summary>
        /// <value>
        /// The success action.
        /// </value>
        SuccessfulRegistrationAction SuccessfulRegistrationAction { get; set; }

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
        /// Gets or sets the subject of the confirmation email.
        /// </summary>
        /// <value>The subject of the email.</value>
        string ConfirmationEmailSubject { get; set; }

        /// <summary>
        /// Gets or sets the template id of the email template used for the success email.
        /// </summary>
        Guid? SuccessEmailTemplateId { get; set; }

        /// <summary>
        /// Gets or sets the template id of the email template used for the confirmation email.
        /// </summary>
        Guid? ConfirmationEmailTemplateId { get; set; }

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
        ActivationMethod ActivationMethod { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the page that will be opened on successful registration.
        /// </summary>
        /// <value>
        /// The successful registration page identifier.
        /// </value>
        Guid? SuccessfulRegistrationPageId { get; set; }

        /// <summary>
        /// Gets or sets whether to send email for user activation or activate it immediately.
        /// </summary>
        /// <value>The confirm registration.</value>
        bool SendRegistrationEmail { get; set; }

        /// <summary>
        /// Gets or sets the id of the page that will be used to confirm the registration.
        /// </summary>
        /// <value>The confirmation page id.</value>
        Guid? ConfirmationPageId { get; set; }

        /// <summary>
        /// Gets or sets whether to redirect to a predefined Url
        /// </summary>
        /// <value>The default redirect Url</value>
        string DefaultReturnUrl { get; set; }

        /// <summary>
        /// Gets or sets the profile property bindings serialized as JSON that override the default bindings.
        /// </summary>
        /// <value>
        /// The profile property bindings JSON.
        /// </value>
        string ProfileBindings { get; set; }

        /// <summary>
        /// Gets the model error.
        /// </summary>
        /// <returns></returns>
        string GetError();

        /// <summary>
        /// Registers a user with the data specified in the model.
        /// </summary>
        /// <param name="model">The model containing the registration form data.</param>
        /// <returns>Status indicating whether the user was created successfully.</returns>
        MembershipCreateStatus RegisterUser(RegistrationViewModel model);

        /// <summary>
        /// Resends the confirmation email.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <returns></returns>
        bool ResendConfirmationEmail(string email);

        /// <summary>
        /// Gets the error message corresponding to the given status.
        /// </summary>
        /// <param name="status">The status.</param>
        /// <returns>The error message.</returns>
        string ErrorMessage(MembershipCreateStatus status);

        /// <summary>
        /// Gets the page URL.
        /// </summary>
        /// <param name="pageId">The page identifier.</param>
        /// <returns>
        /// The page url as string.
        /// </returns>
        string GetPageUrl(Guid? pageId);

        /// <summary>
        /// Initializes the view model.
        /// </summary>
        /// <param name="viewModel">The view model.</param>
        void InitializeViewModel(RegistrationViewModel viewModel);
    }
}
