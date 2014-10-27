using System;
using System.Collections.Generic;
using System.ComponentModel;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Frontend.Mvc.Models;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Taxonomies.Model;

namespace DynamicContent.Mvc.Model
{
    public interface IDynamicContentModel : ICacheDependable
    {
        /// <summary>
        /// Gets or sets the type of the content.
        /// </summary>
        /// <value>
        /// The type of the content.
        /// </value>
        string ContentType { get; set; }

        /// <summary>
        /// Gets the list of items to be displayed inside the widget.
        /// </summary>
        /// <value>
        /// The items collection.
        /// </value>
        [Browsable(false)]
        IList<IDataItem> Items { get; }

        /// <summary>
        /// Gets or sets the id of the selected item.
        /// </summary>
        /// <value>
        /// The selected items.
        /// </value>
        Guid SelectedItemId { get; set; }

        /// <summary>
        /// Gets or sets the CSS class that will be applied on the wrapper div of the widget when it is in List view.
        /// </summary>
        /// <value>
        /// The CSS class.
        /// </value>
        string ListCssClass { get; set; }

        /// <summary>
        /// Gets or sets the detail item.
        /// </summary>
        /// <value>
        /// The detail item.
        /// </value>
        [Browsable(false)]
        dynamic DetailItem { get; set; }

        /// <summary>
        /// Gets or sets the CSS class that will be applied on the wrapper div of the widget when it is in Details view.
        /// </summary>
        /// <value>
        /// The CSS class.
        /// </value>
        string DetailCssClass { get; set; }

        /// <summary>
        /// Gets or sets which items to be displayed in the list view.
        /// </summary>
        /// <value>The page display mode.</value>
        SelectionMode SelectionMode { get; set; }

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
        /// Gets or sets the total pages count.
        /// </summary>
        /// <value>
        /// The total pages count.
        /// </value>
        [Browsable(false)]
        int? TotalPagesCount { get; set; }

        /// <summary>
        /// Gets or sets the current page.
        /// </summary>
        /// <value>
        /// The current page.
        /// </value>
        [Browsable(false)]
        int CurrentPage { get; set; }

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
        /// Populates the items.
        /// </summary>
        /// <param name="taxonFilter">The taxon that should be contained in the items.</param>
        /// <param name="taxonField">The taxon field.</param>
        /// <param name="page">The page.</param>
        void PopulateItems(ITaxon taxonFilter, string taxonField, int? page);

        /// <summary>
        /// Compiles a filter expression based on the widget settings.
        /// </summary>
        /// <returns>Filter expression that will be applied on the query.</returns>
        string CompileFilterExpression();
    }
}
