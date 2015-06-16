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
    /// SubmitWaitingForApprovalCommentForPageNotLoggedUserOnBootstrapPage arrangement class.
    /// </summary>
    public class SubmitWaitingForApprovalCommentForPageNotLoggedUserOnBootstrapPage : ITestArrangement
    {
        /// <summary>
        /// Server side set up.
        /// </summary>
        [ServerSetUp]
        public void SetUp()
        {
            AuthenticationHelper.AuthenticateUser(AdminUserName, AdminPass, true);

            ServerOperations.Comments().AllowComments(ThreadType, true);
            ServerOperations.Comments().RequireApproval(ThreadType, true);

            Guid templateId = Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.Templates().GetTemplateIdByTitle(PageTemplateName);
            Guid pageId = Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.Pages().CreatePage(PageName, templateId);
            pageId = ServerOperations.Pages().GetPageNodeId(pageId);
            ServerOperationsFeather.Pages().AddCommentsWidgetToPage(pageId, "Contentplaceholder1");
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
        /// Tears down.
        /// </summary>
        [ServerTearDown]
        public void TearDown()
        {
            AuthenticationHelper.AuthenticateUser(AdminUserName, AdminPass, true);

            ServerOperations.Pages().DeleteAllPages();
            var siteID = ServerOperations.Comments().GetCurrentSiteId.ToString();
            ServerOperations.Comments().DeleteAllComments(siteID);
            ServerOperations.Comments().RequireApproval(ThreadType, false);
            ServerOperations.Comments().AllowComments(ThreadType, false);
        }

        private const string PageName = "CommentsPage";
        private const string PageTemplateName = "Bootstrap.default";
        private const string ThreadType = "Telerik.Sitefinity.Pages.Model.PageNode";
        private const string AdminUserName = "admin";
        private const string AdminPass = "admin@2";
        private const string CommentStatusPublish = "Published";
    }
}
