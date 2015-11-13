
namespace Telerik.Sitefinity.Frontend.Forms.Mvc.Models.Fields.PageBreak
{
    /// <summary>
    /// This interface provides API for form PageBreak fields.
    /// </summary>
    public interface IPageBreakModel : IFormElementModel
    {
        /// <summary>
        /// Gets or sets the text of the next button
        /// </summary>
        string NextStepText { get; set; }

        /// <summary>
        /// Gets or sets the text of the previous button
        /// </summary>
        string PreviousStepText { get; set; }

        /// <summary>
        /// Checks if go back is allowed
        /// </summary>
        bool AllowGoBack { get; set; }
    }
}
