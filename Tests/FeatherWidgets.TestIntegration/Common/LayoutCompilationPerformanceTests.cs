using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.Hosting;
using FeatherWidgets.TestUtilities.CommonOperations;
using MbUnit.Framework;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Frontend.Mvc.Controllers;
using Telerik.Sitefinity.Frontend.TestUtilities;
using Telerik.Sitefinity.Frontend.TestUtilities.CommonOperations;
using Telerik.Sitefinity.Model;
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
        #region SetUp and TearDown

        /// <summary>
        /// Set up method.
        /// </summary>
        [SetUp]
        public override void TestSetUp()
        {
            base.TestSetUp();

            this.EnableProfiler("HttpRequestsProfiler");
            this.EnableProfiler("WidgetExecutionsProfiler");
            this.EnableProfiler("RazorViewCompilationsProfiler");
        }

        #endregion

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

            try
            {
                this.CreateLayoutFile(TestTemplateFileName);

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

                this.ClearData();
                this.ExecuteAuthenticatedRequest(fullPageUrl);
                this.FlushData();

                this.AssertWidgetExecutionCount(1);
                this.AssertViewCompilationCount(0);
            }
            finally
            {
                this.DeletePages(pageNode);
                ServerOperations.Templates().DeletePageTemplate(TestTemplateTitle);
                this.DeleteLayoutFile(TestTemplateFileName);
            }
        }

        /// <summary>
        /// Verifies that when a new layout file is added, a template based on this layout is created and then a page based on this new template is requested, a compilation for the layout file is logged.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "Telerik.Sitefinity.TestUtilities.CommonOperations.WidgetOperations.AddContentBlockToPage(System.Guid,System.String,System.String,System.String)"), Test]
        [Author(FeatherTeams.FeatherTeam)]
        [Description("Verifies that when a new layout file is added, a template based on this layout is created and then a page based on this new template is requested, a compilation for the layout file is logged.")]
        public void TemplateBasedOnNewLayout_RequestPage_ShouldLogRazorViewCompilation()
        {
            string childTemplateTitle = "NewTestLayout";
            PageNode pageNode = null;

            try
            {
                this.CreateLayoutFile(TestTemplateFileName);
                this.CreatePureMvcPageTemplate(childTemplateTitle, TestTemplateTitle);

                pageNode = this.CreatePageWithMvcWidget(childTemplateTitle, TestPlaceholder);
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

                this.ClearData();
                this.ExecuteAuthenticatedRequest(fullPageUrl);
                this.FlushData();

                this.AssertWidgetExecutionCount(1);
                this.AssertViewCompilationCount(0);
            }
            finally
            {
                this.DeletePages(pageNode);

                ServerOperations.Templates().DeletePageTemplate(childTemplateTitle);
                ServerOperations.Templates().DeletePageTemplate(TestTemplateTitle);

                this.DeleteLayoutFile(TestTemplateFileName);
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

            try
            {
                this.CreateLayoutFile(TestTemplateFileName);
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

                this.ClearData();
                this.ExecuteAuthenticatedRequest(fullPageUrl);
                this.FlushData();

                this.AssertWidgetExecutionCount(1);
                this.AssertViewCompilationCount(0);
            }
            finally
            {
                this.DeletePages(pageNode);
                ServerOperations.Templates().DeletePageTemplate(TestTemplateTitle);
                this.DeleteLayoutFile(TestTemplateFileName);
            }
        }

        /// <summary>
        /// Verifies that when layout from the root folders is edited and page based on this layout is requested then a compilation for the layout is logged.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "Telerik.Sitefinity.TestUtilities.CommonOperations.WidgetOperations.AddContentBlockToPage(System.Guid,System.String,System.String,System.String)"), Test]
        [Author(FeatherTeams.FeatherTeam)]
        [Description("Verifies that when layout from the root folders is edited and page based on this layout is requested then a compilation for the layout is logged.")]
        public void EditLayoutRoot_RequestPageBasedOnTemplate_ShouldLogCompilation()
        {
            PageNode pageNode = null;
            var layoutText = @"@Html.SfPlaceHolder(""TestPlaceHolder"")	";
            var layoutTextEdited = @"edited @Html.SfPlaceHolder(""TestPlaceHolder"") ";

            try
            {
                this.CreateLayoutFile(TestTemplateFileName);
                pageNode = this.CreatePageWithMvcWidget(TestTemplateTitle, TestPlaceholder);
                var fullPageUrl = RouteHelper.GetAbsoluteUrl(pageNode.GetUrl());
                this.ExecuteAuthenticatedRequest(fullPageUrl);
                this.FlushData();

                var viewPath = "~/Frontend-Assembly/Telerik.Sitefinity.Frontend/Mvc/Views/Layouts/TestLayout.cshtml";
                var layoutFilePath = Path.Combine(HostingEnvironment.MapPath(LayoutsFolderRelativePath), TestTemplateFileName);
                FeatherServerOperations.ResourcePackages().EditLayoutFile(layoutFilePath, layoutText, layoutTextEdited);
                this.WaitForAspNetCacheToBeInvalidated(viewPath);

                this.ClearData();
                this.ExecuteAuthenticatedRequest(fullPageUrl);
                this.FlushData();

                // Assert data
                this.AssertWidgetExecutionCount(1);
                this.AssertViewCompilationCount(1);

                var rootOperationId = this.GetRequestLogRootOperationId(fullPageUrl);

                var widgetCompilationText = "Compile view \"TestLayout.cshtml#Bootstrap.cshtml\" of controller \"" + typeof(GenericController).FullName + "\"";
                this.AssertViewCompilationParams(rootOperationId, viewPath, widgetCompilationText);

                this.ClearData();
                this.ExecuteAuthenticatedRequest(fullPageUrl);
                this.FlushData();

                this.AssertWidgetExecutionCount(1);
                this.AssertViewCompilationCount(0);
            }
            finally
            {
                this.DeletePages(pageNode);
                ServerOperations.Templates().DeletePageTemplate(TestTemplateTitle);
                this.DeleteLayoutFile(TestTemplateFileName);
            }
        }

        #endregion

        #region Private Methods

        private void CreateLayoutFile(string layoutFileName)
        {
            PageManager pageManager = PageManager.GetManager();
            int initialTemplatesCount = pageManager.GetTemplates().Count();

            using (new UnrestrictedModeRegion())
            {
                var layoutsFolderPath = HostingEnvironment.MapPath(LayoutsFolderRelativePath);

                if (!Directory.Exists(layoutsFolderPath))
                    Directory.CreateDirectory(layoutsFolderPath);

                string layoutFilePath = Path.Combine(layoutsFolderPath, layoutFileName);

                if (File.Exists(layoutFilePath))
                    File.Delete(layoutFilePath);

                FeatherServerOperations.ResourcePackages().AddNewResource(FileResource, layoutFilePath);
                FeatherServerOperations.ResourcePackages().WaitForTemplatesCountToIncrease(initialTemplatesCount, 1);
            }
        }

        private void DeleteLayoutFile(string layoutFileName)
        {
            using (new UnrestrictedModeRegion())
            {
                var layoutsFolderPath = HostingEnvironment.MapPath(LayoutsFolderRelativePath);

                if (!Directory.Exists(layoutsFolderPath))
                    return;

                string layoutFilePath = Path.Combine(layoutsFolderPath, layoutFileName);

                if (!File.Exists(layoutFilePath))
                    return;

                Directory.Delete(layoutsFolderPath, recursive: true);
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

        private PageTemplate CreatePureMvcPageTemplate(string templateTitle, string parentTemplateTitle)
        {
            var pageManager = PageManager.GetManager();
            var parentTemplate = pageManager.GetTemplates().SingleOrDefault(t => t.Title == parentTemplateTitle);

            var template = pageManager.CreateTemplate();
            template.Title = templateTitle;
            template.Name = new Lstring(Regex.Replace(templateTitle, ArrangementConstants.UrlNameCharsToReplace, string.Empty).ToLower());
            template.Description = templateTitle + " descr";
            template.ParentTemplate = parentTemplate;
            template.ShowInNavigation = true;
            template.Framework = PageTemplateFramework.Mvc;
            template.Category = SiteInitializer.CustomTemplatesCategoryId;
            template.Visible = true;

            pageManager.SaveChanges();
            var draft = pageManager.TemplatesLifecycle.Edit(template);
            pageManager.TemplatesLifecycle.Publish(draft);
            pageManager.SaveChanges();

            return template;
        }

        #endregion

        #region Fields and Constants

        private const string Bootstrap = "Bootstrap";
        private const string WidgetName = "ContentBlock";
        private const string TestTemplateTitle = "TestLayout";
        private const string TestTemplateFileName = "TestLayout.cshtml";
        private const string TestPlaceholder = "TestPlaceHolder";
        private const string FileResource = "Telerik.Sitefinity.Frontend.TestUtilities.Data.TestLayout.cshtml";
        private const string LayoutsFolderRelativePath = "~/ResourcePackages/Bootstrap/MVC/Views/Layouts";

        #endregion
    }
}