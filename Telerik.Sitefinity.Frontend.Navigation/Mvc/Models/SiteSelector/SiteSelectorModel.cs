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
            vm.LanguageVersionsAsSeparateSites = this.LanguageVersionsAsSeparateSites;

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
        /// Determines whether to display each language version as a separate site in the list of sites.
        /// </summary>
        /// <value>The each language as separate site.</value>
        public bool LanguageVersionsAsSeparateSites { get; set; }
    }
}
