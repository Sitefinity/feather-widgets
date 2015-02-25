using System;
using System.Linq;
using System.Reflection;
using System.Web;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Claims;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Web.Events;

namespace Telerik.Sitefinity.Frontend.Identity.Mvc.Models.LoginStatus
{
    /// <summary>
    /// This class is used as a model for the LoginStatusController.
    /// </summary>
    public class LoginStatusModel : ILoginStatusModel
    {
        /// <inheritdoc />
        public Guid? LogoutPageId { get; set; }

        /// <inheritdoc />
        public Guid? ProfilePageId { get; set; }

        /// <inheritdoc />
        public Guid? RegistrationPageId { get; set; }

        /// <inheritdoc />
        public string CssClass { get; set; }

        /// <inheritdoc />
        public virtual string GetLogoutPageUrl()
        {
            return this.GetPageUrl(this.LogoutPageId);
        }

        /// <inheritdoc />
        public virtual string GetProfilePageUrl()
        {
            return this.GetPageUrl(this.ProfilePageId);
        }

        /// <inheritdoc />
        public virtual string GetRegistrationPageUrl()
        {
            return this.GetPageUrl(this.RegistrationPageId);
        }

        /// <inheritdoc />
        public Guid? LoginPageId { get; set; }

        /// <inheritDoc/>
        public string ExternalLoginUrl { get; set; }

        /// <inheritDoc/>
        public bool AllowInstantLogin { get; set; }

        /// <inheritDoc/>
        public LoginStatusViewModel GetViewModel()
        {
            return new LoginStatusViewModel()
            {
                LogoutPageUrl = this.GetLogoutPageUrl(),
                ProfilePageUrl = this.GetProfilePageUrl(),
                RegistrationPageUrl = this.GetRegistrationPageUrl(),
                LoginPageUrl = this.GetLoginPageUrl()
            };
        }

        /// <inheritdoc />
        public StatusViewModel GetStatusViewModel()
        {
            var response = new StatusViewModel() { IsLoggedIn = false };

            var user = SecurityManager.GetUser(SecurityManager.GetCurrentUserId());

            if (user != null)
            {
                Libraries.Model.Image avatarImage;

                var displayNameBuilder = new SitefinityUserDisplayNameBuilder();

                response.IsLoggedIn = true;
                response.Email = user.Email;
                response.DisplayName = displayNameBuilder.GetUserDisplayName(user.Id);
                response.AvatarImageUrl = displayNameBuilder.GetAvatarImageUrl(user.Id, out avatarImage);
            }

            return response;
        }

        /// <summary>
        /// Gets the login redirect URL.
        /// </summary>
        /// <returns></returns>
        public virtual string GetLoginPageUrl()
        {
            var loginRedirectUrl = this.ExternalLoginUrl;
            if (string.IsNullOrEmpty(loginRedirectUrl))
            {
                var claimsModule = SitefinityClaimsAuthenticationModule.Current;
                string pageUrl;


                if (this.AllowInstantLogin)
                {
                    pageUrl = claimsModule.GetIssuer();
                }
                else if (this.LoginPageId.HasValue)
                {
                    pageUrl = this.GetPageUrl(this.LoginPageId);
                }
                else
                {
                    pageUrl = GetLoginPageBackendSetting();
                }

                var currentUrl = HttpContext.Current.Request.RawUrl;
                var returnUrl = this.AppendUrlParameter(currentUrl, LoginStatusModel.HandleRejectedUser, "true");
                loginRedirectUrl = "{0}?realm={1}&redirect_uri={2}&deflate=true".Arrange(
                    pageUrl, claimsModule.GetRealm(), HttpUtility.UrlEncode(returnUrl));
            }

            return loginRedirectUrl;
        }

        #region Private members

        /// <summary>
        /// Gets the page URL by id.
        /// </summary>
        /// <returns></returns>
        private string GetPageUrl(Guid? pageId)
        {
            if (pageId.HasValue)
            {
                var pageManager = PageManager.GetManager();
                var node = pageManager.GetPageNode(pageId.Value);
                if (node != null)
                {
                    var relativeUrl = node.GetFullUrl();
                    return UrlPath.ResolveUrl(relativeUrl, true);
                }
            }
            return string.Empty;
        }

        /// <summary>
        /// Gets the login page backend setting.
        /// </summary>
        /// <returns></returns>
        private static string GetLoginPageBackendSetting()
        {
            RedirectStrategyType redirectStrategy = RedirectStrategyType.None;
            var wrapper = new HttpContextWrapper(HttpContext.Current);
            MethodInfo methodInfo = typeof(RouteHelper).GetMethod(
                "GetFrontEndLogin",
                BindingFlags.NonPublic | BindingFlags.Static,
                Type.DefaultBinder,
                new[] { typeof(HttpContextBase), typeof(RedirectStrategyType).MakeByRefType(), typeof(SiteMapProvider) },
                null
            );
            var inputParameters = new object[] { wrapper, redirectStrategy, null };
            var pageUrl = (string)methodInfo.Invoke(null, inputParameters);

            return pageUrl;
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

        #endregion
    }
}
