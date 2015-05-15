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
    /// This is the entry point class for Video Gallery on the frontend.
    /// </summary>
    public class VideoGalleryWrapper : BaseWrapper
    {
        public void VerifyVideo(string src, int width = 0, int height = 0)
        {           
            HtmlVideo video = ActiveBrowser.Find.ByExpression<HtmlVideo>("tagname=video")
                .AssertIsPresent("video");

            Assert.IsTrue(video.Src.StartsWith(src), "src is not correct");
            if (width != 0 && height != 0)
            {
                Assert.IsTrue(video.Width.Equals(width), "width is not correct");
                Assert.IsTrue(video.Height.Equals(height), "height is not correct");
            }
        

        }

        /// <summary>
        /// Verifies the Video is not present.
        /// </summary>
        /// <param name="altText">The alt text.</param>
        public void VerifyVideoIsNotPresent(string altText)
        {
            ActiveBrowser.Find.ByExpression<HtmlVideo>("tagname=video", "alt=" + altText).AssertIsNull(altText);
        }

        /// <summary>
        /// Determines whether is Video title present on detail master page.
        /// </summary>
        /// <param name="VideoTitle">The Video title.</param>
        /// <returns></returns>
        public bool IsVideoTitlePresentOnDetailMasterPage(string videoTitle)
        {
            ActiveBrowser.RefreshDomTree();
            HtmlDiv frontendPageMainDiv = BAT.Wrappers().Frontend().Pages().PagesWrapperFrontend().GetPageContent();

            return frontendPageMainDiv.InnerText.Contains(videoTitle);           
        }

        /// <summary>
        /// Verifies the previous Video.
        /// </summary>
        /// <param name="href">The href.</param>
        public void VerifyPreviousVideo(string href)
        {
            ActiveBrowser.Find.ByExpression<HtmlAnchor>("innertext=Previous Video", "href=~" + href)
                .AssertIsPresent("Previous Video");
        }

        /// <summary>
        /// Verifies the next Video.
        /// </summary>
        /// <param name="href">The href.</param>
        public void VerifyNextVideo(string href)
        {
            ActiveBrowser.Find.ByExpression<HtmlAnchor>("innertext=Next Video", "href=~" + href)
                .AssertIsPresent("Next Video");
        }

        /// <summary>
        /// Verifies the back to all Videos.
        /// </summary>
        /// <param name="href">The href.</param>
        public void VerifyBackToAllVideos(string href)
        {
            ActiveBrowser.Find.ByExpression<HtmlAnchor>("innertext=Back to all Videos", "href=" + href)
                .AssertIsPresent("Back to all Videos");
        }

        /// <summary>
        /// Verifies the selected Video overlay template.
        /// </summary>
        /// <param name="altText">The alt text.</param>
        public void VerifySelectedVideoOverlayTemplate(string altText)
        {
            ActiveBrowser.WaitForElementWithCssClass("mfp-img");
            ActiveBrowser.Find.ByExpression<HtmlVideo>("class=mfp-img", "alt=" + altText)
                .AssertIsPresent("Overlay Video");
        }

        /// <summary>
        /// Verifies the alt text div.
        /// </summary>
        /// <param name="altText">The alt text.</param>
        public void VerifyAltTextDiv(string altText)
        {
            ActiveBrowser.Find.ByExpression<HtmlDiv>("class=mfp-title", "innertext=" + altText)
                .AssertIsPresent("Alt text div");
        }

        /// <summary>
        /// Verifies the count div.
        /// </summary>
        /// <param name="count">The count.</param>
        public void VerifyCountDiv(string count)
        {
            ActiveBrowser.Find.ByExpression<HtmlDiv>("class=mfp-counter", "innertext=" + count)
                .AssertIsPresent("count div");
        }

        /// <summary>
        /// Verifies the previous and next Video arrows overlay template.
        /// </summary>
        public void VerifyPreviousAndNextVideoArrowsOverlayTemplate()
        {
            EM.ImageGallery.ImageGalleryFrontend.PreviousButtonOverlayTemplate
               .AssertIsPresent("previous button");

            EM.ImageGallery.ImageGalleryFrontend.NextButtonOverlayTemplate
               .AssertIsPresent("next button");
        }

        /// <summary>
        /// Closes the selected Video overlay template.
        /// </summary>
        public void CloseSelectedVideoOverlayTemplate()
        {
            var close = EM.ImageGallery.ImageGalleryFrontend.CloseButtonOverlayTemplate
               .AssertIsPresent("close button");
            close.Click();
        }
    }
}
