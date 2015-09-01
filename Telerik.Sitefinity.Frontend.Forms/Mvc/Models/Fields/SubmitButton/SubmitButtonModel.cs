using Telerik.Sitefinity.Frontend.Forms.Mvc.StringResources;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Metadata.Model;
using Telerik.Sitefinity.Modules.Forms.Web.UI.Fields;
namespace Telerik.Sitefinity.Frontend.Forms.Mvc.Models.Fields.SubmitButton
{
    /// <summary>
    /// Implements API for working with form submit button.
    /// </summary>
    public class SubmitButtonModel : ISubmitButtonModel
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
        public string CssClass { get; set; }

        /// <inheritDocs />
        public SubmitButtonViewModel GetViewModel()
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