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
    /// ChangeReviewsStatusesForPage test class.
    /// </summary>
    [TestClass]
    public class ChangeReviewsStatusesForPage_ : FeatherTestCase
    {
        /// <summary>
        /// UI test ChangeReviewsStatusesForPage
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.Team2),
        TestCategory(FeatherTestCategories.CommentsAndReviews),
        TestCategory(FeatherTestCategories.Bootstrap)]
        public void ChangeReviewsStatusesForPage()
        {
            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower(), false);
            BATFeather.Wrappers().Frontend().CommentsAndReviews().ReviewsWrapper().AssertMessageAndCountOnPage(ReviewMessage);
            BATFeather.Wrappers().Frontend().CommentsAndReviews().ReviewsWrapper().TypeAMessage(this.reviewsToPage[0]);
            BATFeather.Wrappers().Frontend().CommentsAndReviews().ReviewsWrapper().ClickRaitingStar(Raiting);
            BATFeather.Wrappers().Frontend().CommentsAndReviews().ReviewsWrapper().TypeYourName(this.reviewAuthor[0]);
            BATFeather.Wrappers().Frontend().CommentsAndReviews().ReviewsWrapper().ClickSubmitButton();
            BATFeather.Wrappers().Frontend().CommentsAndReviews().ReviewsWrapper().VerifyAlertMessageOnTheFrontendNotLoggedUser(AllertMessageWaiting);
            BATFeather.Wrappers().Frontend().CommentsAndReviews().ReviewsWrapper().AssertMessageAndCountOnPage(ReviewMessage);
            BAT.Macros().User().EnsureAdminLoggedIn();
            this.PublishCommentAndVerifyFrontEndAndBackend();

            BAT.Macros().NavigateTo().Modules().Comments();
            BAT.Wrappers().Backend().Comments().ManageCommentsWrapper(ActiveBrowser).MarkAsSpamComment(this.reviewsToPage[0]);
            this.VerifyCommentBackend(ReviewsStatusSpam);
            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower(), false);
            BATFeather.Wrappers().Frontend().CommentsAndReviews().ReviewsWrapper().AssertMessageAndCountOnPage(ReviewMessage);

            this.PublishCommentAndVerifyFrontEndAndBackend();

            BAT.Macros().NavigateTo().Modules().Comments();
            BAT.Wrappers().Backend().Comments().ManageCommentsWrapper(ActiveBrowser).ClickHideCommentLink(this.reviewsToPage[0]);
            this.VerifyCommentBackend(ReviewsStatusHidden);
            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower(), false);
            BATFeather.Wrappers().Frontend().CommentsAndReviews().ReviewsWrapper().AssertMessageAndCountOnPage(ReviewMessage);

            this.PublishCommentAndVerifyFrontEndAndBackend();
        }

        public void VerifyCommentBackend(string commentsStatus)
        {
            ManageCommentsWrapper manageComments = new ManageCommentsWrapper(ActiveBrowser);
            manageComments.VerifyCommentBackend(commentsStatus, this.reviewsToPage[0], this.reviewAuthor[0], PageName);
        }

        public void PublishCommentAndVerifyFrontEndAndBackend()
        {
            BAT.Macros().NavigateTo().Modules().Comments();
            BAT.Wrappers().Backend().Comments().ManageCommentsWrapper(ActiveBrowser).ClickPublishCommentLink(this.reviewsToPage[0]);
            this.VerifyCommentBackend(ReviewsStatusPublished);
            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower(), false);
            BATFeather.Wrappers().Frontend().CommentsAndReviews().CommentsWrapper().AssertMessageAndCountOnPage(ReviewsCount);
            BATFeather.Wrappers().Frontend().CommentsAndReviews().ReviewsWrapper().VerifyReviewsAuthorRaitingAndContent(this.reviewAuthor, this.reviewsToPage, this.reviewRaiting);
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
        private string[] reviewsToPage = { "Reviews to page" };
        private string[] reviewAuthor = { "New user" };
        private string[] reviewRaiting = { "(3)" };
        private const int Raiting = 3;
        private const string ReviewsStatusPublished = "Published";
        private const string ReviewsStatusSpam = "Spam";
        private const string ReviewsStatusHidden = "Hidden";
        private const string ReviewsCount = "1 review";
        private const string ReviewMessage = "Write a review";
        private const string AllertMessage = "Thank you! Your review has been submitted successfully";
        private const string ReviewsStatusWaiting = "Waiting for approval";
        private const string AllertMessageWaiting = "Thank you for the review! Your review must be approved first";
    }
}
