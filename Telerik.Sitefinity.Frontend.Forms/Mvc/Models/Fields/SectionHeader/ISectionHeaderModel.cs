using Telerik.Sitefinity.Modules.Forms.Web.UI;

namespace Telerik.Sitefinity.Frontend.Forms.Mvc.Models.Fields.SectionHeader
{
    /// <summary>
    /// This interface provides API for form section header element.
    /// </summary>
    public interface ISectionHeaderModel : IFormElementModel, IHideable
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