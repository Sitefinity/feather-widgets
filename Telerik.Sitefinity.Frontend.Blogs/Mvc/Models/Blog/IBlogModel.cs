using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Frontend.Mvc.Models;
using Telerik.Sitefinity.Model;

namespace Telerik.Sitefinity.Frontend.Blogs.Mvc.Models.Blog
{
    /// <summary>
    /// This interface defines API for working with <see cref="Telerik.Sitefinity.Blogs.Model.Blog"/> items.
    /// </summary>
    public interface IBlogModel
    {
        /// <summary>
        /// Gets the list of blog to be displayed inside the widget when option "Selected blogs" is enabled.
        /// </summary>
        /// <value>
        /// The selected blog items.
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
        /// Specifies the least amount of posts needed to show a blog in the list (when selection mode is Filtered)
        /// </summary>
        int MinPostsCount { get; set; }

        /// <summary>
        /// Specifies the minimum age of the posts in months (when selection mode is Filtered)
        /// </summary>
        int MaxPostsAge { get; set; }

        /// <summary>
        /// Specifies when <see cref="SelectionMode"/> is Filtered, By which value is it filtered
        /// </summary>
        FilteredSelectionMode FilteredSelectionMode { get; set; }

        /// <summary>
        /// Gets or sets the items count per page.
        /// </summary>
        /// <value>
        /// The items per page.
        /// </value>
        int? ItemsPerPage { get; set; }

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
        /// Gets or sets the sort expression.
        /// </summary>
        /// <value>
        /// The sort expression.
        /// </value>
        string SortExpression { get; set; }

        /// <summary>
        /// Gets or sets which blog posts to be displayed in the list view.
        /// </summary>
        /// <value>The page display mode.</value>
        SelectionMode SelectionMode { get; set; }

        /// <summary>
        /// Creates a view model for use in list views.
        /// </summary>
        /// <param name="page">The page.</param>
        /// <returns>A view model for use in list views.</returns>
        /// <exception cref="System.ArgumentException">'page' argument has to be at least 1.;page</exception>
        ContentListViewModel CreateListViewModel(int page);

        /// <summary>
        /// Creates the details view model.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>A view model for use in detail views.</returns>
        ContentDetailsViewModel CreateDetailsViewModel(IDataItem item);
    }
}
