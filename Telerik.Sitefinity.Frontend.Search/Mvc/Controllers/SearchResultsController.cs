using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers.Attributes;
using Telerik.Sitefinity.Frontend.Search.Mvc.Models;
using Telerik.Sitefinity.Frontend.Search.Mvc.StringResources;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Mvc;
using Telerik.Sitefinity.Services;

namespace Telerik.Sitefinity.Frontend.Search.Mvc.Controllers
{
    /// <summary>
    /// Represents the Controller of the Search results widget.
    /// </summary>
    [ControllerToolboxItem(Name = "SearchResults", Title = "Search results", SectionName = "MvcWidgets")]
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
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult Index(int? page, string searchQuery = null, string indexCatalogue = null, string wordsMode = null, string language = null, string orderBy = null)
        {
            if (!String.IsNullOrEmpty(searchQuery))
            {
                bool isValid = this.Model.ValidateQuery(ref searchQuery);

                if (isValid)
                {
                    this.InitializeOrderByEnum(orderBy);

                    var queryStringFormat = "?indexCatalogue={0}&searchQuery={1}&wordsMode={2}&orderBy={3}";
                    var languageParamFormat = "&language={0}";

                    var queryString = string.Format(queryStringFormat, indexCatalogue, searchQuery, wordsMode, this.Model.OrderBy);
                    var languageParam = String.IsNullOrEmpty(language) ? String.Empty : String.Format(languageParamFormat, language);
                    var currentPageUrl = this.GetCurrentPageUrl();

                    this.ViewBag.LanguageSearchUrlTemplate = String.Concat(currentPageUrl, queryString, languageParamFormat);
                    this.ViewBag.RedirectPageUrlTemplate = String.Concat(this.GetCurrentPageUrl(), "/{0}", queryString, languageParam);
                    
                    this.Model.IndexCatalogue = indexCatalogue;

                    if (page == null || page < 1)
                        page = 1;

                    int? itemsToSkip = this.Model.DisplayMode == ListDisplayMode.Paging ? ((page.Value - 1) * this.Model.ItemsPerPage) : 0;
                    this.Model.PopulateResults(searchQuery, itemsToSkip, language);

                    return View(this.TemplateName, this.Model);
                }
                else
                {
                    return View(SearchResultsController.ValidationErrorViewName);
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
            if (!String.IsNullOrEmpty(searchQuery))
            {
                this.InitializeOrderByEnum(orderBy);
                this.Model.IndexCatalogue = indexCatalogue;
                this.Model.PopulateResults(searchQuery, skip, language);
            }

            return Json(this.Model.Results, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Private methods
        
        /// <summary>
        /// Initializes the model.
        /// </summary>
        private ISearchResultsModel InitializeModel()
        {
            var languages = SystemManager.CurrentContext.AppSettings.DefinedFrontendLanguages;
            var constructorParams = new Dictionary<string, object>
            {
                {"languages", languages}
            };
            

            return ControllerModelFactory.GetModel<ISearchResultsModel>(this.GetType(), constructorParams);
        }

        private void InitializeOrderByEnum(string orderBy)
        {
            if (!orderBy.IsNullOrEmpty())
            {
                OrderByOptions orderByOption;
                Enum.TryParse<OrderByOptions>(orderBy, true, out orderByOption);
                this.Model.OrderBy = orderByOption;
            }
        }

        #endregion

        #region Private fields and constants
        private ISearchResultsModel model;
        private string templateName = "SearchResults";
        private const string ValidationErrorViewName = "InputValidationError";
        #endregion

    }
}
