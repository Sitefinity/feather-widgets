using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Claims;
using Telerik.Sitefinity.Security.Configuration;
using Telerik.Sitefinity.Web;

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

        /// <inheritDoc/>
        public Guid? LoginRedirectPageId { get; set; }

        #endregion 

        #region Public Methods

        /// <inheritDoc/>
        public LoginFormViewModel GetLoginFormViewModel()
        {
            return new LoginFormViewModel() 
            {
                ServiceUrl = this.ServiceUrl,
                MembershipProvider = this.MembershipProvider,
                RedirectUrlAfterLogin = this.GetPageUrl(this.LoginRedirectPageId),
                Realm = SitefinityClaimsAuthenticationModule.Current.GetRealm()
            };
        }

        /// <inheritDoc/>
        public ResetPasswordViewModel GetResetPasswordViewModel()
        {
            return new ResetPasswordViewModel();
        }

        /// <inheritDoc/>
        public ForgotPasswordViewModel GetForgotPasswordViewModel()
        {
            return new ForgotPasswordViewModel();
        }

        #endregion

        #region Private Fields and methods

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

            return null;
        }

        private string serviceUrl;
        private const string DefaultRealmConfig = "http://localhost";
        private string membershipProvider;

        #endregion
    }
}
