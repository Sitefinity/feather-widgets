using System;
using System.Linq;

namespace Telerik.Sitefinity.Frontend.Navigation.Mvc.Models.SiteSelector
{
    /// <summary>
    /// The model of the site selector widget.
    /// </summary>
    public interface ISiteSelectorModel
    {
        /// <summary>
        /// Determines whether to include the current site in the list of sites.
        /// </summary>
        /// <value>The include current site.</value>
        bool IncludeCurrentSite { get; set; }

        /// <summary>
        /// Determines whether to display each language version as a separate site and show only the language.
        /// </summary>
        /// <value>Value that indicates whether to show languages only.</value>
        bool ShowLanguagesOnly { get; set; }

        /// <summary>
        /// Determines whether to display each language version as a separate site and show the site name and its language.
        /// </summary>
        /// <value>Value that indicates whether to show site name and its language.</value>
        bool ShowSiteNamesAndLanguages { get; set; }

        /// <summary>
        /// Determines whether to use the live or the staging URLs of the sites.
        /// </summary>
        /// <value>The use live URL.</value>
        bool UseLiveUrl { get; set; }

        /// <summary>
        /// Gets or sets the CSS class that will be applied on the wrapper div of the Site selector widget (if such is presented).
        /// </summary>
        /// <value>
        /// The CSS class.
        /// </value>
        string CssClass { get; set; }

        /// <summary>
        /// Creates the site selector view model.
        /// </summary>
        SiteSelectorViewModel CreateViewModel();
    }
}
