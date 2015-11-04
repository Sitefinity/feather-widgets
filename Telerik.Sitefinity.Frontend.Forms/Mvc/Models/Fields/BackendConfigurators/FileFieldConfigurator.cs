using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;
using Telerik.Sitefinity.Frontend.Forms.Mvc.Controllers.Base;
using Telerik.Sitefinity.Frontend.Forms.Mvc.Models.Fields.CheckboxesField;
using Telerik.Sitefinity.Frontend.Forms.Mvc.Models.Fields.FileField;
using Telerik.Sitefinity.Modules.Forms;
using Telerik.Sitefinity.Modules.Forms.Web.UI.Fields;
using Telerik.Sitefinity.Web.UI.Fields;

namespace Telerik.Sitefinity.Frontend.Forms.Mvc.Models.Fields.BackendConfigurators
{
    /// <summary>
    /// Configures specifics for file field in the backend.
    /// </summary>
    internal class FileFieldConfigurator : IFieldConfigurator
    {
        /// <inheritDocs/>
        public Guid FormId
        {
            get;
            set;
        }

        /// <inheritDocs/>
        public void Configure(ref FieldControl backendControl, IFormFieldController<IFormFieldModel> formFieldController)
        {
            backendControl.GetType().GetProperty("FormId").SetValue(backendControl, this.FormId);
            backendControl.GetType().GetProperty("FormsProviderName").SetValue(backendControl, FormsManager.GetDefaultProviderName());
            backendControl.GetType().GetProperty("AllowMultipleAttachments").SetValue(backendControl, ((IFileFieldModel)formFieldController.Model).AllowMultipleFiles);
        }
    }
}
