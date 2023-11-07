using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Frontend.Search.Mvc.StringResources;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Search;
using Telerik.Sitefinity.Search.Impl.Facets;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Services.Search;
using Telerik.Sitefinity.Services.Search.Configuration;
using Telerik.Sitefinity.Services.Search.Data;
using Telerik.Sitefinity.Services.Search.Model;
using Telerik.Sitefinity.Utilities.TypeConverters;

namespace Telerik.Sitefinity.Frontend.Search.Mvc.Models
{
    /// <inheritdoc />
    public class SearchResultsModel : ISearchResultsModel
    {
        #region Construction

        /// <summary>
        /// Initializes a new instance of the <see cref="SearchResultsModel"/> class.
        /// </summary>
        /// <param name="languages">The languages.</param>
        public SearchResultsModel(CultureInfo[] languages)
        {
            this.Languages = languages;
            this.Results = new ResultModel();
            this.searchProcessor = new SearchFacetsQueryStringProcessor();
        }

        #endregion

        #region Properties

        /// <inheritdoc />
        public ResultModel Results { get; set; }

        /// <inheritdoc />
        public string ResultText { get; set; }

        /// <inheritdoc />
        public int? ItemsPerPage
        {
            get
            {
                return this.itemsPerPage;
            }

            set
            {
                this.itemsPerPage = value;
            }
        }

        /// <inheritdoc />
        public int? LimitCount
        {
            get
            {
                return this.limitCount;
            }

            set
            {
                this.limitCount = value;
            }
        }

        /// <inheritdoc />
        public int CurrentPage { get; set; }

        /// <inheritdoc />
        public int? TotalPagesCount { get; set; }

        /// <inheritdoc />
        public ListDisplayMode DisplayMode { get; set; }

        /// <inheritdoc />
        public bool AllowSorting { get; set; }

        /// <inheritdoc />
        public OrderByOptions OrderBy { get; set; }

        /// <inheritdoc />
        public string CssClass { get; set; }

        /// <inheritdoc />
        public string IndexCatalogue { get; set; }

        /// <inheritdoc />
        public bool ShowLinksOnlyFromCurrentSite { get; set; }

        /// <inheritdoc />
        [TypeConverter(typeof(StringArrayConverter))]
        public string[] SearchFields
        {
            get
            {
                return this.searchFields;
            }
            set
            {
                this.searchFields = value;
            }
        }

        /// <inheritdoc />
        [TypeConverter(typeof(StringArrayConverter))]
        public string[] HighlightedFields
        {
            get
            {
                return this.highlightedFields;
            }
            set
            {
                this.highlightedFields = value;
            }
        }

        /// <inheritdoc />
        public bool EscapeSpecialChars { get; set; }

        /// <inheritdoc />
        public CultureInfo[] Languages { get; set; }

        #endregion

        #region Public methods

        /// <inheritdoc />
        public virtual void PopulateResults(string searchQuery, string indexCatalogue, int? skip, string language, string orderBy)
        {
            this.PopulateResults(searchQuery, indexCatalogue, skip, language, orderBy, null, null, null);
        }

        /// <inheritdoc />
        public virtual void PopulateResults(string searchQuery, string indexCatalogue, int? skip, string language, string orderBy, string filter, SearchScoring searchScoringSettings, bool? resultsForAllSites)
        {
            this.IndexCatalogue = indexCatalogue;
            this.InitializeOrderByEnum(orderBy);

            if (skip == null)
                skip = 0;

            var itemsToSkip = this.DisplayMode == ListDisplayMode.Paging ? skip.Value : 0;

            int? take = 0;

            if (this.DisplayMode == ListDisplayMode.Limit)
            {
                take = this.LimitCount;
            }
            else if (this.DisplayMode == ListDisplayMode.Paging)
            {
                take = this.ItemsPerPage;
            }
            else
            {
                take = int.MaxValue;
            }

            int totalCount = 0;
            var result = this.Search(searchQuery, language, itemsToSkip, take.Value, filter, searchScoringSettings, resultsForAllSites, out totalCount);

            int? totalPagesCount = (int)Math.Ceiling((double)(totalCount / (double)this.ItemsPerPage.Value));
            this.TotalPagesCount = this.DisplayMode == ListDisplayMode.Paging ? totalPagesCount : null;

            string queryTest = searchQuery.Trim('\"');
            var filteredResultsText = Res.Get<SearchWidgetsResources>().SearchResultsStatusMessageShort;
            this.ResultText = string.Format(filteredResultsText, HttpUtility.HtmlEncode(queryTest));

            var culture = string.IsNullOrEmpty(language) ? Telerik.Sitefinity.Services.SystemManager.CurrentContext.Culture.Name : language;
            using (new CultureRegion(culture))
            {
                this.Results = new ResultModel(result.ToList(), totalCount);
            }
        }

        /// <summary>
        /// Validates the search query.
        /// </summary>
        /// <param name="searchQuery">The search query.</param>
        /// <returns></returns>
        public virtual bool ValidateQuery(ref string searchQuery)
        {
            string message = string.Empty;
            bool isValid = this.EscapeSpecialChars ? this.EscapeSpecialChars : this.Validate(ref searchQuery, out message);
            if (isValid)
            {
                string queryTest = searchQuery.Trim('\"');
                var filteredResultsText = Res.Get<SearchWidgetsResources>().SearchResultsStatusMessageShort;
                this.ResultText = string.Format(filteredResultsText, HttpUtility.HtmlEncode(queryTest));
            }
            else
            {
                this.ResultText = message;
            }

            return isValid;
        }

        /// <summary>
        /// Searches the specified query.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="language">The language.</param>
        /// <param name="skip">The skip.</param>
        /// <param name="take">The take.</param>
        /// <param name="scoringSettings">The search scoring settings.</param>
        /// <param name="hitCount">The hit count.</param>
        /// <param name="filterParameters">The filter parameters</param>
        /// <param name="resultsForAllSites">Indicates wether results are shown for all indexed sites or only for the current site if the search index is created for all sites.</param> 
        /// <returns></returns>
        public IEnumerable<IDocument> Search(string query, string language, int skip, int take, string filterParameters, SearchScoring scoringSettings, bool? resultsForAllSites, out int hitCount)
        {
            var searcher = ObjectFactory.Resolve<ISearchResultsBuilder>();
            var searchParameters = new SearchBuilderParams()
            {
                IndexName = this.IndexCatalogue,
                SearchText = query,
                Culture = language,
                SearchFields = this.SearchFields,
                HighlightedFields = this.HighlightedFields,
                Skip = skip,
                Take = take,
                OrderBy = this.GetOrderList(),
                GetResultsFromAllSites = resultsForAllSites,
                SetLinksOnlyFromCurrentSite = this.ShowLinksOnlyFromCurrentSite,
                ScoringSettings = scoringSettings,
                SearchFilter = filterParameters.IsNullOrEmpty() ? null : this.searchProcessor.BuildFacetFilter(filterParameters, this.IndexCatalogue)
            };

            return searcher.Search(searchParameters, out hitCount);
        }

        #endregion

        #region Private methods

        private IList<ISearchFilter> GetFilterGroups(ISearchQuery searchQuery)
        {
            if (searchQuery != null && searchQuery.Filter != null && searchQuery.Filter.Groups != null)
            {
                return searchQuery.Filter.Groups.ToList();
            }

            return new List<ISearchFilter>();
        }

        /// <summary>
        /// Validates the passed user input against the defined validation rules
        /// </summary>
        /// <param name="searchQuery">user input</param>
        /// <param name="message">message from the config section if input is invalid</param>
        /// <returns>the modified user input according the rules
        /// defined in the config section and the message for the rule applied
        /// </returns>
        private bool Validate(ref string searchQuery, out string message)
        {
            message = string.Empty;

            foreach (var matchRule in this.GetValidationRules())
            {
                if (matchRule.RegEx != null)
                {
                    Match match = matchRule.RegEx.Match(searchQuery);

                    if (match.Success)
                    {
                        message = matchRule.Message;

                        if (!string.IsNullOrEmpty(matchRule.Replacement))
                        {
                            searchQuery = matchRule.RegEx.Replace(searchQuery, matchRule.Replacement);
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// Gets the validation rules.
        /// </summary>
        /// <returns>List of validation rules.</returns>
        private IList<Telerik.Sitefinity.Services.Search.Web.UI.Public.SearchResults.InputMatch> GetValidationRules()
        {
            List<Telerik.Sitefinity.Services.Search.Web.UI.Public.SearchResults.InputMatch> rules;
            RegexOptions regexOptions = RegexOptions.Compiled;

            rules = new List<Telerik.Sitefinity.Services.Search.Web.UI.Public.SearchResults.InputMatch>();
            var config = Config.Get<SearchConfig>();
            foreach (SearchValidationElement element in config.SearchInputValidation)
            {
                bool enabled;
                bool.TryParse(element.Enabled, out enabled);

                if (element.MatchPattern != null
                    && enabled)
                {
                    rules.Add(new Telerik.Sitefinity.Services.Search.Web.UI.Public.SearchResults.InputMatch(
                            element.ReplacementString,
                            new Regex(element.MatchPattern, regexOptions),
                            element.MatchAlert));
                }
            }

            return rules;
        }

        /// <summary>
        /// Gets the order list.
        /// </summary>
        /// <returns></returns>
        protected IEnumerable<string> GetOrderList()
        {
            switch (this.OrderBy)
            {
                case OrderByOptions.Relevance:
                    return null;
                case OrderByOptions.Oldest:
                    return new[] { "PublicationDate" };
                case OrderByOptions.Newest:
                    return new[] { "PublicationDate desc" };
                default:
                    return null;
            }
        }

        /// <summary>
        /// Initializes the order by enum.
        /// </summary>
        /// <param name="orderBy">The order by.</param>
        private void InitializeOrderByEnum(string orderBy)
        {
            if (!orderBy.IsNullOrEmpty())
            {
                OrderByOptions orderByOption;
                Enum.TryParse<OrderByOptions>(orderBy, true, out orderByOption);
                this.OrderBy = orderByOption;
            }
        }

        #endregion

        #region Private fields and constants

        private int? limitCount = 20;
        private int? itemsPerPage = 20;
        private string[] searchFields = new[] { "Title", "Content" };
        private string[] highlightedFields = new[] { "Title", "Content" };
        private readonly SearchFacetsQueryStringProcessor searchProcessor;

        #endregion
    }
}