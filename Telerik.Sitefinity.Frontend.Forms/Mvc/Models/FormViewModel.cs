﻿using System;

namespace Telerik.Sitefinity.Frontend.Forms.Mvc.Models
{
    /// <summary>
    /// This class represents the view model used to render the Form widget.
    /// </summary>
    public class FormViewModel
    {
        /// <summary>
        /// Gets or sets the view mode.
        /// </summary>
        public FormViewMode ViewMode { get; set; }

        /// <summary>
        /// Gets or sets the error if any.
        /// </summary>
        public string Error { get; set; }

        /// <summary>
        /// Gets or sets the CSS class that will be applied on the wrapper div of the widget.
        /// </summary>
        public string CssClass { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the control to use Ajax submit when the form submit button is clicked.
        /// </summary>
        public bool UseAjaxSubmit { get; set; }

        /// <summary>
        /// Gets or sets the submit URL when using AJAX for submitting.
        /// </summary>
        /// <value>
        /// The ajax submit URL.
        /// </value>
        public string AjaxSubmitUrl { get; set; }

        /// <summary>
        /// Gets or sets the success message.
        /// </summary>
        /// <value>
        /// The success message.
        /// </value>
        public string SuccessMessage { get; set; }

        /// <summary>
        /// Gets or sets the redirect URL.
        /// </summary>
        /// <value>
        /// The redirect URL.
        /// </value>
        public string RedirectUrl { get; set; }

        /// <summary>
        /// Gets or sets the form identifier.
        /// </summary>
        /// <value>
        /// The form identifier.
        /// </value>
        public string FormId { get; set; }

        public bool IsMultiStep { get; set; }
    }
}
