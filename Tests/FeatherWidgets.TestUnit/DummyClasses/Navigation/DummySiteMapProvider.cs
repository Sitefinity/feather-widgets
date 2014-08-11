using System.Web;

namespace FeatherWidgets.TestUnit.DummyClasses.Navigation
{
    /// <summary>
    /// This class creates dummy <see cref="System.Web.SiteMapProvider"/>
    /// </summary>
    public class DummySiteMapProvider : SiteMapProvider
    {
        #region Public properties 

        /// <summary>
        /// Gets or sets the number of site map levels which should be created.
        /// </summary>
        /// <value>
        /// The site map levels to create.
        /// </value>
        public int SiteMapLevelsToCreate { get; set; }

        #endregion

        #region SiteMapProvider methods

        /// <inheritdoc />
        public override SiteMapNode FindSiteMapNode(string rawUrl)
        {
            return null;
        }

        /// <summary>
        /// The get child nodes.
        /// </summary>
        /// <param name="node">
        /// The node.
        /// </param>
        /// <returns>
        /// The <see cref="SiteMapNodeCollection"/>.
        /// </returns>
        public override SiteMapNodeCollection GetChildNodes(SiteMapNode node)
        {
            SiteMapNodeCollection collection = new SiteMapNodeCollection();
            var currentLevel = this.GetNodeLevel(node.Key);

            if (currentLevel < this.SiteMapLevelsToCreate)
            {
                currentLevel++;

                for (int i = 0; i < DummySiteMapProvider.ChildNodesCount; i++)
                {
                    var key = DummySiteMapProvider.LevelPrefix + currentLevel + DummySiteMapProvider.NodeIndexPrefix + i;
                    var title = string.Format(DummySiteMapProvider.ChildTitleFormat, i);
                    var url = string.Format(DummySiteMapProvider.ChildUrlFormat, i);
                    var childnode = new SiteMapNode(this, key, url, title);
                    collection.Add(childnode);
                }
            }

            return collection;
        }

        /// <inheritdoc />
        public override SiteMapNode GetParentNode(SiteMapNode node)
        {
            return null;
        }

        /// <inheritdoc />
        protected override SiteMapNode GetRootNodeCore()
        {
            return null;
        }

        #endregion 

        #region Helper methods

        /// <summary>
        /// Gets the node level.
        /// </summary>
        /// <param name="key">
        /// The key.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        private int GetNodeLevel(string key)
        {
            var nodeNumber = key.IndexOf(DummySiteMapProvider.NodeIndexPrefix);
            var level = key.Substring(DummySiteMapProvider.LevelPrefix.Length, nodeNumber - DummySiteMapProvider.LevelPrefix.Length);

            return int.Parse(level);
        }

        #endregion 

        #region Constants

        /// <summary>
        /// The child nodes title format
        /// </summary>
        public const string ChildTitleFormat = "childTitle{0}";

        /// <summary>
        /// The child nodes URL format
        /// </summary>
        public const string ChildUrlFormat = "childUrl{0}";

        /// <summary>
        /// The level prefix. Used for calculating the level of the current node.
        /// </summary>
        public const string LevelPrefix = "level";

        /// <summary>
        /// The node index prefix. Used for calculating the level of the current node.
        /// </summary>
        public const string NodeIndexPrefix = "nodeIndex";

        /// <summary>
        /// The number of child nodes that will be created for every node.
        /// </summary>
        public const int ChildNodesCount = 3;

        #endregion
    }
}
