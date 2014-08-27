using System;
using System.Collections.Generic;
using System.Web;
using MbUnit.Framework;
using Navigation.Mvc.Models;
using Telerik.Sitefinity;
using Telerik.Sitefinity.Fluent.Pages;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.TestIntegration.Core.SiteMap;
using Telerik.Sitefinity.TestIntegration.Data.Content;
using Telerik.Sitefinity.Web;

namespace FeatherWidgets.TestIntegration.Sample
{
    /// <summary>
    /// This is a sample class with a test that always passes.
    /// </summary>
    [TestFixture]
    [Description("This is a sample class with a test that always passes.")]
    public class SampleTestsThatAlwaysPasses
    {
        private List<Guid> createdPageIDs = new List<Guid>();
        
        /// <summary>
        /// Clean up method
        /// </summary>
        [TearDown]
        public void Clean()
        {
            var fluentPages = App.WorkWith();

            fluentPages.Page().PageManager.Provider.SuppressSecurityChecks = true;
            for (int i = this.createdPageIDs.Count - 1; i >= 0; i--)
            {
                fluentPages.Page(this.createdPageIDs[i])
                    .Delete()
                    .SaveChanges();
            }

            this.createdPageIDs.Clear();
        }

        /// <summary>
        /// Samples the test that always passes.
        /// </summary>
        [Test]
        [Category(TestCategories.Samples)]
        [Author("idimitrov")]
        public void SampleTestThatAlwaysPasses()
        {
            var expected = new Guid("28AABFCA-6FFF-49FD-96C1-B7C1023DAE7A");
            var actual = new Guid("28AABFCA-6FFF-49FD-96C1-B7C1023DAE7A");
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Navigation widget - All sibling pages of currently opened page
        /// </summary>
        [Test]
        [Category(TestCategories.Samples)]
        [Author("bogoeva")]
        public void NavigationWidgetAllSiblingPagesOfCurrentlyOpenedPage()
        {
            string pageNamePrefix1 = "NavigationPage1";
            string pageTitlePrefix1 = "Navigation Page1";
            string urlNamePrefix1 = "navigation-page1";

            string pageNamePrefix2 = "NavigationPage2";
            string pageTitlePrefix2 = "Navigation Page2";
            string urlNamePrefix2 = "navigation-page2";

            var fluent = App.WorkWith();
            var page1Key = TestUtils.CreateAndPublishPage(fluent, PageLocation.Frontend, pageNamePrefix1, pageTitlePrefix1, urlNamePrefix1, null, false);

            this.createdPageIDs.Add(page1Key);

            var page2Key = TestUtils.CreateAndPublishPage(fluent, PageLocation.Frontend, pageNamePrefix2, pageTitlePrefix2, urlNamePrefix2, null, false);

            this.createdPageIDs.Add(page2Key);

            var page1Node = SitefinitySiteMap.GetCurrentProvider().FindSiteMapNodeFromKey(page1Key.ToString());
            SystemManager.CurrentHttpContext.Items[SiteMapBase.CurrentNodeKey] = page1Node;

            var navModel = new NavigationModel(PageSelectionMode.CurrentPageSiblings, -1, true, string.Empty);

            var expectedCount = 2;
            var actualCount = navModel.Nodes.Count;
            Assert.AreEqual(expectedCount, actualCount);
            Assert.AreEqual(pageTitlePrefix1, navModel.Nodes[0].Title);
        }
    }
}
