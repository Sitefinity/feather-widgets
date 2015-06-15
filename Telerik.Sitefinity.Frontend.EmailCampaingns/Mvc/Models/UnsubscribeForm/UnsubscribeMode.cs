using System;
using System.Linq;

namespace Telerik.Sitefinity.Frontend.EmailCampaigns.Mvc.Models.UnsubscribeForm
{
    /// <summary>
    /// Defines available modes for unsubscribing.
    /// </summary>
    public enum UnsubscribeMode
    {
        /// <summary>
        /// Unsubscribe by entering an e-mail.
        /// </summary>
        EmailAddress,

        /// <summary>
        /// Unsubscribe by link included in a newsletter.
        /// </summary>
        Link
    }
}
