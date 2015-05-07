using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Telerik.Sitefinity.Frontend.Navigation.Mvc.Models.Breadcrumb
{
    /// <summary>
    ///     This class represents model used for Breadcrumb widget.
    /// </summary>
    public interface IBreadcrumbModel
    {
        /// <summary>
        /// Gets or sets the name of the site map provider.
        /// </summary>
        /// <value>The name of the site map provider.</value>
        string SiteMapProviderName { get; set; }

        /// <summary>
        /// Gets or sets the show home page link.
        /// </summary>
        /// <value>The show home page link.</value>
        bool ShowHomePageLink { get; set; }

        /// <summary>
        /// Gets or sets the breadcrumb include option.
        /// </summary>
        /// <value>The breadcrumb include option.</value>
        BreadcrumbIncludeOption BreadcrumbIncludeOption { get; set; }

        /// <summary>
        /// Gets or sets the show current page in the end.
        /// </summary>
        /// <value>The show current page in the end.</value>
        bool ShowCurrentPageInTheEnd { get; set; }

        /// <summary>
        /// Gets or sets the show group pages.
        /// </summary>
        /// <value>The show group pages.</value>
        bool ShowGroupPages { get; set; }

        /// <summary>
        /// Gets or sets the CSS class that will be applied on the wrapper div of the BreadcrumbWidget (if such is presented).
        /// </summary>
        /// <value>The CSS class.</value>
        string CssClass { get; set; }

        /// <summary>
        /// Gets or sets the strting page id from which to start breadcrumb path.
        /// </summary>
        /// <value>The strting page id.</value>
        Guid StartingPageId { get; set; }

        /// <summary>
        /// Gets or sets the allow virtual nodes.
        /// For example, the last control on the page,
        /// which registers a breadcrumb extender with Telerik.Sitefinity.Web.RegisterBreadcrumbExtender(this Page page, IBreadcrumExtender extender) extension method of the page,
        /// will be applied.
        /// </summary>
        /// <value>The allow virtual nodes.</value>
        bool AllowVirtualNodes { get; set; }

        /// <summary>
        /// Gets the site map provider.
        /// </summary>
        /// <value>The site map provider.</value>
        [Browsable(false)]
        SiteMapProvider SiteMapProvider { get; }

        /// <summary>
        /// Gets the is template rendered.
        /// </summary>
        /// <value>The is template rendered.</value>
        [Browsable(false)]
        bool IsTemplateRendered { get; }

        /// <summary>
        /// Creates the view model.
        /// </summary>
        /// <param name="virtualNodes">The virtual nodes.</param>
        /// <returns></returns>
        BreadcrumbViewModel CreateViewModel(IEnumerable<SiteMapNode> virtualNodes);
    }
}
