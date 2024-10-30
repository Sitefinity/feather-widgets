using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Frontend.Mvc.Helpers;
using Telerik.Sitefinity.Modules.UserProfiles;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Configuration;
using Telerik.Sitefinity.Web;

namespace Telerik.Sitefinity.Frontend.Identity.Mvc.Models.ChangePassword
{
    /// <summary>
    /// This class is used as a model for the <see cref="ChangePasswordController"/>.
    /// </summary>
    public class ChangePasswordModel : IChangePasswordModel
    {
        #region Properties

        /// <inheritDoc/>
        public string MembershipProvider
        {
            get
            {
                return this.membershipProvider;
            }
            set
            {
                this.membershipProvider = value;
            }
        }

        /// <inheritdoc />
        public string CssClass { get; set; }

        /// <inheritdoc />
        public bool SendEmailOnChangePassword { get; set; }

        /// <inheritDoc/>
        public Guid? ChangePasswordRedirectPageId { get; set; }

        /// <inheritDoc/>
        public ChangePasswordCompleteAction ChangePasswordCompleteAction { get; set; }

        #endregion

        #region Public Methods

        /// <inheritDoc/>
        public virtual void ChangePassword(Guid userId, string oldPassword, string newPassword)
        {
            var providerName = this.MembershipProvider;
            var currentIdentity = Sitefinity.Security.Claims.ClaimsManager.GetCurrentIdentity();
            if (currentIdentity != null && currentIdentity.UserId == userId)
                providerName = currentIdentity.MembershipProvider;

            UserManager.ChangePasswordForUser(UserManager.GetManager(providerName), userId, oldPassword, newPassword, Config.Get<SecurityConfig>().UserRegistrationSettings.SendEmailOnPasswordChanged);
        }

        /// <inheritDoc/>
        public virtual ChangePasswordViewModel GetViewModel()
        {
            var currentIdentity = Sitefinity.Security.Claims.ClaimsManager.GetCurrentIdentity();
            var userId = currentIdentity.UserId;
            var userManager = UserManager.GetManager(currentIdentity.MembershipProvider);
            var user = userManager.GetUser(userId);

            return new ChangePasswordViewModel()
            {
                CssClass = this.CssClass,
                ExternalProviderName = user.ExternalProviderName
            };
        }

        /// <inheritDoc/>
        public string GetErrorFromViewModel(System.Web.Mvc.ModelStateDictionary modelStateDict)
        {
            var firstErrorValue = modelStateDict.Values.FirstOrDefault(v => v.Errors.Any());
            if (firstErrorValue != null)
            {
                var firstError = firstErrorValue.Errors.FirstOrDefault();
                if (firstError != null)
                {
                   return firstError.ErrorMessage;
                }
            }

            return null;
        }

        /// <inheritDoc/>
        public string GetPageUrl(Guid? pageId)
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

        #endregion

        #region Private methods and fields

        private string membershipProvider;

        #endregion
    }
}
