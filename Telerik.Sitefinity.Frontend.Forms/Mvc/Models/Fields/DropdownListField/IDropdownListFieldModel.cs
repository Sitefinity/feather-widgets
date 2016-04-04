using System.Collections.Generic;
using System.ComponentModel;
using Telerik.Sitefinity.Web.UI.Validation.Definitions;

namespace Telerik.Sitefinity.Frontend.Forms.Mvc.Models.Fields.DropdownListField
{
    /// <summary>
    /// This interface provides API for form dropdown list fields.
    /// </summary>
    public interface IDropdownListFieldModel : IFormFieldModel
    {
        /// <summary>
        /// Gets or sets the serialized choices.
        /// </summary>
        /// <value>
        /// The serialized choices.
        /// </value>
        string SerializedChoices { get; set; }
        
        /// <summary>
        /// Gets or sets a validation mechanism for the field.
        /// </summary>
        /// <value>The validation.</value>
        [TypeConverter(typeof(ExpandableObjectConverter))]
        ValidatorDefinition ValidatorDefinition { get; set; }

        /// <summary>
        /// Deserializes the choices.
        /// </summary>
        /// <returns>Deserialized choices.</returns>
        IEnumerable<string> DeserializeChoices();
    }
}
