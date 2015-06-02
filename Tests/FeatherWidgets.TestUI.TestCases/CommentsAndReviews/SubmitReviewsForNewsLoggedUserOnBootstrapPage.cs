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
    /// SubmitReviewsForNewsLoggedUserOnBootstrapPage test class.
    /// </summary>
    [TestClass]
    public class SubmitReviewsForNewsLoggedUserOnBootstrapPage_ : FeatherTestCase
    {
        /// <summary>
        /// UI test SubmitReviewsForNewsLoggedUserOnBootstrapPage
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.Team2),
        TestCategory(FeatherTestCategories.PagesAndContent),
        TestCategory(FeatherTestCategories.CommentsAndReviews),
        TestCategory(FeatherTestCategories.Bootstrap)]
        public void SubmitReviewsForNewsLoggedUserOnBootstrapPage()
        {
            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower(), false);
            BATFeather.Wrappers().Frontend().CommentsAndReviews().ReviewsWrapper().AssertExpectedCount(ReviewMessage);
            BATFeather.Wrappers().Frontend().CommentsAndReviews().ReviewsWrapper().ClickCountLink();
            BATFeather.Wrappers().Frontend().CommentsAndReviews().ReviewsWrapper().AssertMessageAndCountOnPage(ReviewMessage);
            BATFeather.Wrappers().Frontend().CommentsAndReviews().ReviewsWrapper().TypeAMessage(this.reviewsToNews[0]);
            BATFeather.Wrappers().Frontend().CommentsAndReviews().ReviewsWrapper().ClickRaitingStar(Raiting);
            BATFeather.Wrappers().Frontend().CommentsAndReviews().ReviewsWrapper().ClickSubmitButton();
            BATFeather.Wrappers().Frontend().CommentsAndReviews().ReviewsWrapper().VerifyAlertMessageOnTheFrontend(AllertMessage);
            BATFeather.Wrappers().Frontend().CommentsAndReviews().ReviewsWrapper().VerifyReviewsAuthorRaitingAndContent(this.reviewAuthor, this.reviewsToNews, this.reviewRaiting);
            BATFeather.Wrappers().Frontend().CommentsAndReviews().ReviewsWrapper().VerifyAverageRaiting(this.reviewRaiting[0]);
            BATFeather.Wrappers().Frontend().CommentsAndReviews().ReviewsWrapper().AssertMessageAndCountOnPage(ReviewsCount);
            this.VerifyCommentBackend();
        }

        public void VerifyCommentBackend()
        {
            BAT.Macros().NavigateTo().Modules().Comments();
            ActiveBrowser.WaitForAsyncJQueryRequests();
            ManageCommentsWrapper manageComments = new ManageCommentsWrapper(ActiveBrowser);
            manageComments.VerifyCommentBackend(ReviewsStatus, this.reviewsToNews[0], this.reviewAuthor[0], NewsTitle);
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
        private const string ReviewMessage = "Write a review";
        private string[] reviewsToNews = { "Reviews to news" };
        private string[] reviewAuthor = { "admin" };
        private string[] reviewRaiting = { "(3)" };
        private const int Raiting = 3;
        private const string ReviewsStatus = "Published";
        private const string ReviewsCount = "1 review";
        private const string AllertMessage = "Thank you! Your review has been submitted successfully";
    }
}
