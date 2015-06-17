using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using Telerik.Sitefinity.Modules.Newsletters.Composition;

namespace Telerik.Sitefinity.Frontend.EmailCampaigns.Mvc.Models.UnsubscribeForm
{
    /// <summary>
    /// The public interface of the Unsubscribe widget's model.
    /// </summary>
    public interface IUnsubscribeFormModel
    {
        /// <summary>
        /// Gets or sets the id of the list from which the user will unsubscribe.
        /// </summary>
        Guid ListId { get; set; }

        /// <summary>
        /// Gets or sets the name of the provider to be used to unsubscribe.
        /// </summary>
        string ProviderName { get; set; }

        /// <summary>
        /// Gets or sets the title of the widget when EmailAddress unsubscribe mode is selected.
        /// </summary>
        string WidgetTitle { get; set; }

        /// <summary>
        /// Gets or sets the description of the widget when EmailAddress unsubscribe mode is selected.
        /// </summary>
        string WidgetDescription { get; set; }

        /// <summary>
        /// Gets or sets the message that will be shown when Link unsubscribe mode is selected.
        /// </summary>
        string Message { get; set; }

        /// <summary>
        /// Determines how to unsubscribe the user, by entering an e-mail address or by link included in a newsletter.
        /// </summary>
        UnsubscribeMode UnsubscribeMode { get; set; }

        /// <summary>
        /// Gets or sets the page id.
        /// </summary>
        /// <value>The page id.</value>
        Guid PageId { get; set; }

        /// <summary>
        /// Gets or sets the successfully submitted form.
        /// </summary>
        /// <value>The successfully submitted form.</value>
        SuccessfullySubmittedForm SuccessfullySubmittedForm { get; set; }

        /// <summary>
        /// Gets or sets the CSS class that will be applied on the wrapper div of the widget when Link unsubscribe mode is selected.
        /// </summary>
        /// <value>
        /// The CSS class.
        /// </value>
        string LinkCssClass { get; set; }

        /// <summary>
        /// Gets or sets the CSS class that will be applied on the wrapper div of the widget when EmailAddress unsubscribe mode is selected.
        /// </summary>
        /// <value>
        /// The CSS class.
        /// </value>
        string EmailAddressCssClass { get; set; }

        /// <summary>
        /// Creates the view model.
        /// </summary>
        /// <returns></returns>
        UnsubscribeFormViewModel CreateViewModel();

        /// <summary>
        /// Subscribes or unsubscribes a user from an issue's mailing list.
        /// </summary>
        /// <param name="subscriberId">The subscriber id.</param>
        /// <param name="issueId">The issue id.</param>
        /// <param name="shouldSubscribe">The should subscribe.</param>
        void ExecuteAction(string subscriberId, string issueId, bool shouldSubscribe);

        /// <summary>
        /// Unsubscribes a user by specified email.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="error">The error.</param>
        /// <returns>True if the user is successfully unsubscribed.</returns>
        bool Unsubscribe(string email, out string error);

        ////TODO: add options for 'When the form is successfully submitted...'
    }
}
