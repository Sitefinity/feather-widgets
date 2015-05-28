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
    /// ValidateAllFieldsInReviewsWidgetOnFrontend test class.
    /// </summary>
    [TestClass]
    public class ValidateAllFieldsInReviewsWidgetOnFrontend_ : FeatherTestCase
    {
        /// <summary>
        /// UI test ValidateAllFieldsInReviewsWidgetOnFrontend
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.Team2),
        TestCategory(FeatherTestCategories.CommentsAndReviews),
        TestCategory(FeatherTestCategories.Bootstrap)]
        public void ValidateAllFieldsInReviewsWidgetOnFrontend()
        {
            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower(), false);
            BATFeather.Wrappers().Frontend().CommentsAndReviews().ReviewsWrapper().AssertMessageAndCountOnPage(ReviewMessage);
            BATFeather.Wrappers().Frontend().CommentsAndReviews().ReviewsWrapper().ClickSubmitButton();
            BATFeather.Wrappers().Frontend().CommentsAndReviews().ReviewsWrapper().VerifyErrorMessageOnTheFrontend(MessageIsRequired);

            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower(), false);
            BATFeather.Wrappers().Frontend().CommentsAndReviews().ReviewsWrapper().TypeAMessage(this.reviewsToPage[0]);
            BATFeather.Wrappers().Frontend().CommentsAndReviews().ReviewsWrapper().ClickSubmitButton();
            BATFeather.Wrappers().Frontend().CommentsAndReviews().ReviewsWrapper().VerifyErrorMessageOnTheFrontend(RaitingIsRequired);

            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower(), false);
            BATFeather.Wrappers().Frontend().CommentsAndReviews().ReviewsWrapper().TypeAMessage(this.reviewsToPage[0]);
            BATFeather.Wrappers().Frontend().CommentsAndReviews().ReviewsWrapper().ClickRaitingStar(Raiting);
            BATFeather.Wrappers().Frontend().CommentsAndReviews().ReviewsWrapper().ClickSubmitButton();
            BATFeather.Wrappers().Frontend().CommentsAndReviews().ReviewsWrapper().VerifyErrorMessageOnTheFrontend(AuthorIsRequired);

            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower(), false);
            BATFeather.Wrappers().Frontend().CommentsAndReviews().ReviewsWrapper().TypeAMessage(this.reviewsToPage[0]);
            BATFeather.Wrappers().Frontend().CommentsAndReviews().ReviewsWrapper().ClickRaitingStar(Raiting);
            BATFeather.Wrappers().Frontend().CommentsAndReviews().ReviewsWrapper().TypeYourName(this.reviewAuthor[0]);
            BATFeather.Wrappers().Frontend().CommentsAndReviews().ReviewsWrapper().TypeEmailAddress(this.reviewAuthorInvalidEmail[0]);
            BATFeather.Wrappers().Frontend().CommentsAndReviews().ReviewsWrapper().ClickSubmitButton();
            BATFeather.Wrappers().Frontend().CommentsAndReviews().ReviewsWrapper().VerifyErrorMessageOnTheFrontend(InvalidEmail);

            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower(), false);
            BATFeather.Wrappers().Frontend().CommentsAndReviews().ReviewsWrapper().TypeAMessage(this.reviewsToPage[0]);
            BATFeather.Wrappers().Frontend().CommentsAndReviews().ReviewsWrapper().ClickRaitingStar(Raiting);
            BATFeather.Wrappers().Frontend().CommentsAndReviews().ReviewsWrapper().TypeYourName(this.reviewAuthor[0]);
            BATFeather.Wrappers().Frontend().CommentsAndReviews().ReviewsWrapper().TypeEmailAddress(this.reviewAuthorEmail[0]);
            BATFeather.Wrappers().Frontend().CommentsAndReviews().ReviewsWrapper().ClickSubmitButton();

            BATFeather.Wrappers().Frontend().CommentsAndReviews().ReviewsWrapper().VerifyReviewsAuthorRaitingAndContent(this.reviewAuthor, this.reviewsToPage, this.reviewRaiting);
            BATFeather.Wrappers().Frontend().CommentsAndReviews().ReviewsWrapper().VerifyAverageRaiting(this.reviewRaiting[0]);
            BATFeather.Wrappers().Frontend().CommentsAndReviews().ReviewsWrapper().AssertMessageAndCountOnPage(ReviewsCount);

            this.VerifyCommentBackend();
        }

        public void VerifyCommentBackend()
        {
            BAT.Macros().User().EnsureAdminLoggedIn();

            BAT.Macros().NavigateTo().Modules().Comments();
            ActiveBrowser.WaitForAsyncJQueryRequests();
            ManageCommentsWrapper manageComments = new ManageCommentsWrapper(ActiveBrowser);
            manageComments.VerifyCommentBackend(ReviewsStatus, this.reviewsToPage[0], this.reviewAuthor[0], PageName);
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

        private const string PageName = "ReviewsPage";
        private const string ReviewMessage = "Write a review";
        private string[] reviewsToPage = { "Reviews to page" };
        private string[] reviewAuthor = { "New user" };
        private string[] reviewAuthorInvalidEmail = { "user" };
        private string[] reviewAuthorEmail = { "newuser@test.com" };
        private const string ReviewsStatus = "Published";
        private const string ReviewsCount = "1 review";
        private const string MessageIsRequired = "Message is required!";
        private const string RaitingIsRequired = "Rating is required!";
        private const string AuthorIsRequired = "Author name is required!";
        private const string InvalidEmail = "Invalid email format!";                       
        private string[] reviewRaiting = { "(3)" };
        private const int Raiting = 3;               
        private const string AllertMessage = "Thank you! Your review has been submitted successfully";
    }
}
