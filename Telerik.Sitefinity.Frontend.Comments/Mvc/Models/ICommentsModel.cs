using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Telerik.Sitefinity.Services.Comments;

namespace Telerik.Sitefinity.Frontend.Comments.Mvc.Models
{
    /// <summary>
    /// Defines API for working with <see cref="Telerik.Sitefinity.Services.Comments.IComment"/> items.
    /// </summary>
    public interface ICommentsModel
    {
        /// <summary>
        /// Gets or sets the type of the thread.
        /// </summary>
        /// <value>
        /// The type of the thread.
        /// </value>
        string ThreadType { set; get; }

        /// <summary>
        /// Gets or sets the thread key.
        /// </summary>
        /// <value>
        /// The thread key.
        /// </value>
        string ThreadKey { set; get; }

        /// <summary>
        /// Gets or sets the group key.
        /// </summary>
        /// <value>
        /// The group key.
        /// </value>
        string GroupKey { set; get; }

        /// <summary>
        /// Gets or sets the thread title.
        /// </summary>
        /// <value>
        /// The thread title.
        /// </value>
        string ThreadTitle { set; get; }

        /// <summary>
        /// Gets or sets the data source.
        /// </summary>
        /// <value>
        /// The data source.
        /// </value>
        string DataSource { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether thread is closed.
        /// </summary>
        /// <value>
        ///   <c>true</c> if thread is closed; otherwise, <c>false</c>.
        /// </value>
        bool ThreadIsClosed { get; set; }

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
        /// Gets or sets the avatar image URL.
        /// </summary>
        /// <value>
        /// The avatar image URL.
        /// </value>
        string DateTimeFormatString { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [comments automatic refresh].
        /// </summary>
        /// <value>
        /// <c>true</c> if [comments automatic refresh]; otherwise, <c>false</c>.
        /// </value>
        bool CommentsAutoRefresh { get; set; }

        /// <summary>
        /// Gets or sets the comments refresh interval.
        /// </summary>
        /// <value>
        /// The comments refresh interval.
        /// </value>
        int CommentsRefreshInterval { get; set; }

        /// <summary>
        /// Gets or sets the CSS class.
        /// </summary>
        /// <value>
        /// The CSS class.
        /// </value>
        string CssClass { get; set; }

        /// <summary>
        /// Gets the view model.
        /// </summary>
        /// <param name="commentsInputModel">The comments input model.</param>
        /// <returns></returns>
        CommentsListViewModel GetCommentsListViewModel(CommentsInputModel commentsInputModel);
        /// Gets the comments count view model.
        /// </summary>
        /// <param name="navigateUrl">The URL where the comments count link will navigate.</param>
        /// <param name="threadKey">The thread key.</param>
        /// <returns></returns>
        CommentsCountViewModel GetCommentsCountViewModel(CommentsCountInputModel commentsCountInputModel);
    }
}
