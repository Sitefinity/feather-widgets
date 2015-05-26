using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArtOfTest.Common.UnitTesting;
using ArtOfTest.WebAii.Controls.HtmlControls;
using ArtOfTest.WebAii.Core;

namespace Feather.Widgets.TestUI.Framework.Framework.Wrappers.Frontend.CommentsAndReviews
{
    /// <summary>
    /// This is the entry point class for comments on the frontend.
    /// </summary>
    public class CommentsWrapper : BaseWrapper
    {
        /// <summary>
        /// Asserts that Leave a comment message is present.
        /// </summary>
        public void AssertLeaveACommentMessage()
        {
            this.EM.CommentsAndReviews.CommentsFrontend.CommentsHeader.AssertIsPresent("Leave a comment message");
        }

        /// <summary>
        /// Asserts comments count
        /// </summary>
        public void AssertCommentsCount(string commentCount)
        {
            HtmlAnchor commentLink = this.EM.CommentsAndReviews.CommentsFrontend.LeaveAComment.AssertIsPresent("Comments count link");
            bool isPresent = commentLink.InnerText.Contains(commentCount);
            Assert.IsTrue(isPresent);
        }

        /// <summary>
        /// Asserts comments count on page
        /// </summary>
        public void AssertCommentsCountOnPage(string commentCount)
        {
            HtmlDiv commentLinkOnPage = this.EM.CommentsAndReviews.CommentsFrontend.CommentsCountOnPage.AssertIsPresent("Comments count on page");
            bool isPresent = commentLinkOnPage.InnerText.Contains(commentCount);
            Assert.IsTrue(isPresent);
        }

        /// <summary>
        /// Click comment link
        /// </summary>
        public void ClickCommentLink()
        {
            HtmlAnchor commentLink = this.EM.CommentsAndReviews.CommentsFrontend.LeaveAComment
                .AssertIsPresent("Comments count link");

            commentLink.Wait.ForVisible();
            commentLink.ScrollToVisible();
            commentLink.MouseClick();
            ActiveBrowser.WaitUntilReady();
        }

        /// <summary>
        /// Type a comment.
        /// </summary>
        /// <param name="commentBody">Comment body.</param>
        public void TypeAComment(string commentBody)
        {
            ActiveBrowser.RefreshDomTree();

            HtmlDiv editable = this.EM.CommentsAndReviews.CommentsFrontend.LeaveACommentArea
                .AssertIsPresent("Leave acomment area");

            editable.ScrollToVisible();
            editable.Focus();
            editable.MouseClick();

            Manager.Current.Desktop.KeyBoard.KeyDown(System.Windows.Forms.Keys.Control);
            Manager.Current.Desktop.KeyBoard.KeyPress(System.Windows.Forms.Keys.A);
            Manager.Current.Desktop.KeyBoard.KeyUp(System.Windows.Forms.Keys.Control);
            Manager.Current.Desktop.KeyBoard.KeyPress(System.Windows.Forms.Keys.Delete);

            Manager.Current.Desktop.KeyBoard.TypeText(commentBody);
        }

        /// <summary>
        /// Type your name.
        /// </summary>
        /// <param name="yourName">Your name.</param>
        public void TypeYourName(string yourName)
        {
            HtmlDiv yourNameField = this.EM.CommentsAndReviews.CommentsFrontend.YourNameField
                .AssertIsPresent("Your name");

            yourNameField.ScrollToVisible();
            yourNameField.Focus();
            yourNameField.MouseClick();

            Manager.Current.Desktop.KeyBoard.KeyDown(System.Windows.Forms.Keys.Control);
            Manager.Current.Desktop.KeyBoard.KeyPress(System.Windows.Forms.Keys.A);
            Manager.Current.Desktop.KeyBoard.KeyUp(System.Windows.Forms.Keys.Control);
            Manager.Current.Desktop.KeyBoard.KeyPress(System.Windows.Forms.Keys.Delete);

            Manager.Current.Desktop.KeyBoard.TypeText(yourName);
        }

        /// <summary>
        /// Type email.
        /// </summary>
        /// <param name="email">email.</param>
        public void TypeEmailAddress(string email)
        {
            HtmlDiv emailAddress = this.EM.CommentsAndReviews.CommentsFrontend.EmailField
                .AssertIsPresent("Email");

            emailAddress.ScrollToVisible();
            emailAddress.Focus();
            emailAddress.MouseClick();

            Manager.Current.Desktop.KeyBoard.KeyDown(System.Windows.Forms.Keys.Control);
            Manager.Current.Desktop.KeyBoard.KeyPress(System.Windows.Forms.Keys.A);
            Manager.Current.Desktop.KeyBoard.KeyUp(System.Windows.Forms.Keys.Control);
            Manager.Current.Desktop.KeyBoard.KeyPress(System.Windows.Forms.Keys.Delete);

            Manager.Current.Desktop.KeyBoard.TypeText(email);
        }

        /// <summary>
        /// Click submit button
        /// </summary>
        public void ClickSubmitButton()
        {
            HtmlButton submitButton = this.EM.CommentsAndReviews.CommentsFrontend.SubmitButton
            .AssertIsPresent("Submit button");
            submitButton.Click();
            ActiveBrowser.WaitUntilReady();
        }

        /// <summary>
        /// Verify comments author and content
        /// </summary>
        /// <param name="commentsAuthor">Comments author</param>
        /// <param name="commentsContent">Comments content</param>
        public void VerifyCommentsAuthorAndContent(string[] commentsAuthor, string[] commentsContent)
        {
            IList<HtmlDiv> allCommentsDivs = this.EM.CommentsAndReviews.CommentsFrontend.ResultsCommentsList;

            Assert.IsNotNull(allCommentsDivs, "Comments list is null");
            Assert.AreNotEqual(0, allCommentsDivs.Count, "Comments list has no elements");

            Assert.AreEqual(commentsContent.Count(), allCommentsDivs.Count, "Expected and actual count of comments are not equal");

            for (int i = 0; i < allCommentsDivs.Count(); i++)
            {
                Assert.AreEqual(commentsAuthor[i], allCommentsDivs[i].ChildNodes[0].InnerText);
                Assert.AreEqual(commentsContent[i], allCommentsDivs[i].ChildNodes[2].InnerText);
            }
        }

        /// <summary>
        /// Verify allert message
        /// </summary>
        /// <param name="alertMessage">Expected allert message</param>
        public void VerifyAlertMessageOnTheFrontend(string alertMessage)
        {
            HtmlDiv alertMessageOnPage = this.EM.CommentsAndReviews.CommentsFrontend.AlertWarningDiv
                .AssertIsPresent("Alert message");
            bool isPresent = alertMessageOnPage.InnerText.Contains(alertMessage);
            Assert.IsTrue(isPresent);
        }

        /// <summary>
        /// Verify error message
        /// </summary>
        /// <param name="errorMessage">Expected error message</param>
        public void VerifyErrorMessageOnTheFrontend(string errorMessage)
        {
            HtmlDiv alertMessageOnPage = this.EM.CommentsAndReviews.CommentsFrontend.ErrorDiv
                .AssertIsPresent("Error message");
            bool isPresent = alertMessageOnPage.InnerText.Contains(errorMessage);
            Assert.IsTrue(isPresent);
        }

        /// <summary>
        /// Click load more link
        /// </summary>
        public void ClickLoadMoreLink()
        {
            HtmlAnchor loadMoreLink = this.EM.CommentsAndReviews.CommentsFrontend.LoadMoreLink
                .AssertIsPresent("Load more link");
            loadMoreLink.MouseClick();
            ActiveBrowser.WaitUntilReady();
        }

        /// <summary>
        /// Verify load more link is not visible
        /// </summary>
        public void VerifyLoadMoreLinkIsNotVisible()
        {
            HtmlAnchor loadMoreLink = this.EM.CommentsAndReviews.CommentsFrontend.LoadMoreLink;
            loadMoreLink.AssertIsNotVisible("Load more link");
        }

        /// <summary>
        /// Verify show oldest and newest on top are not visible
        /// </summary>
        public void VerifyShowOldestAndNewstOnTopLinksAreNotVisible()
        {
            HtmlAnchor newestOnTop = this.EM.CommentsAndReviews.CommentsFrontend.ShowNewestOnTop;
            newestOnTop.AssertIsNotVisible("Show newest on top link");

            HtmlAnchor oldestOnTop = this.EM.CommentsAndReviews.CommentsFrontend.ShowOldestOnTop;
            oldestOnTop.AssertIsNotVisible("Show oldest on top link");
        }

        /// <summary>
        /// Click oldest on top link
        /// </summary>
        public void ClickOldestOnTopLink()
        {
            HtmlAnchor oldestOnTopLink = this.EM.CommentsAndReviews.CommentsFrontend.ShowOldestOnTop
                .AssertIsPresent("Oldest on top link");
            oldestOnTopLink.MouseClick();
            ActiveBrowser.WaitUntilReady();
        }

        /// <summary>
        /// Click newest on top link
        /// </summary>
        public void ClickNewestOnTopLink()
        {
            HtmlAnchor newestOnTopLink = this.EM.CommentsAndReviews.CommentsFrontend.ShowNewestOnTop
                .AssertIsPresent("Newest on top link");
            newestOnTopLink.MouseClick();
            ActiveBrowser.WaitUntilReady();
        }
    }
}
