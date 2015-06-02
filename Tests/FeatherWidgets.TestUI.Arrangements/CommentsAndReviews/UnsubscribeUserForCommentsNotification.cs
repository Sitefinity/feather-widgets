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
    /// UnsubscribeUserForCommentsNotification arrangement class.
    /// </summary>
    public class UnsubscribeUserForCommentsNotification : ITestArrangement
    {
        /// <summary>
        /// Server side set up.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "Telerik.Sitefinity.TestUtilities.CommonOperations.CommentOperations.CreateComment(System.String,System.String,System.Guid,System.String,System.String,System.String,System.Boolean)"), ServerSetUp]
        public void SetUp()
        {
            ServerOperations.Comments().AllowComments(ThreadType, true);

            NotificationsTestHelper.ResetDummySmtpSenderData(profileName: UnsubscribeUserForCommentsNotification.NotificationsProfileName);
            ServerOperations.Comments().SetCommentsNotificationProfile(UnsubscribeUserForCommentsNotification.NotificationsProfileName);

            Guid templateId = Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.Templates().GetTemplateIdByTitle(PageTemplateName);
            Guid pageId = Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.Pages().CreatePage(PageName, templateId);
            pageId = ServerOperations.Pages().GetPageNodeId(pageId);
            ServerOperationsFeather.Pages().AddCommentsWidgetToPage(pageId, "Contentplaceholder1");
            var groupKey = ServerOperations.Comments().GetCurrentSiteId.ToString();
            ServerOperations.Comments().CreateComment(groupKey, ThreadType, pageId, PageName, CommentToPage, "published", false);
            ServerOperations.Comments().EnableEmailSubscription(ThreadType, true);
        }

        /// <summary>
        /// Verify message received
        /// </summary>
        [ServerArrangement]
        public void VerifyMessageReceived()
        {
            ServerOperations.Comments().VerifySentNotificationCount(0);
        }

        /// <summary>
        /// Tears down.
        /// </summary>
        [ServerTearDown]
        public void TearDown()
        {
            AuthenticationHelper.AuthenticateUser("admin", "admin@2", true);
            ServerOperations.Comments().SetCommentsNotificationProfile("Default");
            NotificationsTestHelper.ResetDummySmtpSenderData(profileName: UnsubscribeUserForCommentsNotification.NotificationsProfileName, cleanOnly: true);

            ServerOperations.Pages().DeleteAllPages();
            var siteID = ServerOperations.Comments().GetCurrentSiteId.ToString();
            ServerOperations.Comments().DeleteAllComments(siteID);
            ServerOperations.Comments().AllowComments(ThreadType, false);
            ServerOperations.Comments().EnableEmailSubscription(ThreadType, false);
        }

        private const string PageName = "CommentsPage";
        private const string PageTemplateName = "Bootstrap.default";
        private const string ThreadType = "Telerik.Sitefinity.Pages.Model.PageNode";
        private const string CommentToPage = "Comment to page ";
        private const string NewCommentToPage = "This is test comment";
        private const string NotificationsProfileName = "comments_test_profile";
    }
}
