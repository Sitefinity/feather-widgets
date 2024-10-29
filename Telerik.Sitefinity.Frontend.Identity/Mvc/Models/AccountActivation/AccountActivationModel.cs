using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Frontend.Mvc.Helpers;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Claims;
using Telerik.Sitefinity.Security.Configuration;
using Telerik.Sitefinity.Security.EmailConfirmationOperations;
using Telerik.Sitefinity.Security.MessageTemplates;
using Telerik.Sitefinity.Security.MessageTemplates.Helpers;
using Telerik.Sitefinity.Security.Model;
using Telerik.Sitefinity.Security.Web.UI;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Services.Notifications;
using Telerik.Sitefinity.SitefinityExceptions;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Web.UI;

namespace Telerik.Sitefinity.Frontend.Identity.Mvc.Models.AccountActivation
{
    /// <summary>
    /// This class is used as a model for the <see cref="AccountActivationController"/>.
    /// </summary>
    public class AccountActivationModel : IAccountActivationModel
    {
        #region Properties

        /// <inheritdoc />
        public string CssClass { get; set; }

        /// <inheritDoc/>
        public Guid? LoginPageId { get; set; }

        /// <inheritDoc/>
        public string MembershipProvider
        {
            get
            {
                this.membershipProvider = this.membershipProvider ?? UserManager.GetDefaultProviderName();
                return this.membershipProvider;
            }
            set
            {
                this.membershipProvider = value;
            }
        }

        #endregion

        #region Public Methods

        /// <inheritDoc/>
        public virtual AccountActivationViewModel GetViewModel()
        {
            var queryString = SystemManager.CurrentHttpContext.Request.QueryStringGet(EncryptedParam);
            var shouldAttemptActivation = this.ShouldAttemptActivation();
            var activationError = false;

            if (shouldAttemptActivation)
            {
                try
                {
                    this.emailConfirmationData = UserRegistrationEmailGenerator.GetEmailConfirmationData<Sitefinity.Security.EmailConfirmationOperations.AccountActivationData>(queryString);
                }
                catch
                {
                    shouldAttemptActivation = false;
                    activationError = true;
                }
            }

            var model = new AccountActivationViewModel()
            {
                CssClass = this.CssClass,
                LoginPageUrl = this.GetPageUrl(this.LoginPageId),
                AttemptedActivation = shouldAttemptActivation,
                ActivationError = activationError
            };

            if (shouldAttemptActivation)
            {
                this.ActivateAccount(model);
            }

            return model;
        }

        /// <inheritdoc/>
        public void SendAgainActivationLink(AccountActivationViewModel model, string url)
        {
            var userManager = this.GetUserManager(model.Provider);
            var user = userManager.GetUser(model.Email);

            var actionMessageTemplate = new AccountActivationEmailMessageTemplate();

            var random = SecurityManager.GetRandomKey(16);
            user.ConfirmationToken = random;
            var userRegistrationSettings = Telerik.Sitefinity.Configuration.Config.Get<SecurityConfig>().UserRegistrationSettings;

            var validity = TimeSpan.FromMinutes(userRegistrationSettings.ActivationMailValidityMinutes);

            var activationData = new AccountActivationData() { ConfirmationToken = random, Expiration = DateTime.UtcNow.TrimSeconds().Add(validity), ProviderName = model.Provider, UserId = user.Id };

            url = UserRegistrationEmailGenerator.GetConfirmationPageUrl(url, activationData);

            var templateItems = new Dictionary<string, TagReplacement>()
            {
                { "Identity.LinkUrl", new TagReplacement() { Value = url, IsHtml = false } },
                { "Identity.SiteName", new TagReplacement() { Value = SystemManager.CurrentContext.CurrentSite.Name, IsHtml = false } },
                { "Identity.Validity", new TagReplacement() { Value = $"{(int)Math.Floor(validity.TotalHours)}:{validity.Minutes.ToString("00")}", IsHtml = false } },
                { "Identity.ValidityHours", new TagReplacement() { Value = ((int) Math.Floor(validity.TotalHours)).ToString(), IsHtml = false } },
                { "Identity.ValidityMinutes", new TagReplacement() { Value = validity.Minutes.ToString(), IsHtml = false } }
            };

            var senderEmailAddress =
                !string.IsNullOrEmpty(userRegistrationSettings.ConfirmRegistrationSenderEmail) ?
                userRegistrationSettings.ConfirmRegistrationSenderEmail :
                userManager.ConfirmationEmailAddress;

            var senderName = userRegistrationSettings.ConfirmRegistrationSenderName;

            var senderProfileName =
                !string.IsNullOrEmpty(userRegistrationSettings.EmailSenderName) ?
                userRegistrationSettings.EmailSenderName :
                null;

            IdentityEmailHelper.SendAuthenticationEmail(
                actionMessageTemplate,
                templateItems,
                new[] { user.Email },
                $"Registration confirmation for {user.Email}",
                senderEmailAddress,
                senderName,
                senderProfileName);
            model.SentActivationLink = true;
        }

        #endregion

        #region Private Fields and methods

        /// <summary>
        /// Activates the account.
        /// </summary>
        /// <returns>
        /// <c>true</c> if it succeeded; otherwise, <c>false</c>.
        /// </returns>
        private void ActivateAccount(AccountActivationViewModel model)
        {
            var userId = this.emailConfirmationData.UserId;
            if (userId == Guid.Empty)
            {
                return;
            }

            UserManager userManager = this.GetUserManager();
            var userProviderSuppressSecurityChecks = userManager.Provider.SuppressSecurityChecks;

            try
            {
                userManager.Provider.SuppressSecurityChecks = true;
                var user = userManager.GetUser(userId);
                if (this.emailConfirmationData.ConfirmationToken != user.ConfirmationToken)
                {
                    return;
                }
                else if (DateTime.UtcNow > emailConfirmationData.Expiration)
                {
                    model.ExpiredActivationLink = true;
                    model.Email = user.Email;
                    model.Provider = user.ProviderName;
                    return;
                }

                user.IsApproved = true;
                user.ConfirmationToken = null;
                userManager.SaveChanges();

                model.Activated = true;

                this.SendRegistrationSuccessEmail(userManager, user);
            }
            finally
            {
                userManager.Provider.SuppressSecurityChecks = userProviderSuppressSecurityChecks;
            }
        }

        protected virtual UserManager GetUserManager()
        {
            return this.GetUserManager(this.emailConfirmationData.ProviderName);
        }

        protected virtual UserManager GetUserManager(string provider)
        {
            return UserManager.GetManager(provider);
        }

        /// <summary>
        /// Gets the query string.
        /// </summary>
        /// <returns></returns>
        protected virtual NameValueCollection GetQueryString()
        {
            return HttpContext.Current.Request.QueryString;
        }

        /// <summary>
        /// Gets the page URL.
        /// </summary>
        /// <param name="pageId">The page id.</param>
        /// <returns></returns>
        protected virtual string GetPageUrl(Guid? pageId)
        {
            if (!pageId.HasValue)
            {
                var currentNode = SiteMapBase.GetActualCurrentNode();
                if (currentNode == null)
                    return null;

                pageId = currentNode.Id;
            }

            return HyperLinkHelpers.GetFullPageUrl(pageId.Value);
        }

        protected virtual void SendRegistrationSuccessEmail(UserManager userManager, User user)
        {
            var actionMessageTemplate = new SuccessfulRegistrationEmailMessageTemplate();

            var url = this.GetDefaultLoginUrl();

            var templateItems = new Dictionary<string, TagReplacement>()
            {
                { "Identity.LinkUrl", new TagReplacement() {Value = url, IsHtml = false } },
                { "Identity.Username", new TagReplacement() {Value = user.UserName, IsHtml = false } },
                { "Identity.SiteName", new TagReplacement() { Value = SystemManager.CurrentContext.CurrentSite.Name, IsHtml = false } }
            };

            var userRegistrationSettings = Telerik.Sitefinity.Configuration.Config.Get<SecurityConfig>().UserRegistrationSettings;
            var senderEmailAddress = !string.IsNullOrEmpty(userRegistrationSettings.SuccessfulRegistrationSenderEmail) ?
                    userRegistrationSettings.SuccessfulRegistrationSenderEmail :
                    userManager.SuccessfulRegistrationEmailAddress;
            var senderName = userRegistrationSettings.SuccessfulRegistrationSenderName;
            var senderProfileName =
                !string.IsNullOrEmpty(userRegistrationSettings.EmailSenderName) ?
                userRegistrationSettings.EmailSenderName :
                null;

            IdentityEmailHelper.SendAuthenticationEmail(
                actionMessageTemplate,
            templateItems,
            new[] { user.Email },
                $"Successful registration for {user.Email}",
                senderEmailAddress,
                senderName,
                senderProfileName);
        }

        private string GetDefaultLoginUrl()
        {
            string defaultLoginPageUrl = string.Empty;
            var currentSite = Telerik.Sitefinity.Services.SystemManager.CurrentContext.CurrentSite;
            if (currentSite.FrontEndLoginPageId != Guid.Empty)
            {
                var provider = SitefinitySiteMap.GetCurrentProvider();
                var redirectPage = provider.FindSiteMapNodeFromKey(currentSite.FrontEndLoginPageId.ToString());
                if (redirectPage != null)
                {
                    defaultLoginPageUrl = redirectPage.Url;
                }
            }
            else if (!string.IsNullOrWhiteSpace(currentSite.FrontEndLoginPageUrl))
            {
                defaultLoginPageUrl = currentSite.FrontEndLoginPageUrl;
            }

            return UrlPath.ResolveAbsoluteUrl(defaultLoginPageUrl);
        }

        private bool ShouldAttemptActivation()
        {
            if (HttpContext.Current.Request.QueryString != null)
            {
                return !string.IsNullOrEmpty(HttpContext.Current.Request.QueryStringGet(EncryptedParam));
            }

            return false;
        }

        private Sitefinity.Security.EmailConfirmationOperations.AccountActivationData emailConfirmationData;

        private string decodedQueryString;
        private string membershipProvider;

        private const string UserRegexPattern = "user=([\\dA-Za-z-]*)";
        private const string ProviderRegexPattern = "provider=([\\dA-Za-z-]*)";
        private const string EncryptedParam = "qs";

        #endregion
    }
}
