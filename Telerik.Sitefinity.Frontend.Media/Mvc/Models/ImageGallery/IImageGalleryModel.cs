using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Telerik.Sitefinity.ContentLocations;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Frontend.Mvc.Models;
using Telerik.Sitefinity.Libraries.Model;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Taxonomies.Model;

namespace Telerik.Sitefinity.Frontend.Media.Mvc.Models.ImageGallery
{
    /// <summary>
    /// Classes that implement this interface provide business logic for getting Image items.
    /// </summary>
    public interface IImageGalleryModel
    {
        /// <summary>
        /// Gets or sets the CSS class that will be applied on the wrapping element of the widget when it is in List view.
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
        /// Gets the list of items to be displayed inside the widget when option "Selected items" is enabled.
        /// </summary>
        /// <value>
        /// The selected item ids.
        /// </value>
        string SerializedSelectedItemsIds { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to enable social sharing.
        /// </summary>
        /// <value>
        ///   <c>true</c> if should enable social sharing; otherwise, <c>false</c>.
        /// </value>
        bool EnableSocialSharing { get; set; }

        /// <summary>
        /// Gets or sets the name of the provider.
        /// </summary>
        /// <value>The name of the provider.</value>
        string ProviderName { get; set; }

        /// <summary>
        /// Gets or sets which items to be displayed in the list view.
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
        /// Gets or sets the additional filter expression.
        /// </summary>
        /// <value>
        /// The filter expression.
        /// </value>
        string FilterExpression { get; set; }

        /// <summary>
        /// Gets or sets the serialized additional filters.
        /// </summary>
        /// <value>
        /// The serialized additional filters.
        /// </value>
        string SerializedAdditionalFilters { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the canonical URL tag should be added to the page when the canonical meta tag should be added to the page.
        /// If the value is not set, the settings from SystemConfig -> ContentLocationsSettings -> DisableCanonicalURLs will be used. 
        /// </summary>
        /// <value>The disable canonical URLs.</value>
        bool? DisableCanonicalUrlMetaTag { get; set; }

        /// <summary>
        /// Gets or sets the parent filtering mode.
        /// </summary>
        /// <value>
        /// The parent filtering mode.
        /// </value>
        ParentFilterMode ParentFilterMode { get; set; }

        /// <summary>
        /// Gets or sets the serialized selected parent ids.
        /// </summary>
        /// <value>
        /// The serialized selected parents ids.
        /// </value>
        string SerializedSelectedParentsIds { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to include items from child libraries of the selected libraries.
        /// </summary>
        /// <value>
        /// <c>true</c> if items of child libraries should be included; otherwise, <c>false</c>.
        /// </value>
        bool IncludeChildLibraries { get; set; }

        /// <summary>
        /// Gets the information for all of the content types that a control is able to show.
        /// </summary>
        [SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
        IEnumerable<IContentLocationInfo> GetLocations();

        /// <summary>
        /// Creates a view model for use in list views.
        /// </summary>
        /// <param name="taxonFilter">The taxon filter.</param>
        /// <param name="page">The page.</param>
        /// <returns>A view model for use in list views.</returns>
        /// <exception cref="System.ArgumentException">'page' argument has to be at least 1.;page</exception>
        ContentListViewModel CreateListViewModel(ITaxon taxonFilter, int page);

        /// <summary>
        /// Creates the details view model.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>A view model for use in detail views.</returns>
        ContentDetailsViewModel CreateDetailsViewModel(IDataItem item);

        /// <summary>
        /// Creates the ListView model by parent.
        /// </summary>
        /// <param name="parentItem">The parent item.</param>
        /// <param name="p">The page.</param>
        /// <returns>A list view model containing all descendant items from the given parent.</returns>
        ContentListViewModel CreateListViewModelByParent(IFolder parentItem, int page);

        /// <summary>
        /// Gets a collection of <see cref="CacheDependencyNotifiedObject"/>.
        ///     The <see cref="CacheDependencyNotifiedObject"/> represents a key for which cached items could be subscribed for
        ///     notification.
        ///     When notified, all cached objects with dependency on the provided keys will expire.
        /// </summary>
        /// <param name="viewModel">View model that will be used for displaying the data.</param>
        /// <returns>
        /// The <see cref="IList"/>.
        /// </returns>
        IList<CacheDependencyKey> GetKeysOfDependentObjects(ContentListViewModel viewModel);

        /// <summary>
        /// Gets a collection of <see cref="CacheDependencyNotifiedObject"/>.
        ///     The <see cref="CacheDependencyNotifiedObject"/> represents a key for which cached items could be subscribed for
        ///     notification.
        ///     When notified, all cached objects with dependency on the provided keys will expire.
        /// </summary>
        /// <param name="viewModel">View model that will be used for displaying the data.</param>
        /// <returns>
        /// The <see cref="IList"/>.
        /// </returns>
        IList<CacheDependencyKey> GetKeysOfDependentObjects(ContentDetailsViewModel viewModel);
    }
}
