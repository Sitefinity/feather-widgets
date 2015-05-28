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
    /// SubscribeUserForReviewsNotificationAndVerifyReceivedEmail test class.
    /// </summary>
    [TestClass]
    public class SubscribeUserForReviewsNotificationAndVerifyReceivedEmail_ : FeatherTestCase
    {
        /// <summary>
        /// UI test SubscribeUserForReviewsNotificationAndVerifyReceivedEmail
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.Team2),
        TestCategory(FeatherTestCategories.CommentsAndReviews),
        TestCategory(FeatherTestCategories.Bootstrap)]
        public void SubscribeUserForReviewsNotificationAndVerifyReceivedEmail()
        {
            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower(), false);
            BATFeather.Wrappers().Frontend().CommentsAndReviews().ReviewsWrapper().VerifySubscribeToNewReviewLinksIsPresent();
            BATFeather.Wrappers().Frontend().CommentsAndReviews().ReviewsWrapper().ClickSubscribeToNewReviewLinks();
            BATFeather.Wrappers().Frontend().CommentsAndReviews().ReviewsWrapper().VerifySuccessfullySubscribedMessageIsPresent();
            BATFeather.Wrappers().Frontend().CommentsAndReviews().ReviewsWrapper().VerifyUnsubscribeLinksIsPresent();

            BAT.Macros().User().LogOut();
            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower(), false);
            BATFeather.Wrappers().Frontend().CommentsAndReviews().ReviewsWrapper().TypeAMessage(this.reviewsToPage[0]);
            BATFeather.Wrappers().Frontend().CommentsAndReviews().ReviewsWrapper().ClickRaitingStar(Raiting);
            BATFeather.Wrappers().Frontend().CommentsAndReviews().ReviewsWrapper().TypeYourName(this.reviewAuthor[0]);            
            BATFeather.Wrappers().Frontend().CommentsAndReviews().ReviewsWrapper().ClickSubmitButton();
            BATFeather.Wrappers().Frontend().CommentsAndReviews().ReviewsWrapper().VerifyReviewsAuthorRaitingAndContent(this.reviewAuthor, this.reviewsToPage, this.reviewRaiting);
            BATFeather.Wrappers().Frontend().CommentsAndReviews().ReviewsWrapper().VerifyAverageRaiting(this.reviewRaiting[0]);
            BATFeather.Wrappers().Frontend().CommentsAndReviews().ReviewsWrapper().AssertMessageAndCountOnPage(ReviewsCount);

            BAT.Arrange(this.TestName).ExecuteArrangement("VerifyMessageReceived");
            BAT.Arrange(this.TestName).ExecuteArrangement("VerifyMessageContent");
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
        private string[] reviewsToPage = { "This is test review" };
        private string[] reviewAuthor = { "New user" };
        private string[] reviewRaiting = { "(3)" };
        private const int Raiting = 3;
        private const string ReviewsCount = "1 review";
        private const string AllertMessage = "Thank you! Your review has been submitted successfully";
    }
}
