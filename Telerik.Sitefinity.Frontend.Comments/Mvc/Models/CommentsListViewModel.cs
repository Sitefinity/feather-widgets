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
        /// Gets or sets the requires authentication.
        /// </summary>
        /// <value>
        /// The requires authentication.
        /// </value>
        public bool RequiresAuthentication { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [allow subscription].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [allow subscription]; otherwise, <c>false</c>.
        /// </value>
        public bool AllowSubscription { get; set; }

        /// <summary>
        /// Gets or sets the requires approval.
        /// </summary>
        /// <value>
        /// The requires approval.
        /// </value>
        public bool RequiresApproval { get; set; }
        
        /// <summary>
        /// Gets or sets the requires captcha.
        /// </summary>
        /// <value>
        /// The requires captcha.
        /// </value>
        public bool RequiresCaptcha { get; set; }

        /// <summary>
        /// Gets whether comments will be displayed on pages.
        /// </summary>
        /// <value>
        ///   <c>true</c> if paging is enabled; otherwise, <c>false</c>.
        /// </value>
        public bool EnablePaging { get; set; }

        /// <summary>
        /// Gets or sets the thread key.
        /// </summary>
        /// <value>
        /// The thread key.
        /// </value>
        public string ThreadKey { get; set; }
    }
}
