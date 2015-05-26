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
    /// ValidateAllFieldsInCommentsWidgetOnFrontend test class.
    /// </summary>
    [TestClass]
    public class ValidateAllFieldsInCommentsWidgetOnFrontend_ : FeatherTestCase
    {
        /// <summary>
        /// UI test ValidateAllFieldsInCommentsWidgetOnFrontend_
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.Team2),
        TestCategory(FeatherTestCategories.CommentsAndReviews),
        TestCategory(FeatherTestCategories.Bootstrap)]
        public void ValidateAllFieldsInCommentsWidgetOnFrontend()
        {
            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower(), false);
            BATFeather.Wrappers().Frontend().CommentsAndReviews().CommentsWrapper().AssertMessageAndCountOnPage(CommentsMessage);
            BATFeather.Wrappers().Frontend().CommentsAndReviews().CommentsWrapper().ClickSubmitButton();
            BATFeather.Wrappers().Frontend().CommentsAndReviews().CommentsWrapper().VerifyErrorMessageOnTheFrontend(MessageIsRequired);
            
            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower(), false);
            BATFeather.Wrappers().Frontend().CommentsAndReviews().CommentsWrapper().TypeAComment(this.commentToPage[0]);
            BATFeather.Wrappers().Frontend().CommentsAndReviews().CommentsWrapper().ClickSubmitButton();
            BATFeather.Wrappers().Frontend().CommentsAndReviews().CommentsWrapper().VerifyErrorMessageOnTheFrontend(AuthorIsRequired);

            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower(), false);
            BATFeather.Wrappers().Frontend().CommentsAndReviews().CommentsWrapper().TypeAComment(this.commentToPage[0]);
            BATFeather.Wrappers().Frontend().CommentsAndReviews().CommentsWrapper().TypeYourName(this.commentAuthor[0]);
            BATFeather.Wrappers().Frontend().CommentsAndReviews().CommentsWrapper().TypeEmailAddress(this.commentAuthorInvalidEmail[0]);
            BATFeather.Wrappers().Frontend().CommentsAndReviews().CommentsWrapper().ClickSubmitButton();
            BATFeather.Wrappers().Frontend().CommentsAndReviews().CommentsWrapper().VerifyErrorMessageOnTheFrontend(InvalidEmail);

            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower(), false);
            BATFeather.Wrappers().Frontend().CommentsAndReviews().CommentsWrapper().TypeAComment(this.commentToPage[0]);
            BATFeather.Wrappers().Frontend().CommentsAndReviews().CommentsWrapper().TypeYourName(this.commentAuthor[0]);
            BATFeather.Wrappers().Frontend().CommentsAndReviews().CommentsWrapper().TypeEmailAddress(this.commentAuthorEmail[0]);
            BATFeather.Wrappers().Frontend().CommentsAndReviews().CommentsWrapper().ClickSubmitButton();

            BATFeather.Wrappers().Frontend().CommentsAndReviews().CommentsWrapper().AssertMessageAndCountOnPage(CommentsCount);
            BATFeather.Wrappers().Frontend().CommentsAndReviews().CommentsWrapper().VerifyCommentsAuthorAndContent(this.commentAuthor, this.commentToPage);

            this.VerifyCommentBackend();
        }

        public void VerifyCommentBackend()
        {
            BAT.Macros().User().EnsureAdminLoggedIn();

            BAT.Macros().NavigateTo().Modules().Comments();
            ActiveBrowser.WaitForAsyncJQueryRequests();
            ManageCommentsWrapper manageComments = new ManageCommentsWrapper(ActiveBrowser);
            manageComments.VerifyCommentBackend(CommentStatus, this.commentToPage[0], this.commentAuthor[0], PageName);
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

        private const string PageName = "CommentsPage";
        private const string LeaveAComment = "Leave a comment";
        private string[] commentToPage = { "Comment to page" };
        private string[] commentAuthor = { "New user" };
        private string[] commentAuthorInvalidEmail = { "user" };
        private string[] commentAuthorEmail = { "user@test.com" };
        private const string CommentStatus = "Published";
        private const string CommentsCount = "1comment";
        private const string MessageIsRequired = "Message is required!";
        private const string AuthorIsRequired = "Author name is required!";
        private const string InvalidEmail = "Invalid email format!";
        private const string CommentsMessage = "Leave a comment";
    }
}
