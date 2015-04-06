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

            this.WaitForNewPageToLoad(expectedUrl, openInNewWindow);
        }

        /// <summary>
        /// Waits for a new page to load on the frontend.
        /// </summary>
        /// <param name="expectedUrl">The expected url after the item is loaded.</param>
        /// <param name="openInNewWindow">true or false dependingon Open in new window option</param>
        public void WaitForNewPageToLoad(string expectedUrl, bool openInNewWindow)
        {
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
        /// Verifies the document.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <param name="href">The href.</param>
        public void VerifyDocument(string title, string href)
        {
            HtmlAnchor doc = ActiveBrowser.Find.ByExpression<HtmlAnchor>("title=" + title)
                .AssertIsPresent("document");

            Assert.IsTrue(doc.HRef.StartsWith(href), "href is not correct");
        }

        /// <summary>
        /// Gets the image source.
        /// </summary>
        /// <param name="isBaseUrlIncluded">The is base URL included.</param>
        /// <param name="libraryUrl">The library URL.</param>
        /// <param name="imageType">Type of the image.</param>
        /// <param name="baseUrl">The base URL.</param>
        /// <returns></returns>
        public string GetMediaSource(bool isBaseUrlIncluded, string libraryUrl, string imageUrl, string baseUrl, string contentType = "images")
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

        /// <summary>
        /// Verifies the style attribute value.
        /// </summary>
        /// <param name="style">Expected style attribute value.</param>
        /// <param name="title">Image title</param>
        /// <param name="altText">Image alternative text</param>
        public void VerifyStyle(string style, string title, string altText)
        {
            ActiveBrowser.Find.ByExpression<HtmlImage>("title=" + title, "alt=" + altText, "style=" + style).AssertIsPresent("image");
        }

        /// <summary>
        /// Verifies the thumbnail string in source attribute.
        /// </summary>
        /// <param name="thumbnail">Thumbnail string in source attribute</param>
        /// <param name="title">Image title</param>
        /// <param name="altText">Image alternative text</param>
        public void VerifyThumbnail(string thumbnail, string title, string altText)
        {
            HtmlImage image = ActiveBrowser.Find.ByExpression<HtmlImage>("title=" + title, "alt=" + altText)
                           .AssertIsPresent("image");

            Assert.IsTrue(image.Src.Contains(thumbnail), "src does not contain thumbnail substring");
        }

        private const int TimeOut = 30000;
    }
}
