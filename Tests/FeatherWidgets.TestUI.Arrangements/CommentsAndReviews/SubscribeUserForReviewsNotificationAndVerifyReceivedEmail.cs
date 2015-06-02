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
    /// SubscribeUserForReviewsNotificationAndVerifyReceivedEmail arrangement class.
    /// </summary>
    public class SubscribeUserForReviewsNotificationAndVerifyReceivedEmail : ITestArrangement
    {
        /// <summary>
        /// Server side set up.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "Telerik.Sitefinity.TestUtilities.CommonOperations.CommentOperations.CreateComment(System.String,System.String,System.Guid,System.String,System.String,System.String,System.Boolean)"), ServerSetUp]
        public void SetUp()
        {
            ServerOperations.Comments().AllowComments(ThreadType, true);

            NotificationsTestHelper.ResetDummySmtpSenderData(profileName: SubscribeUserForReviewsNotificationAndVerifyReceivedEmail.NotificationsProfileName);
            ServerOperations.Comments().SetCommentsNotificationProfile(SubscribeUserForReviewsNotificationAndVerifyReceivedEmail.NotificationsProfileName);

            Guid templateId = Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.Templates().GetTemplateIdByTitle(PageTemplateName);
            Guid pageId = Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.Pages().CreatePage(PageName, templateId);
            pageId = ServerOperations.Pages().GetPageNodeId(pageId);
            ServerOperationsFeather.Pages().AddReviewsWidgetToPage(pageId, "Contentplaceholder1");

            ServerOperations.Comments().EnableEmailSubscription(ThreadType, true);
        }

        /// <summary>
        /// Verify message received
        /// </summary>
        [ServerArrangement]
        public void VerifyMessageReceived()
        {
            ServerOperations.Comments().VerifySomeMessagesReceived();
            ServerOperations.Comments().VerifySentNotificationCount(1);
        }

        /// <summary>
        /// Verify message content
        /// </summary>
        [ServerArrangement]
        public void VerifyMessageContent()
        {
            ServerOperations.Comments().AssertNotificationBodyContainsExpectedMessage(PageName, 1);
            ServerOperations.Comments().AssertNotificationBodyContainsExpectedMessage(NewCommentToPage, 1);
        }

        /// <summary>
        /// Tears down.
        /// </summary>
        [ServerTearDown]
        public void TearDown()
        {
            AuthenticationHelper.AuthenticateUser("admin", "admin@2", true);
            ServerOperations.Comments().SetCommentsNotificationProfile("Default");
            NotificationsTestHelper.ResetDummySmtpSenderData(profileName: SubscribeUserForReviewsNotificationAndVerifyReceivedEmail.NotificationsProfileName, cleanOnly: true);

            ServerOperations.Pages().DeleteAllPages();
            var siteID = ServerOperations.Comments().GetCurrentSiteId.ToString();
            ServerOperations.Comments().DeleteAllComments(siteID);
            ServerOperations.Comments().AllowComments(ThreadType, false);
            ServerOperations.Comments().EnableEmailSubscription(ThreadType, false);
        }

        private const string PageName = "ReviewsPage";
        private const string PageTemplateName = "Bootstrap.default";
        private const string ThreadType = "Telerik.Sitefinity.Pages.Model.PageNode";
        private const string NewCommentToPage = "This is test review";
        private const string NotificationsProfileName = "reviews_test_profile";
    }
}
