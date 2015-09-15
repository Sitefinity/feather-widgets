using Telerik.Sitefinity.Frontend.Forms.Mvc.StringResources;
using Telerik.Sitefinity.Localization;

namespace Telerik.Sitefinity.Frontend.Forms.Mvc.Models.Fields.SectionHeader
{
    /// <summary>
    /// Implements API for working with form section header fields.
    /// </summary>
    public class SectionHeaderFieldModel : FormElementModel, ISectionHeaderModel
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
        public HeadingType HeadingType { get; set; }

        /// <inheritDocs />
        public override object GetViewModel(object value)
        {
            return new SectionHeaderFieldViewModel()
            {
                CssClass = this.CssClass,
                HeadingType = this.HeadingType,
                Text = value.ToString()
            };
        }

        private string placeholderText;
    }
}
