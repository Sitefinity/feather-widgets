using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Telerik.Sitefinity.ContentLocations;

namespace Telerik.Sitefinity.Frontend.Forms.Mvc.Models
{
    /// <summary>
    /// This interface represents the model used for Form widget.
    /// </summary>
    public interface IFormModel
    {
        /// <summary>
        /// Gets or sets the form identifier.
        /// </summary>
        Guid FormId { get; set; }

        /// <summary>
        /// Gets or sets the view mode.
        /// </summary>
        FormViewMode ViewMode { get; set; }

        /// <summary>
        /// Gets or sets the CSS class that will be applied on the wrapper div of the widget.
        /// </summary>
        string CssClass { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether a custom confirmation to a submited form should be used.
        /// </summary>
        bool UseCustomConfirmation { get; set; }

        /// <summary>
        /// Gets or sets a value indicating what custom confirmation should be used.
        /// </summary>
        CustomConfirmationMode CustomConfirmationMode { get; set; }

        /// <summary>
        /// Gets or sets the custom confirmation message to show when the form is submited.
        /// </summary>
        string CustomConfirmationMessage { get; set; }

        /// <summary>
        /// Gets or sets the custom confirmation page id to redirect to when the form is submited.
        /// </summary>
        Guid CustomConfirmationPageId { get; set; }

        /// <summary>
        /// Gets the information for all of the content types that a model is able to show
        /// </summary>
        IEnumerable<IContentLocationInfo> GetLocations();

        /// <summary>
        /// Gets the view model used to render the Form widget.
        /// </summary>
        FormViewModel GetViewModel();

        /// <summary>
        /// Gets the view path used to render the Form widget.
        /// </summary>
        string GetViewPath();

        /// <summary>
        /// Tries to submit the form.
        /// </summary>
        bool TrySubmitForm(FormCollection collection, string userHostAddress);
    }
}