namespace Telerik.Sitefinity.Frontend.Comments.Mvc.Models
{
    /// <summary>
    /// The view model for the index action of the <see cref="CommentsController"/>
    /// </summary>
    public class CommentsListViewModel
    {
        /// <summary>
        /// Gets or sets the serialized widget settings.
        /// </summary>
        /// <value>
        /// The serialized widget settings.
        /// </value>
        public string SerializedWidgetSettings { get; set; }

        /// <summary>
        /// Gets or sets the serialized widget resources.
        /// </summary>
        /// <value>
        /// The serialized widget resources.
        /// </value>
        public string SerializedWidgetResources { get; set; }

        /// <summary>
        /// Gets or sets the CSS class.
        /// </summary>
        /// <value>
        /// The CSS class.
        /// </value>
        public string CssClass { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [thread is closed].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [thread is closed]; otherwise, <c>false</c>.
        /// </value>
        public bool ThreadIsClosed { get; set; }

        /// <summary>
        /// Gets or sets the user avatar image URL.
        /// </summary>
        /// <value>
        /// The user avatar image URL.
        /// </value>
        public string UserAvatarImageUrl { get; set; }

        /// <summary>
        /// Gets or sets the login page URL.
        /// </summary>
        /// <value>
        /// The login page URL.
        /// </value>
        public string LoginPageUrl { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [allow comments].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [allow comments]; otherwise, <c>false</c>.
        /// </value>
        public bool AllowComments { get; set; }
    }
}
