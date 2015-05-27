using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Feather.Widgets.TestUI.Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telerik.Sitefinity.Frontend.TestUtilities;

namespace FeatherWidgets.TestUI.TestCases.CommentsAndReviews
{
    /// <summary>
    /// CommentsFrontendShowOldestAndNewestOptions test class.
    /// </summary>
    [TestClass]
    public class CommentsFrontendShowOldestAndNewestOptions_ : FeatherTestCase
    {
        /// <summary>
        /// UI test CommentsFrontendShowOldestAndNewestOptions
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.Team2),
        TestCategory(FeatherTestCategories.CommentsAndReviews),
        TestCategory(FeatherTestCategories.Bootstrap)]
        public void CommentsFrontendShowOldestAndNewestOptions()
        {
            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower(), false);
            BATFeather.Wrappers().Frontend().CommentsAndReviews().CommentsWrapper().AssertCommentsCount(CommentsCount);
            BATFeather.Wrappers().Frontend().CommentsAndReviews().CommentsWrapper().ClickCommentLink();
            BATFeather.Wrappers().Frontend().CommentsAndReviews().CommentsWrapper().VerifyShowOldestAndNewstOnTopLinksAreNotVisible();
            BATFeather.Wrappers().Frontend().CommentsAndReviews().CommentsWrapper().TypeAMessage(this.commentToNewsOldest[1]);
            BATFeather.Wrappers().Frontend().CommentsAndReviews().CommentsWrapper().ClickSubmitButton();
            BATFeather.Wrappers().Frontend().CommentsAndReviews().CommentsWrapper().AssertCommentsCount(CommentsCountNew);
            BATFeather.Wrappers().Frontend().CommentsAndReviews().CommentsWrapper().ClickOldestOnTopLink();            
            BATFeather.Wrappers().Frontend().CommentsAndReviews().CommentsWrapper().VerifyCommentsAuthorAndContent(this.commentAuthor, this.commentToNewsOldest);
            BATFeather.Wrappers().Frontend().CommentsAndReviews().CommentsWrapper().ClickNewestOnTopLink();
            BATFeather.Wrappers().Frontend().CommentsAndReviews().CommentsWrapper().VerifyCommentsAuthorAndContent(this.commentAuthor, this.commentToNewsNewest);
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

        private const string PageName = "NewsPage";
        private const string NewsTitle = "NewsTitle";
        private const string LeaveAComment = "Leave a comment";
        private string[] commentToNewsOldest = { "Comment1", "Comment2" };
        private string[] commentToNewsNewest = { "Comment2", "Comment1" };
        private string[] commentAuthor = { "admin", "admin" };
        private const string CommentStatus = "Published";
        private const string CommentsCount = "1 comment";
        private const string CommentsCountNew = "2 comments";
    }
}
