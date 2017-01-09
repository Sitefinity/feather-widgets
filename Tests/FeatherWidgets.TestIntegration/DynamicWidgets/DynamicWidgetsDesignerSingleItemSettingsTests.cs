using System;
using System.IO;
using System.Linq;
using System.Threading;
using FeatherWidgets.TestUtilities.CommonOperations;
using MbUnit.Framework;
using Telerik.Sitefinity.Frontend.DynamicContent.Mvc.Controllers;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers;
using Telerik.Sitefinity.Frontend.TestUtilities;
using Telerik.Sitefinity.Mvc.Proxy;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.Web;

namespace FeatherWidgets.TestIntegration.DynamicWidgets
{
    /// <summary>
    /// This is a test class with tests related to dynamic widgets in Single item settings section.
    /// </summary>
    [TestFixture]
    [Description("This is a test class with tests related to dynamic widgets designer Single item settings section.")]
    public class DynamicWidgetsDesignerSingleItemSettingsTests
    {
        [FixtureSetUp]
        public void Setup()
        {
            ServerOperationsFeather.DynamicModules().EnsureModuleIsImported(ModuleName, ModuleResource);
        }

        [Test]
        [Category(TestCategories.DynamicWidgets)]
        [Author(FeatherTeams.SitefinityTeam6)]
        [Description("Verify set detail template functionality.")]
        public void DynamicWidgetsDesignerSingleItemSettingsTests_SetDetailTemplate()
        {
            string detailTemplate = "PressArticleNew";
            string paragraphText = "Detail template";
            this.pageOperations = new PagesOperations();
            string testName = System.Reflection.MethodInfo.GetCurrentMethod().Name;
            string pageNamePrefix = testName + "DynamicPage";
            string pageTitlePrefix = testName + "Dynamic Page";
            string urlNamePrefix = testName + "dynamic-page";

            string fileDeatil = null;
            string fileList = null;

            var dynamicCollection = ServerOperationsFeather.DynamicModulePressArticle().RetrieveCollectionOfPressArticles();

            try
            {
                fileDeatil = this.CopyFile(DynamicFileName, DynamicFileFileResource);
                fileList = this.CopyFile(DynamicFileListName, DynamicFileListFileResource);

                for (int i = 0; i < this.dynamicTitles.Length; i++)
                {
                    string url = UrlPath.ResolveAbsoluteUrl("~/" + urlNamePrefix + i.ToString());

                    Guid itemId = ServerOperationsFeather.DynamicModulePressArticle().CreatePressArticleItem(this.dynamicTitles[i], this.dynamicUrls[i]).Id;
                    dynamicCollection = ServerOperationsFeather.DynamicModulePressArticle().RetrieveCollectionOfPressArticles().Where(item => item.OriginalContentId == itemId && item.Status == Telerik.Sitefinity.GenericContent.Model.ContentLifecycleStatus.Live).ToList();                    
                    var itemsToDelete = ServerOperationsFeather.DynamicModulePressArticle().RetrieveCollectionOfPressArticles().Where(item => item.Id != itemId && item.OriginalContentId != itemId).ToList();
                    ServerOperationsFeather.DynamicModulePressArticle().DeleteDynamicItems(itemsToDelete);

                    var mvcProxy = new MvcWidgetProxy();
                    mvcProxy.ControllerName = typeof(DynamicContentController).FullName;
                    var dynamicController = new DynamicContentController();
                    dynamicController.Model.ContentType = TypeResolutionService.ResolveType(ResolveType);
                    dynamicController.Model.ProviderName = ((Telerik.Sitefinity.Data.DataProviderBase)dynamicCollection.First().Provider).Name;
                    dynamicController.ListTemplateName = detailTemplate;
                    dynamicController.DetailTemplateName = detailTemplate;
                    dynamicController.Model.ProviderName = FeatherWidgets.TestUtilities.CommonOperations.DynamicModulesOperations.ProviderName;
                    mvcProxy.Settings = new ControllerSettings(dynamicController);
                    mvcProxy.WidgetName = WidgetName;

                    this.pageOperations.CreatePageWithControl(mvcProxy, pageNamePrefix, pageTitlePrefix, urlNamePrefix, i);

                    string detailNewsUrl = url + dynamicCollection[0].ItemDefaultUrl;
                    string responseContent = PageInvoker.ExecuteWebRequest(detailNewsUrl);

                    Assert.IsTrue(responseContent.Contains(paragraphText), "The paragraph text was not found!");

                    for (int j = 0; j < this.dynamicTitles.Length; j++)
                    {
                        if (j != i)
                        {
                            Assert.IsFalse(responseContent.Contains(this.dynamicTitles[j]), "The dynamic item with this title was found!");
                        }
                        else
                        {
                            Assert.IsTrue(responseContent.Contains(this.dynamicTitles[j]), "The dynamic item with this title was not found!");
                        }
                    }
                }
            }
            finally
            {
                File.Delete(fileDeatil);
                File.Delete(fileList);
                Directory.Delete(this.folderPath);
                this.pageOperations.DeletePages();
                dynamicCollection = ServerOperationsFeather.DynamicModulePressArticle().RetrieveCollectionOfPressArticles();
                ServerOperationsFeather.DynamicModulePressArticle().DeleteDynamicItems(dynamicCollection);
            }
        }

        [Test]
        [Category(TestCategories.DynamicWidgets)]
        [Author(FeatherTeams.SitefinityTeam6)]
        [Description("Verify when delete selected detail template functionality.")]
        public void DynamicWidgetsDesignerSingleItemSettingsTests_DeleteSelectedDetailTemplate()
        {
            string detailTemplate = "PressArticleNew";
            string paragraphText = "Detail template";
            this.pageOperations = new PagesOperations();
            string testName = System.Reflection.MethodInfo.GetCurrentMethod().Name;
            string pageNamePrefix = testName + "DynamicPage";
            string pageTitlePrefix = testName + "Dynamic Page";
            string urlNamePrefix = testName + "dynamic-page";

            string fileDeatil = null;
            string fileList = null;

            var dynamicCollection = ServerOperationsFeather.DynamicModulePressArticle().RetrieveCollectionOfPressArticles();

            try
            {
                fileDeatil = this.CopyFile(DynamicFileName, DynamicFileFileResource);
                fileList = this.CopyFile(DynamicFileListName, DynamicFileListFileResource);

                for (int i = 0; i < this.dynamicTitles.Length; i++)
                {
                    string url = UrlPath.ResolveAbsoluteUrl("~/" + urlNamePrefix + i.ToString());

                    Guid itemId = ServerOperationsFeather.DynamicModulePressArticle().CreatePressArticleItem(this.dynamicTitles[i], this.dynamicUrls[i]).Id;
                    dynamicCollection = ServerOperationsFeather.DynamicModulePressArticle().RetrieveCollectionOfPressArticles().Where(item => item.OriginalContentId == itemId && item.Status == Telerik.Sitefinity.GenericContent.Model.ContentLifecycleStatus.Live).ToList();
                    var itemsToDelete = ServerOperationsFeather.DynamicModulePressArticle().RetrieveCollectionOfPressArticles().Where(item => item.Id != itemId && item.OriginalContentId != itemId).ToList();
                    ServerOperationsFeather.DynamicModulePressArticle().DeleteDynamicItems(itemsToDelete);

                    var mvcProxy = new MvcWidgetProxy();
                    mvcProxy.ControllerName = typeof(DynamicContentController).FullName;
                    var dynamicController = new DynamicContentController();
                    dynamicController.Model.ContentType = TypeResolutionService.ResolveType(ResolveType);
                    dynamicController.Model.ProviderName = ((Telerik.Sitefinity.Data.DataProviderBase)dynamicCollection.First().Provider).Name;
                    dynamicController.ListTemplateName = detailTemplate;
                    dynamicController.DetailTemplateName = detailTemplate;
                    dynamicController.Model.ProviderName = FeatherWidgets.TestUtilities.CommonOperations.DynamicModulesOperations.ProviderName;
                    mvcProxy.Settings = new ControllerSettings(dynamicController);
                    mvcProxy.WidgetName = WidgetName;

                    this.pageOperations.CreatePageWithControl(mvcProxy, pageNamePrefix, pageTitlePrefix, urlNamePrefix, i);

                    File.Delete(fileDeatil);

                    string detailNewsUrl = url + dynamicCollection[0].ItemDefaultUrl;
                    string responseContent = PageInvoker.ExecuteWebRequest(detailNewsUrl);

                    Assert.IsFalse(responseContent.Contains(paragraphText), "The paragraph text was found!");

                    for (int j = 0; j < this.dynamicTitles.Length; j++)
                    {
                        if (j != i)
                        {
                            Assert.IsFalse(responseContent.Contains(this.dynamicTitles[j]), "The dynamic item with this title was found!");
                        }
                        else
                        {
                            Assert.IsTrue(responseContent.Contains(this.dynamicTitles[j]), "The dynamic item with this title was not found!");
                        }
                    }
                }
            }
            finally
            {                
                File.Delete(fileList);
                Directory.Delete(this.folderPath);
                this.pageOperations.DeletePages();
                dynamicCollection = ServerOperationsFeather.DynamicModulePressArticle().RetrieveCollectionOfPressArticles();
                ServerOperationsFeather.DynamicModulePressArticle().DeleteDynamicItems(dynamicCollection);
            }
        }

        [FixtureTearDown]
        public void Teardown()
        {
            Telerik.Sitefinity.Mvc.TestUtilities.CommonOperations.AuthenticationHelper.AuthenticateUser("admin@test.test", "admin@2");
            Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.ModuleBuilder().DeleteAllModules(string.Empty, TransactionName);
        }

        #region Helper methods

        public string CopyFile(string fileName, string fileResource)
        {
            if (!Directory.Exists(this.folderPath))
            {
                Directory.CreateDirectory(this.folderPath);
            }

            string filePath = Path.Combine(this.folderPath, fileName);
            ServerOperationsFeather.DynamicModules().AddNewResource(fileResource, filePath);
            Thread.Sleep(1000);

            return filePath;
        }

        #endregion

        #region Fields and constants

        private const string ModuleName = "Press Release";
        private const string ModuleResource = "FeatherWidgets.TestUtilities.Data.DynamicModules.PressRelease.zip";
        private const string ResolveType = "Telerik.Sitefinity.DynamicTypes.Model.PressRelease.PressArticle";
        private const string TransactionName = "Module Installations";
        private const string WidgetName = "PressArticle";
        private string[] dynamicTitles = { "Angel", "Boat", "Cat" };
        private string[] dynamicUrls = { "AngelUrl", "BoatUrl", "CatUrl" };
        private PagesOperations pageOperations;
        private const string DynamicFileFileResource = "FeatherWidgets.TestUtilities.Data.DynamicModules.Detail.PressArticleNew.cshtml";
        private const string DynamicFileName = "Detail.PressArticleNew.cshtml";
        private const string DynamicFileListFileResource = "FeatherWidgets.TestUtilities.Data.DynamicModules.List.PressArticleNew.cshtml";
        private const string DynamicFileListName = "List.PressArticleNew.cshtml";
        private string folderPath = Path.Combine(System.Web.Hosting.HostingEnvironment.MapPath("~/"), "MVC", "Views", "PressArticle");

        #endregion
    }
}
