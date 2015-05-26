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
    }
}
