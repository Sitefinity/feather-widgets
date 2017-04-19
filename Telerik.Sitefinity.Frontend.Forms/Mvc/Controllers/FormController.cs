using System;
using System.ComponentModel;
using System.Web.Mvc;
using Telerik.Sitefinity.Frontend.Forms.Mvc.Models;
using Telerik.Sitefinity.Frontend.Forms.Mvc.StringResources;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers.Attributes;
using Telerik.Sitefinity.Frontend.Resources;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.Pages.Configuration;
using Telerik.Sitefinity.Mvc;
using Telerik.Sitefinity.Mvc.ActionFilters;
using Telerik.Sitefinity.Services;
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

            set
            {
                this.model = value;
            }
        }

        /// <summary>
        /// Renders the form selected via the FormId property of the model.
        /// </summary>
        public ActionResult Index()
        {
            var viewModel = this.Model.GetViewModel();
            if (viewModel != null)
            {
                if (SystemManager.CurrentHttpContext != null)
                    this.AddCacheDependencies(this.Model.GetKeysOfDependentObjects(viewModel));

                if (string.IsNullOrEmpty(viewModel.Error))
                {
                    var viewPath = this.GetViewPath(this.Model.FormId);
                    return this.View(viewPath, viewModel);
                }
                else
                {
                    return this.Content(viewModel.Error);
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
            if (!this.ViewData.ContainsKey(FormController.ShouldProcessRequestKey) || (bool)this.ViewData[FormController.ShouldProcessRequestKey])
            {
                var success = this.Model.TrySubmitForm(collection, this.Request != null ? this.Request.Files : null, this.Request != null ? this.Request.UserHostAddress : null);

                if (success == SubmitStatus.Success && this.Model.NeedsRedirect)
                {
                    if (this.Model.RaiseBeforeFormActionEvent())
                    {
                        return this.Redirect(this.Model.GetRedirectPageUrl());
                    }
                    else
                    {
                        return this.Index();
                    }
                }

                if (this.Model.RaiseBeforeFormActionEvent())
				{
					var resultMessage = this.Model.GetSubmitMessage(success);
					this.ViewBag.SubmitMessage = resultMessage;

                    if (success == SubmitStatus.Success)
                    {
                        var viewTemplatePath = FormController.TemplateNamePrefix + FormController.SubmitResultTemplateName;
                        return this.View(viewTemplatePath);
                    }
                    else
                    {
                        this.ViewBag.ErrorMessage = resultMessage;
                        this.Model.FormCollection = collection;
                        return this.Index();
                    }
				}
                else
				{
                    return this.Index();
				}
            }
			else
			{
				return this.Index();
			}
        }

        /// <summary>
        /// Action that processes ajax form submit.
        /// </summary>
        /// <param name="collection">The collection.</param>
        /// <returns>The action result in json format.</returns>
        [HttpPost]
        [StandaloneResponseFilter]
        public JsonResult AjaxSubmit(FormCollection collection)
        {
            var result = this.Model.TrySubmitForm(collection, this.Request.Files, this.Request.UserHostAddress);
            if (result != SubmitStatus.Success && this.Model.RaiseBeforeFormActionEvent())
            {
                return this.Json(new { success = false, error = this.Model.GetSubmitMessage(result) });
            }
            else
            {
                return this.Json(new { success = true, error = string.Empty });
            }
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
            get { return this.Model.FormId == Guid.Empty; }
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
            this.ActionInvoker.InvokeAction(this.ControllerContext, "Index");
        }

        #endregion

        #region Private methods

        private IFormModel InitializeModel()
        {
            return ControllerModelFactory.GetModel<IFormModel>(this.GetType());
        }

        private string GetViewPath(Guid formId)
        {
            var currentPackage = new PackageManager().GetCurrentPackage();
            if (string.IsNullOrEmpty(currentPackage))
            {
                currentPackage = "default";
            }

            var viewPath = FormsVirtualRazorResolver.Path + currentPackage + "/" + formId.ToString("D") + ".cshtml";

            return viewPath;
        }

        #endregion

        #region Private fields and constants

        private IFormModel model;
        private const string ShouldProcessRequestKey = "should-process-request";
        internal const string WidgetIconCssClass = "sfFormsIcn sfMvcIcn";
        internal const string TemplateNamePrefix = "Form.";
        internal const string SubmitResultTemplateName = "SubmitResultView";        

        #endregion
    }
}