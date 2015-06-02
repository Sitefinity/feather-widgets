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
    /// SubmitCommentForDynamicItemLoggedUserOnBootstrapPage test class.
    /// </summary>
    [TestClass]
    public class SubmitCommentForDynamicItemLoggedUserOnBootstrapPage_ : FeatherTestCase
    {
        /// <summary>
        /// UI test SubmitCommentForDynamicItemLoggedUserOnBootstrapPage
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.Team2),
        TestCategory(FeatherTestCategories.PagesAndContent),
        TestCategory(FeatherTestCategories.CommentsAndReviews),
        TestCategory(FeatherTestCategories.Bootstrap)]
        public void SubmitCommentForDynamicItemLoggedUserOnBootstrapPage()
        {
            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower(), false);
            BATFeather.Wrappers().Frontend().CommentsAndReviews().CommentsWrapper().AssertExpectedCount(LeaveAComment);
            BATFeather.Wrappers().Frontend().CommentsAndReviews().CommentsWrapper().ClickCountLink();
            BATFeather.Wrappers().Frontend().CommentsAndReviews().CommentsWrapper().AssertMessageAndCountOnPage(CommentsMessage);
            BATFeather.Wrappers().Frontend().CommentsAndReviews().CommentsWrapper().TypeAMessage(this.commentToDynamicItem[0]);
            BATFeather.Wrappers().Frontend().CommentsAndReviews().CommentsWrapper().ClickSubmitButton();
            BATFeather.Wrappers().Frontend().CommentsAndReviews().CommentsWrapper().VerifyCommentsAuthorAndContent(this.commentAuthor, this.commentToDynamicItem);
            BATFeather.Wrappers().Frontend().CommentsAndReviews().CommentsWrapper().AssertExpectedCount(CommentsCount);
            this.VerifyCommentBackend();
        }

        public void VerifyCommentBackend()
        {
            BAT.Macros().NavigateTo().Modules().Comments();
            ActiveBrowser.WaitForAsyncJQueryRequests();
            ManageCommentsWrapper manageComments = new ManageCommentsWrapper(ActiveBrowser);
            manageComments.VerifyCommentBackend(CommentStatus, this.commentToDynamicItem[0], this.commentAuthor[0], DynamicTitle);
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

        private const string PageName = "DynamicPage";
        private const string DynamicTitle = "Angel";
        private const string LeaveAComment = "Leave a comment";
        private string[] commentToDynamicItem = { "Comment to dynamic item" };
        private string[] commentAuthor = { "admin" };
        private const string CommentStatus = "Published";
        private const string CommentsCount = "1 comment";
        private const string CommentsMessage = "Leave a comment";
    }
}
