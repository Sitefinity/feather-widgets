using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Telerik.Sitefinity.Frontend.Navigation.Mvc.Models.LanguageSelector
{
    /// <summary>
    /// The Language selector widget options what to do with languages without translations. 
    /// </summary>
    public enum NoTranslationAction
    {
        /// <summary>
        /// The link(item) for the non-available languages will not be displayed.
        /// </summary>
        HideLink,

        /// <summary>
        /// The link(item) for the non-available languages will be displayed and will link to a specified page.
        /// </summary>
        RedirectToPage
    }
}
