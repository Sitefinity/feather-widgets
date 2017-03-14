using System;
using System.Linq;
using System.Web;
using Telerik.Sitefinity.Frontend.Mvc.Helpers;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Claims;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web;

namespace Telerik.Sitefinity.Frontend.Identity.Mvc.Models.LoginStatus
{
    /// <summary>
    /// This class is used as a model for the LoginStatusController.
    /// </summary>
    public class LoginStatusModel : ILoginStatusModel
    {
        #region Construction
        public LoginStatusModel(string currentPageUrl)
        {
            this.currentPageUrl = currentPageUrl;
        }
        #endregion

        #region Properties

        /// <inheritdoc />
        public Guid? LoginPageId { get; set; }

        /// <inheritdoc />
        public Guid? LogoutPageId { get; set; }

        /// <inheritdoc />
        public Guid? RegistrationPageId { get; set; }

        /// <inheritdoc />
        public Guid? ProfilePageId { get; set; }

        /// <inheritDoc/>
        public string ExternalLoginUrl { get; set; }

        /// <inheritDoc/>
        public string ExternalLogoutUrl { get; set; }

        /// <inheritDoc/>
        public string ExternalRegistrationUrl { get; set; }

        /// <inheritDoc/>
        public string ExternalProfileUrl { get; set; }

        /// <inheritDoc/>
        public bool AllowWindowsStsLogin { get; set; }

        /// <inheritdoc />
        public string CssClass { get; set; }

        #endregion

        #region Virtual Methods

        /// <inheritDoc/>
        public virtual string GetLoginPageUrl()
        {
            var loginRedirectUrl = this.ExternalLoginUrl;

            if (string.IsNullOrEmpty(loginRedirectUrl))
            {   
                string pageUrl;

                if (this.LoginPageId.HasValue)
                {
                    pageUrl = HyperLinkHelpers.GetFullPageUrl(this.LoginPageId.Value);
                }
                else
                {
                    pageUrl = SitefinityContext.FrontendLoginUrl;
                }

                loginRedirectUrl = pageUrl;
            }

            return loginRedirectUrl;
        }

        /// <inheritdoc />
        public virtual string GetLogoutPageUrl()
        {
            string logoutRedirectUrl = this.ExternalLogoutUrl;

            if (string.IsNullOrEmpty(logoutRedirectUrl))
            {
                if (this.LogoutPageId.HasValue)
                {
                    logoutRedirectUrl = HyperLinkHelpers.GetFullPageUrl(this.LogoutPageId.Value);
                }
                else
                {
                    logoutRedirectUrl = UrlPath.ResolveAbsoluteUrl(this.currentPageUrl, true);
                }                
            }

            if (HttpContext.Current.Request.Url == null)
                return string.Empty;

            return logoutRedirectUrl;
        }

        /// <inheritdoc />
        public virtual string GetRegistrationPageUrl()
        {
            var registrationRedirectUrl = this.ExternalRegistrationUrl;
            if (string.IsNullOrEmpty(registrationRedirectUrl) && this.RegistrationPageId.HasValue)
            {
                registrationRedirectUrl = HyperLinkHelpers.GetFullPageUrl(this.RegistrationPageId.Value);
            }

            return registrationRedirectUrl;
        }

        /// <inheritdoc />
        public virtual string GetProfilePageUrl()
        {
            var profileRedirectUrl = this.ExternalProfileUrl;
            if (string.IsNullOrEmpty(profileRedirectUrl) && this.ProfilePageId.HasValue)
            {
                profileRedirectUrl = HyperLinkHelpers.GetFullPageUrl(this.ProfilePageId.Value);
            }

            return profileRedirectUrl;
        }
        #endregion

        #region Public Methods
        /// <inheritDoc/>
        public virtual LoginStatusViewModel GetViewModel()
        {
            return new LoginStatusViewModel()
            {
                LogoutPageUrl = this.GetLogoutPageUrl(),
                ProfilePageUrl = this.GetProfilePageUrl(),
                RegistrationPageUrl = this.GetRegistrationPageUrl(),
                LoginPageUrl = this.GetLoginPageUrl(),
                CssClass = this.CssClass,
                AllowWindowsStsLogin = this.AllowWindowsStsLogin,
                StatusServiceUrl = RouteHelper.ResolveUrl("~/rest-api/login-status", UrlResolveOptions.Rooted)
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

        #endregion

        #region Private members

        /// <summary>
        /// Appends the URL parameter.
        /// </summary>
        /// <param name="baseUrl">The base URL.</param>
        /// <param name="paramName">Name of the parameter.</param>
        /// <param name="paramValue">The parameter value.</param>
        /// <returns><
        /// The url.
        /// /returns>
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

        #region Private fields and constants
        private string currentPageUrl;
        #endregion
    }
}
