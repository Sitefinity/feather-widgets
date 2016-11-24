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
    [Category(TestCategories.RazorViewCompilation)]
    [Description("This class contains tests for tracking the razor view compilations of layout files.")]
    public class LayoutCompilationPerformanceTests : ProfilingTestBase
    {
        public override void TestSetUp()
        {
            base.TestSetUp();

            this.EnableProfiler("HttpRequestsProfiler");
            this.EnableProfiler("WidgetExecutionsProfiler");
            this.EnableProfiler("RazorViewCompilationsProfiler");
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
            PageNode pageNode = null;
            var layoutFilePath = System.Web.Hosting.HostingEnvironment.MapPath("~/ResourcePackages/Bootstrap/MVC/Views/Layouts/TestLayout.cshtml");

            try
            {
                this.CreateLayoutFolderAndCopyLayoutFile(layoutFilePath);
                pageNode = this.CreatePageWithMvcWidget(TestTemplateTitle, TestPlaceholder);
                var fullPageUrl = RouteHelper.GetAbsoluteUrl(pageNode.GetUrl());

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

                ServerOperations.Templates().DeletePageTemplate(TestTemplateTitle);

                using (new UnrestrictedModeRegion())
                {
                    File.Delete(layoutFilePath);
                }
            }
        }

        /// <summary>
        /// Verifies that when edit existing layout and page based on this layout is requested then a compilation for the layout is logged.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "Telerik.Sitefinity.TestUtilities.CommonOperations.WidgetOperations.AddContentBlockToPage(System.Guid,System.String,System.String,System.String)"), Test]
        [Author(FeatherTeams.FeatherTeam)]
        [Description("Verifies that when edit existing layout and page based on this layout is requested then a compilation for the layout is logged.")]
        public void EditLayout_RequestPageBasedOnTemplate_ShouldLogCompilation()
        {
            var templateTitle = "Bootstrap.default";
            var bootstrapPlaceholder = "Contentplaceholder1";
            var layoutName = "Bootstrap.default.cshtml";
            var layoutText = @"@Html.SfPlaceHolder(""Contentplaceholder1"")	";
            var layoutTextEdited = @"edited @Html.SfPlaceHolder(""Contentplaceholder1"") ";
            string filePath = FeatherServerOperations.ResourcePackages().GetResourcePackageMvcViewDestinationFilePath(Bootstrap, "Layouts", layoutName);
            PageNode pageNode = null;

            try
            {
                pageNode = this.CreatePageWithMvcWidget(templateTitle, bootstrapPlaceholder);
                var fullPageUrl = RouteHelper.GetAbsoluteUrl(pageNode.GetUrl());

                this.ExecuteAuthenticatedRequest(fullPageUrl);
                this.FlushData();
                this.ClearData();

                var viewPath = "~/Frontend-Assembly/Telerik.Sitefinity.Frontend/Mvc/Views/Layouts/Bootstrap.default.cshtml";
                FeatherServerOperations.ResourcePackages().EditLayoutFile(filePath, layoutText, layoutTextEdited);
                this.WaitForAspNetCacheToBeInvalidated(viewPath);

                this.ExecuteAuthenticatedRequest(fullPageUrl);
                this.FlushData();

                // Assert data
                this.AssertWidgetExecutionCount(1);
                this.AssertViewCompilationCount(1);

                var rootOperationId = this.GetRequestLogRootOperationId(fullPageUrl);

                var widgetCompilationText = "Compile view \"Bootstrap.default.cshtml#Bootstrap.cshtml\" of controller \"" + typeof(GenericController).FullName + "\"";
                this.AssertViewCompilationParams(rootOperationId, viewPath, widgetCompilationText);
            }
            finally
            {
                FeatherServerOperations.ResourcePackages().EditLayoutFile(filePath, layoutTextEdited, layoutText);
                this.DeletePages(pageNode);
                this.ClearData();
            }
        }

        /// <summary>
        /// Verifies that when new layout is added on root level and page based on this layout is requested then a compilation for the layout is logged.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "Telerik.Sitefinity.TestUtilities.CommonOperations.WidgetOperations.AddContentBlockToPage(System.Guid,System.String,System.String,System.String)"), Test]
        [Author(FeatherTeams.FeatherTeam)]
        [Description("Verifies that when new layout is added on root level and page based on this layout is requested then a compilation for the layout is logged.")]
        public void NewLayoutRoot_RequestPageBasedOnTemplate_ShouldLogCompilation()
        {
            PageNode pageNode = null;
            var layoutFileName = "TestLayout.cshtml";
            var folderPath = System.Web.Hosting.HostingEnvironment.MapPath("~/MVC/Views/Layouts");
            var layoutFilePath = Path.Combine(folderPath, layoutFileName);

            try
            {
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                this.CreateLayoutFolderAndCopyLayoutFile(layoutFilePath);
                pageNode = this.CreatePageWithMvcWidget(TestTemplateTitle, TestPlaceholder);
                var fullPageUrl = RouteHelper.GetAbsoluteUrl(pageNode.GetUrl());

                this.ClearData();
                this.ExecuteAuthenticatedRequest(fullPageUrl);
                this.FlushData();

                var viewPath = "~/Frontend-Assembly/Telerik.Sitefinity.Frontend/Mvc/Views/Layouts/TestLayout.cshtml";

                // Assert data
                this.AssertWidgetExecutionCount(1);
                this.AssertViewCompilationCount(1);

                var rootOperationId = this.GetRequestLogRootOperationId(fullPageUrl);

                var widgetCompilationText = "Compile view \"TestLayout.cshtml\" of controller \"" + typeof(GenericController).FullName + "\"";
                this.AssertViewCompilationParams(rootOperationId, viewPath, widgetCompilationText);
            }
            finally
            {
                this.DeletePages(pageNode);
                this.ClearData();

                ServerOperations.Templates().DeletePageTemplate(TestTemplateTitle);

                using (new UnrestrictedModeRegion())
                {
                    File.Delete(layoutFilePath);
                }
            }
        }

        #endregion

        #region Private Methods

        private void CreateLayoutFolderAndCopyLayoutFile(string layoutFilePath)
        {
            PageManager pageManager = PageManager.GetManager();
            int templatesCount = pageManager.GetTemplates().Count();

            using (new UnrestrictedModeRegion())
            {
                FeatherServerOperations.ResourcePackages().AddNewResource(FileResource, layoutFilePath);
                FeatherServerOperations.ResourcePackages().WaitForTemplatesCountToIncrease(templatesCount, 1);
            }
        }

        private PageNode CreatePageWithMvcWidget(string templateTitle, string widgetPlaceholder)
        {
            Guid templateId = Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.Templates().GetTemplateIdByTitle(templateTitle);
            var pageId = ServerOperations.Pages().CreatePage("TestPage1", templateId);
            var pageNodeId = ServerOperations.Pages().GetPageNodeId(pageId);
            var pageManager = Telerik.Sitefinity.Modules.Pages.PageManager.GetManager();
            var pageNode = pageManager.GetPageNode(pageNodeId);
            ServerOperationsFeather.Pages().AddContentBlockWidgetToPage(pageNodeId, LayoutCompilationPerformanceTests.WidgetName, widgetPlaceholder);

            return pageNode;
        }

        #endregion

        #region Fields and Constants

        private const string Bootstrap = "Bootstrap";
        private const string WidgetName = "ContentBlock";
        private const string TestTemplateTitle = "TestLayout";
        private const string TestPlaceholder = "TestPlaceHolder";
        private const string FileResource = "Telerik.Sitefinity.Frontend.TestUtilities.Data.TestLayout.cshtml";

        #endregion
    }
}