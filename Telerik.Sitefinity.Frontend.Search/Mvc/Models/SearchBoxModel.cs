using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Telerik.Sitefinity.Frontend.Search.Mvc.Models
{
    public class SearchBoxModel : ISearchBoxModel
    {

        public WordsMode WordsMode
        {
            get;
            set;
        }

        public string ResultsUrl
        {
            get;
            set;
        }

        public string IndexCatalogue
        {
            get;
            set;
        }

        public string SuggestionFields
        {
            get;
            set;
        }

        public string SuggestionsRoute
        {
            get;
            set;
        }

        public bool DisableSuggestions
        {
            get;
            set;
        }

        public int MinSuggestionLength
        {
            get;
            set;
        }

        public string Language
        {
            get;
            set;
        }
    }
}
