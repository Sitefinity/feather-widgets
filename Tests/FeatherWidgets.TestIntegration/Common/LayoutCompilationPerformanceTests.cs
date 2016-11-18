using System;
using System.IO;
using System.Linq;
using FeatherWidgets.TestUtilities.CommonOperations;
using MbUnit.Framework;
using Telerik.Sitefinity.Frontend.Mvc.Controllers;
using Telerik.Sitefinity.Frontend.TestUtilities;
using Telerik.Sitefinity.Frontend.TestUtilities.CommonOperations;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Restriction;
using Telerik.Sitefinity.TestUtilities.CommonOperations;
using Telerik.Sitefinity.TestUtilities.Modules.Diagnostics;
using Telerik.Sitefinity.Web;

namespace FeatherWidgets.TestIntegration.Common
{
    /// <summary>
    /// This class contains tests for tracking the razor view compilations of layout files.
    /// </summary>
    [TestFixture]
    [Category(TestCategories.Common)]
    [Description("This class contains tests for tracking the razor view compilations of layout files.")]
    public class LayoutCompilationPerformanceTests : ProfilingTestBase
    {
        /// <summary>
        /// Test tear down.
        /// </summary>
        [TearDown]
        public override void TestTearDown()
        {
            base.TestTearDown();

            ServerOperations.Templates().DeletePageTemplate(TemplateTitle);

            using (new UnrestrictedModeRegion())
            {
                string filePath = this.GetFilePath();
                File.Delete(filePath);
            }
        }

        #region Tests

        /// <summary>
        /// Verifies that when new layout is added and page based on this layout is requested then a compilation for the layout is logged.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "Telerik.Sitefinity.TestUtilities.CommonOperations.WidgetOperations.AddContentBlockToPage(System.Guid,System.String,System.String,System.String)"), Test]
        [Author(FeatherTeams.FeatherTeam)]
        [Description("Verifies that when new layout is added and page based on this layout is requested then a compilation for the layout is logged.")]
        public void NewLayout_RequestPageBasedOnTemplate_ShouldLogCompilation()
        {
            string widgetName = "ContentBlock";
            PageNode pageNode = null;

            try
            {
                this.EnableProfiler("HttpRequestsProfiler");
                this.EnableProfiler("WidgetExecutionsProfiler");
                this.EnableProfiler("RazorViewCompilationsProfiler");

                this.CreateLayoutFolderAndCopyLayoutFile();
                Guid templateId = Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.Templates().GetTemplateIdByTitle(TemplateTitle);
                var pageId = ServerOperations.Pages().CreatePage("TestPage1", templateId);
                var pageNodeId = ServerOperations.Pages().GetPageNodeId(pageId);
                var pageManager = Telerik.Sitefinity.Modules.Pages.PageManager.GetManager();
                pageNode = pageManager.GetPageNode(pageNodeId);
                var fullPageUrl = RouteHelper.GetAbsoluteUrl(pageNode.GetUrl());

                ServerOperationsFeather.Pages().AddContentBlockWidgetToPage(pageNodeId, widgetName, "TestPlaceHolder");

                this.ClearData();
                this.ExecuteAuthenticatedRequest(fullPageUrl);
                this.FlushData();

                var viewPath = "~/Frontend-Assembly/Telerik.Sitefinity.Frontend/Mvc/Views/Layouts/TestLayout.cshtml";

                // Assert data
                this.AssertWidgetExecutionCount(1);
                this.AssertViewCompilationCount(1);

                var rootOperationId = this.GetRequestLogRootOperationId(fullPageUrl);

                var widgetCompilationText = "Compile view \"TestLayout.cshtml#Bootstrap.cshtml\" of controller \"" + typeof(GenericController).FullName + "\"";
                this.AssertViewCompilationParams(rootOperationId, viewPath, widgetCompilationText);
            }
            finally
            {
                this.DeletePages(pageNode);
                this.ClearData();
            }
        }

        #endregion

        #region Private Methods

        private string GetFilePath()
        {
            var path = System.Web.Hosting.HostingEnvironment.MapPath("~/ResourcePackages/Bootstrap/MVC/Views/Layouts/TestLayout.cshtml");

            return path;
        }

        private void CreateLayoutFolderAndCopyLayoutFile()
        {
            PageManager pageManager = PageManager.GetManager();
            int templatesCount = pageManager.GetTemplates().Count();

            string filePath = this.GetFilePath();

            using (new UnrestrictedModeRegion())
            {
                FeatherServerOperations.ResourcePackages().AddNewResource(FileResource, filePath);
                FeatherServerOperations.ResourcePackages().WaitForTemplatesCountToIncrease(templatesCount, 1);
            }
        }

        #endregion

        #region Fields and Constants

        private const string TemplateTitle = "TestLayout";
        private const string FileResource = "Telerik.Sitefinity.Frontend.TestUtilities.Data.TestLayout.cshtml";

        #endregion
    }
}