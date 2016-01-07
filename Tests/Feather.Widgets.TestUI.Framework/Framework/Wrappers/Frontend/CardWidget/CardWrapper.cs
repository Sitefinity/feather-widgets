using System;
using System.Collections.Generic;
using ArtOfTest.Common.UnitTesting;
using ArtOfTest.WebAii.Controls.HtmlControls;
using ArtOfTest.WebAii.Core;

namespace Feather.Widgets.TestUI.Framework.Framework.Wrappers.Frontend.CardWidget
{
    /// <summary>
    /// This is the entry point class for card widget on the frontend.
    /// </summary>
    public class CardWrapper : BaseWrapper
    {
        /// <summary>
        /// Verify card widget content on frontend
        /// </summary>
        /// <param name="cardWidgetContent">Content of the card widget.</param>
        public void VerifyCardWidgetContentOnFrontend(string cardWidgetContent, bool isSimple = false)
        {
            HtmlDiv frontendPageMainDiv = BAT.Wrappers().Frontend().Pages().PagesWrapperFrontend().GetPageContent();
            HtmlDiv divContainer;
            bool isContained;

            if (isSimple)
            {
                divContainer = frontendPageMainDiv.Find
                                                  .ByExpression<HtmlDiv>("tagname=div", "class=row")
                                                  .AssertIsPresent("Div with this class");
                isContained = divContainer.InnerText.Contains(cardWidgetContent);
            }
            else
            {
                try
                {
                     divContainer = frontendPageMainDiv.Find
                                                 .ByExpression<HtmlDiv>("tagname=div", "class=thumbnail")
                                                 .AssertIsPresent("Div with this class");
                     isContained = divContainer.InnerText.Contains(cardWidgetContent);
                }
                catch (Exception)
                {
                    isContained = frontendPageMainDiv.InnerText.Contains(cardWidgetContent);
                }
            }
                Assert.IsTrue(isContained, "Card widget content is not as expected");
        }

        /// <summary>
        /// Verify card widget content on frontend not presented
        /// </summary>
        /// <param name="cardWidgetContent">Content of the card widget.</param>
        public void VerifyCardWidgetContentNotPresentedOnFrontend(string cardWidgetContent)
        {
            HtmlDiv frontendPageMainDiv = BAT.Wrappers().Frontend().Pages().PagesWrapperFrontend().GetPageContent();

            bool isContained = frontendPageMainDiv.InnerText.Contains(cardWidgetContent);
            Assert.IsFalse(isContained, "Card widget content is not as expected");
        }

        /// <summary>
        /// Verifies the image is not present.
        /// </summary>
        /// <param name="labelName">The image title.</param>
        public void VerifyImageIsPresentOnFrontend(string imageTitle)
        {
            ActiveBrowser.Find.ByExpression<HtmlImage>("title=" + imageTitle).AssertIsNotNull(imageTitle);
        }

        /// <summary>
        /// Verify page is presented
        /// </summary>
        public void VerifyPageIsPresentOnFrontend(string labelName, string pageName)
        {
            HtmlDiv frontendPageMainDiv = BAT.Wrappers().Frontend().Pages().PagesWrapperFrontend().GetPageContent();

            HtmlAnchor pageAnchor = frontendPageMainDiv.Find
                                                       .ByExpression<HtmlAnchor>("tagname=a", "InnerText=" + labelName)
                                                       .AssertIsPresent("Tag with this title was not found");
            bool isContained = pageAnchor.HRef.Contains(pageName);
            Assert.IsTrue(isContained, "Page is not as expected");
        }
    }
}
