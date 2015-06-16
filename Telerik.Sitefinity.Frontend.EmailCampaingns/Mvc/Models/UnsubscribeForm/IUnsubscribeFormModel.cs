using System;
using System.Linq;
using System.Web.UI;

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

        ////TODO: rename the method. maybe call it in CreateViewModel and remove it from the model?
        void RemoveSubscriber(string subscriberId, string issueId, string subscribe);

        ////TODO: add options for 'When the form is successfully submitted...'
    }
}
