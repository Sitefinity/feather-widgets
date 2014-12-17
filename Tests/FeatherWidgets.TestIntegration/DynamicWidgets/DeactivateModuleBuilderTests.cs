using System;
using System.Threading;
using FeatherWidgets.TestUtilities.CommonOperations;
using MbUnit.Framework;
using Telerik.Sitefinity.Frontend.DynamicContent.Mvc.Controllers;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers;
using Telerik.Sitefinity.Frontend.Mvc.Models;
using Telerik.Sitefinity.Frontend.Navigation.Mvc.Controllers;
using Telerik.Sitefinity.Mvc.Proxy;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.Web;

namespace FeatherWidgets.TestIntegration.DynamicWidgets
{
    /// <summary>
    /// This is a test class with tests related to activate and deactivate dynamic module.
    /// </summary>
    [TestFixture]
    [Description("This is a test class with tests related to activate and deactivate dynamic module.")]
    public class DeactivateModuleBuilderTests
    {
        [FixtureSetUp]
        public void Setup()
        {
            Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.News().CreatePublishedNewsItem(NewsTitle, NewsContent, NewsProvider);

            ServerOperationsFeather.DynamicModules().ImportModule(ModuleResource);
            Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.ModuleBuilder().ActivateModule(ModuleName, string.Empty, "Module Installations");

            ServerOperationsFeather.DynamicModuleAllTypes().CreateFieldWithTitle(this.dynamicTitles, this.dynamicUrls);
        }

        [Test]
        [Category(TestCategories.DynamicWidgets)]
        [Author("FeatherTeam")]
        [Description("Verify news, navigation and content block on page after deactivate module builder.")]
        public void DeactivateModuleBuilder_DeactivateModuleBuilder()
        {
            this.pageOperations = new PagesOperations();
            string testName = System.Reflection.MethodInfo.GetCurrentMethod().Name;
            string pageNamePrefix = testName + "DynamicPage";
            string pageTitlePrefix = testName + "Dynamic Page";
            string urlNamePrefix = testName + "dynamic-page";
            int index = 1;
            string url = UrlPath.ResolveAbsoluteUrl("~/" + urlNamePrefix + index);

            try
            {
                var mvcProxy = new MvcWidgetProxy();
                mvcProxy.ControllerName = typeof(DynamicContentController).FullName;
                var dynamicController = new DynamicContentController();
                dynamicController.Model.ContentType = TypeResolutionService.ResolveType(ResolveType);
                dynamicController.Model.SelectionMode = SelectionMode.AllItems;
                mvcProxy.Settings = new ControllerSettings(dynamicController);
                mvcProxy.WidgetName = WidgetName;

                Guid pageId = this.pageOperations.CreatePageWithControl(mvcProxy, pageNamePrefix, pageTitlePrefix, urlNamePrefix, index);
              
                this.pageOperations.AddNewsWidgetToPage(pageId);

                this.pageOperations.AddContentBlockWidgetToPage(pageId, ContentBlockContent);

                Telerik.Sitefinity.Frontend.TestUtilities.CommonOperations.FeatherServerOperations.Pages().AddMvcWidgetToPage(pageId, typeof(NavigationController).FullName, WidgetCaption, PlaceHolderId);

                Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.StaticModules().DeactivateModule(SfModuleName);

                string responseContent = PageInvoker.ExecuteWebRequest(url);

                Assert.IsFalse(responseContent.Contains(this.dynamicTitles), "The dynamic item with this title was found!");
                Assert.IsTrue(responseContent.Contains(NewsTitle), "The news item with this title was not found!");
                Assert.IsTrue(responseContent.Contains(ContentBlockContent), "The content was not found!");
                Assert.IsTrue(responseContent.Contains(pageTitlePrefix), "The page with this title was not found!");
            }
            finally
            {
                Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.StaticModules().ActivateModule(SfModuleName);
                this.pageOperations.DeletePages();
            }
        }

        [FixtureTearDown]
        public void Teardown()
        {
            Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.News().DeleteAllNews();
            Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.ModuleBuilder().DeleteAllModules(string.Empty, TransactionName);
        }

        #region Fields and constants

        private const string ModuleName = "AllTypesModule";
        private const string ModuleResource = "FeatherWidgets.TestUtilities.Data.DynamicModules.AllTypesModule.zip";
        private const string ResolveType = "Telerik.Sitefinity.DynamicTypes.Model.AllTypesModule.Alltypes";
        private const string TransactionName = "Module Installations";
        private const string WidgetName = "AllTypes";
        private string dynamicTitles = "Angel";
        private string dynamicUrls = "AngelUrl";
        private PagesOperations pageOperations;
        private const string NewsContent = "News content";
        private const string NewsTitle = "NewsTitle";
        private const string NewsProvider = "Default News";
        private const string ContentBlockContent = "Test content";
        private const string WidgetCaption = "Navigation";
        private const string PlaceHolderId = "Body";
        private const string SfModuleName = "ModuleBuilder";

        #endregion
    }
}
