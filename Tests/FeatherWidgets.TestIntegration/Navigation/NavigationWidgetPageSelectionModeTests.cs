using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Web;
using FeatherWidgets.TestUtilities.CommonOperations;
using MbUnit.Framework;
using Telerik.Sitefinity;
using Telerik.Sitefinity.Fluent.Pages;
using Telerik.Sitefinity.Frontend.Navigation.Mvc.Controllers;
using Telerik.Sitefinity.Frontend.Navigation.Mvc.Models;
using Telerik.Sitefinity.Frontend.TestUtilities;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Mvc.Proxy;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.TestIntegration.Core.SiteMap;
using Telerik.Sitefinity.TestIntegration.Helpers;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Workflow;

namespace FeatherWidgets.TestIntegration.Navigation
{
    /// <summary>
    /// This is a class with Navigation tests.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1063:ImplementIDisposableCorrectly"), TestFixture]
    [Description("This is a class with Navigation tests.")]
    public class NavigationWidgetPageSelectionModeTests
    {
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
        [Author(FeatherTeams.FeatherTeam)]
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

            var navModel = new NavigationModel(PageSelectionMode.CurrentPageSiblings, Guid.Empty, null, -1, true, string.Empty, false);

            var expectedCount = 2;
            var actualCount = navModel.Nodes.Count;
            Assert.AreEqual(expectedCount, actualCount);
            Assert.AreEqual(pageTitlePrefix1, navModel.Nodes[0].Title);
        }

        /// <summary>
        /// Navigation widget - Horizontal template and 5 levels to include
        /// </summary>
        [Test]
        [Category(TestCategories.Navigation)]
        [Author(FeatherTeams.FeatherTeam)]
        public void NavigationWidget_HorizontalTemplate5LevelsToInclude()
        {
            string url = UrlPath.ResolveAbsoluteUrl("~/" + UrlNamePrefix + Index);
            string navigationId = "navbar";

            var mvcProxy = new MvcControllerProxy();
            mvcProxy.ControllerName = typeof(NavigationController).FullName;
            var navigationController = new NavigationController();
            navigationController.LevelsToInclude = 5;
            navigationController.TemplateName = "Horizontal";
            mvcProxy.Settings = new ControllerSettings(navigationController);

            Guid paretnPageId = this.pageOperations.CreatePageWithControl(mvcProxy, PageNamePrefix, PageTitlePrefix, UrlNamePrefix, Index);
            this.createdPageIDs.Add(paretnPageId);

            for (int i = 1; i <= 6; i++)
            {
                this.createdPageIDs.Add(Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.Pages().CreatePage(PageTitlePrefix + i, Guid.NewGuid(), this.createdPageIDs[(i - 1)]));
            }

            string responseContent = PageInvoker.ExecuteWebRequest(url);

            Assert.IsTrue(responseContent.Contains(navigationId), "The navigation with this id was not found!");

            for (int i = 0; i <= 6; i++)
            {
                if (i <= 4)
                {
                    Assert.IsTrue(responseContent.Contains(PageTitlePrefix + i), "The page with this title was not found!");
                }
                else
                {
                    Assert.IsFalse(responseContent.Contains(PageTitlePrefix + i), "The page with this title was found!");
                }
            }
        }

        /// <summary>
        /// Navigation widget - Vertical template and 5 levels to include
        /// </summary>
        [Test]
        [Category(TestCategories.Navigation)]
        [Author(FeatherTeams.FeatherTeam)]
        public void NavigationWidget_VerticalTemplate5LevelsToInclude()
        {
            string url = UrlPath.ResolveAbsoluteUrl("~/" + UrlNamePrefix + Index);

            var mvcProxy = new MvcControllerProxy();
            mvcProxy.ControllerName = typeof(NavigationController).FullName;
            var navigationController = new NavigationController();
            navigationController.LevelsToInclude = 5;
            navigationController.TemplateName = "Vertical";
            mvcProxy.Settings = new ControllerSettings(navigationController);

            Guid paretnPageId = this.pageOperations.CreatePageWithControl(mvcProxy, PageNamePrefix, PageTitlePrefix, UrlNamePrefix, Index);
            this.createdPageIDs.Add(paretnPageId);

            for (int i = 1; i <= 6; i++)
            {
                this.createdPageIDs.Add(Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.Pages().CreatePage(PageTitlePrefix + i, Guid.NewGuid(), this.createdPageIDs[(i - 1)]));
            }

            string responseContent = PageInvoker.ExecuteWebRequest(url);

            for (int i = 0; i <= 6; i++)
            {
                if (i <= 4)
                {
                    Assert.IsTrue(responseContent.Contains(PageTitlePrefix + i), "The page with this title was not found!");
                }
                else
                {
                    Assert.IsFalse(responseContent.Contains(PageTitlePrefix + i), "The page with this title was found!");
                }
            }
        }

        /// <summary>
        /// Navigation widget - Tabs template and 5 levels to include
        /// </summary>
        [Test]
        [Category(TestCategories.Navigation)]
        [Author(FeatherTeams.FeatherTeam)]
        public void NavigationWidget_TabsTemplate5LevelsToInclude()
        {
            string url = UrlPath.ResolveAbsoluteUrl("~/" + UrlNamePrefix + Index);

            var mvcProxy = new MvcControllerProxy();
            mvcProxy.ControllerName = typeof(NavigationController).FullName;
            var navigationController = new NavigationController();
            navigationController.LevelsToInclude = 5;
            navigationController.TemplateName = "Tabs";
            mvcProxy.Settings = new ControllerSettings(navigationController);

            Guid paretnPageId = this.pageOperations.CreatePageWithControl(mvcProxy, PageNamePrefix, PageTitlePrefix, UrlNamePrefix, Index);
            this.createdPageIDs.Add(paretnPageId);

            for (int i = 1; i <= 6; i++)
            {
                this.createdPageIDs.Add(Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.Pages().CreatePage(PageTitlePrefix + i, Guid.NewGuid(), this.createdPageIDs[(i - 1)]));
            }

            string responseContent = PageInvoker.ExecuteWebRequest(url);

            for (int i = 0; i <= 6; i++)
            {
                if (i <= 4)
                {
                    Assert.IsTrue(responseContent.Contains(PageTitlePrefix + i), "The page with this title was not found!");
                }
                else
                {
                    Assert.IsFalse(responseContent.Contains(PageTitlePrefix + i), "The page with this title was found!");
                }
            }
        }

        /// <summary>
        /// Navigation widget - Sitemap template and 5 levels to include
        /// </summary>
        [Test]
        [Category(TestCategories.Navigation)]
        [Author(FeatherTeams.FeatherTeam)]
        public void NavigationWidget_SitemapTemplate5LevelsToInclude()
        {
            string url = UrlPath.ResolveAbsoluteUrl("~/" + UrlNamePrefix + Index);

            var mvcProxy = new MvcControllerProxy();
            mvcProxy.ControllerName = typeof(NavigationController).FullName;
            var navigationController = new NavigationController();
            navigationController.LevelsToInclude = 5;
            navigationController.TemplateName = "Sitemap";
            mvcProxy.Settings = new ControllerSettings(navigationController);

            Guid paretnPageId = this.pageOperations.CreatePageWithControl(mvcProxy, PageNamePrefix, PageTitlePrefix, UrlNamePrefix, Index);
            this.createdPageIDs.Add(paretnPageId);

            for (int i = 1; i <= 6; i++)
            {
                this.createdPageIDs.Add(Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.Pages().CreatePage(PageTitlePrefix + i, Guid.NewGuid(), this.createdPageIDs[(i - 1)]));
            }

            string responseContent = PageInvoker.ExecuteWebRequest(url);

            for (int i = 0; i <= 6; i++)
            {
                if (i <= 4)
                {
                    Assert.IsTrue(responseContent.Contains(PageTitlePrefix + i), "The page with this title was not found!");
                }
                else
                {
                    Assert.IsFalse(responseContent.Contains(PageTitlePrefix + i), "The page with this title was found!");
                }
            }
        }

        /// <summary>
        /// Navigation widget - All child pages of a specified page
        /// </summary>
        [Test]
        [Category(TestCategories.Navigation)]
        [Author(FeatherTeams.FeatherTeam)]
        public void NavigationWidget_AllChildPagesOfSpecifiedPage()
        {
            string pageName1 = "NavigationPage1";
            string pageTitle1 = "Navigation Page1";
            string urlName1 = "navigation-page1";

            string pageName2 = "NavigationPage2";
            string pageTitle2 = "Navigation Page2";
            string urlName2 = "navigation-page2";

            var fluent = App.WorkWith();
            var page1Key = TestUtils.CreateAndPublishPage(fluent, PageLocation.Frontend, pageName1, pageTitle1, urlName1, null, false);

            this.createdPageIDs.Add(page1Key);

            var page1Node = fluent.Page(page1Key).Get();
            var page2Key = TestUtils.CreateAndPublishPage(fluent, PageLocation.Frontend, pageName2, pageTitle2, urlName2, page1Node, false);

            this.createdPageIDs.Add(page2Key);

            var navModel = new NavigationModel(PageSelectionMode.SelectedPageChildren, page1Key, null, -1, false, string.Empty, false);

            var expectedCount = 1;
            var actualCount = navModel.Nodes.Count;
            Assert.AreEqual(expectedCount, actualCount);
            Assert.AreEqual(pageTitle2, navModel.Nodes[0].Title);
        }

        /// <summary>
        /// Navigation widget - Selected pages
        /// </summary>
        [Test]
        [Category(TestCategories.Navigation)]
        [Author(FeatherTeams.FeatherTeam)]
        public void NavigationWidget_SelectedPages()
        {
            string pageName1 = "NavigationPage1";
            string pageTitle1 = "Navigation Page1";
            string urlName1 = "navigation-page1";

            string pageName2 = "NavigationPage2";
            string pageTitle2 = "Navigation Page2";
            string urlName2 = "navigation-page2";

            string pageName3 = "NavigationPage3";
            string pageTitle3 = "Navigation Page3";
            string urlName3 = "navigation-page3";

            var fluent = App.WorkWith();
            var page1Key = TestUtils.CreateAndPublishPage(fluent, PageLocation.Frontend, pageName1, pageTitle1, urlName1, null, false);
            this.createdPageIDs.Add(page1Key);

            var page2Key = TestUtils.CreateAndPublishPage(fluent, PageLocation.Frontend, pageName2, pageTitle2, urlName2, null, false);
            this.createdPageIDs.Add(page2Key);

            var page3Key = TestUtils.CreateAndPublishPage(fluent, PageLocation.Frontend, pageName3, pageTitle3, urlName3, null, false);
            this.createdPageIDs.Add(page3Key);

            var selectedPage1 = new SelectedPageModel();
            selectedPage1.Id = page1Key;
            var selectedPage2 = new SelectedPageModel();
            selectedPage2.Id = page2Key;

            var navModel = new NavigationModel(PageSelectionMode.SelectedPages, Guid.Empty, new[] { selectedPage1, selectedPage2 }, -1, false, string.Empty, false);

            var expectedCount = 2;
            var actualCount = navModel.Nodes.Count;
            Assert.AreEqual(expectedCount, actualCount);
            Assert.AreEqual(pageTitle1, navModel.Nodes[0].Title);
            Assert.AreEqual(pageTitle2, navModel.Nodes[1].Title);
        }

        /// <summary>
        /// Checks if the navigation widget properly invalidates the cached page if a page is renamed.
        /// </summary>
        [Test]
        [Category(TestCategories.Navigation)]
        [Author(FeatherTeams.FeatherTeam)]
        [Description("Checks if the navigation widget properly invalidates the cached page if a page is renamed")]
        public void NavigationWidget_ValidatePagesCacheDependenciesOnPageRename()
        {
            const string AdditionalPageTitle = "TempPage";
            const string AdditionalPageNewTitle = "RenamedPage";

            string url = UrlPath.ResolveAbsoluteUrl("~/" + UrlNamePrefix + Index);

            var mvcProxy = new MvcControllerProxy() { ControllerName = typeof(NavigationController).FullName, Settings = new ControllerSettings(new NavigationController()) };
            var paretnPageId = this.pageOperations.CreatePageWithControl(mvcProxy, PageNamePrefix, PageTitlePrefix, UrlNamePrefix, Index);
            this.createdPageIDs.Add(paretnPageId);

            var additionalPageId = new Telerik.Sitefinity.TestIntegration.Data.Content.PageContentGenerator().CreatePage(AdditionalPageTitle, AdditionalPageTitle, AdditionalPageTitle);
            this.createdPageIDs.Add(additionalPageId);
            
            var cookies = new CookieContainer();
            using (new AuthenticateUserRegion(null))
            {
                var responseContent = this.GetResponse(url, cookies);
                Assert.IsTrue(responseContent.Contains(AdditionalPageTitle + "<"), "The page title was not found");
                Assert.IsFalse(responseContent.Contains(AdditionalPageNewTitle + "<"), "The new page title was present on page");
            }

            var pageManager = PageManager.GetManager();
            var additionalPage = pageManager.GetPageNode(additionalPageId);
            additionalPage.Title = AdditionalPageNewTitle;
            pageManager.SaveChanges();

            using (new AuthenticateUserRegion(null))
            {
                var responseContent = this.GetResponse(url, cookies);
                Assert.IsTrue(responseContent.Contains(AdditionalPageNewTitle + "<"), "The page title was not invalidated");
                Assert.IsFalse(responseContent.Contains(AdditionalPageTitle + "<"), "The old page title was present on page");
            }
        }

        /// <summary>
        /// Checks if the navigation widget properly invalidates the cached page if a page is created.
        /// </summary>
        [Test]
        [Category(TestCategories.Navigation)]
        [Author(FeatherTeams.FeatherTeam)]
        [Description("Checks if the navigation widget properly invalidates the cached page if a page is created")]
        public void NavigationWidget_ValidatePagesCacheDependenciesOnPageCreate()
        {
            const string TempPageTitle = "TempPage";
            const string CreatedPageNewTitle = "CreatedPage";

            string url = UrlPath.ResolveAbsoluteUrl("~/" + UrlNamePrefix + Index);

            var mvcProxy = new MvcControllerProxy() { ControllerName = typeof(NavigationController).FullName, Settings = new ControllerSettings(new NavigationController()) };
            var paretnPageId = this.pageOperations.CreatePageWithControl(mvcProxy, PageNamePrefix, PageTitlePrefix, UrlNamePrefix, Index);
            this.createdPageIDs.Add(paretnPageId);

            var pageGenerator = new Telerik.Sitefinity.TestIntegration.Data.Content.PageContentGenerator();
            var tempPageId = pageGenerator.CreatePage(TempPageTitle, TempPageTitle, TempPageTitle);
            this.createdPageIDs.Add(tempPageId);

            var cookies = new CookieContainer();
            using (new AuthenticateUserRegion(null))
            {
                var responseContent = this.GetResponse(url, cookies);
                Assert.IsTrue(responseContent.Contains(TempPageTitle + "<"), "The existing page was not found");
                Assert.IsFalse(responseContent.Contains(CreatedPageNewTitle + "<"), "The created page was found");
            }

            var createdPageId = pageGenerator.CreatePage(CreatedPageNewTitle, CreatedPageNewTitle, CreatedPageNewTitle);
            this.createdPageIDs.Add(createdPageId);

            using (new AuthenticateUserRegion(null))
            {
                var responseContent = this.GetResponse(url, cookies);
                Assert.IsTrue(responseContent.Contains(TempPageTitle + "<"), "The existing page was not found");
                Assert.IsTrue(responseContent.Contains(CreatedPageNewTitle + "<"), "The created page was not found");
            }
        }

        /// <summary>
        /// Checks if the navigation widget properly invalidates the cached page if a child page is republished.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Unpublish"), Test]
        [Ignore("Unstable")]
        [Category(TestCategories.Navigation)]
        [Author(FeatherTeams.FeatherTeam)]
        [Description("Checks if the navigation widget properly invalidates the cached page if a child page is republished")]
        public void NavigationWidget_ValidatePagesCacheDependenciesOnChildPagePublishUnpublish()
        {
            const string ParentPageTitle = "ParentPage";
            const string ChildPageTitle = "ChildPage";

            string url = UrlPath.ResolveAbsoluteUrl("~/" + UrlNamePrefix + Index);

            var mvcProxy = new MvcControllerProxy() { ControllerName = typeof(NavigationController).FullName, Settings = new ControllerSettings(new NavigationController() { LevelsToInclude = 2 }) };
            var paretnPageId = this.pageOperations.CreatePageWithControl(mvcProxy, PageNamePrefix, PageTitlePrefix, UrlNamePrefix, Index);
            this.createdPageIDs.Add(paretnPageId);

            var pageGenerator = new Telerik.Sitefinity.TestIntegration.Data.Content.PageContentGenerator();
            var pageManager = PageManager.GetManager();

            var parentPageId = pageGenerator.CreatePage(ParentPageTitle, ParentPageTitle, ParentPageTitle);
            this.createdPageIDs.Add(parentPageId);

            var childPageId = pageGenerator.CreatePage(ChildPageTitle, ChildPageTitle, ChildPageTitle);
            this.createdPageIDs.Add(childPageId);

            var parent = pageManager.GetPageNode(parentPageId);
            var child = pageManager.GetPageNode(childPageId);
            child.Parent = parent;
            pageManager.SaveChanges();

            var cookies = new CookieContainer();
            using (new AuthenticateUserRegion(null))
            {
                var responseContent = this.GetResponse(url, cookies);
                Assert.IsTrue(responseContent.Contains(ParentPageTitle + "<"), "The parent page was not found");
                Assert.IsTrue(responseContent.Contains(ChildPageTitle + "<"), "The child page was not found");
            }

            pageManager.UnpublishPage(child.GetPageData());
            pageManager.SaveChanges();

            using (new AuthenticateUserRegion(null))
            {
                var responseContent = this.GetResponse(url, cookies);
                Assert.IsTrue(responseContent.Contains(ParentPageTitle + "<"), "The parent page was not found");
                Assert.IsFalse(responseContent.Contains(ChildPageTitle + "<"), "The child page was found");
            }

            var bag = new Dictionary<string, string>();
            bag.Add("ContentType", child.GetType().FullName);
            WorkflowManager.MessageWorkflow(childPageId, child.GetType(), null, "Publish", false, bag);

            using (new AuthenticateUserRegion(null))
            {
                var responseContent = this.GetResponse(url, cookies);
                Assert.IsTrue(responseContent.Contains(ParentPageTitle + "<"), "The parent page was not found");
                Assert.IsTrue(responseContent.Contains(ChildPageTitle + "<"), "The child page was not found");
            }
        }

        /// <summary>
        /// Checks if the navigation widget properly invalidates the cached page if a grouped page child is republished.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Unpublish"), Test]
        [Ignore("Unstable")]
        [Category(TestCategories.Navigation)]
        [Author(FeatherTeams.FeatherTeam)]
        [Description("Checks if the navigation widget properly invalidates the cached page if a grouped page child is republished")]
        public void NavigationWidget_ValidatePagesCacheDependenciesOnGroupPagePublishUnpublish()
        {
            const string GroupPageTitle = "GroupPage";
            const string ChildPageTitle = "ChildPage";

            string url = UrlPath.ResolveAbsoluteUrl("~/" + UrlNamePrefix + Index);

            var mvcProxy = new MvcControllerProxy() { ControllerName = typeof(NavigationController).FullName, Settings = new ControllerSettings(new NavigationController() { LevelsToInclude = 2 }) };
            var paretnPageId = this.pageOperations.CreatePageWithControl(mvcProxy, PageNamePrefix, PageTitlePrefix, UrlNamePrefix, Index);
            this.createdPageIDs.Add(paretnPageId);

            var pageGenerator = new Telerik.Sitefinity.TestIntegration.Data.Content.PageContentGenerator();
            var pageManager = PageManager.GetManager();

            var parentPageId = pageGenerator.CreatePage(GroupPageTitle, GroupPageTitle, GroupPageTitle, action: (n, d) => n.NodeType = NodeType.Group);
            this.createdPageIDs.Add(parentPageId);

            var childPageId = pageGenerator.CreatePage(ChildPageTitle, ChildPageTitle, ChildPageTitle);
            this.createdPageIDs.Add(childPageId);

            var parent = pageManager.GetPageNode(parentPageId);
            var child = pageManager.GetPageNode(childPageId);
            child.Parent = parent;
            pageManager.SaveChanges();

            var cookies = new CookieContainer();
            using (new AuthenticateUserRegion(null))
            {
                var responseContent = this.GetResponse(url, cookies);
                Assert.IsTrue(responseContent.Contains(GroupPageTitle + "<"), "The group page was not found");
                Assert.IsTrue(responseContent.Contains(ChildPageTitle + "<"), "The child page was not found");
            }

            pageManager.UnpublishPage(child.GetPageData());
            pageManager.SaveChanges();

            using (new AuthenticateUserRegion(null))
            {
                var responseContent = this.GetResponse(url, cookies);
                Assert.IsFalse(responseContent.Contains(GroupPageTitle + "<"), "The group page was found");
                Assert.IsFalse(responseContent.Contains(ChildPageTitle + "<"), "The child page was found");
            }

            var bag = new Dictionary<string, string>();
            bag.Add("ContentType", child.GetType().FullName);
            WorkflowManager.MessageWorkflow(childPageId, child.GetType(), null, "Publish", false, bag);

            using (new AuthenticateUserRegion(null))
            {
                var responseContent = this.GetResponse(url, cookies);
                Assert.IsTrue(responseContent.Contains(GroupPageTitle + "<"), "The group page was not found");
                Assert.IsTrue(responseContent.Contains(ChildPageTitle + "<"), "The child page was not found");
            }
        }

        private string GetResponse(string url, CookieContainer cookies)
        {
            var handler = new HttpClientHandler() { CookieContainer = cookies, UseCookies = true };
            var client = new HttpClient(handler) { Timeout = TimeSpan.FromMilliseconds(1000 * 60 * 120) };
            return client.GetStringAsync(url).Result;
        }

        #region Fields and constants

        private List<Guid> createdPageIDs = new List<Guid>();
        private PagesOperations pageOperations = new PagesOperations();
        private const string PageNamePrefix = "NavigationPage";
        private const string PageTitlePrefix = "Navigation Page";
        private const string UrlNamePrefix = "navigation-page";
        private const int Index = 0;

        #endregion
    }
}
