using System;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using Telerik.Sitefinity.Frontend.EmailCampaigns.Mvc.Helpers;
using Telerik.Sitefinity.Frontend.EmailCampaigns.Mvc.Models;
using Telerik.Sitefinity.Frontend.EmailCampaigns.Mvc.StringResources;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers.Attributes;
using Telerik.Sitefinity.Licensing;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.Newsletters;
using Telerik.Sitefinity.Modules.Pages.Configuration;
using Telerik.Sitefinity.Mvc;
using Telerik.Sitefinity.Web.UI;

namespace Telerik.Sitefinity.Frontend.EmailCampaigns.Mvc.Controllers
{
    /// <summary>
    /// This class represents the controller of the subscribe form widget.
    /// </summary>
    [ControllerToolboxItem(Name = "SubscribeForm_MVC",
        Title = "Subscribe form",
        SectionName = ToolboxesConfig.NewslettersToolboxSectionName,
        CssClass = SubscribeFormController.WidgetIconCssClass)]
    [Localization(typeof(SubscribeFormResources))]
    public class SubscribeFormController : Controller, ICustomWidgetVisualizationExtended, ILicensedControl
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
        /// Gets the subscribe form widget model.
        /// </summary>
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public virtual ISubscribeFormModel Model
        {
            get
            {
                if (this.model == null)
                    this.model = ControllerModelFactory.GetModel<ISubscribeFormModel>(this.GetType());

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
            get { return SubscribeFormController.WidgetIconCssClass; }
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

        /// <summary>
        /// Gets a value indicating whether this instance of the control is licensed.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is licensed; otherwise, <c>false</c>.
        /// </value>
        [Browsable(false)]
        public virtual bool IsLicensed
        {
            get { return LicenseState.CheckIsModuleLicensedInCurrentDomain(NewslettersModule.ModuleId); }
        }

        /// <summary>
        /// Gets the custom licensing message. If null the system will use a default message
        /// </summary>
        /// <value>The licensing message.</value>
        [Browsable(false)]
        public virtual string LicensingMessage
        {
            get { return this.GetResource("ModuleNotLicensed"); }
        }

        /// <summary>
        /// Gets the message shown when the newsletters module is deactivated.
        /// </summary>
        /// <value>The newsletters module deactivated message.</value>
        [Browsable(false)]
        public virtual string NewslettersModuleDeactivatedMessage
        {
            get { return this.GetResource("NewslettersModuleDeactivatedMessage"); }
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
            if (!this.IsLicensed)
            {
                return this.Content(this.LicensingMessage);
            }

            if (!this.IsNewslettersModuleActivated())
            {
                return this.Content(this.NewslettersModuleDeactivatedMessage);
            }

            if (!this.IsEmpty)
            {
                var viewModel = this.Model.CreateViewModel();

                return this.View(this.TemplateName, viewModel);
            }

            return null;
        }

        /// <summary>
        /// Indexes the specified model.
        /// </summary>
        /// <param name="viewModel">The model.</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Index(SubscribeFormViewModel viewModel)
        {
            if (!this.IsLicensed)
            {
                return this.Content(this.LicensingMessage);
            }

            if (ModelState.IsValid)
            {
                string error;
                bool isSucceeded = this.Model.AddSubscriber(viewModel, out error);

                this.ViewBag.Error = error;
                this.ViewBag.IsSucceeded = isSucceeded;

                if (isSucceeded && this.Model.SuccessfullySubmittedForm == SuccessfullySubmittedForm.OpenSpecificPage)
                {
                    return this.Redirect(viewModel.RedirectPageUrl);
                }
            }

            return this.View(this.TemplateName, viewModel);
        }

        /// <summary>
        /// Called when a request matches this controller, but no method with the specified action name is found in the controller.
        /// </summary>
        /// <param name="actionName">The name of the attempted action.</param>
        protected override void HandleUnknownAction(string actionName)
        {
            this.Index().ExecuteResult(this.ControllerContext);
        }

        #endregion

        /// <summary>
        /// Gets the resource.
        /// </summary>
        /// <typeparam name="T">The type of the T.</typeparam>
        /// <param name="resourceName">Name of the resource.</param>
        /// <returns></returns>
        protected virtual string GetResource(string resourceName)
        {
            return Res.Get(typeof(SubscribeFormResources), resourceName);
        }

        /// <summary>
        /// Determines whether the newsletters module is activated.
        /// </summary>
        /// <returns></returns>
        protected virtual bool IsNewslettersModuleActivated()
        {
            return EmailCampaignsExtensions.IsModuleActivated();
        }

        #region Private fields and constants
        internal const string WidgetIconCssClass = "sfFormsIcn sfMvcIcn";
        private ISubscribeFormModel model;
        private string templateName = "SubscribeForm";
        #endregion
    }
}
