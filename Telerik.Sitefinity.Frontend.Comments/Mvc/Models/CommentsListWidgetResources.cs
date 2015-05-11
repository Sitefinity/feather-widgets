using Newtonsoft.Json;

namespace Telerik.Sitefinity.Frontend.Comments.Mvc.Models
{
    /// <summary>
    /// Resources for the comments-list widget.
    /// </summary>
    public class CommentsListWidgetResources
    {
        /// <summary>
        /// Gets or sets the read full comment.
        /// </summary>
        /// <value>
        /// The read full comment.
        /// </value>
        [JsonProperty("readFullComment")]
        public string ReadFullComment { get; set; }

        /// <summary>
        /// Gets or sets the comments plural.
        /// </summary>
        /// <value>
        /// The comments plural.
        /// </value>
        [JsonProperty("commentsPlural")]
        public string CommentsPlural { get; set; }

        /// <summary>
        /// Gets or sets the subscribe to new comments.
        /// </summary>
        /// <value>
        /// The subscribe to new comments.
        /// </value>
        [JsonProperty("subscribeToNewComments")]
        public string SubscribeToNewComments { get; set; }

        /// <summary>
        /// Gets or sets the unsubscribe from new comments.
        /// </summary>
        /// <value>
        /// The unsubscribe from new comments.
        /// </value>
        [JsonProperty("unsubscribeFromNewComments")]
        public string UnsubscribeFromNewComments { get; set; }

        /// <summary>
        /// Gets or sets the comment pending approval.
        /// </summary>
        /// <value>
        /// The comment pending approval.
        /// </value>
        [JsonProperty("commentPendingApproval")]
        public string CommentPendingApproval { get; set; }
       
        //// "readFullComment": "@Html.Resource("ReadFullComment")", "commentsPlural": "@Html.Resource("CommentsPlural")", "subscribeToNewComments":"@Html.Resource("SubscribeToNewComments")", 
        //// "unsubscribeFromNewComments":"@Html.Resource("UnsubscribeFromNewComments")", "commentPendingApproval" : "@Html.Resource("PendingApproval")"
    }
}
