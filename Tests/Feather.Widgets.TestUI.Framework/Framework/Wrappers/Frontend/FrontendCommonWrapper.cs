using System.Linq;
using System.Net.Http;
using ArtOfTest.Common.UnitTesting;
using ArtOfTest.WebAii.Controls.HtmlControls;
using ArtOfTest.WebAii.Core;

namespace Feather.Widgets.TestUI.Framework.Framework.Wrappers.Frontend
{
    /// <summary>
    /// This is a common wrapper for all frontend operations.
    /// </summary>
    public class FrontendCommonWrapper : BaseWrapper
    {
        /// <summary>
        /// Clicks on the link and waits until the new url is loaded.
        /// </summary>
        /// <param name="text">The text of the link to select</param>
        /// <param name="expectedUrl">The expected url after the item is loaded.</param>
        /// <param name="openInNewWindow">true or false dependingon Open in new window option checked.</param>
        public void VerifySelectedAnchorLink(string text, string expectedUrl, bool openInNewWindow = false)
        {
            HtmlAnchor link = ActiveBrowser.Find.ByExpression<HtmlAnchor>("tagname=a", "InnerText=" + text)
                .AssertIsPresent("Link");

            link.Click();

            if (openInNewWindow)
            {
                Manager.WaitForNewBrowserConnect(expectedUrl, true, TimeOut);
            }
            else
            {
                ActiveBrowser.WaitUntilReady();
                ActiveBrowser.WaitForUrl(expectedUrl);
            }

            HttpResponseMessage response = new HttpResponseMessage();
            Assert.AreEqual(200, (int)response.StatusCode);
        }

        /// <summary>
        /// Verifies image on the frontend.
        /// </summary>
        /// <param name="title">The image title.</param>
        /// <param name="altText">The image alt text.</param>
        /// <param name="src">The image src.</param>
        public void VerifyImage(string title, string altText, string src)
        {
            HtmlImage image = ActiveBrowser.Find.ByExpression<HtmlImage>("title=" + title, "alt=" + altText)
                .AssertIsPresent("image");

            Assert.IsTrue(image.Src.StartsWith(src), "src is not correct");
        }

        /// <summary>
        /// Gets the image source.
        /// </summary>
        /// <param name="isBaseUrlIncluded">The is base URL included.</param>
        /// <param name="libraryUrl">The library URL.</param>
        /// <param name="imageName">Name of the image.</param>
        /// <param name="imageType">Type of the image.</param>
        /// <param name="baseUrl">The base URL.</param>
        /// <returns></returns>
        public string GetImageSource(bool isBaseUrlIncluded, string libraryUrl, string imageUrl, string baseUrl, string contentType = "images")
        {
            string providerUrl = "default-source";

            if (isBaseUrlIncluded)
            {                
                return baseUrl + contentType + "/" + providerUrl + "/" + libraryUrl + "/" + imageUrl;
            }
            else
            {
                return "/" + contentType + "/" + providerUrl + "/" + libraryUrl + "/" + imageUrl;
            }
        }

        private const int TimeOut = 30000;
    }
}
