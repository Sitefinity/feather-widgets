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
    /// ReviewsFrontendShowOldestAndNewestOptions test class.
    /// </summary>
    [TestClass]
    public class ReviewsFrontendShowOldestAndNewestOptions_ : FeatherTestCase
    {
        /// <summary>
        /// UI test ReviewsFrontendShowOldestAndNewestOptions
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.Team2),
        TestCategory(FeatherTestCategories.PagesAndContent),
        TestCategory(FeatherTestCategories.CommentsAndReviews),
        TestCategory(FeatherTestCategories.Bootstrap)]
        public void ReviewsFrontendShowOldestAndNewestOptions()
        {
            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower(), false);
            BATFeather.Wrappers().Frontend().CommentsAndReviews().ReviewsWrapper().AssertExpectedCount(ReviewMessage);
            BATFeather.Wrappers().Frontend().CommentsAndReviews().ReviewsWrapper().ClickCountLink();
            BATFeather.Wrappers().Frontend().CommentsAndReviews().ReviewsWrapper().AssertMessageAndCountOnPage(ReviewMessage);
            BATFeather.Wrappers().Frontend().CommentsAndReviews().ReviewsWrapper().TypeAMessage(this.reviewsToNewsOldest[0]);
            BATFeather.Wrappers().Frontend().CommentsAndReviews().ReviewsWrapper().ClickRaitingStar(Raiting);
            BATFeather.Wrappers().Frontend().CommentsAndReviews().ReviewsWrapper().TypeYourName(this.reviewAuthorOldest[0]);
            BATFeather.Wrappers().Frontend().CommentsAndReviews().ReviewsWrapper().ClickSubmitButton();

            BAT.Macros().User().EnsureAdminLoggedIn();
            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower(), false);
            BATFeather.Wrappers().Frontend().CommentsAndReviews().ReviewsWrapper().AssertExpectedCount(ReviewsCount);
            BATFeather.Wrappers().Frontend().CommentsAndReviews().ReviewsWrapper().ClickCountLink();
            BATFeather.Wrappers().Frontend().CommentsAndReviews().ReviewsWrapper().VerifyShowOldestAndNewstOnTopLinksAreNotVisible();
            BATFeather.Wrappers().Frontend().CommentsAndReviews().ReviewsWrapper().TypeAMessage(this.reviewsToNewsOldest[1]);
            BATFeather.Wrappers().Frontend().CommentsAndReviews().ReviewsWrapper().ClickRaitingStar(Raiting);
            BATFeather.Wrappers().Frontend().CommentsAndReviews().ReviewsWrapper().ClickSubmitButton();
            BATFeather.Wrappers().Frontend().CommentsAndReviews().ReviewsWrapper().AssertExpectedCount(ReviewsCountNew);
            BATFeather.Wrappers().Frontend().CommentsAndReviews().ReviewsWrapper().VerifyAverageRaiting(this.reviewRaiting[0]);
            BATFeather.Wrappers().Frontend().CommentsAndReviews().ReviewsWrapper().ClickOldestOnTopLink();
            BATFeather.Wrappers().Frontend().CommentsAndReviews().ReviewsWrapper().VerifyReviewsAuthorRaitingAndContent(this.reviewAuthorNewest, this.reviewsToNewsNewest, this.reviewRaiting);
            BATFeather.Wrappers().Frontend().CommentsAndReviews().ReviewsWrapper().ClickNewestOnTopLink();
            BATFeather.Wrappers().Frontend().CommentsAndReviews().ReviewsWrapper().VerifyReviewsAuthorRaitingAndContent(this.reviewAuthorOldest, this.reviewsToNewsOldest, this.reviewRaiting);
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

        private const string PageName = "NewsPage";
        private const string NewsTitle = "NewsTitle";
        private const string ReviewsCount = "1 review";
        private const string ReviewsCountNew = "2 reviews";
        private string[] reviewsToNewsNewest = { "Reviews to news", "Reviews to news new" };
        private string[] reviewsToNewsOldest = { "Reviews to news new", "Reviews to news" };
        private string[] reviewAuthorOldest = { "New user", "admin" };
        private string[] reviewAuthorNewest = { "admin", "New user" };
        private const int Raiting = 3;
        private string[] reviewRaiting = { "(3)", "(3)" };
        private const string ReviewMessage = "Write a review";
    }
}
