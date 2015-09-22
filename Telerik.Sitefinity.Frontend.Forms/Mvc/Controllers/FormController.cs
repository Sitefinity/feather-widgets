using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web.Mvc;
using Telerik.Sitefinity.ContentLocations;
using Telerik.Sitefinity.Frontend.Forms.Mvc.Models;
using Telerik.Sitefinity.Frontend.Forms.Mvc.StringResources;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers.Attributes;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.Pages.Configuration;
using Telerik.Sitefinity.Mvc;
using Telerik.Sitefinity.Web.UI;

namespace Telerik.Sitefinity.Frontend.Forms.Mvc.Controllers
{
    /// <summary>
    /// This class represents the controller of Form widget.
    /// </summary>
    [ControllerToolboxItem(Name = "Form_MVC", Title = "Form", SectionName = ToolboxesConfig.ContentToolboxSectionName, CssClass = FormController.WidgetIconCssClass)]
    [Localization(typeof(FormResources))]
    [RequiresEmbeddedWebResource("Telerik.Sitefinity.Resources.Themes.LayoutsBasics.css", "Telerik.Sitefinity.Resources.Reference")]
    public class FormController : Controller, ICustomWidgetVisualizationExtended
    {
        /// <summary>
        /// Gets the Form widget model.
        /// </summary>
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public IFormModel Model
        {
            get
            {
                if (this.model == null)
                    this.model = this.InitializeModel();

                return this.model;
            }
        }

        /// <summary>
        /// Renders the form selected via the FormId property of the model.
        /// </summary>
        public ActionResult Index()
        {
            var success = this.TempData[sfSubmitSuccessKey];
            if (success == null)
            {
                var viewModel = this.Model.GetViewModel();
                if (viewModel != null)
                {
                    if (string.IsNullOrEmpty(viewModel.Error))
                    {
                        var viewPath = this.Model.GetViewPath();
                        return this.View(viewPath, viewModel);
                    }
                    else
                    {
                        return this.Content(viewModel.Error);
                    }
                }
            }
            else
            {
                var successValue = success as SubmitStatus?;
                if (successValue.HasValue)
                {
                    return this.Content(this.Model.GetSubmitMessage(successValue.Value));
                }
            }

            return new EmptyResult();
        }

        /// <summary>
        /// Submits the form selected via the FormId property of the model.
        /// </summary>
        [HttpPost]
        public ActionResult Index(FormCollection collection)
        {
            var success = this.Model.TrySubmitForm(collection, this.Request.Files, this.Request.UserHostAddress);

            if (success == SubmitStatus.Success && this.Model.NeedsRedirect)
            {
                return this.Redirect(this.Model.GetRedirectPageUrl());
            }

            this.TempData[sfSubmitSuccessKey] = success;
            return this.Index();
        }

        #region ICustomWidgetVisualization

        /// <inheritDocs />
        [Browsable(false)]
        public string EmptyLinkText
        {
            get { return Res.Get<FormResources>().SelectForm; }
        }

        /// <inheritDocs />
        [Browsable(false)]
        public bool IsEmpty
        {
            get { return (this.Model.FormId == Guid.Empty); }
        }

        /// <inheritDocs />
        [Browsable(false)]
        public string WidgetCssClass
        {
            get
            {
                return FormController.WidgetIconCssClass;
            }
        }

        #endregion 

        #region Controller overrides

        /// <summary>
        /// Called when a request matches this controller, but no method with the specified action name is found in the controller.
        /// </summary>
        /// <param name="actionName">The name of the attempted action.</param>
        protected override void HandleUnknownAction(string actionName)
        {
            this.Index().ExecuteResult(this.ControllerContext);
        }

        #endregion

        #region Private methods

        private IFormModel InitializeModel()
        {
            return ControllerModelFactory.GetModel<IFormModel>(this.GetType());
        }

        #endregion

        #region Private fields and constants

        internal const string WidgetIconCssClass = "sfFormsIcn sfMvcIcn";

        private IFormModel model;
        private bool? disableCanonicalUrlMetaTag;
        private string sfSubmitSuccessKey = "sfSubmitSuccess";

        #endregion
    }
}