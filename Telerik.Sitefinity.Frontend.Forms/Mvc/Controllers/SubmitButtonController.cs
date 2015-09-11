using System.ComponentModel;
using System.Web.Mvc;
using Telerik.Sitefinity.Frontend.Forms.Mvc.Controllers.Base;
using Telerik.Sitefinity.Frontend.Forms.Mvc.Models.Fields.SubmitButton;
using Telerik.Sitefinity.Frontend.Forms.Mvc.StringResources;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers.Attributes;
using Telerik.Sitefinity.Metadata.Model;
using Telerik.Sitefinity.Mvc;
using Telerik.Sitefinity.Web.UI.Fields.Enums;

namespace Telerik.Sitefinity.Frontend.Forms.Mvc.Controllers
{
    /// <summary>
    /// This class represents the controller of the MVC forms submit button.
    /// </summary>
    [ControllerToolboxItem(Name = "MvcSubmitButton", Title = "Submit Button", Toolbox = FormsConstants.FormControlsToolboxName, SectionName = FormsConstants.CommonSectionName)]
    [Localization(typeof(FieldResources))]
    public class SubmitButtonController : FormElementControllerBase<ISubmitButtonModel>
    {
        public SubmitButtonController()
        {
            this.ReadTemplateName = SubmitButtonController.templateName;
            this.WriteTemplateName = SubmitButtonController.templateName;
        }

        /// <summary>
        /// Gets the Form widget model.
        /// </summary>
        [TypeConverter(typeof(ExpandableObjectConverter))]
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
        private const string templateNamePrefix = "Index.";
        private const string templateName = "Default";
    }
}