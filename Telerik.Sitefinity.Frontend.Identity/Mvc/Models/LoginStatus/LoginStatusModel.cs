using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Telerik.Sitefinity.Frontend.Identity.Mvc.Models.LoginStatus
{
    public class LoginStatusModel : ILoginStatusModel
    {
        public LoginStatusViewModel GetViewModel()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public string CssClass { get; set; }

        /// <inheritdoc />
        public Guid LoginPageId { get; set; }

        /// <inheritdoc />
        public Guid LogoutPageId { get; set; }

        /// <inheritdoc />
        public Guid ProfilePageId { get; set; }

        /// <inheritdoc />
        public Guid RegistrationPageId { get; set; }
    }
}
