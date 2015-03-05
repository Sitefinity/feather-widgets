using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Security;

using ServiceStack.Text;
using Telerik.Sitefinity.Abstractions.VirtualPath;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Frontend.Identity.Mvc.Models.Profile;
using Telerik.Sitefinity.Frontend.Identity.Mvc.StringResources;
using Telerik.Sitefinity.Frontend.Mvc.Helpers;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Modules.UserProfiles;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Model;
using Telerik.Sitefinity.Utilities;
using Telerik.Sitefinity.Web;
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
        public string SerializedSelectedRoles
        {
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

        /// <inheritDoc/>
        public SuccessfulRegistrationAction SuccessfulRegistrationAction { get; set; }

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
        public virtual string ConfirmationEmailSubject
        {
            get
            {
                return this.confirmationEmailSubject;
            }
            set
            {
                this.confirmationEmailSubject = value;
            }
        }

        /// <inheritDoc/>
        public Guid? SuccessEmailTemplateId { get; set; }

        /// <inheritDoc/>
        public Guid? ConfirmationEmailTemplateId { get; set; }

        /// <inheritDoc/>
        public virtual string EmailSenderName { get; set; }

        /// <inheritDoc/>
        public ActivationMethod ActivationMethod { get; set; }

        /// <inheritDoc/>
        public string SuccessfulRegistrationMsg
        {
            get
            {
                if (this.successfulRegistrationMsg.IsNullOrEmpty())
                {
                    return Res.Get<RegistrationResources>().DefaultSuccessfulRegistrationMessage;
                }
                else
                {
                    return this.successfulRegistrationMsg;
                }
            }
            set
            {
                if (this.successfulRegistrationMsg != value)
                {
                    this.successfulRegistrationMsg = value;
                }
            }
        }

        /// <inheritDoc/>
        public Guid? SuccessfulRegistrationPageId { get; set; }

        /// <inheritDoc/>
        public bool SendRegistrationEmail { get; set; }

        /// <inheritDoc/>
        public virtual Guid? ConfirmationPageId { get; set; }

        /// <inheritDoc/>
        public virtual string DefaultReturnUrl { get; set; }

        /// <inheritDoc/>
        public string ProfileBindings { get; set; }

        #endregion

        #region Public methods

        /// <inheritDoc/>
        public RegistrationViewModel GetViewModel()
        {
            return new RegistrationViewModel()
            {
                LoginPageUrl = this.GetLoginPageUrl(),
                MembershipProviderName = this.MembershipProviderName,
                CssClass = this.CssClass,
                SuccessfulRegistrationMsg = this.SuccessfulRegistrationMsg,
                SuccessfulRegistrationPageUrl = this.GetPageUrl(this.SuccessfulRegistrationPageId)
            };
        }

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

        /// <inheritdoc />
        public virtual MembershipCreateStatus RegisterUser(RegistrationViewModel viewModel)
        {
            var userManager = UserManager.GetManager(this.MembershipProviderName);
            User user;
            MembershipCreateStatus status;
            using (new ElevatedModeRegion(userManager))
            {
                if (this.TryCreateUser(userManager, viewModel, out user, out status))
                {
                    userManager.SaveChanges();

                    this.CreateUserProfiles(user, viewModel.Profile);

                    this.AssignRolesToUser(user);

                    this.ConfirmRegistration(userManager, user);
                    //this.ExecuteUserProfileSuccessfullUpdateActions();
                }
            }

            return status;
        }

        /// <summary>
        /// Gets the error message corresponding to the given status.
        /// </summary>
        /// <param name="status">The status.</param>
        /// <returns>The error message.</returns>
        public virtual string ErrorMessage(MembershipCreateStatus status)
        {
            switch (status)
            {
                case MembershipCreateStatus.Success:
                    return null;
                case MembershipCreateStatus.InvalidPassword:
                    {
                        var manager = UserManager.GetManager(this.MembershipProviderName);
                        var invalidPasswordErrorMessage = Res.Get<ErrorMessages>().CreateUserWizardDefaultInvalidPasswordErrorMessage.Arrange(manager.MinRequiredPasswordLength, manager.MinRequiredNonAlphanumericCharacters);
                        return invalidPasswordErrorMessage;
                    }
                case MembershipCreateStatus.InvalidQuestion:
                    return Res.Get<ErrorMessages>().CreateUserWizardDefaultInvalidQuestionErrorMessage;
                case MembershipCreateStatus.InvalidAnswer:
                    return Res.Get<ErrorMessages>().CreateUserWizardDefaultInvalidAnswerErrorMessage;
                case MembershipCreateStatus.InvalidEmail:
                    return Res.Get<ErrorMessages>().CreateUserWizardDefaultInvalidEmailErrorMessage;
                case MembershipCreateStatus.DuplicateUserName:
                    return Res.Get<ErrorMessages>().CreateUserWizardDefaultDuplicateUserNameErrorMessage;
                case MembershipCreateStatus.DuplicateEmail:
                    return Res.Get<ErrorMessages>().CreateUserWizardDefaultDuplicateEmailErrorMessage;
                default:
                    return Res.Get<ErrorMessages>().CreateUserWizardDefaultUnknownErrorMessage;
            }
        }

        /// <inheritDoc/>
        public string GetPageUrl(Guid? pageId)
        {
            if (!pageId.HasValue)
            {
                pageId = SiteMapBase.GetActualCurrentNode().Id;
            }

            return HyperLinkHelpers.GetFullPageUrl(pageId.Value);
        }

        /// <inheritDoc/>
        public virtual string GetError()
        {
            if (this.ActivationMethod == ActivationMethod.AfterConfirmation && !this.ConfirmationPageId.HasValue)
            {
                return Res.Get<ErrorMessages>().NoConfirmationPageIsSet;
            }
            return string.Empty;
        }

        #endregion

        #region Protected methods

        /// <summary>
        /// Assigns the specified roles to the newly created user.
        /// </summary>
        /// <param name="user">The user.</param>
        protected virtual void AssignRolesToUser(User user)
        {
            if (this.selectedRoles != null)
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
            user = manager.CreateUser(userData.UserName, userData.Password, userData.Email, null, null, !this.SendRegistrationEmail, null, out status);
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
                this.SendRegistrationConfirmationEmail(userManager, user);
            }
            else if (this.ActivationMethod == Registration.ActivationMethod.Immediately && this.SendEmailOnSuccess)
            {
                this.SendSuccessfulRegistrationEmail(userManager, user);
            }
        }

        /// <summary>
        /// Sends the successful registration email.
        /// </summary>
        /// <param name="userManager">The user manager.</param>
        /// <param name="user">The user.</param>
        protected virtual void SendSuccessfulRegistrationEmail(UserManager userManager, User user)
        {
            string messageBody = this.GetEmailMessageBody(this.SuccessEmailTemplateId) ?? userManager.SuccessfulRegistrationEmailBody;

            var registrationSuccessEmail = 
                EmailSender.CreateRegistrationSuccessEmail(
                                userManager.SuccessfulRegistrationEmailAddress, 
                                user.Email, 
                                user.UserName, 
                                this.SuccessEmailSubject, 
                                messageBody);
            var emailSender = EmailSender.Get(this.EmailSenderName);
            emailSender.SendAsync(registrationSuccessEmail, null);
        }

        /// <summary>
        /// Creates and populates the user profiles.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="profileProperties">A dictionary containing the profile properties.</param>
        protected virtual void CreateUserProfiles(User user, IDictionary<string, string> profileProperties)
        {
            if (this.ProfileBindings.IsNullOrEmpty())
            {
                if (!VirtualPathManager.FileExists(RegistrationModel.ProfileBindingsFile))
                    return;

                var fileStream = VirtualPathManager.OpenFile(RegistrationModel.ProfileBindingsFile);
                using (var streamReader = new StreamReader(fileStream))
                {
                    this.ProfileBindings = streamReader.ReadToEnd();
                }
            }

            var profiles = new JavaScriptSerializer().Deserialize<List<ProfileBindingsContract>>(this.ProfileBindings);
            var userProfileManager = UserProfileManager.GetManager();
            using (new ElevatedModeRegion(userProfileManager))
            {
                foreach (var profileBinding in profiles)
                {
                    var userProfile = userProfileManager.CreateProfile(user, profileBinding.ProfileType);
                    foreach (var property in profileBinding.Properties)
                    {
                        var value = profileProperties.GetValueOrDefault(property.Name);
                        userProfile.SetValue(property.FieldName, value);
                    }

                    userProfileManager.RecompileItemUrls(userProfile);
                }

                userProfileManager.SaveChanges();
            }
        }

        /// <summary>
        /// Sends the registration confirmation email.
        /// </summary>
        protected virtual void SendRegistrationConfirmationEmail(UserManager userManager, User user)
        {
            string messageBody = this.GetEmailMessageBody(this.ConfirmationEmailTemplateId) ?? userManager.ConfirmRegistrationMailBody;

            string confirmationPageUrl = this.GetConfirmationPageUrl(user);

            var confirmationEmail =
                EmailSender.CreateRegistrationConfirmationEmail(
                                userManager.SuccessfulRegistrationEmailAddress,
                                user.Email,
                                user.UserName,
                                confirmationPageUrl,
                                this.ConfirmationEmailSubject,
                                messageBody);

            var emailSender = EmailSender.Get(this.EmailSenderName);
            emailSender.SendAsync(confirmationEmail, null);
        }

        /// <summary>
        /// Gets the confirmation page URL.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        protected virtual string GetConfirmationPageUrl(User user)
        {
            if (!this.ConfirmationPageId.HasValue)
            {
                return string.Empty;
            }
            string confirmationPageUrl = HyperLinkHelpers.GetFullPageUrl(this.ConfirmationPageId.Value);

            if(string.IsNullOrWhiteSpace(confirmationPageUrl))
            {
                return string.Empty;
            }

            var url = new Url(confirmationPageUrl);

            url.Query["user"] = HttpUtility.UrlEncode(user.Id.ToString());

            url.Query["provider"] = HttpUtility.UrlEncode(this.MembershipProviderName);
            
            var queryString = HttpContext.Current.Request.QueryString;

            if (queryString.Keys.Contains(ReturnUrlName))
            {
                url.Query[ReturnUrlName] = HttpUtility.UrlEncode(queryString[ReturnUrlName]);
            }
            else if (!string.IsNullOrEmpty(this.DefaultReturnUrl))
            {
                url.Query[ReturnUrlName] = HttpUtility.UrlEncode(this.DefaultReturnUrl);
            }

            return url.ToString();
        }

        protected virtual string GetEmailMessageBody(Guid? templateId)
        {
            if (templateId.HasValue && templateId.Value != Guid.Empty)
            {
                var pageManager = PageManager.GetManager();
                var emailTemplate = pageManager.GetPresentationItems<ControlPresentation>().Where(tmpl => tmpl.DataType == Presentation.EmailTemplate && tmpl.Id == templateId.Value).SingleOrDefault();
                if (emailTemplate != null)
                {
                    return emailTemplate.Data;
                }
            }
            return null;
        }

        private string membershipProviderName;
        private string successEmailSubject = Res.Get<RegistrationResources>().SuccessEmailDefaultSubject;
        private string confirmationEmailSubject = Res.Get<UserProfilesResources>().ConfirmationEmailDefaultSubject;
        private const string ReturnUrlName = "ReturnUrl";
        private const string ProfileBindingsFile = "~/Frontend-Assembly/Telerik.Sitefinity.Frontend.Identity/Mvc/Views/Registration/ProfileBindings.json";

        #endregion

        #region Private fields and constants

        private const string DefaultSortExpression = "PublicationDate DESC";

        private string successfulRegistrationMsg;
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
