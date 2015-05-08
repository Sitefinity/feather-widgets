using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Telerik.Sitefinity.Frontend.Comments.Mvc.Models
{
    /// <summary>
    /// Defines API for working with <see cref="Telerik.Sitefinity.Services.Comments.IComment"/> items.
    /// </summary>
    public interface ICommentsModel
    {
        /// <summary>
        /// Gets or sets the CSS class.
        /// </summary>
        /// <value>
        /// The CSS class.
        /// </value>
        string CssClass { get; set; }

        /// <summary>
        /// Gets or sets the thread key that will be used for association of the comment.
        /// </summary>
        string ThreadKey { set; get; }

        /// <summary>
        /// Gets or sets the thread key that will be used for association of the comment.
        /// </summary>
        string ThreadType { set; get; }

        /// <summary>
        /// Gets or sets the group key that will be used for association of the comment.
        /// </summary>
        string GroupKey { set; get; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        string ThreadTitle { set; get; }

        /// <summary>
        /// Gets or sets the data source.
        /// </summary>
        string DataSource { get; set; }

        /// <summary>
        /// Gets or sets the allow comments.
        /// </summary>
        /// <value>
        /// The allow comments.
        /// </value>
        bool? AllowComments { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether thread is closed.
        /// </summary>
        /// <value>
        ///   <c>true</c> if thread is closed; otherwise, <c>false</c>.
        /// </value>
        bool ThreadIsClosed { get; set; }

        /// <summary>
        /// Gets a value indicating whether comments widget can be displayed.
        /// </summary>
        /// <value>
        ///   <c>true</c> if comments widget can be displayed; otherwise, <c>false</c>.
        /// </value>
        [Browsable(false)]
        bool ShowComments { get; }

        /// <summary>
        /// Gets or sets the maximum length of the comment text to be displayed when loaded.
        /// </summary>
        /// <remarks> The rest of comment text will be displayed after clicking "Read full comment" link. 
        /// </remarks>
        /// <value>
        /// The maximum length of the comment text to be dispalyed when comment is loaded.
        /// </value>
        int CommentTextMaxLength { get; set; }

        /// <summary>
        /// Gets the login page URL.
        /// </summary>
        /// <value>
        /// The login page URL.
        /// </value>
        [Browsable(false)]
        string LoginPageUrl { get; }

        /// <summary>
        /// Gets the user avatar image URL.
        /// </summary>
        /// <value>
        /// The avatar image URL.
        /// </value>
        [Browsable(false)]
        string UserAvatarImageUrl { get; }

        /// <summary>
        /// Gets the display name of the user.
        /// </summary>
        /// <value>
        /// The display name of the user.
        /// </value>
        [Browsable(false)]
        string UserDisplayName { get; }

        /// <summary>
        /// Gets or sets the avatar image URL.
        /// </summary>
        /// <value>
        /// The avatar image URL.
        /// </value>
        string DateTimeFormatString { get; set; }

        /// <summary>
        /// Gets the configuration for the thread
        /// </summary>
        /// <value>
        /// The threads configuration.
        /// </value>
        [Browsable(false)]
        ThreadsConfigModel ThreadsConfig { get; }

        /// <summary>
        /// Gets the configuration for the comments module
        /// </summary>
        /// <value>
        /// The comments module configuration.
        /// </value>
        [Browsable(false)]
        CommentsConfigModel CommentsConfig { get; }
    }
}
