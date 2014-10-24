using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.News.Model;
using Telerik.Sitefinity.Taxonomies.Model;

namespace News.Mvc.Models
{
    /// <summary>
    /// Classes that implement this interface could be used as model for the News widget.
    /// </summary>
    public interface INewsModel : ICacheDependable
    {
        /// <summary>
        /// Gets the list of items to be displayed inside the widget.
        /// </summary>
        /// <value>
        /// The items collection.
        /// </value>
        [Browsable(false)]
        IList<NewsItem> Items { get; }

        /// <summary>
        /// Gets the list of news to be displayed inside the widget when option "Selected news" is enabled.
        /// </summary>
        /// <value>
        /// The selected news items.
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
        /// Gets or sets the detail item.
        /// </summary>
        /// <value>
        /// The detail news.
        /// </value>
        [Browsable(false)]
        NewsItem DetailItem { get; set; }

        /// <summary>
        /// Gets or sets the CSS class that will be applied on the wrapper div of the widget when it is in Details view.
        /// </summary>
        /// <value>
        /// The CSS class.
        /// </value>
        string DetailCssClass { get; set; }

        /// <summary>
        /// Gets or sets which news to be displayed in the list view.
        /// </summary>
        /// <value>The page display mode.</value>
        NewsSelectionMode SelectionMode { get; set; }

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
        /// Gets or sets the taxonomy filter.
        /// </summary>
        /// <value>
        /// The taxonomy filter.
        /// </value>
        string SerializedTaxonomyFilter { get; set; }

        /// <summary>
        /// Gets or sets the serialized selected taxonomies.
        /// </summary>
        /// <value>
        /// The serialized selected taxonomies.
        /// </value>
        string SerializedSelectedTaxonomies { get; set; }

        /// <summary>
        /// Gets or sets the additional filter expression.
        /// </summary>
        /// <value>
        /// The filter expression.
        /// </value>
        string FilterExpression { get; set; }

        /// <summary>
        /// Populates the news.
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
