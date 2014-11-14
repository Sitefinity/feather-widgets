using System;
using DynamicContent.Mvc.Controllers;
using FeatherWidgets.TestUtilities.CommonOperations;
using MbUnit.Framework;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers;
using Telerik.Sitefinity.Mvc.Proxy;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.Web;

namespace FeatherWidgets.TestIntegration.DynamicWidgets
{
    /// <summary>
    /// This is a test class with tests related to dynamic widgets in List settings section.
    /// </summary>
    [TestFixture]
    [Description("This is a test class with tests related to dynamic widgets designer List settings section.")]
    public class DynamicWidgetsDesignerListSettingsTests
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
        [Description("Verify all items per page.")]
        public void DynamicWidgetsDesignerListSettings_VerifyUsePagingFunctionality()
        {
            this.CreatePressArticleAndReturnTagId();
            var dynamicCollection = ServerOperationsFeather.DynamicModulePressArticle().RetrieveCollectionOfPressArticles();

            this.pageOperations = new PagesOperations();
            string testName = System.Reflection.MethodInfo.GetCurrentMethod().Name;
            string pageNamePrefix = testName + "DynamicPage";
            string pageTitlePrefix = testName + "Dynamic Page";
            string urlNamePrefix = testName + "dynamic-page";
            int index = 1;
            int itemsPerPage = 1;
            string index2 = "/2";
            string index3 = "/3";
            string url1 = UrlPath.ResolveAbsoluteUrl("~/" + urlNamePrefix + index);
            string url2 = UrlPath.ResolveAbsoluteUrl("~/" + urlNamePrefix + index + index2);
            string url3 = UrlPath.ResolveAbsoluteUrl("~/" + urlNamePrefix + index + index3);

            var mvcProxy = new MvcWidgetProxy();
            mvcProxy.ControllerName = typeof(DynamicContentController).FullName;
            var dynamicController = new DynamicContentController();
            dynamicController.Model.ContentType = TypeResolutionService.ResolveType(ResolveType);
            dynamicController.Model.ItemsPerPage = itemsPerPage;
            mvcProxy.Settings = new ControllerSettings(dynamicController);
            mvcProxy.WidgetName = WidgetName;

            try
            {
                this.pageOperations.CreatePageWithControl(mvcProxy, pageNamePrefix, pageTitlePrefix, urlNamePrefix, index);

                string responseContent1 = PageInvoker.ExecuteWebRequest(url1);
                string responseContent2 = PageInvoker.ExecuteWebRequest(url2);
                string responseContent3 = PageInvoker.ExecuteWebRequest(url3);

                for (int i = 0; i < this.dynamicTitles.Length; i++)
                {
                    switch (i)
                    {
                        case 0:
                            Assert.IsTrue(responseContent3.Contains(this.dynamicTitles[i]), "The dynamic item with this title was not found!");
                            Assert.IsFalse(responseContent3.Contains(this.dynamicTitles[i + 1]), "The dynamic item with this title was found!");
                            Assert.IsFalse(responseContent3.Contains(this.dynamicTitles[i + 2]), "The dynamic item with this title was found!");
                            break;
                        case 1:
                            Assert.IsTrue(responseContent2.Contains(this.dynamicTitles[i]), "The dynamic item with this title was not found!");
                            Assert.IsFalse(responseContent2.Contains(this.dynamicTitles[i + 1]), "The dynamic item with this title was found!");
                            Assert.IsFalse(responseContent2.Contains(this.dynamicTitles[i - 1]), "The dynamic item with this title was found!");
                            break;
                        case 2:
                            Assert.IsTrue(responseContent1.Contains(this.dynamicTitles[i]), "The dynamic item with this title was not found!");
                            Assert.IsFalse(responseContent1.Contains(this.dynamicTitles[i - 1]), "The dynamic item with this title was found!");
                            Assert.IsFalse(responseContent1.Contains(this.dynamicTitles[i - 2]), "The dynamic item with this title was found!");
                            break;
                    }
                } 
            }
            finally
            {
                this.pageOperations.DeletePages();
                ServerOperationsFeather.DynamicModulePressArticle().DeletePressArticle(dynamicCollection);
                Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.Taxonomies().DeleteTags(this.tagTitle);
            }
        }

        [FixtureTearDown]
        public void Teardown()
        {
            Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.ModuleBuilder().DeleteModule(ModuleName, string.Empty, TransactionName);
        }

        private Guid[] CreatePressArticleAndReturnTagId()
        {
            Guid[] taxonId = new Guid[3];

            for (int i = 0; i < 3; i++)
            {
                taxonId[i] = Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.Taxonomies().CreateFlatTaxon(Telerik.Sitefinity.TestUtilities.CommonOperations.TaxonomiesConstants.TagsTaxonomyId, this.tagTitle[i]);
                ServerOperationsFeather.DynamicModulePressArticle().CreatePressArticle(this.dynamicTitles[i], this.dynamicUrls[i], taxonId[i]);
            }

            return taxonId;
        }

        #region Fields and constants

        private const string ModuleName = "Press Release";
        private const string ModuleResource = "FeatherWidgets.TestUtilities.Data.DynamicModules.PressRelease.zip";
        private const string ResolveType = "Telerik.Sitefinity.DynamicTypes.Model.PressRelease.PressArticle";
        private const string TransactionName = "Module Installations";
        private const string WidgetName = "PressArticle";
        private string[] dynamicTitles = { "Angel", "Boat", "Cat" };
        private string[] dynamicUrls = { "AngelUrl", "BoatUrl", "CatUrl" };
        private string[] tagTitle = { "Tag 0", "Tag 1", "Tag 2" };
        private PagesOperations pageOperations;

        #endregion
    }
}
