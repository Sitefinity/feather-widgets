using Microsoft.VisualStudio.TestTools.UnitTesting;
using Navigation.Mvc.Models;
using System;
using System.Linq;
using System.Web;
using UnitTests.DummyClasses.Navigation;

namespace UnitTests.Navigation
{
    /// <summary>
    /// Tests methods for the NavigationModel
    /// </summary>
    [TestClass]
    public class NavigationModelTests
    {
        [TestInitialize]
        public void TestInitialize()
        {
            model = new DummyNavigationModel(PageSelectionMode.CurrentPageChildren, 2, true, "MyClass");
            siteMapNode = this.CreateParentSiteMapNode(2);
            model.CurrentNode = siteMapNode;
        }

        [TestCleanup]
        public void TestCleanup()
        {
            model = null;
            siteMapNode = null;
        }

        #region AddChildNodes mehtod

        [TestMethod]
        [Owner("EGaneva")]
        [Description("Checks whether the PublicAddChildNodes restricts the node collection depending of the levelsToInclude.")]
        public void PublicAddChildNodes_LevelsToInclude_RestrictsNodeCollectionToTheLevel()
        { 
            //Act
            model.PublicAddChildNodes(siteMapNode, true, 2);

            //Assert
            var resultParentNode = model.Nodes.First();
            this.AssertParentNode(resultParentNode);

            for (int i = 0; i < DummySiteMapProvider.childNodesCount; i++)
            {
                var title = String.Format(DummySiteMapProvider.childTitleFormat, i);
                var url = String.Format(DummySiteMapProvider.childUrlFormat, i);
                var resultNode = resultParentNode.ChildNodes[i];
                this.AssertNodeViewModel(title, url, 0, resultNode);
            }
        }

        [TestMethod]
        [Owner("EGaneva")]
        [Description("Checks whether the PublicAddChildNodes returns whole node collection when levelsToInclude is set to negative integer.")]
        public void PublicAddChildNodes_AllLevelsToInclude_ReturnsWholeNodeCollection()
        {
            //Act
            model.PublicAddChildNodes(siteMapNode, true, -1);

            //Assert
            var resultParentNode = model.Nodes.First();
            this.AssertParentNode(resultParentNode);

            for (int i = 0; i < DummySiteMapProvider.childNodesCount; i++)
            {
                var title = String.Format(DummySiteMapProvider.childTitleFormat, i);
                var url = String.Format(DummySiteMapProvider.childUrlFormat, i);
                var resultNode = resultParentNode.ChildNodes[i];
                this.AssertNodeViewModel(title, url, DummySiteMapProvider.childNodesCount, resultNode);

                for (int j = 0; j < DummySiteMapProvider.childNodesCount; j++)
                {
                    var childTitle = String.Format(DummySiteMapProvider.childTitleFormat, j);
                    var childUrl = String.Format(DummySiteMapProvider.childUrlFormat, j);
                    var childResultNode = resultNode.ChildNodes[j];
                    this.AssertNodeViewModel(childTitle, childUrl, 0, childResultNode);
                }
            }
        }

        [TestMethod]
        [Owner("EGaneva")]
        [Description("Checks whether the PublicAddChildNodes doesn't include parent node when addParent is set to false.")]
        public void PublicAddChildNodes_WithoutParentNode_RestrictsNodeCollectionWhitoutParentNode()
        {
            //Act
            model.PublicAddChildNodes(siteMapNode, false, 2);

            //Assert
            Assert.IsNotNull(model.Nodes, "The node collection is not initialized.");
            Assert.AreEqual(DummySiteMapProvider.childNodesCount, model.Nodes.Count(), "The node collection should contain 3 node at the parent level.");

            for (int i = 0; i < DummySiteMapProvider.childNodesCount; i++)
            {
                var resultParentNode = model.Nodes[i];

                for (int j = 0; j < DummySiteMapProvider.childNodesCount; j++)
                {
                    var title = String.Format(DummySiteMapProvider.childTitleFormat, j);
                    var url = String.Format(DummySiteMapProvider.childUrlFormat, j);
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
        private void AssertParentNode(NodeViewModel resultParentNode)
        {
            Assert.IsNotNull(model.Nodes, "The node collection is not initialized.");
            Assert.AreEqual(1, model.Nodes.Count(), "The node collection should contain 1 node at the parent level.");
            Assert.AreEqual(siteMapNode, resultParentNode.OriginalSiteMapNode, "The OriginalSiteMapNode property of the first node is not populated correctly.");
            Assert.AreEqual(DummySiteMapProvider.childNodesCount, resultParentNode.ChildNodes.Count(), "The count of the child nodes of the parent node is not correct.");
        }

        /// <summary>
        /// Creates the parent site map node.
        /// </summary>
        /// <param name="levelsToCreate">The levels to create.</param>
        /// <returns></returns>
        private SiteMapNode CreateParentSiteMapNode(int levelsToCreate)
        {
            var provider = new DummySiteMapProvider();
            provider.SiteMapLevelsToCreate = levelsToCreate;
            provider.Initialize("dummyProvider", null);
            var parentKey = DummySiteMapProvider.levelPrefix + 0 + DummySiteMapProvider.nodeIndexPrefix + 0;
            var parentNode = new SiteMapNode(provider, parentKey);

            return parentNode;
        }

        #endregion 

        #region Private fields and constants

        DummyNavigationModel model;
        SiteMapNode siteMapNode;

        #endregion 

    }
}
