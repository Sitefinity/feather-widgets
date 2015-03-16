using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telerik.Sitefinity.Frontend.Mvc.Helpers;
using Telerik.Sitefinity.Security;
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

        /// <inheritdoc />
        public bool SendEmailOnChangePassword { get; set; }

        /// <inheritDoc/>
        public Guid? ChangePasswordRedirectPageId { get; set; }

        /// <inheritDoc/>
        public ChangePasswordCompleteAction ChangePasswordCompleteAction { get; set; }

        #endregion

        #region Public Methods

        /// <inheritDoc/>
        public void ChangePassword(Guid userId, string oldPassword, string newPassword)
        {
            UserManager.ChangePasswordForUser(UserManager.GetManager(this.MembershipProvider), userId, oldPassword, newPassword, this.SendEmailOnChangePassword);
        }

        /// <inheritDoc/>
        public virtual ChangePasswordViewModel GetViewModel()
        {
            return new ChangePasswordViewModel()
            {
                CssClass = this.CssClass
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
