using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    /// This is a class with News tests - Advanced settings.
    /// </summary>
    [TestFixture]
    [Description("This is a class with News tests for advanced settings.")]
    public class NewsWidgetAdvancedSettingsTests
    {
        /// <summary>
        /// News widget - Social share functionality
        /// </summary>
        [Test]
        [Category(TestCategories.News)]
        [Author("FeatherTeam")]
        public void NewsWidget_SocialShareButtonsFunctionality()
        {
            string socialShare = "list-inline s-social-share-list";
            string testName = System.Reflection.MethodInfo.GetCurrentMethod().Name;
            string pageNamePrefix = testName + "NewsPage";
            string pageTitlePrefix = testName + "News Page";
            string urlNamePrefix = testName + "news-page";
            int index = 1;
            var newsManager = NewsManager.GetManager();
            string url = UrlPath.ResolveAbsoluteUrl("~/" + urlNamePrefix + index);

            var mvcProxy = new MvcControllerProxy();
            mvcProxy.ControllerName = typeof(NewsController).FullName;
            var newsController = new NewsController();
            newsController.Model.EnableSocialSharing = true;
            mvcProxy.Settings = new ControllerSettings(newsController);

            try
            {
                this.pageOperations.CreatePageWithControl(mvcProxy, pageNamePrefix, pageTitlePrefix, urlNamePrefix, index);

                Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.News().CreateNewsItem(NewsTitle);

                NewsItem newsItem = newsManager.GetNewsItems().Where<NewsItem>(ni => ni.Status == Telerik.Sitefinity.GenericContent.Model.ContentLifecycleStatus.Master && ni.Title == NewsTitle).FirstOrDefault();

                string detailNewsUrl = url + newsItem.ItemDefaultUrl;

                string responseContent = PageInvoker.ExecuteWebRequest(detailNewsUrl);

                Assert.IsTrue(responseContent.Contains(socialShare), "Social share button was not found!");
                Assert.IsTrue(responseContent.Contains(NewsTitle), "The news with this title was not found!");
            }
            finally
            {
                this.pageOperations.DeletePages();
                Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.News().DeleteAllNews();
            }
        }

        #region Fields and constants
        
        private const string NewsTitle = "Title";
        private PagesOperations pageOperations = new PagesOperations();

        #endregion
    }
}
