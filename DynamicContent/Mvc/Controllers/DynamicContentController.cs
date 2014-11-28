using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

using DynamicContent.Mvc.Models;
using DynamicContent.Mvc.StringResources;
using Telerik.Sitefinity.ContentLocations;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.DynamicModules;
using Telerik.Sitefinity.DynamicModules.Builder;
using Telerik.Sitefinity.DynamicModules.Builder.Web.UI;
using Telerik.Sitefinity.Frontend.Mvc.Helpers;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers.Attributes;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Taxonomies.Model;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.Web;

namespace DynamicContent.Mvc.Controllers
{
    /// <summary>
    /// This class represents a controller for Dynamic Content widget.
    /// </summary>
    [Localization(typeof(DynamicContentResources))]
    [ControllerMetadataAttribute(IsTemplatableControl = false)]
    public class DynamicContentController : Controller, ISelfRoutingController, IContentLocatableView, IDynamicContentWidget
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
                    this.listTemplateName = this.GetDynamicContentTypeDisplayName();

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
                    this.detailTemplateName = this.GetDynamicContentTypeDisplayName();

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
            if (this.Model.ParentFilterMode != ParentFilterMode.CurrentlyOpen || this.Model.ShowListViewOnEmpyParentFilter)
            {
                this.InitializeListViewBag("/{0}");

                var viewModel = this.Model.CreateListViewModel(taxonFilter: null, page: page ?? 1);
                if (SystemManager.CurrentHttpContext != null)
                    this.AddCacheDependencies(this.Model.GetKeysOfDependentObjects(viewModel));

                var fullTemplateName = this.listTemplateNamePrefix + this.ListTemplateName;
                return this.View(fullTemplateName, viewModel);
            }
            else
            {
                if (this.Model.CurrentlyOpenParentType != null && SitefinityContext.IsBackend)
                {
                    var manager = ModuleBuilderManager.GetManager().Provider;
                    var dynamicType = manager.GetDynamicModuleTypes().FirstOrDefault(t => t.TypeName == this.Model.ContentType.Name && t.TypeNamespace == this.Model.ContentType.Namespace);
                    if (dynamicType != null)
                    {
                        if (this.Model.CurrentlyOpenParentType != DynamicContentController.AnyParentValue)
                        {
                            var parentType = manager.GetDynamicModuleTypes().FirstOrDefault(t => t.TypeNamespace + "." + t.TypeName == this.Model.CurrentlyOpenParentType);
                            if (parentType != null)
                            {
                                return this.Content(Res.Get<DynamicContentResources>().DisplaysFromCurrentlyOpen.Arrange(dynamicType.DisplayName, PluralsResolver.Instance.ToPlural(parentType.DisplayName)));
                            }
                        }
                        else
                        {
                            return this.Content(Res.Get<DynamicContentResources>().DisplaysFromCurrentlyOpen.Arrange(dynamicType.DisplayName, Res.Get<DynamicContentResources>().AnyParentContentType));
                        }
                    }
                }

                return this.Content(string.Empty);
            }
        }

        /// <summary>
        /// Displays successors of the specified parent item.
        /// </summary>
        /// <param name="parentItem">The parent item.</param>
        /// <param name="page">The page.</param>
        /// <returns>
        /// The <see cref="ActionResult" />.
        /// </returns>
        public ActionResult Successors(Telerik.Sitefinity.DynamicModules.Model.DynamicContent parentItem, int? page)
        {
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
            this.InitializeListViewBag("/" + taxonFilter.UrlName + "/{0}");

            var viewModel = this.Model.CreateListViewModel(taxonFilter, page ?? 1);
            if (SystemManager.CurrentHttpContext != null)
                this.AddCacheDependencies(this.Model.GetKeysOfDependentObjects(viewModel));

            var fullTemplateName = this.FullListTemplateName();
            return this.View(fullTemplateName, viewModel);
        }

        /// <summary>
        /// Displays related items of the specified item.
        /// </summary>
        /// <param name="parentItem">The related item.</param>
        /// <param name="page">The page.</param>
        /// <returns>
        /// The <see cref="ActionResult" />.
        /// </returns>
        public ActionResult RelatedData(IDataItem relatedItem, int? page)
        {
            string itemUrl = relatedItem is ILocatableExtended ? (string)((ILocatableExtended)relatedItem).ItemDefaultUrl : "/" + ((ILocatable)relatedItem).UrlName;
            this.InitializeListViewBag(itemUrl + "?page={0}");

            var viewModel = this.Model.CreateListViewModelByRelatedItem(relatedItem, page ?? 1);
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
        public ActionResult Details(Telerik.Sitefinity.DynamicModules.Model.DynamicContent item)
        {
            if (!this.Model.ListMode)
            {
                var viewModel = this.Model.CreateDetailsViewModel(item);
                this.ViewBag.Title = ((IHasTitle)viewModel.Item).GetTitle();
                if (SystemManager.CurrentHttpContext != null)
                    this.AddCacheDependencies(this.Model.GetKeysOfDependentObjects(viewModel));

                var fullTemplateName = this.detailTemplateNamePrefix + this.DetailTemplateName;
                return this.View(fullTemplateName, viewModel);
            }
            else
            {
                this.InitializeListViewBag("/{0}");

                var viewModel = this.Model.CreateListViewModel(null, 1);
                ((DynamicContentListViewModel)viewModel).SelectedItem = (Telerik.Sitefinity.DynamicModules.Model.DynamicContent)DynamicModuleManager.GetManager().Lifecycle.GetLive(item);

                if (SystemManager.CurrentHttpContext != null)
                    this.AddCacheDependencies(this.Model.GetKeysOfDependentObjects(viewModel));

                var fullTemplateName = this.FullListTemplateName();
                return this.View(fullTemplateName, viewModel);
            }
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

        /// <inheritdoc />
        [NonAction]
        public bool TryMapRouteParameters(string[] urlParams, RequestContext requestContext)
        {
            if (urlParams == null)
                throw new ArgumentNullException("urlParams");

            if (requestContext == null)
                throw new ArgumentNullException("requestContext");

            if (urlParams.Length == 0)
                return false;

            var manager = DynamicModuleManager.GetManager(this.Model.ProviderName);

            if (this.Model.ParentFilterMode == ParentFilterMode.CurrentlyOpen && !this.Model.CurrentlyOpenParentType.IsNullOrEmpty())
            {
                if (this.Model.CurrentlyOpenParentType != DynamicContentController.AnyParentValue)
                {
                    var parentType = TypeResolutionService.ResolveType(this.Model.CurrentlyOpenParentType, throwOnError: false);
                    if (parentType == null)
                        return false;

                    return this.TryMapSuccessorsRouteData(urlParams, requestContext, manager, parentType);
                }
                else
                {
                    var dynamicType = this.GetDynamicContentType();
                    var currentParentType = dynamicType != null ? dynamicType.ParentModuleType : null;

                    bool isParentResolved = false;
                    while (currentParentType != null && isParentResolved == false)
                    {
                        var parentType = TypeResolutionService.ResolveType(currentParentType.GetFullTypeName(), throwOnError: false);
                        if (parentType != null)
                            isParentResolved = this.TryMapSuccessorsRouteData(urlParams, requestContext, manager, parentType);

                        currentParentType = currentParentType.ParentModuleType;
                    }

                    return isParentResolved;
                }
            }

            if (!this.Model.RelatedItemType.IsNullOrEmpty() && !this.Model.RelatedFieldName.IsNullOrEmpty())
            {
                return this.TryMapRelatedDataRouteData(urlParams, requestContext);
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

        /// <summary>
        /// Gets the display name of the <see cref="DynamicModuleType"/>
        /// </summary>
        /// <returns><see cref="DynamicModuleType"/>'s display name</returns>
        private string GetDynamicContentTypeDisplayName()
        {
            var widgetName = this.ViewBag.WidgetName as string;
            if (widgetName.IsNullOrEmpty())
                return null;

            var dynamicType = ControllerExtensions.GetDynamicContentType(widgetName);
            if (dynamicType != null)
                return dynamicType.DisplayName;

            return null;
        }

        private bool TryMapSuccessorsRouteData(string[] urlParams, RequestContext requestContext, DynamicModuleManager manager, Type parentType)
        {
            string redirectUrl;
            var item = manager.Provider.GetItemFromUrl(parentType, RouteHelper.GetUrlParameterString(urlParams), out redirectUrl);
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
                this.TryMapSuccessorsRouteData(urlParams.Take(urlParams.Length - 1).ToArray(), requestContext, manager, parentType);
            }

            return false;
        }

        private void InitializeListViewBag(string redirectPageUrl)
        {
            this.ViewBag.CurrentPageUrl = this.GetCurrentPageUrl();
            this.ViewBag.RedirectPageUrlTemplate = this.ViewBag.CurrentPageUrl + redirectPageUrl;
            this.ViewBag.DetailsPageId = this.DetailsPageId;
            this.ViewBag.OpenInSamePage = this.OpenInSamePage;
        }

        private bool TryMapRelatedDataRouteData(string[] urlParams, RequestContext requestContext)
        {
            var manager = ManagerBase.GetMappedManager(this.Model.RelatedItemType, this.Model.RelatedItemProviderName);

            if (this.Model.RelatedItemProviderName.IsNullOrEmpty())
                this.Model.RelatedItemProviderName = manager.Provider.Name;

            var urlProvider = manager.Provider as IUrlProvider;
            if (urlProvider == null)
                return false;

            var relatedItemType = TypeResolutionService.ResolveType(this.Model.RelatedItemType, throwOnError: false);
            if (relatedItemType == null)
                return false;

            string redirectUrl;
            var item = urlProvider.GetItemFromUrl(relatedItemType, RouteHelper.GetUrlParameterString(urlParams), out redirectUrl);
            if (item == null)
                return false;

            requestContext.RouteData.Values["action"] = "RelatedData";
            requestContext.RouteData.Values["relatedItem"] = item;
            if (this.Request["page"] != null)
                requestContext.RouteData.Values["page"] = int.Parse(this.Request["page"]);

            return true;
        }

        private string FullListTemplateName()
        {
            return this.listTemplateNamePrefix + this.ListTemplateName;
        }

        #endregion

        #region Private fields and constants

        internal const string AnyParentValue = "AnyParent";

        private IDynamicContentModel model;
        private string listTemplateName;
        private string listTemplateNamePrefix = "List.";
        private string detailTemplateName;
        private string detailTemplateNamePrefix = "Detail.";
        private bool openInSamePage = true;

        #endregion
    }
}
