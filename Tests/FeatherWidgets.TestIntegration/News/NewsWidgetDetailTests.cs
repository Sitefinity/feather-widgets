using System.IO;
using System.Linq;
using System.Web.Mvc;
using FeatherWidgets.TestUtilities.CommonOperations;
using FeatherWidgets.TestUtilities.CommonOperations.Templates;
using MbUnit.Framework;
using Telerik.Sitefinity.Frontend.News.Mvc.Controllers;
using Telerik.Sitefinity.Frontend.News.Mvc.Models;
using Telerik.Sitefinity.Frontend.TestUtilities;
using Telerik.Sitefinity.Frontend.TestUtilities.CommonOperations.ActionFilters;
using Telerik.Sitefinity.Modules.News;
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
            Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.Pages().DeleteAllPages();
        }

        /// <summary>
        /// News widget - test whether single item view is displayed in the same page.
        /// </summary>
        [Test]
        [Category(TestCategories.News)]
        [Author(FeatherTeams.FeatherTeam)]
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

            var items = newsController.Model.CreateListViewModel(null, 1).Items.ToArray();

            Assert.AreEqual(this.newsCount, items.Length, "The count of the news item is not as expected");

            var expectedDetailNews = (NewsItem)items[0].DataItem;

            string detailNewsUrl = pageUrl + expectedDetailNews.ItemDefaultUrl;

            ActionExecutionRegister.ExecutedActionInfos.Clear();
            PageInvoker.ExecuteWebRequest(detailNewsUrl);

            this.AssertDetailActionInvokation(expectedDetailNews);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling"), Test]
        [Category(TestCategories.News)]
        [Author(FeatherTeams.FeatherTeam)]
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

            this.pageOperations.CreatePageWithControl(mvcProxy, pageNamePrefix, pageTitlePrefix, urlNamePrefix, firstPageIndex);
            var secondPageId = this.pageOperations.CreatePageWithControl(mvcProxy, pageNamePrefix, pageTitlePrefix, urlNamePrefix, secondPageIndex);
            
            var newsController = new NewsController();
            newsController.OpenInSamePage = false;
            newsController.DetailsPageId = secondPageId;
            mvcProxy.Settings = new ControllerSettings(newsController);

            newsController.Index(null);
            Assert.AreEqual(secondPageId, newsController.ViewBag.DetailsPageID, "The second page ID is not provided correctly to the list view");

            var items = newsController.Model.CreateListViewModel(null, 1).Items.ToArray();
            Assert.AreEqual(this.newsCount, items.Length, "The count of the news item is not as expected");

            var expectedDetailNews = (NewsItem)items[0].DataItem;
            string detailNewsUrl = secondPageUrl + expectedDetailNews.ItemDefaultUrl;

            ActionExecutionRegister.ExecutedActionInfos.Clear();
            PageInvoker.ExecuteWebRequest(detailNewsUrl);

            this.AssertDetailActionInvokation(expectedDetailNews);
        }

        [Test]
        [Category(TestCategories.News)]
        [Author(FeatherTeams.FeatherTeam)]
        public void NewsWidget_SelectDetailTemplate()
        {
            string testName = System.Reflection.MethodInfo.GetCurrentMethod().Name;
            string pageNamePrefix = testName + "NewsPage";
            string pageTitlePrefix = testName + "News Page";
            string urlNamePrefix = testName + "news-page";
            int pageIndex = 1;
            string textEdited = "<p> Test paragraph </p>";
            string paragraphText = "Test paragraph";
            var newsManager = NewsManager.GetManager();
            string url = UrlPath.ResolveAbsoluteUrl("~/" + urlNamePrefix + pageIndex);

            string detailTemplate = "DetailPageNew";
            var detailTemplatePath = Path.Combine(this.templateOperation.SfPath, "ResourcePackages", "Bootstrap", "MVC", "Views", "News", "Detail.DetailPage.cshtml");
            var newDetailTemplatePath = Path.Combine(this.templateOperation.SfPath, "MVC", "Views", "Shared", "Detail.DetailPageNew.cshtml");

            try
            {
                File.Copy(detailTemplatePath, newDetailTemplatePath);
                this.EditFile(newDetailTemplatePath, textEdited);

                var mvcProxy = new MvcControllerProxy();
                mvcProxy.ControllerName = typeof(NewsController).FullName;
                var newsController = new NewsController();
                newsController.DetailTemplateName = detailTemplate;
                mvcProxy.Settings = new ControllerSettings(newsController);

                this.pageOperations.CreatePageWithControl(mvcProxy, pageNamePrefix, pageTitlePrefix, urlNamePrefix, pageIndex);

                NewsItem newsItem = newsManager.GetNewsItems().Where<NewsItem>(ni => ni.Status == Telerik.Sitefinity.GenericContent.Model.ContentLifecycleStatus.Master && ni.Title == NewsTitleDetail).FirstOrDefault();
                string detailNewsUrl = url + newsItem.ItemDefaultUrl;

                string responseContent = PageInvoker.ExecuteWebRequest(detailNewsUrl);

                Assert.IsTrue(responseContent.Contains(NewsTitleDetail), "The news with this title was not found!");
                Assert.IsTrue(responseContent.Contains(paragraphText), "The news with this template was not found!");
            }
            finally
            {
                File.Delete(newDetailTemplatePath);
            }
        }

        #region Helper methods

        private void AssertDetailActionInvokation(NewsItem expectedDetailNews)
        {
            var newsActionInfos = ActionExecutionRegister.ExecutedActionInfos.Where(a => (string)a.ActionRouteData.Values["controller"] == "News").ToArray();
            Assert.AreEqual(1, newsActionInfos.Length, "The action has been executed.");
            var actionInfo = newsActionInfos[0];
            Assert.AreEqual("Details", actionInfo.Name, "The correct action has not been invoked.");
            var newsItem = (NewsItem)actionInfo.ActionRouteData.Values["newsItem"];
            Assert.IsNotNull(newsItem, "The news item is not provided correctly.");
            Assert.AreEqual(expectedDetailNews.Id, newsItem.Id, "The correct news item is not provided");
        }

        private void EditFile(string newDetailTemplatePath, string textEdited)
        {
            using (StreamWriter output = File.AppendText(newDetailTemplatePath))
            {
                output.WriteLine(textEdited);
            }        
        }

        #endregion

        #region Fields and constants

        private ExecutionRegistrationFilterAttribute actionFilter;
        private const string NewsTitle = "Title";
        private const string NewsTitleDetail = "Title1";
        private int newsCount = 3;
        private PagesOperations pageOperations;
        private TemplateOperations templateOperation = new TemplateOperations();

        #endregion
    }
}
