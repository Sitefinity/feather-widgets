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
    /// This is an entry point for DocumentSelectorWrapper.
    /// </summary>
    public class DocumentSelectorWrapper : MediaSelectorBaseWrapper
    {
        /// <summary>
        /// Verifies that all elements from no documents screen are present.
        /// </summary>
        public void VerifyNoDocumentsEmptyScreen()
        {
            this.EM.Media.MediaSelectorScreen.NoMediaIcon.AssertIsPresent("No document icon");
            this.EM.Media.MediaSelectorScreen.NoMediaText("No documents").AssertIsPresent("No document text");
            this.EM.Media.MediaSelectorScreen.SelectMediaFileFromYourComputerLink.AssertIsPresent("Select ducument link");
            this.EM.Media.MediaSelectorScreen.DragAndDropLabel.AssertIsPresent("Drag and drop label"); 
        }

        /// <summary>
        /// Verifies image tooltip on mouse over.
        /// </summary>
        /// /// <param name="documentTitle">The image name.</param>
        /// <param name="libraryName">The library name.</param>
        /// <param name="documentType">The image type.</param>
        public void VerifyDocumentTooltip(string documentTitle, string libraryName, string documentType)
        {
            HtmlSpan tooltip = this.EM.Media.MediaSelectorScreen.Tooltip.AssertIsNotNull("tooltip icon");
            string imageTooltipTitle = tooltip.Attributes.Where(a => a.Name == "sf-popover-title").First().Value;
            Assert.AreEqual(documentTitle, imageTooltipTitle, "Image title in tooltip is not correct");

            tooltip.ScrollToVisible();
            tooltip.Focus();
            tooltip.MouseHover();

            var tooltipContent = tooltip.Attributes.Where(a => a.Name == "sf-popover-content").First().Value;

            Assert.IsTrue(tooltipContent.Contains(libraryName), "Library name not found in the tooltip");
            Assert.IsTrue(tooltipContent.Contains(documentType), "Image type not found in the tooltip");
        }

        /// <summary>
        /// Selects doc from selector.
        /// </summary>
        /// <param name="documentTitle">The document title.</param>
        public void SelectDocument(string documentTitle)
        {
            HtmlDiv doc = ActiveBrowser.Find.ByExpression<HtmlDiv>("tagName=div", "class=Media-item-title ng-binding", "innertext=" + documentTitle);
            doc.ScrollToVisible();
            doc.Focus();
            doc.MouseClick();
        }

        /// <summary>
        /// Verifies the correct docs.
        /// </summary>
        /// <param name="documentTitles">The doc titles.</param>
        public void VerifyCorrectDocuments(params string[] documentTitles)
        {
            HtmlDiv holder = this.EM.Media.MediaSelectorScreen.MediaSelectorThumbnailHolderDiv.AssertIsPresent("holder");
            foreach (var doc in documentTitles)
            {
                holder.Find.ByExpression<HtmlImage>("tagName=div", "class=Media-item-title ng-binding", "innertext=" + doc).AssertIsPresent(doc);
            }          
        }
    }
}
