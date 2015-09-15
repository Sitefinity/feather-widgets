using System.Linq;
using FeatherWidgets.TestUtilities.CommonOperations;
using MbUnit.Framework;
using Telerik.Sitefinity.Frontend.GridSystem;
using Telerik.Sitefinity.Frontend.TestUtilities;
using Telerik.Sitefinity.Frontend.TestUtilities.CommonOperations;
using Telerik.Sitefinity.Modules.Pages;
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
            var formId = ServerOperationsFeather.Forms().CreateFormWithWidget(control);

            var pageManager = PageManager.GetManager();

            try
            {
                var semanticTemplate = pageManager.GetTemplates().FirstOrDefault(t => t.Name == "SemanticUI.default" && t.Title == "default");
                Assert.IsNotNull(semanticTemplate, "Template was not found");

                var semanticPageId = FeatherServerOperations.Pages().CreatePageWithTemplate(semanticTemplate, "FormsPageSemantic", "forms-page-semantic");
                ServerOperationsFeather.Forms().AddFormControlToPage(semanticPageId, formId);

                string semanticPageContent = FeatherServerOperations.Pages().GetPageContent(semanticPageId);

                Assert.IsTrue(semanticPageContent.Contains("class=\"sf_colsIn four wide column\""), "SemanticUI grid content not found.");

                var bootstrapTemplate = pageManager.GetTemplates().FirstOrDefault(t => t.Name == "Bootstrap.default" && t.Title == "default");
                Assert.IsNotNull(bootstrapTemplate, "Template was not found");

                var bootstrapPageId = FeatherServerOperations.Pages().CreatePageWithTemplate(bootstrapTemplate, "FormsPageBootstrap", "forms-page-bootstrap");
                ServerOperationsFeather.Forms().AddFormControlToPage(bootstrapPageId, formId);

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
            var formId = ServerOperationsFeather.Forms().CreateFormWithWidget(control);

            var pageManager = PageManager.GetManager();

            try
            {
                var template = pageManager.GetTemplates().FirstOrDefault(t => t.Name == "SemanticUI.default" && t.Title == "default");
                Assert.IsNotNull(template, "Template was not found");

                var pageId = FeatherServerOperations.Pages().CreatePageWithTemplate(template, "FormsPageCacheTest", "forms-page-cache-test");
                ServerOperationsFeather.Forms().AddFormControlToPage(pageId, formId);

                string pageContent = FeatherServerOperations.Pages().GetPageContent(pageId);

                Assert.IsTrue(pageContent.Contains("class=\"sf_colsIn four wide column\""), "SemanticUI grid content not found.");

                ServerOperationsFeather.Forms().AddFormWidget(formId, new GridControl() { Layout = "<div class=\"sf_colsIn\">Funny widget.</div>" });
                pageContent = FeatherServerOperations.Pages().GetPageContent(pageId);

                Assert.IsTrue(pageContent.Contains("Funny widget."), "Form did not render the new control.");
            }
            finally
            {
                Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.Pages().DeleteAllPages();
                FormsModuleCodeSnippets.DeleteForm(formId);
            }
        }
    }
}
