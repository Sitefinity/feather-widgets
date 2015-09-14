using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.Sitefinity.Web.UI.Fields;

namespace Telerik.Sitefinity.Frontend.Forms.Mvc.Models.Fields.BackendConfigurators
{
    public interface IFieldConfigurator
    {
        void Configure(FieldControl backendControl, IFormFieldController<IFormFieldModel> formFieldController);
    }
}
