using System.ComponentModel;
using Telerik.Sitefinity.Metadata.Model;
using Telerik.Sitefinity.Web.UI.Validation.Definitions;

namespace Telerik.Sitefinity.Frontend.Forms.Mvc.Models.Fields.ParagraphTextField
{
    /// <summary>
    /// This interface provides API for form paragraph text fields.
    /// </summary>
    public interface IParagraphTextFieldModel : IFormFieldModel
    {
        /// <summary>
        /// Gets or sets the placeholder text.
        /// </summary>
        /// <value>
        /// The placeholder text.
        /// </value>
        string PlaceholderText { get; set; }
        
        /// <summary>
        /// Gets or sets a validation mechanism for the field.
        /// </summary>
        /// <value>The validation.</value>
        [TypeConverter(typeof(ExpandableObjectConverter))]
        ValidatorDefinition ValidatorDefinition { get; set; }
    }
}
