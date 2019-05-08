using System.Collections.Generic;
using System.ComponentModel;
using Telerik.Sitefinity.Modules.Forms.Web.UI;
using Telerik.Sitefinity.Web.UI.Validation.Definitions;

namespace Telerik.Sitefinity.Frontend.Forms.Mvc.Models.Fields.MultipleChoiceField
{
    /// <summary>
    /// This interface provides API for form multiple choice fields.
    /// </summary>
    public interface IMultipleChoiceFieldModel : IFormFieldModel, IHideable
    {
        /// <summary>
        /// Gets or sets the serialized choices.
        /// </summary>
        /// <value>
        /// The serialized choices.
        /// </value>
        string SerializedChoices { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance has other choice.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance has other choice; otherwise, <c>false</c>.
        /// </value>
        bool HasOtherChoice { get; set; }

        /// <summary>
        /// Gets or sets a validation mechanism for the field.
        /// </summary>
        /// <value>The validation.</value>
        [TypeConverter(typeof(ExpandableObjectConverter))]
        ValidatorDefinition ValidatorDefinition { get; set; }

        /// <summary>
        /// Deserializes the choices.
        /// </summary>
        /// <returns></returns>
        IEnumerable<string> DeserializeChoices();
    }
}
