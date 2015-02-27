using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telerik.Sitefinity.Security;

namespace Telerik.Sitefinity.Frontend.Identity.Mvc.Models.Registration
{
    /// <summary>
    /// This class is used as a model for the <see cref="RegistrationController"/>.
    /// </summary>
    public class RegistrationModel : IRegistrationModel
    {
        /// <inheritDoc/>
        public Guid? LoginPageId { get; set; }

        /// <inheritDoc/>
        public string CssClass { get; set; }

        /// <inheritDoc/>
        public string MembershipProviderName
        {
            get
            {
                this.membershipProviderName = this.membershipProviderName ?? UserManager.GetDefaultProviderName();
                return this.membershipProviderName;
            }
            set
            {
                this.membershipProviderName = value;
            }
        }

        /// <inheritDoc/>
        public bool SendEmailOnSuccess { get; set; }

        /// <inheritDoc/>
        public ActivationMethod ActivationMethod { get; set; }

        /// <inheritDoc/>
        public RegistrationViewModel GetViewModel()
        {
            return new RegistrationViewModel()
            {
                LoginPageUrl = this.GetLoginPageUrl(),
                MembershipProviderName = this.MembershipProviderName,
                CssClass = this.CssClass
            };
        }

        /// <summary>
        /// Gets the login redirect URL.
        /// </summary>
        /// <returns></returns>
        public virtual string GetLoginPageUrl()
        {
            return null;
        }

        private string membershipProviderName;
    }
}
