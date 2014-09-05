using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Web.UI;
using MbUnit.Framework;
using News.Mvc.Controllers;
using Telerik.Sitefinity.Mvc.Proxy;
using Telerik.Sitefinity.TestIntegration.Data.Content;
using Telerik.Sitefinity.Web;

namespace FeatherWidgets.TestIntegration.News
{
    /// <summary>
    /// This is a class with News tests.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1063:ImplementIDisposableCorrectly"), TestFixture]
    [Description("This is a class with News tests for list settings.")]
    public class NewsWidgetListSettingsTests : IDisposable
    {
        /// <summary>
        /// Set up method
        /// </summary>
        [SetUp]
        public void Setup()
        {
            this.locationGenerator = new PageContentGenerator();
        }

        /// <summary>
        /// Clean up method
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1063:ImplementIDisposableCorrectly")]
        public void Dispose()
        {
            this.locationGenerator.Dispose();
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// News widget - test Use paging functionality 
        /// </summary>
        [Test]
        [Category(TestCategories.News)]
        public void NewsWidget_VerifyUsePagingFunctionality()
        {
            string testName = System.Reflection.MethodInfo.GetCurrentMethod().Name;
            string pageNamePrefix = testName + "NewsPage";
            string pageTitlePrefix = testName + "News Page";
            string urlNamePrefix = testName + "news-page";
            int index = 1;
            string index2 = "/2";
            string url = UrlPath.ResolveAbsoluteUrl("~/" + urlNamePrefix + index);
            string url2 = UrlPath.ResolveAbsoluteUrl("~/" + urlNamePrefix + index + index2);

            var mvcProxy = new MvcControllerProxy();
            mvcProxy.ControllerName = typeof(NewsController).FullName;
            var newsController = new NewsController();
            newsController.Model.ItemsPerPage = 3;
            mvcProxy.Settings = new Telerik.Sitefinity.Mvc.Proxy.ControllerSettings(newsController);

            try
            {
                this.CreatePageWithControl(mvcProxy, pageNamePrefix, pageTitlePrefix, urlNamePrefix, index);

                int newsCount = 5;

                for (int i = 1; i <= newsCount; i++)
                    Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.News().CreateNewsItem(NewsTitle + i);

                string responseContent = this.ExecuteWebRequest(url);
                string responseContent2 = this.ExecuteWebRequest(url2);

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
                Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.News().DeleteAllNews();
                this.Dispose();
            }
        }

        #region Helper methods

        /// <summary>
        /// Executes the web request.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <returns>The response content</returns>
        private string ExecuteWebRequest(string url)
        {
            var webResponse = this.GetResponse(url);

            return this.GetResponseContent(webResponse);
        }

        private string GetResponseContent(WebResponse webResponse)
        {
            string responseContent;

            using (var sr = new System.IO.StreamReader(webResponse.GetResponseStream(), System.Text.Encoding.UTF8))
            {
                responseContent = sr.ReadToEnd();
            }

            return responseContent;
        }

        private WebResponse GetResponse(string url)
        {
            var webRequest = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(url);
            webRequest.Timeout = 120 * 1000; // 120 sec
            webRequest.CachePolicy = new System.Net.Cache.RequestCachePolicy();
            var webResponse = (System.Net.HttpWebResponse)webRequest.GetResponse();

            return webResponse;
        }

        private void CreatePageWithControl(Control control, string pageNamePrefix, string pageTitlePrefix, string urlNamePrefix, int index)
        {
            var controls = new List<System.Web.UI.Control>();
            controls.Add(control);

            var pageId = this.locationGenerator.CreatePage(
                                    string.Format(CultureInfo.InvariantCulture, "{0}{1}", pageNamePrefix, index.ToString(CultureInfo.InvariantCulture)),
                                    string.Format(CultureInfo.InvariantCulture, "{0}{1}", pageTitlePrefix, index.ToString(CultureInfo.InvariantCulture)),
                                    string.Format(CultureInfo.InvariantCulture, "{0}{1}", urlNamePrefix, index.ToString(CultureInfo.InvariantCulture)));

            PageContentGenerator.AddControlsToPage(pageId, controls);
        }

        #endregion

        #region Fields and constants

        private PageContentGenerator locationGenerator;
        private const string NewsTitle = "Title";

        #endregion
    }
}
