using System;
using System.Linq;

namespace Telerik.Sitefinity.Frontend.Taxonomies.Mvc.Models.HierarchicalTaxonomy
{
    /// <summary>
    /// The public interface of the hierarchical taxonomy widget't model.
    /// </summary>
    public interface IHierarchicalTaxonomyModel : ITaxonomyModel
    {
        /// <summary>
        /// Determines what taxa will be displayed by the widget/
        /// </summary>
        /// <value>The taxa to display.</value>
        HierarchicalTaxaToDisplay TaxaToDisplay { get; set; }

        /// <summary>
        /// Determines how many levels from the hierarchy to include.
        /// </summary>
        /// <value>The levels.</value>
        int Levels { get; set; }

        /// <summary>
        /// Gets or sets the root category which children will be displayed as a top level in the widget.
        /// Used only if this display mode is selected.
        /// </summary>
        /// <value>The parent category.</value>
        Guid RootCategory { get; set; }
    }
}
