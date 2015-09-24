﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArtOfTest.WebAii.Core;
using Feather.Widgets.TestUI.Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telerik.Sitefinity.Frontend.TestUtilities;
using Telerik.Sitefinity.TestUI.Framework.Wrappers.Backend;

namespace FeatherWidgets.TestUI.TestCases.CommentsAndReviews
{
    /// <summary>
    /// SubmitWaitingForApprovalCommentForPageNotLoggedUserOnBootstrapPage test class.
    /// </summary>
    [TestClass]
    public class SubmitWaitingForApprovalCommentForPageNotLoggedUserOnBootstrapPage_ : FeatherTestCase
    {
        /// <summary>
        /// UI test SubmitWaitingForApprovalCommentForPageNotLoggedUserOnBootstrapPage
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.FeatherTeam),
        TestCategory(FeatherTestCategories.PagesAndContent),
        TestCategory(FeatherTestCategories.CommentsAndReviews),
        TestCategory(FeatherTestCategories.Bootstrap)]
        public void SubmitWaitingForApprovalCommentForPageNotLoggedUserOnBootstrapPage()
        {
            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower(), false, this.Culture);
            BATFeather.Wrappers().Frontend().CommentsAndReviews().CommentsWrapper().AssertMessageAndCountOnPage(CommentsMessage);
            BATFeather.Wrappers().Frontend().CommentsAndReviews().CommentsWrapper().TypeAMessage(this.commentToPage[0]);
            BATFeather.Wrappers().Frontend().CommentsAndReviews().CommentsWrapper().TypeYourName(this.commentAuthor[0]);
            BATFeather.Wrappers().Frontend().CommentsAndReviews().CommentsWrapper().TypeEmailAddress(this.commentAuthorEmail[0]);
            BATFeather.Wrappers().Frontend().CommentsAndReviews().CommentsWrapper().ClickSubmitButton();
            BATFeather.Wrappers().Frontend().CommentsAndReviews().CommentsWrapper().VerifyAlertMessageOnTheFrontend(AllertMessage);
            BATFeather.Wrappers().Frontend().CommentsAndReviews().CommentsWrapper().AssertMessageAndCountOnPage(CommentsMessage);
            BAT.Macros().User().EnsureAdminLoggedIn();
            BAT.Arrange(this.TestName).ExecuteArrangement("PublishComment");
            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower(), false, this.Culture);
            BATFeather.Wrappers().Frontend().CommentsAndReviews().CommentsWrapper().AssertMessageAndCountOnPage(CommentsCount);
            BATFeather.Wrappers().Frontend().CommentsAndReviews().CommentsWrapper().VerifyCommentsAuthorAndContent(this.commentAuthor, this.commentToPage);
        }

        /// <summary>
        /// Performs Server Setup and prepare the system with needed data.
        /// </summary>
        protected override void ServerSetup()
        {
            BAT.Arrange(this.TestName).ExecuteSetUp();
        }

        /// <summary>
        /// Performs clean up and clears all data created by the test.
        /// </summary>
        protected override void ServerCleanup()
        {
            BAT.Arrange(this.TestName).ExecuteTearDown();
        }

        private const string PageName = "CommentsPage";
        private const string LeaveAComment = "Leave a comment";
        private string[] commentToPage = { "Comment to page" };
        private string[] commentAuthor = { "New user" };
        private string[] commentAuthorEmail = { "user@test.com" };
        private const string CommentStatus = "Published";
        private const string CommentStatusWaiting = "Waiting for approval";
        private const string CommentsCount = "1comment";
        private const string AllertMessage = "Thank you for the comment! Your comment must be approved first";
        private const string CommentsMessage = "Leave a comment";
    }
}
