﻿using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Telerik.Sitefinity.ContentLocations;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Frontend.Mvc.Models;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Taxonomies.Model;

namespace Telerik.Sitefinity.Frontend.Events.Mvc.Models
{
    /// <summary>
    /// This interface defines API for working with <see cref="Telerik.Sitefinity.Events.Model.Event"/> items.
    /// </summary>
    public interface IEventModel
    {
        /// <summary>
        /// Gets the list of events to be displayed inside the widget when option "Selected events" is enabled.
        /// </summary>
        /// <value>
        /// The selected event items.
        /// </value>
        string SerializedSelectedItemsIds { get; set; }

        /// <summary>
        /// Gets or sets the name of the provider.
        /// </summary>
        /// <value>The name of the provider.</value>
        string ProviderName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to divide items in the list.
        /// </summary>
        /// <value>
        /// The display mode.
        /// </value>
        ListDisplayMode DisplayMode { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether social sharing is enabled.
        /// </summary>
        /// <value>
        ///   <c>true</c> if [enable social sharing]; otherwise, <c>false</c>.
        /// </value>
        bool EnableSocialSharing { get; set; }

        /// <summary>
        /// Gets or sets the items count per page or items total dependeing on the selected <see cref="ListDisplayMode"/>
        /// </summary>
        /// <value>
        /// The items per page.
        /// </value>
        int? ItemsPerPage { get; set; }

        /// <summary>
        /// Gets or sets the items limit count.
        /// </summary>
        /// <value>
        /// The items limit.
        /// </value>
        int? LimitCount { get; set; }

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
        /// Gets or sets the serialized narrow selection filters. Contains information about taxonomies and calendar filters as serialized QueryData.
        /// </summary>
        /// <value>
        /// The serialized narrow selection filters.
        /// </value>
        string SerializedNarrowSelectionFilters { get; set; }

        /// <summary>
        /// Gets or sets which events to be displayed in the list view.
        /// </summary>
        /// <value>The page display mode.</value>
        /// <remarks>
        /// All = all events, Filtered = events by date, Selected = Selected events
        /// </remarks>
        SelectionMode SelectionMode { get; set; }

        /// <summary>
        /// Gets or sets the selected mode for Narrow selection. That is the filtering by taxonomies and calendars.
        /// </summary>
        /// <value>
        /// The narrow selection mode.
        /// </value>
        SelectionMode NarrowSelectionMode { get; set; }

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
        /// Gets or sets the URL key prefix.
        /// </summary>
        /// <value>The URL key prefix.</value>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1056:UriPropertiesShouldNotBeStrings")]
        string UrlKeyPrefix { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the widget allows calendar export.
        /// </summary>
        /// <value>
        ///   <c>true</c> if [allow calendar export]; otherwise, <c>false</c>.
        /// </value>
        bool AllowCalendarExport { get; set; }

        /// <summary>
        /// Creates a view model for use in list views.
        /// </summary>
        /// <param name="taxonFilter">The taxon filter.</param>
        /// <param name="page">The page.</param>
        /// <returns>
        /// A view model for use in list views.
        /// </returns>
        /// <exception cref="System.ArgumentException">'page' argument has to be at least 1.;page</exception>
        ContentListViewModel CreateListViewModel(ITaxon taxonFilter, int page);

        /// <summary>
        /// Creates the details view model.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>A view model for use in detail views.</returns>
        ContentDetailsViewModel CreateDetailsViewModel(IDataItem item);

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

        /// <summary>
        /// Gets the information for all of the content types that a control is able to show.
        /// </summary>
        [SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
        IEnumerable<IContentLocationInfo> GetLocations();
    }
}
