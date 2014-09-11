using System;
using System.Collections.Generic;
using System.Globalization;
using MbUnit.Framework;
using Navigation.Mvc.Models;
using Telerik.Sitefinity;
using Telerik.Sitefinity.Fluent.Pages;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.TestIntegration.Core.SiteMap;
using Telerik.Sitefinity.Web;

namespace FeatherWidgets.TestIntegration.Navigation
{
    /// <summary>
    /// This is a class with Navigation tests.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1063:ImplementIDisposableCorrectly"), TestFixture]
    [Description("This is a class with Navigation tests.")]
    public class NavigationWidgetPageSelectionModeTests 
    {
        private List<Guid> createdPageIDs = new List<Guid>();

        /// <summary>
        /// Clean up method
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1304:SpecifyCultureInfo", MessageId = "Telerik.Sitefinity.Fluent.Pages.PageFacade.Delete"), TearDown]
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
        /// Navigation widget - All sibling pages of currently opened page
        /// </summary>
        [Test]
        [Category(TestCategories.Navigation)]
        [Author("bogoeva")]
        public void NavigationWidget_AllSiblingPagesOfCurrentlyOpenedPage()
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
