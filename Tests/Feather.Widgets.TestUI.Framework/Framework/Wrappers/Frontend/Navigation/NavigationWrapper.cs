using ArtOfTest.Common.UnitTesting;
using ArtOfTest.WebAii.Controls.HtmlControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feather.Widgets.TestUI.Framework.Framework.Wrappers.Frontend
{
    public class NavigationWrapper : BaseWrapper
    {
        public void VerifyNavigationOnThePageFrontend(string cssClass, string[] pages)
        {
            HtmlDiv frontendPageMainDiv = BAT.Wrappers().Frontend().Pages().PagesWrapperFrontend().GetPageContent();

            List<HtmlDiv> navigationCount = frontendPageMainDiv.Find.AllByExpression<HtmlDiv>("tagname=div", "id=bs-example-navbar-collapse-1").ToList<HtmlDiv>();

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
    }
}
