using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using Telerik.OpenAccess.Metadata;
using Telerik.Sitefinity.DynamicModules.Model;
using Telerik.Sitefinity.Frontend.Mvc.Helpers;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Mvc;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web;

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

            var page = this.GetPageFromHandler();

            if (page != null)
            {
                var routeParams = MvcRequestContextBuilder.GetRouteParams(page.GetRequestContext());
                if (routeParams == null || routeParams.Length == 0)
                    return null;

                if (SystemManager.IsDetailsView(out object dataItem) && dataItem != null)
                {
                    if (dataItem is IContent content)
                    {
                        if (!string.IsNullOrEmpty(content.Title))
                        {
                            var siteMapNode = new SiteMapNode(
                                this.provider, content.Id.ToString(), string.Empty, content.Title, content.Description);
                            nodes.Add(siteMapNode);
                        }
                    }
                    else if (dataItem is DynamicContent dynamicContent)
                    {
                        nodes.AddRange(this.GetDynamicContentlVirtualNodes(dynamicContent));
                    }
                }
            }

            return nodes;
        }

        private IEnumerable<SiteMapNode> GetDynamicContentlVirtualNodes(DynamicContent dataItem)
        {
            List<SiteMapNode> list = new List<SiteMapNode>();
            var currentParentItem = dataItem.SystemParentItem;
            while (currentParentItem != null)
            {
                var page = this.GetPageFromHandler();
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

        [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCopPlus.StyleCopPlusRules", "SP2101:MethodMustNotContainMoreLinesThan")]
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
                breadcrumbDataSource = new BreadcrumbGenerator().Generate(new BreadcrumbGenerator.BreadcrumbInput()
                {
                    AddHomePageAtBeginning = this.ShowHomePageLink,
                    AddStartingPageAtEnd = this.ShowCurrentPageInTheEnd,
                    IncludeGroupPages = this.ShowGroupPages,
                    SiteMapProvider = (SiteMapBase)this.SiteMapProvider,
                    CurrentPageId = currentNode.Key,
                    StartingPageId = this.StartingPageId.ToString(),
                    ShowFullPath = this.BreadcrumbIncludeOption == BreadcrumbIncludeOption.CurrentPageFullPath
                });
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

        private System.Web.UI.Page GetPageFromHandler()
        {
            var page = HttpContext.Current.Handler as System.Web.UI.Page;

            if (page == null)
            {
                var pageHandlerWrapper = HttpContext.Current.Handler as IHandlerWrapper;
                if (pageHandlerWrapper != null)
                {
                    page = pageHandlerWrapper.OriginalHandler as System.Web.UI.Page;
                }
            }

            return page;
        }

        #region Private fields
        private bool showHomePageLink = true;
        private bool showCurrentPageInTheEnd = true;
        private SiteMapProvider provider;
        #endregion
    }
}
