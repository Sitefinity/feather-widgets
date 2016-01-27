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
            ActiveBrowser.WaitForElementWithCssClass("sfPublicWrapper");
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
        /// Verifies the categories titles on the frontend page.
        /// </summary>
        /// <param name="categoriesTitles">The categories titles.</param>
        /// <returns>true or false depending on categories titles presence on frontend</returns>
        public bool IsCategoriesTitlesPresentOnTheFrontendPage(string[] categoriesTitles)
        {
            ActiveBrowser.WaitForElementWithCssClass("sfPublicWrapper");
            HtmlDiv frontendPageMainDiv = BAT.Wrappers().Frontend().Pages().PagesWrapperFrontend().GetPageContent();

            for (int i = 0; i < categoriesTitles.Length; i++)
            {
                HtmlAnchor categoriesAnchor = frontendPageMainDiv.Find.ByExpression<HtmlAnchor>("tagname=a", "InnerText=" + categoriesTitles[i]);
                if (categoriesAnchor == null || !categoriesAnchor.IsVisible())
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Click category title on the frontend
        /// </summary>
        /// <param name="categoryTitle">The category title</param>
        public void ClickCategoryTitle(string categoryTitle)
        {
            HtmlDiv frontendPageMainDiv = BAT.Wrappers().Frontend().Pages().PagesWrapperFrontend().GetPageContent();

            HtmlAnchor categoriesAnchor = frontendPageMainDiv.Find
                                                       .ByExpression<HtmlAnchor>("tagname=a", "InnerText=" + categoryTitle)
                                                       .AssertIsPresent("Tag with this title was not found");

            categoriesAnchor.MouseClick();
            ActiveBrowser.WaitUntilReady();
            ActiveBrowser.WaitForUrl(categoryTitle.ToLower().Replace(" ", "%20"));
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


        /// <summary>
        /// Check Show Empty Categories checkbox.
        /// </summary>
        public void CheckCategoriesItemCount(string count, string categoryName, int index)
        {
            HtmlUnorderedList categoriesList = this.EM.Classifications.CategoriesWidgetFrontend.CategoryList;

            Assert.IsTrue(categoriesList.ChildNodes[2].InnerText.Contains(categoryName + index));
            Assert.IsTrue(categoriesList.ChildNodes[2].InnerText.Contains(count));
        }

        /// <summary>
        /// Check Show Empty Categories checkbox.
        /// </summary>
        public void CheckCategoriesSorting(string categoryName, string sorting)
        {
            HtmlUnorderedList categoriesList = this.EM.Classifications.CategoriesWidgetFrontend.CategoryList;

            var itemsCount = categoriesList.ChildNodes.Count;
                
            if (sorting == "By Title (A-Z)")
            {
                for (int i = 0; i < itemsCount; i++)
                {
                    Assert.IsTrue(categoriesList.ChildNodes[i].InnerText.Contains((i + 1) + categoryName));
                }
            } else 
            {
                for (int i = 0; i < itemsCount; i++)
                {
                    Assert.IsTrue(categoriesList.ChildNodes[i].InnerText.Contains((itemsCount - i) + categoryName));
                }
            }
            
        }
    }
}