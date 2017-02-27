using System.Globalization;
using System.Linq;
using System.Web.Script.Serialization;
using FeatherWidgets.TestUtilities.CommonOperations;
using MbUnit.Framework;
using Telerik.Sitefinity.DynamicModules;
using Telerik.Sitefinity.Frontend.DynamicContent.Mvc.Controllers;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers;
using Telerik.Sitefinity.Frontend.Mvc.Models;
using Telerik.Sitefinity.Frontend.TestUtilities;
using Telerik.Sitefinity.Modules.Pages;
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
            ServerOperationsFeather.DynamicModules().EnsureModuleIsImported(ModuleName, ModuleResource);
        }

        [Test]
        [Category(TestCategories.DynamicWidgets)]
        [Author(FeatherTeams.SitefinityTeam6)]
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
                dynamicController.Model.ProviderName = FeatherWidgets.TestUtilities.CommonOperations.DynamicModulesOperations.ProviderName;
                mvcProxy.Settings = new ControllerSettings(dynamicController);
                mvcProxy.WidgetName = WidgetName;

                this.pageOperations.CreatePageWithControl(mvcProxy, pageNamePrefix, pageTitlePrefix, urlNamePrefix, index);

                string pageName = string.Format(CultureInfo.InvariantCulture, "{0}{1}", pageNamePrefix, index.ToString(CultureInfo.InvariantCulture));
                var pageNode = PageManager.GetManager().GetPageNodes().Where(p => p.Name == pageName).FirstOrDefault();
                Assert.IsNotNull(pageNode, "Page is null");
                var pagePublishedTranslations = pageNode.GetPageData().PublishedTranslations;

                var dynamicModuleManager = DynamicModuleManager.GetManager(FeatherWidgets.TestUtilities.CommonOperations.DynamicModulesOperations.ProviderName);
                var dynamicItem = dynamicModuleManager.GetDataItems(TypeResolutionService.ResolveType(ResolveType)).FirstOrDefault();
                Assert.IsNotNull(dynamicItem, "Dynamic item is null");
                var dynamicItemPublishedTranslations = dynamicItem.PublishedTranslations;

                if (dynamicItemPublishedTranslations.Count > 0)
                {
                    var serlizer = new JavaScriptSerializer();
                    string translationsAssertMsg = string.Format("Page languages ({0}) does not match dynamic item languages ({1})", serlizer.Serialize(pagePublishedTranslations), serlizer.Serialize(dynamicItemPublishedTranslations));
                    Assert.IsTrue(dynamicItemPublishedTranslations.Any(d => pagePublishedTranslations.Contains(d)), translationsAssertMsg);
                }

                string responseContent = PageInvoker.ExecuteWebRequest(url);

                Assert.IsNotNull(url, "Url is null");
                Assert.IsNotNull(responseContent, string.Format("Request to URL: '{0}' returned null as content", url));
                string assertMessage = string.Format("The dynamic item with {0} title was not found in response!", this.dynamicTitles);
                Assert.IsTrue(responseContent.Contains(this.dynamicTitles), assertMessage);

                string detailNewsUrl = url + dynamicCollection[0].ItemDefaultUrl;
                string detailResponseContent = PageInvoker.ExecuteWebRequest(detailNewsUrl);

                Assert.IsNotNull(detailResponseContent, string.Format("Request to URL: '{0}' returned null as content", detailNewsUrl));
                string detailAssertMessage = string.Format("The title {0} was not found in response!", this.dynamicTitles);
                Assert.IsTrue(detailResponseContent.Contains(this.dynamicTitles), detailAssertMessage);
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
