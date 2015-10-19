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
        public override object GetViewModel(object value)
        {
            var viewModel = new SubmitButtonViewModel()
            {
                Label = this.Label,
                CssClass = this.CssClass
            };

            return viewModel;
        }

        private string label = Res.Get<FieldResources>().SubmitButtonLabel;
    }
}