using System.ComponentModel;
using System.Web.Mvc;
using Telerik.Sitefinity.Data.Metadata;
using Telerik.Sitefinity.Frontend.Forms.Mvc.Models.Fields;
using Telerik.Sitefinity.Frontend.Forms.Mvc.Models.Fields.SectionHeaderField;
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
    /// This class represents the controller of the MVC forms section header field.
    /// </summary>
    [ControllerToolboxItem(Name = "MvcSectionHeaderField", Title = "Section Header", Toolbox = FormsConstants.FormControlsToolboxName, SectionName = FormsConstants.CommonSectionName)]
    [DatabaseMapping(UserFriendlyDataType.ShortText)]
    [Localization(typeof(FieldResources))]
    public class SectionHeaderFieldController : Controller, IFormFieldController<ISectionHeaderFieldModel>
    {
        #region Constructors

        public SectionHeaderFieldController()
        {
            this.DisplayMode = FieldDisplayMode.Write;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the section header field model.
        /// </summary>
        /// <value>
        /// The model.
        /// </value>
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public virtual ISectionHeaderFieldModel Model
        {
            get
            {
                if (this.model == null)
                    this.model = ControllerModelFactory.GetModel<ISectionHeaderFieldModel>(this.GetType());

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
        /// Gets or sets the name of the template that will be displayed.
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

        public virtual ActionResult Index(object value = null)
        {
            var viewPath = SectionHeaderFieldController.TemplateNamePrefix + this.TemplateName;
            var viewModel = this.Model.GetViewModel();

            return this.View(viewPath, viewModel);
        }

        #endregion

        #region IValidatable

        /// <inheritDocs />
        public bool IsValid()
        {
            return true;
        }

        #endregion

        #region Private fields and constants

        private ISectionHeaderFieldModel model;

        private const string TemplateNamePrefix = "Index.";
        private string templateName = "Default";

        #endregion
    }
}
