using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArtOfTest.Common.UnitTesting;
using ArtOfTest.WebAii.Controls.HtmlControls;
using ArtOfTest.WebAii.Core;

namespace Feather.Widgets.TestUI.Framework.Framework.Wrappers.Frontend
{
    /// <summary>
    /// This is the entry point class for classifications widget on the frontend.
    /// </summary>
    public class ClassificationsWrapper : BaseWrapper
    {
        /// <summary>
        /// Verifies the tags titles on the page frontend.
        /// </summary>
        /// <param name="tagsTitles">The tags titles.</param>
        /// <returns>true or false depending on tags titles presence on frontend</returns>
        public bool IsTagsTitlesPresentOnThePageFrontend(string[] tagsTitles)
        {
            HtmlDiv frontendPageMainDiv = BAT.Wrappers().Frontend().Pages().PagesWrapperFrontend().GetPageContent();

            for (int i = 0; i < tagsTitles.Length; i++)
            {              
                HtmlAnchor tagsAnchor = frontendPageMainDiv.Find.ByExpression<HtmlAnchor>("tagname=a", "InnerText=" + tagsTitles[i]);
                if ((tagsAnchor == null) || (tagsAnchor != null && !tagsAnchor.IsVisible()))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Click tags title on the frontend
        /// </summary>
        /// <param name="tagsTitle">Tags title</param>
        public void ClickTagsTitle(string newsTitle)
        {
            HtmlDiv frontendPageMainDiv = BAT.Wrappers().Frontend().Pages().PagesWrapperFrontend().GetPageContent();

            HtmlAnchor tagsAnchor = frontendPageMainDiv.Find.ByExpression<HtmlAnchor>("tagname=a", "InnerText=" + newsTitle)
                      .AssertIsPresent("News with this title was not found");

            tagsAnchor.Wait.ForVisible();
            tagsAnchor.ScrollToVisible();
            tagsAnchor.MouseClick();
            ActiveBrowser.WaitUntilReady();
            ActiveBrowser.WaitForUrl(newsTitle.ToLower().Replace(" ", "%20"));
        }


        /// <summary>
        /// Checks if a tags title is present on the frontend.
        /// </summary>
        /// <param name="newsTitle">The tags title.</param>
        /// <returns>True or False depending on the tags item presense.</returns>
        public bool IsTagsTitlePresentOnDetailMasterPage(string tagsTitle)
        {
            ActiveBrowser.RefreshDomTree();
            HtmlDiv frontendPageMainDiv = BAT.Wrappers().Frontend().Pages().PagesWrapperFrontend().GetPageContent();

            if (frontendPageMainDiv.InnerText.Contains(tagsTitle))
            {
               return true;
            }

            return false;
        }

        /// <summary>
        /// Verify css class 
        /// </summary>
        /// <param name="cssClass">css class</param>
        public void VerifyCssClass(string cssClass)
        {
            HtmlUnorderedList cssClassWrapper = this.ActiveBrowser.Find.ByExpression<HtmlUnorderedList>("class=~" + cssClass);
            cssClassWrapper.AssertIsNotNull("cssClassWrapper is not found");
        }
    }
}
