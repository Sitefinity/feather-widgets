using Telerik.Sitefinity.Frontend.Forms.Mvc.StringResources;
using Telerik.Sitefinity.Localization;

namespace Telerik.Sitefinity.Frontend.Forms.Mvc.Models.Fields.SectionHeader
{
    /// <summary>
    /// Implements API for working with form section header elements.
    /// </summary>
    public class SectionHeaderModel : FormElementModel, ISectionHeaderModel
    {
        /// <inheritDocs />
        public string PlaceholderText
        {
            get
            {
                if (string.IsNullOrEmpty(this.placeholderText))
                {
                    return Res.Get<FieldResources>().SectionHeaderPlaceholderText;
                }

                return this.placeholderText;
            }
            set
            {
                this.placeholderText = value;
            }
        }

        /// <inheritDocs />
        public string Text
        { 
            get; 
            set; 
        }

        /// <inheritDocs />
        public HeadingType HeadingType { get; set; }

        /// <inheritDocs />
        public override object GetViewModel(object value)
        {
            return new SectionHeaderViewModel()
            {
                CssClass = this.CssClass,
                HeadingType = this.HeadingType,
                Text = this.Text
            };
        }

        private string placeholderText;
    }
}
