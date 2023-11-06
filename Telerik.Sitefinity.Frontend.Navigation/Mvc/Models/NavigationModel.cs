using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading;
using System.Web;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Frontend.Mvc.Models;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.RelatedData;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Web.UI.ContentUI.Contracts;
using Telerik.Sitefinity.Web.UI.NavigationControls;

namespace Telerik.Sitefinity.Frontend.Navigation.Mvc.Models
{
    /// <summary>
    ///     This class represents model used for Navigation widget.
    /// </summary>
    public class NavigationModel : NavigationModelBase<NodeViewModel>, INavigationModel
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="NavigationModel" /> class.
        /// </summary>
        public NavigationModel() : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NavigationModel"/> class.
        /// </summary>
        /// <param name="selectionMode">The selection mode.</param>
        /// <param name="selectedPageId">The selected page identifier.</param>
        /// <param name="selectedPages">The selected pages.</param>
        /// <param name="levelsToInclude">The levels to include.</param>
        /// <param name="showParentPage">if set to <c>true</c> [show parent page].</param>
        /// <param name="cssClass">The CSS class.</param>
        /// <param name="openExternalPageInNewTab">if set to <c>true</c> [open external page in new tab].</param>
        public NavigationModel(
            PageSelectionMode selectionMode,
            Guid selectedPageId,
            SelectedPageModel[] selectedPages,
            int? levelsToInclude,
            bool showParentPage,
            string cssClass,
            bool openExternalPageInNewTab)
        {
            this.SelectionMode = selectionMode;
            this.SelectionModeString = selectionMode.ToString();
            this.LevelsToInclude = levelsToInclude;
            this.ShowParentPage = showParentPage;
            this.CssClass = cssClass;
            this.SelectedPageId = selectedPageId;
            this.SelectedPages = selectedPages;
            this.OpenExternalPageInNewTab = openExternalPageInNewTab;
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets or sets the page links to display selection mode.
        /// </summary>
        /// <value>The page display mode.</value>
        [Browsable(false)]
        public PageSelectionMode SelectionMode
        {
            get
            {
                return this.selectionMode;
            }

            set
            {
                this.selectionMode = value;
                this.SelectionModeString = value.ToString();
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Creates a blank instance of a list view model.
        /// </summary>
        /// <returns>The list view model.</returns>
        protected virtual ContentListViewModel CreateListViewModelInstance()
        {
            return new ContentListViewModel();
        }

        /// <summary>subs
        /// Instantiates a node view model.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <returns>An instance of a node view model.</returns>
        protected override NodeViewModel InstantiateNodeViewModel(SiteMapNode node)
        {
            bool isSelectedPage = this.CurrentSiteMapNode != null && this.CurrentSiteMapNode.Key == node.Key;
            string url = this.ResolveUrl(node);
            string target = this.GetLinkTarget(node);
            return new NodeViewModel(node, url, target, isSelectedPage, this.HasSelectedChild(node));
        }

        /// <summary>
        /// Instantiates a node view model.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="target">The target.</param>
        /// <returns>An instance of a node view model.</returns>
        [SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings", MessageId = "0#")]
        protected override NodeViewModel InstantiateNodeViewModel(string url, string target)
        {
            return new NodeViewModel(null, url, target, false, false);
        }

        #endregion

        #region Private Fields

        private static readonly Type CacheDependencyPageNodeObjectType = Type.GetType("Telerik.Sitefinity.Pages.Model.CacheDependencyPageNodeObject, Telerik.Sitefinity.Model");
        private static readonly Type CacheDependencyObjectForAllSitesType = Type.GetType("Telerik.Sitefinity.Pages.Model.CacheDependencyObjectForAllSites, Telerik.Sitefinity.Model");
        private static readonly Type CacheDependencyPageNodeStateChangeType = Type.GetType("Telerik.Sitefinity.Pages.Model.CacheDependencyPageNodeStateChange, Telerik.Sitefinity.Model");
        private PageSelectionMode selectionMode;

        #endregion
    }
}