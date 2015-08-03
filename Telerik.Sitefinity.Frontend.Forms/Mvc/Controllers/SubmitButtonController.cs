using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Telerik.Sitefinity.Mvc;

namespace Telerik.Sitefinity.Frontend.Forms.Mvc.Controllers
{
    [ControllerToolboxItem(Name = "MvcSubmitButton", Title = "MvcSubmitButton", Toolbox = "FormControls", SectionName = "Common")]
    public class SubmitButtonController : Controller
    {
        public ActionResult Read()
        {
            return new EmptyResult();
        }

        public ActionResult Write()
        {
            return this.Content("<input type=\"submit\" ></input>");
        }

        protected override void HandleUnknownAction(string actionName)
        {
            this.Write().ExecuteResult(this.ControllerContext);
        }
    }
}