using System.Web.Mvc;
using Telerik.Sitefinity.Modules.Forms.Web.UI.Fields;
using Telerik.Sitefinity.Web.UI.Fields.Contracts;

namespace Telerik.Sitefinity.Frontend.Forms
{
    public interface IFormFieldController : IFormFieldControl, IHasFieldDisplayMode
    {
        ActionResult Read(object value);

        ActionResult Write(object value);
    }
}