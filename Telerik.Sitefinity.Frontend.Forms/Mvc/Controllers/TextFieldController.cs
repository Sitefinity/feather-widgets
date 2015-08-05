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
    public class TextFieldController : Controller, IFormFieldController
    {
        #region Properties

        /// <inheritDocs />
        [Browsable(false)]
        public virtual IFormFieldModel FieldModel
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
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public IMetaField MetaField
        {
            get
            {
                if (this.metaField == null)
                {
                    this.metaField = this.Model.GetMetaField(this);
                }

                return this.metaField;
            }

            set
            {
                this.metaField = value;
            }
        }

        /// <summary>
        /// Gets or sets the display mode.
        /// </summary>
        /// <value>
        /// The display mode.
        /// </value>
        public FieldDisplayMode DisplayMode
        {
            get
            {
                return this.displayMode;
            }

            set
            {
                this.displayMode = value;
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
        public ActionResult Read(object value)
        {
            if (value == null || !(value is string))
                value = this.MetaField.DefaultValue;

            this.Model.Value = value;
            this.ViewBag.MetaField = this.MetaField;
            var fullTemplateName = this.readTemplateNamePrefix + this.ReadTemplateName;

            return this.View(fullTemplateName, this.Model);
        }

        /// <inheritDocs />
        public ActionResult Write(object value)
        {
            if (value == null || !(value is string))
                value = this.MetaField.DefaultValue;

            this.Model.Value = value;
            this.ViewBag.MetaField = this.MetaField;
            var fullTemplateName = this.writeTemplateNamePrefix + this.WriteTemplateName;

            return this.View(fullTemplateName, this.Model);
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Determines whether this instance is valid.
        /// </summary>
        /// <returns></returns>
        public bool IsValid()
        {
            return this.Model.IsValid(this.Model.Value);
        }

        /// <summary>
        /// Called when a request matches this controller, but no method with the specified action name is found in the controller.
        /// </summary>
        /// <param name="actionName">The name of the attempted action.</param>
        protected override void HandleUnknownAction(string actionName)
        {
            if (this.DisplayMode == FieldDisplayMode.Read)
            {
                this.Read(null).ExecuteResult(this.ControllerContext);
            }
            else if (this.DisplayMode == FieldDisplayMode.Write)
            {
                this.Write(null).ExecuteResult(this.ControllerContext);
            }
        }

        #endregion

        #region Private fields and Constants

        private string writeTemplateName = "Default";
        private string readTemplateName = "Default";
        private FieldDisplayMode displayMode = FieldDisplayMode.Write;
        private readonly string writeTemplateNamePrefix = "Write.";
        private readonly string readTemplateNamePrefix = "Read.";
        private IMetaField metaField;
        private ITextFieldModel model;

        #endregion
    }
}