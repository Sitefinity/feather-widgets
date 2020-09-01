﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using Telerik.Sitefinity.Blogs.Model;
using Telerik.Sitefinity.ContentLocations;
using Telerik.Sitefinity.Frontend.Blogs.Mvc.Models.BlogPost;
using Telerik.Sitefinity.Frontend.Blogs.Mvc.StringResources;
using Telerik.Sitefinity.Frontend.Mvc.Helpers;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers.Attributes;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Routing;
using Telerik.Sitefinity.Modules.Blogs;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Modules.Pages.Configuration;
using Telerik.Sitefinity.Mvc;
using Telerik.Sitefinity.Personalization;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Taxonomies.Model;
using Telerik.Sitefinity.Web;

namespace Telerik.Sitefinity.Frontend.Blogs.Mvc.Controllers
{
    /// <summary>
    /// This class represents the controller of the Blog post widget.
    /// </summary>
    [Localization(typeof(BlogPostResources))]
    [ControllerToolboxItem(
        Name = BlogPostController.WidgetName,
        Title = nameof(BlogPostResources.BlogPostsViewTitle),
        Description = nameof(BlogPostResources.BlogPostsViewDescription),
        SectionName = ToolboxesConfig.ContentToolboxSectionName,
        ModuleName = "Blogs",
        ResourceClassId = nameof(BlogPostResources),
        CssClass = BlogPostController.WidgetIconCssClass)]
    public class BlogPostController : ContentBaseController, IContentLocatableView, IRouteMapper, IPersonalizable, ICanFilterByParent
    {
        #region Properties

        /// <summary>
        /// Gets the Blog posts widget model.
        /// </summary>
        /// <value>
        /// The model.
        /// </value>
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public virtual IBlogPostModel Model
        {
            get
            {
                if (this.model == null)
                    this.model = ControllerModelFactory.GetModel<IBlogPostModel>(this.GetType());

                return this.model;
            }
        }

        /// <summary>
        /// Gets or sets the name of the template that will be displayed when widget is in List view.
        /// </summary>
        /// <value>
        /// The name of the list template.
        /// </value>
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
        /// <value>
        /// The name of the details template.
        /// </value>
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
        /// Gets or sets a value indicating whether detail view for the blog post should be opened in the same page.
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
        /// Gets or sets the id of the page where will be displayed details view for selected item.
        /// </summary>
        /// <value>
        /// The details page id.
        /// </value>
        public Guid DetailsPageId { get; set; }

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
            if (this.Model.ParentFilterMode != ParentFilterMode.CurrentlyOpen)
            {
                ITaxon taxonFilter = TaxonUrlEvaluator.GetTaxonFromQuery(this.HttpContext, this.Model.UrlKeyPrefix);

                this.InitializeListViewBag();
                this.SetRedirectUrlQueryString(taxonFilter);

                this.UpdatePageFromQuery(ref page, this.Model.UrlKeyPrefix);
                var viewModel = this.Model.CreateListViewModel(taxonFilter, this.ExtractValidPage(page));

                var fullTemplateName = this.listTemplateNamePrefix + this.ListTemplateName;

                if (this.ShouldReturnDetails(this.Model.ContentViewDisplayMode, viewModel))
                    return this.Details((BlogPost)viewModel.Items.First().DataItem);

                this.AddCacheDependencies(this.Model.GetKeysOfDependentObjects(viewModel));
                if (viewModel.ContentType != null)
                    this.AddCacheVariations(viewModel.ContentType, viewModel.ProviderName);

                return this.View(fullTemplateName, viewModel);
            }

            return this.Content(string.Empty);
        }

        /// <summary>
        /// Displays successors of the specified parent item.
        /// </summary>
        /// <param name="parentItem">The parent item.</param>
        /// <param name="page">The page.</param>
        /// <returns>
        /// The <see cref="ActionResult" />.
        /// </returns>
        public ActionResult Successors(Blog parentItem, int? page)
        {
            if (parentItem != null)
                this.InitializeListViewBag(parentItem.ItemDefaultUrl + "?page={0}");

            var viewModel = this.Model.CreateListViewModelByParent(parentItem, page ?? 1);

            if (SystemManager.CurrentHttpContext != null)
                this.AddCacheDependencies(this.Model.GetKeysOfDependentObjects(viewModel));

            var fullTemplateName = this.listTemplateNamePrefix + this.ListTemplateName;
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
            if (taxonFilter != null)
            {
                var redirectPageUrlTemplate = UrlHelpers.GetRedirectPagingUrl(taxonFilter);
                this.InitializeListViewBag(redirectPageUrlTemplate);
            }

            var viewModel = this.Model.CreateListViewModel(taxonFilter, page ?? 1);

            if (SystemManager.CurrentHttpContext != null)
            {
                this.AddCacheDependencies(this.Model.GetKeysOfDependentObjects(viewModel));
                if (viewModel.ContentType != null)
                    this.AddCacheVariations(viewModel.ContentType, viewModel.ProviderName);
            }

            var fullTemplateName = this.listTemplateNamePrefix + this.ListTemplateName;
            return this.View(fullTemplateName, viewModel);
        }

        /// <summary>
        /// Renders appropriate list view depending on the <see cref="ListTemplateName" />
        /// </summary>
        /// <param name="from">The start date from the date filter.</param>
        /// <param name="to">The end date from the date filter.</param>
        /// <param name="page">The page.</param>
        /// <returns>
        /// The <see cref="ActionResult" />.
        /// </returns>
        public ActionResult ListByDate(DateTime from, DateTime to, int? page)
        {
            int indexOfPrefix = this.HttpContext.Request.Url.AbsolutePath.IndexOf("/archive/");
            string urlPath = this.HttpContext.Request.Url.AbsolutePath.Substring(indexOfPrefix);

            this.InitializeListViewBag(urlPath + "?page={0}");

            var viewModel = this.Model.CreateListViewModelByDate(from, to, page ?? 1);

            if (SystemManager.CurrentHttpContext != null)
            {
                this.AddCacheDependencies(this.Model.GetKeysOfDependentObjects(viewModel));
                if (viewModel.ContentType != null)
                    this.AddCacheVariations(viewModel.ContentType, viewModel.ProviderName);
            }

            var fullTemplateName = this.listTemplateNamePrefix + this.ListTemplateName;
            return this.View(fullTemplateName, viewModel);
        }

        /// <summary>
        /// Renders appropriate list view depending on the <see cref="DetailTemplateName"/>
        /// </summary>
        /// <param name="item">The item which details will be displayed.</param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult Details(BlogPost item)
        {
            this.InitializeMetadataDetailsViewBag(item);

            var fullTemplateName = this.detailTemplateNamePrefix + this.DetailTemplateName;

            if (item != null)
                this.ViewBag.Title = item.Title;

            this.ViewBag.DetailsPageId = this.DetailsPageId;
            this.ViewBag.OpenInSamePage = this.OpenInSamePage;

            var viewModel = this.Model.CreateDetailsViewModel(item);
            if (SystemManager.CurrentHttpContext != null)
                this.AddCacheDependencies(this.Model.GetKeysOfDependentObjects(viewModel));

            var page = this.HttpContext.CurrentHandler.GetPageHandler();
            this.AddCanonicalUrlTagIfEnabled(page, item);

            return this.View(fullTemplateName, viewModel);
        }

        /// <summary>
        /// Gets the parent types.
        /// </summary>
        /// <returns>Collection of parent types to filter by.</returns>
        [NonAction]
        public IEnumerable<Type> GetParentTypes()
        {
            return new[] { typeof(Blog) };
        }

        #endregion

        #region Overrides

        /// <summary>
        /// Called when a request matches this controller, but no method with the specified action name is found in the controller.
        /// </summary>
        /// <param name="actionName">The name of the attempted action.</param>
        protected override void HandleUnknownAction(string actionName)
        {
            this.ActionInvoker.InvokeAction(this.ControllerContext, "Index");
        }

        /// <summary>
        /// Maps the route parameters from URL and returns true if the URL is a valid route.
        /// </summary>
        /// <param name="urlParams">The URL parameters.</param>
        /// <param name="requestContext">The request context.</param>
        /// <returns>True if the URL is a valid route. False otherwise.</returns>
        [NonAction]
        public bool TryMapRouteParameters(string[] urlParams, RequestContext requestContext)
        {
            if (urlParams == null)
                throw new ArgumentNullException("urlParams");

            if (requestContext == null)
                throw new ArgumentNullException("requestContext");

            if (urlParams.Length == 0)
                return false;

            if (this.Model.ParentFilterMode == ParentFilterMode.CurrentlyOpen)
            {
                return this.TryResolveParentFilterMode(urlParams, requestContext);
            }

            return false;
        }

        /// <summary>
        /// Tries to resolve parent filter mode.
        /// </summary>
        /// <param name="urlParams">The URL params.</param>
        /// <param name="requestContext">The request context.</param>
        /// <returns></returns>
        protected virtual bool TryResolveParentFilterMode(string[] urlParams, RequestContext requestContext, BlogsManager manager = null)
        {
            var blogsManager = manager ?? BlogsManager.GetManager(this.Model.ProviderName);

            string param = RouteHelper.GetUrlParameterString(urlParams);

            string redirectUrl;

            var item = blogsManager.GetItemFromUrl(typeof(Blog), param, out redirectUrl);

            if (item != null)
            {
                requestContext.RouteData.Values["action"] = "Successors";
                requestContext.RouteData.Values["parentItem"] = item;

                if (this.Request["page"] != null)
                    requestContext.RouteData.Values["page"] = int.Parse(this.Request["page"]);

                return true;
            }

            if (urlParams.Length > 1)
            {
                this.TryResolveParentFilterMode(urlParams.Take(urlParams.Length - 1).ToArray(), requestContext, manager);
            }

            return false;
        }

        #endregion

        #region IContentLocatableView

        /// <summary>
        /// Gets or sets a value indicating whether the canonical URL tag should be added to the page when the canonical meta tag should be added to the page.
        /// If the value is not set, the settings from SystemConfig -> ContentLocationsSettings -> DisableCanonicalURLs will be used. 
        /// </summary>
        /// <value>
        /// The disable canonical URLs.
        /// </value>
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

        #region Private Methods

        /// <summary>
        /// Initializes the ListView bag.
        /// </summary>
        /// <param name="redirectPageUrl">The redirect page URL.</param>
        private void InitializeListViewBag(string redirectPageUrl = null)
        {
            var pageUrl = this.GetCurrentPageUrl();
            var template = redirectPageUrl != null ? string.Concat(pageUrl, redirectPageUrl) :
                                                     this.GeneratePagingTemplate(pageUrl, this.Model.UrlKeyPrefix);

            this.ViewBag.CurrentPageUrl = pageUrl;
            this.ViewBag.RedirectPageUrlTemplate = template;
            this.ViewBag.DetailsPageId = this.DetailsPageId;
            this.ViewBag.OpenInSamePage = this.OpenInSamePage;
            this.ViewBag.ItemsPerPage = this.Model.ItemsPerPage;
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

        internal const string WidgetIconCssClass = "sfBlogsViewIcn sfMvcIcn";

        private IBlogPostModel model;
        private MetadataModel metadata;

        private string listTemplateName = "BlogPostList";
        private string listTemplateNamePrefix = "List.";
        private string detailTemplateName = "DetailPage";
        private string detailTemplateNamePrefix = "Detail.";

        private bool? disableCanonicalUrlMetaTag;
        private bool openInSamePage = true;
        private const string WidgetName = "BlogPost_MVC";

        #endregion
    }
}
