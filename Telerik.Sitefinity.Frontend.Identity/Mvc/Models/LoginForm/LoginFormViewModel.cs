using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Telerik.Sitefinity.Frontend.Identity.Mvc.Models.LoginForm
{
    /// <summary>
    /// This class represents view model for the <see cref="LoginFormController"/>.
    /// </summary>
    public class LoginFormViewModel
    {
        public virtual string UserName { get; set; }
        public virtual string Password { get; set; }
        public virtual bool RememberMe { get; set; }
    }
}
