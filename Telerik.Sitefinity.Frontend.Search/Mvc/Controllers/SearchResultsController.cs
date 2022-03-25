﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web.Mvc;
using Telerik.Sitefinity.Abstractions;
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
using Telerik.Sitefinity.Web.OutputCache;

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
        /// <param name="wordsMode">The words mode.</param>
        /// <param name="language">The language.</param>
        /// <param name="orderBy">The order by.</param>
        /// <param name="scoringInfo">The param used to boost the search results.</param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult Index(int? page, string searchQuery = null, string indexCatalogue = null, string wordsMode = null, string language = null, string orderBy = null, string scoringInfo = null)
        {
            if (!this.IsSearchModuleActivated())
            {
                return null;
            }

            //// We use the QueryStringGet in order to register output cache variations from the different parameters in the query string
            //// For reference you can check https://www.progress.com/documentation/sitefinity-cms/configure-cache-variation-by-query-string
            if (HttpContext != null)
            {
                HttpContext.Request.QueryStringGet(nameof(page));
                HttpContext.Request.QueryStringGet(nameof(searchQuery));
                HttpContext.Request.QueryStringGet(nameof(indexCatalogue));
                HttpContext.Request.QueryStringGet(nameof(wordsMode));
                HttpContext.Request.QueryStringGet(nameof(language));
                HttpContext.Request.QueryStringGet(nameof(orderBy));
                HttpContext.Request.QueryStringGet(nameof(scoringInfo));
            }

            if (!string.IsNullOrEmpty(searchQuery))
            {
                bool isValid = this.Model.ValidateQuery(ref searchQuery);

                if (isValid)
                {
                    string queryString = this.BuildSearchResultsQueryString(searchQuery, indexCatalogue, wordsMode, orderBy, scoringInfo);

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

                    this.Model.PopulateResults(searchQuery, indexCatalogue, itemsToSkip, language, orderBy, searchScoring);

                    return this.View(this.TemplateName, this.Model);
                }
                else
                {
                    return this.View(SearchResultsController.ValidationErrorViewName);
                }
            }

            return null;
        }

        private string BuildSearchResultsQueryString(string searchQuery, string indexCatalogue, string wordsMode, string orderBy, string scoringInfo)
        {
            var queryStringFormat = "?indexCatalogue={0}&searchQuery={1}&wordsMode={2}&orderBy={3}";

            var queryString = string.Format(queryStringFormat, indexCatalogue, searchQuery, wordsMode, orderBy ?? this.Model.OrderBy.ToString());
            if (!string.IsNullOrEmpty(scoringInfo))
            {
                queryString = $"{queryString}&scoringInfo={scoringInfo}";
            }

            return queryString;
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