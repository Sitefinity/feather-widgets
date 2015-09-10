using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.UI;
using Telerik.Sitefinity.Metadata.Model;
using Telerik.Sitefinity.Modules.Forms.Web.UI.Fields;
using Telerik.Sitefinity.Web.UI.Validation.Definitions;

namespace Telerik.Sitefinity.Frontend.Forms.Mvc.Models.Fields.TextField
{
    /// <summary>
    /// This interface provides API for form text fields.
    /// </summary>
    public interface ITextFieldModel : IFormFieldModel
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
        /// Gets or sets the CSS class.
        /// </summary>
        /// <value>
        /// The CSS class.
        /// </value>
        string CssClass { get; set; }

        /// <summary>
        /// Gets the serialized input type regex patterns.
        /// </summary>
        /// <value>
        /// The serialized input type regex patterns.
        /// </value>
        string SerializedInputTypeRegexPatterns { get; }

        /// <summary>
        /// Gets or sets a validation mechanism for the field.
        /// </summary>
        /// <value>The validation.</value>
        [TypeConverter(typeof(ExpandableObjectConverter))]
        ValidatorDefinition ValidatorDefinition { get; set; }

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
        TextFieldViewModel GetViewModel(object value, IMetaField metaField);
    }
}
