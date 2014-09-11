using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace FeatherWidgets.TestUtilities.CommonOperations
{
    /// <summary>
    /// This class contains logic for invoking web pages.
    /// </summary>
    public static class PageInvoker
    {
        /// <summary>
        /// Executes the web request.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <returns>The response content</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings", MessageId = "0#")]
        public static string ExecuteWebRequest(string url)
        {
            var webResponse = PageInvoker.GetResponse(url);

            return PageInvoker.GetResponseContent(webResponse);
        }

        private static string GetResponseContent(WebResponse webResponse)
        {
            string responseContent;

            using (var sr = new System.IO.StreamReader(webResponse.GetResponseStream(), System.Text.Encoding.UTF8))
            {
                responseContent = sr.ReadToEnd();
            }

            return responseContent;
        }

        private static WebResponse GetResponse(string url)
        {
            var webRequest = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(url);
            webRequest.Timeout = 120 * 1000; // 120 sec
            webRequest.CachePolicy = new System.Net.Cache.RequestCachePolicy();
            var webResponse = (System.Net.HttpWebResponse)webRequest.GetResponse();

            return webResponse;
        }
    }
}
