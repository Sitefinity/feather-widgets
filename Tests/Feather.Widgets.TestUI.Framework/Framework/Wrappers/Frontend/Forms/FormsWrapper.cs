using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArtOfTest.Common.UnitTesting;
using ArtOfTest.WebAii.Controls.HtmlControls;
using ArtOfTest.WebAii.Core;
using ArtOfTest.WebAii.jQuery;
using Telerik.TestUI.Core.Asserts;
using Telerik.TestUI.Core.Navigation;

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
        /// Verify if checkboxes field label is NOT visible
        /// </summary>
        public void VerifyCheckboxesFieldLabelIsNotVisible(string fieldLabel)
        {
            Assert.IsFalse(EM.Forms.FormsFrontend.CheckboxesField.InnerText.Contains(fieldLabel));
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
        /// <param name="contentText">The content text.</param>
        /// <param name="isVisible">if set to <c>true</c> [is visible].</param>
        public void VerifyContentBlockFieldTextIsVisible(string contentText, bool isVisible = true)
        {
            HtmlDiv frontendPageMainDiv = BAT.Wrappers().Frontend().Pages().PagesWrapperFrontend().GetPageContent();
            if (!isVisible)
            {
                Assert.IsFalse(frontendPageMainDiv.InnerText.Contains(contentText));
            }
            else
            {
                Assert.IsTrue(frontendPageMainDiv.InnerText.Contains(contentText));
            }
        }

        /// <summary>
        /// Verifies the submit buttons count in front end.
        /// </summary>
        /// <param name="expectedCount">The expected count.</param>
        public void VerifySubmitButtonsCountInFrontEnd(int expectedCount)
        {
            var submitButtons = ActiveBrowser.Find.AllByExpression("tagName=button", "class=sf-SubmitButton btn btn-primary");
            Assert.AreEqual(expectedCount, submitButtons.Count);
        }

        /// <summary>
        /// Verify if Paragraph text field label is visible
        /// </summary>
        public void VerifyParagraphTextFieldLabelIsVisible(string fieldLabel)
        {
            Assert.IsTrue(EM.Forms.FormsFrontend.ParagraphTextField.InnerText.Contains(fieldLabel));
        }

        /// <summary>
        /// Verify if text field is visible
        /// </summary>
        public void VerifyTextFieldlIsVisibleHybrid()
        {
            Assert.IsNotNull(EM.Forms.FormsFrontend.TextboxFieldHybrid, "Text field is not");
            Assert.IsTrue(EM.Forms.FormsFrontend.TextboxFieldHybrid.IsVisible(), "The text input field is not visible");
        }

        /// <summary>
        /// Verify if Submit button is visible
        /// </summary>
        public void VerifySubmitButtonIsVisible()
        {
            Assert.IsTrue(EM.Forms.FormsFrontend.SubmitButton.IsVisible(), "The submit button is not visible");
        }

        /// <summary>
        /// Verify if Submit button is not visible
        /// </summary>
        public void VerifySubmitButtonIsNotVisible()
        {
            Assert.IsFalse(EM.Forms.FormsFrontend.SubmitButton.IsVisible(), "The submit button is visible");
        }
              
        /// <summary>
        /// Verify the delete form in use message is shown on the frontend
        /// </summary>
        public void VerifyMessageIsDisplayedAfterFormIsDeleted()
        {
            var message = EM.Forms.FormsFrontend.DeleteFormInUseMessage;
            Assert.AreEqual(message.InnerText, "The specified form no longer exists or is currently unpublished.");
            Assert.IsTrue(message.IsVisible(), String.Format("Total amount {0} was not found", message.InnerText));
        }

        /// <summary>
        /// Sets the TextBox Content in the Frontend of the form
        /// </summary>
        public void SetTextboxContent(string content)
        {
            HtmlInputText textbox = this.EM.Forms.FormsFrontend.TextField.AssertIsPresent("Text field");

            textbox.ScrollToVisible();
            textbox.Focus();
            textbox.MouseClick();

            Manager.Current.Desktop.KeyBoard.KeyDown(System.Windows.Forms.Keys.Control);
            Manager.Current.Desktop.KeyBoard.KeyPress(System.Windows.Forms.Keys.A);
            Manager.Current.Desktop.KeyBoard.KeyUp(System.Windows.Forms.Keys.Control);
            Manager.Current.Desktop.KeyBoard.KeyPress(System.Windows.Forms.Keys.Delete);

            Manager.Current.Desktop.KeyBoard.TypeText(content);
            Manager.Current.Desktop.KeyBoard.KeyPress(System.Windows.Forms.Keys.Tab);
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
        /// Clicks the submit button in the frontend of the form and checks the succsess message
        /// </summary>
        public void SubmitForm()
        {
            HtmlButton submitButton = EM.Forms.FormsFrontend.SubmitButton;
            submitButton.MouseClick();

            this.WaitForSuccessMessage();
        }

        /// <summary>
        /// Clicks the submit button multiple times in the frontend of the form
        /// </summary>
        /// <param name="count">The count.</param>
        public void MultipleSubmitForm(int count)
        {
            HtmlButton submitButton = EM.Forms.FormsFrontend.SubmitButton;
            for (int i = 0; i < count; i++)
            {
                submitButton.MouseClick();
            }

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
        /// Clicks the submit button in the frontend of the form
        /// </summary>
        public void ClickSubmit()
        {
            HtmlButton submitButton = EM.Forms.FormsFrontend.SubmitButton;
            submitButton.MouseClick();
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
            checkbox.MouseClick();
            ActiveBrowser.WaitUntilReady();
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

        /// <summary>
        /// Verify details news page URL
        /// </summary>
        public void VerifyPageUrl(string pageName)
        {
            Assert.IsTrue(ActiveBrowser.Url.EndsWith(pageName.ToLower()));
        }

        /// <summary>
        /// Verify Checkboxes widget is deleted or Null
        /// </summary>
        public void VerifyCheckboxesFieldIsNotVisible()
        {
            Assert.IsNull(EM.Forms.FormsFrontend.CheckboxesField, "Checkboxes field is still visible at the frontend");
        }

        /// <summary>
        /// Verify if dropdown list field  is NOT visible
        /// </summary>
        public void VerifyDropdownListFieldIsNotVisible()
        {
            Assert.IsNull(EM.Forms.FormsFrontend.DropdownListField);
        }

        /// <summary>
        /// Verify if ParagraphTextField  is NOT visible
        /// </summary>
        public void VerifyParagraphTextFieldIsNotVisible()
        {
            Assert.IsNull(EM.Forms.FormsFrontend.ParagraphTextField);
        }

        /// <summary>
        /// Verify if MultipleChoiceField is NOT visible
        /// </summary>
        public void VerifyMultipleChoiceFieldIsNotVisible()
        {
            Assert.IsNull(EM.Forms.FormsFrontend.MultipleChoiceField);
        }

        /// <summary>
        /// Verify if CaptchaFieldContainer is NOT visible
        /// </summary>
        public void VerifyCaptchaFieldIsNotVisible()
        {
            Assert.IsNull(EM.Forms.FormsFrontend.CaptchaField);
        }

        /// <summary>
        /// Verify CaptchaField widget is visble
        /// </summary>
        public void VerifyCaptchaFieldContainerIsVisible()
        {
            Assert.IsNotNull(EM.Forms.FormsFrontend.CaptchaField, "CaptchaField is not visible at the frontend");
            Assert.IsTrue(EM.Forms.FormsFrontend.CaptchaField.IsVisible(), "CaptchaField is not visible");
        }

        /// <summary>
        /// Verify TextboxField widget is visble
        /// </summary>
        public void VerifyTextboxFieldContainerIsVisible()
        {
            Assert.IsNotNull(EM.Forms.FormsFrontend.TextboxField, "TextboxField is not visible at the frontend");
            Assert.IsTrue(EM.Forms.FormsFrontend.TextboxField.IsVisible(), "TextboxField is not visible");
        }

        /// <summary>
        /// Verify if paragraph widget is visible
        /// </summary>
        public void VerifyParagraphFieldContainerIsVisible()
        {
            Assert.IsNotNull(EM.Forms.FormsFrontend.ParagraphTextField, "ParagraphTextField is not found");
            Assert.IsTrue(EM.Forms.FormsFrontend.ParagraphTextField.IsVisible(), "ParagraphTextField is not visible");
        }

        /// <summary>
        /// Verify MultipleChoiceFieldContainer widget visble
        /// </summary>
        public void VerifyMultipleChoiceFieldContainerIsVisible()
        {
            Assert.IsNotNull(EM.Forms.FormsFrontend.MultipleChoiceField, "MultipleChoiceField is not visible at the frontend");
            Assert.IsTrue(EM.Forms.FormsFrontend.MultipleChoiceField.IsVisible(), "MultipleChoiceField is not visible");
        }

        /// <summary>
        /// Verify FileUploadField widget is visble
        /// </summary>
        public void VerifyFileUploadFieldContainerIsVisible()
        {
            Assert.IsNotNull(EM.Forms.FormsFrontend.FileUploadField, "FileUploadField is not visible at the frontend");
            Assert.IsTrue(EM.Forms.FormsFrontend.FileUploadField.IsVisible(), "FileUploadField is not visible");
        }

        /// <summary>
        /// Verify Checkboxes field widget is visble
        /// </summary>
        public void VerifyCheckboxesFieldContainerIsVisible()
        {
            Assert.IsNotNull(EM.Forms.FormsFrontend.CheckboxesField, "CheckboxesField is not visible at the frontend");
            Assert.IsTrue(EM.Forms.FormsFrontend.CheckboxesField.IsVisible(), "CheckboxesField is not visible");
        }

        /// <summary>
        /// Verify Dropdown field widget is visble
        /// </summary>
        public void VerifyDropdownFieldContainerIsVisible()
        {
            Assert.IsNotNull(EM.Forms.FormsFrontend.DropdownListField, "DropdownListField is not visible at the frontend");
            Assert.IsTrue(EM.Forms.FormsFrontend.DropdownListField.IsVisible(), "DropdownListField is not visible");
        }

        /// <summary>
        /// Navigates to preview form 
        /// </summary>
        /// <returns>
        /// An instance of the <see cref="PageAssertFacade"/> facade which provides functionality 
        /// for verifying the page.
        /// </returns>
        public PageAssertFacade PreviewForm(string formName, string culture = null )
        {
            string pagesUrl;
            if (culture != null)
            {
                pagesUrl = "~/sitefinity/forms/" + formName + "/" + "Preview" + "/" + culture;
            }
            else
            {
                pagesUrl = "~/sitefinity/forms/" + formName + "/" + "Preview";
            }

            Navigator.Navigate(pagesUrl);
            return new PageAssertFacade();
        }

        /// <summary>
        /// Verify success message after the form is submitted is not shown
        /// </summary>
        public void VerifySuccessSubmitMessageIsNotShown()
        {
            Assert.IsNull(EM.Forms.FormsFrontend.SuccessMessage, "SuccessMessage should not be shown");
        }
    }
}
