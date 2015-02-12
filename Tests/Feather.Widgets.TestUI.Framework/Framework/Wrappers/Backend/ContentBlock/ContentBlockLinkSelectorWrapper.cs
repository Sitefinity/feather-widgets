using System.Linq;
using ArtOfTest.Common.UnitTesting;
using ArtOfTest.WebAii.Controls.HtmlControls;
using ArtOfTest.WebAii.Core;
using ArtOfTest.WebAii.jQuery;

namespace Feather.Widgets.TestUI.Framework.Framework.Wrappers.Backend
{
    /// <summary>
    /// This is the entry point class for content block link selector.
    /// </summary>
    public class ContentBlockLinkSelectorWrapper : BaseWrapper
    {
        /// <summary>
        /// Enters the web address.
        /// </summary>
        /// <param name="content">The content.</param>
        public void EnterWebAddress(string content)
        {
            HtmlInputText webAddress = EM.GenericContent
                                         .ContentBlockLinkSelector
                                         .WebAddress
                                         .AssertIsPresent("web address");

            webAddress.ScrollToVisible();
            webAddress.Focus();
            webAddress.MouseClick();

            webAddress.Text = string.Empty;
            Manager.Current.Desktop.KeyBoard.TypeText(content);
        }

        /// <summary>
        /// Verifies the corrct web address.
        /// </summary>
        /// <param name="content">The content.</param>
        public void VerifyCorrectWebAddress(string content)
        {
            HtmlInputText webAddress = EM.GenericContent
                                         .ContentBlockLinkSelector
                                         .WebAddress
                                         .AssertIsPresent("web address");

            Assert.AreEqual(content, webAddress.Text);
        }

        /// <summary>
        /// Enters the text to display.
        /// </summary>
        /// <param name="content">The content.</param>
        public void EnterTextToDisplay(string content, int tabIndex)
        {
            HtmlInputText textToDisplay = EM.GenericContent
                                            .ContentBlockLinkSelector
                                            .TextToDisplay(tabIndex)
                                            .AssertIsPresent("text to display");
            textToDisplay.ScrollToVisible();
            textToDisplay.Focus();
            textToDisplay.MouseClick();

            textToDisplay.Text = string.Empty;
            Manager.Current.Desktop.KeyBoard.TypeText(content);
        }

        /// <summary>
        /// Verifies the correct text to display.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <param name="tabIndex">The index of the current tab.</param>
        public void VerifyCorrectTextToDisplay(string content, int tabIndex)
        {
            HtmlInputText textToDisplay = EM.GenericContent
                                            .ContentBlockLinkSelector
                                            .TextToDisplay(tabIndex)
                                            .AssertIsPresent("text to display");

            Assert.AreEqual(content, textToDisplay.Text);
        }

        /// <summary>
        /// Verifies the text to display is not visible.
        /// </summary>
        public void VerifyTextToDisplayIsNotVisible(int tabIndex)
        {
            EM.GenericContent
              .ContentBlockLinkSelector
              .TextToDisplay(tabIndex)
              .AssertIsNull("text to display");
        }

        /// <summary>
        /// Clicks open in new window option
        /// </summary>
        /// <param name="tabIndex">The index of the current tab.</param>
        /// <param name="isSelected">true or false depending on the option state.</param>
        public void SelectOpenInNewWindowOption(int tabIndex, bool isSelected = true)
        {
            HtmlInputCheckBox checkbox = EM.GenericContent
                                           .ContentBlockLinkSelector
                                           .OpenInNewWindow(tabIndex)
                                           .AssertIsPresent("open in new window");

            if (isSelected)
            {
                if (!checkbox.Checked)
                {
                    checkbox.Click();
                }
            }
            else
            {
                if (checkbox.Checked)
                {
                    checkbox.Click();
                }
            }
        }
      
        /// <summary>
        /// Verifies the test this link attributes.
        /// </summary>
        /// <param textToDisplay="textToDisplay">The textToDisplay.</param>
        /// <param textToDisplay="href">The href.</param>
        public void VerifyTestThisLinkAttributes(string textToDisplay, string href)
        {
            ActiveBrowser.Find.ByExpression<HtmlAnchor>("href=" + href, "target=_blank", "InnerText=" + textToDisplay)
                .AssertIsPresent(textToDisplay + " or " + href + " was not present.");
        }

        /// <summary>
        /// Tests the this link visibility.
        /// </summary>
        /// <param name="isExpectedTestThisLinkToBeVisible">The is suppose test this link to be visible.</param>
        public void VerifyTestThisLinkVisibility(bool isExpectedTestThisLinkToBeVisible)
        {
            var label = EM.GenericContent
            .ContentBlockLinkSelector
            .TestThisLink;
            if (isExpectedTestThisLinkToBeVisible)
            {
                bool isVisible = label.CssClass.Equals("form-group");
                Assert.IsTrue(isVisible, "label should appear!");
            }
            else
            {
                bool isVisible = label.CssClass.Equals("form-group ng-hide");
                Assert.IsTrue(isVisible, "label should not appear!");
            }
        }

        /// <summary>
        /// Selects the anchor.
        /// </summary>
        /// <param name="anchorName">Name of the anchor.</param>
        public void SelectAnchor(string anchorName)
        {
            var anchorSelector = EM.GenericContent
                                   .ContentBlockLinkSelector
                                   .AnchorSelector
                                   .AssertIsPresent("select anchor dropdown");
            anchorSelector.SelectByValue(anchorName);
            anchorSelector.AsjQueryControl().InvokejQueryEvent(jQueryControl.jQueryControlEvents.click);
            anchorSelector.AsjQueryControl().InvokejQueryEvent(jQueryControl.jQueryControlEvents.change);
            ActiveBrowser.WaitForAsyncOperations();
        }

        /// <summary>
        /// Enters the email.
        /// </summary>
        /// <param name="content">The content.</param>
        public void EnterEmail(string content)
        {
            HtmlInputText email = EM.GenericContent
                                    .ContentBlockLinkSelector
                                    .Email
                                    .AssertIsPresent("email address");

            email.MouseClick();

            Manager.Current.Desktop.KeyBoard.TypeText(content);
        }

        /// <summary>
        /// Verifies the correct email.
        /// </summary>
        /// <param name="content">The content.</param>
        public void VerifyInvalidEmailMessage(bool isSupposeMessageToAppear)
        {
            var label = EM.GenericContent
            .ContentBlockLinkSelector
            .InvalidEmailMessage;
            if (isSupposeMessageToAppear)
            {
                Assert.IsNotNull(label, "label should appear!");
            }
            else
            {
                Assert.IsNull(label, "label should not appear");
            }
        }

        /// <summary>
        /// Inserts the link.
        /// </summary>
        public void InsertLink()
        {
            HtmlButton insertLinkButton = EM.GenericContent
                                            .ContentBlockLinkSelector
                                            .InsertLinkButton
                                            .AssertIsPresent("Insert link");

            Assert.IsTrue(insertLinkButton.IsEnabled, "Button is disabled!");
            insertLinkButton.Click();
            ActiveBrowser.WaitUntilReady();
            ActiveBrowser.WaitForAsyncRequests();
        }

        /// <summary>
        /// Cancels the editing link selector.
        /// </summary>
        public void CancelEditingLinkSelector()
        {
            HtmlButton cancelButton = EM.GenericContent
                                            .ContentBlockLinkSelector
                                            .CancelButton
                                            .AssertIsPresent("Cancel");

            cancelButton.Click();
            ActiveBrowser.WaitUntilReady();
            ActiveBrowser.WaitForAsyncRequests();
        }

        /// <summary>
        /// Determines whether insert link is enabled.
        /// </summary>
        /// <returns></returns>
        public bool IsInsertLinkEnabled()
        {
            HtmlButton insertLinkButton = EM.GenericContent
                                            .ContentBlockLinkSelector
                                            .InsertLinkButton
                                            .AssertIsPresent("Insert link");

            return insertLinkButton.IsEnabled;
        }

        /// <summary>
        /// Selects another tab from the link selector options.
        /// </summary>
        /// <param name="tabName">The name of the tab.</param>
        public void SwitchToSelectedTab(string tabName)
        {
            HtmlDiv tabs = EM.GenericContent.ContentBlockLinkSelector.TabsNavigation
                .AssertIsPresent("Navigation tabs");

            HtmlControl tabLabel = tabs.ChildNodes.Where(n => n.InnerText.Contains(tabName)).FirstOrDefault().As<HtmlControl>();
            Assert.IsNotNull(tabLabel, "The tab label " + tabName + " was not found");

            HtmlInputRadioButton option = tabLabel.Find.ByExpression<HtmlInputRadioButton>("tagName=input", "type=radio");
            option.AssertIsPresent(tabName);

            option.Click();
        }
    }
}