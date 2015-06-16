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
        /// Gets or sets the title.
        /// </summary>
        /// <value>The title.</value>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the URL.
        /// </summary>
        /// <value>The URL.</value>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1056:UriPropertiesShouldNotBeStrings")]
        public string Url { get; set; }

        /// <summary>
        /// Gets or sets the tooltip.
        /// </summary>
        /// <value>The tooltip.</value>
        public string Tooltip { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [open in new window].
        /// </summary>
        /// <value><c>true</c> if [open in new window]; otherwise, <c>false</c>.</value>
        public bool OpenInNewWindow { get; set; }

        /// <summary>
        /// Gets or sets the CSS class that will be applied on the wrapper div of the widget.
        /// </summary>
        /// <value>
        /// The CSS class.
        /// </value>
        public string CssClass { get; set; }
    }
}
