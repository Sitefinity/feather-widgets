using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Telerik.Sitefinity.Data.Metadata;
using Telerik.Sitefinity.Frontend.Forms.Mvc.Controllers.Base;
using Telerik.Sitefinity.Frontend.Forms.Mvc.Models.Fields.MultipleChoiceField;
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
    /// This class represents the controller of the MVC forms multiple choice field.
    /// </summary>
    [ControllerToolboxItem(Name = "MvcMultipleChoiceField", Title = "Multiple Choice", Toolbox = FormsConstants.FormControlsToolboxName, SectionName = FormsConstants.CommonSectionName, CssClass = MultipleChoiceFieldController.WidgetIconCssClass)]
    [DatabaseMapping(UserFriendlyDataType.ShortText)]
    [Localization(typeof(FieldResources))]
    public class MultipleChoiceFieldController : FormFieldControllerBase<IMultipleChoiceFieldModel>
    {
        public MultipleChoiceFieldController()
        {
            this.DisplayMode = FieldDisplayMode.Write;
        }

        /// <inheritDocs />
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ReflectInheritedProperties]
        public override IMultipleChoiceFieldModel Model
        {
            get
            {
                if (this.model == null)
                    this.model = ControllerModelFactory.GetModel<IMultipleChoiceFieldModel>(this.GetType());

                return this.model;
            }
        }

        private IMultipleChoiceFieldModel model;
        internal const string WidgetIconCssClass = "sfMultipleChoiceIcn sfMvcIcn";
    }
}