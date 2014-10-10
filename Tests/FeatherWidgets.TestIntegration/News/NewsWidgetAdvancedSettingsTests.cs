using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FeatherWidgets.TestUtilities.CommonOperations;
using MbUnit.Framework;
using News.Mvc.Controllers;
using News.Mvc.Models;
using Telerik.Sitefinity.Mvc.Proxy;
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
        /// News widget - Social share functionality
        /// </summary>
        [Test]
        [Category(TestCategories.News)]
        public void NewsWidget_SocialShareButtonsFunctionality()
        {
            string pageNamePrefix = "NewsPage";
            string pageTitlePrefix = "News Page";
            string urlNamePrefix = "news-page";
            string newsTitle1 = "Title1";
            string newsTitle2 = "Title2";
            string socialShare = "list-inline s-social-share-list";
            int index = 1;
            string url = UrlPath.ResolveAbsoluteUrl("~/" + urlNamePrefix + index);

            var mvcProxy = new MvcControllerProxy();
            mvcProxy.ControllerName = typeof(NewsController).FullName;
            var newsController = new NewsController();
            newsController.Model.EnableSocialSharing = true;
            mvcProxy.Settings = new ControllerSettings(newsController);

            try
            {
                this.pageOperations.CreatePageWithControl(mvcProxy, pageNamePrefix, pageTitlePrefix, urlNamePrefix, index);

                Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.News().CreateNewsItem(newsTitle1);
                Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.News().CreateNewsItem(newsTitle2);

                newsController.Index(1);
                var expectedDetailNews = newsController.Model.Items[1];
                string detailNewsUrl = url + expectedDetailNews.ItemDefaultUrl;

                string responseContent = PageInvoker.ExecuteWebRequest(detailNewsUrl);

                Assert.IsTrue(responseContent.Contains(socialShare), "Social share button was not found!");
                Assert.IsTrue(responseContent.Contains(newsTitle1), "The news with this title was not found!");
                Assert.IsFalse(responseContent.Contains(newsTitle2), "The news with this title button was found!");
            }
            finally
            {
                this.pageOperations.DeletePages();
            }
        }

        #region Fields and constants

        private PagesOperations pageOperations;

        #endregion
    }
}
