using System.Collections.Generic;
using System.Web;

namespace Navigation.Mvc.Models
{
    /// <summary>
    /// This class represents the model of the Nodes that will be rendered inside the Navigation templates.
    /// </summary>
    public class NodeViewModel
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="NodeViewModel"/> class.
        /// </summary>
        public NodeViewModel()
        {
            this.ChildNodes = new List<NodeViewModel>();
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
        {
            this.OriginalSiteMapNode = node;
            this.Title = node.Title;
            this.Url = url;
            this.LinkTarget = target;
            this.ChildNodes = new List<NodeViewModel>();
            this.IsCurrentlyOpened = isCurrentlyOpened;
            this.HasChildOpen = hasChildOpen;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the node title.
        /// </summary>
        /// <value>
        /// The title.
        /// </value>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the node URL.
        /// </summary>
        /// <value>
        /// The URL.
        /// </value>
        public string Url { get; set; }

        /// <summary>
        /// Gets or sets the link target.
        /// </summary>
        /// <value>
        /// The link target.
        /// </value>
        public string LinkTarget { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this node represents currently opened page
        /// </summary>
        /// <value>
        ///   <c>true</c> if page node is currently opened; otherwise, <c>false</c>.
        /// </value>
        public bool IsCurrentlyOpened { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the currently opened page is a descendent of this node.
        /// </summary>
        /// <value>
        ///   <c>true</c> if currently opened page is descendent of this node; otherwise, <c>false</c>.
        /// </value>
        public bool HasChildOpen { get; set; }

        /// <summary>
        /// Gets or sets the original site map node.
        /// </summary>
        /// <value>
        /// The original site map node.
        /// </value>
        public SiteMapNode OriginalSiteMapNode { get; set; }

        /// <summary>
        /// Gets or sets the child nodes.
        /// </summary>
        /// <value>
        /// The child nodes.
        /// </value>
        public IList<NodeViewModel> ChildNodes { get; set; }

        #endregion
    }
}
