﻿using System;
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
        public virtual AccountActivationViewModel GetViewModel(NameValueCollection securityParams)
        {
            bool activationAttempted;
            var activationSuccess = this.ActivateAccount(securityParams, out activationAttempted);

            return new AccountActivationViewModel()
            {
                CssClass = this.CssClass,
                ProfilePageUrl = this.GetPageUrl(this.ProfilePageId),
                Activated = activationSuccess,
                AttemptedToActivate = activationAttempted
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
        private bool ActivateAccount(NameValueCollection securityParams, out bool activationAttempted)
        {
            bool success = false;
            Guid userId;

            activationAttempted = this.TryGetUserId(securityParams, out userId);

            if (!activationAttempted || userId == Guid.Empty)
            {
                return false;
            }

            UserManager userManager = this.GetUserManager(securityParams);
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

        protected virtual UserManager GetUserManager(NameValueCollection securityParams)
        {
            string provider;

            if (this.TryGetProvider(securityParams, out provider))
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
        /// Gets the page URL.
        /// </summary>
        /// <param name="pageId">The page id.</param>
        /// <returns></returns>
        protected virtual string GetPageUrl(Guid? pageId)
        {
            if (!pageId.HasValue)
            {
                pageId = SiteMapBase.GetActualCurrentNode().Id;
            }

            return HyperLinkHelpers.GetFullPageUrl(pageId.Value);
        }

        /// <summary>
        /// Inners the get user identifier.
        /// </summary>
        /// <returns>
        /// The user id or null.
        /// </returns>
        private bool TryGetUserId(NameValueCollection securityParams, out Guid userId)
        {
            string userIdString = securityParams.Get("user");

            if (!string.IsNullOrEmpty(userIdString))
            {
                return Guid.TryParse(userIdString, out userId);
            }

            userId = Guid.Empty;
            return false;
        }

        private bool TryGetProvider(NameValueCollection securityParams, out string provider)
        {
            provider = securityParams.Get("provider");

            return !string.IsNullOrEmpty(provider);
        }

        private string membershipProvider;

        #endregion
    }
}
