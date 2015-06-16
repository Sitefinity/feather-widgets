using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FeatherWidgets.TestUtilities.CommonOperations;
using Telerik.Sitefinity.TestUI.Arrangements.Framework;
using Telerik.Sitefinity.TestUI.Arrangements.Framework.Attributes;
using Telerik.Sitefinity.TestUtilities.CommonOperations;

namespace FeatherWidgets.TestUI.Arrangements
{
     /// <summary>
    /// ChangeCommentsStatusesForPage arrangement class.
    /// </summary>
    public class ChangeReviewsStatusesForPage
    {
        /// <summary>
        /// Server side set up.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "FeatherWidgets.TestUtilities.CommonOperations.CommentsAndReviews.CreateReview(System.String,System.String,System.Guid,System.String,System.String,System.Nullable<System.Decimal>,System.String)")]
        public void SetUp()
        {
            ServerOperations.Comments().AllowComments(ThreadType, true);
            var domainKey = ServerOperations.Comments().GetCurrentSiteId.ToString();

            Guid templateId = Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.Templates().GetTemplateIdByTitle(PageTemplateName);
            Guid pageId = Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.Pages().CreatePage(PageName, templateId);
            pageId = ServerOperations.Pages().GetPageNodeId(pageId);
            ServerOperationsFeather.Pages().AddReviewsWidgetToPage(pageId, "Contentplaceholder1");

            ServerOperationsFeather.CommentsAndReviews().CreateReview(domainKey, ThreadType, pageId, PageName, ReviewsMessage, ReviewsRating, ReviewsStatusWaiting);
        }

        /// <summary>
        ///  Publish review
        /// </summary>
        [ServerArrangement]
        public void PublishReview()
        {
            Guid pageId = ServerOperations.Pages().GetPageId(PageName);
            string threadKey = pageId.ToString() + "_en_review";
            ServerOperationsFeather.CommentsAndReviews().ChangeCommentsAndReviewsStatus(ReviewsStatusPublish, threadKey);
        }

        /// <summary>
        ///  Mark as spam review
        /// </summary>
        [ServerArrangement]
        public void MarkAsSpamReview()
        {
            Guid pageId = ServerOperations.Pages().GetPageId(PageName);
            string threadKey = pageId.ToString() + "_en_review";
            ServerOperationsFeather.CommentsAndReviews().ChangeCommentsAndReviewsStatus(ReviewsStatusSpam, threadKey);
        }

        /// <summary>
        ///  Hide review
        /// </summary>
        [ServerArrangement]
        public void HideReview()
        {
            Guid pageId = ServerOperations.Pages().GetPageId(PageName);
            string threadKey = pageId.ToString() + "_en_review";
            ServerOperationsFeather.CommentsAndReviews().ChangeCommentsAndReviewsStatus(ReviewsStatusHidden, threadKey);
        }

        /// <summary>
        /// Tears down.
        /// </summary>
        [ServerTearDown]
        public void TearDown()
        {
            ServerOperations.Pages().DeleteAllPages();
            var siteID = ServerOperations.Comments().GetCurrentSiteId.ToString();
            ServerOperations.Comments().DeleteAllComments(siteID);
            ServerOperations.Comments().AllowComments(ThreadType, false);
        }

        private const string PageName = "ReviewsPage";
        private const string PageTemplateName = "Bootstrap.default";
        private const string ThreadType = "Telerik.Sitefinity.Pages.Model.PageNode";
        private const string ReviewsStatusWaiting = "Waiting for approval";
        private const string ReviewsStatusPublish = "Published";
        private const string ReviewsStatusSpam = "Spam";
        private const string ReviewsStatusHidden = "Hidden";
        private const string ReviewsMessage = "Reviews to page";
        private const decimal ReviewsRating = 2;
    }
}
