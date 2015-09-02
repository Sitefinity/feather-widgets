using System.ComponentModel;
using Telerik.Sitefinity.Metadata.Model;

namespace Telerik.Sitefinity.Frontend.Forms.Mvc.Models.Fields.SectionHeaderField
{
    /// <summary>
    /// This interface provides API for form section header field.
    /// </summary>
    public interface ISectionHeaderFieldModel : IFormFieldModel
    {
        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>
        /// The text.
        /// </value>
        string Text { get; set; }

        /// <summary>
        /// Gets or sets the placeholder text.
        /// </summary>
        /// <value>
        /// The placeholder text.
        /// </value>
        string PlaceholderText { get; set; }

        /// <summary>
        /// Gets or sets the type of the heading.
        /// </summary>
        /// <value>
        /// The type of the heading.
        /// </value>
        HeadingType HeadingType { get; set; }

        /// <summary>
        /// Gets or sets the CSS class.
        /// </summary>
        /// <value>
        /// The CSS class.
        /// </value>
        string CssClass { get; set; }

        /// <summary>
        /// Gets or sets the meta field.
        /// </summary>
        /// <value>
        /// The meta field.
        /// </value>
        [TypeConverter(typeof(ExpandableObjectConverter))]
        IMetaField MetaField { get; set; }

        /// <summary>
        /// Gets the view model.
        /// </summary>
        /// <returns></returns>
        SectionHeaderFieldViewModel GetViewModel();
    }
}
