using ServiceStack.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web.Security;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Frontend.Identity.Mvc.StringResources;
using Telerik.Sitefinity.Frontend.Mvc.Helpers;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Model;
using Telerik.Sitefinity.Security.Web.UI;
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
        public string SerializedSelectedRoles { 
            get
            {
                return this.serializedSelectedRoles;
            }

            set
            {
                if (this.serializedSelectedRoles != value)
                {
                    this.serializedSelectedRoles = value;
                    if (!this.serializedSelectedRoles.IsNullOrEmpty())
                    {
                        this.selectedRoles = JsonSerializer.DeserializeFromString<IList<Role>>(this.serializedSelectedRoles);
                    }
                }
            }
        }

        /// <summary>
        /// Gets the role managers to submit.
        /// </summary>
        /// <value>The role managers to submit.</value>
        protected virtual Dictionary<string, RoleManager> RoleManagersToSubmit
        {
            get
            {
                if (this.roleManagersToSubmit == null)
                {
                    roleManagersToSubmit = new Dictionary<string, RoleManager>();
                }
                return this.roleManagersToSubmit;
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

        /// <summary>
        /// Occurs when assigning roles to a user.
        /// </summary>
        public event UserOperationInvokingEventHandler AssigningRolesToUser;

        /// <summary>
        /// Occurs when roles were assigned to a user.
        /// </summary>
        public event UserOperationInvokedEventHandler RolesAssignedToUser;

        #endregion

        #region Public methods

        /// <summary>
        /// Gets the login redirect URL.
        /// </summary>
        /// <returns></returns>
        public virtual string GetLoginPageUrl()
        {
            string result;
            if (this.LoginPageId.HasValue)
            {
                result = HyperLinkHelpers.GetFullPageUrl(this.LoginPageId.Value);
            }
            else
            {
                result = SitefinityContext.FrontendLoginUrl;
            }

            return result;
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

                    this.AssignRolesToUser(user);

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

        #region Event Delegates

        /// <summary>
        /// Represents a method that operates on a <see cref="User"/> instance.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">An argument passed to event handlers operating on a user instance that contains a flag specifying whether the operation should be canceled or not.</param>
        public delegate void UserOperationInvokingEventHandler(object sender, UserOperationInvokingEventArgs e);

        /// <summary>
        /// Represents a method that operates on a <see cref="User"/> instance.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">An argument passed to event handlers operating on a user instance.</param>
        public delegate void UserOperationInvokedEventHandler(object sender, UserOperationInvokedEventArgs e);

        #endregion

        #region Protected methods

        /// <summary>
        /// Assigns the specified roles to the newly created user.
        /// </summary>
        /// <param name="user">The user.</param>
        protected virtual void AssignRolesToUser(User user)
        {
            if (!this.OnAssigningUserRoles(user).Cancel && this.selectedRoles != null)
            {
                foreach (var roleInfo in this.selectedRoles)
                {
                    var roleManager = this.GetRoleManager(roleInfo.ProviderName);
                    var roleToAssign = roleManager.GetRole(roleInfo.Id);
                    if (!roleManager.Provider.Abilities.Keys.Contains("AssingUserToRole") || (roleManager.Provider.Abilities["AssingUserToRole"].Supported))
                    {
                        var suppressSecurityCheks = roleManager.Provider.SuppressSecurityChecks;
                        try
                        {
                            roleManager.Provider.SuppressSecurityChecks = true;
                            roleManager.AddUserToRole(user, roleToAssign);
                        }
                        catch (Exception)
                        {
                            throw;
                        }
                        finally
                        {
                            roleManager.Provider.SuppressSecurityChecks = suppressSecurityCheks;
                        }
                    }
                }
                foreach (var roleManagerPair in this.RoleManagersToSubmit)
                {
                    roleManagerPair.Value.SaveChanges();
                }
                this.OnUserRolesAssigned(user);
            }
        }

        /// <summary>
        /// Gets the manager.
        /// </summary>
        /// <param name="providerName">Name of the provider.</param>
        /// <returns></returns>
        protected virtual RoleManager GetRoleManager(string providerName)
        {
            if (this.RoleManagersToSubmit.ContainsKey(providerName))
            {
                return this.RoleManagersToSubmit[providerName];
            }
            else
            {
                var manager = RoleManager.GetManager(providerName);
                this.RoleManagersToSubmit.Add(providerName, manager);
                return manager;
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

        private UserOperationInvokingEventArgs OnAssigningUserRoles(User user)
        {
            UserOperationInvokingEventArgs assigningUserRolesArgs = new UserOperationInvokingEventArgs(user);
            if (this.AssigningRolesToUser != null)
            {
                this.AssigningRolesToUser(this, assigningUserRolesArgs);
            }
            return assigningUserRolesArgs;
        }

        private UserOperationInvokedEventArgs OnUserRolesAssigned(User user)
        {
            UserOperationInvokedEventArgs userRolesAssigned = new UserOperationInvokedEventArgs(user);
            if (this.RolesAssignedToUser != null)
            {
                this.RolesAssignedToUser(this, userRolesAssigned);
            }
            return userRolesAssigned;
        }

        private string membershipProviderName;
        private string successEmailSubject = Res.Get<RegistrationResources>().SuccessEmailDefaultSubject;

        #endregion

        private const string DefaultSortExpression = "PublicationDate DESC";

        #region Private fields and constants

        private string serializedSelectedRoles;
        private IList<Role> selectedRoles = new List<Role>();
        private Dictionary<string, RoleManager> roleManagersToSubmit = null;

        #endregion

        private class Role
        {
            public Guid Id { get; set; }

            public string Name { get; set; }

            public string ProviderName { get; set; }
        }
    }
}
