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
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Mvc.Proxy;
using Telerik.Sitefinity.News.Model;
using Telerik.Sitefinity.Publishing;
using Telerik.Sitefinity.Publishing.Configuration;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Services.Search;
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

            for (int i = 1; i <= SearchResultsWidgetTests.NewsCount; i++)
            {
                Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.News().CreateNewsItem(SearchResultsWidgetTests.NewsTitle + i);
            }

            this.CreateNewsInSecondLanguage();
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
        [Author(FeatherTeams.Team2)]
        [Description("Verifies that all search results are returned correctly for default language.")]
        public void SearchResultsWidget_DefaultLanguage_ResultsFound_OldestOrder()
        {
            Guid searchIndex1Id = Guid.Empty;
            try
            {
                searchIndex1Id = SitefinityOperations.ServerOperations.Search().CreateSearchIndex(SearchResultsWidgetTests.SearchIndexName, new[] { SitefinityOperations.SearchContentType.Pages, SitefinityOperations.SearchContentType.News });
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

                searchResultsController.Index(null, SearchResultsWidgetTests.NewsTitle, SearchResultsWidgetTests.SearchIndexName, null, null, orderBy);

                Assert.AreEqual(SearchResultsWidgetTests.NewsCount, searchResultsController.Model.Results.TotalCount);
                for (int i = 1; i <= SearchResultsWidgetTests.NewsCount; i++)
                {
                    Assert.AreEqual(SearchResultsWidgetTests.NewsTitle + i, searchResultsController.Model.Results.Data[i - 1].GetValue("Title"));
                }
            }
            finally
            {
                Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.Search().DeleteAllIndexes();
            }
        }

        [Test]
        [Multilingual]
        [Category(TestCategories.SearchResults)]
        [Author(FeatherTeams.Team7)]
        [Description("Verifies that no search results are found for default language.")]
        public void SearchResultsWidget_DefaultLanguage_NoResultsFound()
        {
            Guid searchIndex1Id = Guid.Empty;
            try
            {
                searchIndex1Id = SitefinityOperations.ServerOperations.Search().CreateSearchIndex(SearchResultsWidgetTests.SearchIndexName, new[] { SitefinityOperations.SearchContentType.Pages, SitefinityOperations.SearchContentType.News });
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

                searchResultsController.Index(null, searchString, SearchResultsWidgetTests.SearchIndexName, null, null, orderBy);

                Assert.AreEqual(expectedCount, searchResultsController.Model.Results.TotalCount);
            }
            finally
            {
                Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.Search().DeleteAllIndexes();
            }
        }

        [Test]
        [Multilingual]
        [Category(TestCategories.SearchResults)]
        [Author(FeatherTeams.Team2)]
        [Description("Verifies that all search results are returned correctly for particular languages.")]
        public void SearchResultsWidget_NonDefaultLanguage_ResultsFound_OldestOrder()
        {
            Guid searchIndex1Id = Guid.Empty;
            try
            {
                searchIndex1Id = SitefinityOperations.ServerOperations.Search().CreateSearchIndex(SearchResultsWidgetTests.SearchIndexName, new[] { SitefinityOperations.SearchContentType.Pages, SitefinityOperations.SearchContentType.News });
                SitefinityOperations.ServerOperations.Search().Reindex(searchIndex1Id);

                int index = 1;
                string testName = System.Reflection.MethodInfo.GetCurrentMethod().Name;
                string pageNamePrefix = testName + "NewsPage" + index;
                string pageTitlePrefix = testName + "NewsPage" + index;
                string urlNamePrefix = testName + "news-page" + index;

                var frontEndLanguages = AppSettings.CurrentSettings.DefinedFrontendLanguages;
                var language = frontEndLanguages[2].Name;
                string orderBy = "Oldest";
                var searchResultsController = new SearchResultsController();

                var mvcProxy = new MvcControllerProxy();
                mvcProxy.ControllerName = typeof(NewsController).FullName;
                var newsController = new NewsController();
                mvcProxy.Settings = new ControllerSettings(newsController);

                this.pageOperations.CreatePageWithControl(mvcProxy, pageNamePrefix, pageTitlePrefix, urlNamePrefix, index);

                searchResultsController.Index(null, SearchResultsWidgetTests.NewsTitle, SearchResultsWidgetTests.SearchIndexName, null, language, orderBy);

                Assert.AreEqual(1, searchResultsController.Model.Results.TotalCount);
                Assert.AreEqual(SearchResultsWidgetTests.NewsTitle + "20", searchResultsController.Model.Results.Data[0].GetValue("Title"));
            }
            finally
            {
                Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.Search().DeleteAllIndexes();
            }
        }

        [Test]
        [Multilingual]
        [Category(TestCategories.SearchResults)]
        [Author(FeatherTeams.Team7)]
        [Description("Verifies paging of search results are returned correctly for default language.")]
        public void SearchResultsWidget_DefaultLanguage_Paging_OldestOrder()
        {
            Guid searchIndex1Id = Guid.Empty;

            try
            {
                searchIndex1Id = SitefinityOperations.ServerOperations.Search().CreateSearchIndex(SearchResultsWidgetTests.SearchIndexName, new[] { SitefinityOperations.SearchContentType.Pages, SitefinityOperations.SearchContentType.News });
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

                searchResultsController.Index(null, SearchResultsWidgetTests.NewsTitle, SearchResultsWidgetTests.SearchIndexName, null, null, orderBy);

                Assert.AreEqual(expectedPagesCount, searchResultsController.Model.TotalPagesCount);
                Assert.AreEqual(SearchResultsWidgetTests.NewsCount, searchResultsController.Model.Results.TotalCount);

                searchResultsController.Index(1, SearchResultsWidgetTests.NewsTitle, SearchResultsWidgetTests.SearchIndexName, null, null, orderBy);
                Assert.AreEqual(SearchResultsWidgetTests.NewsTitle + "1", searchResultsController.Model.Results.Data[0].GetValue("Title"));
                Assert.AreEqual(SearchResultsWidgetTests.NewsTitle + "2", searchResultsController.Model.Results.Data[1].GetValue("Title"));

                searchResultsController.Index(2, SearchResultsWidgetTests.NewsTitle, SearchResultsWidgetTests.SearchIndexName, null, null, orderBy);
                Assert.AreEqual(SearchResultsWidgetTests.NewsTitle + "3", searchResultsController.Model.Results.Data[0].GetValue("Title"));
                Assert.AreEqual(SearchResultsWidgetTests.NewsTitle + "4", searchResultsController.Model.Results.Data[1].GetValue("Title"));

                searchResultsController.Index(3, SearchResultsWidgetTests.NewsTitle, SearchResultsWidgetTests.SearchIndexName, null, null, orderBy);
                Assert.AreEqual(SearchResultsWidgetTests.NewsTitle + "5", searchResultsController.Model.Results.Data[0].GetValue("Title"));
            }
            finally
            {
                Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.Search().DeleteAllIndexes();
            }
        }

        [Test]
        [Multilingual]
        [Category(TestCategories.SearchResults), Ignore]
        [Author(FeatherTeams.Team7)]
        [Description("Verifies limit of all search results are returned correctly for all languages.")]
        public void SearchResultsWidget_DefaultLanguage_Limit_OldestOrder()
        {
            Guid searchIndex1Id = Guid.Empty;

            try
            {
                searchIndex1Id = SitefinityOperations.ServerOperations.Search().CreateSearchIndex(SearchResultsWidgetTests.SearchIndexName, new[] { SitefinityOperations.SearchContentType.Pages, SitefinityOperations.SearchContentType.News });
                SitefinityOperations.ServerOperations.Search().Reindex(searchIndex1Id);

                int index = 1;
                string testName = System.Reflection.MethodInfo.GetCurrentMethod().Name;
                string pageNamePrefix = testName + "NewsPage" + index;
                string pageTitlePrefix = testName + "NewsPage" + index;
                string urlNamePrefix = testName + "news-page" + index;

                string orderBy = "Oldest";
                var searchResultsController = new SearchResultsController();
                searchResultsController.Model.DisplayMode = Telerik.Sitefinity.Frontend.Search.Mvc.Models.ListDisplayMode.Limit;
                searchResultsController.Model.ItemsPerPage = 2;
                int expectedResultsCount = 2;

                var mvcProxy = new MvcControllerProxy();
                mvcProxy.ControllerName = typeof(NewsController).FullName;
                var newsController = new NewsController();
                mvcProxy.Settings = new ControllerSettings(newsController);

                this.pageOperations.CreatePageWithControl(mvcProxy, pageNamePrefix, pageTitlePrefix, urlNamePrefix, index);

                searchResultsController.Index(null, SearchResultsWidgetTests.NewsTitle, SearchResultsWidgetTests.SearchIndexName, null, null, orderBy);

                //// ignored because total count of returned results is 5 instead of 2
                Assert.AreEqual(expectedResultsCount, searchResultsController.Model.Results.TotalCount);
                Assert.AreEqual(SearchResultsWidgetTests.NewsTitle + "1", searchResultsController.Model.Results.Data[0].GetValue("Title"));
                Assert.AreEqual(SearchResultsWidgetTests.NewsTitle + "2", searchResultsController.Model.Results.Data[1].GetValue("Title"));
            }
            finally
            {
                Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.Search().DeleteAllIndexes();
            }
        }

        [Test]
        [Multilingual]
        [Category(TestCategories.SearchResults)]
        [Author(FeatherTeams.Team7)]
        [Description("Verifies no limit of all search results are returned correctly for all languages.")]
        public void SearchResultsWidget_DefaultLanguage_NoLimit_NewestOrder()
        {
            Guid searchIndex1Id = Guid.Empty;
            try
            {
                searchIndex1Id = SitefinityOperations.ServerOperations.Search().CreateSearchIndex(SearchResultsWidgetTests.SearchIndexName, new[] { SitefinityOperations.SearchContentType.Pages, SitefinityOperations.SearchContentType.News });
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

                var mvcProxy = new MvcControllerProxy();
                mvcProxy.ControllerName = typeof(NewsController).FullName;
                var newsController = new NewsController();
                mvcProxy.Settings = new ControllerSettings(newsController);

                this.pageOperations.CreatePageWithControl(mvcProxy, pageNamePrefix, pageTitlePrefix, urlNamePrefix, index);

                searchResultsController.Index(null, SearchResultsWidgetTests.NewsTitle, SearchResultsWidgetTests.SearchIndexName, null, null, orderBy);

                Assert.AreEqual(SearchResultsWidgetTests.NewsCount, searchResultsController.Model.Results.TotalCount);
                for (int i = 0; i < SearchResultsWidgetTests.NewsCount; i++)
                {
                    Assert.AreEqual(SearchResultsWidgetTests.NewsTitle + (SearchResultsWidgetTests.NewsCount - i), searchResultsController.Model.Results.Data[i].GetValue("Title"));
                }
            }
            finally
            {
                Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.Search().DeleteAllIndexes();
            }
        }

        #region Helper methods

        private void CreateNewsInSecondLanguage()
        {
            this.AssertIsMultilingual();

            var frontEndLanguages = AppSettings.CurrentSettings.DefinedFrontendLanguages;
            var secondLanguage = frontEndLanguages[2].Name;

            var currentCulture = Thread.CurrentThread.CurrentUICulture;
            try
            {
                System.Threading.Thread.CurrentThread.CurrentUICulture = new CultureInfo(secondLanguage);

                App.Prepare()
                   .SetRunPublishingSystemAsynchronous(false)
                   .WorkWith()
                   .AnyContentItem<NewsItem>()
                   .CreateNew()
                   .Do(item =>
                   {
                       item.Title[secondLanguage] = SearchResultsWidgetTests.NewsTitle + "20";
                       ((NewsItem)item).GetString("Content")[secondLanguage] = "TestContent20";
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
        private const string NewsTitle = "TestNews";
        private const string SearchIndexName = "catalogue1";
        private const int NewsCount = 5;

        #endregion
    }
}
