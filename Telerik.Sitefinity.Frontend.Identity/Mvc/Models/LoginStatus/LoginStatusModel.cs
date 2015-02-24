using System;
using System.Linq;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Model;

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
                RedirectUrl = redirectUrl
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
    }
}
