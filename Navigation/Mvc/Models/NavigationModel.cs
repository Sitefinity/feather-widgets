using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Web.UI.NavigationControls;

namespace Navigation.Mvc.Models
{
    /// <summary>
    /// This class represents model used for Navigation widget.
    /// </summary>
    public class NavigationModel : INavigationModel
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="NavigationModel"/> class.
        /// </summary>
        public NavigationModel()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NavigationModel"/> class.
        /// </summary>
        /// <param name="selectionMode">The selection mode.</param>
        /// <param name="levelsToInclude">The levels to include.</param>
        /// <param name="showParentPage">if set to <c>true</c> adds parent page.</param>
        public NavigationModel(PageSelectionMode selectionMode, int? levelsToInclude, bool showParentPage, string cssClass)
        {
            this.SelectionMode = selectionMode;
            this.LevelsToInclude = levelsToInclude;
            this.ShowParentPage = showParentPage;
            this.CssClass = cssClass;

            this.InitializeNavigationWidgetSettings();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the CSS class that will be applied on the wrapper div of the NavigationWidget (if such is presented).
        /// </summary>
        /// <value>
        /// The CSS class.
        /// </value>
        public string CssClass
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets whether to show parent page
        /// </summary>
        public bool ShowParentPage
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the levels to include.
        /// </summary>
        public virtual int? LevelsToInclude
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the page links to display selection mode.
        /// </summary>
        /// <value>The page display mode.</value>
        public PageSelectionMode SelectionMode
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the name of the site map provider.
        /// </summary>
        /// <value>The name of the site map provider.</value>
        public string SiteMapProviderName
        {
            get
            {
                return this.siteMapProviderName;
            }
            set
            {
                this.siteMapProviderName = value;
            }
        }

        /// <summary>
        /// Gets the site map.
        /// </summary>
        /// <value>
        /// The site map.
        /// </value>
        public virtual SiteMapBase SiteMap
        {
            get
            {
                return (SiteMapBase)SitefinitySiteMap.GetCurrentProvider();
            }
        }

        /// <summary>
        /// Gets the current site map node.
        /// </summary>
        /// <value>
        /// The current site map node.
        /// </value>
        public virtual SiteMapNode CurrentSiteMapNode
        {
            get 
            {
                return this.SiteMap.CurrentNode;
            }
        }

        /// <summary>
        /// Gets or sets the list of site map nodes that will be displayed in the navigation widget.
        /// </summary>
        /// <value>
        /// The nodes.
        /// </value>
        public IList<NodeViewModel> Nodes 
        {
            get 
            {
                if (this.nodes == null)
                    nodes = new List<NodeViewModel>();

                return nodes;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Initializes the settings for the navigation widget.
        /// </summary>
        private void InitializeNavigationWidgetSettings()
        {
            var siteMapProvider = this.GetProvider();

            switch (this.SelectionMode)
            {
                case PageSelectionMode.TopLevelPages:
                    this.AddChildNodes(siteMapProvider.RootNode, false);
                    break;
                case PageSelectionMode.CurrentPageChildren:

                    if (this.CurrentSiteMapNode != null)
                        this.AddChildNodes(this.CurrentSiteMapNode, this.ShowParentPage);

                    break;
                case PageSelectionMode.CurrentPageSiblings:
                    if (this.CurrentSiteMapNode != null)
                    {
                        var parentNodeTemp = this.CurrentSiteMapNode.ParentNode;

                        if (parentNodeTemp != null)
                            this.AddChildNodes(parentNodeTemp, this.ShowParentPage);
                    }

                    break;
            }
        }

        /// <summary>
        /// Adds the child nodes to the <see cref="Nodes"/> collection.
        /// </summary>
        /// <param name="startNode">The start node.</param>
        /// <param name="addParentNode">if set to <c>true</c> adds parent node.</param>
        /// <param name="levelsToInclude">The levels to include.</param>
        protected void AddChildNodes(SiteMapNode startNode, bool addParentNode)
        {
            if (this.LevelsToInclude != 0)
            {
                if (addParentNode && this.CheckSiteMapNode(startNode)
                    && startNode.Key != this.GetRootNodeId().ToString().ToUpperInvariant())
                {
                    var nodeViewModel = this.CreateNodeViewModelRecursive(startNode, this.LevelsToInclude);

                    if(nodeViewModel!=null)
                        this.Nodes.Add(nodeViewModel);
                }
                else
                {
                    var directChildren = startNode.ChildNodes;

                    foreach (SiteMapNode childNode in directChildren)
                    {
                        var nodeViewModel = this.CreateNodeViewModelRecursive(childNode, this.LevelsToInclude);

                        if (nodeViewModel != null)
                            this.Nodes.Add(nodeViewModel);
                    }
                }
            }
        }

        /// <summary>
        /// Creates the <see cref="NodeViewModel"/> from the SiteMapNode and populates recursive their child nodes.
        /// </summary>
        /// <param name="node">The original site map node.</param>
        /// <param name="levelsToInclude">The levels to include.</param>
        /// <returns></returns>
        private NodeViewModel CreateNodeViewModelRecursive(SiteMapNode node, int? levelsToInclude)
        {
            if (levelsToInclude != 0 && this.CheckSiteMapNode(node))
            {
                var isSelectedPage = this.CurrentSiteMapNode != null && this.CurrentSiteMapNode.Key == node.Key;
                var url = this.ResolveUrl(node);
                var target = this.GetLinkTarget(node);
                var nodeViewModel = new NodeViewModel(node, url, target,
                    isSelectedPage, this.HasSelectedChild(node));
                levelsToInclude--;

                var directChildren = node.ChildNodes;
                foreach (SiteMapNode childNode in directChildren)
                {
                    var childViewModel = this.CreateNodeViewModelRecursive(childNode, levelsToInclude);
                    if (childViewModel != null)
                        nodeViewModel.ChildNodes.Add(childViewModel);
                }

                return nodeViewModel;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Gets the root node identifier.
        /// </summary>
        /// <returns></returns>
        protected virtual Guid GetRootNodeId()
        {
            return SiteInitializer.CurrentFrontendRootNodeId;
        }

        /// <summary>
        /// Checks the site map node.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <returns></returns>
        protected virtual bool CheckSiteMapNode(SiteMapNode node)
        {
            return RouteHelper.CheckSiteMapNode(node);
        }

        /// <summary>
        /// Resolves the URL.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <returns></returns>
        protected virtual string ResolveUrl(SiteMapNode node)
        {
            return NavigationUtilities.ResolveUrl(node);
        }

        /// <summary>
        /// Gets the link target.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <returns></returns>
        protected virtual string GetLinkTarget(SiteMapNode node)
        {
            return NavigationUtilities.GetLinkTarget(node);
        }

        /// <summary>
        /// Determines whether the current node is descendant of the <see cref="SiteMapNode"/> instance.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <returns></returns>
        private bool HasSelectedChild(SiteMapNode node)
        {
            return this.CurrentSiteMapNode != null && this.CurrentSiteMapNode.IsDescendantOf(node);
        }

        /// <summary>
        /// Gets the sitemap provider.
        /// </summary>
        internal virtual SiteMapProvider GetProvider()
        {
            if (this.provider == null)
            {
                try
                {
                    this.provider = SiteMapBase.GetSiteMapProvider(this.SiteMapProviderName);
                }
                catch (Exception)
                {
                    this.provider = null;
                }
            }
            return this.provider;
        }

        #endregion

        #region Private fields and constants

        private string siteMapProviderName = SiteMapBase.DefaultSiteMapProviderName;
        private SiteMapProvider provider;
        private SiteMapBase siteMap;
        private IList<NodeViewModel> nodes;

        #endregion
    }
}
