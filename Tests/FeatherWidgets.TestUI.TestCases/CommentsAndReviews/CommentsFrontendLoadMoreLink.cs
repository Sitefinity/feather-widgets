using System;
using System.Linq;
using Feather.Widgets.TestUI.Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FeatherWidgets.TestUI.TestCases.CommentsAndReviews
{
    /// <summary>
    /// CommentsFrontendLoadMoreLink test class.
    /// </summary>
    [TestClass]
    public class CommentsFrontendLoadMoreLink_ : FeatherTestCase
    {
        /// <summary>
        /// UI test CommentsFrontendLoadMoreLink
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.SitefinityTeam2),
        TestCategory(FeatherTestCategories.PagesAndContent),
        TestCategory(FeatherTestCategories.CommentsAndReviews),
        TestCategory(FeatherTestCategories.Bootstrap)]
        public void CommentsFrontendLoadMoreLink()
        {
            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower(), false, this.Culture);
            BATFeather.Wrappers().Frontend().CommentsAndReviews().CommentsWrapper().AssertExpectedCount(CommentsCount);
            BATFeather.Wrappers().Frontend().CommentsAndReviews().CommentsWrapper().ClickCountLink();
            BATFeather.Wrappers().Frontend().CommentsAndReviews().CommentsWrapper().VerifyLoadMoreLinkIsNotVisible();
            BATFeather.Wrappers().Frontend().CommentsAndReviews().CommentsWrapper().TypeAMessage(this.commentToNews[0]);
            BATFeather.Wrappers().Frontend().CommentsAndReviews().CommentsWrapper().ClickSubmitButton();
            BATFeather.Wrappers().Frontend().CommentsAndReviews().CommentsWrapper().ClickLoadMoreLink();
            BATFeather.Wrappers().Frontend().CommentsAndReviews().CommentsWrapper().AssertExpectedCount(CommentsCountNew);
            BATFeather.Wrappers().Frontend().CommentsAndReviews().CommentsWrapper().VerifyCommentsAuthorAndContent(this.commentAuthor, this.commentToNews);
        }

        /// <summary>
        /// Performs Server Setup and prepare the system with needed data.
        /// </summary>
        protected override void ServerSetup()
        {
            BAT.Macros().User().EnsureAdminLoggedIn();
            BAT.Arrange(this.TestName).ExecuteSetUp();

            for (int i = 0; i <= 50; i++)
            {
                this.commentToNews[i] = "Comment" + i;
                this.commentAuthor[i] = FeatherTestCase.AdminNickname;
            }
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
        private string[] commentToNews = new string[51];
        private string[] commentAuthor = new string[51];
        private const string CommentStatus = "Published";
        private const string CommentsCount = "50 comments";
        private const string CommentsCountNew = "51 comments";
    }
}
