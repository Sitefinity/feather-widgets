using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace FeatherWidgets.TestUtilities.CommonOperations
{
    /// <summary>
    /// This class contains logic for invoking web pages.
    /// </summary>
    public static class PageInvoker
    {
        /// <summary>
        /// Executes a web request with http GET method.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="setAuthorization">Indicates whether to set the request authorization headers from the current HttpContext Request authorization headers</param>
        /// <returns>The response content</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings", MessageId = "0#"),
            System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed")]
        public static string ExecuteWebRequest(string url, bool setAuthorization = true)
        {
            var webResponse = PageInvoker.GetResponse(url, "GET", null, setAuthorization);

            return PageInvoker.GetResponseContent(webResponse);
        }

        /// <summary>
        /// Executes a web request with POST http method.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="setAuthorization">Indicates whether to set the request authorization headers from the current HttpContext Request authorization headers</param>
        /// <returns>The response content</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings", MessageId = "0#"),
            System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed")]
        public static string PostWebRequest(string url, string parameters, bool setAuthorization = true)
        {
            var webResponse = PageInvoker.GetResponse(url, "POST", parameters, setAuthorization);

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

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed")]
        private static WebResponse GetResponse(string url, string method, string postParameters, bool setAuthorization)
        {
            var webRequest = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(url);
            webRequest.Timeout = 120 * 1000; // 120 sec
            webRequest.CookieContainer = new CookieContainer();

            if (setAuthorization)
            {
                ////Legacy code
                ////set authorization from the current request context
                webRequest.Headers["Authorization"] = HttpContext.Current.Request.Headers["Authorization"];
            }
            else
            {
                ////check for PageInvokerRegion
                if (PageInvokerRegion.Current != null)
                {
                    ////inject PageInvokerRegion data into the request
                    webRequest.CookieContainer.Add(PageInvokerRegion.Current.Cookies);
                    webRequest.Referer = PageInvokerRegion.Current.Referer;
                }
            }
            
            webRequest.CachePolicy = new System.Net.Cache.RequestCachePolicy();
            webRequest.Method = method;
            switch (method)
            {
                case "POST":
                    webRequest.ContentType = "application/x-www-form-urlencoded";
                    break;
            }

            ////write postParameters in the web request stream
            if (postParameters != null)
            {
                byte[] bytes = System.Text.Encoding.ASCII.GetBytes(postParameters);
                webRequest.ContentLength = bytes.Length;
                System.IO.Stream webRequestStream = webRequest.GetRequestStream();
                webRequestStream.Write(bytes, 0, bytes.Length);
                webRequestStream.Close();
            }

            var webResponse = (System.Net.HttpWebResponse)webRequest.GetResponse();

            if (!setAuthorization && PageInvokerRegion.Current != null)
            {
                ////pre-peserve the respose data for future PageInvokerRegion requests
                PageInvokerRegion.Current.UpdateCookies(webResponse.Cookies);
                PageInvokerRegion.Current.Referer = url;
            }

            return webResponse;
        }
    }
}
