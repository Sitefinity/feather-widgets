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
            HtmlDiv placeholder = ActiveBrowser.Find.ByExpression<HtmlDiv>("tagname=div", "id=viewsPlaceholder")
              .AssertIsPresent("Placeholder");

            HtmlTable table = placeholder.Find.ByExpression<HtmlTable>("tagname=table", "class=k-widget k-editor k-header k-editor-widget")
                .AssertIsPresent("Table");

            HtmlTableCell editable = table.Find.ByExpression<HtmlTableCell>("tagname=td", "class=k-editable-area")
                .AssertIsPresent("Cell");

            editable.ScrollToVisible();
            editable.Focus();
            editable.MouseClick();

            Manager.Current.Desktop.KeyBoard.TypeText(content);
        }

        public void SaveChanges()
        {
            HtmlDiv footer = ActiveBrowser.Find.ByExpression<HtmlDiv>("tagname=div", "class=modal-footer ng-scope")
            .AssertIsPresent("Footer");

            var saveButton = footer.Find.ByExpression<HtmlButton>("InnerText=Save")
            .AssertIsPresent("Save button");
            saveButton.Click();
        }
    }
}
