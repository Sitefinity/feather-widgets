using System.ComponentModel;
using System.Web.Mvc;
using Telerik.Sitefinity.Data.Metadata;
using Telerik.Sitefinity.Frontend.Forms.Mvc.Models.Fields.ParagraphTextField;
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
    /// This class represents the controller of the MVC forms paragraph text field.
    /// </summary>
    [ControllerToolboxItem(Name = "MvcParagraphTextField", Title = "Paragraph Text", Toolbox = FormsConstants.FormControlsToolboxName, SectionName = FormsConstants.CommonSectionName)]
    [DatabaseMapping(UserFriendlyDataType.ShortText)]
    [Localization(typeof(FieldResources))]
    public class ParagraphTextFieldController : Controller, IFormFieldController<IParagraphTextFieldModel>
    {
        #region Constructors

        public ParagraphTextFieldController()
        {
            this.DisplayMode = FieldDisplayMode.Write;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the text field model.
        /// </summary>
        /// <value>
        /// The model.
        /// </value>
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public virtual IParagraphTextFieldModel Model
        {
            get
            {
                if (this.model == null)
                    this.model = ControllerModelFactory.GetModel<IParagraphTextFieldModel>(this.GetType());

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

        public virtual ActionResult Index(object value = null)
        {
            if (value == null || !(value is string))
                value = this.MetaField.DefaultValue;
            
            string templateName;
            var viewModel = this.Model.GetViewModel(value, this.MetaField);
            
            if (this.DisplayMode == FieldDisplayMode.Read)
                templateName = ParagraphTextFieldController.ReadTemplateNamePrefix + this.ReadTemplateName;
            else
                templateName = ParagraphTextFieldController.WriteTemplateNamePrefix + this.WriteTemplateName;

            return this.View(templateName, viewModel);
        }
        
        #endregion

        #region Public methods

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
            this.Index().ExecuteResult(this.ControllerContext);
        }

        #endregion

        #region Private fields and Constants

        private string writeTemplateName = "Default";
        private string readTemplateName = "Default";
        private const string WriteTemplateNamePrefix = "Write.";
        private const string ReadTemplateNamePrefix = "Read.";
        private IParagraphTextFieldModel model;

        #endregion
    }
}