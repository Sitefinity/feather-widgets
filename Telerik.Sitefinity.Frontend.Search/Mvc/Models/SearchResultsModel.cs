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
using Telerik.Sitefinity.Publishing;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Services.Search;
using Telerik.Sitefinity.Services.Search.Configuration;
using Telerik.Sitefinity.Services.Search.Data;
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
        public virtual void PopulateResults(string searchQuery, int? skip, string language)
        {
            if (skip == null)
                skip = 0;

            var itemsToSkip = this.DisplayMode == ListDisplayMode.Paging ? skip.Value : 0; 
            int totalCount = 0;
            int? itemsPerPage = this.DisplayMode == ListDisplayMode.All ? 0 : this.ItemsPerPage;

            var result = this.Search(searchQuery, this.IndexCatalogue, language, itemsToSkip, itemsPerPage.Value, out totalCount);

            string queryTest = searchQuery.Trim('\"');

            var filteredResultsText = Res.Get<SearchWidgetsResources>().SearchResultsStatusMessageShort;
            this.ResultText = string.Format(filteredResultsText, HttpUtility.HtmlEncode(queryTest));
            this.Results = new ResultModel(result.ToList(), totalCount);
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
        /// <param name="catalogue">The catalogue.</param>
        /// <param name="skip">The skip.</param>
        /// <param name="take">The take.</param>
        /// <param name="hitCount">The hit count.</param>
        /// <returns></returns>
        public IEnumerable<IDocument> Search(string query, string catalogue, string language, int skip, int take, out int hitCount)
        {
            var service = Telerik.Sitefinity.Services.ServiceBus.ResolveService<ISearchService>();
            var queryBuilder = ObjectFactory.Resolve<IQueryBuilder>();
            var config = Config.Get<SearchConfig>();
            var enableExactMatch = config.EnableExactMatch;

            var searchQuery = queryBuilder.BuildQuery(query, this.SearchFields);
            searchQuery.IndexName = catalogue;
            searchQuery.Skip = skip;
            searchQuery.Take = take;
            searchQuery.OrderBy = this.GetOrderList();
            searchQuery.EnableExactMatch = enableExactMatch;
            searchQuery.HighlightedFields = this.HighlightedFields;

            ISearchFilter filter;
            if (this.TryBuildLanguageFilter(language, out filter))
            {
                searchQuery.Filter = filter;
            }

            var oldSkipValue = skip;
            IResultSet result = service.Search(searchQuery);
            hitCount = result.HitCount;

            return result.SetContentLinks();
        }
        #endregion

        #region Private methods

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

        private bool TryBuildLanguageFilter(string language, out ISearchFilter filter)
        {
            if (String.IsNullOrEmpty(language) ||
                !SystemManager.CurrentContext.AppSettings.Multilingual)
            {
                filter = null;
                return false;
            }

            filter = ObjectFactory.Resolve<ISearchFilter>();
            filter.Clauses = new List<ISearchFilterClause>()
            {
                new SearchFilterClause(PublishingConstants.LanguageField, this.TransformLanguageFieldValue(language), FilterOperator.Equals),
                new SearchFilterClause(PublishingConstants.LanguageField, "nullvalue", FilterOperator.Equals)
            };
            filter.Operator = QueryOperator.Or;

            return true;
        }

        private string TransformLanguageFieldValue(string language)
        {
            var result = language.ToLowerInvariant().Replace("-", string.Empty);
            return result;
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
                case OrderByOptions.TitleAsc:
                    return new[] { "Title" };
                case OrderByOptions.TitleDesc:
                    return new[] { "Title desc" };
                case OrderByOptions.Oldest:
                    return new[] { "PublicationDate" };
                case OrderByOptions.Newest:
                    return new[] { "PublicationDate desc" };
                case OrderByOptions.NewModified:
                    return new[] { "LastModified desc" };
                default:
                    return null;
            }
        }

        #endregion

        #region Private fields and constants

        private int? itemsPerPage = 20;
        private string[] searchFields = new[] { "Title", "Content" };
        private string[] highlightedFields = new[] { "Title", "Content" };

        #endregion
    }
}
