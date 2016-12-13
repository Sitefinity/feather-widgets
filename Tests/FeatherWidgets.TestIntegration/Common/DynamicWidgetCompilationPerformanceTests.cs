using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web.Hosting;
using FeatherWidgets.TestUtilities.CommonOperations;
using MbUnit.Framework;
using Telerik.Sitefinity.DynamicModules.Builder;
using Telerik.Sitefinity.DynamicModules.Web.UI.Frontend;
using Telerik.Sitefinity.Frontend.DynamicContent.Mvc.Controllers;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers;
using Telerik.Sitefinity.Frontend.Mvc.Models;
using Telerik.Sitefinity.Frontend.TestUtilities;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Mvc.Proxy;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.TestUtilities.CommonOperations;
using Telerik.Sitefinity.TestUtilities.Modules.Diagnostics;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.Web;

namespace FeatherWidgets.TestIntegration.Common
{
    /// <summary>
    /// This class contains tests for the performance method region and tracking razor view compilations of dynamic widgets.
    /// </summary>
    [TestFixture]
    [Category(TestCategories.Common)]
    [Category(TestCategories.RazorViewCompilation)]
    [Description("This class contains tests for the performance method region and tracking razor view compilations of dynamic widgets.")]
    public class DynamicWidgetCompilationPerformanceTests : ProfilingTestBase
    {
        #region Set up and Tear down

        /// <summary>
        /// Fixtures the set up.
        /// </summary>
        [FixtureSetUp]
        public override void FixtureSetUp()
        {
            base.FixtureSetUp();

            this.EnableProfiler("HttpRequestsProfiler");
            this.EnableProfiler("WidgetExecutionsProfiler");
            this.EnableProfiler("RazorViewCompilationsProfiler");

            ServerOperationsFeather.DynamicModules().EnsureModuleIsImported(ModuleName, ModuleResource);
        }

        /// <summary>
        /// Fixtures the tear down.
        /// </summary>
        [FixtureTearDown]
        public override void FixtureTearDown()
        {
            base.FixtureTearDown();

            ServerOperations.ModuleBuilder().DeleteAllModules(string.Empty, TransactionName);
        }

        #endregion

        #region Tests

        /// <summary>
        /// Verifies that fist-time request of page with dynamic widget logs the execution and the compilation of the widget.
        /// </summary>
        [Test]
        [Author(FeatherTeams.FeatherTeam)]
        [Description("Verifies that fist-time request of page with dynamic widget logs the execution and the compilation of the widget.")]
        public void DynamicWidget_RequestPage_ShouldLogRazorViewCompilation()
        {
            var widgetText = "</asp:PlaceHolder>";
            var addedText = "Some added text.";
            var widgetTextEdited = string.Concat(widgetText, addedText);            

            PageNode pageNode = null;
            try
            {
                pageNode = this.CreateBootstrapPageWithDynamicWidget();

                this.EditDynamicContentWidgetMasterTemplate(widgetText, widgetTextEdited);

                var url = RouteHelper.GetAbsoluteUrl(pageNode.GetUrl());
                this.ExecuteAuthenticatedRequest(url);
                this.FlushData();

                // Assert widget performance
                int widgetCount = 1;
                this.AssertWidgetExecutionCount(widgetCount);
                this.AssertViewCompilationCount(widgetCount);
            }
            finally
            {
                this.EditDynamicContentWidgetMasterTemplate(widgetTextEdited, widgetText);
                this.DeletePages(pageNode);
            }
        }

        /// <summary>
        /// Verifies that requesting a Bootstrap page with Bootstrap dynamic widget view logs the execution and the compilation of the dynamic widget.
        /// </summary>
        [Test]
        [Author(FeatherTeams.FeatherTeam)]
        [Description("Verifies that requesting a Bootstrap page with Bootstrap dynamic widget view logs the execution and the compilation of the dynamic widget.")]
        public void DynamicWidgetResourcePackage_RequestPage_ShouldLogRazorViewCompilation()
        {
            var text = "Expected text";
            var viewName = "List.Press Article.cshtml";
            var basePath = HostingEnvironment.MapPath("~/");

            var widgetViewsPath = Path.Combine(basePath, "ResourcePackages\\Bootstrap\\MVC\\Views\\PressArticle");
            if (!Directory.Exists(widgetViewsPath))
                Directory.CreateDirectory(widgetViewsPath);

            var listViewPath = Path.Combine(widgetViewsPath, viewName);

            PageNode pageNode = null;
            try
            {
                pageNode = this.CreateBootstrapPageWithDynamicWidget();

                var url = RouteHelper.GetAbsoluteUrl(pageNode.GetUrl());
                this.ExecuteAuthenticatedRequest(url);
                this.FlushData();
                this.ClearData();

                File.WriteAllText(listViewPath, text);

                var content = this.ExecuteAuthenticatedRequest(url);
                this.FlushData();

                // Assert widget performance
                Assert.Contains(content, text, StringComparison.Ordinal);

                int widgetCount = 1;
                this.AssertWidgetExecutionCount(widgetCount);
                this.AssertViewCompilationCount(widgetCount);
            }
            finally
            {
                this.DeletePages(pageNode);

                if (Directory.Exists(widgetViewsPath))
                    Directory.Delete(widgetViewsPath, true);
            }
        }

        /// <summary>
        /// Verifies that requesting a Bootstrap page with dynamic widget view in the MVC folder logs the execution and the compilation of the dynamic widget.
        /// </summary>
        [Test]
        [Author(FeatherTeams.FeatherTeam)]
        [Description("Verifies that requesting a Bootstrap page with dynamic widget view in the MVC folder logs the execution and the compilation of the dynamic widget.")]
        public void DynamicWidgetMvc_RequestPage_ShouldLogRazorViewCompilation()
        {
            var text = "Expected text";
            var viewName = "List.Press Article.cshtml";
            var basePath = HostingEnvironment.MapPath("~/");

            var widgetViewsPath = Path.Combine(basePath, "Mvc\\Views\\PressArticle");
            if (!Directory.Exists(widgetViewsPath))
                Directory.CreateDirectory(widgetViewsPath);

            var listViewPath = Path.Combine(widgetViewsPath, viewName);

            PageNode pageNode = null;
            try
            {
                pageNode = this.CreateBootstrapPageWithDynamicWidget();

                var url = RouteHelper.GetAbsoluteUrl(pageNode.GetUrl());
                this.ExecuteAuthenticatedRequest(url);
                this.FlushData();
                this.ClearData();

                File.WriteAllText(listViewPath, text);

                var content = this.ExecuteAuthenticatedRequest(url);
                this.FlushData();

                // Assert widget performance
                Assert.Contains(content, text, StringComparison.Ordinal);

                int widgetCount = 1;
                this.AssertWidgetExecutionCount(widgetCount);
                this.AssertViewCompilationCount(widgetCount);
            }
            finally
            {
                this.DeletePages(pageNode);

                if (Directory.Exists(widgetViewsPath))
                    Directory.Delete(widgetViewsPath, true);
            }
        }

        #endregion

        #region Private Methods

        private PageNode CreateBootstrapPageWithDynamicWidget()
        {
            Guid templateId = Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.Templates().GetTemplateIdByTitle(PageTemplateName);
            var pageId = ServerOperations.Pages().CreatePage("TestPage1", templateId);
            var pageNodeId = ServerOperations.Pages().GetPageNodeId(pageId);
            var pageManager = Telerik.Sitefinity.Modules.Pages.PageManager.GetManager();
            var pageNode = pageManager.GetPageNode(pageNodeId);
            var pageDraft = pageManager.EditPage(pageNode.PageId, CultureInfo.CurrentUICulture);

            var mvcProxy = new MvcWidgetProxy();
            mvcProxy.ControllerName = typeof(DynamicContentController).FullName;
            var dynamicController = new DynamicContentController();
            dynamicController.Model.ContentType = TypeResolutionService.ResolveType(ResolveType);
            dynamicController.Model.SelectionMode = SelectionMode.AllItems;
            dynamicController.Model.ProviderName = FeatherWidgets.TestUtilities.CommonOperations.DynamicModulesOperations.ProviderName;
            mvcProxy.Settings = new ControllerSettings(dynamicController);
            mvcProxy.WidgetName = WidgetName;

            var draftControlDefault = pageManager.CreateControl<PageDraftControl>(mvcProxy, "Body");
            draftControlDefault.Caption = string.Empty;
            pageManager.SetControlDefaultPermissions(draftControlDefault);
            pageDraft.Controls.Add(draftControlDefault);
            pageManager.PublishPageDraft(pageDraft, CultureInfo.CurrentUICulture);
            pageManager.SaveChanges();

            return pageNode;
        }

        private void EditDynamicContentWidgetMasterTemplate(string textToReplace, string textToReplaceWith)
        {
            var template = this.GetDynamicContentWidgetMasterTemplate();
            template.Data = template.Data.Replace(textToReplace, textToReplaceWith);
            PageManager.GetManager().SaveChanges();
        }

        private ControlPresentation GetDynamicContentWidgetMasterTemplate()
        {
            var moduleBuilderManager = ModuleBuilderManager.GetManager();
            var dynamicModuleType = moduleBuilderManager.Provider.GetDynamicModuleTypes().FirstOrDefault();
            var dynamicModule = moduleBuilderManager.Provider.GetDynamicModule(dynamicModuleType.ParentModuleId);
            var areaName = string.Format(CultureInfo.CurrentUICulture, "{0} - {1}", dynamicModule.Title, dynamicModuleType.DisplayName);

            return this.GetWidgetTemplate(typeof(DynamicContentViewMaster), areaName, dynamicModuleType.GetFullTypeName());
        }

        private ControlPresentation GetWidgetTemplate(Type controlType, string areaName, string typeFullName)
        {
            var pageManager = PageManager.GetManager();
            return pageManager.GetPresentationItems<ControlPresentation>()
                                            .FirstOrDefault(p =>
                                                p.DataType == Presentation.AspNetTemplate &&
                                                p.ControlType == controlType.FullName &&
                                                p.AreaName == areaName &&
                                                p.Condition == typeFullName);
        }

        #endregion

        #region Fields and Constants

        private const string WidgetName = "PressArticle";
        private const string ModuleName = "Press Release";
        private const string ModuleResource = "FeatherWidgets.TestUtilities.Data.DynamicModules.PressRelease.zip";
        private const string ResolveType = "Telerik.Sitefinity.DynamicTypes.Model.PressRelease.PressArticle";
        private const string TransactionName = "Module Installations";

        private const string PageTemplateName = "Bootstrap.default";

        #endregion
    }
}
