using System;
using System.Linq;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Frontend.Mvc.Helpers;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Claims;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Data;
using System.Collections.Specialized;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Security.Model;
using System.Web;
using Microsoft.IdentityModel.Tokens;
using Microsoft.IdentityModel.Claims;
using Microsoft.IdentityModel.Web;
using Telerik.Sitefinity.Security.Claims.SWT;

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
                this.membershipProvider = this.membershipProvider ?? UserManager.GetDefaultProviderName();
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
                viewModel.ShowForgotPasswordLink = this.AllowResetPassword && (this.EnablePasswordReset || this.EnablePasswordRetrieval);
                viewModel.Realm = SitefinityClaimsAuthenticationModule.Current.GetRealm();
                viewModel.CssClass = this.CssClass;
                viewModel.ShowRememberMe = this.ShowRememberMe;
            }
        }

        /// <inheritDoc/>
        public virtual ResetPasswordViewModel GetResetPasswordViewModel(string securityToken, bool resetComplete = false, string error = null)
        {
            return new ResetPasswordViewModel()
            {
                CssClass = this.CssClass,
                LoginPageUrl = this.GetPageUrl(null),
                RequiresQuestionAndAnswer = UserManager.GetManager(this.MembershipProvider).RequiresQuestionAndAnswer,
                Error = error,
                ResetComplete = resetComplete,
                SecurityToken = securityToken
            };
        }

        /// <inheritDoc/>
        public virtual ForgotPasswordViewModel GetForgotPasswordViewModel(string email = null, bool emailNotFound = false, bool emailSent = false, string error = null)
        {
            return new ForgotPasswordViewModel()
            {
                CssClass = this.CssClass,
                LoginPageUrl = this.GetPageUrl(null),
                EmailSent = emailSent,
                Error = error,
                EmailNotFound = emailNotFound,
                Email = email
            };
        }

        /// <inheritDoc/>
        public virtual void ResetUserPassword(string newPassword, string answer, NameValueCollection securityParams)
        {
            var userId = this.GetUserId(securityParams);

            if (userId == Guid.Empty)
            {
                throw new ArgumentNullException("User could not be retrieved.");
            }

            var manager = UserManager.GetManager(this.MembershipProvider);
            using (new ElevatedModeRegion(manager))
            {
                var resetPassword = manager.ResetPassword(userId, answer);
                manager.ChangePassword(userId, resetPassword, newPassword);
                manager.SaveChanges();
            }
        }

        /// <inheritDoc/>
        public virtual ForgotPasswordViewModel SendResetPasswordEmail(string email)
        {
            var viewModel = new ForgotPasswordViewModel();
            viewModel.Email = email;
            viewModel.EmailSent = false;


            var manager = UserManager.GetManager(this.MembershipProvider);
            var user = manager.GetUserByEmail(email);

            if (user != null)
            {
                if (!UserManager.ShouldSendPasswordEmail(user, manager.Provider.GetType()))
                {
                    viewModel.Error = "Not supported";
                }
                else
                {
                    var currentNode = SiteMapBase.GetActualCurrentNode();
                    if (currentNode != null)
                    {
                        var resetPassUrl = Url.Combine(currentNode.Url, "resetpassword");

                        try
                        {
                            UserManager.SendRecoveryPasswordMail(UserManager.GetManager(user.ProviderName), email, resetPassUrl);

                            viewModel.EmailSent = true;
                        }
                        catch (Exception ex)
                        {
                            if (Exceptions.HandleException(ex, ExceptionPolicyName.IgnoreExceptions))
                                throw ex;

                            viewModel.Error = ex.Message;
                        }
                    }
                }
            }
            else
            {
                viewModel.EmailNotFound = true;
            }

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

        /// <inheritDoc/>
        public virtual LoginFormViewModel Authenticate(LoginFormViewModel input, HttpContextBase context)
        {
            User user;
            UserLoggingReason result = SecurityManager.AuthenticateUser(
                this.MembershipProvider,
                input.UserName,
                input.Password,
                input.RememberMe,
                out user);

            var identity = ClaimsManager.GetCurrentIdentity();
            if (user != null && identity != null && identity.OriginalIdentity is SitefinityIdentity)
            {
                IClaimsPrincipal cp = new ClaimsPrincipal(new[] { new ClaimsIdentity(identity.Claims) });
                var wifCredentials = new FederatedServiceCredentials(FederatedAuthentication.ServiceConfiguration);
                cp = wifCredentials.ClaimsAuthenticationManager.Authenticate(context.Request.RequestType, cp);
                SitefinityClaimsAuthenticationModule.Current.AuthenticatePrincipalWithCurrentToken(cp, input.RememberMe);
            }

            if (result == UserLoggingReason.Unknown)
            {
                input.IncorrectCredentials = true;
            }
            else
            {
                string redirectUrl;
                if (this.LoginRedirectPageId.HasValue)
                {
                    redirectUrl = this.GetPageUrl(this.LoginRedirectPageId);
                }
                else
                {
                    this.TryResolveUrlFromUrlReferrer(context, out redirectUrl);
                }

                input.RedirectUrlAfterLogin = redirectUrl;

                if (result != UserLoggingReason.Success)
                {
                    SFClaimsAuthenticationManager.ProcessRejectedUser(context, input.RedirectUrlAfterLogin);
                }

                //validates whether the RedirectUrlAfterLogin is allowed in the relying parties.
                SWTIssuer.GetRelyingPartyKey(input.RedirectUrlAfterLogin);
            }

            return input;
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
                //There is two ways to redirect to another page
                //First method is to combine realm param with redirect_uri param to get the full redirect url
                //Example: ?realm=http://localhost:8086/&redirect_uri=/Sitefinity/Dashboard
                //Second method is to get ReturnUrl param
                //Example: ?ReturnUrl=http://localhost:8086/Sitefinity/Dashboard
                Uri urlReferrer = context.Request.UrlReferrer;
                if (urlReferrer != null)
                {
                    var querySegment = HttpUtility.UrlDecode(urlReferrer.Query);
                    if (querySegment.StartsWith("?"))
                    {
                        querySegment = querySegment.Substring(1);
                    }

                    //check query string for all search params
                    var queryStrings = querySegment.Split('&');
                    string realm = string.Empty;
                    string redirect_uri = string.Empty;
                    string returnUrl = string.Empty;
                    foreach (var queryString in queryStrings)
                    {
                        var queryStringPair = queryString.Split('=');
                        switch (queryStringPair[0])
                        {
                            case "realm":
                                realm = queryStringPair[1];
                                break;
                            case "redirect_uri":
                                redirect_uri = queryStringPair[1];
                                break;
                            case "ReturnUrl":
                                returnUrl = queryStringPair[1];
                                break;
                        }
                    }

                    //based on the found params get the correct redirectUrl
                    if (!string.IsNullOrWhiteSpace(realm))
                    {
                        redirectUrl = realm;

                        if (!string.IsNullOrWhiteSpace(redirect_uri))
                        {
                            redirectUrl += redirect_uri;
                        }
                    }
                    else if (!string.IsNullOrWhiteSpace(redirect_uri))
                    {
                        redirectUrl = redirect_uri;
                    }
                    else if (!string.IsNullOrWhiteSpace(returnUrl))
                    {
                        redirectUrl = returnUrl;
                    }

                    return true;
                }
            }
            catch (UriFormatException)
            {
                // According to the documentation (http://msdn.microsoft.com/en-us/library/system.web.httprequest.urlreferrer.aspx),
                // UrlReferrer could throw UriFormatException in case The HTTP Referer request header is malformed and cannot be converted to a Uri object. 
                return false;
            }
            return false;
        }

        /// <summary>
        /// Gets the claims issuer.
        /// </summary>
        /// <returns></returns>
        private string GetClaimsIssuer()
        {
            var claimsModule = SitefinityClaimsAuthenticationModule.Current;

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

        private string serviceUrl;
        private const string DefaultRealmConfig = "http://localhost";
        private string membershipProvider;

        #endregion
    }
}
