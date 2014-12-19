using ArtOfTest.Common.UnitTesting;
using ArtOfTest.WebAii.Controls.HtmlControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feather.Widgets.TestUI.Framework.Framework.Wrappers.Frontend.ModuleBuilder
{
    /// <summary>
    /// This is the entry point class for module builder on the frontend.
    /// </summary>
    public class ModuleBuilderWrapper : BaseWrapper
    {
        /// <summary>
        /// Verify dynamic title is present on the frontend.
        /// </summary>
        /// <param name="dynamicTitle">The dynamic title value</param>
        public void VerifyDynamicTitlePresentOnTheFrontend(string[] dynamicTitle)
        {
            HtmlDiv frontendPageMainDiv = BAT.Wrappers().Frontend().Pages().PagesWrapperFrontend().GetPageContent();

            for (int i = 0; i < dynamicTitle.Length; i++)
            {
                var isContained = frontendPageMainDiv.InnerText.Contains(dynamicTitle[i]);
                Assert.IsTrue(isContained, string.Concat("Expected ", dynamicTitle[i], " but found not found"));
            }
        }

        /// <summary>
        /// Navigate on page on the frontend
        /// </summary>
        public void NavigateToPage(string page)
        {
            HtmlDiv frontendPageMainDiv = BAT.Wrappers().Frontend().Pages().PagesWrapperFrontend().GetPageContent();

            HtmlUnorderedList pagination = frontendPageMainDiv.Find.ByExpression<HtmlUnorderedList>("tagname=ul", "class=pagination").
                AssertIsPresent("Pagination");

            HtmlListItem listItem = pagination.ChildNodes.Where(i => i.InnerText.Contains(page)).FirstOrDefault().As<HtmlListItem>();
            listItem.AssertIsPresent<HtmlListItem>("List Item");

            HtmlAnchor link = listItem.Find.ByExpression<HtmlAnchor>("InnerText=" + page);

            link.Click();
        }
    }
}
