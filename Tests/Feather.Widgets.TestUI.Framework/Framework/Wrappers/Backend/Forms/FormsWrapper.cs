using System;
using ArtOfTest.Common.UnitTesting;
using ArtOfTest.WebAii.Controls.HtmlControls;
using ArtOfTest.WebAii.Core;

namespace Feather.Widgets.TestUI.Framework.Framework.Wrappers.Backend.Forms
{
    /// <summary>
    /// This is an entry point for FormsWrapper.
    /// </summary>
    public class FormsWrapper : BaseWrapper
    {
        /// <summary>
        /// Verify number of responses in backend
        /// </summary>
        public void VerifyNumberOfResponses(int expectedResponsesCount)
        {
            var actualResponsesCount = BAT.Wrappers().Backend().Forms().FormsResponseScreen().GetResponsesCountInGrid();
            Assert.AreEqual(expectedResponsesCount, actualResponsesCount, "The expected responses count differs from the actual response count.");
        }

        /// <summary>
        /// Verify response author name in backend
        /// </summary>
        public void VerifyResponseAuthorUsername(string expectedAuthorName)
        {
            var actualAuthorUsername = BAT.Wrappers().Backend().Forms().FormsResponseScreen().GetResponseAuthorUsername();
            Assert.AreEqual(expectedAuthorName, actualAuthorUsername, "Actual form response author differs from the expected one");
        }

        /// <summary>
        /// Verify response submit date in backend
        /// </summary>
        public void VerifyResponseSubmitDate()
        {
            var actualSubmitDate = BAT.Wrappers().Backend().Forms().FormsResponseScreen().GetResponseSubmitDate();
            Assert.IsTrue(actualSubmitDate.Day == DateTime.Now.Day && actualSubmitDate.Month == DateTime.Now.Month, "Actual submit date differs from the expected one");
        }

        /// <summary>
        /// Verify response text in backend
        /// </summary>
        public void VerifyResponseTextboxAnswer(string textBoxContent)
        {
            var actualTextboxAnswer = BAT.Wrappers().Backend().Forms().FormsResponseScreen().GetResponseTextboxAnswer();
            Assert.AreEqual(textBoxContent, actualTextboxAnswer, "Actual form response differs from the expected one");
        }

        /// <summary>
        /// Verify response for checkbox field in backend
        /// </summary>
        public void VerifyResponseCheckboxesAnswer(string checkboxContent)
        {            
            var checkboxChoiceFieldAnswer = EM.Forms.FormsBackend.GetResponseCheckboxesField;
            checkboxChoiceFieldAnswer.AssertIsPresent("Checkbox Choice Field Text");
            Assert.AreEqual<string>(checkboxChoiceFieldAnswer.InnerText, checkboxContent);
        }

        /// <summary>
        /// Verify response for multiple choice field in backend
        /// </summary>
        public void VerifyResponseMultipleChoiceAnswer(string multipleChoiceContent)
        {
            var multipleChoiceFieldAnswer = ActiveBrowser.Find.ByExpression<HtmlDiv>("TagName=div", "id=~frmRspnsesCntView_formsBackendListDetail", "innertext=" + multipleChoiceContent);
            multipleChoiceFieldAnswer.AssertIsPresent("Multiple Choice Field Text");
        }

        /// <summary>
        /// Verify response for dropdown list in backend
        /// </summary>
        public void VerifyResponseDropdownListAnswer(string dropdownContent)
        {
            var dropdownChoiceFieldAnswer = EM.Forms.FormsBackend.GetResponseDropdownListField;
            dropdownChoiceFieldAnswer.AssertIsPresent("Dropdown List Choice Field Text");
            Assert.AreEqual<string>(dropdownChoiceFieldAnswer.InnerText, dropdownContent);
        }

        /// <summary>
        /// Verify response for content block field in backend
        /// </summary>
        public void VerifyResponseContentBlockAnswer(string contentText)
        {
            var dropdownChoiceFieldAnswer = EM.Forms.FormsBackend.ResponseDetailsPane;
            dropdownChoiceFieldAnswer.Find.ByExpression<HtmlDiv>("TagName=div", "innertext=" + contentText);
            dropdownChoiceFieldAnswer.AssertIsPresent("Dropdown List Choice Field Text");
        }

        /// <summary>
        /// Sets the Section Header content in the field designer
        /// </summary>
        public void SetSectionHeaderText(string sectionHeader)
        {
            HtmlTableCell textbox = this.EM.Forms.FormsBackend.SectionHeaderText.AssertIsPresent("Text field");
            textbox.MouseClick();
            Manager.Current.Desktop.KeyBoard.TypeText(sectionHeader);
        }
    }
}
