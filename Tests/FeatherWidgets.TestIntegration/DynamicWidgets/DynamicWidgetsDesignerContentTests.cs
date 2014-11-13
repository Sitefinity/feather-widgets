using System;
using System.Linq;
using DynamicContent.Mvc.Controllers;
using FeatherWidgets.TestUtilities.CommonOperations;
using MbUnit.Framework;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers;
using Telerik.Sitefinity.Frontend.Mvc.Models;
using Telerik.Sitefinity.Mvc.Proxy;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.Web;

namespace FeatherWidgets.TestIntegration.DynamicWidgets
{
    /// <summary>
    /// This is a test class with tests related to dynamic widgets in toolbox section.
    /// </summary>
    [TestFixture]
    [Description("This is a test class with tests related to dynamic widgets designer Content section.")]
    public class DynamicWidgetsDesignerContentTests
    {
        [FixtureSetUp]
        public void Setup()
        {
            ServerOperationsFeather.DynamicModules().ImportModule(ModuleResource);
            Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.ModuleBuilder().ActivateModule(ModuleName, string.Empty, "Module Installations");

            for (int i = 0; i < 3; i++)
                ServerOperationsFeather.DynamicModulePressArticle().CreatePressArticle(this.dynamicTitles[i], this.dynamicUrls[i]);
        }

        [Test]
        [Category(TestCategories.DynamicWidgets)]
        [Author("FeatherTeam")]
        [Description("Verify all items per page.")]
        public void DynamicWidgetsDesignerContent_VerifyAllFunctionality()
        {
            this.pageOperations = new PagesOperations();
            string testName = System.Reflection.MethodInfo.GetCurrentMethod().Name;
            string pageNamePrefix = testName + "DynamicPage";
            string pageTitlePrefix = testName + "Dynamic Page";
            string urlNamePrefix = testName + "dynamic-page";
            int index = 1;
            string url = UrlPath.ResolveAbsoluteUrl("~/" + urlNamePrefix + index);

            var mvcProxy = new MvcWidgetProxy();
            mvcProxy.ControllerName = typeof(DynamicContentController).FullName;
            var dynamicController = new DynamicContentController();
            dynamicController.Model.ContentType = TypeResolutionService.ResolveType(ResolveType);
            dynamicController.Model.SelectionMode = SelectionMode.AllItems;
            mvcProxy.Settings = new ControllerSettings(dynamicController);
            mvcProxy.WidgetName = WidgetName;          

            try
            {
                this.pageOperations.CreatePageWithControl(mvcProxy, pageNamePrefix, pageTitlePrefix, urlNamePrefix, index);

                string responseContent = PageInvoker.ExecuteWebRequest(url);

                for (int i = 0; i < this.dynamicTitles.Length; i++)
                    Assert.IsTrue(responseContent.Contains(this.dynamicTitles[i]), "The dynamic item with this title was not found!");
            }
            finally
            {
                this.pageOperations.DeletePages();
            }
        }

        [Test]
        [Category(TestCategories.DynamicWidgets)]
        [Author("FeatherTeam")]
        [Description("Verify selected items per page.")]
        public void DynamicWidgetsDesignerContent_VerifySelectedItemsFunctionality()
        {
            var dynamicCollection = ServerOperationsFeather.DynamicModulePressArticle().RetrieveCollectionOfPressArticles();

            var mvcProxy = new MvcWidgetProxy();
            mvcProxy.ControllerName = typeof(DynamicContentController).FullName;
            var dynamicController = new DynamicContentController();
            dynamicController.Model.ContentType = TypeResolutionService.ResolveType(ResolveType);
            dynamicController.Model.SelectionMode = SelectionMode.SelectedItems;
            dynamicController.Model.SerializedSelectedItemsIds = "[\"" + dynamicCollection[3].Id.ToString() + "\"]";
            mvcProxy.Settings = new ControllerSettings(dynamicController);
            mvcProxy.WidgetName = WidgetName;          

           var modelItems = dynamicController.Model.CreateListViewModel(taxonFilter: null, page: 1);
           var dynamicItems = modelItems.Items.ToList();
           int itemsCount = dynamicItems.Count;

           Assert.AreEqual(1, itemsCount, "The count of the dynamic item is not as expected");          

           string title = dynamicItems[0].Title;
           Assert.IsTrue(title.Equals(this.dynamicTitles[1]), "The dynamic item with this title was not found!");               
        }

        [FixtureTearDown]
        public void Teardown()
        {
           Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.ModuleBuilder().DeleteModule(ModuleName, string.Empty, TransactionName);
        }

        #region Fields and constants

        private const string ModuleName = "Press Release";
        private const string ModuleResource = "FeatherWidgets.TestUtilities.Data.DynamicModules.PressRelease.zip";
        private const string ResolveType = "Telerik.Sitefinity.DynamicTypes.Model.PressRelease.PressArticle";
        private const string TransactionName = "Module Installations";
        private const string WidgetName = "PressArticle";
        private string[] dynamicTitles = { "Angel", "Boat", "Cat" };
        private string[] dynamicUrls = { "AngelUrl", "BoatUrl", "CatUrl" };        
        private PagesOperations pageOperations;

        #endregion
    }
}
