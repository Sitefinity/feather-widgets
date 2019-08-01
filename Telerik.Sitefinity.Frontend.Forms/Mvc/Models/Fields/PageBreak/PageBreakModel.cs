using Telerik.Sitefinity.Frontend.Forms.Mvc.StringResources;
using Telerik.Sitefinity.Localization;

namespace Telerik.Sitefinity.Frontend.Forms.Mvc.Models.Fields.PageBreak
{
    /// <summary>
    /// Implements API for working with form PageBreak fields.
    /// </summary>
    public class PageBreakModel : FormElementModel, IPageBreakModel
    {
        /// <inheritDocs />
        public string NextStepText { get; set; } = Res.Get<FieldResources>().NextStepText;

        /// <inheritDocs />
        public string PreviousStepText { get; set; } = Res.Get<FieldResources>().PreviousStepText;

        /// <inheritDocs />
        public bool AllowGoBack { get; set; } = false;

        /// <inheritDocs />
        public override object GetViewModel(object value)
        {
            return new PageBreakViewModel()
            {
                NextStepText = this.NextStepText,
                PreviousStepText = this.PreviousStepText,
                AllowGoBack = this.AllowGoBack,
                CssClass = this.CssClass
            };
        }
    }
}