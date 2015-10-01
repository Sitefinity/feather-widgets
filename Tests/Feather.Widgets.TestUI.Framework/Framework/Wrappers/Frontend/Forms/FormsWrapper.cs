using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArtOfTest.Common.UnitTesting;
using ArtOfTest.WebAii.Controls.HtmlControls;
using ArtOfTest.WebAii.Core;

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
        public void VerifyFieldLabelIsVisible(string fieldLabel)
        {
            Assert.IsTrue(EM.Forms.FormsFrontend.TextboxLabel.InnerText.Contains(fieldLabel));
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
    }
}
