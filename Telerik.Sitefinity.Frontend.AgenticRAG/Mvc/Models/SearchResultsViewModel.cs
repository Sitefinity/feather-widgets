using System.Collections.Generic;

namespace Telerik.Sitefinity.Frontend.AgenticRAG.Mvc.Models
{
    public class SearchResultsViewModel
    {
        public List<SearchResultModel> SearchResults { get; set; }

        public string CssClass { get; set; }

        public string ResultsHeader { get; set; }

        public string ResultsNumberLabel { get; set; }

        public int PageSize { get; set; }
    }

    public class SearchResultModel
    {
        public string Title { get; set; }

        public string Link { get; set; }

        internal int Order { get; set; }
    }
}
