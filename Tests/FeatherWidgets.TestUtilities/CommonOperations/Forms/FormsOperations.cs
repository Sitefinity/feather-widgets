using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;
using Telerik.Sitefinity.Forms.Model;
using Telerik.Sitefinity.Frontend.Forms.Mvc.Controllers;
using Telerik.Sitefinity.Frontend.Forms.Mvc.Models;
using Telerik.Sitefinity.Modules.Forms;
using Telerik.Sitefinity.Mvc.Proxy;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.TestIntegration.Data.Content;
using Telerik.Sitefinity.TestIntegration.SDK.DevelopersGuide.SitefinityEssentials.Modules.Forms;
using Telerik.Sitefinity.Web.UI;

namespace FeatherWidgets.TestUtilities.CommonOperations.Forms
{
    /// <summary>
    /// This class provides access to forms common server operations
    /// </summary>
    public class FormsOperations
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "Telerik.Sitefinity.TestIntegration.Data.Content.PageContentGenerator.AddControlToPage(System.Guid,System.Web.UI.Control,System.String,System.String,System.Action<Telerik.Sitefinity.Pages.Model.PageDraftControl>)"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "TestForm")]
        public void AddFormControlToPage(Guid pageId, Guid formId)
        {
            var mvcProxy = new MvcControllerProxy();
            mvcProxy.ControllerName = typeof(FormController).FullName;
            var controller = new FormController();

            controller.Model.FormId = formId;
            controller.Model.ViewMode = FormViewMode.Write;

            mvcProxy.Settings = new ControllerSettings(controller);

            PageContentGenerator.AddControlToPage(pageId, mvcProxy, "TestForm", "Contentplaceholder1");
        }

        public Guid CreateFormWithWidget(Control widget)
        {
            var formId = Guid.NewGuid();

            string formSuccessMessage = "Test form success message";

            var formControls = new Dictionary<Control, string>();
            formControls.Add(widget, "Body");

            FormsModuleCodeSnippets.CreateForm(formId, "form_" + formId.ToString("N"), formId.ToString("N"), formSuccessMessage, formControls);

            SystemManager.ClearCurrentTransactions();
            SystemManager.RestartApplication(false);
            System.Threading.Thread.Sleep(1000);

            return formId;
        }

        public void AddFormWidget(Guid formId, Control widget)
        {
            var formManager = FormsManager.GetManager();
            var form = formManager.GetForms().FirstOrDefault(f => f.Id == formId);
            var draft = formManager.EditForm(form.Id);
            var master = formManager.Lifecycle.CheckOut(draft);

            widget.ID = string.Format(CultureInfo.InvariantCulture, form.Name + "_C" + Guid.NewGuid().ToString("N", CultureInfo.InvariantCulture));
            var formControl = formManager.CreateControl<FormDraftControl>(widget, "Body");

            formControl.IsLayoutControl = widget is LayoutControl;
            formControl.SiblingId = FormsOperations.GetLastControlInPlaceHolder(form, "Body");
            formControl.Caption = widget.GetType().Name;

            master.Controls.Add(formControl);

            master = formManager.Lifecycle.CheckIn(master);
            formManager.Lifecycle.Publish(master);

            formManager.SaveChanges();
        }

        private static Guid GetLastControlInPlaceHolder(FormDescription form, string placeHolder)
        {
            var id = Guid.Empty;
            FormControl control;

            var controls = new List<FormControl>(form.Controls.Where(c => c.PlaceHolder == placeHolder));

            while (controls.Count > 0)
            {
                control = controls.Where(c => c.SiblingId == id).SingleOrDefault();
                id = control.Id;

                controls.Remove(control);
            }

            return id;
        }
    }
}
