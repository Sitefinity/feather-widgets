﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using Telerik.Sitefinity.DynamicModules.Model;
using Telerik.Sitefinity.Frontend.Mvc.Helpers;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Mvc;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Web.UI.NavigationControls;

namespace Telerik.Sitefinity.Frontend.Navigation.Mvc.Models.Breadcrumb
{
    /// <summary>
    ///     This class represents model used for Breadcrumb widget.
    /// </summary>
    public class BreadcrumbModel : IBreadcrumbModel
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets the show home page link.
        /// </summary>
        /// <value>The show home page link.</value>
        public bool ShowHomePageLink
        {
            get
            {
                return this.showHomePageLink;
            }

            set
            {
                this.showHomePageLink = value;
            }
        }

        /// <summary>
        /// Gets or sets the breadcrumb include option.
        /// </summary>
        /// <value>The breadcrumb include option.</value>
        public BreadcrumbIncludeOption BreadcrumbIncludeOption { get; set; }

        /// <summary>
        /// Gets or sets the show current page in the end.
        /// </summary>
        /// <value>The show current page in the end.</value>
        public bool ShowCurrentPageInTheEnd
        {
            get
            {
                return this.showCurrentPageInTheEnd;
            }

            set
            {
                this.showCurrentPageInTheEnd = value;
            }
        }

        /// <summary>
        /// Gets or sets the show group pages.
        /// </summary>
        /// <value>The show group pages.</value>
        public bool ShowGroupPages { get; set; }

        /// <summary>
        /// Gets or sets the CSS class that will be applied on the wrapper div of the BreadcrumbWidget (if such is presented).
        /// </summary>
        /// <value>The CSS class.</value>
        public string CssClass { get; set; }

        /// <summary>
        /// Gets or sets the strting page id from which to start breadcrumb path.
        /// </summary>
        /// <value>The strting page id.</value>
        public Guid StartingPageId { get; set; }

        /// <summary>
        /// Gets or sets the name of the site map provider.
        /// </summary>
        /// <value>The name of the site map provider.</value>
        public string SiteMapProviderName { get; set; }

        /// <summary>
        /// Gets or sets the allow virtual nodes.
        /// For example, the last control on the page,
        /// which registers a breadcrumb extender with Telerik.Sitefinity.Web.RegisterBreadcrumbExtender(this Page page, IBreadcrumExtender extender) extension method of the page,
        /// will be applied.
        /// </summary>
        /// <value>The allow virtual nodes.</value>
        public bool AllowVirtualNodes { get; set; }
        #endregion

        /// <summary>
        /// Creates the view model.
        /// </summary>
        /// <param name="extender">The object used for retrieving the breadcrump datasources.</param>
        /// <returns></returns>
        public virtual BreadcrumbViewModel CreateViewModel(IBreadcrumExtender extender)
        {
            Tuple<bool, List<SiteMapNode>> result = this.GetBreadcrumbDataSource();

            if (this.AllowVirtualNodes)
            {
                result.Item2.AddRange(this.GetVirtualNodes(extender));
                var virtualNodes = this.GetMvcDetailWidgetVirtualNodes();
                if (virtualNodes != null)
                {
                    virtualNodes = virtualNodes.Where(n => !result.Item2.Contains(n));
                    result.Item2.AddRange(virtualNodes);
                }
            }      

            return new BreadcrumbViewModel(result.Item2)
            {
                ShowCurrentPageInTheEnd = this.ShowCurrentPageInTheEnd,
                IsTemplateRendered = !result.Item1,
                CssClass = this.CssClass
            };
        }

        /// <summary>
        /// Gets the virtual nodes for the controllers that have detail action method.
        /// </summary>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
        public virtual IEnumerable<SiteMapNode> GetMvcDetailWidgetVirtualNodes()
        {
            var nodes = new List<SiteMapNode>();

            var page = HttpContext.Current.Handler as System.Web.UI.Page;
            if (page != null)
            {
                var routeParams = MvcRequestContextBuilder.GetRouteParams(page.GetRequestContext());
                if (routeParams == null || routeParams.Length == 0)
                    return null;

                var mvcProxyControls = GetControlsRecusrvive<Telerik.Sitefinity.Mvc.Proxy.MvcControllerProxy>(page);
                if (mvcProxyControls.Any())
                {
                    var contentItemResolver = new ContentDataItemResolver();
                    foreach (var mvcProxy in mvcProxyControls)
                    {
                        var dataItem = contentItemResolver.GetItemByController(mvcProxy.Controller, routeParams);
                        if (dataItem != null)
                        {
                            var content = dataItem as IContent;
                            var dynamicContent = dataItem as DynamicContent;

                            if (content != null)
                            {
                                if (!string.IsNullOrEmpty(content.Title))
                                {
                                    var siteMapNode = new SiteMapNode(
                                        this.provider, dataItem.Id.ToString(), string.Empty, content.Title, content.Description);
                                    nodes.Add(siteMapNode);
                                }
                            }
                            else if (dynamicContent != null)
                            {
                                nodes.AddRange(this.GetDynamicContentlVirtualNodes(dynamicContent));
                            }
                        }
                    }
                }
            }

            return nodes;
        }
     
        private static IList<T> GetControlsRecusrvive<T>(Control control) where T : Control
        {
            var rtn = new List<T>();
            foreach (Control item in control.Controls)
            {
                var ctr = item as T;
                if (ctr != null)
                {
                    rtn.Add(ctr);
                }
                else
                {
                    rtn.AddRange(GetControlsRecusrvive<T>(item));
                }
            }

            return rtn;
        }

        private IEnumerable<SiteMapNode> GetDynamicContentlVirtualNodes(DynamicContent dataItem)
        {
            List<SiteMapNode> list = new List<SiteMapNode>();
            var currentParentItem = dataItem.SystemParentItem;
            while (currentParentItem != null)
            {
                var page = HttpContext.Current.Handler as System.Web.UI.Page;
                var url = page.Request.RawUrl;
                var indexOfCurrentUrl = url.IndexOf(currentParentItem.ItemDefaultUrl, StringComparison.OrdinalIgnoreCase);
                if (indexOfCurrentUrl > -1)
                {
                    var node = new SiteMapNode(
                    this.provider, currentParentItem.Id.ToString(), url.Substring(0, indexOfCurrentUrl) + currentParentItem.ItemDefaultUrl, ((IHasTitle)currentParentItem).GetTitle(), string.Empty);
                    list.Insert(0, node);
                }

                currentParentItem = currentParentItem.SystemParentItem;
            }

            var siteMapNode = new SiteMapNode(
                    this.provider, dataItem.Id.ToString(), string.Empty, ((IHasTitle)dataItem).GetTitle(), string.Empty);
            list.Add(siteMapNode);

            return list;
        }

        /// <summary>
        /// Gets the virtual nodes.
        /// </summary>
        /// <param name="extender">The extender.</param>
        /// <returns></returns>
        private IEnumerable<SiteMapNode> GetVirtualNodes(IBreadcrumExtender extender)
        {
            if (extender == null)
            {
                return Enumerable.Empty<SiteMapNode>();
            }

            return extender.GetVirtualNodes(this.SiteMapProvider);
        }

        private Tuple<bool, List<SiteMapNode>> GetBreadcrumbDataSource()
        {
            var breadcrumbDataSource = new List<SiteMapNode>();

            bool hasCurrentNode = false;

            if (this.SiteMapProvider == null)
            {
                return new Tuple<bool, List<SiteMapNode>>(hasCurrentNode, breadcrumbDataSource);
            }

            SiteMapNode currentNode = this.SiteMapProvider.CurrentNode;

            if (currentNode != null)
            {
                hasCurrentNode = true;

                var currentSite = SystemManager.CurrentContext.CurrentSite;
                var rootNodeKey = currentSite.SiteMapRootNodeId.ToString();

                string nodeKey = currentNode.Key;

                var homePageKey = currentSite.HomePageId;
                var homePageNode = homePageKey != Guid.Empty ? (PageSiteNode)this.SiteMapProvider.FindSiteMapNodeFromKey(homePageKey.ToString()) : null;

                //// Gets the actual home page when the site is set to multilingual and the home page is split
                homePageNode = (PageSiteNode)((SiteMapBase)this.SiteMapProvider).FindSiteMapNodeForSpecificLanguage(homePageNode, CultureInfo.CurrentUICulture) ?? homePageNode;

                var showToSelectedNode = this.SelectedPageNode != null && this.BreadcrumbIncludeOption != BreadcrumbIncludeOption.CurrentPageFullPath;
                var isSelectedPageNodeFound = false;

                while (!string.IsNullOrEmpty(nodeKey) &&
                        string.Compare(nodeKey, rootNodeKey, StringComparison.OrdinalIgnoreCase) != 0)
                {
                    var node = (PageSiteNode)((SiteMapBase)this.SiteMapProvider).FindSiteMapNodeFromKey(nodeKey, false);
                    if (node != homePageNode && node.IsAccessibleToUser(HttpContext.Current))
                    {
                        if (this.ShowGroupPages)
                            breadcrumbDataSource.Insert(0, node);
                        else if (!node.IsGroupPage)
                            breadcrumbDataSource.Insert(0, node);
                    }

                    if (showToSelectedNode && node == this.SelectedPageNode)
                    {
                        isSelectedPageNodeFound = true;
                        break;
                    }

                    nodeKey = node.ParentKey;
                }

                if (!isSelectedPageNodeFound && this.BreadcrumbIncludeOption != BreadcrumbIncludeOption.CurrentPageFullPath)
                    breadcrumbDataSource.Clear();
                else if (homePageNode != null)
                    breadcrumbDataSource.Insert(0, homePageNode);

                if (breadcrumbDataSource.Count > 0)
                {
                    //// remove the first element if we have to hide the home page
                    if (!this.ShowHomePageLink)
                        breadcrumbDataSource.RemoveAt(0);

                    //// remove the last element if we have to hide the last page
                    var index = breadcrumbDataSource.Count - 1;
                    if (!this.ShowCurrentPageInTheEnd && index > -1)
                        breadcrumbDataSource.RemoveAt(index);
                }
            }

            return new Tuple<bool, List<SiteMapNode>>(hasCurrentNode, breadcrumbDataSource);
        }

        /// <summary>
        /// Gets the selected page node.
        /// </summary>
        private PageSiteNode SelectedPageNode
        {
            get
            {
                if (this.StartingPageId != Guid.Empty)
                {
                    var node = ((SiteMapBase)this.SiteMapProvider).FindSiteMapNodeFromKey(this.StartingPageId.ToString(), false);
                    return (PageSiteNode)node;
                }

                return null;
            }
        }

        /// <summary>
        /// Gets the site map provider.
        /// </summary>
        /// <value>The site map provider.</value>
        private SiteMapProvider SiteMapProvider
        {
            get
            {
                if (this.provider == null)
                {
                    if (string.IsNullOrEmpty(this.SiteMapProviderName))
                        this.provider = SiteMapBase.GetSiteMapProvider(SiteMapBase.DefaultSiteMapProviderName);
                    else
                        this.provider = SiteMapBase.GetSiteMapProvider(this.SiteMapProviderName);
                }

                return this.provider;
            }
        }

        #region Private fields
        private bool showHomePageLink = true;
        private bool showCurrentPageInTheEnd = true;
        private SiteMapProvider provider;
        #endregion
    }
}
