using System;
using System.Linq;
using Feather.Widgets.TestUI.Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FeatherWidgets.TestUI.TestCases.CommentsAndReviews
{
    /// <summary>
    /// ChangeReviewsStatusesForPage test class.
    /// </summary>
    [TestClass]
    public class ChangeReviewsStatusesForPage_ : FeatherTestCase
    {
        /// <summary>
        /// UI test ChangeReviewsStatusesForPage
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.SitefinityTeam2),
        TestCategory(FeatherTestCategories.PagesAndContent),
        TestCategory(FeatherTestCategories.CommentsAndReviews),
        TestCategory(FeatherTestCategories.Bootstrap)]
        public void ChangeReviewsStatusesForPage()
        {
            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower(), false, this.Culture);
            BATFeather.Wrappers().Frontend().CommentsAndReviews().ReviewsWrapper().AssertMessageAndCountOnPage(ReviewMessage);
            BATFeather.Wrappers().Frontend().CommentsAndReviews().ReviewsWrapper().VerifyAlertMessageOnTheFrontend(AllertMessage);

            this.PublishReviewAndVerifyFrontEnd();

            BAT.Arrange(this.TestName).ExecuteArrangement("MarkAsSpamReview");
            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower(), false, this.Culture);
            BATFeather.Wrappers().Frontend().CommentsAndReviews().ReviewsWrapper().AssertMessageAndCountOnPage(ReviewMessage);
            BATFeather.Wrappers().Frontend().CommentsAndReviews().ReviewsWrapper().VerifyAlertMessageOnTheFrontend(AllertMessage);

            this.PublishReviewAndVerifyFrontEnd();

            BAT.Arrange(this.TestName).ExecuteArrangement("HideReview");
            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower(), false, this.Culture);
            BATFeather.Wrappers().Frontend().CommentsAndReviews().ReviewsWrapper().AssertMessageAndCountOnPage(ReviewMessage);
            BATFeather.Wrappers().Frontend().CommentsAndReviews().ReviewsWrapper().VerifyAlertMessageOnTheFrontend(AllertMessage);

            this.PublishReviewAndVerifyFrontEnd();
        }

        public void PublishReviewAndVerifyFrontEnd()
        {
            BAT.Arrange(this.TestName).ExecuteArrangement("PublishReview");
            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower(), false, this.Culture);
            BATFeather.Wrappers().Frontend().CommentsAndReviews().CommentsWrapper().AssertMessageAndCountOnPage(ReviewsCount);
            BATFeather.Wrappers().Frontend().CommentsAndReviews().ReviewsWrapper().VerifyReviewsAuthorRaitingAndContent(this.reviewAuthor, this.reviewsToPage, this.reviewRaiting);
            BATFeather.Wrappers().Frontend().CommentsAndReviews().ReviewsWrapper().VerifyAlertMessageOnTheFrontend(AllertMessage);
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

        private const string PageName = "ReviewsPage";
        private readonly string[] reviewsToPage = { "Reviews to page" };
        private readonly string[] reviewAuthor = { FeatherTestCase.AdminNickname };
        private readonly string[] reviewRaiting = { "(2)" };
        private const string ReviewsCount = "1 review";
        private const string ReviewMessage = "Write a review";
        private const string AllertMessage = "You've already submitted a review for this item";
    }
}
