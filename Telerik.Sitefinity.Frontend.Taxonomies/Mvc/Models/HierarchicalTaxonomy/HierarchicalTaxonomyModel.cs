using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Telerik.Sitefinity.Frontend.Taxonomies.Mvc.Models.HierarchicalTaxonomy
{
    /// <summary>
    /// The hierarchical taxonomy model.
    /// </summary>
    public class HierarchicalTaxonomyModel : TaxonomyModel, IHierarchicalTaxonomyModel
    {
        /// <summary>
        /// Determines what taxa will be displayed by the widget.
        /// </summary>
        /// <value>The taxa to display.</value>
        public HierarchicalTaxaToDisplay TaxaToDisplay { get; set; }

        /// <summary>
        /// Determines how many levels from the hierarchy to include.
        /// </summary>
        /// <value>The levels.</value>
        public int Levels { get; set; }

        /// <summary>
        /// Gets or sets the root category which children will be displayed as a top level in the widget.
        /// Used only if this display mode is selected.
        /// </summary>
        /// <value>The parent category.</value>
        public Guid RootCategory { get; set; }
    }
}
