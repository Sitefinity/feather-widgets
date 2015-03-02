using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
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
        #region Constructors

        public LoginFormModel()
        {
            this.userManager = UserManager.GetManager(this.MembershipProvider);
        }

        #endregion


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
        public Guid? RegisterRedirectPageId { get; set; }

        /// <inheritDoc/>
        public bool EnablePasswordRetrieval 
        {
            get
            {
                return this.userManager.EnablePasswordRetrieval;
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
                return this.userManager.EnablePasswordReset;
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
                RedirectUrlAfterLogin = this.GetPageUrl(this.LoginRedirectPageId),
                RegisterPageUrl = this.GetPageUrl(this.RegisterRedirectPageId),
                Realm = SitefinityClaimsAuthenticationModule.Current.GetRealm(),
                CssClass = this.CssClass
            };
        }

        /// <inheritDoc/>
        public ResetPasswordViewModel GetResetPasswordViewModel()
        {
            return new ResetPasswordViewModel()
            {
                CssClass = this.CssClass,
                LoginPageUrl = this.GetPageUrl(null),
                RequiresQuestionAndAnswer = this.userManager.RequiresQuestionAndAnswer
            };
        }

        /// <inheritDoc/>
        public ForgotPasswordViewModel GetForgotPasswordViewModel()
        {
            return new ForgotPasswordViewModel()
            {
                CssClass = this.CssClass,
                LoginPageUrl = this.GetPageUrl(null)
            };
        }

        /// <inheritDoc/>
        public void ResetUserPassword(string newPassword, string answer)
        {
            var userId = this.GetUserId();

            if (userId == Guid.Empty)
            {
                throw new ArgumentNullException("User could not be retrieved.");
            }

            var resetPassword = this.userManager.ResetPassword(userId, answer);
            this.userManager.ChangePassword(userId, resetPassword, newPassword);
            this.userManager.SaveChanges();
        }

        /// <inheritDoc/>
        public bool TrySendResetPasswordEmail(string userEmail)
        {
            // TODO: Implement
            return false;
        }
        
        /// <inheritDoc/>
        public string GetErrorFromViewModel(System.Web.Mvc.ModelStateDictionary modelStateDict)
        {
            var error = "Invalid data";

            var firstErrorValue = modelStateDict.Values.FirstOrDefault(v => v.Errors.Any());
            if (firstErrorValue != null)
            {
                var firstError = firstErrorValue.Errors.FirstOrDefault();
                if (firstError != null)
                {
                    // Replaces all new lines (forbiden in url) with underscore.
                    error = Regex.Replace(firstError.ErrorMessage, @"(?:\r\n|[\r\n])", "_");
                }
            }

            return error;
        }

        /// <inheritDoc/>
        public string GetPageUrl(Guid? pageId)
        {
            if (pageId.HasValue)
            {
                return HyperLinkHelpers.GetFullPageUrl(pageId.Value);
            }
            else
            {
                return HyperLinkHelpers.GetFullPageUrl(SiteMapBase.GetActualCurrentNode().Id);
            }
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
        /// Inners the get user identifier.
        /// </summary>
        /// <returns>
        /// The user id or null.
        /// </returns>
        private Guid GetUserId()
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

            return Guid.Empty;
        }

        private string serviceUrl;
        private const string DefaultRealmConfig = "http://localhost";
        private string membershipProvider;
        private readonly UserManager userManager;

        #endregion
    }
}
