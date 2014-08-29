using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FeatherWidgets.TestUnit.DummyClasses.Navigation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Navigation.Mvc.Models;

namespace FeatherWidgets.TestUnit.Navigation
{
    /// <summary>
    /// Tests methods for the NavigationModel
    /// </summary>
    [TestClass]
    public class NavigationModelTests
    {
        /// <summary>
        /// Initializes the tests.
        /// </summary>
        [TestInitialize]
        public void TestInitialize()
        {
            this.siteMapNode = this.CreateParentSiteMapNode(2);
        }

        /// <summary>
        /// Cleanup after tests.
        /// </summary>
        [TestCleanup]
        public void TestCleanup()
        {
            this.siteMapNode = null;
        }

        #region AddChildNodes mehtod

        /// <summary>
        /// Checks whether the PublicAddChildNodes restricts the node collection depending of the levelsToInclude.
        /// </summary>
        [TestMethod]
        [Owner("EGaneva")]
        [Description("Checks whether the PublicAddChildNodes restricts the node collection depending of the levelsToInclude.")]
        public void PublicAddChildNodes_LevelsToInclude_RestrictsNodeCollectionToTheLevel()
        {
            var model = new DummyNavigationModel(PageSelectionMode.CurrentPageChildren, 2, true, "MyClass");
            model.CurrentNode = this.siteMapNode;

            // Act
            model.PublicAddChildNodes(this.siteMapNode, true);

            // Assert
            var resultParentNode = model.Nodes.First();
            this.AssertParentNode(resultParentNode, model.Nodes);

            for (int i = 0; i < DummySiteMapProvider.ChildNodesCount; i++)
            {
                var title = string.Format(System.Globalization.CultureInfo.InvariantCulture, DummySiteMapProvider.ChildTitleFormat, i);
                var url = string.Format(System.Globalization.CultureInfo.InvariantCulture, DummySiteMapProvider.ChildUrlFormat, i);
                var resultNode = resultParentNode.ChildNodes[i];
                this.AssertNodeViewModel(title, url, 0, resultNode);
            }
        }

        /// <summary>
        /// Checks whether the PublicAddChildNodes returns whole node collection when levelsToInclude is set to negative integer.
        /// </summary>
        [TestMethod]
        [Owner("EGaneva")]
        [Description("Checks whether the PublicAddChildNodes returns whole node collection when levelsToInclude is set to negative integer.")]
        public void PublicAddChildNodes_AllLevelsToInclude_ReturnsWholeNodeCollection()
        {
            var model = new DummyNavigationModel(PageSelectionMode.CurrentPageChildren, -1, true, "MyClass");
            model.CurrentNode = this.siteMapNode;

            model.PublicAddChildNodes(this.siteMapNode, true);

            var resultParentNode = model.Nodes.First();
            this.AssertParentNode(resultParentNode, model.Nodes);

            for (int i = 0; i < DummySiteMapProvider.ChildNodesCount; i++)
            {
                var title = string.Format(System.Globalization.CultureInfo.InvariantCulture, DummySiteMapProvider.ChildTitleFormat, i);
                var url = string.Format(System.Globalization.CultureInfo.InvariantCulture, DummySiteMapProvider.ChildUrlFormat, i);
                var resultNode = resultParentNode.ChildNodes[i];
                this.AssertNodeViewModel(title, url, DummySiteMapProvider.ChildNodesCount, resultNode);

                for (int j = 0; j < DummySiteMapProvider.ChildNodesCount; j++)
                {
                    var childTitle = string.Format(System.Globalization.CultureInfo.InvariantCulture, DummySiteMapProvider.ChildTitleFormat, j);
                    var childUrl = string.Format(System.Globalization.CultureInfo.InvariantCulture, DummySiteMapProvider.ChildUrlFormat, j);
                    var childResultNode = resultNode.ChildNodes[j];
                    this.AssertNodeViewModel(childTitle, childUrl, 0, childResultNode);
                }
            }
        }

        /// <summary>
        /// Checks whether the PublicAddChildNodes doesn't include parent node when addParent is set to false.
        /// </summary>
        [TestMethod]
        [Owner("EGaneva")]
        [Description("Checks whether the PublicAddChildNodes doesn't include parent node when addParent is set to false.")]
        public void PublicAddChildNodes_WithoutParentNode_RestrictsNodeCollectionWithoutParentNode()
        {
            var model = new DummyNavigationModel(PageSelectionMode.CurrentPageChildren, 2, true, "MyClass");
            model.CurrentNode = this.siteMapNode;

            model.PublicAddChildNodes(this.siteMapNode, false);

            Assert.IsNotNull(model.Nodes, "The node collection is not initialized.");
            Assert.AreEqual(DummySiteMapProvider.ChildNodesCount, model.Nodes.Count(), "The node collection should contain 3 node at the parent level.");

            for (int i = 0; i < DummySiteMapProvider.ChildNodesCount; i++)
            {
                var resultParentNode = model.Nodes[i];

                for (int j = 0; j < DummySiteMapProvider.ChildNodesCount; j++)
                {
                    var title = string.Format(System.Globalization.CultureInfo.InvariantCulture, DummySiteMapProvider.ChildTitleFormat, j);
                    var url = string.Format(System.Globalization.CultureInfo.InvariantCulture, DummySiteMapProvider.ChildUrlFormat, j);
                    var resultNode = resultParentNode.ChildNodes[j];
                    this.AssertNodeViewModel(title, url, 0, resultNode);
                }
            }
        }

        #endregion

        #region Helper methods

        /// <summary>
        /// Asserts whether NodeViewModel instance is populated correctly.
        /// </summary>
        /// <param name="expectedTitle">The expected title.</param>
        /// <param name="expectedUrl">The expected URL.</param>
        /// <param name="expectedChildNodesCount">The expected child nodes count.</param>
        /// <param name="resultNode">The result node.</param>
        private void AssertNodeViewModel(string expectedTitle, string expectedUrl, int expectedChildNodesCount, NodeViewModel resultNode)
        {
            Assert.AreEqual(expectedTitle, resultNode.Title, "The title of the node is not as expected.");
            Assert.AreEqual(expectedUrl, resultNode.Url, "The url of the node is not as expected.");
            Assert.AreEqual(expectedChildNodesCount, resultNode.ChildNodes.Count(), "The child levels are not restricted correctly.");
        }

        /// <summary>
        /// Asserts the parent node.
        /// </summary>
        /// <param name="resultParentNode">The result parent node.</param>
        /// <param name="nodes">The nodes.</param>
        private void AssertParentNode(NodeViewModel resultParentNode, IEnumerable<NodeViewModel> nodes)
        {
            Assert.IsNotNull(nodes, "The node collection is not initialized.");
            Assert.AreEqual(1, nodes.Count(), "The node collection should contain 1 node at the parent level.");
            Assert.AreEqual(this.siteMapNode, resultParentNode.OriginalSiteMapNode, "The OriginalSiteMapNode property of the first node is not populated correctly.");
            Assert.AreEqual(DummySiteMapProvider.ChildNodesCount, resultParentNode.ChildNodes.Count(), "The count of the child nodes of the parent node is not correct.");
        }

        /// <summary>
        /// Creates the parent site map node.
        /// </summary>
        /// <param name="levelsToCreate">The levels to create.</param>
        /// <returns>CreateParentSiteMap Node</returns>
        private SiteMapNode CreateParentSiteMapNode(int levelsToCreate)
        {
            var provider = new DummySiteMapProvider();
            provider.SiteMapLevelsToCreate = levelsToCreate;
            provider.Initialize("dummyProvider", null);
            var parentKey = DummySiteMapProvider.LevelPrefix + 0 + DummySiteMapProvider.NodeIndexPrefix + 0;
            var parentNode = new SiteMapNode(provider, parentKey);

            return parentNode;
        }

        #endregion 

        #region Private fields and constants

        private SiteMapNode siteMapNode;

        #endregion 
    }
}
