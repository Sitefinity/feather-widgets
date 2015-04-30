using System;
using System.Linq;
using Telerik.Sitefinity.Frontend.Identity.Mvc.Models.UsersList;

namespace FeatherWidgets.TestUnit.DummyClasses.Identity
{
    public class DummyUsersListModel : IUsersListModel
    {
        public string SerializedSelectedItemsIds { get; set; }

        public string ListCssClass { get; set; }

        public string DetailCssClass { get; set; }

        public string ProviderName { get; set; }

        public Telerik.Sitefinity.Frontend.Mvc.Models.SelectionMode SelectionMode { get; set; }

        public Telerik.Sitefinity.Frontend.Mvc.Models.ListDisplayMode DisplayMode { get; set; }

        public int? ItemsPerPage { get; set; }

        public string SortExpression { get; set; }

        public string SerializedAdditionalFilters { get; set; }

        public string FilterExpression { get; set; }

        public string ProfileTypeFullName { get; set; }

        public UsersListViewModel CreateListViewModel(int page)
        {
            return new UsersListViewModel();
        }

        public UserDetailsViewModel CreateDetailsViewModel(Telerik.Sitefinity.Model.IDataItem item)
        {
            return new UserDetailsViewModel();
        }

        public System.Collections.Generic.IEnumerable<Telerik.Sitefinity.Data.CacheDependencyKey> GetKeysOfDependentObjects(UsersListViewModel viewModel)
        {
            return null;
        }

        public System.Collections.Generic.IEnumerable<Telerik.Sitefinity.Data.CacheDependencyKey> GetKeysOfDependentObjects(UserDetailsViewModel viewModel)
        {
            return null;
        }
    }
}
