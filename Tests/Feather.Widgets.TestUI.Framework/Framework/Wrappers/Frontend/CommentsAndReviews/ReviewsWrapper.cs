using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArtOfTest.Common.UnitTesting;
using ArtOfTest.WebAii.Controls.HtmlControls;

namespace Feather.Widgets.TestUI.Framework.Framework.Wrappers.Frontend.CommentsAndReviews
{
    /// <summary>
    /// This is the entry point class for reviews on the frontend.
    /// </summary>
    public class ReviewsWrapper : CommentsAndReviewsCommonWrapper
    {
        /// <summary>
        /// Click star in raitngs
        /// </summary>
        /// <param name="star">Which star to click</param>
        public void ClickRaitingStar(int star)
        {
            HtmlDiv raitngStardiv = this.EM.CommentsAndReviews.ReviewsFrontend.RaitingStars
                .AssertIsPresent("Raitings");

            var starToClick = raitngStardiv.ChildNodes[star];
            starToClick.Focus();
            raitngStardiv.MouseClick();
        }

        /// <summary>
        /// Verify reviews author, raiting and content
        /// </summary>
        /// <param name="reviewsAuthor">Reviews author</param>
        /// <param name="reviewsContent">Reviews content</param>
        /// <param name="raiting">Reviews raiting</param>
        public void VerifyReviewsAuthorRaitingAndContent(string[] reviewsAuthor, string[] reviewsContent, string[] raiting)
        {
            IList<HtmlDiv> allReviewsDivs = this.EM.CommentsAndReviews.ReviewsFrontend.ReviewsDivs.ToList();

            Assert.IsNotNull(allReviewsDivs, "Reviews list is null");
            Assert.AreNotEqual(0, allReviewsDivs.Count, "Reviews list has no elements");
            int actualReviewCount = allReviewsDivs.Count - 2;
            Assert.AreEqual(reviewsContent.Count(), actualReviewCount, "Expected and actual count of reviews are not equal");

            for (int i = 0; i < actualReviewCount; i++)
            {
                Assert.AreEqual(reviewsAuthor[i], allReviewsDivs[i].ChildNodes[0].InnerText);
                HtmlSpan ratingSpan = allReviewsDivs[i].Find.ByExpression<HtmlSpan>("tagname=span", "class=text-muted sf-Ratings-average");
                Assert.AreEqual(raiting[i], ratingSpan.InnerText);
                Assert.AreEqual(reviewsContent[i], allReviewsDivs[i].ChildNodes[4].InnerText);
            }
        }

        /// <summary>
        /// Verify average raiting
        /// </summary>
        /// <param name="expectedRaiting">Expected raiting</param>
        public void VerifyAverageRaiting(string expectedRaiting)
        {
            HtmlDiv averageDiv = this.EM.CommentsAndReviews.CommentsFrontend.MessageAndCountOnPage.AssertIsPresent("Comments count on page");
            HtmlSpan actualRaiting = averageDiv.Find.ByExpression<HtmlSpan>("tagname=span", "class=text-muted sf-Ratings-average");
            Assert.AreEqual(expectedRaiting, actualRaiting.InnerText, "Expected raitng and actual raiting");
        }

        /// <summary>
        /// Verify allert message
        /// </summary>
        /// <param name="alertMessage">Expected allert message</param>
        public void VerifyAlertMessageOnTheFrontendNotLoggedUser(string alertMessage)
        {
            HtmlDiv alertMessageOnPage = this.EM.CommentsAndReviews.ReviewsFrontend.AlertWarningDiv
                .AssertIsPresent("Alert message");
            bool isPresent = alertMessageOnPage.InnerText.Contains(alertMessage);
            Assert.IsTrue(isPresent);
        }
    }
}
