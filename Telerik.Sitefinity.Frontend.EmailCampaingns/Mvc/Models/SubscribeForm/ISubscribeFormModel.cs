using System;
using System.Linq;

namespace Telerik.Sitefinity.Frontend.EmailCampaigns.Mvc.Models
{
    /// <summary>
    /// This interface is used as a model for the SubscribeFormController.
    /// </summary>
    public interface ISubscribeFormModel
    {
        /// <summary>
        /// Gets or sets the selected mailing list id.
        /// </summary>
        /// <value>The selected mailing list id.</value>
        Guid SelectedMailingListId { get; set; }

        /// <summary>
        /// Gets or sets the successfully submitted form.
        /// </summary>
        /// <value>The successfully submitted form.</value>
        SuccessfullySubmittedForm SuccessfullySubmittedForm { get; set; }

        /// <summary>
        /// Gets or sets the css class that will be applied on the wrapping element of the widget.
        /// </summary>
        /// <value>
        /// The css class value as a string.
        /// </value>
        string CssClass { get; set; }

        /// <summary>
        /// Gets or sets the provider.
        /// </summary>
        /// <value>The provider.</value>
        string Provider { get; set; }

        /// <summary>
        /// Gets or sets the page id.
        /// </summary>
        /// <value>The page id.</value>
        Guid PageId { get; set; }

        /// <summary>
        /// Creates the view model.
        /// </summary>
        /// <returns></returns>
        SubscribeFormViewModel CreateViewModel();

        /// <summary>
        /// Adds the subscriber.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="error">The error.</param>
        /// <returns></returns>
        bool AddSubscriber(SubscribeFormViewModel model, out string error);
    }
}
