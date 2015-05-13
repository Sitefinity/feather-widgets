using System;
using System.Collections.Generic;
using System.Linq;

namespace Telerik.Sitefinity.Frontend.Navigation.Mvc.Models.SiteSelector
{
    /// <summary>
    /// The view model of the site selector widget.
    /// </summary>
    public class SiteSelectorViewModel
    {
        #region Properties
        /// <summary>
        /// Determines whether to include the current site in the list of sites.
        /// </summary>
        /// <value>The include current site.</value>
        public bool IncludeCurrentSite { get; set; }

        /// <summary>
        /// Determines whether to display each language version as a separate site and show only the language.
        /// </summary>
        /// <value>Value that indicates whether to show languages only.</value>
        public bool ShowLanguagesOnly { get; set; }

        /// <summary>
        /// Determines whether to display each language version as a separate site and show the site name and its language.
        /// </summary>
        /// <value>Value that indicates whether to show site name and its language.</value>
        public bool ShowSiteNamesAndLanguages { get; set; }

        /// <summary>
        /// Gets or sets the sites.
        /// </summary>
        /// <value>The sites.</value>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public IList<SiteViewModel> Sites { get; set; }

        /// <summary>
        /// Gets or sets the CSS class that will be applied on the wrapper div of the Site selector widget (if such is presented).
        /// </summary>
        /// <value>The CSS class.</value>
        public string CssClass { get; set; }
        #endregion
    }
}
