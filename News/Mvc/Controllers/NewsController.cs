using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web.Mvc;

using News.Mvc.Models;
using News.Mvc.StringResources;
using Telerik.Sitefinity.ContentLocations;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers.Attributes;
using Telerik.Sitefinity.Modules.News;
using Telerik.Sitefinity.Mvc;
using Telerik.Sitefinity.News.Model;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Taxonomies.Model;

namespace News.Mvc.Controllers
{
    /// <summary>
    /// This class represents the controller of News widget.
    /// </summary>
    [ControllerToolboxItem(Name = "News", Title = "News", SectionName = "MvcWidgets", ModuleName = "News")]
    [Localization(typeof(NewsWidgetResources))]
    public class NewsController : Controller, IContentLocatableView
    {
        #region Properties

        /// <summary>
        /// Gets or sets the name of the template that will be displayed when widget is in List view.
        /// </summary>
        /// <value></value>
        public string ListTemplateName
        {
            get
            {
                return this.listTemplateName;
            }

            set
            {
                this.listTemplateName = value;
            }
        }

        /// <summary>
        /// Gets or sets the name of the template that will be displayed when widget is in Detail view.
        /// </summary>
        /// <value></value>
        public string DetailTemplateName
        {
            get
            {
                return this.detailTemplateName;
            }

            set
            {
                this.detailTemplateName = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the canonical URL tag should be added to the page when the canonical meta tag should be added to the page.
        /// If the value is not set, the settings from SystemConfig -> ContentLocationsSettings -> DisableCanonicalURLs will be used. 
        /// </summary>
        /// <value>The disable canonical URLs.</value>
        public bool? DisableCanonicalUrlMetaTag
        {
            get
            {
                return this.disableCanonicalUrlMetaTag;
            }

            set
            {
                this.disableCanonicalUrlMetaTag = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether detail view for a news item should be opened in the same page.
        /// </summary>
        /// <value>
        /// <c>true</c> if details link should be opened in the same page; otherwise, (if should redirect to custom selected page)<c>false</c>.
        /// </value>
        public bool OpenInSamePage
        {
            get
            {
                return this.openInSamePage;
            }

            set
            {
                this.openInSamePage = value;
            }
        }

        /// <summary>
        /// Gets or sets the id of the page where will be displayed details view for selected news item.
        /// </summary>
        /// <value>The details page id.</value>
        public Guid DetailsPageId { get; set; }

        /// <summary>
        /// Gets or sets the page URL where will be displayed details view for selected news item.
        /// </summary>
        /// <value>
        /// The page URL where will be displayed details view for selected news item.
        /// </value>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1056:UriPropertiesShouldNotBeStrings")]
        [Browsable(false)]
        [Obsolete("This property is not used anymore. The details page url is resolved based on the DetailsPageId property.")]
        public string DetailsPageUrl
        {
            get
            {
                if (this.OpenInSamePage)
                {
                    var url = this.GetCurrentPageUrl();
                    return url;
                }
                else
                {
                    return this.detailsPageUrl;
                }
            }

            set
            {
                this.detailsPageUrl = value;
            }
        }        

        /// <summary>
        /// Gets the News widget model.
        /// </summary>
        /// <value>
        /// The model.
        /// </value>
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public INewsModel Model 
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
        /// Renders appropriate list view depending on the <see cref="ListTemplateName" />
        /// </summary>
        /// <param name="page">The page.</param>
        /// <returns>
        /// The <see cref="ActionResult" />.
        /// </returns>
        public ActionResult Index(int? page)
        {
            var fullTemplateName = this.listTemplateNamePrefix + this.ListTemplateName;
            this.ViewBag.CurrentPageUrl = this.GetCurrentPageUrl();
            this.ViewBag.RedirectPageUrlTemplate = this.ViewBag.CurrentPageUrl + "/{0}";
            this.ViewBag.DetailsPageId = this.DetailsPageId;
            this.ViewBag.OpenInSamePage = this.OpenInSamePage;

            this.Model.PopulateItems(null, null, page);
            this.AddCacheDependencies();

            return this.View(fullTemplateName, this.Model);
        }

        /// <summary>
        /// Renders appropriate list view depending on the <see cref="ListTemplateName" /> 
        /// </summary>
        /// <param name="taxonFilter">The taxonomy filter.</param>
        /// <param name="page">The page.</param>
        /// <returns>
        /// The <see cref="ActionResult" />.
        /// </returns>
        public ActionResult ListByTaxon(ITaxon taxonFilter, int? page)
        {
            var fullTemplateName = this.listTemplateNamePrefix + this.ListTemplateName;
            var fieldName = this.GetExpectedTaxonFieldName(taxonFilter);
            this.ViewBag.CurrentPageUrl = this.GetCurrentPageUrl();
            this.ViewBag.RedirectPageUrlTemplate = this.ViewBag.CurrentPageUrl + "/" + taxonFilter.UrlName + "/{0}";
            this.ViewBag.DetailsPageId = this.DetailsPageId;
            this.ViewBag.OpenInSamePage = this.OpenInSamePage;

            this.Model.PopulateItems(taxonFilter, fieldName, page);
            this.AddCacheDependencies();

            return this.View(fullTemplateName, this.Model);
        }

        /// <summary>
        /// Renders appropriate list view depending on the <see cref="DetailTemplateName"/>
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult Details(NewsItem newsItem)
        {
            var fullTemplateName = this.detailTemplateNamePrefix + this.DetailTemplateName;
            this.Model.DetailItem = newsItem;
            this.ViewBag.Title = newsItem.Title;
            this.AddCacheDependencies();

            return this.View(fullTemplateName, this.Model);
        }

        /// <summary>
        /// Gets the information for all of the content types that a control is able to show.
        /// </summary>
        /// <returns>
        /// List of location info of the content that this control is able to show.
        /// </returns>
        [NonAction]
        public IEnumerable<IContentLocationInfo> GetLocations()
        {
            var location = new ContentLocationInfo();
            location.ContentType = typeof(NewsItem);
            location.ProviderName = NewsManager.GetManager(this.Model.ProviderName).Provider.Name;

            var filterExpression = this.Model.CompileFilterExpression();
            if (!string.IsNullOrEmpty(filterExpression))
            {
                location.Filters.Add(new BasicContentLocationFilter(filterExpression));
            }

            return new[] { location };
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Initializes the model.
        /// </summary>
        /// <returns>
        /// The <see cref="INewsModel"/>.
        /// </returns>
        private INewsModel InitializeModel()
        {
            return ControllerModelFactory.GetModel<INewsModel>(this.GetType());
        }

        /// <summary>
        /// Adds the cache dependencies.
        /// </summary>
        private void AddCacheDependencies()
        {
            if (SystemManager.CurrentHttpContext != null)
            {
                this.AddCacheDependencies(this.Model.GetKeysOfDependentObjects());
            }
        }
 
        private string GetExpectedTaxonFieldName(ITaxon taxon)
        {
            if (taxon.Taxonomy.Name == "Categories")
                return taxon.Taxonomy.TaxonName;

            return taxon.Taxonomy.Name;
        }

        #endregion

        #region Private fields and constants

        private INewsModel model;
        private string listTemplateName = "NewsList";
        private string listTemplateNamePrefix = "List.";
        private string detailTemplateName = "DetailPage";
        private string detailTemplateNamePrefix = "Detail.";
        private bool openInSamePage = true;

        private bool? disableCanonicalUrlMetaTag;
        private string detailsPageUrl;

        #endregion
    }
}
