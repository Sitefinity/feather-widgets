using System;
using System.Linq;

namespace Telerik.Sitefinity.Frontend.Taxonomies.Mvc.Models.FlatTaxonomy
{
    public class FlatTaxonomyModel : TaxonomyModel, IFlatTaxonomyModel
    {
        /// <summary>
        /// Determines what taxa will be displayed by the widget.
        /// </summary>
        /// <value>The taxa to display.</value>
        public FlatTaxaToDisplay TaxaToDisplay { get; set; }
    }
}
