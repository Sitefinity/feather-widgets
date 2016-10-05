namespace Telerik.Sitefinity.Frontend.SocialShare.Mvc.Models
{
    using System.Collections.Generic;

    /// <summary>
    /// Classes that implement this interface could be used as a model for Social Share widget.
    /// </summary>
    public interface ISocialShareModel
    {
        /// <summary>
        /// Gets the social buttons
        /// </summary>
        ICollection<SocialButtonModel> SocialButtons { get; }

        /// <summary>
        /// Gets or sets the CSS class that will be applied on the wrapper div of the widget.
        /// </summary>
        /// <value>
        /// The CSS class.
        /// </value>
        string CssClass { get; set; }

        /// <summary>
        /// Gets or sets the data item's title we wish to share
        /// </summary>
        /// <value>
        /// The shared item's Title
        /// </value>
        string ItemTitle { get; set; }

        /// <summary>
        /// Initialize the selected social share options.
        /// </summary>
        void InitializeSocialShareButtons(IList<SocialShareGroup> socialShareGroups);
    }
}