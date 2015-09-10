using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Telerik.Sitefinity.Data.Metadata;
using Telerik.Sitefinity.Frontend.Forms.Mvc.Models.Fields;
using Telerik.Sitefinity.Frontend.Forms.Mvc.Models.Fields.RecaptchaField;
using Telerik.Sitefinity.Frontend.Forms.Mvc.StringResources;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers.Attributes;
using Telerik.Sitefinity.Metadata.Model;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.Forms.Web.UI.Fields;
using Telerik.Sitefinity.Mvc;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Web.UI.Fields.Enums;

namespace Telerik.Sitefinity.Frontend.Forms.Mvc.Controllers
{
    /// <summary>
    /// This class represents the controller of the MVC forms reCaptcha field.
    /// </summary>
    [FormControlDisplayMode(FormControlDisplayMode.Write)]
    [ControllerToolboxItem(Name = "MvcReCaptchaField", Title = "reCAPTCHA", Toolbox = FormsConstants.FormControlsToolboxName, SectionName = FormsConstants.CommonSectionName, CssClass = RecaptchaFieldController.WidgetIconCssClass)]
    [DatabaseMapping(UserFriendlyDataType.YesNo)]
    [Localization(typeof(FieldResources))]
    public class RecaptchaFieldController : Controller, IValidatable
    {
        #region Constructors

        public RecaptchaFieldController()
        {
            this.DisplayMode = FieldDisplayMode.Write;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the reCaptcha field model.
        /// </summary>
        /// <value>
        /// The model.
        /// </value>
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public virtual IRecaptchaFieldModel Model
        {
            get
            {
                if (this.model == null)
                {
                    this.model = ControllerModelFactory.GetModel<IRecaptchaFieldModel>(this.GetType());
                }

                return this.model;
            }
        }

        /// <inheritDocs />
        public virtual FieldDisplayMode DisplayMode
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the name of the template that will be displayed when field is in write view.
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

        #endregion

        #region Actions

        /// <inheritDocs />
        public virtual ActionResult Index(object value)
        {
            if (this.DisplayMode == FieldDisplayMode.Write && this.Model.ShouldRenderCaptcha())
            {
                var viewPath = RecaptchaFieldController.TemplateNamePrefix + this.TemplateName;
                var viewModel = this.Model.GetViewModel(value);

                return this.View(viewPath, viewModel);
            }

            return new EmptyResult();
        }

        #endregion

        #region IValidatable

        /// <inheritDocs />
        public bool IsValid()
        {
            return this.Model.IsValid(this.Model.Value);
        }

        #endregion

        #region Controller overrides

        /// <summary>
        /// Called when a request matches this controller, but no method with the specified action name is found in the controller.
        /// </summary>
        /// <param name="actionName">The name of the attempted action.</param>
        protected override void HandleUnknownAction(string actionName)
        {
            this.Index(null).ExecuteResult(this.ControllerContext);
        }

        #endregion

        #region Private fields and Constants

        internal const string WidgetIconCssClass = "sfCaptchaIcn sfMvcIcn";
        private string templateName = "Default";
        private const string TemplateNamePrefix = "Index.";
        private IRecaptchaFieldModel model;

        #endregion
    }
}
