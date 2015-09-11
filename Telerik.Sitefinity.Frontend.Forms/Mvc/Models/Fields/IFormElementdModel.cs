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

namespace Telerik.Sitefinity.Frontend.Forms.Mvc.Models.Fields
{
    /// <summary>
    /// This interface provides API for working with forms fields.
    /// </summary>
    public interface IFormElementdModel
    {
        /// <summary>
        /// Gets or sets the value of the form element.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        object Value { get; set; }

        /// <summary>
        /// Gets or sets the CSS class.
        /// </summary>
        /// <value>
        /// The CSS class.
        /// </value>
        string CssClass { get; set; }

        /// <summary>
        /// Gets or sets a validation mechanism for the control.
        /// </summary>
        /// <value>The validation.</value>
        [TypeConverter(typeof(ExpandableObjectConverter))]
        ValidatorDefinition ValidatorDefinition { get; set; }
        
        /// <summary>
        /// Determines whether this instance is valid.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        bool IsValid(object value);

        /// <summary>
        /// Gets the populated ViewModel associated with the element.
        /// </summary>
        /// <param name="value">The value of the element.</param>
        /// <returns></returns>
        object GetViewModel(object value);
    }
}
