using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telerik.Sitefinity.Frontend.Mvc.Helpers;
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

        /// <inheritdoc />
        public string CssClass { get; set; }

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
                RedirectUrlAfterLogin = HyperLinkHelpers.GetFullPageUrl(this.LoginRedirectPageId.Value),
                Realm = SitefinityClaimsAuthenticationModule.Current.GetRealm(),
                CssClass = this.CssClass
            };
        }

        /// <inheritDoc/>
        public ResetPasswordViewModel GetResetPasswordViewModel()
        {
            return new ResetPasswordViewModel()
            {
                CssClass = this.CssClass
            };
        }

        /// <inheritDoc/>
        public ForgotPasswordViewModel GetForgotPasswordViewModel()
        {
            return new ForgotPasswordViewModel()
            {
                CssClass = this.CssClass
            };
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

        private string serviceUrl;
        private const string DefaultRealmConfig = "http://localhost";
        private string membershipProvider;

        #endregion
    }
}
