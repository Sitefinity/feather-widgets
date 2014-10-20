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
    /// This is the entry point class for content block share wrapper.
    /// </summary>
    public class ContentBlockWidgetShareWrapper : BaseWrapper
    {
        /// <summary>
        /// Fill content block title
        /// </summary>
        /// <param name="title">The title of the content block</param>
        public void FillContentBlockTitle(string title)
        {
            HtmlInputText input = EM.GenericContent.ContentBlockWidget.ShareContentTitle
                .AssertIsPresent("Title field");

            input.ScrollToVisible();
            input.Focus();
            input.MouseClick();

            Manager.Current.Desktop.KeyBoard.TypeText(title);
        }

        /// <summary>
        /// Provide access to share button
        /// </summary>
        public void ShareButton()
        {
            HtmlButton shareButton = EM.GenericContent.ContentBlockWidget.ShareButton
            .AssertIsPresent("Share button");
            shareButton.Click();
            ActiveBrowser.WaitUntilReady();
            ActiveBrowser.WaitForAsyncRequests();
        }

        public void VerifyMessageTitleIsRequired()
        {
            HtmlControl contentBlockPlaceholder = EM.GenericContent.ContentBlockWidget.TitleIsRequired
           .AssertIsPresent("Title is required");
        }

        /// <summary>
        /// Provide access to unshare button
        /// </summary>
        public void UnshareButton()
        {
            HtmlButton shareButton = EM.GenericContent.ContentBlockWidget.UnshareButton
            .AssertIsPresent("Unshare button");
            shareButton.Click();
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
        /// Provide access to cancel button
        /// </summary>
        public void CancelButton()
        {
            HtmlAnchor cancelButton = EM.GenericContent.ContentBlockWidget.CancelButton
            .AssertIsPresent("Cancel button");
            cancelButton.Click();
        }

        /// <summary>
        /// Select shared content block
        /// </summary>
        /// <param name="sharedContentTitle">The title of the shared content</param>
        public void SelectContentBlock(string sharedContentTitle)
        {
            ActiveBrowser.WaitUntilReady();
            ActiveBrowser.WaitForAsyncRequests();
            ActiveBrowser.RefreshDomTree();

            HtmlDiv sharedContentBlockList = EM.GenericContent.ContentBlockWidget.ContentBlockList
            .AssertIsPresent("Shared content list");

            var itemSpan = sharedContentBlockList.Find.ByExpression<HtmlSpan>("class=ng-binding", "InnerText=" + sharedContentTitle);

            itemSpan.Wait.ForVisible();
            itemSpan.ScrollToVisible();
            itemSpan.MouseClick();
            this.DoneSelectingButton();
        }
    }
}
