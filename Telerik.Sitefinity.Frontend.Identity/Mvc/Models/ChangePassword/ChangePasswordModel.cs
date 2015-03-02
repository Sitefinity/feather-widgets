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

        /// <inheritDoc/>
        public Guid? ChangePasswordRedirectPageId { get; set; }

        /// <inheritDoc/>
        public ChangePasswordCompleteAction ChangePasswordCompleteAction { get; set; }

        #endregion

        #region Public Methods

        /// <inheritDoc/>
        public virtual ChangePasswordViewModel GetViewModel()
        {
            return new ChangePasswordViewModel()
            {
                CssClass = this.CssClass
            };
        }

        /// <inheritDoc/>
        public string GetPageUrl(Guid? pageId)
        {
            if (!pageId.HasValue)
            {
                pageId = SiteMapBase.GetActualCurrentNode().Id;
            }

            return HyperLinkHelpers.GetFullPageUrl(pageId.Value);
        }

        #endregion

        #region Private methods and fields

        private string membershipProvider;

        #endregion
    }
}
