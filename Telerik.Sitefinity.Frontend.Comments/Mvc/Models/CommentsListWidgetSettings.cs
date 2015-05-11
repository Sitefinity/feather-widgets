using Newtonsoft.Json;
using Telerik.Sitefinity.Services.Comments;

namespace Telerik.Sitefinity.Frontend.Comments.Mvc.Models
{
    /// <summary>
    /// Settings for the comments-list widget.
    /// </summary>
    public class CommentsListWidgetSettings
    {
        /// <summary>
        /// Gets or sets the comments thread key.
        /// </summary>
        /// <value>
        /// The comments thread key.
        /// </value>
        [JsonProperty("commentsThreadKey")]
        public string CommentsThreadKey { get; set; }

        /// <summary>
        /// Gets or sets the type of the comments thread.
        /// </summary>
        /// <value>
        /// The type of the comments thread.
        /// </value>
        [JsonProperty("commentsThreadType")]
        public string CommentsThreadType { get; set; }

        /// <summary>
        /// Gets or sets the root URL.
        /// </summary>
        /// <value>
        /// The root URL.
        /// </value>
        [JsonProperty("rootUrl")]
        public string RootUrl { get; set; }

        /// <summary>
        /// Gets or sets the is user authenticated URL.
        /// </summary>
        /// <value>
        /// The is user authenticated URL.
        /// </value>
        [JsonProperty("isUserAuthenticatedUrl")]
        public string IsUserAuthenticatedUrl { get; set; }

        /// <summary>
        /// Gets or sets the comments per page.
        /// </summary>
        /// <value>
        /// The comments per page.
        /// </value>
        [JsonProperty("commentsPerPage")]
        public int CommentsPerPage { get; set; }

        /// <summary>
        /// Gets or sets the maximum length of the comments text.
        /// </summary>
        /// <value>
        /// The maximum length of the comments text.
        /// </value>
        [JsonProperty("commentsTextMaxLength")]
        public int CommentsTextMaxLength { get; set; }

        /// <summary>
        /// Gets or sets the comments allow subscription.
        /// </summary>
        /// <value>
        /// The comments allow subscription.
        /// </value>
        [JsonProperty("commentsAllowSubscription")]
        public bool CommentsAllowSubscription { get; set; }

        /// <summary>
        /// Gets or sets the requires captcha.
        /// </summary>
        /// <value>
        /// The requires captcha.
        /// </value>
        [JsonProperty("requiresCaptcha")]
        public bool RequiresCaptcha { get; set; }

        /// <summary>
        /// Gets or sets the requires authentication.
        /// </summary>
        /// <value>
        /// The requires authentication.
        /// </value>
        [JsonProperty("requiresAuthentication")]
        public bool RequiresAuthentication { get; set; }

        /// <summary>
        /// Gets or sets the requires approval.
        /// </summary>
        /// <value>
        /// The requires approval.
        /// </value>
        [JsonProperty("requiresApproval")]
        public bool RequiresApproval { get; set; }

        /// <summary>
        /// Gets or sets the comments automatic refresh.
        /// </summary>
        /// <value>
        /// The comments automatic refresh.
        /// </value>
        [JsonProperty("commentsAutoRefresh")]
        public bool CommentsAutoRefresh { get; set; }

        /// <summary>
        /// Gets or sets the comments refresh interval.
        /// </summary>
        /// <value>
        /// The comments refresh interval.
        /// </value>
        [JsonProperty("commentsRefreshInterval")]
        public int CommentsRefreshInterval { get; set; }

        /// <summary>
        /// Gets or sets the comments initially sorted descending.
        /// </summary>
        /// <value>
        /// The comments initially sorted descending.
        /// </value>
        [JsonProperty("commentsInitiallySortedDescending")]
        public bool CommentsInitiallySortedDescending { get; set; }

        /// <summary>
        /// Gets or sets the comment date time format string.
        /// </summary>
        /// <value>
        /// The comment date time format string.
        /// </value>
        [JsonProperty("commentDateTimeFormatString")]
        public string CommentDateTimeFormatString { get; set; }

        /// <summary>
        /// Gets or sets the user avatar image URL.
        /// </summary>
        /// <value>
        /// The user avatar image URL.
        /// </value>
        [JsonProperty("userAvatarImageUrl")]
        public string UserAvatarImageUrl { get; set; }

        /// <summary>
        /// Gets or sets the display name of the user.
        /// </summary>
        /// <value>
        /// The display name of the user.
        /// </value>
        [JsonProperty("userDisplayName")]
        public string UserDisplayName { get; set; }

        /// <summary>
        /// Gets or sets the is design mode.
        /// </summary>
        /// <value>
        /// The is design mode.
        /// </value>
        [JsonProperty("isDesignMode")]
        public bool IsDesignMode { get; set; }

        /// <summary>
        /// Gets or sets the comments thread.
        /// </summary>
        /// <value>
        /// The comments thread.
        /// </value>
        [JsonProperty("commentsThread")]
        public IThread CommentsThread { get; set; }

        //// "commentsThreadKey": "@Model.ThreadKey", "commentsThreadType": "@Model.ThreadType", "rootUrl" : "/RestApi/comments-api/" , "isUserAuthenticatedUrl":"/RestApi/session/is-authenticated" , "commentsPerPage": @Model.CommentsConfig.CommentsPerPage, "commentsTextMaxLength": @Model.CommentTextMaxLength, 
        ////  "commentsAllowSubscription":@commentsAllowSubscription.ToString().ToLower(), "requiresCaptcha": @Model.CommentsConfig.UseSpamProtectionImage.ToString().ToLower(), "requiresAuthentication" : @Model.ThreadsConfig.RequiresAuthentication.ToString().ToLower(), 
        //// "requiresApproval" : @Model.ThreadsConfig.RequiresApproval.ToString().ToLower(), "commentsAutoRefresh" : false, "commentsRefreshInterval": 3000, "commentsInitiallySortedDescending": true, 
        //// "commentDateTimeFormatString" : "@Model.DateTimeFormatString", "userAvatarImageUrl" : "@Model.UserAvatarImageUrl", "userDisplayName" : "@Model.UserDisplayName", "isDesignMode" : @Model.IsDesignMode.ToString().ToLower(), "commentsThread" : @serializedThread
    }
}
