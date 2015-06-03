using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using Telerik.Sitefinity.ContentLocations;
using Telerik.Sitefinity.Frontend.Media.Mvc.Models;
using Telerik.Sitefinity.Frontend.Media.Mvc.Models.ImageGallery;
using Telerik.Sitefinity.Frontend.Media.Mvc.StringResources;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers.Attributes;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Routing;
using Telerik.Sitefinity.Libraries.Model;
using Telerik.Sitefinity.Modules.Libraries;
using Telerik.Sitefinity.Modules.Pages.Configuration;
using Telerik.Sitefinity.Mvc;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Taxonomies.Model;
using Telerik.Sitefinity.Web;

namespace Telerik.Sitefinity.Frontend.Media.Mvc.Controllers
{
    /// <summary>
    /// This class represents controller for the Image Gallery widget.
    /// </summary>
    [Localization(typeof(ImageGalleryResources))]
    [ControllerToolboxItem(Name = "ImageGallery_MVC", Title = "Image gallery", SectionName = ToolboxesConfig.ContentToolboxSectionName, ModuleName = "Libraries", CssClass = ImageGalleryController.WidgetIconCssClass)]
    public class ImageGalleryController : Controller, IContentLocatableView, IRouteMapper
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
        /// Gets or sets the id of the page where will be displayed details view for selected item.
        /// </summary>
        /// <value>The details page id.</value>
        public Guid DetailsPageId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the canonical URL tag should be added to the page when the canonical meta tag should be added to the page.
        /// If the value is not set, the settings from SystemConfig -> ContentLocationsSettings -> DisableCanonicalURLs will be used. 
        /// </summary>
        /// <value>The disable canonical URLs.</value>
        [Browsable(false)]
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
        /// Gets or sets a value indicating whether detail view for image should be opened in the same page.
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
        /// Gets the Image gallery widget model.
        /// </summary>
        /// <value>
        /// The model.
        /// </value>
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public virtual IImageGalleryModel Model
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
            ITaxon taxonFilter = TaxonUrlEvaluator.GetTaxonFromQuery(this.HttpContext, this.Model.UrlKeyPrefix);

            this.InitializeListViewBag("/{0}");
            this.SetRedirectUrlQueryString(taxonFilter);

            var viewModel = this.Model.CreateListViewModel(taxonFilter: taxonFilter, page: page ?? 1);
            if (SystemManager.CurrentHttpContext != null)
                this.AddCacheDependencies(this.Model.GetKeysOfDependentObjects(viewModel));

            var fullTemplateName = this.FullListTemplateName();
            return this.View(fullTemplateName, viewModel);
        }

        /// <summary>
        /// Displays successors of the specified parent item.
        /// </summary>
        /// <param name="parentItem">The parent item.</param>
        /// <param name="page">The page.</param>
        /// <returns>
        /// The <see cref="ActionResult" />.
        /// </returns>
        public ActionResult Successors(Album parentItem, int? page)
        {
            if (parentItem != null)
                this.InitializeListViewBag(parentItem.ItemDefaultUrl + "?page={0}");

            var viewModel = this.Model.CreateListViewModelByParent(parentItem, page ?? 1);
            if (SystemManager.CurrentHttpContext != null)
                this.AddCacheDependencies(this.Model.GetKeysOfDependentObjects(viewModel));

            var fullTemplateName = this.FullListTemplateName();
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
                this.InitializeListViewBag("/" + taxonFilter.UrlName + "/{0}");

            var viewModel = this.Model.CreateListViewModel(taxonFilter, page ?? 1);
            if (SystemManager.CurrentHttpContext != null)
                this.AddCacheDependencies(this.Model.GetKeysOfDependentObjects(viewModel));

            var fullTemplateName = this.FullListTemplateName();
            return this.View(fullTemplateName, viewModel);
        }

        /// <summary>
        /// Renders appropriate list view depending on the <see cref="DetailTemplateName"/>
        /// </summary>
        /// <param name="item">The item which details will be displayed.</param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult Details(Image item)
        {
            var itemIndex = this.ParseToNullableInt32(this.GetQueryString("itemIndex"));
            var fullTemplateName = this.detailTemplateNamePrefix + this.DetailTemplateName;

            if (item != null)
                this.ViewBag.Title = item.Title;

            this.ViewBag.DetailsPageId = this.DetailsPageId;
            this.ViewBag.OpenInSamePage = this.OpenInSamePage;
            this.ViewBag.ItemIndex = itemIndex;

            var viewModel = this.Model.CreateDetailsViewModel(item, itemIndex);
            if (SystemManager.CurrentHttpContext != null)
                this.AddCacheDependencies(this.Model.GetKeysOfDependentObjects(viewModel));

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

        #region Overrides

        /// <summary>
        /// Called when a request matches this controller, but no method with the specified action name is found in the controller.
        /// </summary>
        /// <param name="actionName">The name of the attempted action.</param>
        protected override void HandleUnknownAction(string actionName)
        {
            this.Index(null).ExecuteResult(this.ControllerContext);
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
        /// Gets the item index from the query string.
        /// </summary>
        /// <returns></returns>
        protected virtual string GetQueryString(string key)
        {
            return Request.QueryString[key];
        }

        /// <summary>
        /// Tries to resolve parent filter mode.
        /// </summary>
        /// <param name="urlParams">The URL params.</param>
        /// <param name="requestContext">The request context.</param>
        /// <returns></returns>
        protected virtual bool TryResolveParentFilterMode(string[] urlParams, RequestContext requestContext, LibrariesManager manager = null)
        {
            var libraryManager = manager ?? LibrariesManager.GetManager(this.Model.ProviderName);

            string param = RouteHelper.GetUrlParameterString(urlParams);

            string redirectUrl;

            var item = libraryManager.GetItemFromUrl(typeof(Album), param, out redirectUrl);

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

        #region Private methods

        /// <summary>
        /// Initializes the model.
        /// </summary>
        /// <returns>
        /// The <see cref="IImageGalleryModel"/>.
        /// </returns>
        private IImageGalleryModel InitializeModel()
        {
            return ControllerModelFactory.GetModel<IImageGalleryModel>(this.GetType());
        }

        /// <summary>
        /// Initializes the ListView bag.
        /// </summary>
        /// <param name="redirectPageUrl">The redirect page URL.</param>
        private void InitializeListViewBag(string redirectPageUrl)
        {
            this.ViewBag.CurrentPageUrl = SystemManager.CurrentHttpContext != null ? this.GetCurrentPageUrl() : string.Empty;
            this.ViewBag.RedirectPageUrlTemplate = this.ViewBag.CurrentPageUrl + redirectPageUrl;
            this.ViewBag.DetailsPageId = this.DetailsPageId;
            this.ViewBag.OpenInSamePage = this.OpenInSamePage;
            this.ViewBag.ItemsPerPage = this.Model.ItemsPerPage;
        }

        /// <summary>
        /// Fulls the name of the list template.
        /// </summary>
        /// <returns></returns>
        private string FullListTemplateName()
        {
            return this.listTemplateNamePrefix + this.ListTemplateName;
        }

        /// <summary>
        /// Parses text to nullable int32.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns></returns>
        private int? ParseToNullableInt32(string text)
        {
            int i;
            if (Int32.TryParse(text, out i))
            {
                return i;
            }
            return null;
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

            this.ViewBag.RedirectPageUrlTemplate = this.ViewBag.RedirectPageUrlTemplate + this.HttpContext.Request.QueryString.ToQueryString();
        }
        #endregion

        #region Private fields and constants

        private const string WidgetIconCssClass = "sfImageLibraryViewIcn sfMvcIcn";
        internal const string AnyParentValue = "AnyParent";

        private IImageGalleryModel model;
        private string listTemplateName = "ImageGallery";
        private string listTemplateNamePrefix = "List.";
        private string detailTemplateName = "DetailPage";
        private string detailTemplateNamePrefix = "Detail.";
        private bool? disableCanonicalUrlMetaTag;
        private bool openInSamePage = true;

        #endregion
    }
}
