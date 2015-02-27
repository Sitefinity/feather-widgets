using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Telerik.Sitefinity.Frontend.Mvc.Helpers;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Claims;
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

        /// <inheritDoc/>
        public bool EnablePasswordRetrieval 
        {
            get
            {
                return UserManager.GetManager(this.MembershipProvider).EnablePasswordRetrieval;
            }

            set
            {
            }
        }

        /// <inheritDoc/>
        public bool EnablePasswordReset
        {
            get
            {
                return UserManager.GetManager(this.MembershipProvider).EnablePasswordReset;
            }

            set
            {
            }
        }

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

        /// <inheritDoc/>
        public void ResetUserPassword(string newPassword)
        {
            var userId = this.InnerGetUserId();

            if (userId.HasValue)
            {
                var userManager = UserManager.GetManager(this.MembershipProvider);

                var resetPassword = userManager.ResetPassword(userId.Value, null);
                userManager.ChangePassword(userId.Value, resetPassword, newPassword);
                userManager.SaveChanges();
            }
            else
            {
                throw new ArgumentNullException("User could not be retrieved.");
            }
        }

        /// <inheritDoc/>
        public bool TrySendResetPasswordEmail(string userEmail)
        {
            // TODO: Implement
            return false;
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

        /// <summary>
        /// Inners the get user identifier.
        /// </summary>
        /// <returns>
        /// The user id or null.
        /// </returns>
        private Guid? InnerGetUserId()
        {
            Type type = Type.GetType("Telerik.Sitefinity.Security.Web.UI.UserChangePasswordWidget, Telerik.Sitefinity");
            object instance = type.GetConstructor(Type.EmptyTypes).Invoke(new object[] { });
            MethodInfo method = type.GetMethod("GetUser", BindingFlags.NonPublic | BindingFlags.Instance);
            object claimsIdentityProxyObject = method.Invoke(instance, new object[] { });
            var claimsIdentityProxy = claimsIdentityProxyObject as ClaimsIdentityProxy;
            if (claimsIdentityProxy != null)
            {
                return claimsIdentityProxy.UserId;
            }

            return null;
        }

        private string serviceUrl;
        private const string DefaultRealmConfig = "http://localhost";
        private string membershipProvider;

        #endregion
    }
}
