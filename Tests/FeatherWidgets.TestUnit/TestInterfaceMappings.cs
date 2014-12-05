using FeatherWidgets.TestUnit.DummyClasses.ContentBlock;
using FeatherWidgets.TestUnit.DummyClasses.Navigation;
using Navigation.Mvc.Models;
using Ninject.Modules;
using Telerik.Sitefinity.Frontend.ContentBlock.Mvc.Models;

namespace FeatherWidgets.TestUnit
{
    /// <summary>
    /// TestInterface Mappings
    /// </summary>
    public class TestInterfaceMappings : NinjectModule
    {
        /// <summary>
        /// Loads the module into the kernel.
        /// </summary>
        public override void Load()
        {
            Bind<IContentBlockModel>().To<DummyContentBlockModel>().When(request => true);
            Bind<INavigationModel>().To<DummyNavigationModel>().When(request => true);
        }
    }
}
