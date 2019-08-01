namespace Telerik.Sitefinity.Frontend.Forms.Mvc.Models.Fields.SectionHeader
{
    /// <summary>
    /// Implements API for working with form section header elements.
    /// </summary>
    public class SectionHeaderModel : FormElementModel, ISectionHeaderModel
    {
        /// <inheritDocs />
        public string Text
        { 
            get; 
            set; 
        }

        /// <inheritDocs />
        public bool Hidden { get; set; }

        /// <inheritDocs />
        public override object GetViewModel(object value)
        {
            return new SectionHeaderViewModel()
            {
                CssClass = this.CssClass,
                Text = this.Text,
                Hidden = this.Hidden
            };
        }
    }
}