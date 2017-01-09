using System;
using System.Linq;
using ArtOfTest.WebAii.Controls.HtmlControls;
using ArtOfTest.WebAii.Messaging.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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

        /// <summary>
        /// Click the item.
        /// </summary>
        /// <param name="itemName">Name of the item.</param>
        public void VerifyCorrectDecInfoIsSend()
        {
            var list = EM.SocialShare.SocialSharePageEditor.UnorderedListContainingOptions.AssertIsPresent("UnorderedList of Options");
            foreach (var listItem in list.AllItems)
            {
                
                var socialProviderName = listItem.Attributes.Where(t => t.Name.Equals("data-sf-socialshareoption")).FirstOrDefault().Value;
                // this test will not correctly test the GooglePlusOne case as the click has to come from the iframe
                // and not from the containing element 
                new HtmlControl(listItem.ChildNodes[0]).Click();
                var searchedAttribute = "sf-decdata=" + socialProviderName;
                var browser = Manager.Browsers[0];
                browser.WaitForElement(15000, searchedAttribute);
                var decData = browser.Find.ByExpression<HtmlInputHidden>(searchedAttribute);
                Assert.IsNotNull(decData);
                Assert.IsTrue(decData.Value.Contains(socialProviderName));
            }
        }
    }
}