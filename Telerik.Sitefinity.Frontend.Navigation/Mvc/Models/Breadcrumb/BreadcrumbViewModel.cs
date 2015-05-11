using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;
using Telerik.Sitefinity.Web.UI.NavigationControls;

namespace Telerik.Sitefinity.Frontend.Navigation.Mvc.Models.Breadcrumb
{
    /// <summary>
    ///     This class represents view model used for displaying Breadcrumb's nodes.
    /// </summary>
    public class BreadcrumbViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BreadcrumbViewModel" /> class.
        /// </summary>
        /// <param name="list">The list.</param>
        public BreadcrumbViewModel(IEnumerable<SiteMapNode> list)
        {
            var collection = list.Select(n => new BreadcrumbNodeModel(NavigationUtilities.ResolveUrl(n), n.Title));

            this.SiteMapNodes = new ReadOnlyCollection<BreadcrumbNodeModel>(collection.ToList());
        }

        /// <summary>
        /// Gets or sets the site map nodes.
        /// </summary>
        /// <value>The site map nodes.</value>
        public ReadOnlyCollection<BreadcrumbNodeModel> SiteMapNodes { get; private set; }

        /// <summary>
        /// Gets or sets the show current page in the end.
        /// </summary>
        /// <value>The show current page in the end.</value>
        public bool ShowCurrentPageInTheEnd { get; set; }
        
        /// <summary>
        /// Gets or sets the is template rendered.
        /// </summary>
        /// <value>The is template rendered.</value>
        public bool IsTemplateRendered { get; set; }

        /// <summary>
        /// Gets or sets the CSS class that will be applied on the wrapper div of the Breadcrumb selector widget (if such is presented).
        /// </summary>
        /// <value>
        /// The CSS class.
        /// </value>
        public string CssClass { get; set; }
    }
}
