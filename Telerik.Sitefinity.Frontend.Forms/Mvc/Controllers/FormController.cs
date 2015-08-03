using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Telerik.Sitefinity.Frontend.Forms.Mvc.Models;
using Telerik.Sitefinity.Modules.Forms;
using Telerik.Sitefinity.Mvc;
using Telerik.Sitefinity.Security.Claims;
using Telerik.Sitefinity.Services;

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
            var manager = FormsManager.GetManager();
            var form = manager.GetForm(this.Model.FormId);
            var formLanguage = SystemManager.CurrentContext.AppSettings.Multilingual ? CultureInfo.CurrentUICulture.Name : null;
            
            var formData = new KeyValuePair<string, object>[collection.Count];
            for (int i = 0; i < collection.Count; i++)
                formData[i] = new KeyValuePair<string,object>(collection.Keys[i], collection[collection.Keys[i]]);

            // TODO: Make this public FormsHelper.SaveFormsEntry(form, formData, null, this.Request.UserHostAddress, ClaimsManager.GetCurrentUserId(), formLanguage);
            return this.Content("Successfully submitted!");
        }
    }
}