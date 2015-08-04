using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Web.Mvc;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Forms.Model;
using Telerik.Sitefinity.Frontend.Forms.Mvc.Models;
using Telerik.Sitefinity.Frontend.Resources;
using Telerik.Sitefinity.Modules.Forms;
using Telerik.Sitefinity.Modules.Forms.Web;
using Telerik.Sitefinity.Modules.Forms.Web.UI.Fields;
using Telerik.Sitefinity.Modules.Pages.Configuration;
using Telerik.Sitefinity.Mvc;
using Telerik.Sitefinity.Security.Claims;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.Web.UI;

namespace Telerik.Sitefinity.Frontend.Forms.Mvc.Controllers
{
    [ControllerToolboxItem(Name = "Form_MVC", Title = "Form", SectionName = ToolboxesConfig.ContentToolboxSectionName, CssClass = FormController.WidgetIconCssClass)]
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
            if (this.Model.FormId == Guid.Empty)
            {
                return new EmptyResult();
            }
            else
            {
                var currentPackage = new PackageManager().GetCurrentPackage();
                var viewPath = FormsVirtualRazorResolver.Path + (currentPackage ?? "default") + "/" + this.Model.FormId.ToString("D") + ".cshtml";

                return this.View(viewPath, this.Model);
            }
        }

        [HttpPost]
        public ActionResult Submit(FormCollection collection)
        {
            var manager = FormsManager.GetManager();
            var form = manager.GetForm(this.Model.FormId);

            if (this.IsValid(form, collection, manager))
            {
                var formLanguage = SystemManager.CurrentContext.AppSettings.Multilingual ? CultureInfo.CurrentUICulture.Name : null;

                var formData = new KeyValuePair<string, object>[collection.Count];
                for (int i = 0; i < collection.Count; i++)
                {
                    formData[i] = new KeyValuePair<string, object>(collection.Keys[i], collection[collection.Keys[i]]);
                }
                
                FormsHelper.SaveFormsEntry(form, formData, null, this.Request.UserHostAddress, ClaimsManager.GetCurrentUserId(), formLanguage);

                return this.Content("Successfully submitted!");
            }
            else
            {
                return this.Content("Entry is not valid!");
            }
        }

        private bool IsValid(FormDescription form, FormCollection collection, FormsManager manager)
        {
            var behaviorResolver = ObjectFactory.Resolve<IControlBehaviorResolver>();
            foreach (var control in form.Controls)
            {
                if (control.IsLayoutControl)
                    continue;

                Type controlType;
                if (!control.ObjectType.StartsWith("~/"))
                    controlType = TypeResolutionService.ResolveType(behaviorResolver.GetBehaviorObjectType(control), true);
                else
                    controlType = FormsManager.GetControlType(control);

                if (!typeof(IFormFieldControl).IsAssignableFrom(controlType))
                    continue;

                var controlInstance = manager.LoadControl(control);
                var fieldControl = (IFormFieldControl)behaviorResolver.GetBehaviorObject(controlInstance);
                
                ((TextFieldController)fieldControl).Model.Value = collection[fieldControl.MetaField.FieldName];

                if (!fieldControl.IsValid())
                    return false;
            }

            return true;
        }

        internal const string WidgetIconCssClass = "sfFormsIcn sfMvcIcon";
    }
}