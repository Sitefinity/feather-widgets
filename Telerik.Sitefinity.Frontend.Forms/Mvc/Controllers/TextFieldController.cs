using System.ComponentModel;
using System.Web.Mvc;
using Telerik.Sitefinity.Data.Metadata;
using Telerik.Sitefinity.Frontend.Forms.Mvc.Models.Fields;
using Telerik.Sitefinity.Frontend.Forms.Mvc.Models.Fields.TextField;
using Telerik.Sitefinity.Frontend.Forms.Mvc.StringResources;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers.Attributes;
using Telerik.Sitefinity.Metadata.Model;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.Forms.Web.UI.Fields;
using Telerik.Sitefinity.Mvc;
using Telerik.Sitefinity.Web.UI.Fields.Enums;

namespace Telerik.Sitefinity.Frontend.Forms.Mvc.Controllers
{
    /// <summary>
    /// This class represents the controller of the MVC forms text field.
    /// </summary>
    [ControllerToolboxItem(Name = "MvcTextField", Title = "Text box", Toolbox = "FormControls", SectionName = "Common")]
    [DatabaseMapping(UserFriendlyDataType.ShortText)]
    [Localization(typeof(FieldResources))]
    public class TextFieldController : FormFieldController
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
        /// Gets the text field model.
        /// </summary>
        /// <value>
        /// The model.
        /// </value>
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public virtual ITextFieldModel Model
        {
            get
            {
                if (this.model == null)
                {
                    this.model = ControllerModelFactory.GetModel<ITextFieldModel>(this.GetType());
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

        /// <summary>
        /// Gets or sets the name of the template that will be displayed when field is in read view.
        /// </summary>
        /// <value></value>
        public string ReadTemplateName
        {
            get
            {
                return this.readTemplateName;
            }

            set
            {
                this.readTemplateName = value;
            }
        }

        #endregion

        #region Actions

        /// <inheritDocs />
        public override ActionResult Read(object value)
        {
            if (value == null || !(value is string))
                value = this.MetaField.DefaultValue;

            this.Model.Value = value;
            var fullTemplateName = this.readTemplateNamePrefix + this.ReadTemplateName;

            return this.View(fullTemplateName, this.Model);
        }

        /// <inheritDocs />
        public override ActionResult Write(object value)
        {
            if (value == null || !(value is string))
                value = this.MetaField.DefaultValue;

            this.Model.Value = value;
            var fullTemplateName = this.writeTemplateNamePrefix + this.WriteTemplateName;

            return this.View(fullTemplateName, this.Model);
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
        private string readTemplateName = "Default";
        private readonly string writeTemplateNamePrefix = "Write.";
        private readonly string readTemplateNamePrefix = "Read.";
        private ITextFieldModel model;

        #endregion
    }
}