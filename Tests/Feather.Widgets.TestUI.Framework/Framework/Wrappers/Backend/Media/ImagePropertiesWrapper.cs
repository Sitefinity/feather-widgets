using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArtOfTest.Common.UnitTesting;
using ArtOfTest.WebAii.Controls.HtmlControls;
using ArtOfTest.WebAii.Core;
using ArtOfTest.WebAii.jQuery;

namespace Feather.Widgets.TestUI.Framework.Framework.Wrappers.Backend.Media
{
    /// <summary>
    /// This is an netry point for ImagePropertiesWrapper.
    /// </summary>
    public class ImagePropertiesWrapper : BaseWrapper
    {
        /// <summary>
        /// Checks if image title is populated correctly.
        /// </summary>
        /// <param name="imageTitle">The image title.</param>
        /// <returns>true or false depending on the image title in the textbox.</returns>
        public bool IsImageTitlePopulated(string imageTitle)
        {
            HtmlInputText titleField = this.EM.Media.ImagePropertiesScreen.ImageTitleField
                .AssertIsPresent("Image title field");

            return imageTitle.Equals(titleField.Text);
        }

        /// <summary>
        /// Enters new title for image.
        /// </summary>
        /// <param name="imageTitle">The image title.</param>
        public void EnterImageTitle(string imageTitle)
        {
            HtmlInputText titleField = this.EM.Media.ImagePropertiesScreen.ImageTitleField
                .AssertIsPresent("Image title field");

            titleField.Text = string.Empty;
            titleField.Text = imageTitle;
            titleField.AsjQueryControl().InvokejQueryEvent(jQueryControl.jQueryControlEvents.change);
        }

        /// <summary>
        /// Checks if image alt text is populated correctly.
        /// </summary>
        /// <param name="imageTitle">The image title.</param>
        /// <returns>true or false depending on the image title in the textbox.</returns>
        public bool IsImageAltTextPopulated(string imageAltText)
        {
            HtmlInputText altTextField = this.EM.Media.ImagePropertiesScreen.ImageAltTextField
                .AssertIsPresent("Image alt text field");

            return imageAltText.Equals(altTextField.Text);
        }

        /// <summary>
        /// Enters new alt tetx for image.
        /// </summary>
        /// <param name="imageTitle">The image alt text.</param>
        public void EnterImageAltText(string imageAltText)
        {
            HtmlInputText altTextField = this.EM.Media.ImagePropertiesScreen.ImageAltTextField
                .AssertIsPresent("Image alt text field");

            altTextField.Text = string.Empty;
            altTextField.Text = imageAltText;
            altTextField.AsjQueryControl().InvokejQueryEvent(jQueryControl.jQueryControlEvents.change);
        }

        /// <summary>
        /// Selects None alignment option
        /// </summary>
        public void SelectNoneAlignmentOption()
        {
            HtmlInputRadioButton alignmentOption = this.EM.Media.ImagePropertiesScreen.AlignmentNone
                .AssertIsPresent("None alignment");

            alignmentOption.Click();
        }

        /// <summary>
        /// Selects Left alignment option
        /// </summary>
        public void SelectLeftAlignmentOption()
        {
            HtmlInputRadioButton alignmentOption = this.EM.Media.ImagePropertiesScreen.AlignmentLeft
                .AssertIsPresent("Left alignment");

            alignmentOption.Click();
        }

        /// <summary>
        /// Selects Center alignment option
        /// </summary>
        public void SelectCenterAlignmentOption()
        {
            HtmlInputRadioButton alignmentOption = this.EM.Media.ImagePropertiesScreen.AlignmentCenter
                .AssertIsPresent("Center alignment");

            alignmentOption.Click();
        }

        /// <summary>
        /// Selects Right alignment option
        /// </summary>
        public void SelectRightAlignmentOption()
        {
            HtmlInputRadioButton alignmentOption = this.EM.Media.ImagePropertiesScreen.AlignmentRight
                .AssertIsPresent("Right alignment");

            alignmentOption.Click();
        }

        /// <summary>
        /// Verifies the selected option in thumbnail selector
        /// </summary>
        /// <param name="thumbnailOption">The thumbnail option.</param>
        public void VerifySelectedOptionThumbnailSelector(string thumbnailOption)
        {
            HtmlSelect selector = this.EM.Media.ImagePropertiesScreen.ThumbnailSelector
                .AssertIsPresent("Thumbnail selector");

            Assert.AreEqual(thumbnailOption, selector.SelectedOption.Text);
        }

        /// <summary>
        /// Selects option from thumbnail selector.
        /// </summary>
        /// <param name="optionValue">Option value.</param>
        public void SelectOptionThumbnailSelector(string optionValue)
        {
            HtmlSelect selector = this.EM.Media.ImagePropertiesScreen.ThumbnailSelector
                .AssertIsPresent("Thumbnail selector");

            selector.SelectByText(optionValue);
            selector.AsjQueryControl().InvokejQueryEvent(jQueryControl.jQueryControlEvents.click);
            selector.AsjQueryControl().InvokejQueryEvent(jQueryControl.jQueryControlEvents.change);
        }

        /// <summary>
        /// Changes the image.
        /// </summary>
        public void ChangeImage()
        {
            HtmlButton changeBtn = this.EM.Media.ImagePropertiesScreen.ChangeImageButton.AssertIsPresent("Change image button");

            changeBtn.Click();
            ActiveBrowser.WaitForAsyncRequests();
        }

        /// <summary>
        /// Edits all properties.
        /// </summary>
        public void EditAllProperties()
        {
            HtmlButton editBtn = this.EM.Media.ImagePropertiesScreen.EditAllPropertiesButton.AssertIsPresent("Edit all properties button");
            editBtn.Click();
            ActiveBrowser.WaitForAsyncOperations();
            ActiveBrowser.RefreshDomTree();
        }

        /// <summary>
        /// Enters the new title.
        /// </summary>
        /// <param name="imageTitle">The image title.</param>
        public void EnterNewTitleInPropertiesDialogAndPublish(string imageTitle)
        {
            var frames = Manager.Current.ActiveBrowser.Frames;
           HtmlInputText titleField = frames[0].Find.ByExpression<HtmlInputText>("tagName=input", "id=?_ImageTitleFieldControl_0_ctl00_0_ctl00_0_textBox_write_0")
                .AssertIsPresent("Image title field");
            titleField.Text = string.Empty;
            titleField.Text = imageTitle;

            HtmlAnchor publishBtn = frames[0].Find.ByExpression<HtmlAnchor>("class=sfLinkBtn sfSave", "title=~Publish", "id=?_Publish").AssertIsPresent("Publish button");
            publishBtn.Click();
            ActiveBrowser.WaitForAsyncOperations();
            ActiveBrowser.RefreshDomTree();
        }
   
        /// <summary>
        /// Verifies image on the frontend.
        /// </summary>
        /// <param name="title">The image title.</param>
        /// <param name="altText">The image alt text.</param>
        /// <param name="src">The image src.</param>
        public void VerifyImageThumbnailInPropertiesDialog(string altText, string src)
        {
            HtmlImage image = ActiveBrowser.Find.ByExpression<HtmlImage>("alt=" + altText)
                .AssertIsPresent("image");

            Assert.IsTrue(image.Src.StartsWith(src), "src is not correct");
        }

        /// <summary>
        /// Clicks Done button on image properties dialog.
        /// </summary>
        public void ConfirmImageProperties()
        {
            HtmlButton doneBtn = this.EM.Media.ImagePropertiesScreen.DoneButton.AssertIsPresent("Done button");

            doneBtn.Click();
        }
    }
}
