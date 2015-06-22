using System;
using System.Linq;

namespace Telerik.Sitefinity.Frontend.Publishing.Mvc.Models
{
    /// <summary>
    /// Enumeration for the feed insertion option
    /// </summary>
    public enum FeedInsertionOption
    {
        /// <summary>
        /// Link is inserted both on the page and the address bar feeds
        /// </summary>
        PageAndAddressBar,
        /// <summary>
        /// Link is inserted on the address bar feeds only
        /// </summary>
        AddressBarOnly,
        /// <summary>
        /// Link is inserted on the page only
        /// </summary>
        PageOnly
    }
}
