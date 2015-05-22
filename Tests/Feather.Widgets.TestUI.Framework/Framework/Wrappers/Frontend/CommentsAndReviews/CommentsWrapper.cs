using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArtOfTest.WebAii.Controls.HtmlControls;
using ArtOfTest.WebAii.Core;
using ArtOfTest.Common.UnitTesting;

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
        /// Asserts comments count present in list template.
        /// </summary>
        public void AssertCommentsCountInListView(string commentCount)
        {
            HtmlAnchor commentLink = this.EM.CommentsAndReviews.CommentsFrontend.LeaveAComment.AssertIsPresent("Comments count link");
            bool isPresent = commentLink.InnerText.Contains(commentCount);
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
            HtmlDiv editable = this.EM.CommentsAndReviews.CommentsFrontend.
                                       LeaveACommentArea
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
        /// Click submit button
        /// </summary>
        public void ClickSubmitButton()
        {
            HtmlButton submitButton = this.EM.CommentsAndReviews.CommentsFrontend.SubmitButton
            .AssertIsPresent("Submit button");
            submitButton.Click();
            ActiveBrowser.WaitUntilReady();
            ActiveBrowser.WaitForAsyncRequests();
        }

        /// <summary>
        /// Verify comments author
        /// </summary>
        /// <param name="commentsAuthor">expected comments author</param>
        public void VerifyCommentsAuthor(string commentsAuthor)
        {
            HtmlDiv commentsList = this.EM.CommentsAndReviews.CommentsFrontend.ResultsCommentsDivList;

            List<HtmlSpan> commentsContainer = commentsList.Find.AllByTagName<HtmlSpan>("span").ToList<HtmlSpan>();
            commentsContainer[0].InnerText.Contains(commentsAuthor);
        }

        /// <summary>
        /// Verify submitted comment
        /// </summary>
        /// <param name="submittedComment">expected comments </param>
        public void VerifySubmittedComment(string submittedComment)
        {
            HtmlDiv commentsList = this.EM.CommentsAndReviews.CommentsFrontend.ResultsCommentsDivList;

            var commentsContainer = commentsList.Find.ByTagIndex("p", 0);
            commentsContainer.InnerText.Contains(submittedComment);
        }
    }
}
