using System;
using System.Linq;
using ArtOfTest.Common.UnitTesting;
using ArtOfTest.WebAii.Controls.HtmlControls;
using ArtOfTest.WebAii.Core;
using ArtOfTest.WebAii.jQuery;

namespace Feather.Widgets.TestUI.Framework.Framework.Wrappers.Backend.CardWidget
{
    /// <summary>
    /// This is the entry point class for card widget wrapper.
    /// </summary>
    public class CardWrapper : BaseWrapper
    {
        /// <summary>
        /// Fill heading
        /// </summary>
        /// <param name="headingText">Heading text</param>
        public void FillHeadingText(string headingText)
        {
            HtmlInputText headingInput = EM.Card.CardEditScreen.HeadingTextField
                .AssertIsPresent("Heading field");

            headingInput.ScrollToVisible();
            headingInput.Focus();
            headingInput.MouseClick();

            Manager.Current.Desktop.KeyBoard.KeyDown(System.Windows.Forms.Keys.Control);
            Manager.Current.Desktop.KeyBoard.KeyPress(System.Windows.Forms.Keys.A);
            Manager.Current.Desktop.KeyBoard.KeyUp(System.Windows.Forms.Keys.Control);
            Manager.Current.Desktop.KeyBoard.KeyPress(System.Windows.Forms.Keys.Delete);

            Manager.Current.Desktop.KeyBoard.TypeText(headingText);
        }

        /// <summary>
        /// Fill text area
        /// </summary>
        /// <param name="text">Text</param>
        public void FillTextArea(string text)
        {
            HtmlTextArea textArea = EM.Card.CardEditScreen.TextArea
                .AssertIsPresent("Text area");

            textArea.ScrollToVisible();
            textArea.Focus();
            textArea.MouseClick();

            Manager.Current.Desktop.KeyBoard.KeyDown(System.Windows.Forms.Keys.Control);
            Manager.Current.Desktop.KeyBoard.KeyPress(System.Windows.Forms.Keys.A);
            Manager.Current.Desktop.KeyBoard.KeyUp(System.Windows.Forms.Keys.Control);
            Manager.Current.Desktop.KeyBoard.KeyPress(System.Windows.Forms.Keys.Delete);

            Manager.Current.Desktop.KeyBoard.TypeText(text);
        }

        /// <summary>
        /// Fill label
        /// </summary>
        /// <param name="labelText">Label text</param>
        public void FillLabel(string labelText)
        {
            HtmlInputText headingInput = EM.Card.CardEditScreen.LabelTextField
                .AssertIsPresent("Heading field");

            headingInput.ScrollToVisible();
            headingInput.Focus();
            headingInput.MouseClick();

            Manager.Current.Desktop.KeyBoard.KeyDown(System.Windows.Forms.Keys.Control);
            Manager.Current.Desktop.KeyBoard.KeyPress(System.Windows.Forms.Keys.A);
            Manager.Current.Desktop.KeyBoard.KeyUp(System.Windows.Forms.Keys.Control);
            Manager.Current.Desktop.KeyBoard.KeyPress(System.Windows.Forms.Keys.Delete);

            Manager.Current.Desktop.KeyBoard.TypeText(labelText);
        }

        /// <summary>
        /// Click select image button
        /// </summary>
        public void ClickSelectImageButton()
        {
            HtmlButton selectImageButton =  EM.Card.CardEditScreen.SelectImage
            .AssertIsPresent("Select image button");
            selectImageButton.Click();
            ActiveBrowser.WaitForAsyncJQueryRequests();
            ActiveBrowser.WaitUntilReady();
            ActiveBrowser.RefreshDomTree();
        }

        /// <summary>
        /// Select external url option
        /// </summary>
        public void SelectExternalUrlOption()
        {
            HtmlInputRadioButton selectExternalUrl = EM.Card.CardEditScreen.ExternalURLRadioButton.AssertIsPresent("External url");
            selectExternalUrl.ScrollToVisible();
            selectExternalUrl.Focus();
            selectExternalUrl.MouseClick();
        }

        /// <summary>
        /// Fill external url
        /// </summary>
        /// <param name="externalUrl">External url</param>
        public void FillExternalUrl(string externalUrl)
        {
            HtmlInputText externalUrlInput = EM.Card.CardEditScreen.ExternalURLInput
                .AssertIsPresent("External url");

            externalUrlInput.ScrollToVisible();
            externalUrlInput.Focus();
            externalUrlInput.MouseClick();

            Manager.Current.Desktop.KeyBoard.KeyDown(System.Windows.Forms.Keys.Control);
            Manager.Current.Desktop.KeyBoard.KeyPress(System.Windows.Forms.Keys.A);
            Manager.Current.Desktop.KeyBoard.KeyUp(System.Windows.Forms.Keys.Control);
            Manager.Current.Desktop.KeyBoard.KeyPress(System.Windows.Forms.Keys.Delete);

            Manager.Current.Desktop.KeyBoard.TypeText(externalUrl);
        }

        /// <summary>
        /// Fill heading in advanced settings
        /// </summary>
        /// <param name="headingText">Heading text</param>
        public void FillHeadingTextInAdvancedSettings(string headingText)
        {
            HtmlInputText headingInput = EM.Card.CardEditScreen.HeadingTextFieldInAdvancedSettings
                .AssertIsPresent("Heading field");

            headingInput.ScrollToVisible();
            headingInput.Focus();
            headingInput.MouseClick();

            Manager.Current.Desktop.KeyBoard.KeyDown(System.Windows.Forms.Keys.Control);
            Manager.Current.Desktop.KeyBoard.KeyPress(System.Windows.Forms.Keys.A);
            Manager.Current.Desktop.KeyBoard.KeyUp(System.Windows.Forms.Keys.Control);
            Manager.Current.Desktop.KeyBoard.KeyPress(System.Windows.Forms.Keys.Delete);

            Manager.Current.Desktop.KeyBoard.TypeText(headingText);
        }

        /// <summary>
        /// Selects widget template from the drop-down in the widget designer
        /// </summary>
        /// <param name="templateTitle">widget template title</param>
        public void SelectCardWidetTemplate(string templateTitle)
        {
            var templateSelector = EM.Card.CardEditScreen.TemplateSelector
              .AssertIsPresent("Template selector drop-down");
            templateSelector.SelectByValue(templateTitle);
            templateSelector.AsjQueryControl().InvokejQueryEvent(jQueryControl.jQueryControlEvents.click);
            templateSelector.AsjQueryControl().InvokejQueryEvent(jQueryControl.jQueryControlEvents.change);
            ActiveBrowser.WaitForAsyncOperations();
        }

        /// <summary>
        /// click add button
        /// </summary>
        public void ClickAddButton()
        {
            HtmlButton addButton = EM.Card.CardEditScreen.AddButton.AssertIsPresent("Add button");
            addButton.Click();
            ActiveBrowser.WaitUntilReady();
        }
    }
}
