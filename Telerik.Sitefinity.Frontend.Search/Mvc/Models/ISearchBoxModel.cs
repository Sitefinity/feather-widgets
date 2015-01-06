using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Telerik.Sitefinity.Frontend.Search.Mvc.Models
{
    public interface ISearchBoxModel
    {
        WordsMode WordsMode { get; set; }

        string ResultsUrl { get; set; }

        string IndexCatalogue { get; set; }

        string SuggestionFields { get; set; }

        string SuggestionsRoute { get; set; }

        bool DisableSuggestions { get; set; }

        int MinSuggestionLength { get; set; }

        string Language { get; set; }

    }
}
