using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FeatherWidgets.TestUtilities.CommonOperations;
using Telerik.Sitefinity.TestUI.Arrangements.Framework;
using Telerik.Sitefinity.TestUI.Arrangements.Framework.Attributes;
using Telerik.Sitefinity.TestUtilities.CommonOperations;

namespace FeatherWidgets.TestUI.Arrangements
{
    /// <summary>
    /// SubmitCommentForNewsLoggedUserOnBootstrapPage arrangement class.
    /// </summary>
    public class CommentsFrontendLoadMoreLink : ITestArrangement
    {
        /// <summary>
        /// Server side set up.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "Telerik.Sitefinity.TestUtilities.CommonOperations.CommentOperations.CreatePublishedComments(System.Int32,System.String,System.String,System.Guid,System.String,System.String)"), ServerSetUp]
        public void SetUp()
        {
            Guid newsId = ServerOperations.News().CreatePublishedNewsItemLiveId(NewsTitle, NewsContent, NewsAuthor, NewsSource);

            Guid templateId = Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.Templates().GetTemplateIdByTitle(PageTemplateName);
            Guid pageId = Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.Pages().CreatePage(PageName, templateId);
            pageId = ServerOperations.Pages().GetPageNodeId(pageId);
            ServerOperationsFeather.Pages().AddNewsWidgetToPage(pageId, "Contentplaceholder1");

            ServerOperations.Comments().CreatePublishedComments(50, Key, ThreadType, newsId, NewsTitle, CommentMessage);
        }

        /// <summary>
        /// Tears down.
        /// </summary>
        [ServerTearDown]
        public void TearDown()
        {
            ServerOperations.Pages().DeleteAllPages();
            ServerOperations.News().DeleteAllNews();
            ServerOperations.Comments().DeleteAllComments(Key);
        }

        private const string PageName = "NewsPage";
        private const string PageTemplateName = "Bootstrap.default";
        private const string NewsContent = "News content";
        private const string NewsTitle = "NewsTitle";
        private const string NewsAuthor = "TestNewsAuthor";
        private const string NewsSource = "TestNewsSource";
        private const string NewsProvider = "Default News";
        private const string Key = "Telerik.Sitefinity.Modules.News.NewsManager_OpenAccessDataProvider";
        private const string CommentMessage = "Comment";
        private const string ThreadType = "Telerik.Sitefinity.News.Model.NewsItem";
    }
}
