using System;
using System.Linq;

namespace Telerik.Sitefinity.Frontend.EmailCampaigns.Mvc.Models.UnsubscribeForm
{
    /// <summary>
    /// This class represents the view model of Unsubscribe widget.
    /// </summary>
    public class UnsubscribeFormViewModel
    {
        /// <summary>
        /// Gets or sets the message that will be shown when Link unsubscribe mode is selected.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the CSS class that will be applied on the wrapper div of the widget.
        /// </summary>
        /// <value>
        /// The CSS class.
        /// </value>
        public string CssClass { get; set; }
    }
}
