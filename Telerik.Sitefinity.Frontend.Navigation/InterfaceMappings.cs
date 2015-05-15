using Ninject.Modules;
using Telerik.Sitefinity.Frontend.Navigation.Mvc.Models;
using Telerik.Sitefinity.Frontend.Navigation.Mvc.Models.Breadcrumb;
using Telerik.Sitefinity.Frontend.Navigation.Mvc.Models.LanguageSelector;
using Telerik.Sitefinity.Frontend.Navigation.Mvc.Models.SiteSelector;

namespace Telerik.Sitefinity.Frontend.Navigation
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
            Bind<INavigationModel>().To<NavigationModel>();
            Bind<IBreadcrumbModel>().To<BreadcrumbModel>();
            Bind<ILanguageSelectorModel>().To<LanguageSelectorModel>();
            Bind<ISiteSelectorModel>().To<SiteSelectorModel>();
        }
    }
}
