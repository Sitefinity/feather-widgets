using System;
using FeatherWidgets.TestUtilities.CommonOperations;
using Telerik.Sitefinity.TestArrangementService.Attributes;
using Telerik.Sitefinity.TestUtilities.CommonOperations;

namespace FeatherWidgets.TestUI.Arrangements
{
    /// <summary>
    /// VerifyUseLimitListSettingsOnFrontendPageForSearchResultsWidget arrangement class.
    /// </summary>
    public class VerifyUseLimitListSettingsOnFrontendPageForSearchResultsWidget : TestArrangementBase
    {
        /// <summary>
        /// Server side set up.
        /// </summary>
        [ServerSetUp]
        public void SetUp()
        {
            Guid pageId = ServerOperations.Pages().CreatePage(PageName);

            for (int i = 1; i < 6; i++)
            {
                ServerOperationsFeather.NewsOperations().CreatePublishedNewsItem(NewsTitle + i, NewsContent, "AuthorName", "SourceName", null, null, null);
            }

            ServerOperationsFeather.Pages().AddNewsWidgetToPage(pageId);
            ServerOperations.Pages().CreatePage(SearchPageTitle);
            ServerOperations.Pages().CreatePage(ResultsPageTitle);

            Guid searchIndexId = ServerOperations.Search().CreateIndex(SearchIndexName, new[] { SearchContentType.News });
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

        private const string SearchIndexName = "VerifySearchResults";
        private const string PageName = "News";
        private const string NewsContent = "News content";
        private const string NewsTitle = "NewsTitle ";
        private const string SearchPageTitle = "SearchPage";
        private const string ResultsPageTitle = "ResultsPage";
    }
}
