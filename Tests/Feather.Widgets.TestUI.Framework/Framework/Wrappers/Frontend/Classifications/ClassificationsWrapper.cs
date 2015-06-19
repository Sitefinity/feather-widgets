using System;
using System.Collections.Generic;
using ArtOfTest.Common.UnitTesting;
using ArtOfTest.WebAii.Controls.HtmlControls;
using ArtOfTest.WebAii.Core;
using Feather.Widgets.TestUI.Framework.Framework.Wrappers.Backend.Classifications;

namespace Feather.Widgets.TestUI.Framework.Framework.Wrappers.Frontend.Classifications
{
    /// <summary>
    /// This is the entry point class for classifications widget on the frontend.
    /// </summary>
    public class ClassificationsWrapper : BaseWrapper
    {
        /// <summary>
        /// Verifies the tags titles on the frontend page.
        /// </summary>
        /// <param name="tagsTitles">The tags titles.</param>
        /// <returns>true or false depending on tags titles presence on frontend</returns>
        public bool IsTagsTitlesPresentOnTheFrontendPage(string[] tagsTitles)
        {
            HtmlDiv frontendPageMainDiv = BAT.Wrappers().Frontend().Pages().PagesWrapperFrontend().GetPageContent();

            for (int i = 0; i < tagsTitles.Length; i++)
            { 
                HtmlAnchor tagsAnchor = frontendPageMainDiv.Find.ByExpression<HtmlAnchor>("tagname=a", "InnerText=" + tagsTitles[i]);
                if (tagsAnchor == null || !tagsAnchor.IsVisible())
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Click tag title on the frontend
        /// </summary>
        /// <param name="tagTitle">The tag title</param>
        public void ClickTagTitle(string tagTitle)
        {
            HtmlDiv frontendPageMainDiv = BAT.Wrappers().Frontend().Pages().PagesWrapperFrontend().GetPageContent();

            HtmlAnchor tagsAnchor = frontendPageMainDiv.Find
                                                       .ByExpression<HtmlAnchor>("tagname=a", "InnerText=" + tagTitle)
                                                       .AssertIsPresent("Tag with this title was not found");

            tagsAnchor.MouseClick();
            ActiveBrowser.WaitUntilReady();
            ActiveBrowser.WaitForUrl(tagTitle.ToLower().Replace(" ", "%20"));
        }

        /// <summary>
        /// Verify css class 
        /// </summary>
        /// <param name="cssClass">css class</param>
        public void VerifyCssClass(string cssClass)
        {
            this.ActiveBrowser.Find.ByExpression<HtmlUnorderedList>("class=~" + cssClass).AssertIsNotNull("cssClassWrapper is not found");
        }

        public void VerifyCloudStyleTemplate(Dictionary<string, int> styledTags, TagsTemplates template)
        {
            HtmlUnorderedList list = null;
            switch (template)
            {
                case TagsTemplates.CloudList:          
                    list = this.ActiveBrowser.Find.ByExpression<HtmlUnorderedList>("class=sf-Tags list-unstyled").AssertIsPresent("unordered list for Cloud list template");                 
                    break;
                case TagsTemplates.TagCloud:
                    list = this.ActiveBrowser.Find.ByExpression<HtmlUnorderedList>("class=sf-Tags list-unstyled list-inline").AssertIsPresent("unordered list for Tags Cloud template");
                    break;
            }

            foreach (var tag in styledTags)
            {
                list.Find.ByExpression<HtmlAnchor>("innertext=" + tag.Key, "class=sf-Tags-size" + tag.Value).AssertIsPresent("anchor with correct size and inner text");
            }

            var allLinks = list.Find.AllByExpression<HtmlAnchor>("class=~sf-Tags");
            Assert.AreEqual(styledTags.Count, allLinks.Count, "Expected and actual count of tag links are not equal"); 
        }
    }
}