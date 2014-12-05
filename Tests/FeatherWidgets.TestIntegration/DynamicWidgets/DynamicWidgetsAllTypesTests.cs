using FeatherWidgets.TestUtilities.CommonOperations;
using MbUnit.Framework;
using Telerik.Sitefinity.Frontend.DynamicContent.Mvc.Controllers;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers;
using Telerik.Sitefinity.Frontend.Mvc.Models;
using Telerik.Sitefinity.Mvc.Proxy;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.Web;

namespace FeatherWidgets.TestIntegration.DynamicWidgets
{
    /// <summary>
    /// This is a test class with tests related to all dynamic fields.
    /// </summary>
    [TestFixture]
    [Description("This is a test class with tests related to all dynamic fields.")]
    public class DynamicWidgetsAllTypesTests
    {
        [FixtureSetUp]
        public void Setup()
        {
            ServerOperationsFeather.DynamicModules().ImportModule(ModuleResource);
            Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.ModuleBuilder().ActivateModule(ModuleName, string.Empty, "Module Installations");
        }

        [Test]
        [Category(TestCategories.DynamicWidgets)]
        [Author("FeatherTeam")]
        [Description("Verify all fields on page.")]
        public void DynamicWidgetsAllTypes_VerifyAllFieldsOnTheFrontendWhereSomeFieldsAreEmpty()
        {
            this.pageOperations = new PagesOperations();
            string testName = System.Reflection.MethodInfo.GetCurrentMethod().Name;
            string pageNamePrefix = testName + "DynamicPage";
            string pageTitlePrefix = testName + "Dynamic Page";
            string urlNamePrefix = testName + "dynamic-page";
            int index = 1;
            string url = UrlPath.ResolveAbsoluteUrl("~/" + urlNamePrefix + index);
            var dynamicCollection = ServerOperationsFeather.DynamicModuleAllTypes().RetrieveCollectionOfAllFields();

            try
            {
                ServerOperationsFeather.DynamicModuleAllTypes().CreateFieldWithTitle(this.dynamicTitles, this.dynamicUrls);

                dynamicCollection = ServerOperationsFeather.DynamicModuleAllTypes().RetrieveCollectionOfAllFields();

                var mvcProxy = new MvcWidgetProxy();
                mvcProxy.ControllerName = typeof(DynamicContentController).FullName;
                var dynamicController = new DynamicContentController();
                dynamicController.Model.ContentType = TypeResolutionService.ResolveType(ResolveType);
                dynamicController.Model.SelectionMode = SelectionMode.AllItems;
                mvcProxy.Settings = new ControllerSettings(dynamicController);
                mvcProxy.WidgetName = WidgetName;

                this.pageOperations.CreatePageWithControl(mvcProxy, pageNamePrefix, pageTitlePrefix, urlNamePrefix, index);

                string responseContent = PageInvoker.ExecuteWebRequest(url);

                Assert.IsTrue(responseContent.Contains(this.dynamicTitles), "The dynamic item with this title was not found!");

                string detailNewsUrl = url + dynamicCollection[0].ItemDefaultUrl;
                string detailResponseContent = PageInvoker.ExecuteWebRequest(detailNewsUrl);

                Assert.IsTrue(detailResponseContent.Contains(this.dynamicTitles), "The title was not found!");
            }
            finally
            {
                this.pageOperations.DeletePages();
                ServerOperationsFeather.DynamicModulePressArticle().DeleteDynamicItems(dynamicCollection);
            }
        }

        [FixtureTearDown]
        public void Teardown()
        {
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

        #endregion
    }
}
