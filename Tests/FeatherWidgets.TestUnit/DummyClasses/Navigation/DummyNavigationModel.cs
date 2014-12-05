using System;
using System.Web;
using Telerik.Sitefinity.Frontend.Navigation.Mvc.Models;

namespace FeatherWidgets.TestUnit.DummyClasses.Navigation
{
    /// <summary>
    /// This class creates dummy <see cref="Navigation.Mvc.Models.NavigationModel"/>
    /// </summary>
    public class DummyNavigationModel : NavigationModel
    {
        #region Constructors 

        /// <summary>
        /// Initializes a new instance of the <see cref="DummyNavigationModel"/> class.
        /// </summary>
        /// <param name="selectionMode">The selection mode.</param>
        /// <param name="levelsToInclude">The levels to include.</param>
        /// <param name="showParentPage">if set to <c>true</c> adds parent page.</param>
        /// <param name="cssClass">css class</param>
        public DummyNavigationModel(PageSelectionMode selectionMode, int? levelsToInclude, bool showParentPage, string cssClass) : base()
        {
            this.SelectionMode = selectionMode;
            this.LevelsToInclude = levelsToInclude;
            this.ShowParentPage = showParentPage;
            this.CssClass = cssClass;
        }

        #endregion

        #region Public properties

        /// <summary>
        /// Gets or sets the current node.
        /// </summary>
        /// <value>
        /// The current node.
        /// </value>
        public SiteMapNode CurrentNode { get; set; }

        #endregion 

        #region Public methods

        /// <summary>
        /// Exposes the AddChildNodes for testing purposes.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <param name="addParent">if set to <c>true</c> adds the parent node.</param>
        public void PublicAddChildNodes(SiteMapNode node, bool addParent)
        {
            this.AddChildNodes(node, addParent);
        }

        #endregion 

        #region NavigationModel members

        /// <summary>
        /// Checks the site map node.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <returns>boolean result</returns>
        protected override bool CheckSiteMapNode(SiteMapNode node)
        {
            return true;
        }

        /// <summary>
        /// Dummies the current site map node.
        /// </summary>
        /// <value>
        /// The current site map node.
        /// </value>
        public override SiteMapNode CurrentSiteMapNode
        {
            get
            {
                return this.CurrentNode;
            }
        }

        /// <summary>
        /// Gets dummy root node identifier.
        /// </summary>
        /// <returns>Guid result</returns>
        protected override Guid RootNodeId
        {
            get 
            {
                return Guid.NewGuid();
            }
        }

        /// <summary>
        /// Dummies the resolving of the URL.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <returns>url result</returns>
        protected override string ResolveUrl(SiteMapNode node)
        {
            return node != null ? node.Url : string.Empty;
        }

        /// <summary>
        /// Dummies the link target.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <returns>link result</returns>
        protected override string GetLinkTarget(SiteMapNode node)
        {
            return "_blank";
        }

        #endregion 
    }
}
