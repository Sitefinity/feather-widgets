using System;
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
    /// ChangeCommentsStatusesForPage test class.
    /// </summary>
    [TestClass]
    public class ChangeCommentsStatusesForPage_ : FeatherTestCase
    {
        /// <summary>
        /// UI test ChangeCommentsStatusesForPage
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.Team2),
        TestCategory(FeatherTestCategories.CommentsAndReviews),
        TestCategory(FeatherTestCategories.Bootstrap)]
        public void ChangeCommentsStatusesForPage()
        {
            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower(), false);
            BATFeather.Wrappers().Frontend().CommentsAndReviews().CommentsWrapper().AssertLeaveACommentMessage();

            this.PublishCommentAndVerifyFrontEndAndBackend();
            
            BAT.Macros().NavigateTo().Modules().Comments();
            BAT.Wrappers().Backend().Comments().ManageCommentsWrapper(ActiveBrowser).MarkAsSpamComment(this.commentToPage[0]);
            this.VerifyCommentBackend(CommentStatusSpam);
            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower(), false);
            BATFeather.Wrappers().Frontend().CommentsAndReviews().CommentsWrapper().AssertLeaveACommentMessage();

            this.PublishCommentAndVerifyFrontEndAndBackend();

            BAT.Macros().NavigateTo().Modules().Comments();
            BAT.Wrappers().Backend().Comments().ManageCommentsWrapper(ActiveBrowser).ClickHideCommentLink(this.commentToPage[0]);
            this.VerifyCommentBackend(CommentStatusHidden);
            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower(), false);
            BATFeather.Wrappers().Frontend().CommentsAndReviews().CommentsWrapper().AssertLeaveACommentMessage();

            this.PublishCommentAndVerifyFrontEndAndBackend();
        }

        public void VerifyCommentBackend(string commentsStatus)
        {
            ManageCommentsWrapper manageComments = new ManageCommentsWrapper(ActiveBrowser);
            manageComments.VerifyCommentBackend(commentsStatus, this.commentToPage[0], this.commentAuthor[0], PageName);
        }

        public void PublishCommentAndVerifyFrontEndAndBackend()
        {
            BAT.Macros().NavigateTo().Modules().Comments();
            BAT.Wrappers().Backend().Comments().ManageCommentsWrapper(ActiveBrowser).ClickPublishCommentLink(this.commentToPage[0]);
            this.VerifyCommentBackend(CommentStatusPublished);
            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower(), false);
            BATFeather.Wrappers().Frontend().CommentsAndReviews().CommentsWrapper().AssertCommentsCountOnPage(CommentsCount);
            BATFeather.Wrappers().Frontend().CommentsAndReviews().CommentsWrapper().VerifyCommentsAuthorAndContent(this.commentAuthor, this.commentToPage);
        }

        /// <summary>
        /// Performs Server Setup and prepare the system with needed data.
        /// </summary>
        protected override void ServerSetup()
        {
            BAT.Macros().User().EnsureAdminLoggedIn();
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
        private string[] commentToPage = { "Comment to page waiting for approval comment" };
        private string[] commentAuthor = { "admin" };
        private const string CommentStatusPublished = "Published";
        private const string CommentStatusSpam = "Spam";
        private const string CommentStatusHidden = "Hidden";
        private const string CommentsCount = "1comment";
    }
}
