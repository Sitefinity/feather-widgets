using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Telerik.Sitefinity.Frontend.Search.Mvc.StringResources;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers.Attributes;
using Telerik.Sitefinity.Mvc;
using Telerik.Sitefinity.Frontend.Search.Mvc.Models;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers;
using System.ComponentModel;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Services.Search.Configuration;
using Telerik.Sitefinity.Services;
using System.Globalization;

namespace Telerik.Sitefinity.Frontend.Search.Mvc.Controllers
{
    /// <summary>
    /// This class represents the controller of Search box widget.
    /// </summary>
    [ControllerToolboxItem(Name = "SearchBox", Title = "Search box", SectionName = "MvcWidgets")]
    [Localization(typeof(SearchWidgetsResources))]
    public class SearchBoxController : Controller
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
        /// Gets the Search box widget model.
        /// </summary>
        /// <value>
        /// The model.
        /// </value>
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public ISearchBoxModel Model
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
        /// Renders appropriate view depending on the <see cref="TemplateName" />
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult" />.
        /// </returns>
        public ActionResult Index()
        {
            var query = this.GetSearchQueryFromQueryString(this.Model.IndexCatalogue);
            this.ViewBag.SearchQuery = query;

            return this.View(this.TemplateName, this.Model);
        }

        #endregion

        #region ProtectedMethods
        /// <summary>
        /// Gets the current UI language.
        /// </summary>
        protected virtual string GetCurrentUILanguage()
        {
            return SystemManager.CurrentContext.AppSettings.Multilingual ?
                CultureInfo.CurrentUICulture.Name : null;
        }

        /// <summary>
        /// Gets the minimal length of the suggestion.
        /// </summary>
        /// <returns></returns>
        protected virtual int GetMinSuggestLength()
        {
            return Config.Get<SearchConfig>().MinSuggestLength;
        }
        #endregion

        #region Private methods
        /// <summary>
        /// Initializes the model.
        /// </summary>
        /// <returns>
        /// The <see cref="ISearchBoxModel"/>.
        /// </returns>
        private ISearchBoxModel InitializeModel()
        {
            var constructorParams = new Dictionary<string, object>
            {
                {"suggestionsRoute", "/restapi/search/suggestions"},
                {"minSuggestionLength", this.GetMinSuggestLength()},
                {"suggestionFields", "Title,Content"},
                {"language", this.GetCurrentUILanguage()}
            };
            return ControllerModelFactory.GetModel<ISearchBoxModel>(this.GetType(), constructorParams);
        }

        /// <summary>
        /// Gets the search query from the query string if the index catalog matchs the one used in the widget.
        /// </summary>
        /// <param name="currentCatalogue">The current index catalogue.</param>
        /// <returns></returns>
        private string GetSearchQueryFromQueryString(string currentCatalogue)
        {
            var searchQuery = string.Empty;

            //Set the search text if searchQuery exists in the QueryString and the IndexCatalogue matches the current one.
            var context = SystemManager.CurrentHttpContext;
            if (context != null)
            {
                 string indexCatalogue = context.Request.QueryString["indexCatalogue"];
                 if (!string.IsNullOrEmpty(indexCatalogue) &&
                     indexCatalogue.Equals(currentCatalogue))
                 {
                     searchQuery = context.Request.QueryString["searchQuery"] ?? string.Empty;
                     searchQuery = searchQuery.Trim();
                 }
            }

            return searchQuery;
        }        
        #endregion

        #region Private fields and constants
        private ISearchBoxModel model;
        private string templateName = "SearchBox";
        #endregion
    }
}
