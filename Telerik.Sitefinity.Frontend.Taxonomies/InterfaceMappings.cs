using Ninject.Modules;
using Telerik.Sitefinity.Frontend.Taxonomies.Mvc.Models;
using Telerik.Sitefinity.Frontend.Taxonomies.Mvc.Models.FlatTaxonomy;
using Telerik.Sitefinity.Frontend.Taxonomies.Mvc.Models.HierarchicalTaxonomy;

namespace Telerik.Sitefinity.Frontend.Taxonomies
{
    /// <summary>
    /// This class is used to describe the bindings which will be used by the Ninject container when resolving classes
    /// </summary>
    public class InterfaceMappings : NinjectModule
    {
        /// <summary>
        /// Loads the module into the kernel.
        /// </summary>
        public override void Load()
        {
            Bind<ITaxonomyModel>().To<TaxonomyModel>();
            Bind<IHierarchicalTaxonomyModel>().To<HierarchicalTaxonomyModel>();
            Bind<IFlatTaxonomyModel>().To<FlatTaxonomyModel>();
        }
    }
}
