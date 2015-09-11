using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.Sitefinity.Frontend.Forms.Mvc.Models.Fields;
using Telerik.Sitefinity.Modules.Forms.Web.UI.Fields;

namespace Telerik.Sitefinity.Frontend.Forms.Mvc.Controllers.Base
{
    public interface IFormFieldController<out T> : IFormElementController<T>, IFormFieldControl
        where T : IFormFieldModel
    {
    }
}
