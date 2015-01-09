using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Web;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Frontend.Search.Mvc.StringResources;
using Telerik.Sitefinity.Localization;
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

        #endregion

        #region Methods

        /// <inheritdoc />
        public void PopulateResults(string searchQuery, int? skip)
        {
            if (skip == null)
                skip = 0;

            var itemsToSkip = this.DisplayMode == ListDisplayMode.Paging ? skip.Value : 0; 
            int totalCount = 0;
            int? itemsPerPage = this.DisplayMode == ListDisplayMode.All ? 0 : this.ItemsPerPage;


            var result = this.Search(searchQuery, this.IndexCatalogue, itemsToSkip, itemsPerPage.Value, out totalCount);

            string queryTest = searchQuery.Trim('\"');

            var filteredResultsText = Res.Get<SearchWidgetsResources>().SearchResultsStatusMessageShort;
            this.ResultText = string.Format(filteredResultsText, HttpUtility.HtmlEncode(queryTest));
            this.Results = new ResultModel() { Data = result.ToList(), TotalCount = totalCount };
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
        protected virtual IEnumerable<IDocument> Search(string query, string catalogue, int skip, int take, out int hitCount)
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

            var oldSkipValue = skip;
            IResultSet result = service.Search(searchQuery);
            hitCount = result.HitCount;

            return result.SetContentLinks();
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
                    return new[] { "_score desc" };
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
                    return new[] { "_score" };
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
