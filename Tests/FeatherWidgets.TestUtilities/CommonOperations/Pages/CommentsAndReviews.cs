using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telerik.Sitefinity.Security.Claims;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Services.Comments;
using Telerik.Sitefinity.Services.Comments.Proxies;
using Telerik.Sitefinity.TestIntegration.Services.Comment;

namespace FeatherWidgets.TestUtilities.CommonOperations
{
    /// <summary>
    /// This class provides access to comments and reviews operations
    /// </summary>
    public class CommentsAndReviews
    {
        /// <summary>
        /// Change comments and reviews status
        /// </summary>
        /// <param name="newStatus">New status</param>
        /// <param name="threadKey">Thread key</param>
        public void ChangeCommentsAndReviewsStatus(string newStatus, string threadKey)
        {
            var cs = SystemManager.GetCommentsService();
            var comments = cs.GetComments(threadKey);

            foreach (var comment in comments)
            {
                comment.Status = newStatus;
                cs.UpdateComment(comment);
            }
        }

        /// <summary>
        /// Change reviews status
        /// </summary>
        /// <param name="newStatus">New status</param>
        /// <param name="reviewKey">Review key</param>
        public void ChangeReviewsStatus(string newStatus, string reviewKey)
        {
            var cs = SystemManager.GetCommentsService();
            var comment = cs.GetComment(reviewKey);
            comment.Status = newStatus;
            cs.UpdateComment(comment);
        }

        public void CreateReview(string domainKey, string threadType, Guid itemId, string itemTitle, string message, decimal? rating, string status)
        {
            var cs = SystemManager.GetCommentsService();
            var authorProxy = new AuthorProxy(ClaimsManager.GetCurrentUserId().ToString());

            var domainProxy = new GroupProxy("TestDomain", "TestDomainDescription", authorProxy) { Key = domainKey };
            cs.CreateGroup(domainProxy);

            var threadProxy = new ThreadProxy(itemTitle, threadType, domainKey, authorProxy) { Key = itemId.ToString() + "_en_review", Language = "en" };
            var thread = cs.CreateThread(threadProxy);

            DateTime dateCreated = DateTime.UtcNow.AddMinutes(5);

            var commentProxy = new CommentProxy(message, thread.Key, authorProxy, SystemManager.CurrentHttpContext.Request.UserHostAddress, rating) 
                                { Status = status, DateCreated = dateCreated };
            cs.CreateComment(commentProxy);
        }
    }
}
