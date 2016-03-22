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
        public string NextStepText
        {
            get
            {
                return this.nextStepText;
            }
            set
            {
                this.nextStepText = value;
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
            return new PageBreakViewModel()
            {
                NextStepText = this.NextStepText,
                PreviousStepText = this.PreviousStepText,
                AllowGoBack = this.AllowGoBack,
                CssClass = this.CssClass
            };
        }

        private string nextStepText = Res.Get<FieldResources>().NextStepText;
        private string previousStepText = Res.Get<FieldResources>().PreviousStepText;
        private bool allowGoBack = false;
    }
}