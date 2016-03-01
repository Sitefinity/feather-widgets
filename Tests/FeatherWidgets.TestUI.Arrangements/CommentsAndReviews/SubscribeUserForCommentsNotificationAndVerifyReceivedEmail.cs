﻿using System;
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
    /// SubscribeUserForCommentsNotificationAndVerifyReceivedEmail arrangement class.
    /// </summary>
    public class SubscribeUserForCommentsNotificationAndVerifyReceivedEmail : TestArrangementBase
    {
        /// <summary>
        /// Server side set up.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "Telerik.Sitefinity.TestUtilities.CommonOperations.CommentOperations.CreateComment(System.String,System.String,System.Guid,System.String,System.String,System.String,System.Boolean)"), ServerSetUp]
        public void SetUp()
        {
            ServerOperations.Comments().AllowComments(ThreadType, true);

            // Use the following line once the Sitefinity is updated to 9.0.
            Telerik.Sitefinity.TestUtilities.Services.Notifications.NotificationsTestHelper.ResetDummySmtpSenderData(profileName: SubscribeUserForCommentsNotificationAndVerifyReceivedEmail.NotificationsProfileName);
            
            // Use the following line if Sitefinity is < 9.0.
            //// NotificationsTestHelper.ResetDummySmtpSenderData(profileName: SubscribeUserForCommentsNotificationAndVerifyReceivedEmail.NotificationsProfileName);
            
            ServerOperations.Comments().SetCommentsNotificationProfile(SubscribeUserForCommentsNotificationAndVerifyReceivedEmail.NotificationsProfileName);

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

            // Use the following line once the Sitefinity is updated to 9.0.
            // Telerik.Sitefinity.TestUtilities.Services.Notifications.NotificationsTestHelper.ResetDummySmtpSenderData(profileName: SubscribeUserForCommentsNotificationAndVerifyReceivedEmail.NotificationsProfileName, cleanOnly: true);
            NotificationsTestHelper.ResetDummySmtpSenderData(profileName: SubscribeUserForCommentsNotificationAndVerifyReceivedEmail.NotificationsProfileName, cleanOnly: true);

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
