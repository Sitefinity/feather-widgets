using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FeatherWidgets.TestUtilities.CommonOperations;
using Telerik.Sitefinity.TestArrangementService.Attributes;
using Telerik.Sitefinity.TestUI.Arrangements.Framework;
using Telerik.Sitefinity.TestUtilities.CommonOperations;

namespace FeatherWidgets.TestUI.Arrangements
{
    /// <summary>
    /// VerifySearchResults_SortingOnFrontend arragement.
    /// </summary>
    public class VerifySearchResults_SortingOnFrontend : TestArrangementBase
    {
        /// <summary>
        /// Server side set up.
        /// </summary>
        [ServerSetUp]
        public void SetUp()
        {
            ServerOperations.News().CreatePublishedNewsItemLiveId(NewsTitle1, NewsContent, NewsAuthor, NewsSource);
            ServerOperations.News().CreatePublishedNewsItemLiveId(NewsTitle2, NewsContent, NewsAuthor, NewsSource);

            ServerOperations.Pages().CreatePage(SearchPageTitle);
            ServerOperations.Pages().CreatePage(ResultsPageTitle);
            Guid newsPageId = ServerOperations.Pages().CreatePage(NewsPageTitle);
            ServerOperationsFeather.Pages().AddNewsWidgetToPage(newsPageId);

            Guid searchIndexId = ServerOperations.Search().CreateSearchIndex(SearchIndexName, new[] { SearchContentType.News });
            ServerOperations.Search().Reindex(searchIndexId);
        }

        /// <summary>
        /// Tears down.
        /// </summary>
        [ServerTearDown]
        public void TearDown()
        {
            ServerOperations.Pages().DeleteAllPages();
            ServerOperations.Search().DeleteAllIndexes();
            ServerOperations.News().DeleteAllNews();
        }

        private const string SearchIndexName = "news index";
        private const string NewsTitle1 = "test news";
        private const string NewsTitle2 = "another news";
        private const string SearchPageTitle = "SearchPage";
        private const string ResultsPageTitle = "ResultsPage";
        private const string NewsPageTitle = "NewsPage";
        private const string NewsAuthor = "TestNewsAuthor";
        private const string NewsSource = "TestNewsSource";
        private const string NewsContent = "News content";
    }
}
