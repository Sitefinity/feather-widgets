using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web;
using Telerik.Sitefinity.Web;

namespace Telerik.Sitefinity.Frontend.Navigation.Mvc.Models
{
    /// <summary>
    /// Classes that implement this interface could be used as model for the Navigation widget.
    /// </summary>
    public interface INavigationModel
    {
        /// <summary>
        /// Gets the list of site map nodes that will be displayed in the navigation widget.
        /// </summary>
        /// <value>
        /// The nodes.
        /// </value>
        [Browsable(false)]
        IList<NodeViewModel> Nodes { get; }

        /// <summary>
        /// Gets or sets the CSS class that will be applied on the wrapper div of the NavigationWidget (if such is presented).
        /// </summary>
        /// <value>
        /// The CSS class.
        /// </value>
        [Browsable(false)]
        string CssClass { get; set; }

        /// <summary>
        /// Gets the current site map node.
        /// </summary>
        /// <value>
        /// The current site map node.
        /// </value>
        [Browsable(false)]
        SiteMapNode CurrentSiteMapNode { get; }

        /// <summary>
        ///     Gets or sets the page links to display selection mode.
        /// </summary>
        /// <value>The page display mode.</value>
        [Browsable(false)]
        PageSelectionMode SelectionMode { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [show parent page].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [show parent page]; otherwise, <c>false</c>.
        /// </value>
        [Browsable(false)]
        bool ShowParentPage { get; set; }

        /// <summary>
        /// Gets or sets a serialized array of the selected pages.
        /// </summary>
        /// <value>
        /// The a serialized array of selected pages.
        /// </value>
        [Browsable(false)]
        SelectedPageModelBase[] SelectedPages { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the page that is selected if SelectionMode is SelectedPageChildren.
        /// </summary>
        /// <value>The identifier of the page that is selected if SelectionMode is SelectedPageChildren.</value>
        [Browsable(false)]
        Guid SelectedPageId { get; set; }

        /// <summary>
        /// Initializes widget settings.
        /// </summary>
        [Browsable(false)]
        void InitializeNavigationWidgetSettings();

        /// <summary>
        ///     Gets or sets the levels to include.
        /// </summary>
        [Browsable(false)]
        int? LevelsToInclude { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether should open external page in new tab.
        /// </summary>
        /// <value>
        /// <c>true</c> if should open external page in new tab; otherwise, <c>false</c>.
        /// </value>
        [Browsable(false)]
        bool OpenExternalPageInNewTab { get; set; }
    }
}
