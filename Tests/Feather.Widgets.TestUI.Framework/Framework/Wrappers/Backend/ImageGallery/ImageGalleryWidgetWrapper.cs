using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArtOfTest.WebAii.Controls.HtmlControls;
using ArtOfTest.WebAii.jQuery;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Feather.Widgets.TestUI.Framework.Framework.Wrappers.Backend.ImageGallery
{
    /// <summary>
    /// This is the entry point class for ImageGalleryWidgetWrapper.
    /// </summary>
    public class ImageGalleryWidgetWrapper : BaseWrapper
    {
        /// <summary>
        /// Selects the radio button option.
        /// </summary>
        /// <param name="optionId">The option id.</param>
        public void SelectRadioButtonOption(ImageGalleryRadioButtonIds optionId)
        {
            HtmlInputRadioButton radioButton = ActiveBrowser.Find.ByExpression<HtmlInputRadioButton>("tagname=input", "id=" + optionId)
                  .AssertIsPresent("radio button");

            radioButton.Click();
        }

        /// <summary>
        /// Verifies the checked radio button option.
        /// </summary>
        /// <param name="optionId">The option id.</param>
        public void VerifyCheckedRadioButtonOption(ImageGalleryRadioButtonIds optionId)
        {
            HtmlInputRadioButton radioButton = ActiveBrowser.Find.ByExpression<HtmlInputRadioButton>("tagname=input", "id=" + optionId)
                  .AssertIsPresent("radio button");

            Assert.IsTrue(radioButton.Checked);
        }

        /// <summary>
        /// Expands the narrow selection by arrow.
        /// </summary>
        public void ExpandNarrowSelectionByArrow()
        {
            HtmlSpan arrow = this.EM.ImageGallery.ImageGalleryWidgetEditScreen.NarrowSelectionByArrow
                  .AssertIsPresent("radio button");

            arrow.Click();
        }

        /// <summary>
        /// Changes the paging or limit value.
        /// </summary>
        /// <param name="number">The number.</param>
        /// <param name="selectedListSettingOption">The selected list setting option.</param>
        public void ChangePagingOrLimitValue(string number, string selectedListSettingOption)
        { 
            HtmlInputText itemsTextBox = ActiveBrowser.Find.ByExpression<HtmlInputText>("ng-disabled=~" + selectedListSettingOption)
                 .AssertIsPresent("text box");

            itemsTextBox.Text = string.Empty;
            itemsTextBox.Text = number;
            itemsTextBox.AsjQueryControl().InvokejQueryEvent(jQueryControl.jQueryControlEvents.change);
        }


        /// <summary>
        /// Selects option from thumbnail selector.
        /// </summary>
        /// <param name="optionValue">Option value.</param>
        public void SelectOptionInThumbnailSelector(string optionValue)
        {
            HtmlSelect selector = this.EM.ImageGallery.ImageGalleryWidgetEditScreen.ThumbnailSelector.FirstOrDefault()
                .AssertIsPresent("Thumbnail selector");

            selector.SelectByText(optionValue);
            selector.AsjQueryControl().InvokejQueryEvent(jQueryControl.jQueryControlEvents.click);
            selector.AsjQueryControl().InvokejQueryEvent(jQueryControl.jQueryControlEvents.change);
        }

        /// <summary>
        /// Selects option from imahe selector.
        /// </summary>
        /// <param name="optionValue">Option value.</param>
        public void SelectOptionInImageSelector(string optionValue)
        {
            HtmlSelect selector = this.EM.ImageGallery.ImageGalleryWidgetEditScreen.ThumbnailSelector.LastOrDefault()
               .AssertIsPresent("Image selector");

            selector.SelectByText(optionValue);
            selector.AsjQueryControl().InvokejQueryEvent(jQueryControl.jQueryControlEvents.click);
            selector.AsjQueryControl().InvokejQueryEvent(jQueryControl.jQueryControlEvents.change);
        }

        /// <summary>
        /// Sorts the options selector.
        /// </summary>
        /// <param name="optionValue">The option value.</param>
        public void SelectOptionInSortingSelector(string optionValue)
        {
            HtmlSelect selector = this.EM.ImageGallery.ImageGalleryWidgetEditScreen.SortImagesSelector
               .AssertIsPresent("Image selector");

            selector.SelectByValue(optionValue);
            selector.AsjQueryControl().InvokejQueryEvent(jQueryControl.jQueryControlEvents.click);
            selector.AsjQueryControl().InvokejQueryEvent(jQueryControl.jQueryControlEvents.change);
        }
    }
}

