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
    /// This is the entry point class for comments and reviews common wrapper.
    /// </summary>
    public abstract class CommentsAndReviewsCommonWrapper : BaseWrapper
    {
        /// <summary>
        /// Asserts message and count on page
        /// </summary>
        public void AssertMessageAndCountOnPage(string commentCount)
        {
            HtmlDiv commentLinkOnPage = this.EM.CommentsAndReviews.CommentsFrontend.MessageAndCountOnPage.AssertIsPresent("Comments count on page");
            bool isPresent = commentLinkOnPage.InnerText.Contains(commentCount);
            Assert.IsTrue(isPresent);
        }

        /// <summary>
        /// Type a message.
        /// </summary>
        /// <param name="message">Message.</param>
        public void TypeAMessage(string message)
        {
            ActiveBrowser.RefreshDomTree();

            HtmlDiv editable = this.EM.CommentsAndReviews.CommentsFrontend.LeaveACommentArea
                .AssertIsPresent("Leave area");

            editable.ScrollToVisible();
            editable.Focus();
            editable.MouseClick();

            Manager.Current.Desktop.KeyBoard.KeyDown(System.Windows.Forms.Keys.Control);
            Manager.Current.Desktop.KeyBoard.KeyPress(System.Windows.Forms.Keys.A);
            Manager.Current.Desktop.KeyBoard.KeyUp(System.Windows.Forms.Keys.Control);
            Manager.Current.Desktop.KeyBoard.KeyPress(System.Windows.Forms.Keys.Delete);
            Manager.Current.Desktop.KeyBoard.TypeText(message);
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
            ActiveBrowser.RefreshDomTree();
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
        /// Asserts comments and reviews count
        /// </summary>
        public void AssertExpectedCount(string expectedCount)
        {
            HtmlAnchor commentLink = this.EM.CommentsAndReviews.CommentsFrontend.LeaveAComment.AssertIsPresent("Comments count link");
            bool isPresent = commentLink.InnerText.Contains(expectedCount);
            Assert.IsTrue(isPresent);
        }

        /// <summary>
        /// Click count link
        /// </summary>
        public void ClickCountLink()
        {
            HtmlAnchor commentLink = this.EM.CommentsAndReviews.CommentsFrontend.LeaveAComment
                .AssertIsPresent("Comments count link");

            commentLink.Wait.ForVisible();
            commentLink.ScrollToVisible();
            commentLink.MouseClick();
            ActiveBrowser.WaitUntilReady();
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
    }
}
