using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using Microsoft.Owin.Security;
using ServiceStack.Text;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Frontend.Identity.Mvc.StringResources;
using Telerik.Sitefinity.Frontend.Mvc.Helpers;
using Telerik.Sitefinity.Frontend.Notifications;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Mvc.Proxy;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Claims;
using Telerik.Sitefinity.Security.Configuration;
using Telerik.Sitefinity.Security.MessageTemplates.Helpers;
using Telerik.Sitefinity.Security.MessageTemplates;
using Telerik.Sitefinity.Security.Model;
using Telerik.Sitefinity.Security.Web.UI;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Services.Notifications;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Web.Mail;
using Telerik.Sitefinity.Web.UI.PublicControls;
using Config = Telerik.Sitefinity.Configuration.Config;
using SecConfig = Telerik.Sitefinity.Security.Configuration;
using Telerik.Sitefinity.Security.EmailConfirmationOperations;

namespace Telerik.Sitefinity.Frontend.Identity.Mvc.Models.LoginForm
{
    /// <summary>
    /// This class is used as a model for the <see cref="LoginFormController"/>.
    /// </summary>
    public class LoginFormModel : ILoginFormModel
    {
        #region Properties

        /// <inheritDoc/>
        public string ServiceUrl
        {
            get
            {
                this.serviceUrl = this.serviceUrl ?? this.GetClaimsIssuer();
                return this.serviceUrl;
            }

            set
            {
                this.serviceUrl = value;
            }
        }

        /// <inheritDoc/>
        public bool AllowResetPassword { get; set; }

        /// <inheritDoc/>
        public bool ShowRememberMe { get; set; }

        /// <inheritDoc/>
        public string MembershipProvider
        {
            get
            {
                if (string.IsNullOrWhiteSpace(this.membershipProvider))
                {
                    var provider = UserManager.GetDefaultProviderName();
                    var availableProviders = UserManager.GetManager().GetContextProviders();
                    if (!availableProviders.Any(x => x.Name == provider))
                    {
                        provider = availableProviders.First().Name;
                    }

                    this.membershipProvider = provider;
                }

                return this.membershipProvider;
            }

            set
            {
                this.membershipProvider = value;
            }
        }

        /// <inheritdoc />
        public string CssClass { get; set; }

        /// <inheritDoc/>
        public Guid? LoginRedirectPageId { get; set; }

        /// <inheritDoc/>
        public Guid? RegisterRedirectPageId { get; set; }

        /// <inheritdoc/>
        public Guid? ActivationPageId { get; set; }

        /// <inheritDoc/>
        public string SerializedExternalProviders
        {
            get
            {
                return this.serializedExternalProviders;
            }

            set
            {
                if (this.serializedExternalProviders != value)
                {
                    this.serializedExternalProviders = value;
                }
            }
        }

        /// <summary>
        /// Gets a value indicating whether password retrieval is enabled.
        /// </summary>
        /// <value>
        /// <c>true</c> if password retrieval is enabled; otherwise, <c>false</c>.
        /// </value>
        public virtual bool EnablePasswordRetrieval
        {
            get
            {
                return UserManager.GetManager(this.MembershipProvider).EnablePasswordRetrieval;
            }
        }

        /// <summary>
        /// Gets a value indicating whether password reset is enabled.
        /// </summary>
        /// <value>
        /// <c>true</c> if password reset is enabled; otherwise, <c>false</c>.
        /// </value>
        public virtual bool EnablePasswordReset
        {
            get
            {
                return UserManager.GetManager(this.MembershipProvider).EnablePasswordReset;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the SMTP settings are set.
        /// </summary>
        /// <value>
        /// <c>true</c> if SMTP settings are set; otherwise, <c>false</c>.
        /// </value>
        public virtual bool AreSmtpSettingsSet
        {
            get
            {
                return LoginUtils.AreSmtpSettingsSet;
            }
        }

        #endregion

        #region Public Methods

        /// <inheritDoc/>
        public virtual LoginFormViewModel GetLoginFormViewModel()
        {
            var viewModel = new LoginFormViewModel();
            this.InitializeLoginViewModel(viewModel);
            return viewModel;
        }

        /// <inheritDoc/>
        public virtual void InitializeLoginViewModel(LoginFormViewModel viewModel)
        {
            if (viewModel != null)
            {
                viewModel.ServiceUrl = this.ServiceUrl;
                viewModel.MembershipProvider = this.MembershipProvider;
                viewModel.RedirectUrlAfterLogin = this.GetPageUrl(this.LoginRedirectPageId);
                viewModel.RegisterPageUrl = this.GetPageUrl(this.RegisterRedirectPageId);
                viewModel.ShowRegistrationLink = this.RegisterRedirectPageId.HasValue;
                viewModel.ShowForgotPasswordLink = this.AllowResetPassword && (this.EnablePasswordReset || this.EnablePasswordRetrieval) && this.AreSmtpSettingsSet;
                viewModel.Realm = ClaimsManager.CurrentAuthenticationModule.GetRealm();
                viewModel.CssClass = this.CssClass;
                viewModel.ShowRememberMe = this.ShowRememberMe;

                if (!string.IsNullOrEmpty(this.serializedExternalProviders))
                {
                    var externalProviders = JsonSerializer.DeserializeFromString<Dictionary<string, string>>(this.serializedExternalProviders);
                    var availableProviders = ClaimsManager.CurrentAuthenticationModule.ExternalAuthenticationProviders.Where(x => x.Enabled == true && !string.IsNullOrEmpty(x.Title)).Select(x => x.Title).ToList();
                    var filteredExternalProviders = externalProviders.Where(x => availableProviders.Contains(x.Key));
                    viewModel.ExternalProviders = new Dictionary<string, string>();
                    foreach (var kv in filteredExternalProviders)
                    {
                        viewModel.ExternalProviders.Add(kv.Key, kv.Value);
                    }
                }
            }
        }

        /// <inheritDoc/>
        public virtual ResetPasswordViewModel GetResetPasswordViewModel(string securityToken, bool resetComplete = false, string error = null)
        {
            var securityParams = HttpUtility.ParseQueryString(securityToken);
            var userId = this.GetUserId(securityParams);
            var userManager = UserManager.GetManager(this.MembershipProvider);
            string question = null;

            if (userId != Guid.Empty)
            {
                var user = userManager.GetUser(userId);

                if (user != null)
                {
                    question = user.PasswordQuestion;
                }
            }

            return new ResetPasswordViewModel()
            {
                CssClass = this.CssClass,
                LoginPageUrl = this.GetPageUrl(null),
                RequiresQuestionAndAnswer = userManager.RequiresQuestionAndAnswer,
                Error = error,
                ResetPasswordQuestion = question,
                ResetComplete = resetComplete,
                SecurityToken = securityToken
            };
        }

        /// <inheritDoc/>
        public virtual ForgotPasswordViewModel GetForgotPasswordViewModel(string email = null, bool emailSent = false, string error = null)
        {
            return new ForgotPasswordViewModel()
            {
                CssClass = this.CssClass,
                LoginPageUrl = this.GetPageUrl(null),
                EmailSent = emailSent,
                Error = error,
                Email = email
            };
        }

        /// <inheritDoc/>
        public virtual void ResetUserPassword(string newPassword, string answer, NameValueCollection securityParams)
        {
            var userId = this.GetUserId(securityParams);

            if (userId == Guid.Empty)
            {
                throw new ResetPasswordUserNotFoundException();
            }

            var manager = UserManager.GetManager(this.MembershipProvider);
            using (new ElevatedModeRegion(manager))
            {
                var resetPassword = manager.ResetPassword(userId, answer);
                manager.ChangePassword(userId, resetPassword, newPassword);
                manager.SaveChanges();
            }

            SecurityManager.GetManager().ExpireResetPasswordToken(securityParams);

            this.SendPasswordChangedEmail(manager, manager.GetUser(userId));
        }

        /// <inheritDoc/>
        public virtual ForgotPasswordViewModel SendResetPasswordEmail(string email)
        {
            var viewModel = new ForgotPasswordViewModel();
            viewModel.Email = email;
            viewModel.EmailSent = false;

            var userManager = UserManager.GetManager(this.MembershipProvider);
            var user = userManager.GetUserByEmail(email);

            if (user != null)
            {
                if (UserManager.ShouldSendPasswordEmail(user, userManager.Provider.GetType()))
                {
                    if (user.IsApproved)
                    {
                        var currentNode = SiteMapBase.GetActualCurrentNode();
                        if (currentNode != null)
                        {
                            var resetPassUrl = Url.Combine(currentNode.Url, "resetpassword");

                            try
                            {
                                UserManager.SendRecoveryPasswordMail(userManager, user, new ResetPasswordMessageTemplate(), resetPassUrl);
                            }
                            catch (Exception ex)
                            {
                                if (Exceptions.HandleException(ex, ExceptionPolicyName.IgnoreExceptions))
                                    throw ex;

                                viewModel.Error = ex.Message;
                            }
                        }
                    }
                    else
                    {
                        if (!this.ActivationPageId.HasValue)
                        {
                            return viewModel;
                        }

                        this.SendActivationEmail(userManager, user, viewModel);

                        using (new ElevatedModeRegion(userManager))
                        {
                            userManager.SaveChanges();
                        }
                    }
                }
            }

            // Always send correct message (for security)
            viewModel.EmailSent = true;

            return viewModel;
        }

        /// <inheritDoc/>
        public virtual string GetErrorFromViewModel(System.Web.Mvc.ModelStateDictionary modelStateDict)
        {
            var firstErrorValue = modelStateDict.Values.FirstOrDefault(v => v.Errors.Any());
            if (firstErrorValue != null)
            {
                var firstError = firstErrorValue.Errors.FirstOrDefault();
                if (firstError != null)
                {
                    return firstError.ErrorMessage;
                }
            }

            return null;
        }

        /// <inheritDoc/>
        public virtual string GetPageUrl(Guid? pageId)
        {
            if (pageId.HasValue)
            {
                return HyperLinkHelpers.GetFullPageUrl(pageId.Value);
            }
            else
            {
                var currentNode = SiteMapBase.GetActualCurrentNode();
                return currentNode != null ? HyperLinkHelpers.GetFullPageUrl(currentNode.Id) : null;
            }
        }

        public virtual LoginFormViewModel Authenticate(LoginFormViewModel input, HttpContextBase context)
        {
            input.LoginError = false;
            string errorRedirectUrl = GetErrorRedirectUrl(context);
            var redirectUrl = this.GetReturnURL(context);

            User user;
            UserLoggingReason result = SecurityManager.AuthenticateUser(
                this.MembershipProvider,
                input.UserName,
                input.Password,
                input.RememberMe,
                out user);

            if (result != UserLoggingReason.Success)
            {
                errorRedirectUrl = AddErrorParameterToQuery(errorRedirectUrl);
                SFClaimsAuthenticationManager.ProcessRejectedUserForDefaultClaimsLogin(context, result, user, input.RememberMe, redirectUrl, errorRedirectUrl);

                input.LoginError = true;
            }
            else
            {
                redirectUrl = RemoveErrorParameterFromQuery(redirectUrl);

                input.RedirectUrlAfterLogin = redirectUrl;
                SystemManager.CurrentHttpContext.GetOwinContext().Authentication.SignIn(new AuthenticationProperties { RedirectUri = redirectUrl });
            }

            return input;
        }

        private void SendPasswordChangedEmail(UserManager userManager, User user)
        {
            var actionMessageTemplate = new PasswordChangedMessageTemplate();
            var url = this.GetFrontEndLoginPageUrl();

            var templateItems = new Dictionary<string, TagReplacement>()
            {
                { "Identity.LinkUrl", new TagReplacement() {Value = url, IsHtml = false } },
                { "Identity.Username", new TagReplacement() {Value = user.UserName , IsHtml = false} },
                { "Identity.SiteName", new TagReplacement() { Value = SystemManager.CurrentContext.CurrentSite.Name, IsHtml = false } },
            };

            this.SendEmail(userManager, user, actionMessageTemplate, templateItems, $"Reset password message for {user.Email}");
        }

        private void SendActivationEmail(UserManager userManager, User user, ForgotPasswordViewModel viewModel)
        {
            var actionMessageTemplate = new ResetPasswordInactiveAccountMessageTemplate();

            var activationPageUrl = HyperLinkHelpers.GetFullPageUrl(this.ActivationPageId.Value);
            var validity = new TimeSpan(0, Config.Get<SecurityConfig>().UserRegistrationSettings.ActivationMailValidityMinutes, 0);
            var cryptographicRandom = SecurityManager.GetRandomKey(16);

            user.ConfirmationToken = cryptographicRandom;

            var emailConfirmation = new AccountActivationData() { UserId = user.Id, ProviderName = user.ProviderName, Expiration = DateTime.UtcNow.TrimSeconds().Add(validity), ConfirmationToken = cryptographicRandom };

            var url = UserRegistrationEmailGenerator.GetConfirmationPageUrl(activationPageUrl, emailConfirmation);

            var templateItems = new Dictionary<string, TagReplacement>()
                        {
                            { "Identity.LinkUrl", new TagReplacement() {Value = url, IsHtml = false } },
                            { "Identity.Username", new TagReplacement() {Value = user.UserName, IsHtml = false } },
                            { "Identity.SiteName", new TagReplacement() { Value = SystemManager.CurrentContext.CurrentSite.Name, IsHtml = false } },
                            { "Identity.Validity", new TagReplacement() { Value = $"{(int)Math.Floor(validity.TotalHours)}:{validity.Minutes.ToString("00")}", IsHtml = false } },
                            { "Identity.ValidityHours", new TagReplacement() { Value = ((int) Math.Floor(validity.TotalHours)).ToString(), IsHtml = false } },
                            { "Identity.ValidityMinutes", new TagReplacement() { Value = validity.Minutes.ToString(), IsHtml = false } }
                        };

            this.SendEmail(userManager, user, actionMessageTemplate, templateItems, $"Activation email for {user.Email}");
        }

        private void SendEmail(UserManager userManager, User user, IdentityEmailMessageTemplateBase actionMessageTemplate, Dictionary<string, TagReplacement> templateItems, string description)
        {
            var userRegistrationSettings = Telerik.Sitefinity.Configuration.Config.Get<SecurityConfig>().UserRegistrationSettings;
            var senderEmailAddress = !string.IsNullOrEmpty(userRegistrationSettings.ConfirmRegistrationSenderEmail) ?
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
                description,
                senderEmailAddress,
                senderName,
                senderProfileName);

        }

        private static string AddErrorParameterToQuery(string redirectUrl)
        {
            var uriBuilder = new UriBuilder(redirectUrl);
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);
            query["err"] = true.ToString();
            uriBuilder.Query = query.ToString();
            redirectUrl = uriBuilder.ToString();

            return redirectUrl;
        }

        private static string RemoveErrorParameterFromQuery(string redirectUrl)
        {
            var uriBuilder = new UriBuilder(redirectUrl);
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);
            if (query.Keys.Contains("err"))
            {
                query.Remove("err");
                uriBuilder.Query = query.ToString();
                redirectUrl = uriBuilder.ToString();
            }

            return redirectUrl;
        }

        private static string GetErrorRedirectUrl(HttpContextBase context)
        {
            string errorRedirectUrl;

            if (context.Request.UrlReferrer?.AbsoluteUri != null)
            {
                errorRedirectUrl = context.Request.UrlReferrer.AbsoluteUri;

                var param = context.Request.Params[MvcControllerProxy.ControllerKey];

                if (param != null)
                {
                    var uriBuilder = new UriBuilder(errorRedirectUrl);
                    var query = HttpUtility.ParseQueryString(uriBuilder.Query);
                    query[LoginControllerKey] = param;
                    uriBuilder.Query = query.ToString();

                    errorRedirectUrl = uriBuilder.ToString();
                }
            }
            else
            {
                errorRedirectUrl = context.Request.Url.ToString();
            }

            return errorRedirectUrl;
        }

        /// <summary>
        /// Authenticates external provider and make IdentityServer challenge
        /// </summary>
        /// <param name="externalProviderName">Provider name.</param>
        /// <param name="context">Current http context from controller</param>
        public void AuthenticateExternal(string externalProviderName, HttpContextBase context)
        {
            var widgetUrl = context.Request.Url.ToString();
            var owinContext = context.Request.GetOwinContext();
            var returnUrl = this.GetReturnURL(context);
            var challengeProperties = ChallengeProperties.ForExternalUser(externalProviderName, widgetUrl, returnUrl);
            challengeProperties.RedirectUri = returnUrl;

            owinContext.Authentication.Challenge(challengeProperties, externalProviderName);
        }

        #endregion

        #region Private Fields and methods

        /// <summary>
        /// Tries the resolve URL from URL referrer.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="redirectUrl">The redirect URL.</param>
        /// <returns></returns>
        private bool TryResolveUrlFromUrlReferrer(HttpContextBase context, out string redirectUrl)
        {
            redirectUrl = string.Empty;

            try
            {
                // There are a few ways to redirect to another page
                // First method is to combine realm param with redirect_uri param to get the full redirect url
                // Example: ?realm=http://localhost:8086/&redirect_uri=/Sitefinity/Dashboard
                // Second method is to use only realm or redirect_uri param to get the full redirect url
                // Examples: ?realm=http://localhost:8086/Sitefinity/Dashboard
                //          ?redirect_uri=http://localhost:8086/Sitefinity/Dashboard
                // Third method is to get ReturnUrl param
                // Example: ?ReturnUrl=http://localhost:8086/Sitefinity/Dashboard
                var urlReferrer = context.Request.UrlReferrer;
                if (urlReferrer == null)
                    return false;

                var querySegment = urlReferrer.Query.TrimStart('?');

                // Check query string for all search params
                string realm = string.Empty;
                string redirectUri = string.Empty;
                string returnUrl = string.Empty;

                var queryParameters = querySegment.Split('&');
                foreach (var param in queryParameters)
                {
                    var queryParamKeyValuePair = param.Split('=');
                    switch (queryParamKeyValuePair[0])
                    {
                        case "realm":
                            realm = queryParamKeyValuePair[1].UrlDecode();
                            break;
                        case "redirect_uri":
                            redirectUri = queryParamKeyValuePair[1].UrlDecode();
                            break;
                        default:
                            if (queryParamKeyValuePair[0] == SecurityManager.AuthenticationReturnUrl)
                            {
                                returnUrl = queryParamKeyValuePair[1].UrlDecode();
                            }
                            break;
                    }

                    if (!string.IsNullOrEmpty(returnUrl))
                        break;
                }

                // Based on the params get the correct redirect url
                redirectUrl = this.BuildRedirectUrl(realm, redirectUri, returnUrl);

                return true;
            }
            catch (UriFormatException)
            {
                // According to the documentation (http://msdn.microsoft.com/en-us/library/system.web.httprequest.urlreferrer.aspx),
                // UrlReferrer could throw UriFormatException in case The HTTP Referer request header is malformed and cannot be converted to a Uri object. 
                return false;
            }
        }

        private string BuildRedirectUrl(string realm, string redirectUri, string returnUrl)
        {
            string redirectUrl = null;

            // Based on the found params get the correct redirectUrl
            if (!string.IsNullOrWhiteSpace(realm))
            {
                redirectUrl = realm;

                // If both realm and redirect_uri are provided, they should be combined
                if (!string.IsNullOrWhiteSpace(redirectUri))
                {
                    redirectUrl = string.Format("{0}/{1}", redirectUrl.TrimEnd('/'), redirectUri.TrimStart('/'));
                }
            }
            else if (!string.IsNullOrWhiteSpace(redirectUri))
            {
                redirectUrl = redirectUri;
            }
            else if (!string.IsNullOrWhiteSpace(returnUrl))
            {
                string query = string.Empty;
                Uri uri = null;
                if (Uri.TryCreate(returnUrl, UriKind.Absolute, out uri))
                    query = uri.Query;

                string pageUrlForCurrentCulture = null;
                Web.Utilities.LinkParser.TryGetPageUrlForCulture(returnUrl, true, out pageUrlForCurrentCulture, null);
                if (!string.IsNullOrEmpty(pageUrlForCurrentCulture))
                {
                    var redirectUriBuilder = new UriBuilder(pageUrlForCurrentCulture);
                    redirectUriBuilder.Query = query.TrimStart('?');
                    redirectUrl = redirectUriBuilder.Uri.ToString();
                }
                else
                {
                    redirectUrl = returnUrl;
                }
            }

            return redirectUrl;
        }

        /// <summary>
        /// Gets the claims issuer.
        /// </summary>
        /// <returns></returns>
        private string GetClaimsIssuer()
        {
            var claimsModule = ClaimsManager.CurrentAuthenticationModule;

            if (claimsModule != null)
            {
                return claimsModule.GetIssuer();
            }
            else
            {
                return LoginFormModel.DefaultRealmConfig;
            }
        }

        /// <summary>
        /// Inners the get user identifier.
        /// </summary>
        /// <returns>
        /// The user id or null.
        /// </returns>
        private Guid GetUserId(NameValueCollection securityParams)
        {
            var cip = SecurityManager.GetManager().GetPasswordRecoveryUser(securityParams);
            if (cip != null)
            {
                return cip.UserId;
            }

            return Guid.Empty;
        }

        private string GetFrontEndLoginPageUrl()
        {
            string defaultLoginPageUrl = string.Empty;
            var currentSite = Telerik.Sitefinity.Services.SystemManager.CurrentContext.CurrentSite;
            if (currentSite.FrontEndLoginPageId != Guid.Empty)
            {
                var manager = Telerik.Sitefinity.Modules.Pages.PageManager.GetManager();
                var redirectPage = manager.GetPageNode(currentSite.FrontEndLoginPageId);

                if (redirectPage != null)
                    defaultLoginPageUrl = Telerik.Sitefinity.Modules.Pages.PageExtesnsions.GetUrl(redirectPage);
            }
            else if (!string.IsNullOrWhiteSpace(currentSite.FrontEndLoginPageUrl))
            {
                defaultLoginPageUrl = currentSite.FrontEndLoginPageUrl;
            }

            return Telerik.Sitefinity.Web.UrlPath.ResolveAbsoluteUrl(defaultLoginPageUrl);
        }

        /// <summary>
        /// Gets ReturnURL set by administrator or taken from query string
        /// </summary>
        /// <returns>
        /// ReturnURL to redirect or empty string
        /// </returns>
        protected internal string GetReturnURL(HttpContextBase context)
        {
            var redirectUrl = context.Request.UrlReferrer?.AbsoluteUri ?? RouteHelper.ResolveUrl(context.Request.AppRelativeCurrentExecutionFilePath, UrlResolveOptions.Absolute);

            if (!string.IsNullOrEmpty(context.Request.Url.Query))
            {
                // Remove err flag in redirect data
                redirectUrl = redirectUrl.Replace("&err=true", string.Empty).Replace("err=true", string.Empty);
            }

            // Get redirectUrl from query string parameter
            string redirectUrlFromQS;
            this.TryResolveUrlFromUrlReferrer(context, out redirectUrlFromQS);

            if (!string.IsNullOrWhiteSpace(redirectUrlFromQS))
            {
                redirectUrl = redirectUrlFromQS;
            }
            else if (this.LoginRedirectPageId.HasValue)
            {
                redirectUrl = this.GetPageUrl(this.LoginRedirectPageId);
            }

            return redirectUrl;
        }

        private string serviceUrl;
        private const string DefaultRealmConfig = "http://localhost";
        private const string LoginControllerKey = "sf_login_cntrl_id";
        private string membershipProvider;
        private string serializedExternalProviders;

        #endregion
    }
}