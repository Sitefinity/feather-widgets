using System;
using System.Linq;

namespace Telerik.Sitefinity.Frontend.Navigation.Mvc.Models.SiteSelector
{
    public class SiteViewModel
    {
        /// <summary>
        /// Gets or sets whether the site is the current one.
        /// </summary>
        /// <value>The is current.</value>
        public bool IsCurrent { get; set; }

        /// <summary>
        /// Gets or sets the name of the site that will be visible to the users.
        /// </summary>
        /// <value>The display name.</value>
        public string DisplayName { get; set; }

        /// <summary>
        /// Gets or sets the URL of the site.
        /// </summary>
        /// <value>The URL.</value>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1056:UriPropertiesShouldNotBeStrings")]
        public string Url { get; set; }
    }
}
