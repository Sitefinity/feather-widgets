using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArtOfTest.WebAii.Core;
using ArtOfTest.WebAii.Controls.HtmlControls;
using Feather.Widgets.TestUI.Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telerik.Sitefinity.Frontend.TestUtilities;
using Telerik.Sitefinity.TestUI.Framework.Wrappers.Backend;

namespace FeatherWidgets.TestUI.TestCases.CommentsAndReviews
{
    /// <summary>
    /// DeleteNewsItemWithSubmittedComments test class.
    /// </summary>
    [TestClass]
    public class DeleteNewsItemWithSubmittedComments_ : FeatherTestCase
    {
        /// <summary>
        /// UI test DeleteNewsItemWithSubmittedComments
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.Team2),
        TestCategory(FeatherTestCategories.CommentsAndReviews),
        TestCategory(FeatherTestCategories.Bootstrap)]
        public void DeleteNewsItemWithSubmittedComments()
        {
            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower(), false);
            BATFeather.Wrappers().Frontend().CommentsAndReviews().CommentsWrapper().AssertCommentsCount(CommentsCount);
            BATFeather.Wrappers().Frontend().CommentsAndReviews().CommentsWrapper().ClickCommentLink();
            BATFeather.Wrappers().Frontend().CommentsAndReviews().CommentsWrapper().VerifyCommentsAuthorAndContent(this.commentAuthor, this.commentToNewsOldest);
            this.VerifyCommentBackend();            
            this.DeleteNewsItemBackend();
            BAT.Macros().NavigateTo().Modules().Comments();
            BAT.Wrappers().Backend().Comments().ManageCommentsWrapper(ActiveBrowser).VerifyNoCommentsExist();
            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower(), false);
            Assert.IsFalse(BAT.Wrappers().Frontend().Pages().PagesWrapperFrontend().GetPageContent().InnerText.Contains(NewsTitle), "News is presented");
        }

        public void VerifyCommentBackend()
        {
            BAT.Macros().NavigateTo().Modules().Comments();
            ActiveBrowser.WaitForAsyncJQueryRequests();
            ManageCommentsWrapper manageComments = new ManageCommentsWrapper(ActiveBrowser);
            manageComments.VerifyCommentBackend(CommentStatus, this.commentToNewsOldest[0], this.commentAuthor[0], NewsTitle);
            manageComments.VerifyCommentBackend(CommentStatus, this.commentToNewsOldest[1], this.commentAuthor[1], NewsTitle);
        }

        public void DeleteNewsItemBackend()
        {
            BAT.Macros().NavigateTo().Modules().News();
            BAT.Macros().GridAutomation().Row().ByTitle(NewsTitle).Select();
            var deleteButton = ActiveBrowser.WaitForElementEndsWithID("_delete").As<HtmlAnchor>();
            deleteButton.Click();
            var confirmDeleteButtons = ActiveBrowser.Find.AllByAttributes("class=sfLinkBtn sfDelete");
            var deleteSingleItemButton = confirmDeleteButtons.Where(c => c.InnerText.StartsWith(BAT.Labels.MoveToRecycleBinLabel)).FirstOrDefault().As<HtmlAnchor>();
            if (deleteSingleItemButton != null)
            {
                deleteSingleItemButton.Click();
            }
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

        private const string PageName = "NewsPage";
        private const string NewsTitle = "NewsTitle";
        private const string LeaveAComment = "Leave a comment";
        private string[] commentToNewsOldest = { "Comment1", "Comment2" };
        private string[] commentAuthor = { "admin", "admin" };
        private const string CommentStatus = "Published";
        private const string CommentsCount = "2 comments";
    }
}
