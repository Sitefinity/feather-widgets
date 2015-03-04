using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Telerik.Sitefinity.Frontend.Mvc.Helpers;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Claims;
using Telerik.Sitefinity.SitefinityExceptions;
using Telerik.Sitefinity.Web;

namespace Telerik.Sitefinity.Frontend.Identity.Mvc.Models.AccountActivation
{
    /// <summary>
    /// This class is used as a model for the <see cref="AccountActivationController"/>.
    /// </summary>
    public class AccountActivationModel : IAccountActivationModel
    {
        /// <inheritdoc />
        public string CssClass { get; set; }

        /// <inheritDoc/>
        public Guid? ProfilePageId { get; set; }

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
        public virtual AccountActivationViewModel GetViewModel()
        {
            var activationSuccess = this.ActivateAccount();

            return new AccountActivationViewModel()
            {
                CssClass = this.CssClass,
                ProfilePageUrl = this.GetPageUrl(this.ProfilePageId),
                Activated = activationSuccess
            };
        }

        /// <summary>
        /// Activates the account.
        /// </summary>
        /// <returns>
        /// <c>true</c> if it succeeded; otherwise, <c>false</c>.
        /// </returns>
        private bool ActivateAccount()
        {
            bool success = false;

            var userManager = UserManager.GetManager(this.MembershipProvider);
            var userProviderSuppressSecurityChecks = userManager.Provider.SuppressSecurityChecks;

            try
            {
                var userId = this.GetUserId();

                userManager.Provider.SuppressSecurityChecks = true;
                var user = userManager.GetUser(userId);
                user.IsApproved = true;
                userManager.SaveChanges();

                success = true;
            }
            catch (ItemNotFoundException)
            {
                success = false;
            }
            finally
            {
                userManager.Provider.SuppressSecurityChecks = userProviderSuppressSecurityChecks;
            }

            return success;
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

        private string GetPageUrl(Guid? pageId)
        {
            if (!pageId.HasValue)
            {
                pageId = SiteMapBase.GetActualCurrentNode().Id;
            }

            return HyperLinkHelpers.GetFullPageUrl(pageId.Value);
        }

        #region Private Fields and methods

        private string membershipProvider;

        #endregion
    }
}
