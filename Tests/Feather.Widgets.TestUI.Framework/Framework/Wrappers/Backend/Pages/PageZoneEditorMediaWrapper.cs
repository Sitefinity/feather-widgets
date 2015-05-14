using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ArtOfTest.Common.UnitTesting;
using ArtOfTest.WebAii.Controls.HtmlControls;
using ArtOfTest.WebAii.Core;
using Telerik.WebAii.Controls.Html;

namespace Feather.Widgets.TestUI.Framework.Framework.Wrappers.Backend
{
    /// <summary>
    /// Wraps actions and elements for media widgets available in the Page Editor
    /// </summary>
    public class PageZoneEditorMediaWrapper : BaseWrapper
    {      
        /// <summary>
        /// Verifies the image thumbnail.
        /// </summary>
        /// <param name="altText">The alt text.</param>
        /// <param name="src">The SRC.</param>
        public void VerifyImageThumbnail(string altText, string src)
        {
            HtmlImage image = ActiveBrowser.Find.ByExpression<HtmlImage>("alt=~" + altText)
                .AssertIsPresent(altText);

            Assert.IsTrue(image.Src.StartsWith(src), "src is not correct");
        }

        /// <summary>
        /// Verifies the document.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <param name="href">The href.</param>
        public void VerifyDocument(string text, string href)
        {
            HtmlAnchor doc = ActiveBrowser.Find.ByExpression<HtmlAnchor>("innertext=" + text)
                .AssertIsPresent("document");

            Assert.IsTrue(doc.HRef.StartsWith(href), "href is not correct");
        }

        /// <summary>
        /// Verifies the video.
        /// </summary>
        /// <param name="src">The SRC.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        public void VerifyVideo(string src, int width = 0, int height = 0)
        {
            HtmlVideo video = ActiveBrowser.Find.ByExpression<HtmlVideo>("src=~" + src)
                .AssertIsPresent("video");
            if (width != 0 && height != 0)
            {
                Assert.IsTrue(video.Width.Equals(width), "width is not correct");
                Assert.IsTrue(video.Height.Equals(height), "height is not correct");
            }
        }

        /// <summary>
        /// Verifies the document.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <param name="href">The href.</param>
        public void VerifyDocumentInTableView(string text, string href)
        {
            HtmlTable table = ActiveBrowser.Find.ByExpression<HtmlTable>("class=rdTable")
                .AssertIsPresent("table");
            HtmlAnchor doc = table.Find.ByExpression<HtmlAnchor>("innertext=" + text)
                .AssertIsPresent("document");
            var parent = doc.Parent<HtmlTableCell>();
            Assert.IsTrue(parent.TagName == "td");
            Assert.IsTrue(parent.Parent<HtmlTableRow>().TagName == "tr");
            Assert.IsTrue(doc.HRef.StartsWith(href), "href is not correct");
        }

        /// <summary>
        /// Verifies the document icon.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="isTableViewSelected">The is table view selected.</param>
        public void VerifyDocumentIconOnTemplate(string type, bool isTableViewSelected = false)
        {
            HtmlContainerControl icon = null;
            if (isTableViewSelected)
            {
                icon = ActiveBrowser.Find.ByExpression<HtmlContainerControl>("class=icon-file icon-txt icon-sm")
                    .AssertIsPresent("icon");
            }
            else 
            {
                icon = ActiveBrowser.Find.ByExpression<HtmlContainerControl>("class=icon-file icon-txt icon-md")
                    .AssertIsPresent("icon");
            }

            icon.Find.ByExpression<HtmlSpan>("class=~icon-txt", "innertext=" + type)
                .AssertIsPresent("type");
        }

        /// <summary>
        /// Verifies the thumbnail strip template info.
        /// </summary>
        /// <param name="countLabel">The count label.</param>
        /// <param name="imageName">Name of the image.</param>
        public void VerifyThumbnailStripTemplateInfo(string countLabel, string imageName)
        {
            ActiveBrowser.Find.ByExpression<HtmlAnchor>("class=~js-Gallery-prev").AssertIsPresent("Prev");
            ActiveBrowser.Find.ByExpression<HtmlAnchor>("class=~js-Gallery-next").AssertIsPresent("Next");
            ActiveBrowser.Find.ByExpression<HtmlDiv>("innertext=" + countLabel).AssertIsPresent(countLabel);
            ActiveBrowser.Find.ByExpression<HtmlContainerControl>("tagname=h2", "class=js-Gallery-title", "innertext=" + imageName).AssertIsPresent("Next");
        }

        /// <summary>
        /// Verifies the image resizing properties.
        /// </summary>
        /// <param name="altText">The alt text.</param>
        /// <param name="src">The SRC.</param>
        public void VerifyImageResizingProperties(string altText, string srcWidth, string srcHeight, string srcQuality, string srcResizingOption)
        {
            HtmlImage image = ActiveBrowser.Find.ByExpression<HtmlImage>("alt=~" + altText)
                .AssertIsPresent(altText);

            Assert.IsTrue(image.Src.Contains(srcWidth) && image.Src.Contains(srcHeight) && image.Src.Contains(srcQuality) && image.Src.Contains(srcResizingOption), "src is not correct");
        }

        /// <summary>
        /// Verifies the image is not present.
        /// </summary>
        /// <param name="altText">The alt text.</param>
        public void VerifyImageIsNotPresent(string altText)
        {
            ActiveBrowser.Find.ByExpression<HtmlImage>("alt=~" + altText).AssertIsNull(altText);
        }

        /// <summary>
        /// Verifies the document is not present.
        /// </summary>
        /// <param name="text">The text.</param>
        public void VerifyDocumentIsNotPresent(string text)
        {
            ActiveBrowser.Find.ByExpression<HtmlAnchor>("innertext=~" + text).AssertIsNull(text);
        }

        /// <summary>
        /// Verifies the correct order of items on backend.
        /// </summary>
        /// <param name="itemAlts">The item names.</param>
        public void VerifyCorrectOrderOfImagesOnBackend(params string[] itemAlts)
        {
            var items = ActiveBrowser.Find.AllByExpression<HtmlImage>("tagname=img", "alt=~AltText_Image");

            int itemsCount = items.Count;
            Assert.IsNotNull(itemsCount);
            Assert.AreNotEqual(0, itemsCount);

            for (int i = 0; i < itemsCount; i++)
            {
                Assert.IsTrue(items[i].Alt.Contains(itemAlts[i]));
            }
        }

        /// <summary>
        /// Verifies the correct order of images.
        /// </summary>
        /// <param name="itemAlts">The item names.</param>
        public void VerifyCorrectOrderOfDocumentsInTableView(params string[] itemTitles)
        {
            var anchors = ActiveBrowser.Find.AllByExpression<HtmlAnchor>("tagname=a", "class=sf-title");
            Assert.IsTrue(anchors.Count != 0);
            int i = 0;

            foreach (var anchor in anchors)
            {
                Assert.IsTrue(anchor.InnerText.Contains(itemTitles[i]));
                i++;
            }
        }
    }
}
