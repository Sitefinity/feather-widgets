using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Navigation.Mvc.Models
{
    /// <summary>
    /// This class represents the model of the Nodes that will be rendered inside the Navigation templates.
    /// </summary>
    public class NodeViewModel
    {
        #region Constructor

        public NodeViewModel()
        {
            this.ChildNodes = new List<NodeViewModel>();
        }

        public NodeViewModel(string title, string url, bool isCurrentlyOpened)
        {
            this.Title = title;
            this.Url = url;
            this.ChildNodes = new List<NodeViewModel>();
            this.IsCurrentlyOpened = isCurrentlyOpened;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the node title.
        /// </summary>
        /// <value>
        /// The title.
        /// </value>
        public string Title { set; get; }

        /// <summary>
        /// Gets or sets the node URL.
        /// </summary>
        /// <value>
        /// The URL.
        /// </value>
        public string Url { set; get; }

        /// <summary>
        /// Gets or sets a value indicating whether this node represents currently opened page
        /// </summary>
        /// <value>
        ///   <c>true</c> if page node is currently opened; otherwise, <c>false</c>.
        /// </value>
        public bool IsCurrentlyOpened { set; get; }

        /// <summary>
        /// Gets or sets the child nodes.
        /// </summary>
        /// <value>
        /// The child nodes.
        /// </value>
        public IList<NodeViewModel> ChildNodes { set; get; }

        #endregion
    }
}
