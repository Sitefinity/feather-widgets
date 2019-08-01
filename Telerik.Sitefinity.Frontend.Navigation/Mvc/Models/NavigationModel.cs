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
    public class NavigationModel : IHasCacheDependency, INavigationModel
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="NavigationModel" /> class.
        /// </summary>
        public NavigationModel()
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
            this.LevelsToInclude = levelsToInclude;
            this.ShowParentPage = showParentPage;
            this.CssClass = cssClass;
            this.selectedPageId = selectedPageId;
            this.selectedPages = selectedPages;
            this.OpenExternalPageInNewTab = openExternalPageInNewTab;

            this.InitializeNavigationWidgetSettings();
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets or sets the CSS class that will be applied on the wrapper div of the NavigationWidget (if such is presented).
        /// </summary>
        /// <value>
        ///     The CSS class.
        /// </value>
        public string CssClass { get; set; }

        /// <summary>
        ///     Gets the current site map node.
        /// </summary>
        /// <value>
        ///     The current site map node.
        /// </value>
        [Browsable(false)]
        public virtual SiteMapNode CurrentSiteMapNode
        {
            get
            {
                return this.SiteMap.CurrentNode;
            }
        }

        /// <summary>
        ///     Gets or sets the levels to include.
        /// </summary>
        [Browsable(false)]
        public virtual int? LevelsToInclude { get; set; }

        /// <summary>
        /// Gets the list of site map nodes that will be displayed in the navigation widget.
        /// </summary>
        /// <value>
        /// The nodes.
        /// </value>
        [Browsable(false)]
        public IList<NodeViewModel> Nodes
        {
            get
            {
                return this.nodes ?? (this.nodes = new List<NodeViewModel>());
            }
        }

        /// <summary>
        ///     Gets or sets the page links to display selection mode.
        /// </summary>
        /// <value>The page display mode.</value>
        [Browsable(false)]
        public PageSelectionMode SelectionMode { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [show parent page].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [show parent page]; otherwise, <c>false</c>.
        /// </value>
        [Browsable(false)]
        public bool ShowParentPage { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether should open external page in new tab.
        /// </summary>
        /// <value>
        /// <c>true</c> if should open external page in new tab; otherwise, <c>false</c>.
        /// </value>
        [Browsable(false)]
        public bool OpenExternalPageInNewTab { get; set; }

        /// <summary>
        ///     Gets the site map.
        /// </summary>
        /// <value>
        ///     The site map.
        /// </value>
        [Browsable(false)]
        public virtual SiteMapBase SiteMap
        {
            get
            {
                return (SiteMapBase)SitefinitySiteMap.GetCurrentProvider();
            }
        }

        /// <summary>
        ///     Gets or sets the name of the site map provider.
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

        /// <summary>
        /// Gets a collection of cached and changed items that need to be invalidated for the specific views that display all types inheriting from
        /// the abstract type <see cref="Telerik.Sitefinity.GenericContent.Model.Content"/>.
        /// </summary>
        /// <returns></returns>
        public virtual IList<CacheDependencyKey> GetCacheDependencyObjects()
        {
            var cacheDependencyNotifiedObjects = new List<CacheDependencyKey>();

            foreach (var nodeId in this.viewModelNodeIds)
            {
                cacheDependencyNotifiedObjects.Add(new CacheDependencyKey() { Type = CacheDependencyObjectForAllSitesType, Key = nodeId });

                var multilingualKey = nodeId;
                if (AppSettings.CurrentSettings.Multilingual)
                    multilingualKey += Thread.CurrentThread.CurrentUICulture;
                
                cacheDependencyNotifiedObjects.Add(new CacheDependencyKey() { Type = CacheDependencyPageNodeStateChangeType, Key = multilingualKey });
                cacheDependencyNotifiedObjects.Add(new CacheDependencyKey() { Type = CacheDependencyPageNodeObjectType, Key = multilingualKey });
            }

            this.SubscribeCacheDependency(cacheDependencyNotifiedObjects);

            return cacheDependencyNotifiedObjects;
        }

        /// <summary>
        ///     Gets the sitemap provider.
        /// </summary>
        /// <returns>
        ///     The <see cref="SiteMapProvider" />.
        /// </returns>
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

                    /// TODO: handle exception
                    throw;
                }
            }

            return this.provider;
        }

        /// <summary>
        /// Adds the child nodes to the <see cref="Nodes"/> collection.
        /// </summary>
        /// <param name="startNode">
        /// The start node.
        /// </param>
        /// <param name="addParentNode">
        /// if set to <c>true</c> adds parent node.
        /// </param>
        protected void AddChildNodes(SiteMapNode startNode, bool addParentNode)
        {
            this.viewModelNodeIds.Add(startNode.Key);
            if (startNode is PageSiteNode)
                this.pageSiteNodes.Add((PageSiteNode)startNode);

            foreach (SiteMapNode node in startNode.GetAllNodes())
            {
                var pageNode = node as PageSiteNode;
                if (pageNode != null && pageNode.NodeType == NodeType.Group)
                {
                    this.viewModelNodeIds.Add(pageNode.Key);
                    this.pageSiteNodes.Add(pageNode);
                }
            }

            if (this.LevelsToInclude != 0 && startNode != null)
            {
                if (addParentNode && this.CheckSiteMapNode(startNode) && startNode.Key != this.RootNodeId.ToString().ToUpperInvariant())
                {
                    var nodeViewModel = this.CreateNodeViewModelRecursive(startNode, this.LevelsToInclude);

                    if (nodeViewModel != null)
                    {
                        this.Nodes.Add(nodeViewModel);
                    }
                }
                else
                {
                    var directChildren = startNode.ChildNodes;

                    foreach (SiteMapNode childNode in directChildren)
                    {
                        var nodeViewModel = this.CreateNodeViewModelRecursive(childNode, this.LevelsToInclude);

                        if (nodeViewModel != null)
                        {
                            this.Nodes.Add(nodeViewModel);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Checks the site map node.
        /// </summary>
        /// <param name="node">
        /// The node.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        protected virtual bool CheckSiteMapNode(SiteMapNode node)
        {
            return RouteHelper.CheckSiteMapNode(node);
        }

        /// <summary>
        /// Gets the link target.
        /// </summary>
        /// <param name="node">
        /// The node.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        protected virtual string GetLinkTarget(SiteMapNode node)
        {
            string target = NavigationUtilities.GetLinkTarget(node);

            if (target.IsNullOrEmpty())
            {
                target = "_self";
            }

            return target;
        }

        /// <summary>
        ///     Gets the root node identifier.
        /// </summary>
        /// <returns>
        ///     The <see cref="Guid" />.
        /// </returns>
        protected virtual Guid RootNodeId
        {
            get
            {
                return SiteInitializer.CurrentFrontendRootNodeId;
            }
        }

        /// <summary>
        /// Resolves the URL.
        /// </summary>
        /// <param name="node">
        /// The node.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        protected virtual string ResolveUrl(SiteMapNode node)
        {
            return NavigationUtilities.ResolveUrl(node);
        }

        /// <summary>
        /// Instantiates a node view model.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <returns>An instance of a node view model.</returns>
        protected virtual NodeViewModel InstantiateNodeViewModel(SiteMapNode node)
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
        protected virtual NodeViewModel InstantiateNodeViewModel(string url, string target)
        {
            return new NodeViewModel(null, url, target, false, false);
        }
        
        private void SubscribeCacheDependency(List<CacheDependencyKey> objects)
        {
            if (!SystemManager.CurrentHttpContext.Items.Contains(PageCacheDependencyKeys.PageNodes))
                SystemManager.CurrentHttpContext.Items.Add(PageCacheDependencyKeys.PageNodes, new List<CacheDependencyKey>());

            ((List<CacheDependencyKey>)SystemManager.CurrentHttpContext.Items[PageCacheDependencyKeys.PageNodes]).AddRange(objects);
        }

        /// <summary>
        /// Creates the <see cref="NodeViewModel"/> from the SiteMapNode and populates recursive their child nodes.
        /// </summary>
        /// <param name="node">
        /// The original site map node.
        /// </param>
        /// <param name="levelsToInclude">
        /// The levels to include.
        /// </param>
        /// <returns>
        /// The <see cref="NodeViewModel"/>.
        /// </returns>
        private NodeViewModel CreateNodeViewModelRecursive(SiteMapNode node, int? levelsToInclude)
        {
            if (levelsToInclude != 0 && this.CheckSiteMapNode(node))
            {
                this.viewModelNodeIds.Add(node.Key);
                if (node is PageSiteNode)
                    this.pageSiteNodes.Add((PageSiteNode)node);

                var nodeViewModel = this.InstantiateNodeViewModel(node);
                levelsToInclude--;

                SiteMapNodeCollection directChildren = node.ChildNodes;
                foreach (SiteMapNode childNode in directChildren)
                {
                    NodeViewModel childViewModel = this.CreateNodeViewModelRecursive(childNode, levelsToInclude);
                    if (childViewModel != null)
                    {
                        nodeViewModel.ChildNodes.Add(childViewModel);
                    }
                }

                return nodeViewModel;
            }

            return null;
        }

        /// <summary>
        /// Determines whether the current node is descendant of the <see cref="SiteMapNode"/> instance.
        /// </summary>
        /// <param name="node">
        /// The node.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        private bool HasSelectedChild(SiteMapNode node)
        {
            return this.CurrentSiteMapNode != null && this.CurrentSiteMapNode.IsDescendantOf(node);
        }

        /// <summary>
        ///     Initializes the settings for the navigation widget.
        /// </summary>
        private void InitializeNavigationWidgetSettings()
        {
            SiteMapProvider siteMapProvider = this.GetProvider();

            switch (this.SelectionMode)
            {
                case PageSelectionMode.TopLevelPages:
                    this.AddChildNodes(siteMapProvider.RootNode, false);
                    break;
                case PageSelectionMode.SelectedPageChildren:
                    if (!Guid.Equals(this.selectedPageId, Guid.Empty))
                    {
                        var siteMapNodeFromKey = siteMapProvider.FindSiteMapNodeFromKey(this.selectedPageId.ToString("D"));
                        this.AddChildNodes(siteMapNodeFromKey, this.ShowParentPage);
                    }

                    break;
                case PageSelectionMode.CurrentPageChildren:

                    if (this.CurrentSiteMapNode != null)
                    {
                        this.AddChildNodes(this.CurrentSiteMapNode, this.ShowParentPage);
                    }

                    break;
                case PageSelectionMode.CurrentPageSiblings:
                    if (this.CurrentSiteMapNode != null)
                    {
                        SiteMapNode parentNodeTemp = this.CurrentSiteMapNode.ParentNode;

                        if (parentNodeTemp != null)
                        {
                            this.AddChildNodes(parentNodeTemp, this.ShowParentPage);
                        }
                    }

                    break;
                case PageSelectionMode.SelectedPages:
                    if (this.selectedPages != null)
                    {
                        var target = this.OpenExternalPageInNewTab ? "_blank" : "_self";
                        foreach (var page in this.selectedPages)
                        {
                            var isExternalSiteMapNode = page.NodeType == NodeType.Rewriting || page.NodeType == NodeType.InnerRedirect || page.NodeType == NodeType.OuterRedirect;
                            if (page.Id != default(Guid) && (!page.IsExternal || isExternalSiteMapNode))
                            {
                                var siteMapNode = siteMapProvider.FindSiteMapNodeFromKey(page.Id.ToString("D"));
                                if (siteMapNode != null && this.CheckSiteMapNode(siteMapNode))
                                {
                                    this.viewModelNodeIds.Add(siteMapNode.Key);
                                    if (siteMapNode is PageSiteNode)
                                        this.pageSiteNodes.Add((PageSiteNode)siteMapNode);

                                    var siteMapHierarchy = this.CreateNodeViewModelRecursive(siteMapNode, this.LevelsToInclude);
                                    this.Nodes.Add(siteMapHierarchy);
                                }
                            }
                            else
                            {
                                var node = this.InstantiateNodeViewModel(page.Url, target);
                                node.Title = page.TitlesPath;
                                this.Nodes.Add(node);
                            }
                        }
                    }

                    break;
            }

            this.pageSiteNodes.Select(pn => pn.RelatedDataHolder).SetRelatedDataSourceContext();
        }

        #endregion

        #region Private Fields

        private static readonly Type CacheDependencyPageNodeObjectType = Type.GetType("Telerik.Sitefinity.Pages.Model.CacheDependencyPageNodeObject, Telerik.Sitefinity.Model");
        private static readonly Type CacheDependencyObjectForAllSitesType = Type.GetType("Telerik.Sitefinity.Pages.Model.CacheDependencyObjectForAllSites, Telerik.Sitefinity.Model");
        private static readonly Type CacheDependencyPageNodeStateChangeType = Type.GetType("Telerik.Sitefinity.Pages.Model.CacheDependencyPageNodeStateChange, Telerik.Sitefinity.Model");

        private HashSet<string> viewModelNodeIds = new HashSet<string>();
        private HashSet<PageSiteNode> pageSiteNodes = new HashSet<PageSiteNode>();

        private IList<NodeViewModel> nodes;

        /// <summary>
        /// The provider
        /// </summary>
        private SiteMapProvider provider;

        // TODO: check why field is never used
        // private SiteMapBase siteMap;
        private string siteMapProviderName = SiteMapBase.DefaultSiteMapProviderName;

        private Guid selectedPageId;
        private SelectedPageModel[] selectedPages;
              
        #endregion
    }
}