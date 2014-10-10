using System;
using System.Linq;
using FeatherWidgets.TestUtilities.CommonOperations;
using MbUnit.Framework;
using News.Mvc.Controllers;
using News.Mvc.Models;
using Telerik.Sitefinity.Modules.News;
using Telerik.Sitefinity.Mvc.Proxy;
using Telerik.Sitefinity.News.Model;
using Telerik.Sitefinity.Web;

namespace FeatherWidgets.TestIntegration.News
{
    /// <summary>
    /// This is a class with News tests.
    /// </summary>
    [TestFixture]
    public class NewsWidgetListSettingsTests
    {
        /// <summary>
        /// Set up method
        /// </summary>
        [SetUp]
        public void Setup()
        {
            this.pageOperations = new PagesOperations();
        }

        /// <summary>
        /// Tears down.
        /// </summary>
        [TearDown]
        public void TearDown()
        {
            Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.News().DeleteAllNews();            
        }

        /// <summary>
        /// News widget - test Use paging functionality 
        /// </summary>
        [Test]
        [Category(TestCategories.News)]
        [Author("FeatherTeam")]
        public void NewsWidget_VerifyUsePagingFunctionality()
        {
            string testName = System.Reflection.MethodInfo.GetCurrentMethod().Name;
            string pageNamePrefix = testName + "NewsPage";
            string pageTitlePrefix = testName + "News Page";
            string urlNamePrefix = testName + "news-page";
            int index = 1;
            string index2 = "/2";
            int itemsPerPage = 3;
            string url = UrlPath.ResolveAbsoluteUrl("~/" + urlNamePrefix + index);
            string url2 = UrlPath.ResolveAbsoluteUrl("~/" + urlNamePrefix + index + index2);

            var mvcProxy = new MvcControllerProxy();
            mvcProxy.ControllerName = typeof(NewsController).FullName;
            var newsController = new NewsController();
            newsController.Model.ItemsPerPage = itemsPerPage;
            mvcProxy.Settings = new Telerik.Sitefinity.Mvc.Proxy.ControllerSettings(newsController);

            try
            {
                this.pageOperations.CreatePageWithControl(mvcProxy, pageNamePrefix, pageTitlePrefix, urlNamePrefix, index);

                int newsCount = 5;

                for (int i = 1; i <= newsCount; i++)
                    Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.News().CreateNewsItem(NewsTitle + i);

                string responseContent = PageInvoker.ExecuteWebRequest(url);
                string responseContent2 = PageInvoker.ExecuteWebRequest(url2);

                for (int i = 1; i <= newsCount; i++)
                {
                    if (i <= 2)
                    {
                        Assert.IsFalse(responseContent.Contains(NewsTitle + i), "The news with this title was found!");
                    }
                    else
                    {
                        Assert.IsTrue(responseContent.Contains(NewsTitle + i), "The news with this title was not found!");
                    }
                }

                for (int i = 1; i <= newsCount; i++)
                {
                    if (i <= 2)
                    {
                        Assert.IsTrue(responseContent2.Contains(NewsTitle + i), "The news with this title was not found!");
                    }
                    else
                    {
                        Assert.IsFalse(responseContent2.Contains(NewsTitle + i), "The news with this title was found!");
                    }
                }
            }
            finally
            {
                this.pageOperations.DeletePages();
            }
        }

        /// <summary>
        /// News widget - test No limit and paging functionality 
        /// </summary>
        [Test]
        [Category(TestCategories.News)]
        [Author("FeatherTeam")]
        public void NewsWidget_VerifyNoLimitAndPagingFunctionality()
        {
            string testName = System.Reflection.MethodInfo.GetCurrentMethod().Name;
            string pageNamePrefix = testName + "NewsPage";
            string pageTitlePrefix = testName + "News Page";
            string urlNamePrefix = testName + "news-page";
            int index = 1;
            string url = UrlPath.ResolveAbsoluteUrl("~/" + urlNamePrefix + index);

            var mvcProxy = new MvcControllerProxy();
            mvcProxy.ControllerName = typeof(NewsController).FullName;
            var newsController = new NewsController();
            newsController.Model.DisplayMode = ListDisplayMode.All;
            mvcProxy.Settings = new Telerik.Sitefinity.Mvc.Proxy.ControllerSettings(newsController);

            try
            {
                this.pageOperations.CreatePageWithControl(mvcProxy, pageNamePrefix, pageTitlePrefix, urlNamePrefix, index);

                int newsCount = 25;

                for (int i = 1; i <= newsCount; i++)
                    Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.News().CreateNewsItem(NewsTitle + i);

                string responseContent = PageInvoker.ExecuteWebRequest(url);

                for (int i = 1; i <= newsCount; i++)
                    Assert.IsTrue(responseContent.Contains(NewsTitle + i), "The news with this title was not found!");
            }
            finally
            {
                this.pageOperations.DeletePages();
            }
        }

        /// <summary>
        /// News widget - test Use limit functionality 
        /// </summary>
        [Test]
        [Category(TestCategories.News)]
        [Author("FeatherTeam")]
        public void NewsWidget_VerifyUseLimitFunctionality()
        {
            string testName = System.Reflection.MethodInfo.GetCurrentMethod().Name;
            string pageNamePrefix = testName + "NewsPage";
            string pageTitlePrefix = testName + "News Page";
            string urlNamePrefix = testName + "news-page";
            int index = 1;
            int itemsPerPage = 3;
            string url = UrlPath.ResolveAbsoluteUrl("~/" + urlNamePrefix + index);

            var mvcProxy = new MvcControllerProxy();
            mvcProxy.ControllerName = typeof(NewsController).FullName;
            var newsController = new NewsController();
            newsController.Model.DisplayMode = ListDisplayMode.Limit;
            newsController.Model.ItemsPerPage = itemsPerPage;
            mvcProxy.Settings = new Telerik.Sitefinity.Mvc.Proxy.ControllerSettings(newsController);

            try
            {
                this.pageOperations.CreatePageWithControl(mvcProxy, pageNamePrefix, pageTitlePrefix, urlNamePrefix, index);

                int newsCount = 5;

                for (int i = 1; i <= newsCount; i++)
                    Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.News().CreateNewsItem(NewsTitle + i);

                string responseContent = PageInvoker.ExecuteWebRequest(url);

                for (int i = 5; i <= itemsPerPage; i--)
                    Assert.IsTrue(responseContent.Contains(NewsTitle + i), "The news with this title was not found!");
            }
            finally
            {
                this.pageOperations.DeletePages();
            }
        }

        /// <summary>
        /// News widget - test Sort news functionality 
        /// </summary>
        [Test]
        [Category(TestCategories.News)]
        [Author("FeatherTeam")]
        public void NewsWidget_VerifySortNewsAscending()
        {
            string sortExpession = "Title ASC";
            string[] newsTitles = { "Cat", "Boat", "Angel" };

            var newsController = new NewsController();
            newsController.Model.DisplayMode = ListDisplayMode.Limit;
            newsController.Model.SortExpression = sortExpession;

            for (int i = 0; i < newsTitles.Length; i++)
                Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.News().CreateNewsItem(newsTitles[i]);

            newsController.Index(null);

            int lastIndex = newsTitles.Length - 1;
            for (int i = 0; i < newsTitles.Length; i++)
            { 
                Assert.IsTrue(newsController.Model.Items[i].Title.Value.Equals(newsTitles[lastIndex]), "The news with this title was not found!");
                lastIndex--;
            }
        }

        [Test]
        [Category(TestCategories.News)]
        [Author("FeatherTeam")]
        public void NewsWidget_VerifySortNewsDescending()
        {
            string sortExpession = "Title DESC";
            string[] newsTitles = { "Cat", "Boat", "Angel" };

            var newsController = new NewsController();
            newsController.Model.DisplayMode = ListDisplayMode.Limit;
            newsController.Model.SortExpression = sortExpession;

            for (int i = 0; i < newsTitles.Length; i++)
                Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.News().CreateNewsItem(newsTitles[i]);

            newsController.Index(null);

            int lastIndex = 0;
            for (int i = 0; i < newsTitles.Length; i++)
            {
                Assert.IsTrue(newsController.Model.Items[i].Title.Value.Equals(newsTitles[lastIndex]), "The news with this title was not found!");
                lastIndex++;
            }
        }

        [Test]
        [Category(TestCategories.News)]
        [Author("FeatherTeam")]
        public void NewsWidget_VerifySortNewsPublicationDateDescending()
        {
            string sortExpession = "PublicationDate DESC";
            string[] newsTitles = { "Cat", "Boat", "Angel" };

            var newsController = new NewsController();
            newsController.Model.DisplayMode = ListDisplayMode.Limit;
            newsController.Model.SortExpression = sortExpession;

            for (int i = 0; i < newsTitles.Length; i++)
                Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.News().CreateNewsItem(newsTitles[i]);

            newsController.Index(null);

            int lastIndex = newsTitles.Length - 1;

            var newsManager = NewsManager.GetManager();
            var newsItems = newsManager.GetNewsItems().Where<NewsItem>(ni => ni.Status == Telerik.Sitefinity.GenericContent.Model.ContentLifecycleStatus.Master);
            
            foreach (NewsItem item in newsItems)
            {
                Assert.IsTrue(newsController.Model.Items[lastIndex].PublicationDate.Equals(item.PublicationDate), "The news with this title was not found!");
                lastIndex--;
            }
        }

        [Test]
        [Category(TestCategories.News)]
        [Author("FeatherTeam")]
        public void NewsWidget_VerifySortNewsLastModifiedDateDescending()
        {
            string sortExpession = "LastModified DESC";
            string[] newsTitles = { "Cat", "Boat", "Angel" };

            var newsController = new NewsController();
            newsController.Model.DisplayMode = ListDisplayMode.Limit;
            newsController.Model.SortExpression = sortExpession;
            var newsManager = NewsManager.GetManager();

            for (int i = 0; i < newsTitles.Length; i++)
                Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.News().CreateNewsItem(newsTitles[i]);

            NewsItem modified = newsManager.GetNewsItems().Where<NewsItem>(ni => ni.Status == Telerik.Sitefinity.GenericContent.Model.ContentLifecycleStatus.Master && ni.Title == "Boat").FirstOrDefault();

            NewsItem temp = newsManager.Lifecycle.CheckOut(modified) as NewsItem;

            temp.Title = "BoatNew";

            modified = newsManager.Lifecycle.CheckIn(temp) as NewsItem;

            var newsItem = newsManager.GetNewsItem(modified.Id);
            newsManager.Lifecycle.Publish(newsItem);
            newsManager.SaveChanges();

            newsController.Index(null);
    
            Assert.IsTrue(newsController.Model.Items[0].LastModified.Equals(modified.LastModified), "The news with this title was not found!");
            Assert.IsTrue(newsController.Model.Items[0].Title.Value.Equals(modified.Title.Value), "The news with this title was not found!");
            Assert.IsTrue(newsController.Model.Items[1].Title.Value.Equals(newsTitles[2]), "The news with this title was not found!");
            Assert.IsTrue(newsController.Model.Items[2].Title.Value.Equals(newsTitles[0]), "The news with this title was not found!");
        }

        #region Fields and constants

        private const string NewsTitle = "Title";
        private PagesOperations pageOperations;
        
        #endregion
    }
}