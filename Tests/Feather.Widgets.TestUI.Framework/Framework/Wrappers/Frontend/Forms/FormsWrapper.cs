using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArtOfTest.Common.UnitTesting;
using ArtOfTest.WebAii.Controls.HtmlControls;
using ArtOfTest.WebAii.Core;
using ArtOfTest.WebAii.jQuery;

namespace Feather.Widgets.TestUI.Framework.Framework.Wrappers.Frontend.Forms
{
    /// <summary>
    /// This is an entry point for FormsWrapper.
    /// </summary>
    public class FormsWrapper : BaseWrapper
    {
        /// <summary>
        /// Verify if text field label is visible
        /// </summary>
        public void VerifyTextFieldLabelIsVisible(string fieldLabel)
        {
            Assert.IsTrue(EM.Forms.FormsFrontend.TextboxField.InnerText.Contains(fieldLabel));
        }

        /// <summary>
        /// Verify if checkboxes field label is visible
        /// </summary>
        public void VerifyCheckboxesFieldLabelIsVisible(string fieldLabel)
        {
            Assert.IsTrue(EM.Forms.FormsFrontend.CheckboxesField.InnerText.Contains(fieldLabel));
        }

        /// <summary>
        /// Verify if multiple choice field label is visible
        /// </summary>
        public void VerifyMultipleChoiceFieldLabelIsVisible(string fieldLabel)
        {
            Assert.IsTrue(EM.Forms.FormsFrontend.MultipleChoiceField.InnerText.Contains(fieldLabel));
        }

        /// <summary>
        /// Verify if dropdown list field label is visible
        /// </summary>
        public void VerifyDropdownListFieldLabelIsVisible(string fieldLabel)
        {
            Assert.IsTrue(EM.Forms.FormsFrontend.DropdownListField.InnerText.Contains(fieldLabel));
        }

        /// <summary>
        /// Verify if content block content is visible
        /// </summary>
        public void VerifyContentBlockFieldTextIsVisible(string contentText)
        {
            HtmlDiv frontendPageMainDiv = BAT.Wrappers().Frontend().Pages().PagesWrapperFrontend().GetPageContent();
            Assert.IsTrue(frontendPageMainDiv.InnerText.Contains(contentText));
        }

        /// <summary>
        /// Verify if Paragraph text field label is visible
        /// </summary>
        public void VerifyParagraphTextFieldLabelIsVisible(string fieldLabel)
        {
            Assert.IsTrue(EM.Forms.FormsFrontend.ParagraphTextField.InnerText.Contains(fieldLabel));
        }

        /// <summary>
        /// Sets the TextBox Content in the Frontend of the form
        /// </summary>
        public void SetTextboxContent(string content)
        {
            HtmlInputText textbox = this.EM.Forms.FormsFrontend.TextField.AssertIsPresent("Text field");
            textbox.SimulateTextTyping(content, Telerik.TestUI.Core.WebAii.Tools.UserInput.SimulationType.LastKeyStroke);
        }

        /// <summary>
        /// Sets the Paragraph Text Content in the Frontend of the form
        /// </summary>
        public void SetParagraphTextContent(string content)
        {
            HtmlTextArea textbox = this.EM.Forms.FormsFrontend.ParagraphTextBox.AssertIsPresent("Text field");
            textbox.MouseClick();
            Manager.Current.Desktop.KeyBoard.TypeText(content);
        }

        /// <summary>
        /// Clicks the submit button in the frontend of the form
        /// </summary>
        public void SubmitForm()
        {
            HtmlButton submitButton = EM.Forms.FormsFrontend.SubmitButton;
            submitButton.MouseClick();

            this.WaitForSuccessMessage();
        }

        /// <summary>
        /// Wait for success message after the form is submitted
        /// </summary>
        public void WaitForSuccessMessage()
        {
            var successMsg = ActiveBrowser.Find.AssociatedBrowser.GetControl<HtmlDiv>("tagname=div", "innertext=Success! Thanks for filling out our form!");
        }

        /// <summary>
        /// Selects checkbox from checkboxes field
        /// </summary>
        public void SelectCheckbox(string choice)
        {
            HtmlInputCheckBox checkbox = ActiveBrowser.Find.ByExpression<HtmlInputCheckBox>("tagname=input", "data-sf-role=checkboxes-field-input", "value=" + choice);
            checkbox.Click();
        }

        /// <summary>
        /// Selects radio button from multiplechoice field
        /// </summary>
        public void SelectRadioButton(string choice)
        {
            HtmlInputRadioButton checkbox = ActiveBrowser.Find.ByExpression<HtmlInputRadioButton>("tagname=input", "data-sf-role=multiple-choice-field-input", "value=" + choice);
            checkbox.Click();
        }

        /// <summary>
        /// Selects option from dropdown field
        /// </summary>
        public void SelectDropdownOption(string choice)
        {
            HtmlSelect dropdown = ActiveBrowser.Find.ByExpression<HtmlSelect>("tagname=select", "data-sf-role=dropdown-list-field-select");
            dropdown.SelectByText(choice);
            dropdown.AsjQueryControl().InvokejQueryEvent(jQueryControl.jQueryControlEvents.click);
            dropdown.AsjQueryControl().InvokejQueryEvent(jQueryControl.jQueryControlEvents.change);
        }
    }
}
