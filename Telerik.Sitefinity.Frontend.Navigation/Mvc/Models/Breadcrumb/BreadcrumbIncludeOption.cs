using System;
using System.Linq;

namespace Telerik.Sitefinity.Frontend.Navigation.Mvc.Models.Breadcrumb
{
    /// <summary>
    /// The rendering options for the Breadcrumb widget. 
    /// </summary>
    public enum BreadcrumbIncludeOption
    {
        /// <summary>
        /// Refers to full path to the current page.
        /// </summary>
        CurrentPageFullPath,

        /// <summary>
        /// Refers to path starting from a specifi page.
        /// </summary>
        SpecificPagePath
    }
}
