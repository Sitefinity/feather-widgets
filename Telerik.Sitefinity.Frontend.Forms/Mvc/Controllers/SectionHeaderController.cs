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
    [ControllerToolboxItem(Name = "MvcSectionHeaderField", Title = "Section Header", Toolbox = FormsConstants.FormControlsToolboxName, SectionName = FormsConstants.CommonSectionName, CssClass = SectionHeaderController.WidgetIconCssClass)]
    [DatabaseMapping(UserFriendlyDataType.ShortText)]
    [Localization(typeof(FieldResources))]
    public class SectionHeaderController : FormElementControllerBase<ISectionHeaderModel>
    {
        public SectionHeaderController()
        {
            this.DisplayMode = FieldDisplayMode.Read;
            this.ReadTemplateName = SectionHeaderController.templateName;
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

        public override ActionResult Write(object value)
        {
            return this.Read(value);
        }

        protected override void HandleUnknownAction(string actionName)
        {
            this.Read(null).ExecuteResult(this.ControllerContext);
        }

        internal const string WidgetIconCssClass = "sfSectionHeaderIcn sfMvcIcn";
        private ISectionHeaderModel model;
        private const string templateNamePrefix = "Read.";
        private const string templateName = "Default";
    }
}