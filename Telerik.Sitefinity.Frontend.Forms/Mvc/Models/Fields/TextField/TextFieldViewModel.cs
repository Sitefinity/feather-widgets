using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Telerik.Sitefinity.Metadata.Model;
using Telerik.Sitefinity.Web.UI.Validation.Definitions;

namespace Telerik.Sitefinity.Frontend.Forms.Mvc.Models.Fields.TextField
{
    /// <summary>
    /// This class represents view model for text field.
    /// </summary>
    public class TextFieldViewModel
    {
        /// <summary>
        /// Gets or sets the placeholder text.
        /// </summary>
        /// <value>
        /// The placeholder text.
        /// </value>
        public string PlaceholderText { get; set; }

        /// <summary>
        /// Gets or sets the type of the input element.
        /// </summary>
        /// <value>
        /// The type of the input element.
        /// </value>
        public TextType InputType { get; set; }

        /// <summary>
        /// Gets or sets the value of the form field.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public object Value { get; set; }

        /// <summary>
        /// Gets or sets the CSS class.
        /// </summary>
        /// <value>
        /// The CSS class.
        /// </value>
        public string CssClass { get; set; }

        /// <summary>
        /// Gets or sets the validation attributes.
        /// </summary>
        /// <value>
        /// The validation attributes.
        /// </value>
        public MvcHtmlString ValidationAttributes { get; internal set; }

        /// <summary>
        /// Gets or sets a validation mechanism for the control.
        /// </summary>
        /// <value>The validation.</value>
        public ValidatorDefinition ValidatorDefinition { get; set; }


        /// <summary>
        /// Gets or sets the meta field.
        /// </summary>
        /// <value>
        /// The meta field.
        /// </value>
        public IMetaField MetaField { get; set; }
    }
}
