using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Telerik.Sitefinity.Frontend.Identity.Mvc.Models.Registration
{
    /// <summary>
    /// Represents options for activation of user registration.
    /// </summary>
    public enum ActivationMethod
    {
        /// <summary>
        /// User account will be activated immediately after registration.
        /// </summary>
        Immediately,

        /// <summary>
        /// User account will be activated after confirmation
        /// </summary>
        AfterConfirmation
    }
}
