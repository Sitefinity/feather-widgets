using System.ComponentModel;
using Telerik.Sitefinity.Data.Metadata;
using Telerik.Sitefinity.Frontend.Forms.Mvc.Controllers.Base;
using Telerik.Sitefinity.Frontend.Forms.Mvc.Models.Fields.HiddenField;
using Telerik.Sitefinity.Frontend.Forms.Mvc.StringResources;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers.Attributes;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.Forms.Web.UI.Fields;
using Telerik.Sitefinity.Modules.Pages.Web.Services;
using Telerik.Sitefinity.Web.UI.Fields.Enums;

namespace Telerik.Sitefinity.Frontend.Forms.Mvc.Controllers
{
    /// <summary>
    /// This class represents the controller of the MVC forms hidden field.
    /// </summary>
    [FormControlDisplayMode(FormControlDisplayMode.Write)]
    [DatabaseMapping(UserFriendlyDataType.ShortText)]
    [Localization(typeof(FieldResources))]
    public class HiddenFieldController : FormFieldControllerBase<IHiddenFieldModel>
    {
        public HiddenFieldController()
        {
            this.DisplayMode = FieldDisplayMode.Write;
        }

        /// <inheritDocs />
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ReflectInheritedProperties]
        public override IHiddenFieldModel Model
        {
            get
            {
                if (this.model == null)
                    this.model = ControllerModelFactory.GetModel<IHiddenFieldModel>(this.GetType());

                return this.model;
            }
        }

        private IHiddenFieldModel model;
    }
}