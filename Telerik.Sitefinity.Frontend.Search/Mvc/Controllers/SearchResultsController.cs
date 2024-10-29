using System.Collections.Generic;
using System.ComponentModel;
using System.Web.Mvc;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers.Attributes;
using Telerik.Sitefinity.Frontend.Search.Mvc.Models;
using Telerik.Sitefinity.Frontend.Search.Mvc.StringResources;
using Telerik.Sitefinity.Mvc;
using Telerik.Sitefinity.Search;
using Telerik.Sitefinity.Search.Impl;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Services.Search;
using Telerik.Sitefinity.Services.Search.Configuration;
using Telerik.Sitefinity.Web;

namespace Telerik.Sitefinity.Frontend.Search.Mvc.Controllers
{
    /// <summary>
    /// Represents the Controller of the Search results widget.
    /// </summary>
    [ControllerToolboxItem(
        Name = SearchResultsController.WidgetName,
        Title = nameof(SearchWidgetsResources.SearchResultsTitle),
        Description = nameof(SearchWidgetsResources.SearchResultsDescription),
        ResourceClassId = nameof(SearchWidgetsResources),
        SectionName = "Search",
        ModuleName = "Search",
        CssClass = SearchResultsController.WidgetIconCssClass)]
    [Localization(typeof(SearchWidgetsResources))]
    public class SearchResultsController : Controller
    {
        #region Properties
        /// <summary>
        /// Gets or sets the name of the template that will be displayed.
        /// </summary>
        /// <value></value>
        public string TemplateName
        {
            get
            {
                return this.templateName;
            }

            set
            {
                this.templateName = value;
            }
        }

        /// <summary>
        /// Gets the Search results widget model.
        /// </summary>
        /// <value>The model.</value>
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public ISearchResultsModel Model
        {
            get
            {
                if (this.model == null)
                    this.model = this.InitializeModel();

                return this.model;
            }
        }
        #endregion

        #region Actions
        /// <summary>
        /// Renders appropriate view depending on the <see cref="TemplateName"/>
        /// </summary>
        /// <param name="page">The page.</param>
        /// <param name="searchQuery">The search query.</param>
        /// <param name="indexCatalogue">The index catalogue.</param>
        /// <param name="language">The language.</param>
        /// <param name="orderBy">The order by.</param>
        /// <param name="scoringInfo">The param used to boost the search results.</param>
        /// <param name="filter">The param used to filter the search results.</param>
        /// <param name="resultsForAllSites">The param used to filter search results per site when the index is for all sites.</param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        [HttpGet]
        public ActionResult Index(int? page, string searchQuery = null, string indexCatalogue = null, string language = null, string orderBy = null, string scoringInfo = null, string filter = null, bool? resultsForAllSites = null)
        {
            if (!this.IsSearchModuleActivated())
            {
                return null;
            }

            //// We use the QueryStringGet in order to register output cache variations from the different parameters in the query string
            //// For reference you can check https://www.progress.com/documentation/sitefinity-cms/configure-cache-variation-by-query-string
            if (HttpContext != null)
            {
                //// We retrieve all parameters from the query string to prevent the case where the MVC model binder retrieves them from the body of the request.
                //// The last can result cache web cache poisoning since we vary the output cache only by qurty params
                page = NullableParser.ParseIntNullable(this.ControllerContext.RequestContext.RouteData.Values[nameof(page)]?.ToString());
                searchQuery = HttpContext.Request.QueryStringGet(nameof(searchQuery));
                indexCatalogue = HttpContext.Request.QueryStringGet(nameof(indexCatalogue));
                language = HttpContext.Request.QueryStringGet(nameof(language));
                orderBy = HttpContext.Request.QueryStringGet(nameof(orderBy));
                scoringInfo = HttpContext.Request.QueryStringGet(nameof(scoringInfo));
                filter = HttpContext.Request.QueryStringGet(nameof(filter));
                resultsForAllSites = NullableParser.ParseBoolNullable(HttpContext.Request.QueryStringGet(nameof(resultsForAllSites)));
            }

            if (!string.IsNullOrEmpty(searchQuery))
            {
                bool isValid = this.Model.ValidateQuery(ref searchQuery);

                if (isValid)
                {
                    string queryString = this.BuildSearchResultsQueryString(searchQuery, indexCatalogue, orderBy, scoringInfo, filter, resultsForAllSites);

                    string languageParamFormat = "&language={0}";
                    string languageParam = string.IsNullOrEmpty(language) ? string.Empty : string.Format(languageParamFormat, language);
                    string currentPageUrl = this.GetCurrentUrl();

                    this.ViewBag.LanguageSearchUrlTemplate = string.Concat(currentPageUrl, queryString, languageParamFormat);
                    this.ViewBag.RedirectPageUrlTemplate = string.Concat(currentPageUrl, "/{0}", queryString, languageParam);
                    this.ViewBag.IsFilteredbyPermission = this.EnableFilterByViewPermissions();

                    if (page == null || page < 1)
                        page = 1;

                    this.Model.CurrentPage = page.Value;

                    int? itemsToSkip = this.Model.DisplayMode == ListDisplayMode.Paging ? ((page.Value - 1) * this.Model.ItemsPerPage) : 0;
                    var searchScoringDecoder = new SearchScoringDecoder();
                    SearchScoring searchScoring = searchScoringDecoder.GetSearchScoringSettings(scoringInfo);

                    string decodedFilter = filter?.Base64Decode();
                    this.Model.PopulateResults(searchQuery, indexCatalogue, itemsToSkip, language, orderBy, decodedFilter, searchScoring, resultsForAllSites);

                    return this.View(this.TemplateName, this.Model);
                }
                else
                {
                    return this.View(SearchResultsController.ValidationErrorViewName);
                }
            }

            return null;
        }

        /// <summary>
        /// Provides search results for the specified search query.
        /// </summary>
        /// <param name="searchQuery">The search query.</param>
        /// <param name="indexCatalogue">The index catalogue.</param>
        /// <param name="orderBy">The order by.</param>
        /// <param name="skip">The skip.</param>
        /// <returns></returns>
        public JsonResult Results(string searchQuery = null, string indexCatalogue = null, string language = null, string orderBy = null, int? skip = null)
        {
            if (!string.IsNullOrEmpty(searchQuery))
            {
                this.Model.PopulateResults(searchQuery, indexCatalogue, skip, language, orderBy);
            }

            return this.Json(this.Model.Results, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Private methods

        /// <summary>
        /// Determines whether the permission filter is enabled.
        /// </summary>
        /// <returns></returns>
        protected virtual bool EnableFilterByViewPermissions()
        {
            var config = Config.Get<SearchConfig>();
            return config.EnableFilterByViewPermissions;
        }

        /// <summary>
        /// Gets the current URL.
        /// </summary>
        /// <returns></returns>
        protected virtual string GetCurrentUrl()
        {
            return this.GetCurrentPageUrl();
        }

        /// <summary>
        /// Initializes the model.
        /// </summary>
        private ISearchResultsModel InitializeModel()
        {
            var languages = SystemManager.CurrentContext.AppSettings.DefinedFrontendLanguages;
            var constructorParams = new Dictionary<string, object>
            {
                { "languages", languages }
            };

            return ControllerModelFactory.GetModel<ISearchResultsModel>(this.GetType(), constructorParams);
        }

        /// <summary>
        /// Determines whether the search module is activated.
        /// </summary>
        /// <returns></returns>
        protected virtual bool IsSearchModuleActivated()
        {
            var module = SystemManager.GetModule(SearchModule.ModuleName);
            return module != null;
        }

        #endregion

        private string BuildSearchResultsQueryString(string searchQuery, string indexCatalogue, string orderBy, string scoringInfo, string filter, bool? resultsForAllSites)
        {
            var queryStringFormat = "?indexCatalogue={0}&searchQuery={1}&orderBy={2}";

            var queryString = string.Format(queryStringFormat, indexCatalogue, searchQuery, orderBy ?? this.Model.OrderBy.ToString());
            if (!string.IsNullOrEmpty(scoringInfo))
            {
                queryString = $"{queryString}&scoringInfo={scoringInfo}";
            }

            if (!string.IsNullOrEmpty(filter))
            {
                queryString = $"{queryString}&filter={filter}";
            }

            if (resultsForAllSites.HasValue)
            {
                queryString = $"{queryString}&resultsForAllSites={resultsForAllSites}";
            }

            return queryString;
        }

        #region Private fields and constants
        internal const string WidgetIconCssClass = "sfSearchResultIcn sfMvcIcn";
        internal const string QueryStringParamName = "indexCatalogue";
        private ISearchResultsModel model;
        private string templateName = "SearchResults";
        private const string ValidationErrorViewName = "InputValidationError";
        private const string WidgetName = "SearchResults_MVC";
        #endregion
    }
}