using System;

namespace Telerik.Sitefinity.Frontend.Forms.Mvc.Models
{
    /// <summary>
    /// This class represents the view model used to render the Form widget.
    /// </summary>
    public class FormViewModel
    {
        /// <summary>
        /// Gets or sets the form identifier.
        /// </summary>
        public Guid FormId { get; set; }

        /// <summary>
        /// Gets or sets the name of the form.
        /// </summary>
        /// <value>
        /// The name of the form.
        /// </value>
        public string FormName { get; set; }

        /// <summary>
        /// Gets or sets the view mode.
        /// </summary>
        public FormViewMode ViewMode { get; set; }

        /// <summary>
        /// Gets or sets the error if any.
        /// </summary>
        public string Error { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the control to use Ajax submit when the form submit button is clicked.
        /// </summary>
        public bool UseAjaxSubmit { get; set; }

        /// <summary>
        /// Gets or sets the submit target URL when using AJAX submit. If empty the default form entry handler is used.
        /// </summary>
        /// <value>
        /// The AJAX submit target URL.
        /// </value>
        public string AjaxSubmitTargetUrl { get; set; }
    }
}
