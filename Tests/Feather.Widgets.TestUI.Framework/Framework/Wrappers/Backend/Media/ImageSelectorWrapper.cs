using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using ArtOfTest.Common.UnitTesting;
using ArtOfTest.WebAii.Controls.HtmlControls;
using ArtOfTest.WebAii.Core;
using ArtOfTest.WebAii.jQuery;

namespace Feather.Widgets.TestUI.Framework.Framework.Wrappers.Backend.Media
{
    /// <summary>
    /// This is an entry point for ImageSelectorWrapper.
    /// </summary>
    public class ImageSelectorWrapper : BaseWrapper
    {
        /// <summary>
        /// Verifies that all elements from no images screen are present.
        /// </summary>
        public void VerifyNoImagesEmptyScreen()
        {
            this.EM.Media.ImageSelectorScreen.NoImagesIcon.AssertIsPresent("No image icon");
            this.EM.Media.ImageSelectorScreen.NoImagesText.AssertIsPresent("No image text");
            this.EM.Media.ImageSelectorScreen.SelectImageFromYourComputerLink.AssertIsPresent("Select image link");
            this.EM.Media.ImageSelectorScreen.DragAndDropLabel.AssertIsPresent("Drag and drop label"); 
        }

        /// <summary>
        /// Press cancel button from image selector footer. 
        /// </summary>
        public void PressCancelButton()
        {
            HtmlButton cancel = this.EM.Media.ImageSelectorScreen.CancelButton.AssertIsPresent("Button Cancel");

            cancel.Click();
        }

        /// <summary>
        /// Verifies image tooltip on mouse over.
        /// </summary>
        /// /// <param name="imageTitle">The image name.</param>
        /// <param name="libraryName">The library name.</param>
        /// <param name="dimensions">The image dimensions.</param>
        /// <param name="imageType">The image type.</param>
        public void VerifyImageTooltip(string imageTitle, string libraryName, string dimensions, string imageType)
        {
            HtmlSpan tooltip = this.EM.Media.ImageSelectorScreen.Tooltip.AssertIsNotNull("tooltip icon");
            string imageTooltipTitle = tooltip.Attributes.Where(a => a.Name == "sf-popover-title").First().Value;
            Assert.AreEqual(imageTitle, imageTooltipTitle, "Image title in tooltip is not correct");

            tooltip.ScrollToVisible();
            tooltip.Focus();
            tooltip.MouseHover();

            var tooltipContent = tooltip.Attributes.Where(a => a.Name == "sf-popover-content").First().Value;

            Assert.IsTrue(tooltipContent.Contains(libraryName), "Library name not found in the tooltip");
            Assert.IsTrue(tooltipContent.Contains(dimensions), "Image dimensions not found in the tooltip");
            Assert.IsTrue(tooltipContent.Contains(imageType), "Image type not found in the tooltip");
        }

        /// <summary>
        /// Selects image from image selector.
        /// </summary>
        /// <param name="imageTitle">The image title.</param>
        public void SelectImage(string imageTitle)
        {
            HtmlImage image = ActiveBrowser.Find.ByExpression<HtmlImage>("tagName=img", "alt=" + imageTitle);
            image.ScrollToVisible();
            image.Focus();
            image.MouseClick();
        }

        /// <summary>
        /// Verifies that a given filter is selected.
        /// </summary>
        /// <param name="filterName">The filter name.</param>
        public void VerifySelectedFilter(string filterName)
        {
            HtmlAnchor filter = ActiveBrowser.Find.ByExpression<HtmlAnchor>("tagName=a", "InnerText=" + filterName);

            Assert.IsTrue(filter.Parent<HtmlListItem>().CssClass == "ng-scope active");
        }

        /// <summary>
        /// Clicks Done button after image is selected.
        /// </summary>
        public void ConfirmImageSelection()
        {
            var doneBtn = this.EM.Media.ImageSelectorScreen.DoneButton.AssertIsPresent("Done button");

            doneBtn.Click();

            ActiveBrowser.WaitUntilReady();
            ActiveBrowser.WaitForAsyncRequests();
        }
    }
}
