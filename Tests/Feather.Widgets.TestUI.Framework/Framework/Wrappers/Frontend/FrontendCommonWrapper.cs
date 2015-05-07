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

        private const int TimeOut = 30000;
    }
}
