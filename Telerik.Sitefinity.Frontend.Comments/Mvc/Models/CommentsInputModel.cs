namespace Telerik.Sitefinity.Frontend.Comments.Mvc.Models
{
    /// <summary>
    /// Represents the DTO for the Index action request of the <see cref="CommentsControlelr"/>
    /// </summary>
    public class CommentsInputModel
    {
        /// <summary>
        /// Gets or sets the allow comments.
        /// </summary>
        /// <value>
        /// The allow comments.
        /// </value>
        public bool? AllowComments { get; set; }

        /// <summary>
        /// Gets or sets the thread key.
        /// </summary>
        /// <value>
        /// The thread key.
        /// </value>
        public string ThreadKey { get; set; }

        /// <summary>
        /// Gets or sets the thread title.
        /// </summary>
        /// <value>
        /// The thread title.
        /// </value>
        public string ThreadTitle { get; set; }

        /// <summary>
        /// Gets or sets the type of the thread.
        /// </summary>
        /// <value>
        /// The type of the thread.
        /// </value>
        public string ThreadType { get; set; }

        /// <summary>
        /// Gets or sets the group key.
        /// </summary>
        /// <value>
        /// The group key.
        /// </value>
        public string GroupKey { get; set; }

        /// <summary>
        /// Gets or sets the data source.
        /// </summary>
        /// <value>
        /// The data source.
        /// </value>
        public string DataSource { get; set; }
    }
}
