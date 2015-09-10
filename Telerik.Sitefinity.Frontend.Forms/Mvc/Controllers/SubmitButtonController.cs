using System.ComponentModel;
using System.Web.Mvc;
using Telerik.Sitefinity.Frontend.Forms.Mvc.Models.Fields.SubmitButton;
using Telerik.Sitefinity.Frontend.Forms.Mvc.StringResources;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers.Attributes;
using Telerik.Sitefinity.Metadata.Model;
using Telerik.Sitefinity.Mvc;
using Telerik.Sitefinity.Web.UI.Fields.Enums;

namespace Telerik.Sitefinity.Frontend.Forms.Mvc.Controllers
{
    /// <summary>
    /// This class represents the controller of the MVC forms submit button.
    /// </summary>
    [ControllerToolboxItem(Name = "MvcSubmitButton", Title = "Submit Button", Toolbox = FormsConstants.FormControlsToolboxName, SectionName = FormsConstants.CommonSectionName, CssClass = SubmitButtonController.WidgetIconCssClass)]
    [Localization(typeof(FieldResources))]
    public class SubmitButtonController : Controller
    {
        #region Properties

        /// <summary>
        /// Gets the Form widget model.
        /// </summary>
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public ISubmitButtonModel Model
        {
            get
            {
                if (this.model == null)
                    this.model = ControllerModelFactory.GetModel<ISubmitButtonModel>(this.GetType());

                return this.model;
            }
        }

        /// <summary>
        /// Gets or sets the name of the template that will be displayed
        /// </summary>
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

        #endregion

        #region Actions

        /// <summary>
        /// Provides the default view of this field
        /// </summary>
        public virtual ActionResult Index()
        {
            var viewPath = SubmitButtonController.TemplateNamePrefix + this.TemplateName;
            var viewModel = this.Model.GetViewModel();

            return this.View(viewPath, viewModel);
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

        #region Private fields and constants

        private ISubmitButtonModel model;

        internal const string WidgetIconCssClass = "sfSubmitBtnIcn sfMvcIcn";
        private const string TemplateNamePrefix = "Index.";
        private string templateName = "Default";

        #endregion
    }
}