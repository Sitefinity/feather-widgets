﻿using System;
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
    /// SubmitReviewForPageLoggedUserOnBootstrapPage test class.
    /// </summary>
    [TestClass]
    public class SubmitReviewForPageLoggedUserOnBootstrapPage_ : FeatherTestCase
    {
        /// <summary>
        /// UI test SubmitReviewForPageLoggedUserOnBootstrapPage
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.FeatherTeam),
        TestCategory(FeatherTestCategories.PagesAndContent),
        TestCategory(FeatherTestCategories.CommentsAndReviews),
        TestCategory(FeatherTestCategories.Bootstrap)]
        public void SubmitReviewForPageLoggedUserOnBootstrapPage()
        {
            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower(), false, this.Culture);
            BATFeather.Wrappers().Frontend().CommentsAndReviews().ReviewsWrapper().AssertMessageAndCountOnPage(ReviewMessage);
            BATFeather.Wrappers().Frontend().CommentsAndReviews().ReviewsWrapper().TypeAMessage(this.reviewsToPage[0]);
            BATFeather.Wrappers().Frontend().CommentsAndReviews().ReviewsWrapper().ClickRaitingStar(Raiting);
            BATFeather.Wrappers().Frontend().CommentsAndReviews().ReviewsWrapper().ClickSubmitButton();
            BATFeather.Wrappers().Frontend().CommentsAndReviews().ReviewsWrapper().VerifyAlertMessageOnTheFrontend(AllertMessage);
            BATFeather.Wrappers().Frontend().CommentsAndReviews().ReviewsWrapper().VerifyReviewsAuthorRaitingAndContent(this.reviewAuthor, this.reviewsToPage, this.reviewRaiting);
            BATFeather.Wrappers().Frontend().CommentsAndReviews().ReviewsWrapper().VerifyAverageRaiting(this.reviewRaiting[0]);
            BATFeather.Wrappers().Frontend().CommentsAndReviews().ReviewsWrapper().AssertMessageAndCountOnPage(ReviewsCount);
            this.VerifyCommentBackend();
        }

        public void VerifyCommentBackend()
        {
            BAT.Macros().NavigateTo().Modules().Comments(this.Culture);
            ActiveBrowser.WaitForAsyncJQueryRequests();
            ManageCommentsWrapper manageComments = new ManageCommentsWrapper(ActiveBrowser);
            manageComments.VerifyCommentBackend(ReviewsStatus, this.reviewsToPage[0], this.reviewAuthor[0], PageName);
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
        private string[] reviewsToPage = { "Reviews to page" };
        private string[] reviewAuthor = { "admin" };
        private string[] reviewRaiting = { "(3)" };
        private const int Raiting = 3;
        private const string ReviewsStatus = "Published";
        private const string ReviewsCount = "1 review";
        private const string ReviewMessage = "Write a review";
        private const string AllertMessage = "Thank you! Your review has been submitted successfully";
    }
}
