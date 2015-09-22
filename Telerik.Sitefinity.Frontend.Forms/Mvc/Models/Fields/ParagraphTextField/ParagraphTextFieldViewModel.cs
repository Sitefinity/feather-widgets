using Telerik.Sitefinity.Metadata.Model;
namespace Telerik.Sitefinity.Frontend.Forms.Mvc.Models.Fields.ParagraphTextField
{
    /// <summary>
    /// This class represents view model for paragraph text field.
    /// </summary>
    public class ParagraphTextFieldViewModel
    {
        /// <summary>
        /// Gets or sets the value of the form field.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public object Value { get; set; }

        /// <summary>
        /// Gets or sets the name of the field.
        /// </summary>
        /// <value>
        /// The name of the field.
        /// </value>
        public IMetaField MetaField { get; set; }

        /// <summary>
        /// Gets or sets the placeholder text.
        /// </summary>
        /// <value>
        /// The placeholder text.
        /// </value>
        public string PlaceholderText { get; set; }

        /// <summary>
        /// Gets or sets the validation attributes.
        /// </summary>
        /// <value>
        /// The validation attributes.
        /// </value>
        public string ValidationAttributes { get; set; }

        /// <summary>
        /// Gets or sets the required error message.
        /// </summary>
        /// <value>
        /// The required error message.
        /// </value>
        public string RequiredViolationMessage { get; set; }

        /// <summary>
        /// Gets or sets the maximum length violation message.
        /// </summary>
        /// <value>
        /// The maximum length violation message.
        /// </value>
        public string MaxLengthViolationMessage { get; set; }
        
        /// <summary>
        /// Gets or sets the CSS class.
        /// </summary>
        /// <value>
        /// The CSS class.
        /// </value>
        public string CssClass { get; set; }
    }
}
