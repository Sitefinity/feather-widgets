using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using Telerik.Sitefinity.ContentLocations;
using Telerik.Sitefinity.Frontend.Mvc.Helpers;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers.Attributes;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Routing;
using Telerik.Sitefinity.Frontend.Mvc.Models;
using Telerik.Sitefinity.Frontend.News.Mvc.Models;
using Telerik.Sitefinity.Frontend.News.Mvc.StringResources;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.News;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Modules.Pages.Configuration;
using Telerik.Sitefinity.Mvc;
using Telerik.Sitefinity.News.Model;
using Telerik.Sitefinity.Personalization;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Taxonomies.Model;
using Telerik.Sitefinity.Web;

namespace Telerik.Sitefinity.Frontend.News.Mvc.Controllers
{
    /// <summary>
    /// This class represents the controller of News widget.
    /// </summary>
    [ControllerToolboxItem(
        Name = NewsController.WidgetName,
        Title = nameof(NewsWidgetResources.NewsViewTitle),
        Description = nameof(NewsWidgetResources.NewsViewDescription),
        ResourceClassId = nameof(NewsWidgetResources),
        SectionName = ToolboxesConfig.ContentToolboxSectionName,
        ModuleName = "News",
        CssClass = NewsController.WidgetIconCssClass)]
    [Localization(typeof(NewsWidgetResources))]
    public class NewsController : ContentBaseController, IContentLocatableView, IPersonalizable
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

        /// <summary>
        /// Gets the metadata container.
        /// </summary>
        /// <value>
        /// The metadata container.
        /// </value>
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public override MetadataModel MetadataFields
        {
            get
            {
                if (this.metadata == null)
                {
                    this.metadata = base.MetadataFields;
                    this.metadata.OpenGraphType = PageHelper.OpenGraphTypes.Article;
                }

                return this.metadata;
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
            ITaxon taxonFilter = TaxonUrlEvaluator.GetTaxonFromQuery(this.HttpContext, this.Model.UrlKeyPrefix);
            var fullTemplateName = this.listTemplateNamePrefix + this.ListTemplateName;
            var currentPageUrl = this.GetCurrentPageUrl();

            this.ViewBag.CurrentPageUrl = currentPageUrl;
            this.ViewBag.RedirectPageUrlTemplate = this.GeneratePagingTemplate(currentPageUrl, this.Model.UrlKeyPrefix);
            this.ViewBag.DetailsPageId = this.DetailsPageId;
            this.ViewBag.OpenInSamePage = this.OpenInSamePage;

            this.SetRedirectUrlQueryString(taxonFilter);

            this.UpdatePageFromQuery(ref page, this.Model.UrlKeyPrefix);
            var viewModel = this.Model.CreateListViewModel(taxonFilter, this.ExtractValidPage(page));

            if (this.ShouldReturnDetails(this.Model.ContentViewDisplayMode, viewModel))
            {
                var itemViewModel = viewModel.Items.FirstOrDefault();

                if (itemViewModel == null)
                    return this.HandleInvalidDetailsAction(Res.Get<NewsResources>().NewsDetailViewDesignerResponseMessage);

                return this.Details((NewsItem)itemViewModel.DataItem);
            }

            this.AddCacheDependencies(this.Model.GetKeysOfDependentObjects(viewModel));
            if (viewModel.ContentType != null)
                this.AddCacheVariations(viewModel.ContentType, viewModel.ProviderName);

            return this.View(fullTemplateName, viewModel);
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
            this.ViewBag.CurrentPageUrl = this.GetCurrentPageUrl();
            this.ViewBag.RedirectPageUrlTemplate = this.ViewBag.CurrentPageUrl + UrlHelpers.GetRedirectPagingUrl(taxonFilter, this.ViewBag.UrlParams, this.HttpContext.Request.QueryString.ToQueryString());
            this.ViewBag.DetailsPageId = this.DetailsPageId;
            this.ViewBag.OpenInSamePage = this.OpenInSamePage;

            var viewModel = this.Model.CreateListViewModel(taxonFilter, page ?? 1);
            if (SystemManager.CurrentHttpContext != null)
            {
                this.AddCacheDependencies(this.Model.GetKeysOfDependentObjects(viewModel));
                if (viewModel.ContentType != null)
                    this.AddCacheVariations(viewModel.ContentType, viewModel.ProviderName);
            }

            return this.View(fullTemplateName, viewModel);
        }

        /// <summary>
        /// Renders appropriate list view depending on the <see cref="DetailTemplateName"/>
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
        public ActionResult Details(NewsItem newsItem)
        {
            this.InitializeMetadataDetailsViewBag(newsItem);

            var fullTemplateName = this.detailTemplateNamePrefix + this.DetailTemplateName;
            this.ViewBag.Title = newsItem.Title;

            var viewModel = this.Model.CreateDetailsViewModel(newsItem);
            if (SystemManager.CurrentHttpContext != null)
                this.AddCacheDependencies(this.Model.GetKeysOfDependentObjects(viewModel));

            var page = this.HttpContext.CurrentHandler.GetPageHandler();

            this.AddCanonicalUrlTagIfEnabled(page, newsItem);
                      
            return this.View(fullTemplateName, viewModel);
        }

        /// <summary>
        /// Gets the information for all of the content types that a control is able to show.
        /// </summary>
        /// <returns>
        /// List of location info of the content that this control is able to show.
        /// </returns>
        [NonAction]
        public virtual IEnumerable<IContentLocationInfo> GetLocations()
        {
            return this.Model.GetLocations();
        }

        #endregion

        #region Overriden methods

        /// <inheritDoc/>
        protected override void HandleUnknownAction(string actionName)
        {
            this.ActionInvoker.InvokeAction(this.ControllerContext, "Index");
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
        /// Sets the redirect URL query string.
        /// </summary>
        /// <param name="taxon">The taxon.</param>
        private void SetRedirectUrlQueryString(ITaxon taxon)
        {
            if (taxon == null || this.HttpContext == null)
            {
                return;
            }

            var taxonQueryStringParams = HyperLinkHelpers.BuildTaxonQueryStringParams(taxon, this.Model.UrlKeyPrefix);

            this.ViewBag.RedirectPageUrlTemplate = this.ViewBag.RedirectPageUrlTemplate + taxonQueryStringParams;
        }

        #endregion

        #region Private fields and constants

        internal const string WidgetIconCssClass = "sfNewsViewIcn sfMvcIcn";
        private INewsModel model;
        private MetadataModel metadata;
        private string listTemplateName = "NewsList";
        private string listTemplateNamePrefix = "List.";
        private string detailTemplateName = "DetailPage";
        private string detailTemplateNamePrefix = "Detail.";
        private bool openInSamePage = true;

        private bool? disableCanonicalUrlMetaTag;
        private const string WidgetName = "News_MVC";
        #endregion
    }
}
