namespace Telerik.Sitefinity.Frontend.Taxonomies.Helpers
{
    /// <summary>
    /// This class represents the basci model for content type. 
    /// </summary>
    public class ContentTypeModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ContentTypeModel" /> class.
        /// </summary>
        /// <param name="displayName">The display name.</param>
        /// <param name="fullTypeName">Full name of the type.</param>
        public ContentTypeModel(string displayName, string fullTypeName)
        {
            this.DisplayName = displayName;
            this.FullTypeName = fullTypeName;
        }

        /// <summary>
        /// Gets or sets the full name of the type.
        /// </summary>
        /// <value>The full name of the type.</value>
        public string FullTypeName { get; set; }

        /// <summary>
        /// Gets or sets the display name.
        /// </summary>
        /// <value>The display name.</value>
        public string DisplayName { get; set; }
    }
}