using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
    public class ImageSelectorWrapper : MediaSelectorBaseWrapper
    {
        /// <summary>
        /// Verifies that all elements from no images screen are present.
        /// </summary>
        public void VerifyNoImagesEmptyScreen()
        {
            this.EM.Media.MediaSelectorScreen.NoMediaIcon.AssertIsPresent("No image icon");
            this.EM.Media.MediaSelectorScreen.NoMediaText("No images").AssertIsPresent("No image text");
            this.EM.Media.MediaSelectorScreen.SelectMediaFileFromYourComputerLink.AssertIsPresent("Select image link");
            this.EM.Media.MediaSelectorScreen.DragAndDropLabel.AssertIsPresent("Drag and drop label"); 
        }

        /// <summary>
        /// Verifies image tooltip on mouse over.
        /// </summary>
        /// /// <param name="documentTitle">The image name.</param>
        /// <param name="libraryName">The library name.</param>
        /// <param name="dimensions">The image dimensions.</param>
        /// <param name="documentType">The image type.</param>
        public void VerifyImageTooltip(string imageTitle, string libraryName, string dimensions, string imageType)
        {
            HtmlSpan tooltip = this.EM.Media.MediaSelectorScreen.Tooltip.AssertIsNotNull("tooltip icon");
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
        /// Verifies the correct images.
        /// </summary>
        /// <param name="imageTitles">The image titles.</param>
        public void VerifyCorrectImages(params string[] imageTitles)
        {
            HtmlDiv holder = this.EM.Media.MediaSelectorScreen.MediaSelectorThumbnailHolderDiv.AssertIsPresent("holder");
            foreach (var image in imageTitles)
            {
                holder.Find.ByExpression<HtmlImage>("tagName=img", "alt=" + image).AssertIsPresent(image);
            }          
        }
    }
}
