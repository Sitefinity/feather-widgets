using System;
using System.Linq;
using Feather.Widgets.TestUI.Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FeatherWidgets.TestUI.TestCases.CommentsAndReviews
{
    /// <summary>
    /// DisableAndEnableEmailNotificationsForPages test class.
    /// </summary>
    [TestClass]
    public class DisableAndEnableEmailNotificationsForPages_ : FeatherTestCase
    {
        /// <summary>
        /// UI test DisableAndEnableEmailNotificationsForPages
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.SitefinityTeam2),
        TestCategory(FeatherTestCategories.PagesAndContent),
        TestCategory(FeatherTestCategories.CommentsAndReviews),
        TestCategory(FeatherTestCategories.Bootstrap)]
        public void DisableAndEnableEmailNotificationsForPages()
        {
            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower(), false, this.Culture);
            BATFeather.Wrappers().Frontend().CommentsAndReviews().CommentsWrapper().VerifyCommentsAuthorAndContent(this.commentAuthor, this.commentToPage);
            BATFeather.Wrappers().Frontend().CommentsAndReviews().CommentsWrapper().AssertMessageAndCountOnPage(CommentsCount);
            BATFeather.Wrappers().Frontend().CommentsAndReviews().CommentsWrapper().VerifySubscribeLinksIsNotVisible(SubscribeToNewComments);

            BAT.Arrange(this.TestName).ExecuteArrangement("EnableEmailNotifications");
            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower(), false, this.Culture);
            BATFeather.Wrappers().Frontend().CommentsAndReviews().CommentsWrapper().VerifySubscribeToNewCommentLinksIsPresent();

            BAT.Macros().User().LogOut();
            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower(), false, this.Culture);
            BATFeather.Wrappers().Frontend().CommentsAndReviews().CommentsWrapper().VerifyCommentsAuthorAndContent(this.commentAuthor, this.commentToPage);
            BATFeather.Wrappers().Frontend().CommentsAndReviews().CommentsWrapper().AssertMessageAndCountOnPage(CommentsCount);
            BATFeather.Wrappers().Frontend().CommentsAndReviews().CommentsWrapper().VerifySubscribeLinksIsNotVisible(SubscribeToNewComments);

            BAT.Macros().User().EnsureAdminLoggedIn();
            BAT.Arrange(this.TestName).ExecuteArrangement("DisableEmailNotifications");
            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower(), false, this.Culture);
            BATFeather.Wrappers().Frontend().CommentsAndReviews().CommentsWrapper().VerifyCommentsAuthorAndContent(this.commentAuthor, this.commentToPage);
            BATFeather.Wrappers().Frontend().CommentsAndReviews().CommentsWrapper().AssertMessageAndCountOnPage(CommentsCount);
            BATFeather.Wrappers().Frontend().CommentsAndReviews().CommentsWrapper().VerifySubscribeLinksIsNotVisible(SubscribeToNewComments);
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

        private const string PageName = "CommentsPage";
        private string[] commentToPage = { "Comment to page published comment" };
        private string[] commentAuthor = { FeatherTestCase.AdminNickname };
        private const string CommentsCount = "1comment";
        private const string SubscribeToNewComments = "Subscribe to new comments";
    }
}
