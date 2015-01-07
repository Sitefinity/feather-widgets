using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Telerik.Sitefinity.Frontend.Search.Mvc.Models
{
    public class SearchBoxModel : ISearchBoxModel
    {
        #region Construction
        /// <summary>
        /// Initializes a new instance of the <see cref="SearchBoxModel" /> class.
        /// </summary>
        /// <param name="suggestionsRoute">The suggestions service end point.</param>
        /// <param name="minSuggestionLength">The minimal suggestion length.</param>
        /// <param name="suggestionFields">The suggestion fields.</param>
        public SearchBoxModel(string suggestionsRoute, int minSuggestionLength, string suggestionFields, string cssClass)
        {
            this.SuggestionFields = suggestionFields;
            this.SuggestionsRoute = suggestionsRoute;
            this.MinSuggestionLength = minSuggestionLength;
            this.CssClass = cssClass;
        }

        #endregion

        #region Properties
        /// <inheritdoc />
        public WordsMode WordsMode { get; set; }

        /// <inheritdoc />
        public string ResultsUrl { get; set; }

        /// <inheritdoc />
        public string IndexCatalogue { get; set; }

        /// <inheritdoc />
        public string SuggestionFields { get; set; }

        /// <inheritdoc />
        public string SuggestionsRoute { get; set; }

        /// <inheritdoc />
        public bool DisableSuggestions { get; set; }

        /// <inheritdoc />
        public int MinSuggestionLength { get; set; }

        /// <inheritdoc />
        public string Language { get; set; }

        /// <inheritdoc />
        public string CssClass { get; set; }        
        #endregion
    }
}
