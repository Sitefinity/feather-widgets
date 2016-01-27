using System;
using System.Linq;
using ArtOfTest.Common.UnitTesting;
using ArtOfTest.WebAii.Controls.HtmlControls;
using ArtOfTest.WebAii.Core;
using ArtOfTest.WebAii.jQuery;

namespace Feather.Widgets.TestUI.Framework.Framework.Wrappers.Backend.Classifications
{
    /// <summary>
    /// This is an entry point for CategoriesWrapper.
    /// </summary>
    public class CategoriesWrapper : BaseWrapper
    {
        /// <summary>
        /// Verifies the checked radio button option.
        /// </summary>
        /// <param name="optionId">The option id.</param>
        public void VerifyCheckedRadioButtonOption(CategoriesRadioButtonIds optionId)
        {
            ActiveBrowser.WaitForElement("tagname=input", "id=" + optionId);
            HtmlInputRadioButton radioButton = ActiveBrowser.Find.ByExpression<HtmlInputRadioButton>("tagname=input", "id=" + optionId)
                  .AssertIsPresent("radio button");

            Assert.IsTrue(radioButton.Checked);
        }

        /// <summary>
        /// Verifies the selected sorting option.
        /// </summary>
        /// <param name="sortingOption">The sorting option.</param>
        public void VerifySelectedSortingOption(string sortingOption)
        {
            HtmlSelect sortingOptionsDropdown = this.EM.Classifications.CategoriesWidgetEditScreen.SortDropdown.AssertIsPresent("Sorting option dropdown");

            Assert.AreEqual(sortingOption, sortingOptionsDropdown.SelectedOption.Text);
        }

        /// <summary>
        /// Switches to Settings tab.
        /// </summary>
        public void SwitchToSettingsTab()
        {
            HtmlAnchor listSettingsTab = this.EM.Classifications.CategoriesWidgetEditScreen.SettingsTab
                                             .AssertIsPresent("Settings");

            listSettingsTab.Click();
            ActiveBrowser.WaitForAsyncRequests();
            ActiveBrowser.RefreshDomTree();
        }

        /// <summary>
        /// Check Show Empty Categories checkbox.
        /// </summary>
        public void CheckShowEmptyCategories()
        {
            HtmlInputCheckBox emptyCategories = this.EM.Classifications.CategoriesWidgetEditScreen.ShowEmptyCategories
                                             .AssertIsPresent("Show empty categories");

            emptyCategories.Click();
            ActiveBrowser.WaitForAsyncRequests();
            ActiveBrowser.RefreshDomTree();
        }

        /// <summary>
        /// Selects the radio button option.
        /// </summary>
        /// <param name="optionId">The option id.</param>
        public void SelectRadioButtonOption(CategoriesRadioButtonIds optionId)
        {
            ActiveBrowser.WaitForElement(new HtmlFindExpression("tagname=input", "id=" + optionId));
            HtmlInputRadioButton radioButton = ActiveBrowser.Find.ByExpression<HtmlInputRadioButton>("tagname=input", "id=" + optionId)
                  .AssertIsPresent("radio button");

            radioButton.Click();
        }

        /// <summary>
        /// Selects the sorting option.
        /// </summary>
        /// <param name="optionId">The option id.</param>
        public void SelectSortingOption(string optionId)
        {
            HtmlSelect contentTypeDropdown = this.EM.Classifications.CategoriesWidgetEditScreen.SortDropdown.AssertIsPresent("Sort categories");

            contentTypeDropdown.SelectByText(optionId, true);
        }

        /// <summary>
        /// Selects the used by content type option.
        /// </summary>
        /// <param name="contentType">Type of the content.</param>
        public void SelectUsedByContentTypeOption(string contentType)
        {
            HtmlSelect contentTypeDropdown = this.EM.Classifications.CategoriesWidgetEditScreen.UsedByContentTypeDropdown.AssertIsPresent("contentType dropdown");

            contentTypeDropdown.SelectByText(contentType, true);
        }
    }
}