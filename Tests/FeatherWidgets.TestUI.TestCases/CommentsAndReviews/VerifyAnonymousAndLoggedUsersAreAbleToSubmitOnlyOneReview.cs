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
    /// VerifyAnonymousAndLoggedUsersAreAbleToSubmitOnlyOneReview test class.
    /// </summary>
    [TestClass]
    public class VerifyAnonymousAndLoggedUsersAreAbleToSubmitOnlyOneReview_ : FeatherTestCase
    {
        /// <summary>
        /// UI test VerifyAnonymousAndLoggedUsersAreAbleToSubmitOnlyOneReview
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.Team2),
        TestCategory(FeatherTestCategories.PagesAndContent),
        TestCategory(FeatherTestCategories.CommentsAndReviews),
        TestCategory(FeatherTestCategories.Bootstrap)]
        public void VerifyAnonymousAndLoggedUsersAreAbleToSubmitOnlyOneReview()
        {            
            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower(), false);
            BATFeather.Wrappers().Frontend().CommentsAndReviews().ReviewsWrapper().TypeAMessage(this.reviewsToPage[0]);
            BATFeather.Wrappers().Frontend().CommentsAndReviews().ReviewsWrapper().ClickRaitingStar(1);
            BATFeather.Wrappers().Frontend().CommentsAndReviews().ReviewsWrapper().TypeYourName(this.reviewAuthor[0]);
            BATFeather.Wrappers().Frontend().CommentsAndReviews().ReviewsWrapper().ClickSubmitButton();
            BATFeather.Wrappers().Frontend().CommentsAndReviews().ReviewsWrapper().VerifyAlertMessageOnTheFrontend(AllertMessageThankYou);

            BATFeather.Wrappers().Frontend().CommentsAndReviews().ReviewsWrapper().VerifyReviewsAuthorRaitingAndContent(this.reviewAuthor, this.reviewsToPage, this.reviewRaiting);
            BATFeather.Wrappers().Frontend().CommentsAndReviews().ReviewsWrapper().VerifyAverageRaiting(this.reviewRaiting[0]);
            BATFeather.Wrappers().Frontend().CommentsAndReviews().ReviewsWrapper().AssertMessageAndCountOnPage(ReviewsCount);
            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower(), false);
            BATFeather.Wrappers().Frontend().CommentsAndReviews().ReviewsWrapper().VerifyAlertMessageOnTheFrontend(AllertMessage);
            BATFeather.Wrappers().Frontend().CommentsAndReviews().ReviewsWrapper().VerifyReviewsTextAreaIsNotVisible();

            BAT.Macros().NavigateTo().CustomPage("~/" + LoginPage.ToLower());
            BAT.Wrappers().Backend().LoginView().LoginViewWrapper().SetUsername(NewUserName);
            BAT.Wrappers().Backend().LoginView().LoginViewWrapper().SetPassword(NewUserPassword);
            BAT.Wrappers().Backend().LoginView().LoginViewWrapper().ExecuteLogin();

            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower(), false);
            BATFeather.Wrappers().Frontend().CommentsAndReviews().ReviewsWrapper().TypeAMessage(this.reviewsToPageAll[0]);
            BATFeather.Wrappers().Frontend().CommentsAndReviews().ReviewsWrapper().ClickRaitingStar(3);
            BATFeather.Wrappers().Frontend().CommentsAndReviews().ReviewsWrapper().ClickSubmitButton();
            BATFeather.Wrappers().Frontend().CommentsAndReviews().ReviewsWrapper().VerifyAlertMessageOnTheFrontend(AllertMessageThankYou);

            BATFeather.Wrappers().Frontend().CommentsAndReviews().ReviewsWrapper().VerifyReviewsAuthorRaitingAndContent(this.reviewAuthorAll, this.reviewsToPageAll, this.reviewRaitingAll);
            BATFeather.Wrappers().Frontend().CommentsAndReviews().ReviewsWrapper().VerifyAverageRaiting(this.averageRaiting[0]);
            BATFeather.Wrappers().Frontend().CommentsAndReviews().ReviewsWrapper().AssertMessageAndCountOnPage(AllReviewsCount);
            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower(), false);
            BATFeather.Wrappers().Frontend().CommentsAndReviews().ReviewsWrapper().VerifyAlertMessageOnTheFrontend(AllertMessage);
            BATFeather.Wrappers().Frontend().CommentsAndReviews().ReviewsWrapper().VerifyReviewsTextAreaIsNotVisible();
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

        private const string ReviewMessage = "Write a review";
        private const string ReviewsCount = "1 review";
        private const string AllertMessageThankYou = "Thank you! Your review has been submitted successfully";
        private const string AllertMessage = "You've already submitted a review for this item";
        private const string PageName = "ReviewsPage";
        private string[] reviewsToPage = { "Reviews to page from Anonymous user" };
        private string[] reviewAuthor = { "Anonymous user" };
        private string[] reviewRaiting = { "(3)" };
        private string[] reviewsToPageAll = { "Reviews to page from new user", "Reviews to page from Anonymous user" };
        private string[] reviewAuthorAll = { "newUser", "Anonymous user" };
        private string[] reviewRaitingAll = { "(3)", "(3)" };
        private string[] averageRaiting = { "(3)" };
        private const string AllReviewsCount = "2 reviews";
        private int[] raiting = { 3, 3 };
        private const string NewUserName = "newUser";
        private const string NewUserPassword = "password";
        private const string LoginPage = "Sitefinity";
    }
}
