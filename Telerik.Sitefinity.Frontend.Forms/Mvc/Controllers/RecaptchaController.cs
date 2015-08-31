using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Telerik.Sitefinity.Data.Metadata;
using Telerik.Sitefinity.Frontend.Forms.Mvc.Models.Fields;
using Telerik.Sitefinity.Frontend.Forms.Mvc.Models.Fields.Recaptcha;
using Telerik.Sitefinity.Frontend.Forms.Mvc.StringResources;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers.Attributes;
using Telerik.Sitefinity.Metadata.Model;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Mvc;
using Telerik.Sitefinity.Web.UI.Fields.Enums;

namespace Telerik.Sitefinity.Frontend.Forms.Mvc.Controllers
{
    /// <summary>
    /// This class represents the controller of the MVC forms reCaptcha field.
    /// </summary>
    [ControllerToolboxItem(Name = "MvcReCaptchaField", Title = "reCAPTCHA", Toolbox = FormsConstants.FormControlsToolboxName, SectionName = FormsConstants.CommonSectionName)]
    [DatabaseMapping(UserFriendlyDataType.YesNo)]
    [Localization(typeof(FieldResources))]
    public class RecaptchaController : Controller, IFormFieldController<IRecaptchaModel>
    {
        #region Constructors

        public RecaptchaController()
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
        public virtual IRecaptchaModel Model
        {
            get
            {
                if (this.model == null)
                {
                    this.model = ControllerModelFactory.GetModel<IRecaptchaModel>(this.GetType());
                }

                return this.model;
            }
        }

        /// <inheritDocs />
        [Browsable(false)]
        public virtual IMetaField MetaField
        {
            get
            {
                if (this.Model.MetaField == null)
                {
                    this.Model.MetaField = this.Model.GetMetaField(this);
                }

                return this.Model.MetaField;
            }
            set
            {
                this.Model.MetaField = value;
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
                var viewPath = RecaptchaController.TemplateNamePrefix + this.TemplateName;
                var viewModel = this.Model.GetViewModel(value, this.MetaField);

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

        #region Private fields and Constants

        private string templateName = "Default";
        private const string TemplateNamePrefix = "Index.";
        private IRecaptchaModel model;

        #endregion
    }
}
