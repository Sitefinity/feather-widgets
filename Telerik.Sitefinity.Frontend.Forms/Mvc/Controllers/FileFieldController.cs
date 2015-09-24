using System.ComponentModel;
using Telerik.Sitefinity.Data.Metadata;
using Telerik.Sitefinity.Frontend.Forms.Mvc.Controllers.Base;
using Telerik.Sitefinity.Frontend.Forms.Mvc.Models.Fields.FileField;
using Telerik.Sitefinity.Frontend.Forms.Mvc.StringResources;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers.Attributes;
using Telerik.Sitefinity.Metadata.Model;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.Pages.Web.Services;
using Telerik.Sitefinity.Mvc;

namespace Telerik.Sitefinity.Frontend.Forms.Mvc.Controllers
{
    /// <summary>
    /// This class represents the controller of the MVC forms file field.
    /// </summary>
    [ControllerToolboxItem(Name = "MvcFileField", Title = "File upload", Toolbox = FormsConstants.FormControlsToolboxName, SectionName = FormsConstants.CommonSectionName, CssClass = FileFieldController.WidgetIconCssClass)]
    [DatabaseMapping(UserFriendlyDataType.FileUpload)]
    [Localization(typeof(FieldResources))]
    public class FileFieldController : FormFieldControllerBase<IFileFieldModel>
    {
        /// <summary>
        /// Gets the form field model.
        /// </summary>
        /// <value>
        /// The model.
        /// </value>
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ReflectInheritedProperties]
        public override IFileFieldModel Model 
        {
            get
            {
                if (this.model == null)
                    this.model = ControllerModelFactory.GetModel<IFileFieldModel>(this.GetType());

                return this.model;
            }
        }

        private IFileFieldModel model;
        private const string WidgetIconCssClass = "sfFileUploadIcn sfMvcIcn";
    }
}
