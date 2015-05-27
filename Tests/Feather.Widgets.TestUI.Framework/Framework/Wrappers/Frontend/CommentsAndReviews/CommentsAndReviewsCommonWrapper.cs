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
    }
}
