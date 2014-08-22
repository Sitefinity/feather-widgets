using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArtOfTest.Common.UnitTesting;
using ArtOfTest.WebAii.Controls.HtmlControls;

namespace Feather.Widgets.TestUI.Framework.Framework.Wrappers.Frontend
{
    /// <summary>
    /// This is the entry point class for navigation widget on the frontend.
    /// </summary>
    public class NavigationWrapper : BaseWrapper
    {
        /// <summary>
        /// Provides list of all navigation div
        /// </summary>
        /// <returns>Returns list of all navigation div</returns>
        public List<HtmlDiv> ListWithNavigationDiv()
        {
            HtmlDiv frontendPageMainDiv = BAT.Wrappers().Frontend().Pages().PagesWrapperFrontend().GetPageContent();

            List<HtmlDiv> navigationList = frontendPageMainDiv.Find.AllByExpression<HtmlDiv>("tagname=div", "id=bs-example-navbar-collapse-1").ToList<HtmlDiv>();

            return navigationList;
        }

        /// <summary>
        /// Verify navigation on the page frontend
        /// </summary>
        /// <param name="cssClass">The navigation css class</param>
        /// <param name="pages">Expected pages</param>
        public void VerifyNavigationOnThePageFrontend(string cssClass, string[] pages)
        {
            List<HtmlDiv> navigationCount = this.ListWithNavigationDiv();

            for (int i = 0; i < navigationCount.Count; i++)
            {
                HtmlUnorderedList navList = navigationCount[i].Find.ByExpression<HtmlUnorderedList>("class=^" + cssClass)
                .AssertIsPresent("Navigation with selected css class");
                Assert.AreEqual(pages.Count(), navList.ChildNodes.Count(), "Unexpected number of pages");

                foreach (string page in pages)
                {
                    Assert.IsTrue(navList.InnerText.Contains(page), "Navigation does not contain the expected page " + page);
                }
            }
        }

        /// <summary>
        /// Verify navigation count on the page frontend
        /// </summary>
        /// <param name="expectedCount">The expected count</param>
        public void VerifyNavigationCountOnThePageFrontend(int expectedCount)
        {
            List<HtmlDiv> navigationCount = this.ListWithNavigationDiv();
            Assert.AreEqual<int>(expectedCount, navigationCount.Count, "unexpected number");
        }

        /// <summary>
        /// Verify child pages on the frontend
        /// </summary>
        /// <param name="cssClass">The navigation css class</param>
        /// <param name="pages">Expected pages</param>
        public void VerifyChildPagesFrontEndNavigation(string cssClass, string[] pages)
        {
            HtmlUnorderedList navList = ActiveBrowser.Find.ByExpression<HtmlUnorderedList>("class=" + cssClass)
                .AssertIsPresent("Navigation with selected class");

            Assert.AreEqual(pages.Count(), navList.ChildNodes.Count(), "Unexpected number of pages");

            foreach (string page in pages)
            {
                Assert.IsTrue(navList.InnerText.Contains(page), "Navigation does not contain the expected page " + page);
            }
        }
    }
}
