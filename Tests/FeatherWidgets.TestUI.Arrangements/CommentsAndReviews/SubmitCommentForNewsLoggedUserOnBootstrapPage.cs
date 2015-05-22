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
    /// AddCommentsWidgetOnPageBasedOnBootstrapTemplate arrangement class.
    /// </summary>
    public class SubmitCommentForNewsLoggedUserOnBootstrapPage : ITestArrangement
    {
        /// <summary>
        /// Server side set up.
        /// </summary>
        [ServerSetUp]
        public void SetUp()
        {
            ServerOperations.News().CreatePublishedNewsItem(NewsTitle, NewsContent, NewsProvider);

            Guid templateId = Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.Templates().GetTemplateIdByTitle(PageTemplateName);
            Guid pageId = Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.Pages().CreatePage(PageName, templateId);
            pageId = ServerOperations.Pages().GetPageNodeId(pageId);
            ServerOperationsFeather.Pages().AddNewsWidgetToPage(pageId, "Contentplaceholder1");
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
        private const string NewsProvider = "Default News";
        private const string Key = "Telerik.Sitefinity.Modules.News.NewsManager_OpenAccessDataProvider";
    }
}
