namespace Telerik.Sitefinity.Frontend.Forms.Mvc.Models.Fields.SubmitButton
{
    /// <summary>
    /// This interface provides API for form submit button.
    /// </summary>
    public interface ISubmitButtonModel : IFormElementModel
    {
        /// <summary>
        /// Gets or sets the label.
        /// </summary>
        string Label { get; set; }

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