using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using MbUnit.Framework;
using Telerik.Sitefinity.Frontend.Forms.Mvc.Controllers;
using Telerik.Sitefinity.Frontend.Forms.Mvc.Models;
using Telerik.Sitefinity.Frontend.GridSystem;
using Telerik.Sitefinity.Frontend.TestUtilities;
using Telerik.Sitefinity.Frontend.TestUtilities.CommonOperations;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Mvc.Proxy;
using Telerik.Sitefinity.TestIntegration.Data.Content;
using Telerik.Sitefinity.TestIntegration.SDK.DevelopersGuide.SitefinityEssentials.Modules.Forms;

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
    }
}
