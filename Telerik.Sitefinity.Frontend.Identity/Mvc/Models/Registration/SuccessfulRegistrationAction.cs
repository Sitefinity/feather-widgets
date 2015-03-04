using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Telerik.Sitefinity.Frontend.Identity.Mvc.Models.Registration
{
    /// <summary>
    /// Represents the action that will be executed when the registration form is successfully submitted.
    /// </summary>
    public enum SuccessfulRegistrationAction
    {
        /// <summary>
        /// Specified message will be displayed on the same page.
        /// </summary>
        ShowMessage,

        /// <summary>
        /// Specially prepared page will be opened.
        /// </summary>
        RedirectToPage
    }
}
