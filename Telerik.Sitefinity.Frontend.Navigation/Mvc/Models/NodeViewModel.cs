using System.Collections.Generic;
using System.Web;
using Telerik.Sitefinity.Web;

namespace Telerik.Sitefinity.Frontend.Navigation.Mvc.Models
{
    /// <summary>
    /// This class represents the model of the Nodes that will be rendered inside the Navigation templates.
    /// </summary>
    public class NodeViewModel : FrontendPageNodeBase<NodeViewModel>
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="NodeViewModel"/> class.
        /// </summary>
        public NodeViewModel() : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NodeViewModel"/> class.
        /// </summary>
        /// <param name="node">
        /// The original SiteMapNode.
        /// </param>
        /// <param name="url">
        /// The URL.
        /// </param>
        /// <param name="target">
        /// The target.
        /// </param>
        /// <param name="isCurrentlyOpened">
        /// if set to <c>true</c> is currently opened.
        /// </param>
        /// <param name="hasChildOpen">
        /// if set to <c>true</c> currently opened page is descendent of this node.
        /// </param>
        public NodeViewModel(SiteMapNode node, string url, string target, bool isCurrentlyOpened, bool hasChildOpen)
            : base(node, url, target, isCurrentlyOpened, hasChildOpen)
        {
            if (node is PageSiteNode)
            {
                this.CustomFields = new PageCustomFieldsAccessor((PageSiteNode)node);
            }
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets a property that accesses custom fields of the data item that is represented by this view model.
        /// </summary>
        public dynamic CustomFields { get; private set; }

        #endregion
    }
}
