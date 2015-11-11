using System.Collections.Generic;

namespace Telerik.Sitefinity.Frontend.Forms.Mvc.Models.Fields.NavigationField
{
    /// <summary>
    /// This class represents view model for PageBreak field.
    /// </summary>
    public class NavigationFieldViewModel
    {
         /// <summary>
        /// Gets or sets the pages.
        /// </summary>
        /// <value>
        /// The choices.
        /// </value>
        public IEnumerable<FormPage> Pages { get; set; }

        /// <summary>
        /// Gets or sets the value of the form element.
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
    }
}
