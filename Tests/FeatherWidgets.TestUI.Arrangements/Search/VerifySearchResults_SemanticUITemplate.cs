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
    /// Arrangement methods for VerifySearchResults_SemanticUITemplate
    /// </summary>
    public class VerifySearchResults_SemanticUITemplate : TestArrangementBase
    {
        /// <summary>
        /// Server side set up.
        /// </summary>
        [ServerSetUp]
        public void SetUp()
        {
            ServerOperations.News().CreatePublishedNewsItemLiveId(NewsTitle1, NewsContent, NewsAuthor, NewsSource);
            ServerOperations.News().CreatePublishedNewsItemLiveId(NewsTitle2, NewsContent, NewsAuthor, NewsSource);

            Guid searchIndexId = ServerOperations.Search().CreateIndex(SearchIndexName, new[] { SearchContentType.News });
            ServerOperations.Search().Reindex(searchIndexId);

            Guid templateId = ServerOperations.Templates().GetTemplateIdByTitle(PageTemplateName);
            ServerOperations.Pages().CreatePage(SearchPageTitle, templateId);
            Guid newsPageId = ServerOperations.Pages().CreatePage(NewsPageTitle);
            ServerOperationsFeather.Pages().AddNewsWidgetToPage(newsPageId);
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

        private const string SearchIndexName = "VerifySearchResults_SemanticUITemplate";
        private const string NewsTitle1 = "test news";
        private const string NewsTitle2 = "another news";
        private const string SearchPageTitle = "SemanticUIPage";
        private const string PageTemplateName = "SemanticUI.default";
        private const string NewsPageTitle = "NewsPage";
        private const string NewsAuthor = "TestNewsAuthor";
        private const string NewsSource = "TestNewsSource";
        private const string NewsContent = "News content";
    }
}
