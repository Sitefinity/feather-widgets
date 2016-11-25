using System;
using System.IO;
using FeatherWidgets.TestUtilities.CommonOperations;
using MbUnit.Framework;
using Telerik.Sitefinity.Frontend.Blogs.Mvc.Controllers;
using Telerik.Sitefinity.Frontend.ContentBlock.Mvc.Controllers;
using Telerik.Sitefinity.Frontend.News.Mvc.Controllers;
using Telerik.Sitefinity.Frontend.TestUtilities;
using Telerik.Sitefinity.Frontend.TestUtilities.CommonOperations;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Mvc.TestUtilities.Data;
using Telerik.Sitefinity.Pages.Model;
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
    [Category(TestCategories.RazorViewCompilation)]
    [Description("This class contains tests for the performance method region and tracking razor view compilations.")]
    public class WidgetCompilationPerformanceTests : ProfilingTestBase
    {
        #region Tests

        #region Widget on page

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

            var widgetText = @"@Html.Raw(Model.Content)";
            var widgetTextEdited = @"edited @Html.Raw(Model.Content)";
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

                var viewPath = "~/Frontend-Assembly/Telerik.Sitefinity.Frontend.ContentBlock/Mvc/Views/ContentBlock/Default.cshtml";
                var fullViewPath = string.Concat(viewPath, "#Bootstrap.cshtml");

                FeatherServerOperations.ResourcePackages().EditLayoutFile(filePath, widgetText, widgetTextEdited);
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
                FeatherServerOperations.ResourcePackages().EditLayoutFile(filePath, widgetTextEdited, widgetText);
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

            var widgetText = @"@Html.Raw(Model.Content)";
            var widgetTextEdited = @"edited @Html.Raw(Model.Content)";
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

                var viewPath = "~/Frontend-Assembly/Telerik.Sitefinity.Frontend.ContentBlock/Mvc/Views/ContentBlock/Default.cshtml";
                var fullViewPath = string.Concat(viewPath, "#Bootstrap.cshtml");

                // Overwrite the view file
                var originalContents = File.ReadAllText(filePath);
                var newContents = originalContents.Replace(widgetText, widgetTextEdited);
                File.Delete(filePath);
                File.WriteAllText(filePath, newContents);

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
                FeatherServerOperations.ResourcePackages().EditLayoutFile(filePath, widgetTextEdited, widgetText);
                this.DeletePages(pageNode);
            }
        }

        #endregion

        #region Widget on template

        /// <summary>
        /// Verifies that executions are logged for every MVC widget placed on the page or on its parent templates.
        /// </summary>
        [Test]
        [Author(FeatherTeams.FeatherTeam)]
        [Description("Verifies that executions are logged for every MVC widget placed on the page or on its parent templates.")]
        public void TemplateHierarchy_RequestPage_ShouldLogExecutions()
        {
            Guid templateId1 = default(Guid);
            Guid templateId2 = default(Guid);

            PageNode pageNode = null;
            try
            {
                this.EnableProfiler("HttpRequestsProfiler");
                this.EnableProfiler("WidgetExecutionsProfiler");
                this.EnableProfiler("RazorViewCompilationsProfiler");

                pageNode = this.CreatePageTemplateHierarchy(ref templateId1, ref templateId2);

                var fullPageUrl = RouteHelper.GetAbsoluteUrl(pageNode.GetUrl());

                // Request page
                this.ClearData();
                this.ExecuteAuthenticatedRequest(fullPageUrl);
                this.FlushData();

                this.AssertWidgetExecutionCount(3);
            }
            finally
            {
                this.DeletePages(pageNode);
                Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.Templates().DeletePageTemplate(templateId2);
                Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.Templates().DeletePageTemplate(templateId1);
            }
        }

        /// <summary>
        /// Verifies that executions and compilations are logged for the MVC widget placed on the page or on its parent templates.
        /// </summary>
        [Test]
        [Author(FeatherTeams.FeatherTeam)]
        [Description("Verifies that executions and compilations are logged for the MVC widget placed on the page or on its parent templates.")]
        public void TemplateHierarchy_EditWidget_ShouldLogCompilation()
        {
            Guid templateId1 = default(Guid);
            Guid templateId2 = default(Guid);

            var widgetName = "News";
            var widgetTemplateName = "List.NewsList.cshtml";
            var widgetText = @"<ul class=""list-unstyled"">";
            var widgetTextEdited = @"<ul class=""list-unstyled"">edited";
            string widgetFilePath = FeatherServerOperations.ResourcePackages().GetResourcePackageMvcViewDestinationFilePath(ResourcePackages.Bootstrap, widgetName, widgetTemplateName);

            PageNode pageNode = null;

            try
            {
                this.EnableProfiler("HttpRequestsProfiler");
                this.EnableProfiler("WidgetExecutionsProfiler");
                this.EnableProfiler("RazorViewCompilationsProfiler");

                pageNode = this.CreatePageTemplateHierarchy(ref templateId1, ref templateId2);
                var fullPageUrl = RouteHelper.GetAbsoluteUrl(pageNode.GetUrl());

                var viewPath = "~/Frontend-Assembly/Telerik.Sitefinity.Frontend.News/Mvc/Views/News/List.NewsList.cshtml";
                var fullViewPath = string.Concat(viewPath, "#Bootstrap.cshtml");

                // Request page
                this.ClearData();
                this.ExecuteAuthenticatedRequest(fullPageUrl);
                this.FlushData();

                this.ClearData();
                FeatherServerOperations.ResourcePackages().EditLayoutFile(widgetFilePath, widgetText, widgetTextEdited);
                this.WaitForAspNetCacheToBeInvalidated(fullViewPath);

                this.ExecuteAuthenticatedRequest(fullPageUrl);
                this.FlushData();

                this.AssertWidgetExecutionCount(3);
                this.AssertViewCompilationCount(1);

                var rootOperationId = this.GetRequestLogRootOperationId(fullPageUrl);

                var widgetCompilationText = "Compile view \"List.NewsList.cshtml#Bootstrap.cshtml\" of controller \"" + typeof(NewsController).FullName + "\"";
                this.AssertViewCompilationParams(rootOperationId, viewPath, widgetCompilationText);
            }
            finally
            {
                FeatherServerOperations.ResourcePackages().EditLayoutFile(widgetFilePath, widgetTextEdited, widgetText);
                this.DeletePages(pageNode);
                Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.Templates().DeletePageTemplate(templateId2);
                Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.Templates().DeletePageTemplate(templateId1);
            }
        }

        #endregion

        #endregion

        #region Helper methods

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "FeatherWidgets.TestUtilities.CommonOperations.Templates.TemplateOperations.AddControlToTemplate(System.Guid,System.Web.UI.Control,System.String,System.String)")]
        private PageNode CreatePageTemplateHierarchy(ref Guid templateId1, ref Guid templateId2)
        {
            var widgetName = "ContentBlock";
            var widgetName1 = "News widget";
            var widgetName2 = "Blog post widget";
            var template1Name = "Template1";
            var template2Name = "Template2";

            templateId1 = ServerOperationsFeather.TemplateOperations().DuplicatePageTemplate(PageTemplateName, template1Name);
            var mvcWidget1 = new Telerik.Sitefinity.Mvc.Proxy.MvcControllerProxy();
            mvcWidget1.ControllerName = typeof(NewsController).FullName;
            mvcWidget1.Settings = new Telerik.Sitefinity.Mvc.Proxy.ControllerSettings(new NewsController());
            ServerOperationsFeather.TemplateOperations().AddControlToTemplate(templateId1, mvcWidget1, BootstrapPlaceholder, widgetName1);

            templateId2 = ServerOperationsFeather.TemplateOperations().CreatePageTemplate(template2Name, templateId1);
            var mvcWidget2 = new Telerik.Sitefinity.Mvc.Proxy.MvcControllerProxy();
            mvcWidget2.ControllerName = typeof(BlogPostController).FullName;
            mvcWidget2.Settings = new Telerik.Sitefinity.Mvc.Proxy.ControllerSettings(new BlogPostController());
            mvcWidget2.ID = widgetName2.Replace(" ", string.Empty);
            ServerOperationsFeather.TemplateOperations().AddControlToTemplate(templateId2, mvcWidget2, BootstrapPlaceholder, widgetName2);

            var pageId = ServerOperations.Pages().CreatePage("TestPage1", templateId2);
            var pageNodeId = ServerOperations.Pages().GetPageNodeId(pageId);
            var pageManager = Telerik.Sitefinity.Modules.Pages.PageManager.GetManager();
            var pageNode = pageManager.GetPageNode(pageNodeId);
            ServerOperationsFeather.Pages().AddContentBlockWidgetToPage(pageNodeId, widgetName, BootstrapPlaceholder);

            return pageNode;
        }

        #endregion

        #region Fields and Constants

        private struct ResourcePackages
        {
            public const string Bootstrap = "Bootstrap";
        }

        private const string WidgetViewPathFormat = "";
        private const string PageTemplateName = "Bootstrap.default";
        private const string BootstrapPlaceholder = "Contentplaceholder1";

        #endregion
    }
}