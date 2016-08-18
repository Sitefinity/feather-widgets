using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Web.Mvc;
using Telerik.Sitefinity.ContentLocations;
using Telerik.Sitefinity.Events.Model;
using Telerik.Sitefinity.Frontend.Events.Mvc.Models.EventScheduler;
using Telerik.Sitefinity.Frontend.Events.Mvc.StringResources;
using Telerik.Sitefinity.Frontend.Mvc.Helpers;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers.Attributes;
using Telerik.Sitefinity.Modules.Pages.Configuration;
using Telerik.Sitefinity.Mvc;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Taxonomies.Model;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Web.DataResolving;

namespace Telerik.Sitefinity.Frontend.Events.Mvc.Controllers
{
    /// <summary>
    /// This class represents the controller of the Events widget.
    /// </summary>
    [ControllerToolboxItem(Name = "Calendar_MVC", Title = "Calendar", SectionName = ToolboxesConfig.ContentToolboxSectionName, ModuleName = "Events", CssClass = EventSchedulerController.WidgetIconCssClass)]
    [Localization(typeof(EventResources))]
    public class EventSchedulerController : Controller, IContentLocatableView
    {
        #region Properties

        /// <summary>
        /// Gets the Events Scheduler widget model.
        /// </summary>
        /// <value>
        /// The model.
        /// </value>
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public virtual IEventSchedulerModel Model
        {
            get
            {
                if (this.model == null)
                    this.model = ControllerModelFactory.GetModel<IEventSchedulerModel>(this.GetType());

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
        /// <returns>
        /// The <see cref="ActionResult" />.
        /// </returns>
        public ActionResult Index()
        {
            this.InitializeListViewBag("/{0}");

            var fullTemplateName = EventSchedulerController.ListTemplateNamePrefix + this.ListTemplateName;
            return this.View(fullTemplateName);
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
            var viewModel = this.Model.CreateDetailsViewModel(item);

            this.InitializeDetailsViewBag(item);

            if (SystemManager.CurrentHttpContext != null)
                this.AddCacheDependencies(this.Model.GetKeysOfDependentObjects(viewModel));

            var fullTemplateName = EventSchedulerController.DetailTemplateNamePrefix + this.DetailTemplateName;
            return this.View(fullTemplateName, viewModel);
        }

        /// <summary>
        /// Gets the scheduler events.
        /// </summary>
        /// <returns>Scheduler events json</returns>
        [Route("web-interface/events/")]
        public ActionResult GetSchedulerEvents(EventSchedulerModel model)
        {
            var events = model.GetSchedulerEvents();

            JsonResult json = new JsonResult()
            {
                Data = events,
                JsonRequestBehavior = System.Web.Mvc.JsonRequestBehavior.AllowGet,
                MaxJsonLength = int.MaxValue
            };

            return json;
        }

        /// <summary>
        /// Gets the calendars.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>Calendars json</returns>
        [Route("web-interface/calendars/")]
        public ActionResult GetCalendars(EventSchedulerModel model)
        {
            var calendars = model.GetCalendars();

            JsonResult json = new JsonResult()
            {
                Data = calendars,
                JsonRequestBehavior = System.Web.Mvc.JsonRequestBehavior.AllowGet,
                MaxJsonLength = int.MaxValue
            };

            return json;
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
            this.Index().ExecuteResult(this.ControllerContext);
        }

        /// <summary>
        /// Initializes the ListView bag.
        /// </summary>
        /// <param name="redirectPageUrl">The redirect page URL.</param>
        protected virtual void InitializeListViewBag(string redirectPageUrl)
        {
            this.ViewBag.CurrentPageUrl = SystemManager.CurrentHttpContext != null ? this.GetCurrentPageUrl() : string.Empty;
            this.ViewBag.RedirectPageUrlTemplate = this.ViewBag.CurrentPageUrl + redirectPageUrl;
            this.ViewBag.ItemsPerPage = this.Model.ItemsPerPage;
            this.ViewBag.AllowCalendarExport = this.Model.AllowCalendarExport;
            this.ViewBag.OpenInSamePage = this.OpenInSamePage;
            this.ViewBag.DetailsPageId = this.DetailsPageId;
            this.ViewBag.DetailsPageUrl = this.GetDetailsPageUrl();
            this.ViewBag.UiCulture = SystemManager.CurrentContext.AppSettings.Multilingual ? CultureInfo.CurrentUICulture.ToString() : string.Empty;
            this.ViewBag.TimeZoneOffset = Telerik.Sitefinity.Security.UserManager.GetManager().GetUserTimeZone().BaseUtcOffset.TotalMilliseconds.ToString();
            this.ViewBag.TimeZoneId = Telerik.Sitefinity.Security.UserManager.GetManager().GetUserTimeZone().Id;
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

        /// <summary>
        /// Get details page url
        /// </summary>
        /// <returns>Url</returns>
        private string GetDetailsPageUrl()
        {
            string currentPageUrl = SystemManager.CurrentHttpContext != null ? this.GetCurrentPageUrl() : string.Empty;
            if (this.DetailsPageId != Guid.Empty)
            {
                currentPageUrl = HyperLinkHelpers.GetFullPageUrl(this.DetailsPageId);
            }

            return currentPageUrl;
        }

        private const string WidgetIconCssClass = "sfEventsViewIcn sfMvcIcn";
        private const string ListTemplateNamePrefix = "List.";
        private const string DetailTemplateNamePrefix = "Detail.";

        private IEventSchedulerModel model;
        private bool openInSamePage = true;

        private string listTemplateName = "Calendar";
        private string detailTemplateName = "EventDetails";
        private bool? disableCanonicalUrlMetaTag;
    }
}