using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using Microsoft.Http;
using Telerik.Sitefinity.TestIntegration.Helpers;

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
        /// <returns>The response content</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings", MessageId = "0#"),
            System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed")]
        public static string ExecuteWebRequest(string url)
        {
            var webResponse = PageInvoker.GetResponse(url, HttpMethod.GET, null);
            return PageInvoker.GetResponseContent(webResponse);
        }

        /// <summary>
        /// Executes a web request with POST http method.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <returns>The response content</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings", MessageId = "0#"),
            System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed")]
        public static string PostWebRequest(string url, string parameters)
        {
            var webResponse = PageInvoker.GetResponse(url, HttpMethod.POST, parameters);
            return PageInvoker.GetResponseContent(webResponse);
        }

        /// <summary>
        /// Executes a web request with PUT http method.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <returns>The response content</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings", MessageId = "0#"),
            System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed")]
        public static string PutWebRequest(string url, string parameters)
        {
            var webResponse = PageInvoker.GetResponse(url, HttpMethod.PUT, parameters);
            return PageInvoker.GetResponseContent(webResponse);
        }

        private static string GetResponseContent(HttpResponseMessage webResponse)
        {
            string responseContent;
            return webResponse.Content.ReadAsString();
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed")]
        private static HttpResponseMessage GetResponse(string url, HttpMethod method, string parameters)
        {
            var client = new SitefinityClient();
            client.RequestAuthenticate();
            var request = new HttpRequestMessage(method.ToString(), url);

            switch (method)
            {
                case HttpMethod.POST:
                    request.Headers.ContentType = "application/x-www-form-urlencoded";
                    break;

                case HttpMethod.PUT:
                    request.Headers.ContentType = "text/json";
                    break;
            }

            if (parameters != null)
            {
                byte[] bytes = System.Text.Encoding.ASCII.GetBytes(parameters);
                request.Headers.ContentLength = bytes.Length;
                request.Content = HttpContent.Create(bytes);
            }

            client.TransportSettings.MaximumAutomaticRedirections = 5;
            var response = client.Send(request);            
            return response;
        }
    }
}