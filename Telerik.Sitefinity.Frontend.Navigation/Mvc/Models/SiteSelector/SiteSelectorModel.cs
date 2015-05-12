using System;
using System.Linq;

namespace Telerik.Sitefinity.Frontend.Navigation.Mvc.Models.SiteSelector
{
    public class SiteSelectorModel : ISiteSelectorModel
    {
        public SiteSelectorViewModel CreateViewModel()
        {
            var vm = new SiteSelectorViewModel();

            vm.CssClass = this.CssClass;
            vm.IncludeCurrentSite = this.IncludeCurrentSite;
            vm.ShowLanguagesOnly = this.ShowLanguagesOnly;
            vm.ShowSiteNamesAndLanguages = this.ShowSiteNamesAndLanguages;

            return vm;
        }

        /// <summary>
        /// Gets or sets the CSS class that will be applied on the wrapper div of the Site selector widget (if such is presented).
        /// </summary>
        /// <value>The CSS class.</value>
        public string CssClass { get; set; }

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
    }
}
