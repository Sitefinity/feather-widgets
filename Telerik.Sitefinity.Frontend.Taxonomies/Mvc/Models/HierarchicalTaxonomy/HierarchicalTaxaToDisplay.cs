using System;
using System.Linq;

namespace Telerik.Sitefinity.Frontend.Taxonomies.Mvc.Models.HierarchicalTaxonomy
{
    /// <summary>
    /// Determines what taxa to display.
    /// </summary>
    public enum HierarchicalTaxaToDisplay
    {
        All,
        TopLevel,
        UnderParticularTaxon,
        Selected,
        UsedByContentType
    }
}
