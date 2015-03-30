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
        /// Selects the image from your computer.
        /// </summary>
        public void SelectImageFromYourComputer()
        {
            var selectImageFromYourComputer = this.EM.Media.ImageSelectorScreen.SelectImageFromYourComputerLink.AssertIsPresent("Select image link");
            selectImageFromYourComputer.MouseClick();            
        }

        /// <summary>
        /// Waits for content to be loaded.
        /// </summary>
        /// <param name="isEmptyScreen">The is empty screen.</param>
        public void WaitForContentToBeLoaded(bool isEmptyScreen)
        {
            Manager.Current.Wait.For(() => this.IsContentLoadedInImageSelector(isEmptyScreen), 20000);
        }
 
        private bool IsContentLoadedInImageSelector(bool isEmptyScreen)
        {
            bool result = false;
            Manager.Current.ActiveBrowser.RefreshDomTree();
            if (isEmptyScreen)
            {
                var selectImageFromYourComputer = this.EM.Media.ImageSelectorScreen.SelectImageFromYourComputerLink;
                if (selectImageFromYourComputer != null && selectImageFromYourComputer.IsVisible())
                {
                    result = true;
                }
            }
            else
            {
                HtmlSpan tooltip = this.EM.Media.ImageSelectorScreen.Tooltip;
                if (tooltip != null && tooltip.IsVisible())
                {
                    result = true;
                }
            }

            return result;
        }

        /// <summary>
        /// Switches to upload mode.
        /// </summary>
        public void SwitchToUploadMode()
        {
            HtmlAnchor uploadImage = this.EM.Media.ImageSelectorScreen.UploadImage.AssertIsPresent("upload image");
            uploadImage.Click();
            ActiveBrowser.WaitForAsyncOperations();
            ActiveBrowser.RefreshDomTree();
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
        /// Verifies the correct images.
        /// </summary>
        /// <param name="imageTitles">The image titles.</param>
        public void VerifyCorrectImages(params string[] imageTitles)
        {
            HtmlDiv holder = this.EM.Media.ImageSelectorScreen.ImageSelectorThumbnailHolderDiv.AssertIsPresent("holder");
            foreach (var image in imageTitles)
            {
                holder.Find.ByExpression<HtmlImage>("tagName=img", "alt=" + image).AssertIsPresent(image);
            }          
        }

        /// <summary>
        /// Selects the folder.
        /// </summary>
        /// <param name="folderTitle">The folder title.</param>
        public void SelectFolder(string folderTitle)
        {
            var allFolders = this.EM.Media.ImageSelectorScreen.ImageSelectorMediaFolderDivs;
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
                var allFolders = this.EM.Media.ImageSelectorScreen.ImageSelectorMediaFolderDivs;
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
                    HtmlAnchor arrow = this.EM.Media.ImageSelectorScreen.NotExpandedArrow.AssertIsPresent("not expanded arrow");
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

            Assert.IsTrue(filter.Parent<HtmlListItem>().CssClass == "ng-scope active");
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
        /// Verifies the correct count of images.
        /// </summary>
        /// <param name="expectedCount">The expected count.</param>
        public void WaitCorrectCountOfImages(int expectedCount)
        {
            Manager.Current.Wait.For(() => this.IsCountOfImagesCorrect(expectedCount), 20000);          
        }
 
        private bool IsCountOfImagesCorrect(int expectedCount)
        {
            ActiveBrowser.RefreshDomTree();
            int divsCount = this.EM.Media.ImageSelectorScreen.ImageSelectorMediaImageFileDivs.Count;
            return expectedCount == divsCount;
        }

        /// <summary>
        /// Verifies the correct count of folders.
        /// </summary>
        /// <param name="expectedCount">The expected count.</param>
        public void WaitCorrectCountOfFolders(int expectedCount)
        {
            Manager.Current.Wait.For(() => this.IsCountOfFoldersCorrect(expectedCount), 20000);
        }
 
        private bool IsCountOfFoldersCorrect(int expectedCount)
        {
            ActiveBrowser.RefreshDomTree();
            int divsCount = this.EM.Media.ImageSelectorScreen.ImageSelectorMediaFolderDivs.Count;
            return expectedCount == divsCount;
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

        /// <summary>
        /// Confirms the image selection in image widget.
        /// </summary>
        public void ConfirmImageSelectionInImageWidget()
        {
            var doneBtn = this.EM.Media.ImageSelectorScreen.DoneButtonInImageWidget.AssertIsPresent("Done button");

            doneBtn.Click();

            ActiveBrowser.WaitUntilReady();
            ActiveBrowser.WaitForAsyncRequests();
        }

        public void SearchInImageSelector(string searchText)
        {
            HtmlInputText input = this.EM.Media.ImageSelectorScreen.SearchBox.AssertIsPresent("Search field");

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
            this.EM.Media.ImageSelectorScreen.NoItemsFoundDiv.AssertIsPresent("No items found").AssertIsPresent("No items found");
        }
    }
}
