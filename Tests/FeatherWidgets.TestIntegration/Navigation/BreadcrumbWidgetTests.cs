using System;
using System.Collections.Generic;
using System.Linq;
using MbUnit.Framework;
using Telerik.Sitefinity;
using Telerik.Sitefinity.Fluent.Pages;
using Telerik.Sitefinity.Frontend.Navigation.Mvc.Models.Breadcrumb;
using Telerik.Sitefinity.Frontend.TestUtilities;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.TestIntegration.Core.SiteMap;
using Telerik.Sitefinity.Web;

namespace FeatherWidgets.TestIntegration.Navigation
{
    [Description("Tests for the Breadcrumb model")]
    [TestFixture]
    public class BreadcrumbWidgetTests
    {
        [Test]
        [Category(TestCategories.Navigation)]
        [Author(FeatherTeams.Team7)]
        [Description("Verifies that BreadcrumbModel works properly when we want bradcrumb from the home page to the current one.")]
        public void BreadcrumbModel_FromHomeToCurrentPage()
        {
            this.CreateTestPages();

            var model = new BreadcrumbModel();
            var viewModel = model.CreateViewModel(null);

            for (int i = 0; i < BreadcrumbWidgetTests.TestPagesCount; i++)
            {
                var expected = SitefinitySiteMap.GetCurrentProvider().FindSiteMapNodeFromKey(this.createdPageIDs[i].ToString());
                var actual = viewModel.SiteMapNodes[i];

                Assert.AreEqual(expected.Title, actual.Title);
            }
        }

        [Test]
        [Category(TestCategories.Navigation)]
        [Author(FeatherTeams.Team7)]
        [Description("Verifies that BreadcrumbModel works properly when we want bradcrumb from the home page to the last before the current one.")]
        public void BreadcrumbModel_FromHomeToLastWithoutCurrentPage()
        {
            this.CreateTestPages();

            var model = new BreadcrumbModel();
            model.ShowCurrentPageInTheEnd = false;
            var viewModel = model.CreateViewModel(null);

            Assert.AreEqual(BreadcrumbWidgetTests.TestPagesCount - 1, viewModel.SiteMapNodes.Count);

            for (int i = 0; i < BreadcrumbWidgetTests.TestPagesCount - 1; i++)
            {
                var expected = SitefinitySiteMap.GetCurrentProvider().FindSiteMapNodeFromKey(this.createdPageIDs[i].ToString());
                var actual = viewModel.SiteMapNodes[i];

                Assert.AreEqual(expected.Title, actual.Title);
            }
        }

        [Test]
        [Category(TestCategories.Navigation)]
        [Author(FeatherTeams.Team7)]
        [Description("Verifies that BreadcrumbModel works properly when we want bradcrumb from the first after home page to the current one.")]
        public void BreadcrumbModel_WithoutHomeToCurrentPage()
        {
            this.CreateTestPages();

            var model = new BreadcrumbModel();
            model.ShowHomePageLink = false;
            var viewModel = model.CreateViewModel(null);

            Assert.AreEqual(BreadcrumbWidgetTests.TestPagesCount - 1, viewModel.SiteMapNodes.Count);

            for (int i = 0; i < BreadcrumbWidgetTests.TestPagesCount - 1; i++)
            {
                var expected = SitefinitySiteMap.GetCurrentProvider().FindSiteMapNodeFromKey(this.createdPageIDs[i + 1].ToString());
                var actual = viewModel.SiteMapNodes[i];

                Assert.AreEqual(expected.Title, actual.Title);
            }
        }

        [Test]
        [Category(TestCategories.Navigation)]
        [Author(FeatherTeams.Team7)]
        [Description("Verifies that BreadcrumbModel workds properly when group pages should be shown.")]
        public void BreadcrumbModel_WithGroupPages()
        {
            this.CreateTestPages(true);

            var model = new BreadcrumbModel();
            model.ShowGroupPages = true;
            var viewModel = model.CreateViewModel(null);

            for (int i = 0; i < BreadcrumbWidgetTests.TestPagesCount; i++)
            {
                var expected = SitefinitySiteMap.GetCurrentProvider().FindSiteMapNodeFromKey(this.createdPageIDs[i].ToString());
                var actual = viewModel.SiteMapNodes[i];

                Assert.AreEqual(expected.Title, actual.Title);
            }
        }

        [Test]
        [Category(TestCategories.Navigation)]
        [Author(FeatherTeams.Team7)]
        [Description("Verifies that BreadcrumbModel works properly when there is a registered breadcrumb extender.")]
        public void BreadcrumbModel_BreadcrumbExtender_VirtualNodes()
        {
            this.CreateTestPages();

            var model = new BreadcrumbModel();
            model.AllowVirtualNodes = true;
            var extender = new DummyBreadcrumbExtender();
            var viewModel = model.CreateViewModel(extender);

            Assert.AreEqual(BreadcrumbWidgetTests.TestPagesCount + 1, viewModel.SiteMapNodes.Count);

            for (int i = 0; i < BreadcrumbWidgetTests.TestPagesCount; i++)
            {
                var expected = SitefinitySiteMap.GetCurrentProvider().FindSiteMapNodeFromKey(this.createdPageIDs[i].ToString());
                var actual = viewModel.SiteMapNodes[i];

                Assert.AreEqual(expected.Title, actual.Title);
            }

            Assert.AreEqual(DummyBreadcrumbExtender.DummySiteMapNodeTitle, viewModel.SiteMapNodes.Last().Title);
        }        

        /// <summary>
        /// Clean up method
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1304:SpecifyCultureInfo", MessageId = "Telerik.Sitefinity.Fluent.Pages.PageFacade.Delete")]
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

        private void CreateTestPages(bool groupPages = false)
        {
            var baseName = "testPage";

            var fluent = App.WorkWith();
            PageNode parentPage = null;

            for (int i = 0; i < BreadcrumbWidgetTests.TestPagesCount; i++)
            {
                var isGroupPage = groupPages && i < BreadcrumbWidgetTests.TestPagesCount - 1;
                var name = baseName + i;
                var pageId = TestUtils.CreateAndPublishPage(fluent, PageLocation.Frontend, name, name, name, parentPage, isGroupPage);
                this.createdPageIDs.Add(pageId);

                parentPage = fluent.Page(pageId).Get();
            }

            var pageNode = SitefinitySiteMap.GetCurrentProvider().FindSiteMapNodeFromKey(this.createdPageIDs.Last().ToString());
            SystemManager.CurrentHttpContext.Items[SiteMapBase.CurrentNodeKey] = pageNode;
        }

        private List<Guid> createdPageIDs = new List<Guid>();
        private const int TestPagesCount = 5;
    }
}
