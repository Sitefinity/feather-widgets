using System;
using System.Collections.Generic;
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
        bool? AllowComments
        { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to restrict to authenticated users.
        /// </summary>
        bool RestrictToAuthenticated { set; get; }

        /// <summary>
        /// Gets or sets a value indicating whether thread is closed.
        /// </summary>
        /// <value>
        ///   <c>true</c> if thread is closed; otherwise, <c>false</c>.
        /// </value>
        bool ThreadIsClosed
        { get; set; }
    }
}
