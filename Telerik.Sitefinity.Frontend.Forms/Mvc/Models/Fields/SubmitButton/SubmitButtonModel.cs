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
        public string Label
        {
            get
            {
                return this.label;
            }

            set
            {
                this.label = value;
            }
        }

        /// <inheritDocs />
        public string PreviousStepText
        {
            get
            {
                return this.previousStepText;
            }
            set
            {
                this.previousStepText = value;
            }
        }

        /// <inheritDocs />
        public bool AllowGoBack
        {
            get
            {
                return this.allowGoBack;
            }
            set
            {
                this.allowGoBack = value;
            }
        }

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

        private string label = Res.Get<FieldResources>().SubmitButtonLabel;
        private string previousStepText = Res.Get<FieldResources>().PreviousStepText;
        private bool allowGoBack = false;
    }
}