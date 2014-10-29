using System;
using FeatherWidgets.TestUtilities.CommonOperations;
using Telerik.Sitefinity.Frontend.TestUtilities.CommonOperations;
using Telerik.Sitefinity.TestUI.Arrangements.Framework;
using Telerik.Sitefinity.TestUI.Arrangements.Framework.Attributes;
using Telerik.Sitefinity.TestUtilities.CommonOperations;

namespace FeatherWidgets.TestUI.Arrangements
{
    /// <summary>
    /// SelectMoreThanOneNewsItem arragement.
    /// </summary>
    public class SelectMoreThanOneNewsItem : ITestArrangement
    {
        [ServerSetUp]
        public void SetUp()
        {
            for (int i = 0; i < 20; i++)
            {
                ServerOperations.News().CreatePublishedNewsItem(newsTitle: NewsItemTitle + i, newsContent: NewsItemContent + i, author: NewsItemAuthor + i);
            }

            Guid pageId = ServerOperations.Pages().CreatePage(PageName);

            ServerOperationsFeather.Pages().AddNewsWidgetToPage(pageId);
        }

        [ServerTearDown]
        public void TearDown()
        {
            ServerOperations.Pages().DeleteAllPages();
            ServerOperations.News().DeleteAllNews();
        }

        private const string PageName = "News";
        private const string NewsItemTitle = "News Item Title";
        private const string NewsItemContent = "This is a news item.";
        private const string NewsItemAuthor = "NewsWriter";
    }
}
