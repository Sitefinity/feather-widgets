using System;
using System.Linq;

namespace Telerik.Sitefinity.Frontend.Taxonomies.Mvc.Models.FlatTaxonomy
{
    /// <summary>
    /// The public interface of the flat taxonomy model.
    /// </summary>
    public interface IFlatTaxonomyModel : ITaxonomyModel
    {
        /// <summary>
        /// Determines what taxa will be displayed by the widget.
        /// </summary>
        /// <value>The taxa to display.</value>
        FlatTaxaToDisplay TaxaToDisplay { get; set; }
    }
}
