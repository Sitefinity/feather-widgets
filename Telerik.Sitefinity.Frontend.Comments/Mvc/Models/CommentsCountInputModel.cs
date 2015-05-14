namespace Telerik.Sitefinity.Frontend.Comments.Mvc.Models
{
    /// <summary>
    /// Represents the DTO for the Count action request of the <see cref="CommentsControlelr"/>
    /// </summary>
    public class CommentsCountInputModel
    {
        /// <summary>
        /// Gets or sets the allow comments.
        /// </summary>
        /// <value>
        /// The allow comments.
        /// </value>
        public bool? AllowComments { get; set; }

        /// <summary>
        /// Gets or sets the navigate URL.
        /// </summary>
        /// <value>
        /// The navigate URL.
        /// </value>
        public string NavigateUrl { get; set; }

        /// <summary>
        /// Gets or sets the thread key.
        /// </summary>
        /// <value>
        /// The thread key.
        /// </value>
        public string ThreadKey { get; set; }
    }
}
