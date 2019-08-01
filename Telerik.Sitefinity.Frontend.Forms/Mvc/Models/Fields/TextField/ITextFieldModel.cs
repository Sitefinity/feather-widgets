using System.ComponentModel;
using Telerik.Sitefinity.Modules.Forms.Web.UI;
using Telerik.Sitefinity.Web.UI.Validation.Definitions;

namespace Telerik.Sitefinity.Frontend.Forms.Mvc.Models.Fields.TextField
{
    /// <summary>
    /// This interface provides API for form text fields.
    /// </summary>
    public interface ITextFieldModel : IFormFieldModel, IHideable
    {
        /// <summary>
        /// Gets or sets the placeholder text.
        /// </summary>
        /// <value>
        /// The placeholder text.
        /// </value>
        string PlaceholderText { get; set; }

        /// <summary>
        /// Gets or sets the type of the input element.
        /// </summary>
        /// <value>
        /// The type of the input element.
        /// </value>
        TextType InputType { get; set; }
        
        /// <summary>
        /// Gets the serialized input type regex patterns.
        /// </summary>
        /// <value>
        /// The serialized input type regex patterns.
        /// </value>
        string SerializedInputTypeRegexPatterns { get; }
    }
}
