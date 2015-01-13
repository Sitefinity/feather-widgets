using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
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
        public CultureInfo[] Languages { get; set; }

        #endregion

        #region Public methods
        /// <inheritdoc />
        public void PopulateResults(string searchQuery, int? skip, string language)
        {
            if (skip == null)
                skip = 0;

            var itemsToSkip = this.DisplayMode == ListDisplayMode.Paging ? skip.Value : 0;

            int totalCount = 0;
            int? itemsPerPage = this.DisplayMode == ListDisplayMode.All ? 0 : this.ItemsPerPage;

            var result = this.Search(searchQuery, this.IndexCatalogue, language, itemsToSkip, itemsPerPage.Value, out totalCount);

            int? totalPagesCount = (int)Math.Ceiling((double)(totalCount / (double)this.ItemsPerPage.Value));
            this.TotalPagesCount = this.DisplayMode == ListDisplayMode.Paging ? totalPagesCount : null;

            string queryTest = searchQuery.Trim('\"');
            var filteredResultsText = Res.Get<SearchWidgetsResources>().SearchResultsStatusMessageShort;
            this.ResultText = string.Format(filteredResultsText, HttpUtility.HtmlEncode(queryTest));

            this.Results = new ResultModel(result.ToList(), totalCount);
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
