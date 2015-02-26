using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArtOfTest.Common.UnitTesting;
using ArtOfTest.WebAii.Controls.HtmlControls;
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
        }

        /// <summary>
        /// Verifies the selected option in thumbnail selector
        /// </summary>
        /// <param name="imageSize">The image size.</param>
        public void VerifySelectedOptionThumbnailSelector(string imageSize)
        {
            HtmlSelect selector = this.EM.Media.ImagePropertiesScreen.ThumbnailSelector
                .AssertIsPresent("Thumbnail selector");

            Assert.AreEqual("Original size: " + imageSize + " px", selector.SelectedOption.Text, "Selected option is not as expected");
        }

        /// <summary>
        /// Selects option from thumbnail selector.
        /// </summary>
        /// <param name="optionValue">Option value.</param>
        public void SelectOptionThumbnailSelector(string optionValue)
        {
            HtmlSelect selector = this.EM.Media.ImagePropertiesScreen.ThumbnailSelector
                .AssertIsPresent("Thumbnail selector");

            selector.SelectByValue(optionValue);
            selector.AsjQueryControl().InvokejQueryEvent(jQueryControl.jQueryControlEvents.click);
            selector.AsjQueryControl().InvokejQueryEvent(jQueryControl.jQueryControlEvents.change);
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
