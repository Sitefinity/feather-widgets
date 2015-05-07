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
    /// This is an entry point for MediaSelectorWrapper.
    /// </summary>
    public class MediaSelectorBaseWrapper : BaseWrapper
    {
        /// <summary>
        /// Selects media file from your computer.
        /// </summary>
        public void SelectMediaFileFromYourComputer()
        {
            var selectImageFromYourComputer = this.EM.Media.MediaSelectorScreen.SelectMediaFileFromYourComputerLink.AssertIsPresent("Select image link");
            selectImageFromYourComputer.MouseClick();
        }

        /// <summary>
        /// Verifies media tooltip on mouse over.
        /// </summary>
        /// /// <param name="documentTitle">The media name.</param>
        /// <param name="libraryName">The library name.</param>
        /// <param name="dimensions">The media dimensions.</param>
        /// <param name="documentType">The media type.</param>
        public void VerifyMediaTooltip(string title, string libraryName, string type, string dimensions = "")
        {
            HtmlSpan tooltip = this.EM.Media.MediaSelectorScreen.Tooltip.AssertIsNotNull("tooltip icon");
            string imageTooltipTitle = tooltip.Attributes.Where(a => a.Name == "sf-popover-title").First().Value;
            Assert.AreEqual(title, imageTooltipTitle, "Media title in tooltip is not correct");

            tooltip.ScrollToVisible();
            tooltip.Focus();
            tooltip.MouseHover();

            var tooltipContent = tooltip.Attributes.Where(a => a.Name == "sf-popover-content").First().Value;

            Assert.IsTrue(tooltipContent.Contains(libraryName), "Library name not found in the tooltip");
            if (!dimensions.Equals(string.Empty))
            {
                Assert.IsTrue(tooltipContent.Contains(dimensions), "Media dimensions not found in the tooltip");
            }

            Assert.IsTrue(tooltipContent.Contains(type), "Media type not found in the tooltip");
        }

        /// <summary>
        /// Verifies that all elements from no media screen are present.
        /// </summary>
        public void VerifyNoMediaEmptyScreen(string text)
        {
            this.EM.Media.MediaSelectorScreen.NoMediaIcon.AssertIsPresent("No media icon");
            this.EM.Media.MediaSelectorScreen.NoMediaText(text).AssertIsPresent("No media text");
            this.EM.Media.MediaSelectorScreen.SelectMediaFileFromYourComputerLink.AssertIsPresent("Select media link");
            this.EM.Media.MediaSelectorScreen.DragAndDropLabel.AssertIsPresent("Drag and drop label");
        }

        /// <summary>
        /// Selects media from media selector.
        /// </summary>
        /// <param name="title">The media title.</param>
        public void SelectMediaFile(string title, bool isDocumentFile = false)
        {
            if (isDocumentFile)
            {
                HtmlDiv doc = ActiveBrowser.Find.ByExpression<HtmlDiv>("tagName=div", "class=Media-item-title ng-binding", "innertext=" + title);
                doc.ScrollToVisible();
                doc.Focus();
                doc.MouseClick();
            }
            else
            {
                HtmlImage image = ActiveBrowser.Find.ByExpression<HtmlImage>("tagName=img", "alt=" + title);
                image.ScrollToVisible();
                image.Focus();
                image.MouseClick();
            }
        }

        /// <summary>
        /// Verifies the correct media files.
        /// </summary>
        /// <param name="titles">The media files titles.</param>
        public void VerifyCorrectMediaFiles(params string[] titles)
        {
            HtmlDiv holder = this.EM.Media.MediaSelectorScreen.MediaSelectorThumbnailHolderDiv.AssertIsPresent("holder");
            foreach (var title in titles)
            {
                var file = holder.Find.ByExpression<HtmlImage>("tagName=img", "alt=" + title);
                if (file == null)
                {
                    holder.Find.ByExpression<HtmlDiv>("tagName=div", "class=Media-item-title ng-binding", "innertext=" + title).AssertIsPresent(title);                 
                }
            }
        }

        /// <summary>
        /// Waits for content to be loaded.
        /// </summary>
        /// <param name="isEmptyScreen">The is empty screen.</param>
        public void WaitForContentToBeLoaded(bool isEmptyScreen)
        {
            Manager.Current.Wait.For(() => this.IsContentLoadedInMediaSelector(isEmptyScreen), 20000);
        }

        /// <summary>
        /// Switches to upload mode.
        /// </summary>
        public void SwitchToUploadMode()
        {
            HtmlAnchor uploadImage = this.EM.Media.MediaSelectorScreen.UploadMediaFile.AssertIsPresent("upload image");
            uploadImage.Click();
            ActiveBrowser.WaitForAsyncOperations();
            ActiveBrowser.RefreshDomTree();
        }

        /// <summary>
        /// Press cancel button from image selector footer. 
        /// </summary>
        public void PressCancelButton()
        {
            HtmlButton cancel = this.EM.Media.MediaSelectorScreen.CancelButton.AssertIsPresent("Button Cancel");

            cancel.Click();
        }

        /// <summary>
        /// Selects the folder.
        /// </summary>
        /// <param name="folderTitle">The folder title.</param>
        public void SelectFolder(string folderTitle)
        {
            var allFolders = this.EM.Media.MediaSelectorScreen.MediaSelectorMediaFolderDivs;
            var folder = allFolders.Where(i => i.InnerText.Contains(folderTitle)).FirstOrDefault();
            folder.ScrollToVisible();
            folder.Focus();
            folder.MouseClick();
            ActiveBrowser.WaitForAsyncOperations();
            ActiveBrowser.RefreshDomTree();
        }

        /// <summary>
        /// Verifies the correct folders.
        /// </summary>
        /// <param name="folderTitles">The folder titles.</param>
        public void VerifyCorrectFolders(params string[] folderTitles)
        {
            foreach (var folder in folderTitles)
            {
                var allFolders = this.EM.Media.MediaSelectorScreen.MediaSelectorMediaFolderDivs;
                allFolders.Where(i => i.InnerText.Contains(folder)).FirstOrDefault().AssertIsPresent(folder);
            }
        }

        /// <summary>
        /// Selects the folder from bread crumb.
        /// </summary>
        /// <param name="folderTitle">The folder title.</param>
        public void SelectFolderFromBreadCrumb(string folderTitle)
        {
            HtmlAnchor folder = ActiveBrowser.Find.ByExpression<HtmlAnchor>("class=ng-binding", "innertext=" + folderTitle).AssertIsPresent(folderTitle);
            folder.Click();
            ActiveBrowser.WaitForAsyncOperations();
            ActiveBrowser.RefreshDomTree();
        }

        /// <summary>
        /// Selects the folder from side bar.
        /// </summary>
        /// <param name="folderTitle">The folder title.</param>
        public void SelectFolderFromSideBar(string folderTitle)
        {
            HtmlSpan folder = null;
            do
            {
                folder = ActiveBrowser.Find.ByExpression<HtmlSpan>("tagName=span", "innertext=" + folderTitle);
                if (folder != null && folder.IsVisible())
                {
                    folder.Click();
                    ActiveBrowser.WaitForAsyncOperations();
                    ActiveBrowser.RefreshDomTree();
                }
                else
                {
                    HtmlAnchor arrow = this.EM.Media.MediaSelectorScreen.NotExpandedArrow.AssertIsPresent("not expanded arrow");
                    arrow.Click();
                }
            }
            while (folder == null);
        }

        /// <summary>
        /// Verifies that a given filter is selected.
        /// </summary>
        /// <param name="filterName">The filter name.</param>
        public void VerifySelectedFilter(string filterName)
        {
            HtmlAnchor filter = ActiveBrowser.Find.ByExpression<HtmlAnchor>("tagName=a", "InnerText=" + filterName);

            Assert.IsTrue(filter.Parent<HtmlListItem>().CssClass.Contains("active"));
        }

        /// <summary>
        /// Selects the filter.
        /// </summary>
        /// <param name="filterName">Name of the filter.</param>
        public void SelectFilter(string filterName)
        {
            HtmlAnchor filter = ActiveBrowser.Find.ByExpression<HtmlAnchor>("tagName=a", "class=ng-binding", "InnerText=" + filterName).AssertIsPresent(filterName);
            filter.Click();
            ActiveBrowser.WaitForAsyncOperations();
            ActiveBrowser.RefreshDomTree();
        }

        /// <summary>
        /// Verifies the correct count of media files.
        /// </summary>
        /// <param name="expectedCount">The expected count.</param>
        /// <param name="mediaType">Type of the media.</param>
        public void WaitCorrectCountOfMediaFiles(int expectedCount, string mediaType = "images")
        {
            Manager.Current.Wait.For(() => this.IsCountOfMediaFilesCorrect(expectedCount, mediaType), 20000);
        }

        /// <summary>
        /// Verifies the correct count of folders.
        /// </summary>
        /// <param name="expectedCount">The expected count.</param>
        public void WaitCorrectCountOfFolders(int expectedCount)
        {
            Manager.Current.Wait.For(() => this.IsCountOfFoldersCorrect(expectedCount), 20000);
        }

        /// <summary>
        /// Clicks Done button after image is selected.
        /// </summary>
        public void ConfirmMediaFileSelection()
        {
            var doneBtn = this.EM.Media.MediaSelectorScreen.DoneButton.AssertIsPresent("Done button");

            doneBtn.Click();

            ActiveBrowser.WaitUntilReady();
            ActiveBrowser.WaitForAsyncRequests();
        }

        /// <summary>
        /// Confirms the document selection in image widget.
        /// </summary>
        public void ConfirmMediaFileSelectionInWidget()
        {
            var doneBtn = this.EM.Media.MediaSelectorScreen.DoneButtonInMediaWidget.AssertIsPresent("Done button");

            doneBtn.Click();

            ActiveBrowser.WaitUntilReady();
            ActiveBrowser.WaitForAsyncRequests();
        }

        /// <summary>
        /// Searches the in media selector.
        /// </summary>
        /// <param name="searchText">The search text.</param>
        public void SearchInMediaSelector(string searchText)
        {
            HtmlInputText input = this.EM.Media.MediaSelectorScreen.SearchBox.AssertIsPresent("Search field");

            Manager.Current.Desktop.Mouse.Click(MouseClickType.LeftClick, input.GetRectangle());
            input.Text = string.Empty;
            Manager.Current.Desktop.KeyBoard.TypeText(searchText);
            Manager.Current.ActiveBrowser.WaitForAsyncOperations();
            Manager.Current.ActiveBrowser.RefreshDomTree();
        }

        /// <summary>
        /// Verifies the no items found message.
        /// </summary>
        public void VerifyNoItemsFoundMessage()
        {
            this.EM.Media.MediaSelectorScreen.NoItemsFoundDiv.AssertIsPresent("No items found").AssertIsPresent("No items found");
        }

        private bool IsContentLoadedInMediaSelector(bool isEmptyScreen)
        {
            bool result = false;
            Manager.Current.ActiveBrowser.RefreshDomTree();
            if (isEmptyScreen)
            {
                var selectImageFromYourComputer = this.EM.Media.MediaSelectorScreen.SelectMediaFileFromYourComputerLink;
                if (selectImageFromYourComputer != null && selectImageFromYourComputer.IsVisible())
                {
                    result = true;
                }
            }
            else
            {
                HtmlSpan tooltip = this.EM.Media.MediaSelectorScreen.Tooltip;
                if (tooltip != null && tooltip.IsVisible())
                {
                    result = true;
                }
            }

            return result;
        }

        private bool IsCountOfMediaFilesCorrect(int expectedCount, string mediaType)
        {
            ActiveBrowser.RefreshDomTree();
            int divsCount = 0;
            switch (mediaType)
            {
                case "images":
                    divsCount = this.EM.Media.MediaSelectorScreen.MediaSelectorMediaImageFileDivs.Count;
                    break;
                case "docs":           
                    divsCount = this.EM.Media.MediaSelectorScreen.MediaSelectorMediaDocFileDivs.Count;
                    break;
                case "videos":
                    divsCount = this.EM.Media.MediaSelectorScreen.MediaSelectorMediaVideoFileDivs.Count;
                    break;
            }
            
            return expectedCount == divsCount;
        }

        private bool IsCountOfFoldersCorrect(int expectedCount)
        {
            ActiveBrowser.RefreshDomTree();
            int divsCount = this.EM.Media.MediaSelectorScreen.MediaSelectorMediaFolderDivs.Count;
            return expectedCount == divsCount;
        }
    }
}