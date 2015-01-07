using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Telerik.Sitefinity.Frontend.Search.Mvc.Models
{
    /// <summary>
    /// An interface that provides all common properties and methods for SearchResults model.
    /// </summary>
    public interface ISearchResultsModel
    {
        void PopulateResults(string searchQuery, int? page);
    }
}
