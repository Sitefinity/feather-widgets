using System.Linq;
using ArtOfTest.WebAii.Controls.HtmlControls;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Feather.Widgets.TestUI.Framework.Framework.Wrappers.Frontend
{
    /// <summary>
    /// Wrapper for SocialShare frontend
    /// </summary>
    public class SocialShareWrapper : BaseWrapper
    {
        /// <summary>
        /// Verifies the social share template content on the frontend.
        /// </summary>
        /// <param name="socialShareContent">Content of the social share.</param>
        /// <returns></returns>
        public bool IsSocialShareTemplateContentPresentOnTheFrontend(string[] socialShareContent)
        {
            HtmlDiv frontendPageMainDiv = BAT.Wrappers().Frontend().Pages().PagesWrapperFrontend().GetPageContent();
            bool isContained = false;

            for (int i = 0; i < socialShareContent.Length; i++)
            {
                isContained = frontendPageMainDiv.InnerText.Contains(socialShareContent[i]);
                if (!isContained)
                {
                    return isContained;
                }
            }

            return isContained;
        }

        /// <summary>
        /// Verifies the social share options in frontend.
        /// </summary>
        /// <param name="expectedNumberOfOptions">The expected number of options.</param>
        /// <param name="optionNames">The option names.</param>
        public void VerifySocialShareOptionsOnFrontend(int expectedNumberOfOptions, params string[] optionNames)
        {
            var list = EM.SocialShare.SocialSharePageEditor.UnorderedListContainingOptions.AssertIsPresent("UnorderedList of Options");
            var count = 0;
            foreach (var optionName in optionNames)
            {                                
                var option = list.Find.ByExpression<HtmlAnchor>("onclick=~" + optionName);
                if (option == null)
                {
                    option = list.Find.ByExpression<HtmlAnchor>("href=~" + optionName);
                    if (option == null)
                    {
                        var div = list.Find.ByExpression<HtmlDiv>("id=~" + optionName);
                        Assert.IsNotNull(div, "No such option " + optionName + " found");
                    }
                }
                count++;               
            }
            Assert.AreEqual(expectedNumberOfOptions, count, "Count is not correct!");
        }

        /// <summary>
        /// Verifies the social share text present on front end.
        /// </summary>
        /// <param name="optionNames">The option names.</param>
        public void VerifySocialShareTextPresentOnFrontend(params string[] optionNames)
        {
            var list = EM.SocialShare.SocialSharePageEditor.UnorderedListContainingOptions.AssertIsPresent("UnorderedList of Options");
            foreach (var optionName in optionNames)
            {
                var option = list.Find.ByExpression<HtmlListItem>("innertext=~" + optionName);
                Assert.IsNotNull(option, optionName + " is not found");
            }
        }

        /// <summary>
        /// Counts the of social share options.
        /// </summary>
        /// <returns></returns>
        public int CountOfSocialShareOptions()
        {
            var options = EM.SocialShare.SocialSharePageEditor.ListOfAllOptions;
            return options.Count();
        }
    }
}