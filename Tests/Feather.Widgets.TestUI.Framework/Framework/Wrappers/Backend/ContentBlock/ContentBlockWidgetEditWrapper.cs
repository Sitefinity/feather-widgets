using ArtOfTest.WebAii.Controls.HtmlControls;
using ArtOfTest.WebAii.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feather.Widgets.TestUI.Framework.Framework.Wrappers.Backend
{
    public class ContentBlockWidgetEditWrapper : BaseWrapper
    {
        public void FillContentToContentBlockWidget(string content)
        {
            HtmlTableCell editable = EM.GenericContent.ContentBlockWidget.EditableArea
                .AssertIsPresent("Editable area");

            editable.ScrollToVisible();
            editable.Focus();
            editable.MouseClick();

            Manager.Current.Desktop.KeyBoard.TypeText(content);
        }

        public void SaveChanges()
        {
            HtmlButton saveButton = EM.GenericContent.ContentBlockWidget.SaveChangesButton
            .AssertIsPresent("Save button");
            saveButton.Click();
            ActiveBrowser.WaitUntilReady();
            ActiveBrowser.WaitForAsyncRequests();
        }

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
