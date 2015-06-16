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
    /// EditReviewsWidgetAndCloseReviewsForPage test class.
    /// </summary>
    [TestClass]
    public class EditReviewsWidgetAndCloseReviewsForPage_ : FeatherTestCase
    {
        /// <summary>
        /// UI test EditReviewsWidgetAndCloseReviewsForPage
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.Team2),
        TestCategory(FeatherTestCategories.PagesAndContent),
        TestCategory(FeatherTestCategories.CommentsAndReviews),
        TestCategory(FeatherTestCategories.Bootstrap)]
        public void EditReviewsWidgetAndCloseReviewsForPage()
        {
            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower(), false);
            BATFeather.Wrappers().Frontend().CommentsAndReviews().ReviewsWrapper().VerifyReviewsAuthorRaitingAndContent(this.reviewAuthor, this.reviewsToPage, this.reviewRaiting);
            BATFeather.Wrappers().Frontend().CommentsAndReviews().ReviewsWrapper().VerifyAlertMessageOnTheFrontend(AllertMessageSubmitted);
                      
            BAT.Macros().NavigateTo().Pages();
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(PageName);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(WidgetName);
            BATFeather.Wrappers().Backend().ModuleBuilder().DynamicWidgetAdvancedSettingsWrapper().ClickAdvancedSettingsButton();
            BATFeather.Wrappers().Backend().ModuleBuilder().DynamicWidgetAdvancedSettingsWrapper().ClickModelButton();
            BATFeather.Wrappers().Backend().CommentsAndReviews().ReviewsWrapper().CloseCommentsAndReviewForThread(CloseReviews);
            BATFeather.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().SaveChanges();
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();

            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower(), false);
            BATFeather.Wrappers().Frontend().CommentsAndReviews().ReviewsWrapper().VerifyAlertMessageOnTheFrontend(AllertMessage);
            BATFeather.Wrappers().Frontend().CommentsAndReviews().ReviewsWrapper().VerifyAverageRaiting(this.reviewRaiting[0]);
            BATFeather.Wrappers().Frontend().CommentsAndReviews().ReviewsWrapper().AssertMessageAndCountOnPage(ReviewsCount);
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
        private const string WidgetName = "Reviews";
        private const string CloseReviews = "True";
        private string[] reviewsToPage = { "Reviews to page" };
        private string[] reviewAuthor = { "admin" };
        private string[] reviewRaiting = { "(2)" };
        private const string ReviewsCount = "1 review";
        private const string AllertMessage = "Reviews are not allowed anymore";
        private const string AllertMessageSubmitted = "You've already submitted a review for this item";
    }
}
