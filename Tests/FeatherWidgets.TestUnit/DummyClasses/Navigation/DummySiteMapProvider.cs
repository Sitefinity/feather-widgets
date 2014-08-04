using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace FeatherWidgets.TestUnit.DummyClasses.Navigation
{
    /// <summary>
    /// This class creates dummy <see cref="System.Web.SiteMapProvider"/>
    /// </summary>
    class DummySiteMapProvider : SiteMapProvider
    {
        #region Public properties 

        /// <summary>
        /// Gets or sets the number of site map levels which should be created.
        /// </summary>
        /// <value>
        /// The site map levels to create.
        /// </value>
        public int SiteMapLevelsToCreate { set; get; }

        #endregion

        #region SiteMapProvider methods

        /// <inheritdoc />
        public override SiteMapNode FindSiteMapNode(string rawUrl)
        {
            return null;
        }

        public override SiteMapNodeCollection GetChildNodes(SiteMapNode node)
        {
            SiteMapNodeCollection collection = new SiteMapNodeCollection();
            var currentLevel = this.GetNodeLevel(node.Key);

            if (currentLevel < this.SiteMapLevelsToCreate)
            {
                currentLevel++;

                for (int i = 0; i < DummySiteMapProvider.childNodesCount; i++)
                {
                    var key = DummySiteMapProvider.levelPrefix + currentLevel + DummySiteMapProvider.nodeIndexPrefix + i;
                    var title = String.Format(DummySiteMapProvider.childTitleFormat, i);
                    var url = String.Format(DummySiteMapProvider.childUrlFormat, i);
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
        /// <param name="key">The key.</param>
        /// <returns></returns>
        private int GetNodeLevel(string key)
        {
            var nodeNumber = key.IndexOf(DummySiteMapProvider.nodeIndexPrefix);
            var level = key.Substring(DummySiteMapProvider.levelPrefix.Length, nodeNumber - DummySiteMapProvider.levelPrefix.Length);

            return Int32.Parse(level);
        }

        #endregion 

        #region Constants

        /// <summary>
        /// The child nodes title format
        /// </summary>
        public const string childTitleFormat = "childTitle{0}";

        /// <summary>
        /// The child nodes URL format
        /// </summary>
        public const string childUrlFormat = "childUrl{0}";

        /// <summary>
        /// The level prefix. Used for calculating the level of the current node.
        /// </summary>
        public const string levelPrefix = "level";

        /// <summary>
        /// The node index prefix. Used for calculating the level of the current node.
        /// </summary>
        public const string nodeIndexPrefix = "nodeIndex";

        /// <summary>
        /// The number of child nodes that will be created for every node.
        /// </summary>
        public const int childNodesCount = 3;

        #endregion
    }
}
