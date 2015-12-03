namespace Telerik.Sitefinity.Frontend.Forms.Mvc.Models.Fields.SubmitButton
{
    /// <summary>
    /// This class represents the view model used to render the Submit Form field.
    /// </summary>
    public class SubmitButtonViewModel
    {
        /// <summary>
        /// Gets or sets the label.
        /// </summary>
        public string Label { get; set; }

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
