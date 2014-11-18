using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

using DynamicContent.Mvc.Models;
using DynamicContent.Mvc.StringResources;
using Telerik.Sitefinity.ContentLocations;
using Telerik.Sitefinity.DynamicModules.Model;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers.Attributes;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Taxonomies.Model;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.Frontend.Mvc;
using Telerik.Sitefinity.DynamicModules;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.DynamicModules.Builder;
using Telerik.Sitefinity.DynamicModules.Builder.Web.UI;
using Telerik.Sitefinity.Frontend.Mvc.Helpers;

namespace DynamicContent.Mvc.Controllers
{
    /// <summary>
    /// This class represents a controller for Dynamic Content widget.
    /// </summary>
    [Localization(typeof(DynamicContentResources))]
    [ControllerMetadataAttribute(IsTemplatableControl = false)]
    public class DynamicContentController : Controller, ISelfRoutingController, IContentLocatableView
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
                if (this.listTemplateName == null)
                    this.listTemplateName = this.Model.ContentType != null ? this.Model.ContentType.Name : null;

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
                if (this.detailTemplateName == null)
                    this.detailTemplateName = this.Model.ContentType != null ? this.Model.ContentType.Name : null;

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
        [Browsable(false)]
        public bool? DisableCanonicalUrlMetaTag
        {
            get
            {
                return this.Model.DisableCanonicalUrlMetaTag;
            }

            set
            {
                this.Model.DisableCanonicalUrlMetaTag = value;
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
        /// Gets or sets the id of the page where will be displayed details view for selected item.
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
        public IDynamicContentModel Model
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
            if (this.Model.ParentFilterMode != ParentFilterMode.CurrentlyOpen)
            {
                var fullTemplateName = this.listTemplateNamePrefix + this.ListTemplateName;
                this.ViewBag.RedirectPageUrlTemplate = "/{0}";
                this.ViewBag.DetailsPageId = this.DetailsPageId;
                this.ViewBag.OpenInSamePage = this.OpenInSamePage;
                this.ViewBag.CurrentPageUrl = this.GetCurrentPageUrl();

                var viewModel = this.Model.CreateListViewModel(taxonFilter: null, page: page ?? 1);
                if (SystemManager.CurrentHttpContext != null)
                    this.AddCacheDependencies(this.Model.GetKeysOfDependentObjects(viewModel));

                return this.View(fullTemplateName, viewModel);
            }
            else
            {
                if (this.Model.CurrentlyOpenParentType != null && SitefinityContext.IsBackend)
                {
                    var manager = ModuleBuilderManager.GetManager().Provider;
                    var dynamicType = manager.GetDynamicModuleTypes().FirstOrDefault(t => t.TypeName == this.Model.ContentType.Name && t.TypeNamespace == this.Model.ContentType.Namespace);
                    var parentType = manager.GetDynamicModuleTypes().FirstOrDefault(t => t.TypeNamespace + "." + t.TypeName == this.Model.CurrentlyOpenParentType);
                    if (dynamicType != null && parentType != null)
                    {
                        return this.Content(Res.Get<DynamicContentResources>().DisplaysFromCurrentlyOpen.Arrange(dynamicType.DisplayName, PluralsResolver.Instance.ToPlural(parentType.DisplayName)));
                    }
                }

                return this.Content(string.Empty);
            }
        }

        /// <summary>
        /// Displays successors of the specified parent item.
        /// </summary>
        /// <param name="parentItem">The parent item.</param>
        /// <returns>
        /// The <see cref="ActionResult" />.
        /// </returns>
        public ActionResult Successors(Telerik.Sitefinity.DynamicModules.Model.DynamicContent parentItem)
        {
            var fullTemplateName = this.listTemplateNamePrefix + this.ListTemplateName;
            this.ViewBag.RedirectPageUrlTemplate = "/{0}";
            this.ViewBag.DetailsPageId = this.DetailsPageId;
            this.ViewBag.OpenInSamePage = this.OpenInSamePage;
            this.ViewBag.CurrentPageUrl = this.GetCurrentPageUrl();

            var viewModel = this.Model.CreateListViewModel(parentItem, 1);
            if (SystemManager.CurrentHttpContext != null)
                this.AddCacheDependencies(this.Model.GetKeysOfDependentObjects(viewModel));

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
            this.ViewBag.RedirectPageUrlTemplate = "/" + taxonFilter.UrlName + "/{0}";
            this.ViewBag.DetailsPageId = this.DetailsPageId;
            this.ViewBag.OpenInSamePage = this.OpenInSamePage;
            this.ViewBag.CurrentPageUrl = this.GetCurrentPageUrl();

            var viewModel = this.Model.CreateListViewModel(taxonFilter, page ?? 1);
            if (SystemManager.CurrentHttpContext != null)
                this.AddCacheDependencies(this.Model.GetKeysOfDependentObjects(viewModel));

            return this.View(fullTemplateName, viewModel);
        }

        /// <summary>
        /// Renders appropriate list view depending on the <see cref="DetailTemplateName"/>
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult Details(Telerik.Sitefinity.DynamicModules.Model.DynamicContent item)
        {
            var fullTemplateName = this.detailTemplateNamePrefix + this.DetailTemplateName;

            var viewModel = this.Model.CreateDetailsViewModel(item);
            this.ViewBag.Title = ((IHasTitle)viewModel.Item).GetTitle();
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
        public IEnumerable<IContentLocationInfo> GetLocations()
        {
            // The IControlBehaviorResolver can set WidgetName. This information is persisted in the MVC proxy control.
            if (this.ViewBag.WidgetName as string != null)
            {
                var dynamicType = this.GetDynamicContentType((string)this.ViewBag.WidgetName);
                if (dynamicType != null)
                {
                    this.Model.ContentType = TypeResolutionService.ResolveType(dynamicType.GetFullTypeName());
                }
            }

            return this.Model.GetLocations();
        }

        #endregion

        #region ISelfRoutingController

        [NonAction]
        public bool TryMapRouteParameters(string[] urlParams, RequestContext requestContext)
        {
            if (urlParams == null)
                throw new ArgumentNullException("urlParams");

            if (requestContext == null)
                throw new ArgumentNullException("requestContext");

            if (urlParams.Length == 0)
                return false;

            if (this.Model.ParentFilterMode == ParentFilterMode.CurrentlyOpen && !this.Model.CurrentlyOpenParentType.IsNullOrEmpty())
            {
                var dynamicType = this.GetDynamicContentType();
                var manager = DynamicModuleManager.GetManager(this.Model.ProviderName);
                var parentType = TypeResolutionService.ResolveType(this.Model.CurrentlyOpenParentType, throwOnError: false);
                if (parentType == null)
                    return false;

                return this.TryMapRouteParametersInternal(urlParams, requestContext, manager, parentType);
            }

            return false;
        }

        #endregion

        #region Overrides

        /// <summary>
        /// Called before the action method is invoked.
        /// </summary>
        /// <param name="filterContext">Information about the current request and action.</param>
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var dynamicType = this.GetDynamicContentType();
            if (dynamicType != null)
            {
                this.Model.ContentType = TypeResolutionService.ResolveType(dynamicType.GetFullTypeName());
            }
            else
            {
                var errorMessage = string.Empty;
                if (SystemManager.IsDesignMode || SystemManager.IsPreviewMode)
                { 
                    errorMessage = Res.Get<DynamicContentResources>().DeletedModuleWarning;
                }

                filterContext.Result = new ContentResult()
                {
                    Content = errorMessage
                };
            }
        }

        /// <summary>
        /// Called when a request matches this controller, but no method with the specified action name is found in the controller.
        /// </summary>
        /// <param name="actionName">The name of the attempted action.</param>
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
        /// The <see cref="IDynamicContentModel"/>.
        /// </returns>
        private IDynamicContentModel InitializeModel()
        {
            return ControllerModelFactory.GetModel<IDynamicContentModel>(this.GetType());
        }

        private bool TryMapRouteParametersInternal(string[] urlParams, RequestContext requestContext, DynamicModuleManager manager, Type parentType)
        {
            string redirectUrl;
            var item = manager.Provider.GetItemFromUrl(parentType, RouteHelper.GetUrlParameterString(urlParams), out redirectUrl);
            if (item != null)
            {
                requestContext.RouteData.Values["action"] = "Successors";
                requestContext.RouteData.Values["parentItem"] = item;
                return true;
            }

            if (urlParams.Length > 1)
            {
                this.TryMapRouteParametersInternal(urlParams.Take(urlParams.Length - 1).ToArray(), requestContext, manager, parentType);
            }

            return false;
        }

        #endregion

        #region Private fields and constants

        private IDynamicContentModel model;
        private string listTemplateName;
        private string listTemplateNamePrefix = "List.";
        private string detailTemplateName;
        private string detailTemplateNamePrefix = "Detail.";
        private bool openInSamePage = true;

        private bool? disableCanonicalUrlMetaTag;
        private string detailsPageUrl;

        #endregion
    }
}
