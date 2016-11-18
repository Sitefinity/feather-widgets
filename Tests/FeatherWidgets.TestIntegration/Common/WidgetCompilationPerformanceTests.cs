using System;
using System.IO;
using System.Reflection;
using System.Web.Compilation;
using FeatherWidgets.TestUtilities.CommonOperations;
using MbUnit.Framework;
using Telerik.Sitefinity.Frontend.ContentBlock.Mvc.Controllers;
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
    /// This class contains tests for the performance method region and tracking razor view compilations.
    /// </summary>
    [TestFixture]
    [Category(TestCategories.Common)]
    [Description("This class contains tests for the performance method region and tracking razor view compilations.")]
    public class WidgetCompilationPerformanceTests : ProfilingTestBase
    {
        #region Tests

        /// <summary>
        /// Verifies that when widget template is edited and page is requested the execution and the compilation of the MVC widgets is logged correctly.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "Telerik.Sitefinity.TestUtilities.CommonOperations.WidgetOperations.AddContentBlockToPage(System.Guid,System.String,System.String,System.String)"), Test]
        [Author(FeatherTeams.FeatherTeam)]
        [Description("Verifies that when widget template is edited and page is requested the execution and the compilation of the MVC widgets is logged correctly.")]
        public void ModifiedView_RequestPage_ShouldLogCompilation()
        {
            string viewFileName = "Default.cshtml";
            string widgetName = "ContentBlock";
            string filePath = FeatherServerOperations.ResourcePackages().GetResourcePackageMvcViewDestinationFilePath(ResourcePackages.Bootstrap, widgetName, viewFileName);

            PageNode pageNode = null;
            try
            {
                this.EnableProfiler("HttpRequestsProfiler");
                this.EnableProfiler("WidgetExecutionsProfiler");
                this.EnableProfiler("RazorViewCompilationsProfiler");

                Guid templateId = Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.Templates().GetTemplateIdByTitle(PageTemplateName);
                var pageId = ServerOperations.Pages().CreatePage("TestPage1", templateId);
                var pageNodeId = ServerOperations.Pages().GetPageNodeId(pageId);
                var pageManager = Telerik.Sitefinity.Modules.Pages.PageManager.GetManager();
                pageNode = pageManager.GetPageNode(pageNodeId);
                var fullPageUrl = RouteHelper.GetAbsoluteUrl(pageNode.GetUrl());

                int widgetCount = 3;
                for (var i = 0; i < widgetCount; i++)
                    ServerOperationsFeather.Pages().AddContentBlockWidgetToPage(pageNodeId, "ContentBlock", "Contentplaceholder1");

                this.ExecuteAuthenticatedRequest(fullPageUrl);
                this.FlushData();
                this.ClearData();

                var viewPath = "~/Frontend-Assembly/Telerik.Sitefinity.Frontend.ContentBlock/Mvc/Views/ContentBlock/Default.cshtml";
                var fullViewPath = string.Concat(viewPath, "#Bootstrap.cshtml");

                this.InvalidateAspNetRazorViewCache(fullViewPath, filePath);
                this.WaitForAspNetCacheToBeInvalidated(fullViewPath);

                // Request page
                this.ExecuteAuthenticatedRequest(fullPageUrl);
                this.FlushData();

                this.AssertWidgetExecutionCount(widgetCount);
                this.AssertViewCompilationCount(1);

                // Assert data
                var rootOperationId = this.GetRequestLogRootOperationId(fullPageUrl);

                var widgetCompilationText = "Compile view \"Default.cshtml#Bootstrap.cshtml\" of controller \"" + typeof(ContentBlockController).FullName + "\"";
                this.AssertViewCompilationParams(rootOperationId, viewPath, widgetCompilationText);

                this.ClearData();

                // Request page again
                this.ExecuteAuthenticatedRequest(fullPageUrl);
                this.FlushData();

                // Assert new data
                this.AssertWidgetExecutionCount(widgetCount);
                this.AssertViewCompilationCount(0);
            }
            finally
            {
                this.DeletePages(pageNode);
            }
        }

        /// <summary>
        /// Verifies that when widget template file is overwritten and page is requested the execution and the compilation of the MVC widget is logged.
        /// </summary>
        [Test]
        [Author(FeatherTeams.FeatherTeam)]
        [Description("Verifies that when widget template file is overwritten and page is requested the execution and the compilation of the MVC widget is logged.")]
        public void OverwrittenView_RequestPage_ShouldLogRazorViewCompilation()
        {
            string viewFileName = "Default.cshtml";
            string widgetName = "ContentBlock";

            string filePath = FeatherServerOperations.ResourcePackages().GetResourcePackageMvcViewDestinationFilePath(ResourcePackages.Bootstrap, widgetName, viewFileName);

            PageNode pageNode = null;
            try
            {
                this.EnableProfiler("HttpRequestsProfiler");
                this.EnableProfiler("WidgetExecutionsProfiler");
                this.EnableProfiler("RazorViewCompilationsProfiler");

                Guid templateId = Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.Templates().GetTemplateIdByTitle(PageTemplateName);
                var pageId = ServerOperations.Pages().CreatePage("TestPage1", templateId);
                var pageNodeId = ServerOperations.Pages().GetPageNodeId(pageId);
                var pageManager = Telerik.Sitefinity.Modules.Pages.PageManager.GetManager();
                pageNode = pageManager.GetPageNode(pageNodeId);
                var fullPageUrl = RouteHelper.GetAbsoluteUrl(pageNode.GetUrl());

                int widgetCount = 3;
                for (var i = 0; i < widgetCount; i++)
                    ServerOperationsFeather.Pages().AddContentBlockWidgetToPage(pageNodeId, "ContentBlock", "Contentplaceholder1");

                this.ExecuteAuthenticatedRequest(fullPageUrl);
                this.FlushData();
                this.ClearData();

                var viewPath = "~/Frontend-Assembly/Telerik.Sitefinity.Frontend.ContentBlock/Mvc/Views/ContentBlock/Default.cshtml";
                var fullViewPath = string.Concat(viewPath, "#Bootstrap.cshtml");

                this.OverwriteRazorViewFile(fullViewPath, filePath);
                this.WaitForAspNetCacheToBeInvalidated(fullViewPath);

                // request page
                this.ExecuteAuthenticatedRequest(fullPageUrl);
                this.FlushData();

                this.AssertWidgetExecutionCount(widgetCount);
                this.AssertViewCompilationCount(1);

                // Assert data
                var rootOperationId = this.GetRequestLogRootOperationId(fullPageUrl);

                var widgetCompilationText = "Compile view \"Default.cshtml#Bootstrap.cshtml\" of controller \"" + typeof(ContentBlockController).FullName + "\"";
                this.AssertViewCompilationParams(rootOperationId, viewPath, widgetCompilationText);

                this.ClearData();

                // Request page again
                this.ExecuteAuthenticatedRequest(fullPageUrl);
                this.FlushData();

                // Assert new data
                this.AssertWidgetExecutionCount(widgetCount);
                this.AssertViewCompilationCount(0);
            }
            finally
            {
                this.DeletePages(pageNode);
            }
        }

        #endregion
        
        #region Fields and Constants

        private struct ResourcePackages
        {
            public const string Bootstrap = "Bootstrap";
        }

        private const string WidgetViewPathFormat = "";
        private const string PageTemplateName = "Bootstrap.default";

        #endregion
    }
}