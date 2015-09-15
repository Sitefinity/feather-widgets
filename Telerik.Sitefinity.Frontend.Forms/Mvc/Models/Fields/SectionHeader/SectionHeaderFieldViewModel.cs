namespace Telerik.Sitefinity.Frontend.Forms.Mvc.Models.Fields.SectionHeader
{
    /// <summary>
    /// This class represents view model for section header field.
    /// </summary>
    public class SectionHeaderFieldViewModel
    {
        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>
        /// The text.
        /// </value>
        public string Text { get; set; }

        /// <summary>
        /// Gets or sets the type of the heading.
        /// </summary>
        /// <value>
        /// The type of the heading.
        /// </value>
        public HeadingType HeadingType { get; set; }

        /// <summary>
        /// Gets or sets the CSS class.
        /// </summary>
        /// <value>
        /// The CSS class.
        /// </value>
        public string CssClass { get; set; }
    }
}
