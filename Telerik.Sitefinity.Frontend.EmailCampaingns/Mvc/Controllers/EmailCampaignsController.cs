using System;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using Telerik.Sitefinity.Frontend.EmailCampaigns.Mvc.Models;
using Telerik.Sitefinity.Frontend.EmailCampaigns.Mvc.StringResources;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers.Attributes;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Modules.Pages.Configuration;
using Telerik.Sitefinity.Mvc;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Web.UI;

namespace Telerik.Sitefinity.Frontend.EmailCampaigns.Mvc.Controllers
{
    /// <summary>
    /// This class represents the controller of the Email Campaigns widget.
    /// </summary>
    [ControllerToolboxItem(Name = "EmailCampaigns_MVC",
        Title = "Subscribe form",
        SectionName = ToolboxesConfig.NewslettersToolboxSectionName,
        CssClass = EmailCampaignsController.WidgetIconCssClass)]
    [Localization(typeof(EmailCampaignsResources))]
    public class EmailCampaignsController : Controller, ICustomWidgetVisualizationExtended
    {
        #region Properties
        /// <summary>
        /// Gets or sets the name of the template that widget will be displayed.
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
        /// Gets the Email Campaigns widget model.
        /// </summary>
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public virtual IEmailCampaignsModel Model
        {
            get
            {
                if (this.model == null)
                    this.model = ControllerModelFactory.GetModel<IEmailCampaignsModel>(this.GetType());

                return this.model;
            }
        }

        /// <summary>
        /// Gets the widget CSS class.
        /// </summary>
        /// <value>
        /// The widget CSS class.
        /// </value>
        [Browsable(false)]
        public string WidgetCssClass
        {
            get { return EmailCampaignsController.WidgetIconCssClass; }
        }

        /// <summary>
        /// Gets the empty link text.
        /// </summary>
        /// <value>
        /// The empty link text.
        /// </value>
        [Browsable(false)]
        public string EmptyLinkText
        {
            get { return this.GetResource("EmptyLinkText"); }
        }

        /// <summary>
        /// Gets a value indicating whether widget is empty.
        /// </summary>
        /// <value>
        ///   <c>true</c> if widget has no image selected; otherwise, <c>false</c>.
        /// </value>
        [Browsable(false)]
        public bool IsEmpty
        {
            get { return this.Model.SelectedMailingListId == Guid.Empty; }
        }
        #endregion

        #region Actions
        /// <summary>
        /// Default Action
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult Index()
        {
            var viewModel = this.Model.CreateViewModel();

            return this.View(this.TemplateName, viewModel);
        }

        /// <summary>
        /// Indexes the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Index(EmailCampaignsViewModel model)
        {
            if (ModelState.IsValid)
            {
                string error;
                bool isSucceeded = this.Model.AddSubscriber(model, out error);

                this.ViewBag.Error = error;
                this.ViewBag.IsSucceeded = isSucceeded;

                if (isSucceeded && this.Model.SuccessfullySubmittedForm == SuccessfullySubmittedForm.OpenSpecificPage)
                {
                    return this.Redirect(model.RedirectPageUrl);
                }
            }

            return this.View(this.TemplateName, model);
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
        /// Gets the resource.
        /// </summary>
        /// <typeparam name="T">The type of the T.</typeparam>
        /// <param name="resourceName">Name of the resource.</param>
        /// <returns></returns>
        protected virtual string GetResource(string resourceName)
        {
            return Res.Get(typeof(EmailCampaignsResources), resourceName);
        }

        #endregion

        #region Private fields and constants
        internal const string WidgetIconCssClass = "sfFormsIcn sfMvcIcn";
        private IEmailCampaignsModel model;
        private string templateName = "SubscribeForm";
        #endregion
    }
}
