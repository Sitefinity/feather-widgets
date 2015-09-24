using System.ComponentModel;
using System.Web.Mvc;
using Telerik.Sitefinity.Data.Metadata;
using Telerik.Sitefinity.Frontend.Forms.Mvc.Controllers.Base;
using Telerik.Sitefinity.Frontend.Forms.Mvc.Models.Fields.ParagraphTextField;
using Telerik.Sitefinity.Frontend.Forms.Mvc.StringResources;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers.Attributes;
using Telerik.Sitefinity.Metadata.Model;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.Pages.Web.Services;
using Telerik.Sitefinity.Mvc;
using Telerik.Sitefinity.Web.UI.Fields.Enums;

namespace Telerik.Sitefinity.Frontend.Forms.Mvc.Controllers
{
    /// <summary>
    /// This class represents the controller of the MVC forms paragraph text field.
    /// </summary>
    [ControllerToolboxItem(Name = "MvcParagraphTextField", Title = "Paragraph Text", Toolbox = FormsConstants.FormControlsToolboxName, SectionName = FormsConstants.CommonSectionName, CssClass = ParagraphTextFieldController.WidgetIconCssClass)]
    [DatabaseMapping(UserFriendlyDataType.ShortText)]
    [Localization(typeof(FieldResources))]
    public class ParagraphTextFieldController : FormFieldControllerBase<IParagraphTextFieldModel>
    {
        public ParagraphTextFieldController()
        {
            this.DisplayMode = FieldDisplayMode.Write;
        }

        /// <inheritDocs />
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ReflectInheritedProperties]
        public override IParagraphTextFieldModel Model
        {
            get
            {
                if (this.model == null)
                    this.model = ControllerModelFactory.GetModel<IParagraphTextFieldModel>(this.GetType());

                return this.model;
            }
        }

        private IParagraphTextFieldModel model;
        internal const string WidgetIconCssClass = "sfParagraphboxIcn sfMvcIcn";
    }
}