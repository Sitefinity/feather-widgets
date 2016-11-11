using System;
using FeatherWidgets.TestUtilities.CommonOperations;
using MbUnit.Framework;
using Telerik.Sitefinity.Frontend.ContentBlock.Mvc.Controllers;
using Telerik.Sitefinity.Frontend.TestUtilities;
using Telerik.Sitefinity.Frontend.TestUtilities.CommonOperations;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.TestUtilities.CommonOperations;
using Telerik.Sitefinity.TestUtilities.Modules.Diagnostics;
using Telerik.Sitefinity.Web;

namespace FeatherWidgets.TestIntegration.Common
{
    /// <summary>
    /// This class contains tests for the performance method region and tracking razor view compilations.
    /// </summary>
    [TestFixture]
    [Category(TestCategories.Common)]
    [Description("This class contains tests for the performance method region and tracking razor view compilations.")]
    public class WidgetCompilationPerformanceTests : ProfilingTestBase
    {
        /// <summary>
        /// Tears down.
        /// </summary>
        [TearDown]
        public override void TestTearDown()
        {
            base.TestTearDown();

            ServerOperations.Pages().DeleteAllPages();
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "Telerik.Sitefinity.TestUtilities.CommonOperations.WidgetOperations.AddContentBlockToPage(System.Guid,System.String,System.String,System.String)"), Test]
        [Author(FeatherTeams.FeatherTeam)]
        [Description("Verifies that when widget template is edited and page is requested the execution and the compilation of the MVC widgets is logged correctly.")]
        public void EditView_RequestPage_CompilationLoggedCorrectly()
        {
            string packageName = "Bootstrap";
            string viewFileName = "Default.cshtml";
            string widgetName = "ContentBlock";

            var widgetText = @"@Html.Raw(Model.Content)";
            var widgetTextEdited = @"edited @Html.Raw(Model.Content)";
            string filePath = FeatherServerOperations.ResourcePackages().GetResourcePackageMvcViewDestinationFilePath(packageName, widgetName, viewFileName);

            try
            {
                this.EnableProfiler("HttpRequestProfiler");
                this.EnableProfiler("WidgetExecutionsProfiler");
                this.EnableProfiler("ViewCompilationsProfiler");

                Guid templateId = Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.Templates().GetTemplateIdByTitle(PageTemplateName);
                var pageId = ServerOperations.Pages().CreatePage("TestPage1", templateId);
                var pageNodeId = ServerOperations.Pages().GetPageNodeId(pageId);
                var pageManager = Telerik.Sitefinity.Modules.Pages.PageManager.GetManager();
                var pageNode = pageManager.GetPageNode(pageNodeId);
                var fullPageUrl = RouteHelper.GetAbsoluteUrl(pageNode.GetUrl());

                for (var i = 0; i < 3; i++)
                    ServerOperationsFeather.Pages().AddContentBlockWidgetToPage(pageNodeId, "ContentBlock", "Contentplaceholder1");

                FeatherServerOperations.ResourcePackages().EditLayoutFile(filePath, widgetText, widgetTextEdited);

                // request page
                this.ExecuteAuthenticatedRequest(fullPageUrl);
                this.FlushData();

                this.AssertWidgetExecutionCount(3);
                this.AssertViewCompilationCount(1);

                // Assert data
                var rootOperationId = this.GetRequestLogRootOperationId(fullPageUrl);
                
                var widgetCompilationText = "Compile view \"Default.cshtml#Bootstrap.cshtml\" of controller \"" + typeof(ContentBlockController).FullName + "\"";
                var viewPath = "~/Frontend-Assembly/Telerik.Sitefinity.Frontend.ContentBlock/Mvc/Views/ContentBlock/Default.cshtml";
                this.AssertViewCompilationParams(rootOperationId, viewPath, widgetCompilationText);
            }
            finally
            {
                FeatherServerOperations.ResourcePackages().EditLayoutFile(filePath, widgetTextEdited, widgetText);
            }
        }

        private const string PageTemplateName = "Bootstrap.default";
    }
}