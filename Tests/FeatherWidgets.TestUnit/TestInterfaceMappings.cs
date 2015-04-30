using FeatherWidgets.TestUnit.DummyClasses.ContentBlock;
using FeatherWidgets.TestUnit.DummyClasses.Identity;
using FeatherWidgets.TestUnit.DummyClasses.Media;
using FeatherWidgets.TestUnit.DummyClasses.Navigation;
using FeatherWidgets.TestUnit.DummyClasses.SearchResults;
using Ninject.Modules;
using Telerik.Sitefinity.Frontend.ContentBlock.Mvc.Models;
using Telerik.Sitefinity.Frontend.Identity.Mvc.Models.AccountActivation;
using Telerik.Sitefinity.Frontend.Identity.Mvc.Models.UsersList;
using Telerik.Sitefinity.Frontend.Lists.Mvc.Models;
using Telerik.Sitefinity.Frontend.Media.Mvc.Models.DocumentsList;
using Telerik.Sitefinity.Frontend.Navigation.Mvc.Models;
using Telerik.Sitefinity.Frontend.Search.Mvc.Models;

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
            Bind<ISearchResultsModel>().To<DummySearchResultsModel>().When(request => true);
            Bind<IAccountActivationModel>().To<DummyAccountActivationModel>().When(request => true);
            Bind<IDocumentsListModel>().To<DummyDocumentsListModel>().When(request => true);
            Bind<IListsModel>().To<DummyListsModel>().When(request => true);
            Bind<IUsersListModel>().To<DummyUsersListModel>().When(request => true);
        }
    }
}
