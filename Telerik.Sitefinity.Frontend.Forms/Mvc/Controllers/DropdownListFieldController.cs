using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Telerik.Sitefinity.Data.Metadata;
using Telerik.Sitefinity.Frontend.Forms.Mvc.Models.Fields.DropdownListField;
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
    /// This class represents the controller of the MVC forms dropdown list field.
    /// </summary>
    [ControllerToolboxItem(Name = "MvcDropdownListField", Title = "Dropdown List", Toolbox = FormsConstants.FormControlsToolboxName, SectionName = FormsConstants.CommonSectionName, CssClass = DropdownListFieldController.WidgetIconCssClass)]
    [DatabaseMapping(UserFriendlyDataType.ShortText)]
    [Localization(typeof(FieldResources))]
    public class DropdownListFieldController : Controller, IFormFieldController<IDropdownListFieldModel>
    {
        #region Constructors

        public DropdownListFieldController()
        {
            this.DisplayMode = FieldDisplayMode.Write;
        }

        #endregion

        #region Properties

        /// <inheritDocs />
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public virtual IDropdownListFieldModel Model
        {
            get
            {
                if (this.model == null)
                    this.model = ControllerModelFactory.GetModel<IDropdownListFieldModel>(this.GetType());

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
                templateName = DropdownListFieldController.ReadTemplateNamePrefix + this.ReadTemplateName;
            else
                templateName = DropdownListFieldController.WriteTemplateNamePrefix + this.WriteTemplateName;

            return this.View(templateName, viewModel);
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

        #region Public methods

        /// <inheritDocs />
        public bool IsValid()
        {
            return this.Model.IsValid(this.Model.Value);
        }

        #endregion

        #region Private fields and Constants

        internal const string WidgetIconCssClass = "sfDropdownIcn sfMvcIcn";
        private string writeTemplateName = "Default";
        private string readTemplateName = "Default";
        private const string WriteTemplateNamePrefix = "Write.";
        private const string ReadTemplateNamePrefix = "Read.";
        private IDropdownListFieldModel model;

        #endregion
    }
}
