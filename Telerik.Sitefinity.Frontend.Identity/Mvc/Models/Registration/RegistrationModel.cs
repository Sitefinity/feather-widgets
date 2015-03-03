using ServiceStack.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web.Security;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Frontend.Identity.Mvc.StringResources;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Model;
using Telerik.Sitefinity.Web.Mail;

namespace Telerik.Sitefinity.Frontend.Identity.Mvc.Models.Registration
{
    /// <summary>
    /// This class is used as a model for the <see cref="RegistrationController"/>.
    /// </summary>
    public class RegistrationModel : IRegistrationModel
    {
        #region Public Properties

        /// <inheritDoc/>
        public Guid? LoginPageId { get; set; }

        /// <inheritDoc/>
        public string CssClass { get; set; }

        /// <inheritDoc/>
        public string MembershipProviderName
        {
            get
            {
                this.membershipProviderName = this.membershipProviderName ?? UserManager.GetDefaultProviderName();
                return this.membershipProviderName;
            }
            set
            {
                this.membershipProviderName = value;
            }
        }

        /// <inheritDoc/>
        public string SerializedSelectedRolesIds { 
            get
            {
                return this.serializedSelectedRolesIds;
            }

            set
            {
                if (this.serializedSelectedRolesIds != value)
                {
                    this.serializedSelectedRolesIds = value;
                    if (!this.serializedSelectedRolesIds.IsNullOrEmpty())
                    {
                        this.selectedRolesIds = JsonSerializer.DeserializeFromString<IList<string>>(this.serializedSelectedRolesIds);
                    }
                }
            }
        }

        /// <inheritDoc/>
        public bool SendEmailOnSuccess { get; set; }

        /// <inheritDoc/>
        public string SuccessEmailSubject
        {
            get
            {
                return this.successEmailSubject;
            }
            set
            {
                this.successEmailSubject = value;
            }
        }

        /// <inheritDoc/>
        public Guid? SuccessEmailTemplateId { get; set; }

        /// <inheritDoc/>
        public virtual string EmailSenderName { get; set; }

        /// <inheritDoc/>
        public ActivationMethod ActivationMethod { get; set; }

        /// <inheritDoc/>
        public string SuccessfulRegistrationMsg { get; set; }

        /// <inheritDoc/>
        public Guid? SuccessfulRegistrationPageId { get; set; }

        #endregion

        #region Events

        /// <summary>
        /// Occurs when sending a registration email.
        /// </summary>
        public event MailMessageEventHandler SendingSuccessfulRegistrationMail;

        /// <inheritDoc/>
        public bool SendRegistrationEmail { get; set; }

        /// <inheritDoc/>
        public RegistrationViewModel GetViewModel()
        {
            return new RegistrationViewModel()
            {
                LoginPageUrl = this.GetLoginPageUrl(),
                MembershipProviderName = this.MembershipProviderName,
                CssClass = this.CssClass,
                SuccessfulRegistrationMsg = this.SuccessfulRegistrationMsg,
                SuccessfulRegistrationPageUrl = this.GetSuccessfulRegistrationPageUrl()
            };
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Gets the login redirect URL.
        /// </summary>
        /// <returns></returns>
        public virtual string GetLoginPageUrl()
        {
            return null;
        }

        /// <summary>
        /// Gets the URL of the page that will be opened on successful registration.
        /// </summary>
        /// <returns></returns>
        public virtual string GetSuccessfulRegistrationPageUrl()
        {
            return null;
        }

        /// <summary>
        /// Registers the user.
        /// </summary>
        /// <param name="model">The model.</param>
        public virtual void RegisterUser(RegistrationViewModel viewModel)
        {
            var userManager = UserManager.GetManager(this.MembershipProviderName);
            User user;
            MembershipCreateStatus status;
            var userProviderSuppressSecurityChecks = userManager.Provider.SuppressSecurityChecks;
            try
            {
                //LoginCancelEventArgs creatingUserArgs = this.OnCreatingUser();
                //if (!creatingUserArgs.Cancel)
                //{
                userManager.Provider.SuppressSecurityChecks = true;
                if (this.TryCreateUser(userManager, viewModel, out user, out status))
                {
                    userManager.SaveChanges();
                    //this.OnUserCreated();

                    //this.CreateUserProfiles(user);

                    //this.AssignRolesToUser(user);

                    this.ConfirmRegistration(userManager, user);
                    //this.ExecuteUserProfileSuccessfullUpdateActions();
                }
                else
                {
                    //this.OnUserCreationError(status);
                    //this.ShowErrorMessage(status, userManager);
                }
                // }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                userManager.Provider.SuppressSecurityChecks = userProviderSuppressSecurityChecks;
            }
        }

        #endregion

        #region Private members

        /// <summary>
        /// Attempt to create user. Returns true if the creation was successful, otherwise false.
        /// </summary>
        /// <param name="manager">The manager.</param>
        /// <param name="user">The user.</param>
        /// <param name="status">The status that will be set depending on the creation outcome.</param>
        protected virtual bool TryCreateUser(UserManager manager, RegistrationViewModel userData, out User user, out MembershipCreateStatus status)
        {
            user = manager.CreateUser(userData.UserName, (string)userData.Password, (string)userData.Email, null, null, false, null, out status);
            return status == MembershipCreateStatus.Success;
        }

        /// <summary>
        /// Executes any user confirmations steps.
        /// </summary>
        /// <param name="user">The user.</param>
        protected virtual void ConfirmRegistration(UserManager userManager, User user)
        {
            if (this.ActivationMethod == Registration.ActivationMethod.AfterConfirmation)
            {
            }
            else if (this.ActivationMethod == Registration.ActivationMethod.Immediately && this.SendEmailOnSuccess)
            {
                this.SendSuccessfulRegistrationEmail(userManager, user);
            }

            //this.RaiseRegistrationEvent(user);
        }

        /// <summary>
        /// Sends the successful registration email.
        /// </summary>
        /// <param name="userManager">The user manager.</param>
        /// <param name="user">The user.</param>
        protected virtual void SendSuccessfulRegistrationEmail(UserManager userManager, User user)
        {
            string messageBody = userManager.SuccessfulRegistrationEmailBody;
            if (this.SuccessEmailTemplateId.HasValue && this.SuccessEmailTemplateId.Value != Guid.Empty)
            {
                var templateId = this.SuccessEmailTemplateId.Value;
                var pageManager = PageManager.GetManager();
                var emailTemplate = pageManager.GetPresentationItems<ControlPresentation>().Where(tmpl => tmpl.DataType == Presentation.EmailTemplate && tmpl.Id == templateId).SingleOrDefault();
                if (emailTemplate != null)
                {
                    messageBody = emailTemplate.Data;
                }
            }

            var registrationSuccessEmail = EmailSender.CreateRegistrationSuccessEmail(userManager.SuccessfulRegistrationEmailAddress, user.Email, user.UserName, this.SuccessEmailSubject, messageBody);
            MailMessageEventArgs mailMessageEventArgs = this.OnSendingSuccessfulRegistrationMail(registrationSuccessEmail);
            if (!mailMessageEventArgs.Cancel)
            {
                var emailSender = EmailSender.Get(this.EmailSenderName);
                emailSender.SendAsync(registrationSuccessEmail, null);
            }
        }

        private MailMessageEventArgs OnSendingSuccessfulRegistrationMail(MailMessage registrationSuccessEmail)
        {
            MailMessageEventArgs mailMessageEventArgs = new MailMessageEventArgs(registrationSuccessEmail);
            if (this.SendingSuccessfulRegistrationMail != null)
            {
                this.SendingSuccessfulRegistrationMail(this, mailMessageEventArgs);
            }
            return mailMessageEventArgs;
        }

        private string membershipProviderName;
        private string successEmailSubject = Res.Get<RegistrationResources>().SuccessEmailDefaultSubject;

        #endregion

        private const string DefaultSortExpression = "PublicationDate DESC";

        #region Private fields and constants

        private string serializedSelectedRolesIds;
        private IList<string> selectedRolesIds = new List<string>();

        #endregion
    }
}
