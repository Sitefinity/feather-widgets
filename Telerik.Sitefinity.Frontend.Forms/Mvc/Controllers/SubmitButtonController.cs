using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        public SubmitButtonController()
        {
            this.Model = new SubmitButtonModel();
        }

        [TypeConverter(typeof(ExpandableObjectConverter))]
        public SubmitButtonModel Model { get; set; }

        public ActionResult Read()
        {
            return new EmptyResult();
        }

        public ActionResult Write()
        {
            return this.View("Write", this.Model);
        }

        protected override void HandleUnknownAction(string actionName)
        {
            this.Write().ExecuteResult(this.ControllerContext);
        }
    }
}