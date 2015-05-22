using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Feather.Widgets.TestUI.Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telerik.Sitefinity.Frontend.TestUtilities;
using Telerik.Sitefinity.TestUI.Framework.Wrappers.Backend;

namespace FeatherWidgets.TestUI.TestCases.CommentsAndReviews
{
    /// <summary>
    /// SubmitCommentForNewsLoggedUserOnBootstrapPage test class.
    /// </summary>
    [TestClass]
    public class SubmitCommentForNewsLoggedUserOnBootstrapPage_ : FeatherTestCase
    {
        /// <summary>
        /// UI test SubmitCommentForNewsLoggedUserOnBootstrapPage
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.Team2),
        TestCategory(FeatherTestCategories.CommentsAndReviews),
        TestCategory(FeatherTestCategories.Bootstrap)]
        public void SubmitCommentForNewsLoggedUserOnBootstrapPage()
        {
            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower(), false);
            BATFeather.Wrappers().Frontend().News().NewsWrapper().ClickNewsTitle(NewsTitle);
            ////BATFeather.Wrappers().Frontend().CommentsAndReviews().CommentsWrapper().AssertCommentsCountInListView(LeaveAComment);
            ////BATFeather.Wrappers().Frontend().CommentsAndReviews().CommentsWrapper().ClickCommentLink();
            ////BATFeather.Wrappers().Frontend().CommentsAndReviews().CommentsWrapper().AssertLeaveACommentMessage();
            ////BATFeather.Wrappers().Frontend().CommentsAndReviews().CommentsWrapper().TypeAComment(CommentToNews);
            ////BATFeather.Wrappers().Frontend().CommentsAndReviews().CommentsWrapper().ClickSubmitButton();
            BATFeather.Wrappers().Frontend().CommentsAndReviews().CommentsWrapper().VerifyCommentsAuthor(CommentAuthor);
            BATFeather.Wrappers().Frontend().CommentsAndReviews().CommentsWrapper().VerifySubmittedComment(CommentToNews);
            BAT.Macros().NavigateTo().Modules().Comments();
            this.VerifyCommentBackend();
        }

        public void VerifyCommentBackend()
        {
            ManageCommentsWrapper manageComments = new ManageCommentsWrapper(ActiveBrowser);
            manageComments.VerifyCommentBackend(CommentStatus, CommentToNews, CommentAuthor, NewsTitle);
        }

        /// <summary>
        /// Performs Server Setup and prepare the system with needed data.
        /// </summary>
        protected override void ServerSetup()
        {
            BAT.Macros().User().EnsureAdminLoggedIn();
           // BAT.Arrange(this.TestName).ExecuteSetUp();
        }

        /// <summary>
        /// Performs clean up and clears all data created by the test.
        /// </summary>
        protected override void ServerCleanup()
        {
            ////BAT.Arrange(this.TestName).ExecuteTearDown();
        }

        private const string PageName = "NewsPage";
        private const string NewsTitle = "NewsTitle";
        private const string LeaveAComment = "Leave a comment";
        private const string CommentToNews = "Comment to news";
        private const string CommentAuthor = "admin";
        private const string CommentStatus = "Published";
    }
}
