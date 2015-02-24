using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArtOfTest.Common.UnitTesting;
using ArtOfTest.WebAii.Controls.HtmlControls;
using ArtOfTest.WebAii.Core;

namespace Feather.Widgets.TestUI.Framework.Framework.Wrappers.Backend
{
    /// <summary>
    /// This is the entry point class for content block edit wrapper.
    /// </summary>
    public class ContentBlockWidgetEditWrapper : BaseWrapper
    {
        /// <summary>
        /// Fill content to the content block widget
        /// </summary>
        /// <param name="content">The content value</param>
        public void FillContentToContentBlockWidget(string content)
        {
            HtmlTableCell editable = EM.GenericContent
                                       .ContentBlockWidget
                                       .EditableArea
                                       .AssertIsPresent("Editable area");

            editable.ScrollToVisible();
            editable.Focus();
            editable.MouseClick();

            Manager.Current.Desktop.KeyBoard.TypeText(content);
        }

        /// <summary>
        /// Selects the content in editable area.
        /// </summary>
        public void SelectAllContentInEditableArea()
        {
            HtmlTableCell editable = EM.GenericContent
                                       .ContentBlockWidget
                                       .EditableArea
                                       .AssertIsPresent("Editable area");
            editable.ScrollToVisible();
            editable.Focus();
            editable.MouseClick();

            Manager.Current.Desktop.KeyBoard.KeyDown(System.Windows.Forms.Keys.Control);
            Manager.Current.Desktop.KeyBoard.KeyPress(System.Windows.Forms.Keys.A);
            Manager.Current.Desktop.KeyBoard.KeyUp(System.Windows.Forms.Keys.Control);
        }

        /// <summary>
        /// Deletes all content in editable area.
        /// </summary>
        public void DeleteAllContentInEditableArea()
        {
            this.SelectAllContentInEditableArea();
            Manager.Current.Desktop.KeyBoard.KeyPress(System.Windows.Forms.Keys.Delete);
        }

        /// <summary>
        /// Selects the text in editable area.
        /// </summary>
        /// <param name="text">The text.</param>
        public void SelectTextInEditableArea(string text)
        {
            Browser frame = this.GetContentBlockFrame();

            var content = frame.Find.ByExpression<HtmlControl>("InnerText=" + text);

            content.MouseClick(MouseClickType.LeftDoubleClick);
        }

        /// <summary>
        /// Verifies the content in HTML editable area.
        /// </summary>
        /// <param name="content">The content.</param>
        public void VerifyContentInHtmlEditableArea(string content)
        {
            HtmlTextArea editable = EM.GenericContent
                                      .ContentBlockWidget
                                      .EditableHtmlArea
                                      .AssertIsPresent("Html editable area");
            Assert.AreEqual(content, editable.TextContent);
        }

        /// <summary>
        /// Verifies the full screen mode.
        /// </summary>
        /// <param name="isActivatedFullScreen">Is activated full screen.</param>
        public void VerifyFullScreenMode(bool isActivatedFullScreen)
        {
            if (isActivatedFullScreen)
            {
                EM.GenericContent
                  .ContentBlockWidget
                  .ModalDialogFullScreenDiv
                  .AssertIsPresent("Full screen");
            }
            else
            {
                EM.GenericContent
                 .ContentBlockWidget
                 .ModalDialogNotFullScreenDiv
                 .AssertIsPresent("Not full screen");
            }
        }

        /// <summary>
        /// Save content block widget
        /// </summary>
        public void SaveChanges()
        {
            HtmlButton saveButton = EM.GenericContent
                                      .ContentBlockWidget
                                      .SaveChangesButton
                                      .AssertIsPresent("Save button");
            saveButton.Click();
            ActiveBrowser.WaitUntilReady();
            ActiveBrowser.WaitForAsyncRequests();
        }

        /// <summary>
        /// Provide access to "Create content" link
        /// </summary>
        public void CreateContentLink()
        {
            HtmlAnchor createContent = EM.GenericContent
                                         .ContentBlockWidget
                                         .CreateContent
                                         .AssertIsPresent("Create content");
            createContent.Click();
            ActiveBrowser.WaitUntilReady();
            ActiveBrowser.WaitForAsyncRequests();
        }

        /// <summary>
        /// Selects the content block in provider.
        /// </summary>
        /// <param name="providerName">Name of the provider.</param>
        /// <param name="contentBlockName">Name of the content block.</param>
        public void SelectContentBlockInProvider(string providerName, string contentBlockName)
        {
            HtmlAnchor selectProvider = EM.GenericContent
                                          .ContentBlockWidget
                                          .SelectProviderDropdown
                                          .AssertIsPresent("Provider dropdown");
            selectProvider.Click();

            var provider = ActiveBrowser.Find.ByExpression<HtmlAnchor>("class=ng-binding", "InnerText=" + providerName).AssertIsPresent("Provider"); 
            provider.Click();

            HtmlDiv sharedContentBlockList = EM.GenericContent
                                               .ContentBlockWidget
                                               .ContentBlockList
                                               .AssertIsPresent("Shared content list");

            var itemSpan = sharedContentBlockList.Find.ByExpression<HtmlSpan>("class=ng-binding", "InnerText=" + contentBlockName).AssertIsPresent("Content Block");
            itemSpan.Click();
            this.DoneSelectingButton();
        }

        /// <summary>
        /// Provide access to done button
        /// </summary>
        public void DoneSelectingButton()
        {
            HtmlButton shareButton = EM.GenericContent
                                       .ContentBlockWidget
                                       .DoneSelectingButton
                                       .AssertIsPresent("Done selecting button");
            shareButton.Click();
            ActiveBrowser.WaitUntilReady();
            ActiveBrowser.WaitForAsyncRequests();
        }

        /// <summary>
        /// Provide access to advance button
        /// </summary>
        public void AdvanceButtonSelecting()
        {
            HtmlDiv contentBlockFooter = EM.GenericContent
                                           .ContentBlockWidget
                                           .ContentBlockWidgetFooter
                                           .AssertIsPresent("Footer");

            HtmlAnchor advanceButton = contentBlockFooter.Find
                                                         .ByExpression<HtmlAnchor>("class=btn btn-default btn-xs m-top-xs ng-scope", "InnerText=Advanced")
                                                         .AssertIsPresent("Advance selecting button");

            advanceButton.Click();
            ActiveBrowser.WaitUntilReady();
            ActiveBrowser.WaitForAsyncRequests();
            ActiveBrowser.RefreshDomTree();
        }

        /// <summary>
        /// Enable social share buttons
        /// </summary>
        /// <param name="isEnabled">Is social share buttons enabled</param>
        public void EnableSocialShareButtons(string isEnabled)
        {
            HtmlInputText input = EM.GenericContent
                                    .ContentBlockWidget
                                    .EnableSocialSharing
                                    .AssertIsPresent("Social share field");

            input.Wait.ForExists();
            input.ScrollToVisible();
            input.Focus();
            input.MouseClick();

            Manager.Current.Desktop.KeyBoard.KeyDown(System.Windows.Forms.Keys.Control);
            Manager.Current.Desktop.KeyBoard.KeyPress(System.Windows.Forms.Keys.A);
            Manager.Current.Desktop.KeyBoard.KeyUp(System.Windows.Forms.Keys.Control);
            Manager.Current.Desktop.KeyBoard.KeyPress(System.Windows.Forms.Keys.Delete);

            ActiveBrowser.WaitForAsyncOperations();

            Manager.Current.Desktop.KeyBoard.TypeText(isEnabled);
        }

        /// <summary>
        /// Opens the link selector.
        /// </summary>
        public void OpenLinkSelector()
        {
            HtmlAnchor createContent = EM.GenericContent
                                         .ContentBlockWidget
                                         .LinkSelector
                                         .AssertIsPresent("link selector");
            createContent.Click();
            ActiveBrowser.WaitUntilReady();
            ActiveBrowser.WaitForAsyncRequests();
        }

        /// <summary>
        /// Switches to HTML view.
        /// </summary>
        public void SwitchToHtmlView()
        {
            HtmlButton htmlButton = EM.GenericContent
                                      .ContentBlockWidget
                                      .HtmlButton
                                      .AssertIsPresent("html view");
            htmlButton.Click();
            ActiveBrowser.WaitUntilReady();
            ActiveBrowser.WaitForAsyncRequests();
        }

        /// <summary>
        /// Switches to design view.
        /// </summary>
        public void SwitchToDesignView()
        {
            HtmlButton designButton = EM.GenericContent
                                        .ContentBlockWidget
                                        .DesignButton
                                        .AssertIsPresent("design view");
            designButton.Click();
            ActiveBrowser.WaitUntilReady();
            ActiveBrowser.WaitForAsyncRequests();
        }

        /// <summary>
        /// Presses the full screen button.
        /// </summary>
        public void PressFullScreenButton()
        {
            HtmlAnchor createContent = EM.GenericContent
                                         .ContentBlockWidget
                                         .FullScreen
                                         .AssertIsPresent("full screen");
            createContent.Click();
            ActiveBrowser.WaitUntilReady();
            ActiveBrowser.WaitForAsyncRequests();
        }

        /// <summary>
        /// Presses the specific button.
        /// </summary>
        /// <param name="title">The title.</param>
        public void PressSpecificButton(string title)
        {
            HtmlAnchor createContent = ActiveBrowser.Find
                                                    .ByExpression<HtmlAnchor>("title=" + title)
                                                    .AssertIsPresent(title);
            createContent.Click();
            ActiveBrowser.WaitUntilReady();
            ActiveBrowser.WaitForAsyncRequests();
        }

        /// <summary>
        /// Verifies the count of all group buttons is correct.
        /// </summary>
        /// <param name="expectedCount">The expected count.</param>
        public void VerifyCountOfButtonsIsCorrect(int expectedCount)
        {
            var count = 0;

            //// For now there are two unordered lists, for design and html view, only one of them is visible
            var containers = EM.GenericContent
                                        .ContentBlockWidget
                                        .ButtonsContainers;
            foreach (var container in containers)
            {
                if (container.IsVisible())
                {
                    var groupButtons = container.Find.AllByTagName("li");
                    foreach (var element in groupButtons)
                    {
                        count = count + element.ChildNodes.Count();
                    }

                    Assert.AreEqual(expectedCount, count);
                    break;
                }
            }
        }

        /// <summary>
        /// Verifies the content block text in design mode.
        /// </summary>
        /// <param name="content">The content.</param>
        public void VerifyContentBlockTextDesignMode(string content)
        {
            Browser frame = this.GetContentBlockFrame();

            var contentArea = frame.Find.AllByTagName("body").FirstOrDefault();
            Assert.AreEqual(content, contentArea.InnerText, "contents are not equal");
        }

        /// <summary>
        /// Verifies the content block image design mode.
        /// </summary>
        /// <param name="src">The SRC.</param>
        public void VerifyContentBlockImageDesignMode(string src)
        {
            Browser frame = this.GetContentBlockFrame();

            var image = frame.Find.AllByTagName("img").FirstOrDefault().As<HtmlImage>();
            Assert.AreEqual(src, image.Src, "src are not equal");
        }

        private Browser GetContentBlockFrame()
        {
            FrameInfo frameInfo = new FrameInfo(string.Empty, string.Empty, "javascript:\"\"", 1);
            Browser frame = ActiveBrowser.Frames[frameInfo];
            return frame;
        }
    }
}