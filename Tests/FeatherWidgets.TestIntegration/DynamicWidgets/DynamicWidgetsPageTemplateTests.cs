using System;
using FeatherWidgets.TestUtilities.CommonOperations;
using FeatherWidgets.TestUtilities.CommonOperations.Templates;
using MbUnit.Framework;
using Telerik.Sitefinity.Frontend.DynamicContent.Mvc.Controllers;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers;
using Telerik.Sitefinity.Frontend.TestUtilities;
using Telerik.Sitefinity.Mvc.Proxy;
using Telerik.Sitefinity.TestIntegration.Data.Content;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.Web;

namespace FeatherWidgets.TestIntegration.DynamicWidgets
{
    /// <summary>
    /// This is a test class with tests related to dynamic widgets on page template.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1001:TypesThatOwnDisposableFieldsShouldBeDisposable"), TestFixture]
    [Description("This is a test class with tests related to dynamic widgets on page template.")]
    public class DynamicWidgetsPageTemplateTests
    {
        [FixtureSetUp]
        public void Setup()
        {
            ServerOperationsFeather.DynamicModules().EnsureModuleIsImported(ModuleName, ModuleResource);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "FeatherWidgets.TestUtilities.CommonOperations.Templates.TemplateOperations.AddControlToTemplate(System.Guid,System.Web.UI.Control,System.String,System.String)"), Test]
        [Category(TestCategories.DynamicWidgets)]
        [Author(FeatherTeams.Team2)]
        [Description("Verify use limit functionality.")]
        public void DynamicWidgetsPageTemplate_DynamicWidgetOnPageTemplate()
        {
            ServerOperationsFeather.DynamicModulePressArticle().CreatePressArticleItem(DynamicTitle, DynamicUrl);
            var dynamicCollection = ServerOperationsFeather.DynamicModulePressArticle().RetrieveCollectionOfPressArticles();

            string testName = System.Reflection.MethodInfo.GetCurrentMethod().Name;
            string pageNamePrefix = testName + "DynamicPage";
            string pageTitlePrefix = testName + "Dynamic Page";
            string urlNamePrefix = testName + "dynamic-page";
            string url = UrlPath.ResolveAbsoluteUrl("~/" + urlNamePrefix);
            Guid templateId = default(Guid);

            try
            {
                templateId = this.templateOperation.DuplicatePageTemplate(OldTemplateName, TemplateName);

                var mvcProxy = new MvcWidgetProxy();
                mvcProxy.ControllerName = typeof(DynamicContentController).FullName;
                var dynamicController = new DynamicContentController();
                dynamicController.Model.ContentType = TypeResolutionService.ResolveType(ResolveType);
                mvcProxy.Settings = new ControllerSettings(dynamicController);
                mvcProxy.WidgetName = WidgetName;

                this.templateOperation.AddControlToTemplate(templateId, mvcProxy, PlaceHolder, Widget);
                Guid pageId = this.locationGenerator.CreatePage(pageNamePrefix, pageTitlePrefix, urlNamePrefix, null, null);
                Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.Templates().SetTemplateToPage(pageId, templateId);

                string responseContent = PageInvoker.ExecuteWebRequest(url);

                Assert.IsTrue(responseContent.Contains(DynamicTitle), "The dynamic item with this title was not found!");
            }
            finally
            {
                Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.Pages().DeleteAllPages();
                Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.Templates().DeletePageTemplate(templateId);
                ServerOperationsFeather.DynamicModulePressArticle().DeleteDynamicItems(dynamicCollection);
            }
        }

        [FixtureTearDown]
        public void Teardown()
        {
            Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.ModuleBuilder().DeleteAllModules(string.Empty, TransactionName);
        }

        #region Fields and constants

        private const string ModuleName = "Press Release";
        private const string ModuleResource = "FeatherWidgets.TestUtilities.Data.DynamicModules.PressRelease.zip";
        private const string ResolveType = "Telerik.Sitefinity.DynamicTypes.Model.PressRelease.PressArticle";
        private const string TransactionName = "Module Installations";
        private const string WidgetName = "PressArticle";
        private const string DynamicTitle = "Angel";
        private const string DynamicUrl = "AngelUrl";
        private TemplateOperations templateOperation = new TemplateOperations();
        private PageContentGenerator locationGenerator = new PageContentGenerator();
        private const string OldTemplateName = "Bootstrap.default";
        private const string TemplateName = "NewTemplate";
        private const string DynamicFileFileResource = "FeatherWidgets.TestUtilities.Data.DynamicModules.List.PressArticleNew.cshtml";
        private const string DynamicFileName = "List.PressArticleNew.cshtml";
        private const string PlaceHolder = "Contentplaceholder1";
        private const string Widget = "Press Articles MVC";

        #endregion
    }
}
