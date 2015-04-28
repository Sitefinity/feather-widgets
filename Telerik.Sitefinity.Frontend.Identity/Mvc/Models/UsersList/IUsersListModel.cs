using System;
using System.Collections.Generic;
using System.Linq;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Frontend.Mvc.Models;
using Telerik.Sitefinity.Model;

namespace Telerik.Sitefinity.Frontend.Identity.Mvc.Models.UsersList
{
    /// <summary>
    /// Classes that implement this interface could be used as model for the Users list widget.
    /// </summary>
    public interface IUsersListModel
    {
        /// <summary>
        /// Gets the list of users to be displayed inside the widget when option "Selected users" is enabled.
        /// </summary>
        /// <value>
        /// The selected users.
        /// </value>
        string SerializedSelectedItemsIds { get; set; }

        /// <summary>
        /// Gets or sets the CSS class that will be applied on the wrapper div of the widget when it is in List view.
        /// </summary>
        /// <value>
        /// The CSS class.
        /// </value>
        string ListCssClass { get; set; }

        /// <summary>
        /// Gets or sets the CSS class that will be applied on the wrapper div of the widget when it is in Details view.
        /// </summary>
        /// <value>
        /// The CSS class.
        /// </value>
        string DetailCssClass { get; set; }

        /// <summary>
        /// Gets or sets the name of the provider.
        /// </summary>
        /// <value>The name of the provider.</value>
        string ProviderName { get; set; }

        /// <summary>
        /// Gets or sets which users to be displayed in the list view.
        /// </summary>
        /// <value>The page display mode.</value>
        SelectionMode SelectionMode { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to divide items in the list.
        /// </summary>
        /// <value>
        /// The display mode.
        /// </value>
        ListDisplayMode DisplayMode { get; set; }

         /// <summary>
        /// Gets or sets the items count per page.
        /// </summary>
        /// <value>
        /// The items per page.
        /// </value>
        int? ItemsPerPage { get; set; }

        /// <summary>
        /// Gets or sets the sort expression.
        /// </summary>
        /// <value>
        /// The sort expression.
        /// </value>
        string SortExpression { get; set; }

        /// <summary>
        /// Gets or sets the serialized additional filters.
        /// </summary>
        /// <value>
        /// The serialized additional filters.
        /// </value>
        string SerializedAdditionalFilters { get; set; }

        /// <summary>
        /// Gets or sets the additional filter expression.
        /// </summary>
        /// <value>
        /// The filter expression.
        /// </value>
        string FilterExpression { get; set; }

        /// <summary>
        /// Creates a view model for use in list views.
        /// </summary>
        /// <param name="page">The page.</param>
        /// <returns>A view model for use in list views.</returns>
        /// <exception cref="System.ArgumentException">'page' argument has to be at least 1.;page</exception>
        UsersListViewModel CreateListViewModel(int page);

        /// <summary>
        /// Creates the details view model.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>A view model for use in detail views.</returns>
        UserDetailsViewModel CreateDetailsViewModel(IDataItem item);

        /// <summary>
        /// Gets a collection of <see cref="CacheDependencyNotifiedObject"/>.
        ///     The <see cref="CacheDependencyNotifiedObject"/> represents a key for which cached items could be subscribed for
        ///     notification.
        ///     When notified, all cached objects with dependency on the provided keys will expire.
        /// </summary>
        /// <param name="viewModel">View model that will be used for displaying the data.</param>
        /// <returns>
        /// The <see cref="IEnumerable"/>.
        /// </returns>
        IEnumerable<CacheDependencyKey> GetKeysOfDependentObjects(UsersListViewModel viewModel);

        /// <summary>
        /// Gets a collection of <see cref="CacheDependencyNotifiedObject"/>.
        ///     The <see cref="CacheDependencyNotifiedObject"/> represents a key for which cached items could be subscribed for
        ///     notification.
        ///     When notified, all cached objects with dependency on the provided keys will expire.
        /// </summary>
        /// <param name="viewModel">View model that will be used for displaying the data.</param>
        /// <returns>
        /// The <see cref="IEnumerable"/>.
        /// </returns>
        IEnumerable<CacheDependencyKey> GetKeysOfDependentObjects(UserDetailsViewModel viewModel);
    }
}
