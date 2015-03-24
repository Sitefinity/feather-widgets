using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ArtOfTest.Common.UnitTesting;
using ArtOfTest.WebAii.Controls.HtmlControls;
using ArtOfTest.WebAii.Core;
using ArtOfTest.WebAii.jQuery;
using ArtOfTest.WebAii.Win32.Dialogs;

namespace Feather.Widgets.TestUI.Framework.Framework.Wrappers.Backend.Media
{
    /// <summary>
    /// This is an netry point for ImageUploadPropertiesWrapper.
    /// </summary>
    public class ImageUploadPropertiesWrapper : BaseWrapper
    {
        /// <summary>
        /// Checks if image title is populated correctly.
        /// </summary>
        /// <param name="imageTitle">The image title.</param>
        /// <returns>true or false depending on the image title in the textbox.</returns>
        public bool IsImageTitlePopulated(string imageTitle)
        {
            HtmlInputText titleField = this.EM.Media.ImageUploadPropertiesScreen.ImageTitleField
                                           .AssertIsPresent("Image title field");

            return imageTitle.Equals(titleField.Text);
        }

        /// <summary>
        /// Enters new title for image.
        /// </summary>
        /// <param name="imageTitle">The image title.</param>
        public void EnterImageTitle(string imageTitle)
        {
            HtmlInputText titleField = this.EM.Media.ImageUploadPropertiesScreen.ImageTitleField
                                           .AssertIsPresent("Image title field");

            titleField.Text = string.Empty;
            titleField.Click();
            Manager.Current.Desktop.KeyBoard.KeyPress(System.Windows.Forms.Keys.Space);
            this.EM.Media.ImageUploadPropertiesScreen.VisibleTitleIsRequiredMessage.AssertIsPresent("Title is required!");
            this.EM.Media.ImageUploadPropertiesScreen.UploadDisabledButton.AssertIsPresent("Upload disabled button");
            titleField.Text = imageTitle;
            titleField.AsjQueryControl().InvokejQueryEvent(jQueryControl.jQueryControlEvents.change);            
        }

        /// <summary>
        /// Enters new alt tetx for image.
        /// </summary>
        /// <param name="imageTitle">The image alt text.</param>
        public void EnterImageAltText(string imageAltText)
        {
            HtmlInputText altTextField = this.EM.Media.ImageUploadPropertiesScreen.ImageAltTextFields.LastOrDefault()
                                             .AssertIsPresent("Image alt text field");

            altTextField.Text = string.Empty;
            altTextField.Text = imageAltText;
            altTextField.AsjQueryControl().InvokejQueryEvent(jQueryControl.jQueryControlEvents.change);
        }

        /// <summary>
        /// Verifies the image to upload section.
        /// </summary>
        /// <param name="imageName">Name of the image.</param>
        /// <param name="size">The size.</param>
        public void VerifyImageToUploadSection(string imageName, string size)
        {
            ActiveBrowser.Find.ByExpression<HtmlSpan>("innertext=" + imageName).AssertIsPresent("name");
            ActiveBrowser.Find.ByExpression<HtmlSpan>("innertext=" + size).AssertIsPresent("size");
            this.EM.Media.ImageUploadPropertiesScreen.CancelUploadIcon.AssertIsPresent("Cancel upload");
        }

        /// <summary>
        /// Clicks the select library button.
        /// </summary>
        public void ClickSelectLibraryButton()
        {
            this.EM.Media.ImageUploadPropertiesScreen.UploadDisabledButton.AssertIsPresent("Upload disabled button");
            HtmlSpan select = this.EM.Media.ImageUploadPropertiesScreen.SelectButtons.FirstOrDefault().AssertIsPresent("select button");
            select.Click();
            ActiveBrowser.WaitForAsyncRequests();
            ActiveBrowser.RefreshDomTree();
        }

        /// <summary>
        /// Verifies the selected library.
        /// </summary>
        /// <param name="libraryName">Name of the library.</param>
        public void VerifySelectedLibrary(string libraryName)
        {
            ActiveBrowser.Find.ByExpression<HtmlSpan>("sf-shrinked-breadcrumb=" + libraryName).AssertIsPresent("name");
            this.EM.Media.ImageUploadPropertiesScreen.ChangeButtons.FirstOrDefault().AssertIsPresent("change button");
        }

        /// <summary>
        /// Clicks the select category button.
        /// </summary>
        public void ClickSelectCategoryButton()
        {
            HtmlSpan select = this.EM.Media.ImageUploadPropertiesScreen.SelectButtons.LastOrDefault().AssertIsPresent("select button");
            select.Click();
            ActiveBrowser.WaitForAsyncRequests();
            ActiveBrowser.RefreshDomTree();
        }

        /// <summary>
        /// Verifies the selected category.
        /// </summary>
        /// <param name="categoryName">Name of the category.</param>
        public void VerifySelectedCategory(string categoryName)
        {
            ActiveBrowser.Find.ByExpression<HtmlSpan>("sf-shrinked-breadcrumb=" + categoryName).AssertIsPresent("name");
            this.EM.Media.ImageUploadPropertiesScreen.ChangeButtons.LastOrDefault().AssertIsPresent("change button");
        }

        /// <summary>
        /// Adds the tag.
        /// </summary>
        /// <param name="tagName">Name of the tag.</param>
        public void AddTag(string tagName)
        {
            HtmlInputText addTag = this.EM.Media.ImageUploadPropertiesScreen.AddTag
                                       .AssertIsPresent("tag field");
            addTag.Text = tagName;
            addTag.AsjQueryControl().InvokejQueryEvent(jQueryControl.jQueryControlEvents.change);
            addTag.Click();
            Manager.Current.Desktop.KeyBoard.KeyPress(System.Windows.Forms.Keys.Enter);
            ActiveBrowser.WaitForAsyncRequests();
            ActiveBrowser.RefreshDomTree();
        }

        /// <summary>
        /// Verifies the selectedtag.
        /// </summary>
        /// <param name="tagName">Name of the tag.</param>
        public void VerifySelectedTag(string tagName)
        {
            HtmlFindExpression expression = new HtmlFindExpression("tagname=span", "InnerText=" + tagName);
            ActiveBrowser.WaitForElement(expression, 20000, false);
        }

        /// <summary>
        /// Expands the categories and tags section.
        /// </summary>
        public void ExpandCategoriesAndTagsSection()
        {
            HtmlSpan expandArrow = this.EM.Media.ImageUploadPropertiesScreen.CategoriesAndTagsArrow.AssertIsPresent("expand button");
            expandArrow.Click();
            ActiveBrowser.WaitForAsyncRequests();
            ActiveBrowser.RefreshDomTree();
        }

        /// <summary>
        /// Uploads the image.
        /// </summary>
        public void UploadImage()
        {
            HtmlButton uploadBtn = this.EM.Media.ImageUploadPropertiesScreen.UploadButton.AssertIsPresent("Upload button");
            uploadBtn.Click();
            ActiveBrowser.WaitForAsyncRequests();
            ActiveBrowser.RefreshDomTree();
        }

        /// <summary>
        /// Cancels the image.
        /// </summary>
        public void CancelImageUpload()
        {
            HtmlButton cancelBtn = this.EM.Media.ImageUploadPropertiesScreen.CancelButton.AssertIsPresent("Cancel button");
            cancelBtn.Click();
            ActiveBrowser.WaitForAsyncRequests();
            ActiveBrowser.RefreshDomTree();
        }

        /// <summary>
        /// Performs uploading of a single file.
        /// </summary>
        /// <param name="fileTitle">The file title.</param>
        /// <param name="projectDir">The project directory.</param>
        public void PerformSingleFileUpload(string fileTitle, string projectDir)
        {
            var fullFilePath = string.Concat(projectDir, fileTitle);

            var uploadDialog = BAT.Macros().DialogOperations().StartFileUploadDialogMonitoring(fullFilePath, DialogButton.OPEN);
            BAT.Macros().DialogOperations().WaitUntillFileUploadDialogIsHandled(uploadDialog);
            ActiveBrowser.WaitUntilReady();
            ActiveBrowser.WaitForAsyncRequests();
            ActiveBrowser.RefreshDomTree();
        }

        /// <summary>
        /// Waits for content to be loaded.
        /// </summary>
        public void WaitForContentToBeLoaded()
        {
            Manager.Current.Wait.For(() => this.IsContentLoadedInImageUploadPropertiesDialog(), 20000);
        }

        private bool IsContentLoadedInImageUploadPropertiesDialog()
        {
            Manager.Current.ActiveBrowser.RefreshDomTree();
            var cancelUpload = this.EM.Media.ImageUploadPropertiesScreen.CancelButton.AssertIsPresent("Cancel upload");
            return cancelUpload != null && cancelUpload.IsVisible();              
        }
    }
}