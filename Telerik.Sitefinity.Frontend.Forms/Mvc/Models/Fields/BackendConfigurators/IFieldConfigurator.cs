using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.Sitefinity.Web.UI.Fields;

namespace Telerik.Sitefinity.Frontend.Forms.Mvc.Models.Fields.BackendConfigurators
{
    /// <summary>
    /// Defines API for field configuration.
    /// </summary>
    public interface IFieldConfigurator
    {
        /// <summary>
        /// Configures the specified backend control.
        /// </summary>
        /// <param name="backendControl">The backend control.</param>
        /// <param name="formFieldController">The form field controller.</param>
        void Configure(FieldControl backendControl, IFormFieldController<IFormFieldModel> formFieldController);
    }
}
