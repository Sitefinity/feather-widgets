using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using Telerik.Sitefinity.Frontend.Mvc.Helpers;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Claims;
using Telerik.Sitefinity.SitefinityExceptions;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Web.UI;

namespace Telerik.Sitefinity.Frontend.Identity.Mvc.Models.AccountActivation
{
    /// <summary>
    /// This class is used as a model for the <see cref="AccountActivationController"/>.
    /// </summary>
    public class AccountActivationModel : IAccountActivationModel
    {
        #region Properties

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

        #endregion

        #region Public Methods

        /// <inheritDoc/>
        public virtual AccountActivationViewModel GetViewModel()
        {
            var shouldAttemptActivation = this.ShouldAttemptActivation();
            var activationSuccess = false;

            if (shouldAttemptActivation)
            {
                activationSuccess = this.ActivateAccount();
            }

            return new AccountActivationViewModel()
            {
                CssClass = this.CssClass,
                ProfilePageUrl = this.GetPageUrl(this.ProfilePageId),
                Activated = activationSuccess,
                AttemptedActivation = shouldAttemptActivation
            };
        }

        #endregion

        #region Private Fields and methods

        /// <summary>
        /// Activates the account.
        /// </summary>
        /// <returns>
        /// <c>true</c> if it succeeded; otherwise, <c>false</c>.
        /// </returns>
        private bool ActivateAccount()
        {
            bool success = false;

            Guid userId = this.GetUserId();
            if (userId == Guid.Empty)
            {
                return false;
            }

            UserManager userManager = this.GetUserManager();
            var userProviderSuppressSecurityChecks = userManager.Provider.SuppressSecurityChecks;

            try
            {
                userManager.Provider.SuppressSecurityChecks = true;
                var user = userManager.GetUser(userId);
                user.IsApproved = true;
                userManager.SaveChanges();

                success = true;
            }
            catch (Exception)
            {
                success = false;
            }
            finally
            {
                userManager.Provider.SuppressSecurityChecks = userProviderSuppressSecurityChecks;
            }

            return success;
        }

        protected virtual UserManager GetUserManager()
        {
            string provider;

            if (this.TryGetProviderFromQuery(out provider))
            {
                return this.GetUserManager(provider);
            }
            return this.GetUserManager(this.MembershipProvider);
        }

        protected virtual UserManager GetUserManager(string provider)
        {
            return UserManager.GetManager(provider);
        }

        /// <summary>
        /// Gets the query string.
        /// </summary>
        /// <returns></returns>
        protected virtual NameValueCollection GetQueryString()
        {
            return HttpContext.Current.Request.QueryString;
        }

        /// <summary>
        /// Gets the page URL.
        /// </summary>
        /// <param name="pageId">The page id.</param>
        /// <returns></returns>
        protected virtual string GetPageUrl(Guid? pageId)
        {
            if (!pageId.HasValue)
            {
                var currentNode = SiteMapBase.GetActualCurrentNode();
                if (currentNode == null)
                    return null;

                pageId = currentNode.Id;
            }

            return HyperLinkHelpers.GetFullPageUrl(pageId.Value);
        }

        /// <summary>
        /// Inners the get user identifier.
        /// </summary>
        /// <returns>
        /// The user id or null.
        /// </returns>
        private Guid GetUserId()
        {
            NameValueCollection queryString = this.GetQueryString();

            string userIdString = queryString.Get("user");

            Guid userId;

            Guid.TryParse(userIdString, out userId);

            return userId;
        }

        private bool TryGetProviderFromQuery(out string provider)
        {
            NameValueCollection queryString = this.GetQueryString();

            provider = queryString.Get("provider");

            return provider != null;
        }

        private bool ShouldAttemptActivation()
        {
            NameValueCollection queryString = this.GetQueryString();

            if (queryString != null)
            {
                return !string.IsNullOrEmpty(queryString.Get("user"));
            }

            return false;
        }

        private string membershipProvider;

        #endregion
    }
}
