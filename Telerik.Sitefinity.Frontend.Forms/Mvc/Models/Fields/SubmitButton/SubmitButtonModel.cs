using Telerik.Sitefinity.Frontend.Forms.Mvc.StringResources;
using Telerik.Sitefinity.Localization;

namespace Telerik.Sitefinity.Frontend.Forms.Mvc.Models.Fields.SubmitButton
{
    /// <summary>
    /// Implements API for working with form submit button.
    /// </summary>
    public class SubmitButtonModel : FormElementModel, ISubmitButtonModel
    {
        /// <inheritDocs />
        public string Label { get; set; } = Res.Get<FieldResources>().SubmitButtonLabel;

        /// <inheritDocs />
        public string PreviousStepText { get; set; } = Res.Get<FieldResources>().PreviousStepText;

        /// <inheritDocs />
        public bool AllowGoBack { get; set; } = false;

        /// <inheritDocs />
        public override object GetViewModel(object value)
        {
            var viewModel = new SubmitButtonViewModel()
            {
                Label = this.Label,
                PreviousStepText = this.PreviousStepText,
                AllowGoBack = this.AllowGoBack,
                CssClass = this.CssClass
            };

            return viewModel;
        }
    }
}