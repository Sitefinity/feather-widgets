using System.ComponentModel;
using Telerik.Sitefinity.Frontend.Forms.Mvc.Controllers.Base;
using Telerik.Sitefinity.Frontend.Forms.Mvc.Models.Fields.SubmitButton;
using Telerik.Sitefinity.Frontend.Forms.Mvc.StringResources;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers.Attributes;
using Telerik.Sitefinity.Modules.Forms.Web.UI.Fields;
using Telerik.Sitefinity.Modules.Pages.Web.Services;
using Telerik.Sitefinity.Web.UI;

namespace Telerik.Sitefinity.Frontend.Forms.Mvc.Controllers
{
    /// <summary>
    /// This class represents the controller of the MVC forms submit button.
    /// </summary>
    [FormControlDisplayMode(FormControlDisplayMode.Write)]
    [Localization(typeof(FieldResources))]
    [IndexRenderMode(IndexRenderModes.NoOutput)]
    public class SubmitButtonController : FormElementControllerBase<ISubmitButtonModel>
    {
        public SubmitButtonController()
        {
            this.ReadTemplateName = SubmitButtonController.TemplateName;
            this.WriteTemplateName = SubmitButtonController.TemplateName;
        }

        /// <summary>
        /// Gets the Form widget model.
        /// </summary>
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ReflectInheritedProperties]
        public override ISubmitButtonModel Model
        {
            get
            {
                if (this.model == null)
                    this.model = ControllerModelFactory.GetModel<ISubmitButtonModel>(this.GetType());

                return this.model;
            }
        }

        private ISubmitButtonModel model;
        private const string TemplateNamePrefix = "Index.";
        private const string TemplateName = "Default";
    }
}