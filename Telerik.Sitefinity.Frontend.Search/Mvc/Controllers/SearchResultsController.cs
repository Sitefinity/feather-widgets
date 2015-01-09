using System;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers.Attributes;
using Telerik.Sitefinity.Frontend.Search.Mvc.Models;
using Telerik.Sitefinity.Frontend.Search.Mvc.StringResources;
using Telerik.Sitefinity.Mvc;

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
        ///  Renders appropriate view depending on the <see cref="TemplateName" />
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult" />.
        /// </returns>
        public ActionResult Index(int? page, string searchQuery = null, string indexCatalogue = null, string wordsMode = null, string orderBy = "Relevance")
        {
            var queryString = string.Format("?indexCatalogue={0}&searchQuery={1}&wordsMode={2}", indexCatalogue, searchQuery, wordsMode);
            this.ViewBag.RedirectPageUrlTemplate = this.GetCurrentPageUrl() + "/{0}" + queryString;

            // Get the model
            if (!String.IsNullOrEmpty(searchQuery))
            {
                OrderByOptions orderByOption;
                Enum.TryParse<OrderByOptions>(orderBy, true, out orderByOption);
                this.Model.OrderBy = orderByOption;
                this.Model.IndexCatalogue = indexCatalogue;
                this.Model.PopulateResults(searchQuery, page);
            }

            return View(this.TemplateName, this.Model);
        }
        #endregion

        #region Private methods
        
        /// <summary>
        /// Initializes the model.
        /// </summary>
        private ISearchResultsModel InitializeModel()
        {
            //var constructorParams = new Dictionary<string, object>
            //{
            //    {"suggestionsRoute", "/restapi/search/suggestions"},
            //    {"minSuggestionLength", this.GetMinSuggestLength()},
            //    {"suggestionFields", "Title,Content"},
            //    {"language", this.GetCurrentUILanguage()},
            //    {"cssClass", this.CssClass}
            //};
            return ControllerModelFactory.GetModel<ISearchResultsModel>(this.GetType());
        }

        #endregion

        #region Private fields and constants
        private ISearchResultsModel model;
        private string templateName = "SearchResults";
        #endregion
    }
}
