using System;
using System.Globalization;
using System.Linq;
using System.Threading;
using FeatherWidgets.TestUtilities.CommonOperations;
using MbUnit.Framework;
using Telerik.Sitefinity;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Frontend.News.Mvc.Controllers;
using Telerik.Sitefinity.Frontend.Search.Mvc.Controllers;
using Telerik.Sitefinity.Frontend.TestUtilities;
using Telerik.Sitefinity.Modules.News;
using Telerik.Sitefinity.Mvc.Proxy;
using Telerik.Sitefinity.News.Model;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Model;
using Telerik.WebTestRunner.Server.Attributes;
using SitefinityOperations = Telerik.Sitefinity.TestUtilities.CommonOperations;

namespace FeatherWidgets.TestIntegration.SearchResults
{
    /// <summary>
    /// This is a class with Search results tests
    /// </summary>
    [TestFixture]
    [Description("This is a class with Search results tests.")]
    public class SearchResultsWidgetTests
    {
        /// <summary>
        /// Set up method
        /// </summary>
        [SetUp]
        public void Setup()
        {
            this.pageOperations = new PagesOperations();
            this.frontEndLanguages = AppSettings.CurrentSettings.DefinedFrontendLanguages;

            var firstLanguage = this.frontEndLanguages[0].Name;
            var secondLanguage = this.frontEndLanguages[2].Name;

            this.CreateNewsInLanguage(firstLanguage, SearchResultsWidgetTests.NewsTitle + "1", "Content1");
            this.CreateNewsInLanguage(firstLanguage, SearchResultsWidgetTests.NewsTitle + "2", "Content2");
            this.CreateNewsInLanguage(firstLanguage, SearchResultsWidgetTests.NewsTitle + "3", "Content3");
            this.CreateNewsInLanguage(firstLanguage, SearchResultsWidgetTests.NewsTitle + "4", "Content4");
            this.CreateNewsInLanguage(firstLanguage, SearchResultsWidgetTests.NewsTitle + "5", "Content5");

            this.CreateNewsInLanguage(secondLanguage, SearchResultsWidgetTests.NewsTitle + "20", "Content20");

            this.searchIndexName = "Search" + Guid.NewGuid();
        }

        /// <summary>
        /// Tears down.
        /// </summary>
        [TearDown]
        public void TearDown()
        {
            Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.Pages().DeleteAllPages();
            Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.News().DeleteAllNews();
        }

        [Test]
        [Multilingual]
        [Category(TestCategories.SearchResults)]
        [Author(FeatherTeams.SitefinityTeam4)]
        [Description("Verifies that all search results are returned correctly for default language.")]
        public void SearchResultsWidget_DefaultLanguage_ResultsFound_OldestOrder()
        {
            Guid searchIndex1Id = Guid.Empty;
            try
            {
                searchIndex1Id = SitefinityOperations.ServerOperations.Search().CreateIndex(this.searchIndexName, new[] { SitefinityOperations.SearchContentType.News });
                SitefinityOperations.ServerOperations.Search().Reindex(searchIndex1Id);

                int index = 1;
                string testName = System.Reflection.MethodInfo.GetCurrentMethod().Name;
                string pageNamePrefix = testName + "NewsPage" + index;
                string pageTitlePrefix = testName + "NewsPage" + index;
                string urlNamePrefix = testName + "news-page" + index;

                string orderBy = "Oldest";
                var searchResultsController = new SearchResultsController();

                var mvcProxy = new MvcControllerProxy();
                mvcProxy.ControllerName = typeof(NewsController).FullName;
                var newsController = new NewsController();
                mvcProxy.Settings = new ControllerSettings(newsController);

                this.pageOperations.CreatePageWithControl(mvcProxy, pageNamePrefix, pageTitlePrefix, urlNamePrefix, index);

                searchResultsController.Index(null, SearchResultsWidgetTests.NewsTitle, this.searchIndexName, null, this.frontEndLanguages[0].Name, orderBy);

                Assert.AreEqual(SearchResultsWidgetTests.NewsCount, searchResultsController.Model.Results.TotalCount);
                for (int i = 1; i <= SearchResultsWidgetTests.NewsCount; i++)
                {
                    Assert.AreEqual(SearchResultsWidgetTests.NewsTitle + i, searchResultsController.Model.Results.Data[i - 1].GetValue("Title"));
                }
            }
            finally
            {
                if (searchIndex1Id != Guid.Empty)
                {
                    Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.Search().DeleteSearchIndex(this.searchIndexName, searchIndex1Id);
                }
            }
        }

        [Test]
        [Multilingual]
        [Category(TestCategories.SearchResults)]
        [Author(FeatherTeams.SitefinityTeam4)]
        [Description("Verifies that no search results are found for default language.")]
        public void SearchResultsWidget_DefaultLanguage_NoResultsFound()
        {
            Guid searchIndex1Id = Guid.Empty;
            try
            {
                searchIndex1Id = SitefinityOperations.ServerOperations.Search().CreateIndex(this.searchIndexName, new[] { SitefinityOperations.SearchContentType.News });
                SitefinityOperations.ServerOperations.Search().Reindex(searchIndex1Id);

                int index = 1;
                string testName = System.Reflection.MethodInfo.GetCurrentMethod().Name;
                string pageNamePrefix = testName + "NewsPage" + index;
                string pageTitlePrefix = testName + "NewsPage" + index;
                string urlNamePrefix = testName + "news-page" + index;

                string orderBy = "Oldest";
                string searchString = "TestEvents";
                int expectedCount = 0;
                var searchResultsController = new SearchResultsController();

                var mvcProxy = new MvcControllerProxy();
                mvcProxy.ControllerName = typeof(NewsController).FullName;
                var newsController = new NewsController();
                mvcProxy.Settings = new ControllerSettings(newsController);

                this.pageOperations.CreatePageWithControl(mvcProxy, pageNamePrefix, pageTitlePrefix, urlNamePrefix, index);

                searchResultsController.Index(null, searchString, this.searchIndexName, null, this.frontEndLanguages[0].Name, orderBy);

                Assert.AreEqual(expectedCount, searchResultsController.Model.Results.TotalCount);
            }
            finally
            {
                if (searchIndex1Id != Guid.Empty)
                {
                    Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.Search().DeleteSearchIndex(this.searchIndexName, searchIndex1Id);
                }
            }
        }

        [Test]
        [Multilingual]
        [Category(TestCategories.SearchResults)]
        [Author(FeatherTeams.SitefinityTeam4)]
        [Description("Verifies that all search results are returned correctly for particular languages.")]
        public void SearchResultsWidget_NonDefaultLanguage_ResultsFound_OldestOrder()
        {
            Guid searchIndex1Id = Guid.Empty;
            try
            {
                searchIndex1Id = SitefinityOperations.ServerOperations.Search().CreateIndex(this.searchIndexName, new[] { SitefinityOperations.SearchContentType.News });
                SitefinityOperations.ServerOperations.Search().Reindex(searchIndex1Id);

                int index = 1;
                string testName = System.Reflection.MethodInfo.GetCurrentMethod().Name;
                string pageNamePrefix = testName + "NewsPage" + index;
                string pageTitlePrefix = testName + "NewsPage" + index;
                string urlNamePrefix = testName + "news-page" + index;

                string orderBy = "Oldest";
                var searchResultsController = new SearchResultsController();

                var mvcProxy = new MvcControllerProxy();
                mvcProxy.ControllerName = typeof(NewsController).FullName;
                var newsController = new NewsController();
                mvcProxy.Settings = new ControllerSettings(newsController);

                this.pageOperations.CreatePageWithControl(mvcProxy, pageNamePrefix, pageTitlePrefix, urlNamePrefix, index);

                searchResultsController.Index(null, SearchResultsWidgetTests.NewsTitle, this.searchIndexName, null, this.frontEndLanguages[2].Name, orderBy);

                Assert.AreEqual(1, searchResultsController.Model.Results.TotalCount);
                Assert.AreEqual(SearchResultsWidgetTests.NewsTitle + "20", searchResultsController.Model.Results.Data[0].GetValue("Title"));
            }
            finally
            {
                if (searchIndex1Id != Guid.Empty)
                {
                    Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.Search().DeleteSearchIndex(this.searchIndexName, searchIndex1Id);
                }
            }
        }

        [Test]
        [Multilingual]
        [Category(TestCategories.SearchResults)]
        [Author(FeatherTeams.SitefinityTeam4)]
        [Description("Verifies paging of search results are returned correctly for default language.")]
        public void SearchResultsWidget_DefaultLanguage_Paging_OldestOrder()
        {
            Guid searchIndex1Id = Guid.Empty;

            try
            {
                searchIndex1Id = SitefinityOperations.ServerOperations.Search().CreateIndex(this.searchIndexName, new[] { SitefinityOperations.SearchContentType.News });
                SitefinityOperations.ServerOperations.Search().Reindex(searchIndex1Id);

                int index = 1;
                string testName = System.Reflection.MethodInfo.GetCurrentMethod().Name;
                string pageNamePrefix = testName + "NewsPage" + index;
                string pageTitlePrefix = testName + "NewsPage" + index;
                string urlNamePrefix = testName + "news-page" + index;

                string orderBy = "Oldest";
                var searchResultsController = new SearchResultsController();
                searchResultsController.Model.DisplayMode = Telerik.Sitefinity.Frontend.Search.Mvc.Models.ListDisplayMode.Paging;
                searchResultsController.Model.ItemsPerPage = 2;
                int expectedPagesCount = 3;

                var mvcProxy = new MvcControllerProxy();
                mvcProxy.ControllerName = typeof(NewsController).FullName;
                var newsController = new NewsController();
                mvcProxy.Settings = new ControllerSettings(newsController);

                this.pageOperations.CreatePageWithControl(mvcProxy, pageNamePrefix, pageTitlePrefix, urlNamePrefix, index);

                searchResultsController.Index(null, SearchResultsWidgetTests.NewsTitle, this.searchIndexName, null, this.frontEndLanguages[0].Name, orderBy);

                Assert.AreEqual(expectedPagesCount, searchResultsController.Model.TotalPagesCount);
                Assert.AreEqual(SearchResultsWidgetTests.NewsCount, searchResultsController.Model.Results.TotalCount);

                searchResultsController.Index(1, SearchResultsWidgetTests.NewsTitle, this.searchIndexName, null, this.frontEndLanguages[0].Name, orderBy);
                Assert.AreEqual(SearchResultsWidgetTests.NewsTitle + "1", searchResultsController.Model.Results.Data[0].GetValue("Title"));
                Assert.AreEqual(SearchResultsWidgetTests.NewsTitle + "2", searchResultsController.Model.Results.Data[1].GetValue("Title"));

                searchResultsController.Index(2, SearchResultsWidgetTests.NewsTitle, this.searchIndexName, null, this.frontEndLanguages[0].Name, orderBy);
                Assert.AreEqual(SearchResultsWidgetTests.NewsTitle + "3", searchResultsController.Model.Results.Data[0].GetValue("Title"));
                Assert.AreEqual(SearchResultsWidgetTests.NewsTitle + "4", searchResultsController.Model.Results.Data[1].GetValue("Title"));

                searchResultsController.Index(3, SearchResultsWidgetTests.NewsTitle, this.searchIndexName, null, this.frontEndLanguages[0].Name, orderBy);
                Assert.AreEqual(SearchResultsWidgetTests.NewsTitle + "5", searchResultsController.Model.Results.Data[0].GetValue("Title"));
            }
            finally
            {
                if (searchIndex1Id != Guid.Empty)
                {
                    Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.Search().DeleteSearchIndex(this.searchIndexName, searchIndex1Id);
                }
            }
        }

        [Test]
        [Multilingual]
        [Category(TestCategories.SearchResults)]
        [Author(FeatherTeams.SitefinityTeam4)]
        [Description("Verifies limit of all search results are returned correctly for all languages.")]
        public void SearchResultsWidget_DefaultLanguage_Limit_OldestOrder()
        {
            Guid searchIndex1Id = Guid.Empty;

            try
            {
                searchIndex1Id = SitefinityOperations.ServerOperations.Search().CreateIndex(this.searchIndexName, new[] { SitefinityOperations.SearchContentType.News });
                SitefinityOperations.ServerOperations.Search().Reindex(searchIndex1Id);

                int index = 1;
                string testName = System.Reflection.MethodInfo.GetCurrentMethod().Name;
                string pageNamePrefix = testName + "NewsPage" + index;
                string pageTitlePrefix = testName + "NewsPage" + index;
                string urlNamePrefix = testName + "news-page" + index;

                string orderBy = "Oldest";
                var searchResultsController = new SearchResultsController();
                searchResultsController.Model.DisplayMode = Telerik.Sitefinity.Frontend.Search.Mvc.Models.ListDisplayMode.Limit;
                searchResultsController.Model.LimitCount = 2;
                int expectedResultsCount = 2;

                var mvcProxy = new MvcControllerProxy();
                mvcProxy.ControllerName = typeof(NewsController).FullName;
                var newsController = new NewsController();
                mvcProxy.Settings = new ControllerSettings(newsController);

                this.pageOperations.CreatePageWithControl(mvcProxy, pageNamePrefix, pageTitlePrefix, urlNamePrefix, index);

                searchResultsController.Index(null, SearchResultsWidgetTests.NewsTitle, this.searchIndexName, null, this.frontEndLanguages[0].Name, orderBy);

                Assert.AreEqual(expectedResultsCount, searchResultsController.Model.Results.Data.Count);
                Assert.AreEqual(SearchResultsWidgetTests.NewsTitle + "1", searchResultsController.Model.Results.Data[0].GetValue("Title"));
                Assert.AreEqual(SearchResultsWidgetTests.NewsTitle + "2", searchResultsController.Model.Results.Data[1].GetValue("Title"));
            }
            finally
            {
                if (searchIndex1Id != Guid.Empty)
                {
                    Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.Search().DeleteSearchIndex(this.searchIndexName, searchIndex1Id);
                }
            }
        }

        [Test]
        [Multilingual]
        [Category(TestCategories.SearchResults)]
        [Author(FeatherTeams.SitefinityTeam4)]
        [Description("Verifies no limit of all search results are returned correctly for all languages.")]
        public void SearchResultsWidget_DefaultLanguage_NoLimit_NewestOrder()
        {
            Guid searchIndex1Id = Guid.Empty;
            try
            {
                searchIndex1Id = SitefinityOperations.ServerOperations.Search().CreateIndex(this.searchIndexName, new[] { SitefinityOperations.SearchContentType.News });
                SitefinityOperations.ServerOperations.Search().Reindex(searchIndex1Id);

                int index = 1;
                string testName = System.Reflection.MethodInfo.GetCurrentMethod().Name;
                string pageNamePrefix = testName + "NewsPage" + index;
                string pageTitlePrefix = testName + "NewsPage" + index;
                string urlNamePrefix = testName + "news-page" + index;

                string orderBy = "Newest";
                var searchResultsController = new SearchResultsController();
                searchResultsController.Model.DisplayMode = Telerik.Sitefinity.Frontend.Search.Mvc.Models.ListDisplayMode.All;
                searchResultsController.Model.ItemsPerPage = 2;
                searchResultsController.Model.LimitCount = 2;

                var mvcProxy = new MvcControllerProxy();
                mvcProxy.ControllerName = typeof(NewsController).FullName;
                var newsController = new NewsController();
                mvcProxy.Settings = new ControllerSettings(newsController);

                this.pageOperations.CreatePageWithControl(mvcProxy, pageNamePrefix, pageTitlePrefix, urlNamePrefix, index);

                searchResultsController.Index(null, SearchResultsWidgetTests.NewsTitle, this.searchIndexName, null, this.frontEndLanguages[0].Name, orderBy);

                Assert.AreEqual(SearchResultsWidgetTests.NewsCount, searchResultsController.Model.Results.TotalCount);
                for (int i = 0; i < SearchResultsWidgetTests.NewsCount; i++)
                {
                    Assert.AreEqual(SearchResultsWidgetTests.NewsTitle + (SearchResultsWidgetTests.NewsCount - i), searchResultsController.Model.Results.Data[i].GetValue("Title"));
                }
            }
            finally
            {
                if (searchIndex1Id != Guid.Empty)
                {
                    Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.Search().DeleteSearchIndex(this.searchIndexName, searchIndex1Id);
                }
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling"), Test]
        [Multilingual]
        [Category(TestCategories.SearchResults)]
        [Author(FeatherTeams.SitefinityTeam8)]
        [Description("Verifies paging of search results are returned correctly for default language when permission filtering is enabled.")]
        public void SearchResultsWidget_DefaultLanguage_FilteringByPermissions_Paging_OldestOrder()
        {
            Guid searchIndex1Id = Guid.Empty;

            try
            {
                var newsManager = NewsManager.GetManager();
                searchIndex1Id = SitefinityOperations.ServerOperations.Search().CreateIndex(this.searchIndexName, new[] { SitefinityOperations.SearchContentType.News });
                SitefinityOperations.ServerOperations.Search().SetFilterByViewPermissions(true);

                int index = 1;
                string testName = System.Reflection.MethodInfo.GetCurrentMethod().Name;
                string pageNamePrefix = testName + "NewsPage" + index;
                string pageTitlePrefix = testName + "NewsPage" + index;
                string urlNamePrefix = testName + "news-page" + index;

                var newsItem = newsManager.GetNewsItems().Where(p => p.Title == SearchResultsWidgetTests.NewsTitle + "1").FirstOrDefault();
                this.BreakPermissions<NewsItem>(newsItem.Id);
                Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.ContentItems().PublishNewsItem(newsItem.Id);

                string orderBy = "Oldest";
                var searchResultsController = new SearchResultsController();
                searchResultsController.Model.DisplayMode = Telerik.Sitefinity.Frontend.Search.Mvc.Models.ListDisplayMode.Paging;
                searchResultsController.Model.ItemsPerPage = 2;
                int expectedPagesCount = 2;

                var mvcProxy = new MvcControllerProxy();
                mvcProxy.ControllerName = typeof(NewsController).FullName;
                var newsController = new NewsController();
                mvcProxy.Settings = new ControllerSettings(newsController);

                this.pageOperations.CreatePageWithControl(mvcProxy, pageNamePrefix, pageTitlePrefix, urlNamePrefix, index);

                SitefinityOperations.ServerOperations.Search().Reindex(searchIndex1Id);

                SecurityManager.Logout();

                searchResultsController.Index(null, SearchResultsWidgetTests.NewsTitle, this.searchIndexName, null, this.frontEndLanguages[0].Name, orderBy);

                Assert.AreEqual(expectedPagesCount, searchResultsController.Model.TotalPagesCount);

                searchResultsController.Index(1, SearchResultsWidgetTests.NewsTitle, this.searchIndexName, null, this.frontEndLanguages[0].Name, orderBy);
                Assert.AreEqual(SearchResultsWidgetTests.NewsTitle + "2", searchResultsController.Model.Results.Data[0].GetValue("Title"));
                Assert.AreEqual(SearchResultsWidgetTests.NewsTitle + "3", searchResultsController.Model.Results.Data[1].GetValue("Title"));

                searchResultsController.Index(2, SearchResultsWidgetTests.NewsTitle, this.searchIndexName, null, this.frontEndLanguages[0].Name, orderBy);
                Assert.AreEqual(SearchResultsWidgetTests.NewsTitle + "4", searchResultsController.Model.Results.Data[0].GetValue("Title"));
                Assert.AreEqual(SearchResultsWidgetTests.NewsTitle + "5", searchResultsController.Model.Results.Data[1].GetValue("Title"));
            }
            finally
            {
                SitefinityOperations.AuthenticationHelper.AuthenticateUser(AdminUserName, AdminPass, true);

                if (searchIndex1Id != Guid.Empty)
                {
                    Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.Search().DeleteSearchIndex(this.searchIndexName, searchIndex1Id);
                }

                SitefinityOperations.ServerOperations.Search().SetFilterByViewPermissions(false);
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling"), Test]
        [Multilingual]
        [Category(TestCategories.SearchResults)]
        [Author(FeatherTeams.SitefinityTeam8)]
        [Description("Verifies limit of all search results are returned correctly for default language when permission filtering is enabled.")]
        public void SearchResultsWidget_DefaultLanguage_FilteringByPermissions_Limit_OldestOrder()
        {
            Guid searchIndex1Id = Guid.Empty;

            try
            {
                var newsManager = NewsManager.GetManager();
                searchIndex1Id = SitefinityOperations.ServerOperations.Search().CreateIndex(this.searchIndexName, new[] { SitefinityOperations.SearchContentType.News });
                SitefinityOperations.ServerOperations.Search().SetFilterByViewPermissions(true);

                int index = 1;
                string testName = System.Reflection.MethodInfo.GetCurrentMethod().Name;
                string pageNamePrefix = testName + "NewsPage" + index;
                string pageTitlePrefix = testName + "NewsPage" + index;
                string urlNamePrefix = testName + "news-page" + index;

                var newsItem = newsManager.GetNewsItems().Where(p => p.Title == SearchResultsWidgetTests.NewsTitle + "1").FirstOrDefault();
                this.BreakPermissions<NewsItem>(newsItem.Id);
                Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.ContentItems().PublishNewsItem(newsItem.Id);

                string orderBy = "Oldest";
                var searchResultsController = new SearchResultsController();
                searchResultsController.Model.DisplayMode = Telerik.Sitefinity.Frontend.Search.Mvc.Models.ListDisplayMode.Limit;
                searchResultsController.Model.LimitCount = 2;
                int expectedResultsCount = 2;

                var mvcProxy = new MvcControllerProxy();
                mvcProxy.ControllerName = typeof(NewsController).FullName;
                var newsController = new NewsController();
                mvcProxy.Settings = new ControllerSettings(newsController);

                this.pageOperations.CreatePageWithControl(mvcProxy, pageNamePrefix, pageTitlePrefix, urlNamePrefix, index);

                SitefinityOperations.ServerOperations.Search().Reindex(searchIndex1Id);

                SecurityManager.Logout();

                searchResultsController.Index(null, SearchResultsWidgetTests.NewsTitle, this.searchIndexName, null, this.frontEndLanguages[0].Name, orderBy);

                Assert.AreEqual(expectedResultsCount, searchResultsController.Model.Results.Data.Count);
                Assert.AreEqual(SearchResultsWidgetTests.NewsTitle + "2", searchResultsController.Model.Results.Data[0].GetValue("Title"));
                Assert.AreEqual(SearchResultsWidgetTests.NewsTitle + "3", searchResultsController.Model.Results.Data[1].GetValue("Title"));
                Assert.IsTrue(searchResultsController.Model.Results.Data.Where(p => p.GetValue("Title") == (SearchResultsWidgetTests.NewsTitle + "1")).FirstOrDefault() == null && SearchResultsWidgetTests.NewsCount > 0);
            }
            finally
            {
                SitefinityOperations.AuthenticationHelper.AuthenticateUser(AdminUserName, AdminPass, true);

                if (searchIndex1Id != Guid.Empty)
                {
                    Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.Search().DeleteSearchIndex(this.searchIndexName, searchIndex1Id);
                }

                SitefinityOperations.ServerOperations.Search().SetFilterByViewPermissions(false);
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling"), Test]
        [Multilingual]
        [Category(TestCategories.SearchResults)]
        [Author(FeatherTeams.SitefinityTeam8)]
        [Description("Verifies no limit of all search results are returned correctly for default language when permission filtering is enabled.")]
        public void SearchResultsWidget_DefaultLanguage_FilteringByPermissions_NoLimit_NewestOrder()
        {
            Guid searchIndex1Id = Guid.Empty;

            try
            {
                var newsManager = NewsManager.GetManager();
                searchIndex1Id = SitefinityOperations.ServerOperations.Search().CreateIndex(this.searchIndexName, new[] { SitefinityOperations.SearchContentType.News });
                SitefinityOperations.ServerOperations.Search().SetFilterByViewPermissions(true);

                int index = 1;
                string testName = System.Reflection.MethodInfo.GetCurrentMethod().Name;
                string pageNamePrefix = testName + "NewsPage" + index;
                string pageTitlePrefix = testName + "NewsPage" + index;
                string urlNamePrefix = testName + "news-page" + index;

                var newsItem = newsManager.GetNewsItems().Where(p => p.Title == SearchResultsWidgetTests.NewsTitle + "1").FirstOrDefault();
                this.BreakPermissions<NewsItem>(newsItem.Id);
                Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.ContentItems().PublishNewsItem(newsItem.Id);

                string orderBy = "Newest";
                var searchResultsController = new SearchResultsController();
                searchResultsController.Model.DisplayMode = Telerik.Sitefinity.Frontend.Search.Mvc.Models.ListDisplayMode.All;

                var mvcProxy = new MvcControllerProxy();
                mvcProxy.ControllerName = typeof(NewsController).FullName;
                var newsController = new NewsController();
                mvcProxy.Settings = new ControllerSettings(newsController);

                this.pageOperations.CreatePageWithControl(mvcProxy, pageNamePrefix, pageTitlePrefix, urlNamePrefix, index);

                SitefinityOperations.ServerOperations.Search().Reindex(searchIndex1Id);

                SecurityManager.Logout();

                searchResultsController.Index(null, SearchResultsWidgetTests.NewsTitle, this.searchIndexName, null, this.frontEndLanguages[0].Name, orderBy);

                Assert.AreEqual(4, searchResultsController.Model.Results.TotalCount);
                Assert.IsTrue(searchResultsController.Model.Results.Data.Where(p => p.GetValue("Title") == (SearchResultsWidgetTests.NewsTitle + "1")).FirstOrDefault() == null && SearchResultsWidgetTests.NewsCount > 0);
            }
            finally
            {
                SitefinityOperations.AuthenticationHelper.AuthenticateUser(AdminUserName, AdminPass, true);

                if (searchIndex1Id != Guid.Empty)
                {
                    Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.Search().DeleteSearchIndex(this.searchIndexName, searchIndex1Id);
                }

                SitefinityOperations.ServerOperations.Search().SetFilterByViewPermissions(false);
            }
        }

        #region Helper methods

        /// <summary>
        /// Break permissions for item. This means that all permissions available for the item will be 'Administrators only'.
        /// </summary>
        /// <typeparam name="T">Defines the type of the content item. Should inherit ISecuredObject.</typeparam>
        /// <param name="itemId">The id of the item.</param>
        private void BreakPermissions<T>(Guid itemId) where T : ISecuredObject
        {
            var itemType = typeof(T);
            var transaction = "permissions_" + Guid.NewGuid();
            var manager = ManagerBase.GetMappedManagerInTransaction(itemType, transaction);
            var item = manager.GetItem(itemType, itemId) as ISecuredObject;

            manager.BreakPermiossionsInheritance(item);
            for (int i = item.Permissions.Count - 1; i >= 0; i--)
            {
                item.Permissions.RemoveAt(i);
            }

            TransactionManager.CommitTransaction(transaction);
        }

        private void CreateNewsInLanguage(string language, string title, string content)
        {
            this.AssertIsMultilingual();

            var currentCulture = Thread.CurrentThread.CurrentUICulture;

            try
            {
                System.Threading.Thread.CurrentThread.CurrentUICulture = new CultureInfo(language);

                App.Prepare()
                   .SetRunPublishingSystemAsynchronous(false)
                   .WorkWith()
                   .AnyContentItem<NewsItem>()
                   .CreateNew()
                   .Do(item =>
                   {
                       item.Title = title;
                       ((NewsItem)item).Content = content;
                   })
                   .Publish()
                   .Done()
                   .SaveChanges();
            }
            finally
            {
                Thread.CurrentThread.CurrentUICulture = currentCulture;
            }
        }

        private void AssertIsMultilingual()
        {
            Assert.IsTrue(AppSettings.CurrentSettings.Multilingual);
            Assert.IsTrue(AppSettings.CurrentSettings.DefinedFrontendLanguages.Count() >= 2, "At least 2 frontend languages should be configured.");
        }
        #endregion

        #region Fields and constants

        private PagesOperations pageOperations;
        private CultureInfo[] frontEndLanguages;
        private const string NewsTitle = "TestNews";
        private string searchIndexName;
        private const int NewsCount = 5;
        private const string AdminUserName = "admin";
        private const string AdminPass = "admin@2";

        #endregion
    }
}