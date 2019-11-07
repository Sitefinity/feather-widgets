using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using Telerik.Sitefinity.ContentLocations;
using Telerik.Sitefinity.Events.Model;
using Telerik.Sitefinity.Frontend.Events.Mvc.Models;
using Telerik.Sitefinity.Frontend.Events.Mvc.StringResources;
using Telerik.Sitefinity.Frontend.Mvc.Helpers;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers.Attributes;
using Telerik.Sitefinity.Modules.Pages.Configuration;
using Telerik.Sitefinity.Mvc;
using Telerik.Sitefinity.Personalization;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Taxonomies.Model;

namespace Telerik.Sitefinity.Frontend.Events.Mvc.Controllers
{
    /// <summary>
    /// This class represents the controller of the Events widget.
    /// </summary>
    [ControllerToolboxItem(
        Name = EventController.WidgetName, 
        Title = nameof(EventResources.EventsViewTitle), 
        Description = nameof(EventResources.EventsViewDescription),
        ResourceClassId = nameof(EventResources),
        SectionName = ToolboxesConfig.ContentToolboxSectionName, 
        ModuleName = "Events", 
        CssClass = EventController.WidgetIconCssClass)]
    [Localization(typeof(EventResources))]
    public class EventController : ContentBaseController, IContentLocatableView, IPersonalizable
    {
        #region Properties

        /// <summary>
        /// Gets the Events widget model.
        /// </summary>
        /// <value>
        /// The model.
        /// </value>
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public virtual IEventModel Model
        {
            get
            {
                if (this.model == null)
                    this.model = ControllerModelFactory.GetModel<IEventModel>(this.GetType());

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
        /// Gets or sets a value indicating whether detail view for the event should be opened in the same page.
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

        #endregion

        /// <summary>
        /// Renders appropriate list view depending on the <see cref="ListTemplateName" />
        /// </summary>
        /// <param name="page">The page.</param>
        /// <returns>
        /// The <see cref="ActionResult" />.
        /// </returns>
        public ActionResult Index(int? page)
        {
            this.UpdatePageFromQuery(ref page, this.Model.UrlKeyPrefix);
            var viewModel = this.Model.CreateListViewModel(null, this.ExtractValidPage(page));

            this.InitializeListViewBag();

            var fullTemplateName = EventController.ListTemplateNamePrefix + this.ListTemplateName;

            if (this.ShouldReturnDetails(this.Model.ContentViewDisplayMode, viewModel))
                return this.Details((Event)viewModel.Items.First().DataItem);

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
            var fullTemplateName = EventController.ListTemplateNamePrefix + this.ListTemplateName;
            var redirectPageUrlTemplate = UrlHelpers.GetRedirectPagingUrl(taxonFilter);
            this.InitializeListViewBag(redirectPageUrlTemplate);

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
        /// Renders appropriate details view depending on the <see cref="DetailTemplateName"/>
        /// </summary>
        /// <param name="item">The item which details will be displayed.</param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult Details(Event item)
        {
            this.InitializeMetadataDetailsViewBag(item);

            var viewModel = this.Model.CreateDetailsViewModel(item);

            this.InitializeDetailsViewBag(item);

            if (SystemManager.CurrentHttpContext != null)
                this.AddCacheDependencies(this.Model.GetKeysOfDependentObjects(viewModel));

            var page = this.HttpContext.CurrentHandler.GetPageHandler();
            this.AddCanonicalUrlTagIfEnabled(page, item);

            var fullTemplateName = EventController.DetailTemplateNamePrefix + this.DetailTemplateName;
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

        /// <summary>
        /// Called when a request matches this controller, but no method with the specified action name is found in the controller.
        /// </summary>
        /// <param name="actionName">The name of the attempted action.</param>
        protected override void HandleUnknownAction(string actionName)
        {
            this.ActionInvoker.InvokeAction(this.ControllerContext, "Index");
        }

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
            this.ViewBag.ItemsPerPage = this.Model.ItemsPerPage;
            this.ViewBag.AllowCalendarExport = this.Model.AllowCalendarExport;
            this.ViewBag.OpenInSamePage = this.OpenInSamePage;
            this.ViewBag.DetailsPageId = this.DetailsPageId;
        }

        /// <summary>
        /// Initializes the DetailsView bag.
        /// </summary>
        /// <param name="item">The event.</param>
        private void InitializeDetailsViewBag(Event item)
        {
            if (item != null)
                this.ViewBag.Title = item.Title;

            this.ViewBag.AllowCalendarExport = this.Model.AllowCalendarExport;
        }

        private const string WidgetIconCssClass = "sfEventsViewIcn sfMvcIcn";
        private const string ListTemplateNamePrefix = "List.";
        private const string DetailTemplateNamePrefix = "Detail.";

        private IEventModel model;
        private bool openInSamePage = true;

        private string listTemplateName = "EventsList";
        private string detailTemplateName = "EventDetails";
        private bool? disableCanonicalUrlMetaTag;
        private const string WidgetName = "Events_MVC";
    }
}