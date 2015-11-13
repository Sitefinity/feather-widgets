
namespace Telerik.Sitefinity.Frontend.Forms.Mvc.Models.Fields.PageBreak
{
    /// <summary>
    /// This class represents view model for PageBreak field.
    /// </summary>
    public class PageBreakViewModel
    {
        /// <summary>
        /// Gets or sets the text of the next button
        /// </summary>
        public string NextStepText { get; set; }

        /// <summary>
        /// Gets or sets the text of the previous button
        /// </summary>
        public string PreviousStepText { get; set; }

        /// <summary>
        /// Checks if go back is allowed
        /// </summary>
        public bool AllowGoBack { get; set; }

        /// <summary>
        /// Gets or sets the CSS class.
        /// </summary>
        public string CssClass { get; set; }
    }
}
