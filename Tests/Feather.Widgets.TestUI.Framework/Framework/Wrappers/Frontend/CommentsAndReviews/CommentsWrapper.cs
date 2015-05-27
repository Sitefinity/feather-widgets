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
    public class CommentsWrapper : CommentsAndReviewsCommonWrapper
    {        
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
            this.EM.CommentsAndReviews.CommentsFrontend.LoadMoreLink.AssertIsNotVisible("Load more link");
        }

        /// <summary>
        /// Verify subscribe for new comment is present
        /// </summary>
        public void VerifySubscribeToNewCommentLinksIsPresent()
        {
            this.EM.CommentsAndReviews.CommentsFrontend.SubscribeToNewComments.AssertIsPresent("Subscribe to new comment");
        }

        /// <summary>
        /// Click subscribe for new comment link
        /// </summary>
        public void ClickSubscribeToNewCommentLinks()
        {
            HtmlSpan subscribeForEmail = this.EM.CommentsAndReviews.CommentsFrontend.SubscribeToNewComments.AssertIsPresent("Subscribe to new comment");

            subscribeForEmail.MouseClick();
            ActiveBrowser.WaitUntilReady();
        }

        /// <summary>
        /// Verify successfully subscribe to new comment is present
        /// </summary>
        public void VerifySuccessfullySubscribedMessageIsPresent()
        {
            this.EM.CommentsAndReviews.CommentsFrontend.SuccessfulySubscribedMessage.AssertIsPresent("Successfully subscribe to new comment");
        }
    }
}
