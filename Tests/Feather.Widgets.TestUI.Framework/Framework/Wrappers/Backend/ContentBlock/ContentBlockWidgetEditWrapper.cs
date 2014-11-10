using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            HtmlTableCell editable = EM.GenericContent.ContentBlockWidget.EditableArea
                .AssertIsPresent("Editable area");

            editable.ScrollToVisible();
            editable.Focus();
            editable.MouseClick();

            Manager.Current.Desktop.KeyBoard.TypeText(content);
        }

        /// <summary>
        /// Save content block widget
        /// </summary>
        public void SaveChanges()
        {
            HtmlButton saveButton = EM.GenericContent.ContentBlockWidget.SaveChangesButton
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
            HtmlAnchor createContent = EM.GenericContent.ContentBlockWidget.CreateContent
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
            HtmlAnchor selectProvider = EM.GenericContent.ContentBlockWidget.SelectProviderDropdown
            .AssertIsPresent("Provider dropdown");
            selectProvider.Click();

            var provider = ActiveBrowser.Find.ByExpression<HtmlAnchor>("class=ng-binding", "InnerText=" + providerName).AssertIsPresent("Provider"); 
            provider.Click();

            HtmlDiv sharedContentBlockList = EM.GenericContent.ContentBlockWidget.ContentBlockList
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
            HtmlButton shareButton = EM.GenericContent.ContentBlockWidget.DoneSelectingButton
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
            HtmlAnchor shareButton = EM.GenericContent.ContentBlockWidget.AdvancedButton
            .AssertIsPresent("Advance selecting button");
            shareButton.Click();
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
            List<HtmlInputText> input = ActiveBrowser.Find.AllByExpression<HtmlInputText>("tagname=input", "class=form-control ng-pristine ng-valid").ToList<HtmlInputText>();

            input[1].ScrollToVisible();
            input[1].Focus();
            input[1].MouseClick();

            Manager.Current.Desktop.KeyBoard.KeyDown(System.Windows.Forms.Keys.Control);
            Manager.Current.Desktop.KeyBoard.KeyPress(System.Windows.Forms.Keys.A);
            Manager.Current.Desktop.KeyBoard.KeyUp(System.Windows.Forms.Keys.Control);
            Manager.Current.Desktop.KeyBoard.KeyPress(System.Windows.Forms.Keys.Delete);

            ActiveBrowser.WaitForAsyncOperations();

            Manager.Current.Desktop.KeyBoard.TypeText(isEnabled);
        }
    }
}
