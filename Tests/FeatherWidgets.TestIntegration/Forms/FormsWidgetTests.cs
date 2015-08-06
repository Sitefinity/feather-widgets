using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.UI;
using MbUnit.Framework;
using Telerik.Sitefinity.Forms.Model;
using Telerik.Sitefinity.Frontend.Forms.Mvc.Controllers;
using Telerik.Sitefinity.Frontend.Forms.Mvc.Models;
using Telerik.Sitefinity.Frontend.GridSystem;
using Telerik.Sitefinity.Frontend.TestUtilities;
using Telerik.Sitefinity.Frontend.TestUtilities.CommonOperations;
using Telerik.Sitefinity.Modules.Forms;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Mvc.Proxy;
using Telerik.Sitefinity.TestIntegration.Data.Content;
using Telerik.Sitefinity.TestIntegration.SDK.DevelopersGuide.SitefinityEssentials.Modules.Forms;
using Telerik.Sitefinity.Web.UI;

namespace FeatherWidgets.TestIntegration.Forms
{
    /// <summary>
    /// This is a class with Forms widget tests.
    /// </summary>
    [TestFixture]
    public class FormsWidgetTests
    {
        /// <summary>
        /// Same grid widget on a form placed on different packages yelds different output.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "FeatherWidgets.TestIntegration.Forms.FormsWidgetTests.AddGridControlToPage(System.Guid,System.String,System.String,System.String)"), Test]
        [Category(TestCategories.Forms)]
        [Author(FeatherTeams.FeatherTeam)]
        [Description("Same grid widget on a form placed on different packages yelds different output.")]
        public void FormsWidget_WithGridWidget_AdaptsToPackage()
        {
            var gridVirtualPath = "~/Frontend-Assembly/Telerik.Sitefinity.Frontend/GridSystem/Templates/grid-3+9.html";
            var control = new GridControl();
            control.Layout = gridVirtualPath;
            var formId = this.CreateFormWithWidget(control);

            var pageManager = PageManager.GetManager();

            try
            {
                var semanticTemplate = pageManager.GetTemplates().FirstOrDefault(t => t.Name == "SemanticUI.default" && t.Title == "default");
                Assert.IsNotNull(semanticTemplate, "Template was not found");

                var semanticPageId = FeatherServerOperations.Pages().CreatePageWithTemplate(semanticTemplate, "FormsPageSemantic", "forms-page-semantic");
                this.AddFormControlToPage(semanticPageId, formId);

                string semanticPageContent = FeatherServerOperations.Pages().GetPageContent(semanticPageId);

                Assert.IsTrue(semanticPageContent.Contains("class=\"sf_colsIn four wide column\""), "SemanticUI grid content not found.");

                var bootstrapTemplate = pageManager.GetTemplates().FirstOrDefault(t => t.Name == "Bootstrap.default" && t.Title == "default");
                Assert.IsNotNull(bootstrapTemplate, "Template was not found");

                var bootstrapPageId = FeatherServerOperations.Pages().CreatePageWithTemplate(bootstrapTemplate, "FormsPageBootstrap", "forms-page-bootstrap");
                this.AddFormControlToPage(bootstrapPageId, formId);

                string bootstrapPageContent = FeatherServerOperations.Pages().GetPageContent(bootstrapPageId);

                Assert.IsTrue(bootstrapPageContent.Contains("class=\"sf_colsIn col-md-3\""), "Bootstrap grid content not found.");
            }
            finally
            {
                Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.Pages().DeleteAllPages();
                FormsModuleCodeSnippets.DeleteForm(formId);
            }
        }

        /// <summary>
        /// Ensures that the form markup is updated when a new widget is added to it.
        /// </summary>
        [Test]
        [Category(TestCategories.Forms)]
        [Author(FeatherTeams.FeatherTeam)]
        [Description("Ensures that the form markup is updated when a new widget is added to it.")]
        public void FormsWidget_AddWidgetToFormDescription_FormIsUpdated()
        {
            var gridVirtualPath = "~/Frontend-Assembly/Telerik.Sitefinity.Frontend/GridSystem/Templates/grid-3+9.html";
            var control = new GridControl();
            control.Layout = gridVirtualPath;
            var formId = this.CreateFormWithWidget(control);

            var pageManager = PageManager.GetManager();

            try
            {
                var template = pageManager.GetTemplates().FirstOrDefault(t => t.Name == "SemanticUI.default" && t.Title == "default");
                Assert.IsNotNull(template, "Template was not found");

                var pageId = FeatherServerOperations.Pages().CreatePageWithTemplate(template, "FormsPageCacheTest", "forms-page-cache-test");
                this.AddFormControlToPage(pageId, formId);

                string pageContent = FeatherServerOperations.Pages().GetPageContent(pageId);

                Assert.IsTrue(pageContent.Contains("class=\"sf_colsIn four wide column\""), "SemanticUI grid content not found.");

                this.AddFormWidget(formId, new GridControl() { Layout = "<div class=\"sf_colsIn\">Funny widget.</div>" });
                pageContent = FeatherServerOperations.Pages().GetPageContent(pageId);

                Assert.IsTrue(pageContent.Contains("Funny widget."), "Form did not render the new control.");
            }
            finally
            {
                Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.Pages().DeleteAllPages();
                FormsModuleCodeSnippets.DeleteForm(formId);
            }
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

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "Telerik.Sitefinity.TestIntegration.Data.Content.PageContentGenerator.AddControlToPage(System.Guid,System.Web.UI.Control,System.String,System.String,System.Action<Telerik.Sitefinity.Pages.Model.PageDraftControl>)"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "TestForm")]
        private void AddFormControlToPage(Guid pageId, Guid formId)
        {
            var mvcProxy = new MvcControllerProxy();
            mvcProxy.ControllerName = typeof(FormController).FullName;
            var controller = new FormController();

            controller.Model.FormId = formId;
            controller.Model.ViewMode = FormViewMode.Write;

            mvcProxy.Settings = new ControllerSettings(controller);

            PageContentGenerator.AddControlToPage(pageId, mvcProxy, "TestForm", "Contentplaceholder1");
        }

        private Guid CreateFormWithWidget(Control widget)
        {
            var formId = Guid.NewGuid();

            string formSuccessMessage = "Test form success message";

            var formControls = new Dictionary<Control, string>();
            formControls.Add(widget, "Body");

            FormsModuleCodeSnippets.CreateForm(formId, "form_" + formId.ToString("N"), formId.ToString("N"), formSuccessMessage, formControls);

            return formId;
        }

        private void AddFormWidget(Guid formId, Control widget)
        {
            var formManager = FormsManager.GetManager();
            var form = formManager.GetForms().FirstOrDefault(f => f.Id == formId);
            var draft = formManager.EditForm(form.Id);
            var master = formManager.Lifecycle.CheckOut(draft);

            widget.ID = string.Format(CultureInfo.InvariantCulture, form.Name + "_C" + Guid.NewGuid().ToString("N", CultureInfo.InvariantCulture));
            var formControl = formManager.CreateControl<FormDraftControl>(widget, "Body");

            formControl.IsLayoutControl = widget is LayoutControl;
            formControl.SiblingId = FormsWidgetTests.GetLastControlInPlaceHolder(form, "Body");
            formControl.Caption = widget.GetType().Name;

            master.Controls.Add(formControl);

            master = formManager.Lifecycle.CheckIn(master);
            formManager.Lifecycle.Publish(master);

            formManager.SaveChanges();
        }
    }
}
