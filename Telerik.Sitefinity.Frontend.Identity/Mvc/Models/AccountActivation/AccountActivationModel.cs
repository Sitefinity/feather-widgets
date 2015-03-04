using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telerik.Sitefinity.Frontend.Mvc.Helpers;
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
        public virtual AccountActivationViewModel GetViewModel()
        {
            return new AccountActivationViewModel()
            {
                CssClass = this.CssClass,
                ProfilePageUrl = this.GetPageUrl(this.ProfilePageId)
            };
        }

        private string GetPageUrl(Guid? pageId)
        {
            if (!pageId.HasValue)
            {
                pageId = SiteMapBase.GetActualCurrentNode().Id;
            }

            return HyperLinkHelpers.GetFullPageUrl(pageId.Value);
        }
    }
}
