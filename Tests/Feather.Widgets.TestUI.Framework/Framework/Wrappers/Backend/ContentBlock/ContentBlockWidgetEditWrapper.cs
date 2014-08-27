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
    }
}
