using System;
using System.Linq;

namespace Telerik.Sitefinity.Frontend.Publishing.Mvc.Models
{
    /// <summary>
    /// Classes that implement this interface could be used as a model for Feed widget.
    /// </summary>
    public interface IFeedModel
    {
        /// <summary>
        /// Gets or sets the programmatic identifier assigned to the feed.
        /// </summary>
        /// <value></value>
        /// <returns>
        /// The programmatic identifier assigned to the feed.
        /// </returns>
        Guid FeedId { get; set; }

        /// <summary>
        /// Gets or sets the feed insertion option.
        /// </summary>
        /// <value>The feed insertion option.</value>
        FeedInsertionOption InsertionOption { get; set; }

        /// <summary>
        /// Gets or sets the text displayed in the link.
        /// </summary>
        /// <value>The displayed text.</value>
        string TextToDisplay { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [open in new window].
        /// </summary>
        /// <value><c>true</c> if [open in new window]; otherwise, <c>false</c>.</value>
        bool OpenInNewWindow { get; set; }

        /// <summary>
        /// Gets or sets the tooltip.
        /// </summary>
        /// <value>The tooltip.</value>
        string ToolTip { get; set; }

        /// <summary>
        /// Gets or sets the CSS class that will be applied on the wrapper div of the widget.
        /// </summary>
        /// <value>
        /// The CSS class.
        /// </value>
        string CssClass { get; set; }

        /// <summary>
        /// Gets the view model of the current model that will be displayed by the view.
        /// </summary>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
        FeedViewModel GetViewModel();
    }
}
