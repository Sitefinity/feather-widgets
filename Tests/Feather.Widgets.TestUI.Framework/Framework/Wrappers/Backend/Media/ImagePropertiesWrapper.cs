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
    public class ImagePropertiesWrapper : MediaPropertiesBaseWrapper
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
        /// <param name="documentTitle">The image title.</param>
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
            HtmlInputRadioButton alignmentOption = this.EM.Media.ImagePropertiesScreen.AlignmentNone.AssertIsPresent("None alignment");
            alignmentOption.Click();
        }

        /// <summary>
        /// Selects Left alignment option
        /// </summary>
        public void SelectLeftAlignmentOption()
        {
            HtmlInputRadioButton alignmentOption = this.EM.Media.ImagePropertiesScreen.AlignmentLeft.AssertIsPresent("Left alignment");
            alignmentOption.Click();
        }

        /// <summary>
        /// Selects Center alignment option
        /// </summary>
        public void SelectCenterAlignmentOption()
        {
            HtmlInputRadioButton alignmentOption = this.EM.Media.ImagePropertiesScreen.AlignmentCenter.AssertIsPresent("Center alignment");
            alignmentOption.Click();
        }

        /// <summary>
        /// Selects Right alignment option
        /// </summary>
        public void SelectRightAlignmentOption()
        {
            HtmlInputRadioButton alignmentOption = this.EM.Media.ImagePropertiesScreen.AlignmentRight.AssertIsPresent("Right alignment");
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
        /// Verifies image on the frontend.
        /// </summary>
        /// <param name="title">The image title.</param>
        /// <param name="altText">The image alt text.</param>
        /// <param name="src">The image src.</param>
        public void VerifyImageThumbnailInPropertiesDialog(string altText, string src)
        {
            HtmlImage image = ActiveBrowser.Find
                                           .ByExpression<HtmlImage>("alt=" + altText)
                                           .AssertIsPresent("image");

            Assert.IsTrue(image.Src.StartsWith(src), "src is not correct");
        }

        /// <summary>
        /// Enters the width of the max.
        /// </summary>
        /// <param name="number">The number.</param>
        public void EnterWidth(string number)
        {
            HtmlInputNumber numberField = this.EM.Media.ImagePropertiesScreen.MaxWidthNumber
                                              .AssertIsPresent("max width");

            numberField.Text = number;
            numberField.AsjQueryControl().InvokejQueryEvent(jQueryControl.jQueryControlEvents.change);
        }

        /// <summary>
        /// Enters the height of the max.
        /// </summary>
        /// <param name="number">The number.</param>
        public void EnterHeight(string number)
        {
            HtmlInputNumber numberField = this.EM.Media.ImagePropertiesScreen.MaxHeightNumber
                                              .AssertIsPresent("max height");

            numberField.Text = number;
            numberField.AsjQueryControl().InvokejQueryEvent(jQueryControl.jQueryControlEvents.change);
        }

        /// <summary>
        /// Verifies the width is required message.
        /// </summary>
        public void VerifyWidthIsRequiredMessage()
        {
            this.EM.Media.ImagePropertiesScreen.WidthIsRequiredMessage.AssertIsPresent("width message");
        }

        /// <summary>
        /// Verifies the height is required message.
        /// </summary>
        public void VerifyHeightIsRequiredMessage()
        {
            this.EM.Media.ImagePropertiesScreen.HeightIsRequiredMessage.AssertIsPresent("height message");
        }

        /// <summary>
        /// Selects option from thumbnail selector.
        /// </summary>
        /// <param name="optionValue">Option value.</param>
        public void SelectResizeImageOption(string optionValue)
        {
            HtmlFindExpression expression = new HtmlFindExpression("class=modal-title", "InnerText=Custom thumbnail size");
            ActiveBrowser.WaitForElement(expression, 30000, false);

            HtmlSelect selector = this.EM.Media.ImagePropertiesScreen.ResizeImageSelector
                                      .AssertIsPresent("resize image selector");

            selector.SelectByText(optionValue);
            selector.AsjQueryControl().InvokejQueryEvent(jQueryControl.jQueryControlEvents.click);
            selector.AsjQueryControl().InvokejQueryEvent(jQueryControl.jQueryControlEvents.change);
        }

        /// <summary>
        /// Selects the quality option.
        /// </summary>
        /// <param name="optionValue">The option value.</param>
        public void SelectQualityOption(string optionValue)
        {
            HtmlSelect selector = this.EM.Media.ImagePropertiesScreen.QualitySelector
                                      .AssertIsPresent("Quality selector");

            selector.SelectByText(optionValue);
            selector.AsjQueryControl().InvokejQueryEvent(jQueryControl.jQueryControlEvents.click);
            selector.AsjQueryControl().InvokejQueryEvent(jQueryControl.jQueryControlEvents.change);
        }

        /// <summary>
        /// Dones the resizing.
        /// </summary>
        public void DoneResizing()
        {
            HtmlButton doneBtn = this.EM.Media.ImagePropertiesScreen.DoneResizingButton.AssertIsPresent("Done button");

            doneBtn.Click();
            ActiveBrowser.WaitForAsyncOperations();
            ActiveBrowser.RefreshDomTree();
        }

        /// <summary>
        /// Clicks the this image is A link.
        /// </summary>
        public void ClickThisImageIsALink()
        {
            HtmlInputCheckBox link = this.EM.Media.ImagePropertiesScreen.ThisImageIsALinkCheckBox.AssertIsPresent("this image is a link");

            link.Click();
        }
    }
}