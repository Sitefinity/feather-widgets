﻿using System;
using System.IO;
using System.Linq;
using System.Threading;
using FeatherWidgets.TestUtilities.CommonOperations;
using MbUnit.Framework;
using Telerik.Sitefinity.Frontend.DynamicContent.Mvc.Controllers;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers;
using Telerik.Sitefinity.Frontend.Mvc.Models;
using Telerik.Sitefinity.Frontend.TestUtilities;
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
            ServerOperationsFeather.DynamicModules().EnsureModuleIsImported(ModuleName, ModuleResource);
        }

        [Test]
        [Category(TestCategories.DynamicWidgets)]
        [Author(FeatherTeams.SitefinityTeam6)]
        [Description("Verify paging functionality.")]
        public void DynamicWidgetsDesignerListSettings_VerifyUsePagingFunctionality()
        {
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

            try
            {
                for (int i = 0; i < this.dynamicTitles.Length; i++)
                    ServerOperationsFeather.DynamicModulePressArticle().CreatePressArticleItem(this.dynamicTitles[i], this.dynamicUrls[i]);

                dynamicCollection = ServerOperationsFeather.DynamicModulePressArticle().RetrieveCollectionOfPressArticles();

                var mvcProxy = new MvcWidgetProxy();
                mvcProxy.ControllerName = typeof(DynamicContentController).FullName;
                var dynamicController = new DynamicContentController();
                dynamicController.Model.ContentType = TypeResolutionService.ResolveType(ResolveType);
                dynamicController.Model.DisplayMode = ListDisplayMode.Paging;
                dynamicController.Model.ItemsPerPage = itemsPerPage;
                dynamicController.Model.ProviderName = FeatherWidgets.TestUtilities.CommonOperations.DynamicModulesOperations.ProviderName;
                mvcProxy.Settings = new ControllerSettings(dynamicController);
                mvcProxy.WidgetName = WidgetName;

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
                ServerOperationsFeather.DynamicModulePressArticle().DeleteDynamicItems(dynamicCollection);
            }
        }

        [Test]
        [Category(TestCategories.DynamicWidgets)]
        [Author(FeatherTeams.SitefinityTeam6)]
        [Description("Verify use limit functionality.")]
        public void DynamicWidgetsDesignerListSettings_VerifyUseLimitFunctionality()
        {
            var dynamicCollection = ServerOperationsFeather.DynamicModulePressArticle().RetrieveCollectionOfPressArticles();

            string testName = System.Reflection.MethodInfo.GetCurrentMethod().Name;
            string pageNamePrefix = testName + "DynamicPage";
            string pageTitlePrefix = testName + "Dynamic Page";
            string urlNamePrefix = testName + "dynamic-page";
            int index = 1;
            int limitCount = 1;
            string url = UrlPath.ResolveAbsoluteUrl("~/" + urlNamePrefix + index);

            try
            {
                for (int i = 0; i < this.dynamicTitles.Length; i++)
                    ServerOperationsFeather.DynamicModulePressArticle().CreatePressArticleItem(this.dynamicTitles[i], this.dynamicUrls[i]);

                dynamicCollection = ServerOperationsFeather.DynamicModulePressArticle().RetrieveCollectionOfPressArticles();

                this.pageOperations = new PagesOperations();

                var mvcProxy = new MvcWidgetProxy();
                mvcProxy.ControllerName = typeof(DynamicContentController).FullName;
                var dynamicController = new DynamicContentController();
                dynamicController.Model.ContentType = TypeResolutionService.ResolveType(ResolveType);
                dynamicController.Model.DisplayMode = ListDisplayMode.Limit;
                dynamicController.Model.LimitCount = limitCount;
                dynamicController.Model.ProviderName = FeatherWidgets.TestUtilities.CommonOperations.DynamicModulesOperations.ProviderName;
                mvcProxy.Settings = new ControllerSettings(dynamicController);
                mvcProxy.WidgetName = WidgetName;

                this.pageOperations.CreatePageWithControl(mvcProxy, pageNamePrefix, pageTitlePrefix, urlNamePrefix, index);
                string responseContent = PageInvoker.ExecuteWebRequest(url);

                Assert.IsTrue(responseContent.Contains(this.dynamicTitles[2]), "The dynamic item with this title was not found!");
                Assert.IsFalse(responseContent.Contains(this.dynamicTitles[0]), "The dynamic item with this title was found!");
                Assert.IsFalse(responseContent.Contains(this.dynamicTitles[1]), "The dynamic item with this title was found!");
            }
            finally
            {
                this.pageOperations.DeletePages();
                ServerOperationsFeather.DynamicModulePressArticle().DeleteDynamicItems(dynamicCollection);
            }
        }

        [Test]
        [Category(TestCategories.DynamicWidgets)]
        [Author(FeatherTeams.SitefinityTeam6)]
        [Description("Verify No limit and paging functionality.")]
        public void DynamicWidgetsDesignerListSettings_VerifyNoLimitAndPagingFunctionality()
        {
            var dynamicCollection = ServerOperationsFeather.DynamicModulePressArticle().RetrieveCollectionOfPressArticles();

            this.pageOperations = new PagesOperations();
            string testName = System.Reflection.MethodInfo.GetCurrentMethod().Name;
            string pageNamePrefix = testName + "DynamicPage";
            string pageTitlePrefix = testName + "Dynamic Page";
            string urlNamePrefix = testName + "dynamic-page";
            int index = 1;
            string url = UrlPath.ResolveAbsoluteUrl("~/" + urlNamePrefix + index);

            try
            {
                for (int i = 0; i < this.dynamicTitles.Length; i++)
                    ServerOperationsFeather.DynamicModulePressArticle().CreatePressArticleItem(this.dynamicTitles[i], this.dynamicUrls[i]);

                dynamicCollection = ServerOperationsFeather.DynamicModulePressArticle().RetrieveCollectionOfPressArticles();

                Assert.IsNotNull(dynamicCollection, "The collection of press articles was NULL.");

                this.pageOperations = new PagesOperations();

                var mvcProxy = new MvcWidgetProxy();
                mvcProxy.ControllerName = typeof(DynamicContentController).FullName;
                var dynamicController = new DynamicContentController();
                dynamicController.Model.ContentType = TypeResolutionService.ResolveType(ResolveType);
                dynamicController.Model.DisplayMode = ListDisplayMode.All;
                dynamicController.Model.ProviderName = FeatherWidgets.TestUtilities.CommonOperations.DynamicModulesOperations.ProviderName;
                mvcProxy.Settings = new ControllerSettings(dynamicController);
                mvcProxy.WidgetName = WidgetName;

                this.pageOperations.CreatePageWithControl(mvcProxy, pageNamePrefix, pageTitlePrefix, urlNamePrefix, index);
                string responseContent = PageInvoker.ExecuteWebRequest(url);

                for (int i = 0; i < this.dynamicTitles.Length; i++)
                {
                    Assert.IsTrue(responseContent.Contains(this.dynamicTitles[i]), "The dynamic item with this title was not found!");
                }
            }
            finally
            {
                this.pageOperations.DeletePages();
                ServerOperationsFeather.DynamicModulePressArticle().DeleteDynamicItems(dynamicCollection);
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Telerik.Sitefinity", "SF1002:AvoidToListOnIEnumerable"), Test]
        [Category(TestCategories.DynamicWidgets)]
        [Author(FeatherTeams.SitefinityTeam6)]
        [Description("Verify sort Descending functionality.")]
        public void DynamicWidgetsDesignerListSettings_VerifySortDescending()
        {
            string sortExpession = "Title DESC";
            var dynamicCollection = ServerOperationsFeather.DynamicModulePressArticle().RetrieveCollectionOfPressArticles();

            try
            {
                for (int i = 0; i < this.dynamicTitles.Length; i++)
                    ServerOperationsFeather.DynamicModulePressArticle().CreatePressArticleItem(this.dynamicTitles[i], this.dynamicUrls[i]);

                dynamicCollection = ServerOperationsFeather.DynamicModulePressArticle().RetrieveCollectionOfPressArticles();

                var mvcProxy = new MvcWidgetProxy();
                mvcProxy.ControllerName = typeof(DynamicContentController).FullName;
                var dynamicController = new DynamicContentController();
                dynamicController.Model.ContentType = TypeResolutionService.ResolveType(ResolveType);
                dynamicController.Model.SortExpression = sortExpession;
                dynamicController.Model.ProviderName = FeatherWidgets.TestUtilities.CommonOperations.DynamicModulesOperations.ProviderName;
                mvcProxy.Settings = new ControllerSettings(dynamicController);
                mvcProxy.WidgetName = WidgetName;

                var modelItems = dynamicController.Model.CreateListViewModel(taxonFilter: null, page: 1);
                var dynamicItems = modelItems.Items.ToList();
                int itemsCount = dynamicItems.Count;
                string[] dynamicTitlesResults = new string[itemsCount];

                Assert.AreEqual(this.dynamicTitles.Length, itemsCount, "The count of the dynamic item is not as expected");

                for (int i = 0; i < itemsCount; i++)
                    dynamicTitlesResults[i] = dynamicItems[i].Fields.Title;

                int lastIndex = itemsCount - 1;
                for (int i = 0; i < this.dynamicTitles.Length; i++)
                {
                    Assert.IsTrue(dynamicTitlesResults[i].Equals(this.dynamicTitles[lastIndex]), "The dynamic item with this title was not found!");
                    lastIndex--;
                }
            }
            finally
            {
                ServerOperationsFeather.DynamicModulePressArticle().DeleteDynamicItems(dynamicCollection);
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Telerik.Sitefinity", "SF1002:AvoidToListOnIEnumerable"), Test]
        [Category(TestCategories.DynamicWidgets)]
        [Author(FeatherTeams.SitefinityTeam6)]
        [Description("Verify sort Ascending functionality.")]
        public void DynamicWidgetsDesignerListSettings_VerifySortAscending()
        {
            string sortExpession = "Title ASC";
            string[] dynamicTitlesDescending = { "Cat", "Boat", "Angel" };
            string[] dynamicUrlsDescending = { "CatUrl", "BoatUrl", "AngelUrl" };

            var dynamicCollection = ServerOperationsFeather.DynamicModulePressArticle().RetrieveCollectionOfPressArticles();

            try
            {
                for (int i = 0; i < dynamicTitlesDescending.Length; i++)
                    ServerOperationsFeather.DynamicModulePressArticle().CreatePressArticleItem(dynamicTitlesDescending[i], dynamicUrlsDescending[i]);

                dynamicCollection = ServerOperationsFeather.DynamicModulePressArticle().RetrieveCollectionOfPressArticles();

                var mvcProxy = new MvcWidgetProxy();
                mvcProxy.ControllerName = typeof(DynamicContentController).FullName;
                var dynamicController = new DynamicContentController();
                dynamicController.Model.ContentType = TypeResolutionService.ResolveType(ResolveType);
                dynamicController.Model.SortExpression = sortExpession;
                dynamicController.Model.ProviderName = FeatherWidgets.TestUtilities.CommonOperations.DynamicModulesOperations.ProviderName;
                mvcProxy.Settings = new ControllerSettings(dynamicController);
                mvcProxy.WidgetName = WidgetName;

                var modelItems = dynamicController.Model.CreateListViewModel(taxonFilter: null, page: 1);
                var dynamicItems = modelItems.Items.ToList();
                int itemsCount = dynamicItems.Count;
                string[] dynamicTitlesResults = new string[itemsCount];

                Assert.AreEqual(dynamicTitlesDescending.Length, itemsCount, "The count of the dynamic item is not as expected");

                for (int i = 0; i < itemsCount; i++)
                    dynamicTitlesResults[i] = dynamicItems[i].Fields.Title;

                int lastIndex = itemsCount - 1;
                for (int i = 0; i < dynamicTitlesDescending.Length; i++)
                {
                    Assert.IsTrue(dynamicTitlesResults[i].Equals(dynamicTitlesDescending[lastIndex]), "The dynamic item with this title was not found!");
                    lastIndex--;
                }
            }
            finally
            {
                ServerOperationsFeather.DynamicModulePressArticle().DeleteDynamicItems(dynamicCollection);
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Telerik.Sitefinity", "SF1002:AvoidToListOnIEnumerable"), Test]
        [Category(TestCategories.DynamicWidgets)]
        [Author(FeatherTeams.SitefinityTeam6)]
        [Description("Verify sort Last modified functionality.")]
        public void DynamicWidgetsDesignerListSettings_VerifySortLastModified()
        {
            string sortExpession = "LastModified DESC";
            string newTitle = "Boat New Name";

            var dynamicCollection = ServerOperationsFeather.DynamicModulePressArticle().RetrieveCollectionOfPressArticles();

            try
            {
                for (int i = 0; i < this.dynamicTitles.Length; i++)
                    ServerOperationsFeather.DynamicModulePressArticle().CreatePressArticleItem(this.dynamicTitles[i], this.dynamicUrls[i]);

                dynamicCollection = ServerOperationsFeather.DynamicModulePressArticle().RetrieveCollectionOfPressArticles();

                ServerOperationsFeather.DynamicModulePressArticle().EditPressArticleTitle(dynamicCollection[2], newTitle);

                var mvcProxy = new MvcWidgetProxy();
                mvcProxy.ControllerName = typeof(DynamicContentController).FullName;
                var dynamicController = new DynamicContentController();
                dynamicController.Model.ContentType = TypeResolutionService.ResolveType(ResolveType);
                dynamicController.Model.SortExpression = sortExpession;
                dynamicController.Model.ProviderName = FeatherWidgets.TestUtilities.CommonOperations.DynamicModulesOperations.ProviderName;
                mvcProxy.Settings = new ControllerSettings(dynamicController);
                mvcProxy.WidgetName = WidgetName;

                var modelItems = dynamicController.Model.CreateListViewModel(taxonFilter: null, page: 1);
                var dynamicItems = modelItems.Items.ToList();
                int itemsCount = dynamicItems.Count;
                string[] dynamicTitlesResults = new string[itemsCount];

                Assert.AreEqual(this.dynamicTitles.Length, itemsCount, "The count of the dynamic item is not as expected");

                for (int i = 0; i < itemsCount; i++)
                    dynamicTitlesResults[i] = dynamicItems[i].Fields.Title;

                Assert.IsTrue(dynamicTitlesResults[0].Equals(newTitle), "The dynamic item with this title was not found!");
                Assert.IsFalse(dynamicTitlesResults[0].Equals(this.dynamicTitles[1]), "The dynamic item with this title was not found!");
                Assert.IsTrue(dynamicTitlesResults[1].Equals(this.dynamicTitles[2]), "The dynamic itemwith this title was not found!");
                Assert.IsTrue(dynamicTitlesResults[2].Equals(this.dynamicTitles[0]), "The dynamic item with this title was not found!");
            }
            finally
            {
                ServerOperationsFeather.DynamicModulePressArticle().DeleteDynamicItems(dynamicCollection);
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Telerik.Sitefinity", "SF1002:AvoidToListOnIEnumerable"), Test]
        [Category(TestCategories.DynamicWidgets)]
        [Author(FeatherTeams.SitefinityTeam6)]
        [Description("Verify sort Publication date descending functionality.")]
        public void DynamicWidgetsDesignerListSettings_VerifyPublicationDateDescending()
        {
            string sortExpession = "PublicationDate DESC";
            DateTime publicationDateAngel = new DateTime(2014, 9, 10, 12, 00, 00);
            DateTime publicationDateBoat = new DateTime(2014, 10, 23, 12, 00, 00);
            DateTime publicationDateCat = new DateTime(2014, 11, 18, 12, 00, 00);

            var dynamicCollection = ServerOperationsFeather.DynamicModulePressArticle().RetrieveCollectionOfPressArticles();

            try
            {
                for (int i = 0; i < this.dynamicTitles.Length; i++)
                    ServerOperationsFeather.DynamicModulePressArticle().CreatePressArticleItem(this.dynamicTitles[i], this.dynamicUrls[i]);

                dynamicCollection = ServerOperationsFeather.DynamicModulePressArticle().RetrieveCollectionOfPressArticles();

                ServerOperationsFeather.DynamicModulePressArticle().PublishPressArticleWithSpecificDate(dynamicCollection[4], publicationDateAngel);
                ServerOperationsFeather.DynamicModulePressArticle().PublishPressArticleWithSpecificDate(dynamicCollection[2], publicationDateBoat);
                ServerOperationsFeather.DynamicModulePressArticle().PublishPressArticleWithSpecificDate(dynamicCollection[0], publicationDateCat);

                var mvcProxy = new MvcWidgetProxy();
                mvcProxy.ControllerName = typeof(DynamicContentController).FullName;
                var dynamicController = new DynamicContentController();
                dynamicController.Model.ContentType = TypeResolutionService.ResolveType(ResolveType);
                dynamicController.Model.SortExpression = sortExpession;
                dynamicController.Model.ProviderName = FeatherWidgets.TestUtilities.CommonOperations.DynamicModulesOperations.ProviderName;
                mvcProxy.Settings = new ControllerSettings(dynamicController);
                mvcProxy.WidgetName = WidgetName;
                
                var modelItems = dynamicController.Model.CreateListViewModel(taxonFilter: null, page: 1);
                var dynamicItems = modelItems.Items.ToList();
                int itemsCount = dynamicItems.Count;
                string[] dynamicTitlesResults = new string[itemsCount];

                Assert.AreEqual(this.dynamicTitles.Length, itemsCount, "The count of the dynamic item is not as expected");

                for (int i = 0; i < itemsCount; i++)
                    dynamicTitlesResults[i] = dynamicItems[i].Fields.Title;

                for (int i = 0; i < dynamicTitlesResults.Length; i++)
                    Assert.IsTrue(dynamicTitlesResults[i].Equals(this.dynamicTitles[i]), "The dynamic item with this title was not found!");
            }
            finally
            {
                ServerOperationsFeather.DynamicModulePressArticle().DeleteDynamicItems(dynamicCollection);
            }
        }

        [Test]
        [Category(TestCategories.DynamicWidgets)]
        [Author(FeatherTeams.SitefinityTeam6)]
        [Description("Verify select list template functionality.")]
        public void DynamicWidgetsDesignerListSettings_SetListTemplate()
        {          
            string listTemplate = "PressArticleNew";
            string paragraphText = "List template";
            this.pageOperations = new PagesOperations();
            string testName = System.Reflection.MethodInfo.GetCurrentMethod().Name;
            string pageNamePrefix = testName + "DynamicPage";
            string pageTitlePrefix = testName + "Dynamic Page";
            string urlNamePrefix = testName + "dynamic-page";
            int index = 1;
            string url = UrlPath.ResolveAbsoluteUrl("~/" + urlNamePrefix + index);

            string file = null;

            var dynamicCollection = ServerOperationsFeather.DynamicModulePressArticle().RetrieveCollectionOfPressArticles();

            try
            {
                file = this.CopyFile();

                for (int i = 0; i < this.dynamicTitles.Length; i++)
                    ServerOperationsFeather.DynamicModulePressArticle().CreatePressArticleItem(this.dynamicTitles[i], this.dynamicUrls[i]);

                dynamicCollection = ServerOperationsFeather.DynamicModulePressArticle().RetrieveCollectionOfPressArticles();

                var mvcProxy = new MvcWidgetProxy();
                mvcProxy.ControllerName = typeof(DynamicContentController).FullName;
                var dynamicController = new DynamicContentController();
                dynamicController.Model.ContentType = TypeResolutionService.ResolveType(ResolveType);
                dynamicController.ListTemplateName = listTemplate;
                dynamicController.Model.ProviderName = FeatherWidgets.TestUtilities.CommonOperations.DynamicModulesOperations.ProviderName;
                mvcProxy.Settings = new ControllerSettings(dynamicController);
                mvcProxy.WidgetName = WidgetName;

                this.pageOperations.CreatePageWithControl(mvcProxy, pageNamePrefix, pageTitlePrefix, urlNamePrefix, index);
                string responseContent = PageInvoker.ExecuteWebRequest(url);

                Assert.IsTrue(responseContent.Contains(paragraphText), "The paragraph text was not found!");

                for (int i = 0; i < this.dynamicTitles.Length; i++)
                    Assert.IsTrue(responseContent.Contains(this.dynamicTitles[i]), "The dynamic item with this title was not found!");
            }
            finally
            {
                this.pageOperations.DeletePages();
                ServerOperationsFeather.DynamicModulePressArticle().DeleteDynamicItems(dynamicCollection);
                File.Delete(file);
                Directory.Delete(this.folderPath);
            }
        }

        [Test]
        [Category(TestCategories.DynamicWidgets)]
        [Author(FeatherTeams.SitefinityTeam6)]
        [Description("Verify when delete selected list template functionality.")]
        public void DynamicWidgetsDesignerListSettings_DeleteSelectedListTemplate()
        {
            string listTemplate = "PressArticleNew";
            string paragraphText = "List template";
            this.pageOperations = new PagesOperations();
            string testName = System.Reflection.MethodInfo.GetCurrentMethod().Name;
            string pageNamePrefix = testName + "DynamicPage";
            string pageTitlePrefix = testName + "Dynamic Page";
            string urlNamePrefix = testName + "dynamic-page";
            int index = 1;
            string url = UrlPath.ResolveAbsoluteUrl("~/" + urlNamePrefix + index);

            string file = null;

            var dynamicCollection = ServerOperationsFeather.DynamicModulePressArticle().RetrieveCollectionOfPressArticles();

            try
            {
                file = this.CopyFile();

                for (int i = 0; i < this.dynamicTitles.Length; i++)
                    ServerOperationsFeather.DynamicModulePressArticle().CreatePressArticleItem(this.dynamicTitles[i], this.dynamicUrls[i]);

                dynamicCollection = ServerOperationsFeather.DynamicModulePressArticle().RetrieveCollectionOfPressArticles();

                var mvcProxy = new MvcWidgetProxy();
                mvcProxy.ControllerName = typeof(DynamicContentController).FullName;
                var dynamicController = new DynamicContentController();
                dynamicController.Model.ContentType = TypeResolutionService.ResolveType(ResolveType);
                dynamicController.ListTemplateName = listTemplate;
                dynamicController.Model.ProviderName = FeatherWidgets.TestUtilities.CommonOperations.DynamicModulesOperations.ProviderName;
                mvcProxy.Settings = new ControllerSettings(dynamicController);
                mvcProxy.WidgetName = WidgetName;

                this.pageOperations.CreatePageWithControl(mvcProxy, pageNamePrefix, pageTitlePrefix, urlNamePrefix, index);

                File.Delete(file);

                string responseContent = PageInvoker.ExecuteWebRequest(url);

                Assert.IsFalse(responseContent.Contains(paragraphText), "The paragraph text was found!");

                for (int i = 0; i < this.dynamicTitles.Length; i++)
                    Assert.IsFalse(responseContent.Contains(this.dynamicTitles[i]), "The dynamic item with this title was found!");
            }
            finally
            {
                this.pageOperations.DeletePages();
                ServerOperationsFeather.DynamicModulePressArticle().DeleteDynamicItems(dynamicCollection);
                Directory.Delete(this.folderPath);
            }
        }

        [FixtureTearDown]
        public void Teardown()
        {
            Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.ModuleBuilder().DeleteAllModules(string.Empty, TransactionName);
        }

        #region Helper methods

        public string CopyFile()
        {
            if (!Directory.Exists(this.folderPath))
            {
                Directory.CreateDirectory(this.folderPath);
            }

            string filePath = Path.Combine(this.folderPath, DynamicFileName);
            ServerOperationsFeather.DynamicModules().AddNewResource(DynamicFileFileResource, filePath);
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
        private const string DynamicFileFileResource = "FeatherWidgets.TestUtilities.Data.DynamicModules.List.PressArticleNew.cshtml";
        private const string DynamicFileName = "List.PressArticleNew.cshtml";
        private string folderPath = Path.Combine(System.Web.Hosting.HostingEnvironment.MapPath("~/"), "MVC", "Views", "PressArticle");

        #endregion
    }
}
