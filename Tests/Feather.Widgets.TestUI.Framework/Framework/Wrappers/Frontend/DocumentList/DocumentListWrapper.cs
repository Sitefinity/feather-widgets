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
    /// This is the entry point class for Document List on the frontend.
    /// </summary>
    public class DocumentListWrapper : BaseWrapper
    {
        /// <summary>
        /// Verifies the document.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <param name="href">The href.</param>
        public void VerifyDocument(string title, string href)
        {
            HtmlAnchor doc = ActiveBrowser.Find.ByExpression<HtmlAnchor>("innertext=" + title)
                .AssertIsPresent("document");

            Assert.IsTrue(doc.HRef.StartsWith(href), "href is not correct");
        }

        /// <summary>
        /// Verifies the document.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <param name="href">The href.</param>
        public void VerifyDocumentInTableView(string text, string href)
        {
            HtmlDiv tableHolder = ActiveBrowser.Find.ByExpression<HtmlDiv>("class=sf-document-list sf-document-list--table")
                .AssertIsPresent("tableHolder");
            HtmlTable table = tableHolder.Find.ByExpression<HtmlTable>("class=table")
                .AssertIsPresent("table");
            HtmlAnchor doc = table.Find.ByExpression<HtmlAnchor>("innertext=" + text)
                .AssertIsPresent("document");
            var parent = doc.Parent<HtmlTableCell>();
            Assert.IsTrue(parent.TagName == "td");
            Assert.IsTrue(parent.Parent<HtmlTableRow>().TagName == "tr");
            Assert.IsTrue(doc.HRef.StartsWith(href), "href is not correct");
        }

        /// <summary>
        /// Verifies the correct order of images.
        /// </summary>
        /// <param name="itemAlts">The item names.</param>
        public void VerifyCorrectOrderOfDocuments(params string[] itemTitles)
        {
            var mediaBodies = ActiveBrowser.Find.AllByExpression<HtmlDiv>("class=media-body");
            Assert.IsTrue(mediaBodies.Count != 0);
            int i = 0;

            foreach (var mediaBody in mediaBodies)
            {
                var item = mediaBody.ChildNodes.Where(t => t.TagName.Equals("a")).FirstOrDefault().As<HtmlAnchor>().AssertIsPresent("document");
                Assert.IsTrue(item.InnerText.Contains(itemTitles[i]));
                i++;
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
        /// Verifies the document is not present.
        /// </summary>
        /// <param name="title">The title text.</param>
        public void VerifyDocumentIsNotPresent(string title)
        {
            ActiveBrowser.Find.ByExpression<HtmlAnchor>("tagname=a", "innertext=" + title).AssertIsNull(title);
        }

        /// <summary>
        /// Clicks the document.
        /// </summary>
        /// <param name="title">The title text.</param>
        public void ClickDocument(string title)
        {
            HtmlAnchor doc = ActiveBrowser.Find.ByExpression<HtmlAnchor>("tagname=a", "innertext=" + title)
                  .AssertIsPresent(title);

            doc.Wait.ForVisible();
            doc.ScrollToVisible();
            doc.MouseClick();
            ActiveBrowser.WaitUntilReady();
        }

        /// <summary>
        /// Determines whether is document title present on detail master page.
        /// </summary>
        /// <param name="imageTitle">The document title.</param>
        /// <returns></returns>
        public bool IsDocumentTitlePresentOnDetailMasterPage(string documentTitle)
        {
            ActiveBrowser.RefreshDomTree();
            HtmlDiv frontendPageMainDiv = BAT.Wrappers().Frontend().Pages().PagesWrapperFrontend().GetPageContent();

            return frontendPageMainDiv.InnerText.Contains(documentTitle);           
        }

        /// <summary>
        /// Verifies the size and extension.
        /// </summary>
        /// <param name="size">The size.</param>
        /// <param name="extension">The extension.</param>
        public void VerifySizeAndExtensionOnTemplate(string size, string extension)
        {
            ActiveBrowser.Find.ByExpression<HtmlSpan>("class=text-muted", "innertext=" + size).AssertIsPresent("size");
            ActiveBrowser.Find.ByExpression<HtmlSpan>("class=text-muted", "innertext=" + extension).AssertIsPresent("extension");
        }

        /// <summary>
        /// Verifies the size and extension.
        /// </summary>
        /// <param name="size">The size.</param>
        public void VerifySizeOnHybridPage(string size)
        {
            ActiveBrowser.Find.ByExpression<HtmlSpan>("innertext=" + size).AssertIsPresent("size");
        }

        /// <summary>
        /// Verifies the download button.
        /// </summary>
        /// <param name="href">The href.</param>
        public void VerifyDownloadButton(string href)
        {
            ActiveBrowser.Find.ByExpression<HtmlAnchor>("tagname=a", "target=_blank", "href=~" + href, "innertext=Download").AssertIsPresent("download");
        }
    }
}
