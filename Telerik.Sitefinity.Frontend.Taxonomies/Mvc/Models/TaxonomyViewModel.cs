using System;
using System.Collections.Generic;
using System.Linq;

namespace Telerik.Sitefinity.Frontend.Taxonomies.Mvc.Models
{
    /// <summary>
    /// This class represents the view model of the taxonomy item.
    /// </summary>
    public class TaxonomyViewModel
    {
        /// <summary>
        /// Gets or sets the taxa that belongs to this taxonomy.
        /// </summary>
        /// <value>The taxa.</value>
        public IList<TaxonViewModel> Taxa { get; set; }

        /// <summary>
        /// Determines whether to display number of items classified by the taxon.
        /// </summary>
        public bool ShowItemCount { get; set; }
    }
}
