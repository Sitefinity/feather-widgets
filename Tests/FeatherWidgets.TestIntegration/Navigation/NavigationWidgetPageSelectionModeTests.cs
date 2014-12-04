﻿using System;
using System.Collections.Generic;
using FeatherWidgets.TestUtilities.CommonOperations;
using MbUnit.Framework;
using Navigation.Mvc.Controllers;
using Navigation.Mvc.Models;
using Telerik.Sitefinity;
using Telerik.Sitefinity.Fluent.Pages;
using Telerik.Sitefinity.Mvc.Proxy;
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
        [Author("FeatherTeam")]
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

        /// <summary>
        /// Navigation widget - Horizontal template and 5 levels to include
        /// </summary>
        [Test]
        [Category(TestCategories.Navigation)]
        [Author("FeatherTeam")]
        public void NavigationWidget_HorizontalTemplate5LevelsToInclude()
        {
            string url = UrlPath.ResolveAbsoluteUrl("~/" + UrlNamePrefix + Index);
            string navigationClass = "navbar navbar-default";

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

            Assert.IsTrue(responseContent.Contains(navigationClass), "The navigation with this css class was not found!");

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
        [Author("FeatherTeam")]
        public void NavigationWidget_VerticalTemplate5LevelsToInclude()
        {
            string url = UrlPath.ResolveAbsoluteUrl("~/" + UrlNamePrefix + Index);
            string navigationClass = "nav nav-pills nav-stacked";

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

            Assert.IsTrue(responseContent.Contains(navigationClass), "The navigation with this css class was not found!");

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
        [Author("FeatherTeam")]
        public void NavigationWidget_TabsTemplate5LevelsToInclude()
        {
            string url = UrlPath.ResolveAbsoluteUrl("~/" + UrlNamePrefix + Index);
            string navigationClass = "nav nav-tabs";

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

            Assert.IsTrue(responseContent.Contains(navigationClass), "The navigation with this css class was not found!");

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
        [Author("FeatherTeam")]
        public void NavigationWidget_SitemapTemplate5LevelsToInclude()
        {
            string url = UrlPath.ResolveAbsoluteUrl("~/" + UrlNamePrefix + Index);
            string navigationClass = "nav nav-sitemap";

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

            Assert.IsTrue(responseContent.Contains(navigationClass), "The navigation with this css class was not found!");

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
