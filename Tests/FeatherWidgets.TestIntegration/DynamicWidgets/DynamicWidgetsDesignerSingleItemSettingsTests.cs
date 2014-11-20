using System.IO;
using System.Threading;
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
    /// This is a test class with tests related to dynamic widgets in Single item settings section.
    /// </summary>
    [TestFixture]
    [Description("This is a test class with tests related to dynamic widgets designer Single item settings section.")]
    public class DynamicWidgetsDesignerSingleItemSettingsTests
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
            int index = 1;
            string url = UrlPath.ResolveAbsoluteUrl("~/" + urlNamePrefix + index);

            string fileDeatil = null;
            string fileList = null;

            var dynamicCollection = ServerOperationsFeather.DynamicModulePressArticle().RetrieveCollectionOfPressArticles();

            try
            {
                fileDeatil = this.CopyFile(DynamicFileName, DynamicFileFileResource);
                fileList = this.CopyFile(DynamicFileListName, DynamicFileListFileResource);

                for (int i = 0; i < this.dynamicTitles.Length; i++)
                    ServerOperationsFeather.DynamicModulePressArticle().CreatePressArticleItem(this.dynamicTitles[i], this.dynamicUrls[i]);

                dynamicCollection = ServerOperationsFeather.DynamicModulePressArticle().RetrieveCollectionOfPressArticles();

                var mvcProxy = new MvcWidgetProxy();
                mvcProxy.ControllerName = typeof(DynamicContentController).FullName;
                var dynamicController = new DynamicContentController();
                dynamicController.Model.ContentType = TypeResolutionService.ResolveType(ResolveType);
                dynamicController.ListTemplateName = detailTemplate;
                dynamicController.DetailTemplateName = detailTemplate;
                mvcProxy.Settings = new ControllerSettings(dynamicController);
                mvcProxy.WidgetName = WidgetName;

                this.pageOperations.CreatePageWithControl(mvcProxy, pageNamePrefix, pageTitlePrefix, urlNamePrefix, index);

                string detailNewsUrl = url + dynamicCollection[0].ItemDefaultUrl; 
                string responseContent = PageInvoker.ExecuteWebRequest(detailNewsUrl);

                Assert.IsTrue(responseContent.Contains(paragraphText), "The paragraph text was not found!");

                Assert.IsTrue(responseContent.Contains(this.dynamicTitles[0]), "The dynamic item with this title was not found!");
                Assert.IsFalse(responseContent.Contains(this.dynamicTitles[1]), "The dynamic item with this title was found!");
                Assert.IsFalse(responseContent.Contains(this.dynamicTitles[2]), "The dynamic item with this title was found!");
            }
            finally
            {
                File.Delete(fileDeatil);
                File.Delete(fileList);
                Directory.Delete(this.folderPath);
                this.pageOperations.DeletePages();
                ServerOperationsFeather.DynamicModulePressArticle().DeletePressArticle(dynamicCollection);
            }
        }

        [FixtureTearDown]
        public void Teardown()
        {
            Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.ModuleBuilder().DeleteModule(ModuleName, string.Empty, TransactionName);
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
