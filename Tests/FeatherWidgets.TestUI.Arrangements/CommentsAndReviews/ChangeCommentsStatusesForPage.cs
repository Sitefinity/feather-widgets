using System;
using FeatherWidgets.TestUtilities.CommonOperations;
using Telerik.Sitefinity.TestUI.Arrangements.Framework;
using Telerik.Sitefinity.TestUI.Arrangements.Framework.Attributes;
using Telerik.Sitefinity.TestUtilities.CommonOperations;

namespace FeatherWidgets.TestUI.Arrangements
{
    /// <summary>
    /// ChangeCommentsStatusesForPage arrangement class.
    /// </summary>
    public class ChangeCommentsStatusesForPage : ITestArrangement
    {
        /// <summary>
        /// Server side set up.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "Telerik.Sitefinity.TestUtilities.CommonOperations.CommentOperations.CreateComment(System.String,System.String,System.Guid,System.String,System.String,System.String,System.Boolean)"), ServerSetUp]
        public void SetUp()
        {
            ServerOperations.Comments().AllowComments(ThreadType, true);
            Guid templateId = Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.Templates().GetTemplateIdByTitle(PageTemplateName);
            Guid pageId = Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.Pages().CreatePage(PageName, templateId);
            pageId = ServerOperations.Pages().GetPageNodeId(pageId);
            ServerOperationsFeather.Pages().AddCommentsWidgetToPage(pageId, "Contentplaceholder1");

            var groupKey = ServerOperations.Comments().GetCurrentSiteId.ToString();
            ServerOperations.Comments().CreateComment(groupKey, ThreadType, pageId, PageName, CommentToPage, "waiting", true);        
        }

        /// <summary>
        /// Publish comment
        /// </summary>
        [ServerArrangement]
        public void PublishComment()
        {
            Guid pageId = ServerOperations.Pages().GetPageId(PageName);
            string threadKey = pageId.ToString() + "_en";
            ServerOperationsFeather.CommentsAndReviews().ChangeCommentsAndReviewsStatus(CommentStatusPublish, threadKey);
        }

        /// <summary>
        /// Mark as spam comment
        /// </summary>
        [ServerArrangement]
        public void MarkAsSpamComment()
        {
            Guid pageId = ServerOperations.Pages().GetPageId(PageName);
            string threadKey = pageId.ToString() + "_en";
            ServerOperationsFeather.CommentsAndReviews().ChangeCommentsAndReviewsStatus(CommentStatusSpam, threadKey);
        }

        /// <summary>
        /// Hide comment
        /// </summary>
        [ServerArrangement]
        public void HideComment()
        {
            Guid pageId = ServerOperations.Pages().GetPageId(PageName);
            string threadKey = pageId.ToString() + "_en";
            ServerOperationsFeather.CommentsAndReviews().ChangeCommentsAndReviewsStatus(CommentStatusHidden, threadKey);
        }

        /// <summary>
        /// Tears down.
        /// </summary>
        [ServerTearDown]
        public void TearDown()
        {
            ServerOperations.Pages().DeleteAllPages();
            var siteID = ServerOperations.Comments().GetCurrentSiteId.ToString();
            ServerOperations.Comments().DeleteAllComments(siteID);
            ServerOperations.Comments().AllowComments(ThreadType, false);
        }

        private const string PageName = "CommentsPage";
        private const string PageTemplateName = "Bootstrap.default";
        private const string ThreadType = "Telerik.Sitefinity.Pages.Model.PageNode";
        private const string CommentToPage = "Comment to page ";
        private const string CommentStatusPublish = "Published";
        private const string CommentStatusSpam = "Spam";
        private const string CommentStatusHidden = "Hidden";
    }
}
