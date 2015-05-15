﻿using Telerik.Sitefinity.Services.Comments;

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
        public string CommentsThreadKey { get; set; }

        /// <summary>
        /// Gets or sets the type of the comments thread.
        /// </summary>
        /// <value>
        /// The type of the comments thread.
        /// </value>
        public string CommentsThreadType { get; set; }

        /// <summary>
        /// Gets or sets the root URL.
        /// </summary>
        /// <value>
        /// The root URL.
        /// </value>
        public string RootUrl { get; set; }

        /// <summary>
        /// Gets or sets the is user authenticated URL.
        /// </summary>
        /// <value>
        /// The is user authenticated URL.
        /// </value>
        public string IsUserAuthenticatedUrl { get; set; }

        /// <summary>
        /// Gets or sets the comments per page.
        /// </summary>
        /// <value>
        /// The comments per page.
        /// </value>
        public int CommentsPerPage { get; set; }

        /// <summary>
        /// Gets or sets the maximum length of the comments text.
        /// </summary>
        /// <value>
        /// The maximum length of the comments text.
        /// </value>
        public int CommentsTextMaxLength { get; set; }

        /// <summary>
        /// Gets or sets the comments allow subscription.
        /// </summary>
        /// <value>
        /// The comments allow subscription.
        /// </value>
        public bool CommentsAllowSubscription { get; set; }

        /// <summary>
        /// Gets or sets the requires captcha.
        /// </summary>
        /// <value>
        /// The requires captcha.
        /// </value>
        public bool RequiresCaptcha { get; set; }

        /// <summary>
        /// Gets or sets the requires authentication.
        /// </summary>
        /// <value>
        /// The requires authentication.
        /// </value>
        public bool RequiresAuthentication { get; set; }

        /// <summary>
        /// Gets or sets the requires approval.
        /// </summary>
        /// <value>
        /// The requires approval.
        /// </value>
        public bool RequiresApproval { get; set; }

        /// <summary>
        /// Gets or sets the comments automatic refresh.
        /// </summary>
        /// <value>
        /// The comments automatic refresh.
        /// </value>
        public bool CommentsAutoRefresh { get; set; }

        /// <summary>
        /// Gets or sets the comments refresh interval.
        /// </summary>
        /// <value>
        /// The comments refresh interval.
        /// </value>
        public int CommentsRefreshInterval { get; set; }

        /// <summary>
        /// Gets or sets the comments initially sorted descending.
        /// </summary>
        /// <value>
        /// The comments initially sorted descending.
        /// </value>
        public bool CommentsInitiallySortedDescending { get; set; }

        /// <summary>
        /// Gets or sets the user avatar image URL.
        /// </summary>
        /// <value>
        /// The user avatar image URL.
        /// </value>
        public string UserAvatarImageUrl { get; set; }

        /// <summary>
        /// Gets or sets the display name of the user.
        /// </summary>
        /// <value>
        /// The display name of the user.
        /// </value>
        public string UserDisplayName { get; set; }

        /// <summary>
        /// Gets or sets the is design mode.
        /// </summary>
        /// <value>
        /// The is design mode.
        /// </value>
        public bool IsDesignMode { get; set; }

        /// <summary>
        /// Gets or sets the comments thread.
        /// </summary>
        /// <value>
        /// The comments thread.
        /// </value>
        public IThread CommentsThread { get; set; }
        
        //// Reviews

        /// <summary>
        /// Gets or sets a value indicating whether [use reviews].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [use reviews]; otherwise, <c>false</c>.
        /// </value>
        public bool UseReviews { get; set; }

        /// <summary>
        /// Gets or sets the has user already reviewed URL.
        /// </summary>
        /// <value>
        /// The has user already reviewed URL.
        /// </value>
        public string HasUserAlreadyReviewedUrl { get; set; }

        /// <summary>
        /// Gets or sets the create comment URL.
        /// </summary>
        /// <value>
        /// The create comment URL.
        /// </value>
        public string CreateCommentUrl { get; set; }
    }
}
