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
        private Manager Manager
        {
            get
            {
                return Manager.Current;
            }
        }

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

            webAddress.MouseClick();

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
        public void EnterTextToDisplay(string content)
        {
            HtmlInputText textToDisplay = EM.GenericContent
                                            .ContentBlockLinkSelector
                                            .TextToDisplay
                                            .AssertIsPresent("text to display");

            textToDisplay.MouseClick();

            Manager.Current.Desktop.KeyBoard.TypeText(content);
        }

        /// <summary>
        /// Verifies the corrct text to display.
        /// </summary>
        /// <param name="content">The content.</param>
        public void VerifyCorrectTextToDisplay(string content)
        {
            HtmlInputText textToDisplay = EM.GenericContent
                                            .ContentBlockLinkSelector
                                            .TextToDisplay
                                            .AssertIsPresent("text to display");

            Assert.AreEqual(content, textToDisplay.Text);
        }

        /// <summary>
        /// Opens the in new window.
        /// </summary>
        /// <param name="isSelected">The is selected.</param>
        public void OpenInNewWindow(bool isSelected = true)
        {
            HtmlInputCheckBox checkbox = EM.GenericContent
                                           .ContentBlockLinkSelector
                                           .OpenInNewWindow
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
        /// Clicks the test this link.
        /// </summary>
        public void ClickTestThisLink(string name, string href)
        {
            var link = ActiveBrowser.Find.ByExpression<HtmlAnchor>("href=" + href, "InnerText=" + name).AssertIsPresent(name + " was not present.");

            link.Click();
            Manager.Current.WaitForNewBrowserConnect(href, true, 10000);
        }

        /// <summary>
        /// Tests the this link visibility.
        /// </summary>
        /// <param name="isSupposeTestThisLinkToBeVisible">The is suppose test this link to be visible.</param>
        public void TestThisLinkVisibility(bool isSupposeTestThisLinkToBeVisible)
        {
            var label = EM.GenericContent
            .ContentBlockLinkSelector
            .TestThisLink;
            var link = ActiveBrowser.Find.ByExpression<HtmlAnchor>("href=~http://");
            if (isSupposeTestThisLinkToBeVisible)
            {
                Assert.IsNotNull(label, "label should appear!");
                Assert.IsNotNull(link, "link should appear!");
            }
            else
            {
                Assert.IsNull(label, "label should not appear");
                Assert.IsNull(link, "link should not appear");
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
        public void VerifyMessageAppearsForEmail(bool isSupposeMessageToAppear)
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
        public void InsertLink(bool isActive = true)
        {
            HtmlButton insertLinkButton = EM.GenericContent
                                                .ContentBlockLinkSelector
                                                .InsertLink;
            HtmlButton insertLinkButtonDisabled = EM.GenericContent
                                                .ContentBlockLinkSelector
                                                .InsertLinkDisabledButton;

            if (isActive)
            {

                insertLinkButton.AssertIsPresent("Insert link");
                insertLinkButtonDisabled.AssertIsNull("Insert link disabled");
                insertLinkButton.Click();
                ActiveBrowser.WaitUntilReady();
                ActiveBrowser.WaitForAsyncRequests();
            }
            else
            {
                insertLinkButtonDisabled.AssertIsPresent("Insert link disabled");
            }
        }
    }
}