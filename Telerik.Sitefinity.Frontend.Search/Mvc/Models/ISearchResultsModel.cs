using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using Telerik.Sitefinity.Services.Search.Data;
using Telerik.Sitefinity.Utilities.TypeConverters;

namespace Telerik.Sitefinity.Frontend.Search.Mvc.Models
{
    /// <summary>
    /// An interface that provides all common properties and methods for SearchResults model.
    /// </summary>
    public interface ISearchResultsModel
    {
        /// <summary>
        /// Gets or sets the search results.
        /// </summary>
        /// <value>The results.</value>
        ResultModel Results { get; set; }

        /// <summary>
        /// Gets or sets the items count per page.
        /// </summary>
        /// <value>
        /// The items per page.
        /// </value>
        int? ItemsPerPage { get; set; }

        /// <summary>
        /// Gets or sets the result text.
        /// </summary>
        /// <value>
        /// The result text.
        /// </value>
        [Browsable(false)]
        string ResultText { get; set; }

        /// <summary>
        /// Gets or sets a value indicating how to display items in the list.
        /// </summary>
        /// <value>
        /// The display mode.
        /// </value>
        ListDisplayMode DisplayMode { get; set; }

        /// <summary>
        /// Determines whether the user will be able to sort the search results.
        /// </summary>
        /// <value>The allow sorting.</value>
        bool AllowSorting { get; set; }

        /// <summary>
        /// Determines the sorting order.
        /// </summary>
        /// <value>The order by.</value>
        OrderByOptions OrderBy { get; set; }

        /// <summary>
        /// Gets or sets the CSS class that will be applied on the wrapper div of the Search results widget.
        /// </summary>
        string CssClass { get; set; }

        /// <summary>
        /// Gets or sets the search fields.
        /// </summary>
        /// <value>
        /// The search fields.
        /// </value>
        [TypeConverter(typeof(StringArrayConverter))]
        string[] SearchFields { get; set; }

        /// <summary>
        /// Gets or sets the highlighted fields.
        /// </summary>
        /// <value>
        /// The highlighted fields.
        /// </value>
        [TypeConverter(typeof(StringArrayConverter))]
        string[] HighlightedFields { get; set; }

        /// <summary>
        /// Gets or sets the index catalogue.
        /// </summary>
        /// <value>
        /// The index catalogue.
        /// </value>
        string IndexCatalogue { get; set; }

        /// <summary>
        /// Gets or sets the languages that will be used to filter the search.
        /// </summary>
        /// <value>The languages.</value>
        CultureInfo[] Languages { get; set; }

        /// <summary>
        /// Performs search by given query and populates the results collection.
        /// </summary>
        /// <param name="searchQuery">The search query.</param>
        /// <param name="skip">The skip.</param>      
        /// <param name="language">The language of the displayed results.</param>
        void PopulateResults(string searchQuery, int? skip,  string language);
    }
}
