using System.ComponentModel;
using Telerik.Sitefinity.Metadata.Model;

namespace Telerik.Sitefinity.Frontend.Forms.Mvc.Models.Fields.SectionHeader
{
    /// <summary>
    /// This interface provides API for form section header element.
    /// </summary>
    public interface ISectionHeaderModel : IFormElementModel
    {
        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>
        /// The text.
        /// </value>
        string Text { get; set; }
    }
}
