using System;
using System.Linq;
using System.Web;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Claims;
using Telerik.Sitefinity.Security.Configuration;
using Telerik.Sitefinity.Security.Model;
using Telerik.Sitefinity.Web;

namespace Telerik.Sitefinity.Frontend.Identity.Mvc.Models.LoginStatus
{
    public class LoginStatusModel : ILoginStatusModel
    {
        private readonly string LogoutUrl = "/Sitefinity/Logout";

        /// <inheritdoc />
        public Guid? LogoutRedirectPageId { get; set; }

        /// <inheritdoc />
        public string LogoutRedirectUrl { get; set; }

        /// <inheritdoc />
        public virtual string GetRedirectUrl()
        {
            // TODO: Remove
            return "www.abv.bg";

            if (this.LogoutRedirectPageId.HasValue)
            {
                return PageManager.GetManager().GetPageNode(LogoutRedirectPageId.Value).Urls.FirstOrDefault().Url;
            }
            else
            {
                return this.LogoutRedirectUrl;
            }
        }

        /// <inheritdoc />
        public Guid? LogoinPageId { get; set; }

        /// <inheritDoc/>
        public string ExternalLoginUrl { get; set; }

        /// <inheritDoc/>
        public string LoginNameFormatString { get; set; }

        /// <inheritDoc/>
        public bool ShowLoginName { get; set; }

        /// <inheritDoc/>
        public LoginStatusViewModel GetViewModel()
        {
            var redirectUrl = LogoutUrl;

            var pageRedirectUrl = this.GetRedirectUrl();
            if (!string.IsNullOrEmpty(pageRedirectUrl))
            {
                redirectUrl += "?redirect_uri=" + pageRedirectUrl;
            }

            return new LoginStatusViewModel()
            {
                RedirectUrl = redirectUrl,
                LoginRedirectUrl = this.GetLoginRedirectUrl()
            };
        }

        /// <inheritdoc />
        public StatusViewModel GetStatusViewModel()
        {
            var response = new StatusViewModel() { IsLoggedIn = false };

            var user = SecurityManager.GetUser(SecurityManager.GetCurrentUserId());

            if (user != null)
            {
                var profile = UserProfileManager.GetManager().GetUserProfile<SitefinityProfile>(user);
                var displayNameBuilder = new SitefinityUserDisplayNameBuilder();

                response.IsLoggedIn = true;
                response.Email = user.Email;
                response.DisplayName = displayNameBuilder.GetUserDisplayName(user.Id);
            }

            return response;
        }

        delegate string GetFrontEndLoginInternal(HttpContextBase httpContext, out Telerik.Sitefinity.Web.Events.RedirectStrategyType redirectStrategy);
        GetFrontEndLoginInternal frontendLoginPage;

        /// <summary>
        /// Gets the login redirect URL.
        /// </summary>
        /// <returns></returns>
        public virtual string GetLoginRedirectUrl()
        {
            var loginRedirectUrl = this.ExternalLoginUrl;
            if (string.IsNullOrEmpty(loginRedirectUrl))
            {
                var claimsModule = SitefinityClaimsAuthenticationModule.Current;

                string pageUrl;
                if (this.LogoinPageId.HasValue)
                {
                    pageUrl = PageManager.GetManager().GetPageNode(this.LogoinPageId.Value).Urls.FirstOrDefault().Url;
                }
                else
                {
                    Telerik.Sitefinity.Web.Events.RedirectStrategyType redirectStrategy = Telerik.Sitefinity.Web.Events.RedirectStrategyType.None;
                    var wrapper = new HttpContextWrapper(HttpContext.Current);
                    frontendLoginPage = (GetFrontEndLoginInternal)Delegate.CreateDelegate(
                        typeof(GetFrontEndLoginInternal),
                        this,
                        typeof(RouteHelper).GetMethod("GetFrontEndLogin"));
                    pageUrl = frontendLoginPage(wrapper, out redirectStrategy);
                    pageUrl = pageUrl ?? claimsModule.GetIssuer();
                }

                var currentUrl = HttpContext.Current.Request.RawUrl;
                var returnUrl = this.AppendUrlParameter(currentUrl, LoginStatusModel.HandleRejectedUser, "true");
                loginRedirectUrl = "{0}?realm={1}&redirect_uri={2}&deflate=true".Arrange(
                    pageUrl, claimsModule.GetRealm(), HttpUtility.UrlEncode(returnUrl));
            }

            return loginRedirectUrl;
        }

        /// <summary>
        /// Appends the URL parameter.
        /// </summary>
        /// <param name="baseUrl">The base URL.</param>
        /// <param name="paramName">Name of the parameter.</param>
        /// <param name="paramValue">The parameter value.</param>
        /// <returns></returns>
        private string AppendUrlParameter(string baseUrl, string paramName, string paramValue)
        {
            string delimiter = "?";
            if (baseUrl.Contains(delimiter))
            {
                delimiter = "&";
            }
            baseUrl += string.Format("{0}{1}={2}", delimiter, paramName, paramValue);
            return baseUrl;
        }

        private const string HandleRejectedUser = "sf-hru";
    }
}
