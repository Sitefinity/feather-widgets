using Newtonsoft.Json;
using Telerik.Sitefinity.Metadata.Model;
using Telerik.Sitefinity.Web.UI.Validation.Definitions;

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
        /// Gets or sets a validation mechanism for the control.
        /// </summary>
        /// <value>The validation.</value>
        public ValidatorDefinition ValidatorDefinition { get; set; }

        /// <summary>
        /// Serializes this instance in JSON format.
        /// </summary>
        /// <returns>This instance serialized in JSON format.</returns>
        public string Serialize()
        {
            return JsonConvert.SerializeObject(this);
        }

        /// <summary>
        /// Gets or sets the CSS class.
        /// </summary>
        /// <value>
        /// The CSS class.
        /// </value>
        public string CssClass { get; set; }

        /// <summary>
        /// Gets or sets the form field visiblity.
        /// </summary>
        /// <value>
        /// The meta field.
        /// </value>
        public bool Hidden { get; set; }
    }
}