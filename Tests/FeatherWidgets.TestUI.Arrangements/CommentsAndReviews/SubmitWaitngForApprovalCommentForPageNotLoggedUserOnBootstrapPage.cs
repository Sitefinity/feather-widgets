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
    /// SubmitWaitingForApprovalCommentForPageNotLoggedUserOnBootstrapPage arrangement class.
    /// </summary>
    public class SubmitWaitingForApprovalCommentForPageNotLoggedUserOnBootstrapPage : TestArrangementBase
    {
        /// <summary>
        /// Server side set up.
        /// </summary>
        [ServerSetUp]
        public void SetUp()
        {
            AuthenticationHelper.AuthenticateUser(this.AdminEmail, this.AdminPass, true);

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
            string culture = ServerOperationsFeather.CommentsAndReviews().GetCurrentCulture();
            string threadKey = pageId.ToString() + "_" + culture;
            ServerOperationsFeather.CommentsAndReviews().ChangeCommentsAndReviewsStatus(CommentStatusPublish, threadKey);
        }

        /// <summary>
        /// Tears down.
        /// </summary>
        [ServerTearDown]
        public void TearDown()
        {
            AuthenticationHelper.AuthenticateUser(this.AdminEmail, this.AdminPass, true);

            ServerOperations.Pages().DeleteAllPages();
            var siteID = ServerOperations.Comments().GetCurrentSiteId.ToString();
            ServerOperations.Comments().DeleteAllComments(siteID);
            ServerOperations.Comments().RequireApproval(ThreadType, false);
            ServerOperations.Comments().AllowComments(ThreadType, false);
        }

        private const string PageName = "CommentsPage";
        private const string PageTemplateName = "Bootstrap.default";
        private const string ThreadType = "Telerik.Sitefinity.Pages.Model.PageNode";
        private const string CommentStatusPublish = "Published";
    }
}
