using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Telerik.Sitefinity.Frontend.Taxonomies.Mvc.Models.HierarchicalTaxonomy
{
    /// <summary>
    /// Determines what taxa to display.
    /// </summary>
    public enum HierarchicalTaxaToDisplay
    {
        TopLevel,
        UnderParticularTaxon,
        Selected,
        UsedByContentType
    }
}
