namespace Telerik.Sitefinity.Frontend.Forms.Mvc.Models.Fields.SubmitButton
{
    /// <summary>
    /// This interface provides API for form submit button.
    /// </summary>
    public interface ISubmitButtonModel : IFormFieldModel
    {
        /// <summary>
        /// Gets or sets the label.
        /// </summary>
        string Label { get; set; }

        /// <summary>
        /// Gets or sets the CSS class.
        /// </summary>
        string CssClass { get; set; }

        /// <summary>
        /// Gets the view model used to render the Form widget.
        /// </summary>
        SubmitButtonViewModel GetViewModel();
    }
}