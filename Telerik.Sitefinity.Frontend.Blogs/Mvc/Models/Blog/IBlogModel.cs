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
