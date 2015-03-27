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
            HtmlImage image = ActiveBrowser.Find.ByExpression<HtmlImage>("tagname=img", "alt=" + altText)
                .AssertIsPresent(altText);

            Assert.IsTrue(image.Src.StartsWith(src), "src is not correct");
        }

        /// <summary>
        /// Verifies the correct order of images.
        /// </summary>
        /// <param name="itemAlts">The item names.</param>
        public void VerifyCorrectOrderOfImages(params string[] itemAlts)
        {
            var items = ActiveBrowser.Find.AllByExpression<HtmlImage>("tagname=img");

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
            ActiveBrowser.Find.ByExpression<HtmlImage>("tagname=img", "alt=" + altText).AssertIsNull(altText);
        }

        /// <summary>
        /// Clicks the image.
        /// </summary>
        /// <param name="altText">The alt text.</param>
        public void ClickImage(string altText)
        {
            HtmlImage image = ActiveBrowser.Find.ByExpression<HtmlImage>("tagname=img", "alt=" + altText)
                  .AssertIsPresent(altText);

            image.Wait.ForVisible();
            image.ScrollToVisible();
            image.MouseClick();
            ActiveBrowser.WaitUntilReady();
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
            ActiveBrowser.Find.ByExpression<HtmlAnchor>("innertext=Previous image", "href=~" + href)
                .AssertIsPresent("Previous image");
        }

        /// <summary>
        /// Verifies the next image.
        /// </summary>
        /// <param name="href">The href.</param>
        public void VerifyNextImage(string href)
        {
            ActiveBrowser.Find.ByExpression<HtmlAnchor>("innertext=Next image", "href=~" + href)
                .AssertIsPresent("Next image");
        }

        /// <summary>
        /// Verifies the back to all images.
        /// </summary>
        /// <param name="href">The href.</param>
        public void VerifyBackToAllImages(string href)
        {
            ActiveBrowser.Find.ByExpression<HtmlAnchor>("innertext=Back to all images", "href=" + href)
                .AssertIsPresent("Back to all images");
        }
    }
}
