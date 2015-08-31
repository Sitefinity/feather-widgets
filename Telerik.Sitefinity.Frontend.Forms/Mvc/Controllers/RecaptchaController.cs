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
    public class RecaptchaController : FormFieldController
    {
        #region Properties

        /// <inheritDocs />
        [Browsable(false)]
        public override IFormFieldModel FieldModel
        {
            get
            {
                return this.Model;
            }
        }

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
        [Browsable(false)]
        public override IMetaField MetaField
        {
            get
            {
                return base.MetaField;
            }
            set
            {
                base.MetaField = value;
            }
        }

        /// <inheritDocs />
        public override FieldDisplayMode DisplayMode
        {
            get
            {
                return base.DisplayMode;
            }
            set
            {
                base.DisplayMode = value;
            }
        }

        /// <summary>
        /// Gets or sets the name of the template that will be displayed when field is in write view.
        /// </summary>
        /// <value></value>
        public string WriteTemplateName
        {
            get
            {
                return this.writeTemplateName;
            }

            set
            {
                this.writeTemplateName = value;
            }
        }

        #endregion

        #region Actions

        /// <inheritDocs />
        public override ActionResult Read(object value)
        {
            return this.View();
        }

        /// <inheritDocs />
        public override ActionResult Write(object value)
        {
            if (this.Model.ShouldRenderCaptcha())
            {
                var model = this.Model.GetViewModel(value, this.MetaField); 
                var fullTemplateName = RecaptchaController.WriteTemplateNamePrefix + this.WriteTemplateName;

                return this.View(fullTemplateName, model);
            }

            return this.View();
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Called when a request matches this controller, but no method with the specified action name is found in the controller.
        /// </summary>
        /// <param name="actionName">The name of the attempted action.</param>
        protected override void HandleUnknownAction(string actionName)
        {
            base.HandleUnknownAction(actionName);
        }

        #endregion

        #region Private fields and Constants

        private string writeTemplateName = "Default";
        private const string WriteTemplateNamePrefix = "Write.";
        private IRecaptchaFieldModel model;

        #endregion
    }
}
