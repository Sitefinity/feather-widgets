﻿using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Telerik.Sitefinity.ContentLocations;
using Telerik.Sitefinity.Forms.Model;
using Telerik.Sitefinity.Modules.Forms;

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
        /// Gets or sets a value indicating whether the control to use Ajax submit when the form submit button is clicked.
        /// </summary>
        bool UseAjaxSubmit { get; set; }

        /// <summary>
        /// Gets or sets the submit target URL when using AJAX submit. If empty the default form entry handler is used.
        /// </summary>
        /// <value>
        /// The AJAX submit target URL.
        /// </value>
        string AjaxSubmitTargetUrl { get; set; }

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
        /// Gets the redirect page url
        /// </summary>
        string GetRedirectPageUrl();

        /// <summary>
        /// Gets the submit message.
        /// </summary>
        /// <param name="submitedSuccessfully">Did the form submited successfully</param>
        string GetSubmitMessage(bool submitedSuccessfully);

        /// <summary>
        /// Tries to submit the form.
        /// </summary>
        bool TrySubmitForm(FormCollection collection, string userHostAddress);

        /// <summary>
        /// Determines whether a form is valid or not.
        /// </summary>
        /// <param name="form">The form.</param>
        /// <param name="collection">The collection.</param>
        /// <param name="manager">The manager.</param>
        /// <returns></returns>
        bool IsValidForm(FormDescription form, FormCollection collection, FormsManager manager);

        /// <summary>
        /// Sanitizes the form collection.
        /// </summary>
        /// <param name="collection">The collection.</param>
        void SanitizeFormCollection(FormCollection collection);
    }
}