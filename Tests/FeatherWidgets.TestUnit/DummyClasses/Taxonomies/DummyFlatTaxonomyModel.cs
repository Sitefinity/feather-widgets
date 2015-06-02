using System;
using System.Collections.Generic;
using System.Linq;
using Telerik.Sitefinity.Frontend.Taxonomies.Mvc.Models;
using Telerik.Sitefinity.Frontend.Taxonomies.Mvc.Models.FlatTaxonomy;

namespace FeatherWidgets.TestUnit.DummyClasses.Taxonomies
{
    /// <summary>
    /// This class creates dummy <see cref="Telerik.Sitefinity.Frontend.Taxonomies.Mvc.Model.FlatTaxonomy.FlatTaxonomyModel"/>
    /// </summary>
    public class DummyFlatTaxonomyModel : FlatTaxonomyModel
    {
        protected override IList<TaxonViewModel> GetAllTaxa<T>()
        {
            return new List<TaxonViewModel>();
        }
    }
}
