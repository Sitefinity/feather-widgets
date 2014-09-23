using System.Web.Mvc;
using FeatherWidgets.TestUtilities.CommonOperations;
using MbUnit.Framework;
using News.Mvc.Controllers;
using News.Mvc.Models;
using Telerik.Sitefinity.Frontend.TestUtilities.CommonOperations.ActionFilters;
using Telerik.Sitefinity.Mvc.Proxy;
using Telerik.Sitefinity.News.Model;
using Telerik.Sitefinity.TestUtilities;
using Telerik.Sitefinity.Web;

namespace FeatherWidgets.TestIntegration.News
{
    [TestFixture]
    [Description("This is class contains test for news widget details functionality.")]
    public class NewsWidgetDetailTests
    {
        /// <summary>
        /// Set up method
        /// </summary>
        [SetUp]
        public void Setup()
        {
            this.actionFilter = new ExecutionRegistrationFilterAttribute();
            GlobalFilters.Filters.Add(this.actionFilter);
            this.pageOperations = new PagesOperations();

            for (int i = 1; i <= this.newsCount; i++)
                Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.News().CreateNewsItem(NewsWidgetDetailTests.NewsTitle + i);
        }

        /// <summary>
        /// Tears down.
        /// </summary>
        [TearDown]
        public void TearDown()
        {
            GlobalFilters.Filters.Remove(this.actionFilter);
            ActionExecutionRegister.ExecutedActionInfos.Clear();

            Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.News().DeleteAllNews();
            this.pageOperations.DeletePages();
        }

        /// <summary>
        /// News widget - test whether single item view is displayed in the same page.
        /// </summary>
        [Test]
        [Category(TestCategories.News)]
        [Author(TestAuthor.Team2)]
        [Description("Verifies that open single item in the same page functionality resolves the correct page.")]
        public void NewsWidget_VerifyOpenSingleItemInSamePage()
        {
            string testName = System.Reflection.MethodInfo.GetCurrentMethod().Name;
            string pageNamePrefix = testName + "NewsPage";
            string pageTitlePrefix = testName + "News Page";
            string urlNamePrefix = testName + "news-page";
            int pageIndex = 1;
            string pageUrl = UrlPath.ResolveAbsoluteUrl("~/" + urlNamePrefix + pageIndex);

            var mvcProxy = new MvcControllerProxy();
            mvcProxy.ControllerName = typeof(NewsController).FullName;
            var newsController = new NewsController();
            newsController.OpenInSamePage = true;
            mvcProxy.Settings = new ControllerSettings(newsController);

            this.pageOperations.CreatePageWithControl(mvcProxy, pageNamePrefix, pageTitlePrefix, urlNamePrefix, pageIndex);

            newsController.Index(1);

            Assert.AreEqual(this.newsCount, newsController.Model.Items.Count, "The count of the news item is not as expected");

            var expectedDetailNews = newsController.Model.Items[0];
            string detailNewsUrl = pageUrl + expectedDetailNews.ItemDefaultUrl;

            ActionExecutionRegister.ExecutedActionInfos.Clear();
            PageInvoker.ExecuteWebRequest(detailNewsUrl);

            this.AssertDetailActionInvokation(expectedDetailNews);
        }

        [Test]
        [Category(TestCategories.News)]
        [Author(TestAuthor.Team2)]
        [Description("Verifies that open single item in the existing page functionality resolves the correct page.")]
        public void NewsWidget_VerifyOpenSingleItemInCustomPage()
        {
            string testName = "VerifyOpenSingleItemInCustomPage";
            string pageNamePrefix = testName + "NewsPage";
            string pageTitlePrefix = testName + "News Page";
            string urlNamePrefix = testName + "news-page";
            int firstPageIndex = 1;
            int secondPageIndex = 2;
            string secondPageUrl = UrlPath.ResolveAbsoluteUrl("~/" + urlNamePrefix + secondPageIndex);

            var mvcProxy = new MvcControllerProxy();
            mvcProxy.ControllerName = typeof(NewsController).FullName;
            var newsController = new NewsController();
            newsController.OpenInSamePage = false;
            newsController.DetailsPageUrl = secondPageUrl;
            mvcProxy.Settings = new ControllerSettings(newsController);

            this.pageOperations.CreatePageWithControl(mvcProxy, pageNamePrefix, pageTitlePrefix, urlNamePrefix, firstPageIndex);
            this.pageOperations.CreatePageWithControl(mvcProxy, pageNamePrefix, pageTitlePrefix, urlNamePrefix, secondPageIndex);

            newsController.Index(1);

            Assert.AreEqual(secondPageUrl, newsController.ViewBag.DetailsPageUrl, "The second page URL is not provided correctly to the list view");
            Assert.AreEqual(this.newsCount, newsController.Model.Items.Count, "The count of the news item is not as expected");

            var expectedDetailNews = newsController.Model.Items[0];
            string detailNewsUrl = secondPageUrl + expectedDetailNews.ItemDefaultUrl;

            ActionExecutionRegister.ExecutedActionInfos.Clear();
            PageInvoker.ExecuteWebRequest(detailNewsUrl);

            this.AssertDetailActionInvokation(expectedDetailNews);
        }

        #region Helper methods

        private void AssertDetailActionInvokation(NewsItem expectedDetailNews)
        {
            Assert.AreEqual(1, ActionExecutionRegister.ExecutedActionInfos.Count, "The action has been executed.");
            var actionInfo = ActionExecutionRegister.ExecutedActionInfos[0];
            Assert.AreEqual("Details", actionInfo.Name, "The correct action has not been invoked.");
            var newsItem = (NewsItem)actionInfo.ActionRouteData.Values["newsItem"];
            Assert.IsNotNull(newsItem, "The news item is not provided correctly.");
            Assert.AreEqual(expectedDetailNews.Id, newsItem.Id, "The correct news item is not provided");
            var viewModel = (actionInfo.Result as ViewResult).Model as INewsModel;
            Assert.AreEqual(expectedDetailNews.Id, viewModel.DetailItem.Id, "The news item id is not provided correctly to the DetailNews property");
        }

        #endregion

        #region Fields and constants

        private ExecutionRegistrationFilterAttribute actionFilter;
        private const string NewsTitle = "Title";
        private int newsCount = 3;
        private PagesOperations pageOperations;

        #endregion
    }
}
