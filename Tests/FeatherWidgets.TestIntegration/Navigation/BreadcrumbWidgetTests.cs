using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using FeatherWidgets.TestUtilities.CommonOperations;
using MbUnit.Framework;
using Telerik.Sitefinity;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Fluent.Pages;
using Telerik.Sitefinity.Frontend.DynamicContent.Mvc.Controllers;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers;
using Telerik.Sitefinity.Frontend.Navigation.Mvc.Controllers;
using Telerik.Sitefinity.Frontend.Navigation.Mvc.Models.Breadcrumb;
using Telerik.Sitefinity.Frontend.News.Mvc.Controllers;
using Telerik.Sitefinity.Frontend.TestUtilities;
using Telerik.Sitefinity.Mvc.Proxy;
using Telerik.Sitefinity.News.Model;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.TestIntegration.Core.SiteMap;
using Telerik.Sitefinity.TestIntegration.Data.Content;
using Telerik.Sitefinity.TestIntegration.Helpers;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.Web;
using CommonOperationsContext = Telerik.Sitefinity.TestUtilities.CommonOperations;

namespace FeatherWidgets.TestIntegration.Navigation
{
    [Description("Tests for the Breadcrumb model")]
    [TestFixture]
    public class BreadcrumbWidgetTests
    {
        /// <summary>
        /// Set up method
        /// </summary>
        [SetUp]
        public void Setup()
        {
            this.pageOperations = new PagesOperations();
        }

        [Test]
        [Category(TestCategories.Navigation)]
        [Author(FeatherTeams.SitefinityTeam7)]
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
        [Author(FeatherTeams.SitefinityTeam7)]
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
        [Author(FeatherTeams.SitefinityTeam7)]
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
        [Author(FeatherTeams.SitefinityTeam7)]
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
        [Author(FeatherTeams.SitefinityTeam7)]
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

        [Test]
        [Category(TestCategories.Navigation)]
        [Author(FeatherTeams.SitefinityTeam3)]
        [Description("Verifies that BreadcrumbModel works properly when we want bradcrumb from the home page to the current one with some restricted pages in the tree.")]
        public void BreadcrumbModel_FromHomeToCurrentPage_WithRestrictedPages()
        {
            this.CreateTestPages();

            ObjectFactory.RegisterSitemapNodeFilter<DummySitemapFilter>("DummyFilter");
            var restrictedPageIndex = 1;
            DummySitemapFilter.RestrictPageNode(this.createdPageIDs[restrictedPageIndex]);

            //// invalidates sitemap cache
            var inf = typeof(SiteMapBase).GetMethod("Reset", BindingFlags.Static | BindingFlags.NonPublic);
            inf.Invoke(null, null);

            using (var userReg = new CreateUserRegion("Viewer" + Guid.NewGuid().ToString(), false))
            {
                using (var profileReg = new CreateUserProfileRegion(userReg.User, "Viewer"))
                {
                    using (new AuthenticateUserRegion(userReg.User))
                    {
                        var model = new BreadcrumbModel();
                        var viewModel = model.CreateViewModel(null);
                        var skipStep = 0;

                        for (int i = 0; i < BreadcrumbWidgetTests.TestPagesCount; i++)
                        {
                            var expected = SitefinitySiteMap.GetCurrentProvider().FindSiteMapNodeFromKey(this.createdPageIDs[i].ToString());
                            if (i == restrictedPageIndex)
                            {
                                Assert.IsNull(expected);
                                skipStep--;
                                continue;
                            }

                            var breadcrumgIndex = i + skipStep;
                            var actual = viewModel.SiteMapNodes[breadcrumgIndex];
                            Assert.AreEqual(expected.Title, actual.Title);
                        }
                    }
                }
            }

            DummySitemapFilter.Clear();
        }

        [Test]
        [Category(TestCategories.Navigation)]
        [Author(FeatherTeams.SitefinityTeam7)]
        [Description("Verifies that virtual nodes are displayed in breadcrump when there are controllers with detail news item on the page.")]
        public void Breadcrumb_DetailNewsItem_VirtualNodes()
        {
            Guid pageId = Guid.Empty;
            try
            {
                string testName = System.Reflection.MethodInfo.GetCurrentMethod().Name;
                string pageNamePrefix = testName + "NewsPage";
                string pageTitlePrefix = testName + "News Page";
                string urlNamePrefix = testName + "news-page";
                int pageIndex = 1;
                string pageTitle = pageTitlePrefix + pageIndex;
                string pageUrl = UrlPath.ResolveAbsoluteUrl("~/" + urlNamePrefix + pageIndex);

                var mvcBreadcrumbWidget = this.CreateBreadcrumbMvcWidget();
                pageId = this.pageOperations.CreatePageWithControl(mvcBreadcrumbWidget, pageNamePrefix, pageTitlePrefix, urlNamePrefix, pageIndex);

                var newsMvcWidget = this.CreateNewsMvcWidget();
                PageContentGenerator.AddControlsToPage(pageId, new List<System.Web.UI.Control>() { newsMvcWidget });
               
                CommonOperationsContext.ServerOperations.News().CreatePublishedNewsItem(newsTitle: NewsTitle, newsContent: NewsTitle);
                var items = ((NewsController)newsMvcWidget.Controller).Model.CreateListViewModel(null, 1).Items.ToArray();
                var newsItem = (NewsItem)items[0].DataItem;
                string detailNewsUrl = pageUrl + newsItem.ItemDefaultUrl;
                var responseContent = PageInvoker.ExecuteWebRequest(detailNewsUrl);
                string relativeUrl = UrlPath.ResolveUrl("~/" + urlNamePrefix + pageIndex);
                string breadcrumbTemplate = @"<a[\w\s]{1,}href=""" + relativeUrl + @""">" + pageTitle + @"[\w\s]{1,}</a>[\w\s]{1,}<span>[\w\s]{1,}/[\w\s]{1,}</span>[\w\s]{1,}" + NewsTitle;
                Match match = Regex.Match(responseContent, breadcrumbTemplate, RegexOptions.IgnorePatternWhitespace & RegexOptions.Multiline);

                Assert.IsTrue(match.Success, "Breadcrumb does not contain selected news item as virtual node!");
            }
            finally
            {
                if (pageId != Guid.Empty)
                {
                    CommonOperationsContext.ServerOperations.Pages().DeletePage(pageId);
                }

                CommonOperationsContext.ServerOperations.News().DeleteNewsItem(NewsTitle);
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "FeatherWidgets.TestUtilities.CommonOperations.Templates.TemplateOperations.AddControlToTemplate(System.Guid,System.Web.UI.Control,System.String,System.String)"), Test]
        [Category(TestCategories.DynamicWidgets)]
        [Author(FeatherTeams.SitefinityTeam6)]
        [Description("Verifies that virtual nodes are displayed in breadcrump when there is a controller with detail dynamic item on the page")]
        public void Breadcrumb_DynamicWidget_VirtualNodes()
        {
            string dynamicTitle = "dynamic type title";
            string dynamicUrl = "dynamic type title";
            ServerOperationsFeather.DynamicModules().EnsureModuleIsImported(ModuleName, ModuleResource);
            ServerOperationsFeather.DynamicModulePressArticle().CreatePressArticleItem(dynamicTitle, dynamicUrl);
            var dynamicCollection = ServerOperationsFeather.DynamicModulePressArticle().RetrieveCollectionOfPressArticles();
           
            string testName = System.Reflection.MethodInfo.GetCurrentMethod().Name;
            string pageNamePrefix = testName + "DynamicPage";
            string pageTitlePrefix = testName + "Dynamic Page";
            string urlNamePrefix = testName + "dynamic-page";
            int pageIndex = 1;
            string pageTitle = pageTitlePrefix + pageIndex;
            Guid pageId = Guid.Empty;
            string pageUrl = UrlPath.ResolveAbsoluteUrl("~/" + urlNamePrefix + pageIndex);

            try
            {
                var mvcBreadcrumbWidget = this.CreateBreadcrumbMvcWidget();
                pageId = this.pageOperations.CreatePageWithControl(mvcBreadcrumbWidget, pageNamePrefix, pageTitlePrefix, urlNamePrefix, pageIndex);

                var mvcProxy = this.CreateDynamicMvcWidget();
                PageContentGenerator.AddControlsToPage(pageId, new List<System.Web.UI.Control>() { mvcProxy });

                string detailsUrl = pageUrl + dynamicCollection[0].ItemDefaultUrl;
                string responseContent = PageInvoker.ExecuteWebRequest(detailsUrl);
                
                string relativeUrl = UrlPath.ResolveUrl("~/" + urlNamePrefix + pageIndex);
                string breadcrumbTemplate = @"<a[\w\s]{1,}href=(?<url>""/""|""" + relativeUrl + @""")>" + pageTitle + @"[\w\s]{1,}</a>[\w\s]{1,}<span>[\w\s]{1,}/[\w\s]{1,}</span>[\w\s]{1,}" + dynamicTitle;
                Match match = Regex.Match(responseContent, breadcrumbTemplate, RegexOptions.IgnorePatternWhitespace & RegexOptions.Multiline);

                Assert.IsTrue(match.Success, "Breadcrumb does not contain selected dynamic item as virtual node!");
            }
            finally
            {
                if (pageId != Guid.Empty)
                {
                    CommonOperationsContext.ServerOperations.Pages().DeletePage(pageId);
                }

                ServerOperationsFeather.DynamicModulePressArticle().DeleteDynamicItems(dynamicCollection);
                Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.ModuleBuilder().DeleteAllModules(string.Empty, TransactionName);
            }
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
                var isGroupPage = groupPages && i < BreadcrumbWidgetTests.TestPagesCount - 1 && i != 0;
                var name = baseName + i;
                var pageId = TestUtils.CreateAndPublishPage(fluent, PageLocation.Frontend, name, name, name, parentPage, isGroupPage);
                this.createdPageIDs.Add(pageId);

                parentPage = fluent.Page(pageId).Get();
            }

            var pageNode = SitefinitySiteMap.GetCurrentProvider().FindSiteMapNodeFromKey(this.createdPageIDs.Last().ToString());
            SystemManager.CurrentHttpContext.Items[SiteMapBase.CurrentNodeKey] = pageNode;
        }

        private MvcWidgetProxy CreateBreadcrumbMvcWidget()
        {
            var mvcWidget = new MvcWidgetProxy();
            mvcWidget.ControllerName = typeof(BreadcrumbController).FullName;
            var breadcrumbController = new BreadcrumbController();
            mvcWidget.Settings = new ControllerSettings(breadcrumbController);
            breadcrumbController.Model.AllowVirtualNodes = true;

            return mvcWidget;
        }

        private MvcWidgetProxy CreateNewsMvcWidget()
        {
            var mvcWidget = new MvcWidgetProxy();
            mvcWidget.ControllerName = typeof(NewsController).FullName;
            var newsController = new NewsController();
            newsController.OpenInSamePage = true;
            mvcWidget.Settings = new ControllerSettings(newsController);

            return mvcWidget;
        }

        private MvcWidgetProxy CreateDynamicMvcWidget()
        {
            var mvcWidget = new MvcWidgetProxy();
            mvcWidget.ControllerName = typeof(DynamicContentController).FullName;
            var dynamicController = new DynamicContentController();
            dynamicController.Model.ContentType = TypeResolutionService.ResolveType(ResolveType);
            dynamicController.Model.ProviderName = FeatherWidgets.TestUtilities.CommonOperations.DynamicModulesOperations.ProviderName;
            mvcWidget.Settings = new ControllerSettings(dynamicController);
            mvcWidget.WidgetName = WidgetName;

            return mvcWidget;
        }

        private List<Guid> createdPageIDs = new List<Guid>();
        private const int TestPagesCount = 5;
        private PagesOperations pageOperations;
        private const string NewsTitle = "NewsTitle";
        private const string ResolveType = "Telerik.Sitefinity.DynamicTypes.Model.PressRelease.PressArticle";
        private const string WidgetName = "PressArticle";
        private const string ModuleName = "Press Release";
        private const string ModuleResource = "FeatherWidgets.TestUtilities.Data.DynamicModules.PressRelease.zip";
        private const string TransactionName = "Module Installations";
    }
}
