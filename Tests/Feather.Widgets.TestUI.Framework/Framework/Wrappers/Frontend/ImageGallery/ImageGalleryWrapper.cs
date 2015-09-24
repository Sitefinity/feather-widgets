﻿using System;
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
    /// This is the entry point class for Image Gallery on the frontend.
    /// </summary>
    public class ImageGalleryWrapper : BaseWrapper
    {
        /// <summary>
        /// Verifies image on the frontend.
        /// </summary>
        /// <param name="altText">The image alt text.</param>
        /// <param name="src">The image src.</param>
        public void VerifyImage(string altText, string src)
        {
            ICollection<HtmlImage> images = EM.MediaGallery.MediaGalleryFrontend.AllImages;
            images.Where<HtmlImage>(k => k.Alt.Equals(altText) && k.Src.StartsWith(src)).FirstOrDefault().AssertIsPresent("image");
        }

        /// <summary>
        /// Verifies the correct order of images.
        /// </summary>
        /// <param name="itemAlts">The item names.</param>
        public void VerifyCorrectOrderOfImages(params string[] itemAlts)
        {
            var items = EM.MediaGallery.MediaGalleryFrontend.AllImages.ToList();

            int itemsCount = items.Count;
            Assert.IsNotNull(itemsCount);
            Assert.AreNotEqual(0, itemsCount);

            for (int i = 0; i < itemsCount; i++)
            {
                Assert.IsTrue(items[i].Alt.Contains(itemAlts[i]));
            }
        }

        /// <summary>
        /// Verifies the image is not present.
        /// </summary>
        /// <param name="altText">The alt text.</param>
        public void VerifyImageIsNotPresent(string altText)
        {
            ICollection<HtmlImage> images = EM.MediaGallery.MediaGalleryFrontend.AllImages;
            images.Where<HtmlImage>(k => k.Alt.Equals(altText)).FirstOrDefault().AssertIsNull("image");
        }

        /// <summary>
        /// Clicks the image.
        /// </summary>
        /// <param name="altText">The alt text.</param>
        public void ClickImage(string altText)
        {
            ICollection<HtmlImage> images = EM.MediaGallery.MediaGalleryFrontend.AllImages;
            var image = images.Where<HtmlImage>(k => k.Alt.Equals(altText)).FirstOrDefault().AssertIsPresent("image");

            image.Wait.ForVisible();
            image.ScrollToVisible();
            image.MouseClick();
            ActiveBrowser.WaitUntilReady();
            ActiveBrowser.RefreshDomTree();
        }

        /// <summary>
        /// Determines whether is image title present on detail master page.
        /// </summary>
        /// <param name="imageTitle">The image title.</param>
        /// <returns></returns>
        public bool IsImageTitlePresentOnDetailMasterPage(string imageTitle)
        {
            ActiveBrowser.RefreshDomTree();
            HtmlDiv frontendPageMainDiv = BAT.Wrappers().Frontend().Pages().PagesWrapperFrontend().GetPageContent();

            return frontendPageMainDiv.InnerText.Contains(imageTitle);           
        }

        /// <summary>
        /// Verifies the previous image.
        /// </summary>
        /// <param name="href">The href.</param>
        public void VerifyPreviousImage(string href)
        {
            var prevImage = EM.MediaGallery.MediaGalleryFrontend.PreviousLink
               .AssertIsPresent("previous image");
            Assert.IsTrue(prevImage.HRef.StartsWith(href));
        }

        /// <summary>
        /// Verifies the next image.
        /// </summary>
        /// <param name="href">The href.</param>
        public void VerifyNextImage(string href)
        {
            var nextImage = EM.MediaGallery.MediaGalleryFrontend.NextLink
               .AssertIsPresent("next image");
            Assert.IsTrue(nextImage.HRef.StartsWith(href));
        }

        /// <summary>
        /// Verifies the back to all images.
        /// </summary>
        /// <param name="href">The href.</param>
        public void VerifyBackToAllImages(string href)
        {
            var backLink = EM.MediaGallery.MediaGalleryFrontend.BeckToAllMediaFilesLink
               .AssertIsPresent("back to all images");
            Assert.IsTrue(backLink.HRef.Contains(href));
        }

        /// <summary>
        /// Verifies the selected image overlay template.
        /// </summary>
        /// <param name="altText">The alt text.</param>
        public void VerifySelectedImageOverlayTemplate(string altText)
        {
            ActiveBrowser.WaitForElementWithCssClass("mfp-img");
            ActiveBrowser.Find.ByExpression<HtmlImage>("class=mfp-img", "alt=" + altText)
                .AssertIsPresent("Overlay image");
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
        /// Verifies the previous and next image arrows overlay template.
        /// </summary>
        public void VerifyPreviousAndNextImageArrowsOverlayTemplate()
        {
            EM.MediaGallery.MediaGalleryFrontend.PreviousButtonOverlayTemplate
               .AssertIsPresent("previous button");

            EM.MediaGallery.MediaGalleryFrontend.NextButtonOverlayTemplate
               .AssertIsPresent("next button");
        }

        /// <summary>
        /// Closes the selected image overlay template.
        /// </summary>
        public void CloseSelectedImageOverlayTemplate()
        {
            var close = EM.MediaGallery.MediaGalleryFrontend.CloseButtonOverlayTemplate
               .AssertIsPresent("close button");
            close.Click();
        }

        /// <summary>
        /// Verifies the thumbnail strip template info.
        /// </summary>
        /// <param name="countLabel">The count label.</param>
        /// <param name="imageName">Name of the image.</param>
        public void VerifyThumbnailStripTemplateInfo(string countLabel, string imageName)
        {
            EM.MediaGallery.MediaGalleryFrontend.ThubnailStripePrev.AssertIsPresent("Prev");
            EM.MediaGallery.MediaGalleryFrontend.ThubnailStripeNext.AssertIsPresent("Next");
            ActiveBrowser.Find.ByExpression<HtmlDiv>("innertext=" + countLabel).AssertIsPresent(countLabel);
            ActiveBrowser.Find.ByExpression<HtmlContainerControl>("tagname=h2", "class=js-Gallery-title", "innertext=" + imageName).AssertIsPresent("Next");
        }
    }
}
