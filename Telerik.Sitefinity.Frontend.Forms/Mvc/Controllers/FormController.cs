using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Telerik.Sitefinity.Frontend.Forms.Mvc.Models;
using Telerik.Sitefinity.Mvc;

namespace Telerik.Sitefinity.Frontend.Forms.Mvc.Controllers
{
    [ControllerToolboxItem(Name = "Form_MVC", Title = "MVC Form", SectionName = "Content", CssClass = "sfMvcIcon")]
    public class FormController : Controller
    {
        public FormController()
        {
            this.Model = new FormModel();
        }

        [TypeConverter(typeof(ExpandableObjectConverter))]
        public FormModel Model { get; set; }

        public ActionResult Index()
        {
            return this.View(FormsVirtualRazorResolver.Path + this.Model.FormId.ToString("D") + ".cshtml", this.Model);
        }

        [HttpPost]
        public ActionResult Submit(FormCollection collection)
        {
            return this.Content("Successfully submitted!");
        }
    }
}