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
            this.searchIndex1Id = SitefinityOperations.ServerOperations.Search().CreateSearchIndex(SearchResultsWidgetTests.SearchIndexName, new[] { SitefinityOperations.SearchContentType.Pages, SitefinityOperations.SearchContentType.News });
            this.pageOperations = new PagesOperations();

            for (int i = 1; i <= SearchResultsWidgetTests.NewsCount; i++)
                Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.News().CreateNewsItem(SearchResultsWidgetTests.NewsTitle + i);

            this.CreateNewsInSecondLanguage();
        }

        /// <summary>
        /// Tears down.
        /// </summary>
        [TearDown]
        public void TearDown()
        {
            this.DeleteSearchIndex(SearchIndexName, this.searchIndex1Id);
            this.pageOperations.DeletePages();
            Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.News().DeleteAllNews();
        }

        [Test]
        [Multilingual]
        [Category(TestCategories.SearchResults)]
        [Author("FeatherTeam")]
        [Description("Verifies that all search results are returned correctly for all languages.")]
        public void SearchResultsWidget_AllLanguages_ResultsFound()
        {
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
            newsController.Model.EnableSocialSharing = true;
            mvcProxy.Settings = new ControllerSettings(newsController);

            this.pageOperations.CreatePageWithControl(mvcProxy, pageNamePrefix, pageTitlePrefix, urlNamePrefix, index);

            searchResultsController.Index(null, SearchResultsWidgetTests.NewsTitle, SearchResultsWidgetTests.SearchIndexName, null, null, orderBy);

            Assert.AreEqual(SearchResultsWidgetTests.NewsCount, searchResultsController.Model.Results.TotalCount);
        }

        [Test]
        [Multilingual]
        [Category(TestCategories.SearchResults)]
        [Author("FeatherTeam")]
        [Description("Verifies that all search results are returned correctly for particular languages.")]
        public void SearchResultsWidget_SpecificLanguages_ResultsFound()
        {
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
            newsController.Model.EnableSocialSharing = true;
            mvcProxy.Settings = new ControllerSettings(newsController);

            this.pageOperations.CreatePageWithControl(mvcProxy, pageNamePrefix, pageTitlePrefix, urlNamePrefix, index);

            searchResultsController.Index(null, SearchResultsWidgetTests.NewsTitle, SearchResultsWidgetTests.SearchIndexName, null, language, orderBy);

            Assert.AreEqual(1, searchResultsController.Model.Results.TotalCount);
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

        /// <summary>
        /// Deletes the index of the search.
        /// </summary>
        /// <param name="searchIndexName">Name of the search index.</param>
        /// <param name="publishingPointId">The publishing point identifier.</param>
        private void DeleteSearchIndex(string searchIndexName, Guid publishingPointId)
        {
            // delete the publishing point
            var transaction = Guid.NewGuid().ToString();
            var manager = PublishingManager.GetManager(PublishingConfig.SearchProviderName, transaction);
            var pp = manager.GetPublishingPoint(publishingPointId);
            foreach (var settings in pp.PipeSettings)
                manager.DeletePipeSettings(settings);
            manager.DeletePublishingPoint(pp);
            TransactionManager.CommitTransaction(transaction);

            var service = ServiceBus.ResolveService<ISearchService>();
            service.DeleteIndex(searchIndexName);
        }

        #endregion

        #region Fields and constants

        private Guid searchIndex1Id;
        private PagesOperations pageOperations;
        private const string NewsTitle = "TestNews";
        private const string SearchIndexName = "catalogue1";
        private const int NewsCount = 5;

        #endregion
    }
}
