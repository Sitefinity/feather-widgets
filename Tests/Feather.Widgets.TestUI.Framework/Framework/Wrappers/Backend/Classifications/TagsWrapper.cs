using System;
using System.Linq;
using ArtOfTest.Common.UnitTesting;
using ArtOfTest.WebAii.Controls.HtmlControls;
using ArtOfTest.WebAii.Core;
using ArtOfTest.WebAii.jQuery;

namespace Feather.Widgets.TestUI.Framework.Framework.Wrappers.Backend.Classifications
{
    /// <summary>
    /// This is an entry point for TagsWrapper.
    /// </summary>
    public class TagsWrapper : BaseWrapper
    {
        /// <summary>
        /// Verifies the selected sorting option.
        /// </summary>
        /// <param name="sortingOption">The sorting option.</param>
        public void VerifySelectedSortingOption(string sortingOption)
        {
            HtmlSelect sortingOptionsDropdown = this.EM.Classifications.TagsWidgetEditScreen.SortDropdown.AssertIsPresent("Sorting option dropdown");

            Assert.AreEqual(sortingOption, sortingOptionsDropdown.SelectedOption.Text);
        }

        /// <summary>
        /// Selects the tags template.
        /// </summary>
        /// <param name="template">The template.</param>
        public void SelectTagsTemplate(TagsTemplates template)
        {
            HtmlSelect templateDropdown = this.EM.Classifications.TagsWidgetEditScreen.TagsTemplateDropdown.AssertIsPresent("Tags template dropdown");
            templateDropdown.SelectByText(template.ToString(), true);
        }

        /// <summary>
        /// Verifies the selected tags template option.
        /// </summary>
        /// <param name="template">The template.</param>
        public void VerifySelectedTagsTemplateOption(TagsTemplates template)
        {
            HtmlSelect optionsDropdown = this.EM.Classifications.TagsWidgetEditScreen.TagsTemplateDropdown.AssertIsPresent("Tags template dropdown");

            Assert.AreEqual(template.ToString(), optionsDropdown.SelectedOption.Text);
        }

        /// <summary>
        /// Verifies the checked radio button option.
        /// </summary>
        /// <param name="optionId">The option id.</param>
        public void VerifyCheckedRadioButtonOption(TagsRadioButtonIds optionId)
        {
            HtmlFindExpression expression = new HtmlFindExpression("tagname=input", "id=" + optionId);
            ActiveBrowser.WaitForElement(expression, 60000, false);
            HtmlInputRadioButton radioButton = ActiveBrowser.Find.ByExpression<HtmlInputRadioButton>("tagname=input", "id=" + optionId)
                  .AssertIsPresent("radio button");

            Assert.IsTrue(radioButton.Checked);
        }

        /// <summary>
        /// Selects the radio button option.
        /// </summary>
        /// <param name="optionId">The option id.</param>
        public void SelectRadioButtonOption(TagsRadioButtonIds optionId)
        {
            HtmlFindExpression expression = new HtmlFindExpression("tagname=input", "id=" + optionId);
            ActiveBrowser.WaitForElement(expression, 60000, false);
            HtmlInputRadioButton radioButton = ActiveBrowser.Find.ByExpression<HtmlInputRadioButton>("tagname=input", "id=" + optionId)
                  .AssertIsPresent("radio button");

            radioButton.Click();
        }

        /// <summary>
        /// Selects the used by content type option.
        /// </summary>
        /// <param name="contentType">Type of the content.</param>
        public void SelectUsedByContentTypeOption(string contentType)
        {
            HtmlSelect contentTypeDropdown = this.EM.Classifications.TagsWidgetEditScreen.UsedByContentTypeDropdown.AssertIsPresent("contentType dropdown");

            contentTypeDropdown.SelectByText(contentType, true);
        }
    }
}