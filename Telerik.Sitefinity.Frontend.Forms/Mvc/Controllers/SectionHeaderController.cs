using System.ComponentModel;
using System.Web.Mvc;
using Telerik.Sitefinity.Data.Metadata;
using Telerik.Sitefinity.Frontend.Forms.Mvc.Controllers.Base;
using Telerik.Sitefinity.Frontend.Forms.Mvc.Models.Fields;
using Telerik.Sitefinity.Frontend.Forms.Mvc.Models.Fields.SectionHeader;
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
    public class SectionHeaderController : FormElementControllerBase<ISectionHeaderModel>
    {
        public SectionHeaderController()
        {
            this.DisplayMode = FieldDisplayMode.Write;
            this.ReadTemplateName = SectionHeaderController.templateName;
            this.WriteTemplateName = SectionHeaderController.templateName;
        }

        /// <inheritDocs />
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public override ISectionHeaderModel Model
        {
            get
            {
                if (this.model == null)
                    this.model = ControllerModelFactory.GetModel<ISectionHeaderModel>(this.GetType());

                return this.model;
            }
        }

        /// <inheritDocs />
        protected override string ReadTemplateNamePrefix
        {
            get
            {
                return SectionHeaderController.templateNamePrefix;
            }
        }

        /// <inheritDocs />
        protected override string WriteTemplateNamePrefix
        {
            get
            {
                return SectionHeaderController.templateNamePrefix;
            }
        }
        
        private ISectionHeaderModel model;
        private const string templateNamePrefix = "Index.";
        private const string templateName = "Default";
    }
}