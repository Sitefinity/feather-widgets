using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArtOfTest.WebAii.Controls.HtmlControls;
using ArtOfTest.WebAii.jQuery;
using ArtOfTest.Common.UnitTesting;
using ArtOfTest.WebAii.Core;

namespace Feather.Widgets.TestUI.Framework.Framework.Wrappers.Backend.Classifications
{
    /// <summary>
    /// This is an entry point for TagsWrapper.
    /// </summary>
    public class TagsWrapper : BaseWrapper
    {
        /// <summary>
        /// Verifies the default sorting option.
        /// </summary>
        /// <param name="sortingOption">The sorting option.</param>
        public void VerifySelectedSortingOption(string sortingOption)
        {
            HtmlSelect sortingOptionsDropdown = this.EM.Classifications.TagsWidgetEditScreen.SortDropdown.AssertIsPresent("Sorting option dropdown");

            Assert.AreEqual(sortingOption, sortingOptionsDropdown.SelectedOption.Text);
        }

        /// <summary>
        /// Selects option in list template
        /// </summary>
        /// <param name="templateName">template name</param>
        public void SelectListTemplate(string templateName)
        {
            HtmlSelect listTemplateDropdown = this.EM.Classifications.TagsWidgetEditScreen.TemplateDropdown.AssertIsPresent("List template dropdown");

            listTemplateDropdown.SelectByText(templateName);
            listTemplateDropdown.AsjQueryControl().InvokejQueryEvent(jQueryControl.jQueryControlEvents.click);
            listTemplateDropdown.AsjQueryControl().InvokejQueryEvent(jQueryControl.jQueryControlEvents.change);
        }

        /// <summary>
        /// Verifies the selected list template option.
        /// </summary>
        /// <param name="option">The option.</param>
        public void VerifySelectedListTemplateOption(string option)
        {
            HtmlSelect optionsDropdown = this.EM.Classifications.TagsWidgetEditScreen.TemplateDropdown.AssertIsPresent("List template dropdown");

            Assert.AreEqual(option, optionsDropdown.SelectedOption.Text);
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
        /// Selects the content type option.
        /// </summary>
        /// <param name="contentType">Type of the content.</param>
        public void SelectContentTypeOption(string contentType)
        {
            HtmlSelect contentTypeDropdown = this.EM.Classifications.TagsWidgetEditScreen.UsedByContentTypeOption.AssertIsPresent("contentType dropdown");

            contentTypeDropdown.SelectByText(contentType);
            contentTypeDropdown.AsjQueryControl().InvokejQueryEvent(jQueryControl.jQueryControlEvents.click);
            contentTypeDropdown.AsjQueryControl().InvokejQueryEvent(jQueryControl.jQueryControlEvents.change);
        }
    }
}