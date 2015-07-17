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
    /// SubscribeUserForCommentsNotificationAndVerifyReceivedEmail test class.
    /// </summary>
    [TestClass]
    public class SubscribeUserForCommentsNotificationAndVerifyReceivedEmail_ : FeatherTestCase
    {
        /// <summary>
        /// UI test SubscribeUserForCommentsNotificationAndVerifyReceivedEmail
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.FeatherTeam),
        TestCategory(FeatherTestCategories.PagesAndContent),
        TestCategory(FeatherTestCategories.CommentsAndReviews),
        TestCategory(FeatherTestCategories.Bootstrap)]
        public void SubscribeUserForCommentsNotificationAndVerifyReceivedEmail()
        {
            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower(), false);
            BATFeather.Wrappers().Frontend().CommentsAndReviews().CommentsWrapper().VerifyCommentsAuthorAndContent(this.commentAuthor, this.commentToPage);
            BATFeather.Wrappers().Frontend().CommentsAndReviews().CommentsWrapper().AssertMessageAndCountOnPage(CommentsCount);
            BATFeather.Wrappers().Frontend().CommentsAndReviews().CommentsWrapper().VerifySubscribeToNewCommentLinksIsPresent();
            BATFeather.Wrappers().Frontend().CommentsAndReviews().CommentsWrapper().ClickSubscribeToNewCommentLinks();
            BATFeather.Wrappers().Frontend().CommentsAndReviews().CommentsWrapper().VerifySuccessfullySubscribedMessageIsPresent();
            BATFeather.Wrappers().Frontend().CommentsAndReviews().CommentsWrapper().VerifyUnsubscribeLinksIsPresent();

            BAT.Macros().User().LogOut();
            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower(), false);
            BATFeather.Wrappers().Frontend().CommentsAndReviews().CommentsWrapper().TypeAMessage(NewCommentToPage);
            BATFeather.Wrappers().Frontend().CommentsAndReviews().CommentsWrapper().TypeYourName(NewUser);
            BATFeather.Wrappers().Frontend().CommentsAndReviews().CommentsWrapper().ClickSubmitButton();
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

        private const string PageName = "CommentsPage";
        private string[] commentToPage = { "Comment to page published comment" };
        private const string NewCommentToPage = "This is test comment";
        private const string NewUser = "New user";
        private string[] commentAuthor = { "admin" };
        private const string CommentsCount = "1comment";
    }
}
