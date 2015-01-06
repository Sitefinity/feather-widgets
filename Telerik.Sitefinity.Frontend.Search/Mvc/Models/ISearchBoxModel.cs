using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Telerik.Sitefinity.Frontend.Search.Mvc.Models
{
    public interface ISearchBoxModel
    {
        /// <summary>
        /// Gets or sets the word mode for indexing service to search within.
        /// </summary>
        WordsMode WordsMode { get; set; }

        /// <summary>
        /// Gets or sets the URL of the page where the results will be displayed.
        /// </summary>
        string ResultsUrl { get; set; }

        /// <summary>
        /// Gets or sets the name of the index catalogue which will be used for searching.
        /// </summary>
        string IndexCatalogue { get; set; }

        /// <summary>
        /// Gets or sets the suggestion fields that will be used by the search box's autcomplete.
        /// </summary>
        string SuggestionFields { get; set; }

        /// <summary>
        /// Gets or sets the service end point used to get new suggestions.
        /// </summary>
        string SuggestionsRoute { get; set; }

        /// <summary>
        /// Gets or sets whether the search box's autocomplete will display suggestions.
        /// </summary>
        bool DisableSuggestions { get; set; }

        /// <summary>
        /// Gets or sets after which typed symbol the suggestions will be shown.
        /// </summary>
        int MinSuggestionLength { get; set; }

        /// <summary>
        /// Gets or sets the current UI language.
        /// </summary>
        /// <value>The language.</value>
        string Language { get; set; }

    }
}
