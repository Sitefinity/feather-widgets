using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArtOfTest.WebAii.Core;
using Feather.Widgets.TestUI.Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telerik.Sitefinity.Frontend.TestUtilities;
using Telerik.Sitefinity.TestUI.Framework.Utilities;
using Telerik.Sitefinity.TestUI.Framework.Wrappers.Backend;

namespace FeatherWidgets.TestUI.TestCases.CommentsAndReviews
{
    /// <summary>
    /// SubmitCommentToNewsItemThatRequireAuthentication test class.
    /// </summary>
    [TestClass]
    public class SubmitCommentToNewsItemThatRequireAuthentication_ : FeatherTestCase
    {
        /// <summary>
        /// UI test SubmitCommentToNewsItemThatRequireAuthentication
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.Team2),
        TestCategory(FeatherTestCategories.PagesAndContent),
        TestCategory(FeatherTestCategories.CommentsAndReviews),
        TestCategory(FeatherTestCategories.Bootstrap)]
        public void SubmitCommentToNewsItemThatRequireAuthentication()
        {
            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower(), false);
            BATFeather.Wrappers().Frontend().CommentsAndReviews().CommentsWrapper().AssertExpectedCount(LeaveAComment);
            BATFeather.Wrappers().Frontend().CommentsAndReviews().CommentsWrapper().ClickCountLink();            
            BATFeather.Wrappers().Frontend().CommentsAndReviews().CommentsWrapper().VerifyAlertMessageOnTheFrontend(AllertMessage);
            BATFeather.Wrappers().Frontend().CommentsAndReviews().CommentsWrapper().VerifyReviewsTextAreaIsNotVisible();
            BATFeather.Wrappers().Frontend().CommentsAndReviews().CommentsWrapper().ClickLoginLink();
            this.AdminLogin();
            BATFeather.Wrappers().Frontend().CommentsAndReviews().CommentsWrapper().AssertMessageAndCountOnPage(CommentsMessage);
            BATFeather.Wrappers().Frontend().CommentsAndReviews().CommentsWrapper().TypeAMessage(this.commentToNews[0]);
            BATFeather.Wrappers().Frontend().CommentsAndReviews().CommentsWrapper().ClickSubmitButton();
            BATFeather.Wrappers().Frontend().CommentsAndReviews().CommentsWrapper().VerifyCommentsAuthorAndContent(this.commentAuthor, this.commentToNews);
            BATFeather.Wrappers().Frontend().CommentsAndReviews().CommentsWrapper().AssertExpectedCount(CommentsCount);
            this.VerifyCommentBackend();
        }

        public void VerifyCommentBackend()
        {
            BAT.Macros().NavigateTo().Modules().Comments();
            ActiveBrowser.WaitForAsyncJQueryRequests();
            ManageCommentsWrapper manageComments = new ManageCommentsWrapper(ActiveBrowser);
            manageComments.VerifyCommentBackend(CommentStatus, this.commentToNews[0], this.commentAuthor[0], NewsTitle);
        }

        public void AdminLogin()
        {
            RuntimeSettingsModificator.ExecuteWithClientTimeout(200000, () => BAT.Wrappers().Backend().Comments().ManageCommentsWrapper(ActiveBrowser).LogInUser(AdminUserName, AdminPassword));
            ActiveBrowser.WaitUntilReady();
            ActiveBrowser.WaitForAsyncJQueryRequests();
            BAT.Macros().User().EnsureAdminLoggedIn();
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
        private const string LeaveAComment = "Leave a comment";
        private string[] commentToNews = { "Comment to news" };
        private string[] commentAuthor = { "admin" };
        private const string CommentStatus = "Published";
        private const string CommentsCount = "1 comment";
        private const string CommentsMessage = "Leave a comment";
        private const string AllertMessage = "Loginto be able to comment";
        private const string AdminUserName = "admin";
        private const string AdminPassword = "admin@2"; 
    }
}
