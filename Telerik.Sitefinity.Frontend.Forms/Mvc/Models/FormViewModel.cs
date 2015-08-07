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
        /// Gets or sets the view mode.
        /// </summary>
        public FormViewMode ViewMode { get; set; }

        /// <summary>
        /// Gets or sets the error if any.
        /// </summary>
        public string Error { get; set; }
    }
}
