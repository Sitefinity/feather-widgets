using System;
using System.Collections.Generic;
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
        /// Navigates to page using pager.
        /// </summary>
        /// <param name="page">The page.</param>
        public void NavigateToPageUsingPager(string page, int expCount)
        {
            HtmlUnorderedList pager = ActiveBrowser.Find.ByExpression<HtmlUnorderedList>("TagName=ul", "class=pagination")
                .AssertIsPresent("Pager");

            Assert.AreEqual(expCount, pager.ChildNodes.Count, "Unexpected number of pages on for pager");
            HtmlAnchor numPage = ActiveBrowser.Find.ByXPath<HtmlAnchor>("*//li[" + page + "]/a[text()=" + page + "]");
            numPage.Click();
        }

        /// <summary>
        /// Ares the titles present on the page frontend.
        /// </summary>
        /// <param name="itemTitles">The item titles.</param>
        /// <returns></returns>
        public bool AreTitlesPresentOnThePageFrontend(IEnumerable<string> itemTitles)
        {
            HtmlDiv frontendPageMainDiv = BAT.Wrappers().Frontend().Pages().PagesWrapperFrontend().GetPageContent();

            foreach (var title in itemTitles)
            {
                var itemAnchor = frontendPageMainDiv.Find.ByExpression<HtmlAnchor>("tagname=a", "InnerText=" + title);
                if ((itemAnchor == null) || (itemAnchor != null && !itemAnchor.IsVisible()))
                {
                    return false;
                }
            }

            return true;
        }

        private const int TimeOut = 30000;
    }
}
