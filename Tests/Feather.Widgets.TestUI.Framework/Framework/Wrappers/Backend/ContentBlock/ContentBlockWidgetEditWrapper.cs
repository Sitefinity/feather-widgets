using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArtOfTest.Common.UnitTesting;
using ArtOfTest.WebAii.Controls.HtmlControls;
using ArtOfTest.WebAii.Core;
using ArtOfTest.WebAii.jQuery;

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
        /// Selects the link text in editable area.
        /// </summary>
        /// <param name="text">The text.</param>
        public void SelectLinkInEditableArea(string linkText)
        {
            Browser frame = this.GetContentBlockFrame();

            HtmlAnchor content = frame.Find.ByExpression<HtmlAnchor>("tagName=a", "InnerText=" + linkText);

            Rectangle contentRect = content.GetRectangle();
            Point startPoint = new Point(contentRect.X, contentRect.Y);
            Point endPoint = new Point(contentRect.X + contentRect.Width, contentRect.Y + contentRect.Height);

            content.Focus();
            content.MouseClick();
            ActiveBrowser.Manager.Desktop.Mouse.DragDrop(startPoint, endPoint);
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
            ActiveBrowser.RefreshDomTree();

            HtmlFindExpression expression = new HtmlFindExpression("class=modal-title", "InnerText=ContentBlock");
            ActiveBrowser.WaitForElement(expression, TimeOut, false);
            Manager.Current.Wait.For(this.WaitForSaveButton, Manager.Current.Settings.ClientReadyTimeout);
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
                                                         .ByExpression<HtmlAnchor>("class=btn btn-default btn-xs m-top-xs designer-btn-PropertyGrid ng-scope", "InnerText=Advanced")
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
            var image = this.GetContentBlockImageDesignMode();
            Assert.IsNotNull(image, "Unable to find image.");
            Assert.IsTrue(image.Src.Contains(src), "src is not correct");
        }

        /// <summary>
        /// Verifies the content block image design mode.
        /// </summary>
        /// <param name="src">the src.</param>
        /// <param name="sfref">the sfref.</param>
        /// <param name="title">the title.</param>
        /// <param name="altText">the altText.</param>
        public void VerifyContentBlockImageDesignMode(string src, string sfref, string title, string altText)
        {
            var image = this.GetContentBlockImageDesignMode();
            Assert.IsNotNull(image, "Unable to find image.");
            Assert.IsTrue(image.Src.StartsWith(src), "src is not correct");

            this.VerifyImageAttribute(image, "sfref", sfref);
            this.VerifyImageAttribute(image, "title", title);
            this.VerifyImageAttribute(image, "alt", altText);
        }

        /// <summary>
        /// Verifies the content block document design mode.
        /// </summary>
        /// <param name="href">The href.</param>
        /// <param name="sfref">The sfref.</param>
        /// <param name="title">The title.</param>
        public void VerifyContentBlockDocumentDesignMode(string href, string sfref, string title)
        {
            Browser frame = this.GetContentBlockFrame();
            var doc = frame.Find.ByExpression<HtmlAnchor>("tagname=a", "title=" + title).AssertIsPresent(title);
            Assert.IsTrue(doc.HRef.StartsWith(href), "href is not correct");
            var attr = doc.Attributes.FirstOrDefault(a => a.Name == "sfref");
            Assert.IsNotNull(attr, "Unable to find attribute: sfref");
            Assert.AreEqual(sfref, attr.Value, "Attribute sfref value not as expected.");
        }

        /// <summary>
        /// Verifies the content block video design mode.
        /// </summary>
        /// <param name="src">The SRC.</param>
        /// <param name="sfref">The sfref.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        public void VerifyContentBlockVideoDesignMode(string src, string sfref, string width = "", string height = "")
        {
            var video = this.GetContentBlockVideoDesignMode();
            Assert.IsNotNull(video, "Unable to find image.");
            Assert.IsTrue(video.Src.StartsWith(src), "src is not correct");

            this.VerifyVideoAttribute(video, "sfref", sfref);
            if (!width.Equals(string.Empty) && !height.Equals(string.Empty))
            {
                this.VerifyVideoAttribute(video, "width", width);
                this.VerifyVideoAttribute(video, "height", height);
            }
        }

        /// <summary>
        /// Opens the image selector.
        /// </summary>
        public void OpenImageSelector()
        {
            HtmlAnchor createContent = EM.GenericContent
                                         .ContentBlockWidget
                                         .ImageSelector
                                         .AssertIsPresent("image selector");
            createContent.Click();
            ActiveBrowser.WaitUntilReady();
            ActiveBrowser.WaitForAsyncRequests();
        }

        /// <summary>
        /// Opens the document selector.
        /// </summary>
        public void OpenDocumentSelector()
        {
            HtmlAnchor createContent = EM.GenericContent
                                         .ContentBlockWidget
                                         .DocumentSelector
                                         .AssertIsPresent("document selector");
            createContent.Click();
            ActiveBrowser.WaitUntilReady();
            ActiveBrowser.WaitForAsyncRequests();
        }

        /// <summary>
        /// Opens the video selector.
        /// </summary>
        public void OpenVideoSelector()
        {
            HtmlAnchor createContent = EM.GenericContent
                                         .ContentBlockWidget
                                         .VideoSelector
                                         .AssertIsPresent("video selector");
            createContent.Click();
            ActiveBrowser.WaitUntilReady();
            ActiveBrowser.WaitForAsyncRequests();
        }

        private Browser GetContentBlockFrame()
        {
            FrameInfo frameInfo = new FrameInfo(string.Empty, string.Empty, "javascript:\"\"", 1);
            Browser frame = ActiveBrowser.Frames[frameInfo];
            return frame;
        }

        private HtmlImage GetContentBlockImageDesignMode()
        {
            Browser frame = this.GetContentBlockFrame();
            return frame.Find.AllByTagName("img").FirstOrDefault().As<HtmlImage>();
        }

        private HtmlVideo GetContentBlockVideoDesignMode()
        {
            Browser frame = this.GetContentBlockFrame();
            return frame.Find.AllByTagName("video").FirstOrDefault().As<HtmlVideo>();
        }

        private void VerifyImageAttribute(HtmlImage image, string attName, string attValue)
        {
            var attr = image.Attributes.FirstOrDefault(a => a.Name == attName);
            Assert.IsNotNull(attr, "Unable to find attribute: " + attName);
            Assert.AreEqual(attValue, attr.Value, "Attribute " + attName + " value not as expected.");
        }

        private void VerifyVideoAttribute(HtmlVideo video, string attName, string attValue)
        {
            var attr = video.Attributes.FirstOrDefault(a => a.Name == attName);
            Assert.IsNotNull(attr, "Unable to find attribute: " + attName);
            Assert.AreEqual(attValue, attr.Value, "Attribute " + attName + " value not as expected.");
        }

        private bool WaitForSaveButton()
        {
            Manager.Current.ActiveBrowser.RefreshDomTree();
            var saveButton = EM.Widgets
                                   .WidgetDesignerContentScreen.SaveChangesButton;

            bool result = saveButton != null && saveButton.IsVisible();

            return result;
        }

        private const int TimeOut = 60000;
    }
}