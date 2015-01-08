using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telerik.Sitefinity.Services.Search.Data;

namespace Telerik.Sitefinity.Frontend.Search.Mvc.Models
{
    /// <inheritdoc />
    public class SearchResultsModel : ISearchResultsModel
    {
        /// <inheritdoc />
        public IList<IDocument> Results { get; set; }

        /// <inheritdoc />
        public int? ItemsPerPage { get; set; }

        /// <inheritdoc />
        public ListDisplayMode DisplayMode { get; set; }

        /// <inheritdoc />
        public bool AllowSorting { get; set; }

        /// <inheritdoc />
        public OrderByOptions OrderBy { get; set; }

        /// <inheritdoc />
        public string CssClass { get; set; }

        /// <inheritdoc />
        public void PopulateResults(string searchQuery, int? page)
        {
        }
    }
}
