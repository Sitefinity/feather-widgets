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
            this.InitializeSiteMapDataSource();
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

            this.InitializeSiteMapDataSource();
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
        public SiteMapBase SiteMap
        {
            get
            {
                return (SiteMapBase)SitefinitySiteMap.GetCurrentProvider();
            }
        }

        /// <summary>
        /// Gets or sets the list of site map nodes that will be displayed in the navigation widget.
        /// </summary>
        /// <value>
        /// The nodes.
        /// </value>
        public IList<NodeViewModel> Nodes { private set; get; }

        #endregion

        #region Methods

        /// <summary>
        /// Initializes the site map data source.
        /// </summary>
        private void InitializeSiteMapDataSource()
        {
            this.Nodes = new List<NodeViewModel>() { };

            var siteMapProvider = this.GetProvider();
            var currentNode = this.SiteMap.CurrentNode;

            switch (this.SelectionMode)
            {
                case PageSelectionMode.TopLevelPages:
                    this.AddChildNodes(siteMapProvider.RootNode, false, this.LevelsToInclude);
                    break;
                case PageSelectionMode.CurrentPageChildren:

                    if (currentNode != null)
                        this.AddChildNodes(currentNode, this.ShowParentPage, this.LevelsToInclude);

                    break;
                case PageSelectionMode.CurrentPageSiblings:
                    if (currentNode != null)
                    {
                        var parentNodeTemp = currentNode.ParentNode;

                        if (parentNodeTemp != null)
                            this.AddChildNodes(parentNodeTemp, this.ShowParentPage, this.LevelsToInclude);
                    }

                    break;
            }
        }

        /// <summary>
        /// Adds the child nodes recursive.
        /// </summary>
        /// <param name="currentNode">The current node.</param>
        /// <param name="addParentNode">if set to <c>true</c> adds parent node.</param>
        private void AddChildNodes(SiteMapNode currentNode, bool addParentNode, int? levelsToInclude)
        {
            if (levelsToInclude != 0)
            {
                if (addParentNode && RouteHelper.CheckSiteMapNode(currentNode)
                    && currentNode.Key != SiteInitializer.CurrentFrontendRootNodeId.ToString().ToUpperInvariant())
                {
                    var nodeViewModel = this.CreateNodeViewModelRecursive(currentNode, levelsToInclude);

                    if(nodeViewModel!=null)
                        this.Nodes.Add(nodeViewModel);
                }
                else
                {
                    var directChildren = currentNode.ChildNodes;

                    foreach (SiteMapNode childNode in directChildren)
                    {
                        var nodeViewModel = this.CreateNodeViewModelRecursive(childNode, levelsToInclude);

                        if (nodeViewModel != null)
                            this.Nodes.Add(nodeViewModel);
                    }
                }
            }
        }


        private NodeViewModel CreateNodeViewModelRecursive(SiteMapNode currentSiteMapNode, int? levelsToInclude)
        {
            if (levelsToInclude != 0 && RouteHelper.CheckSiteMapNode(currentSiteMapNode))
            {
                var isSelectedPage = this.SiteMap.CurrentNode.Key == currentSiteMapNode.Key;
                var nodeViewModel = new NodeViewModel(currentSiteMapNode.Title, NavigationUtilities.ResolveUrl(currentSiteMapNode), isSelectedPage);
                levelsToInclude--;

                var directChildren = currentSiteMapNode.ChildNodes;
                foreach (SiteMapNode childNode in directChildren)
                {
                    var childViewModel = this.CreateNodeViewModelRecursive(childNode, levelsToInclude);
                    if (childViewModel != null)
                        nodeViewModel.ChildNodes.Add(childViewModel);
                }

                return nodeViewModel;
            }

            return null;
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

        #endregion
    }
}
