using System;
using System.Linq;

namespace Telerik.Sitefinity.Frontend.Publishing.Mvc.Models
{
    /// <summary>
    /// This class represents the view model of feed.
    /// </summary>
    public class FeedViewModel
    {
        /// <summary>
        /// Gets or sets the feed insertion option.
        /// </summary>
        /// <value>The feed insertion option.</value>
        public FeedInsertionOption InsertionOption { get; set; }

        /// <summary>
        /// Gets or sets the feed link that will be embedded in the head tag.
        /// </summary>
        /// <value>The java script code.</value>
        public string HeadLink { get; set; }

        /// <summary>
        /// Gets or sets the feed link that will be embedded in the page.
        /// </summary>
        /// <value>The link.</value>
        public string Link { get; set; }

        /// <summary>
        /// Gets or sets the CSS class that will be applied on the wrapper div of the widget.
        /// </summary>
        /// <value>
        /// The CSS class.
        /// </value>
        public string CssClass { get; set; }
    }
}
