using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArtOfTest.Common.UnitTesting;
using ArtOfTest.WebAii.Controls.HtmlControls;
using ArtOfTest.WebAii.Core;
using ArtOfTest.WebAii.jQuery;

namespace Feather.Widgets.TestUI.Framework.Framework.Wrappers.Frontend
{
    /// <summary>
    /// This is the entry point class for inline editing on the frontend.
    /// </summary>
    public class InlineEditingWrapper : BaseWrapper
    {
        private Manager Manager
        {
            get
            {
                return Manager.Current;
            }
        }

        public void EditField(string controlName)
        {
            HtmlDiv frontendPageMainDiv = BAT.Wrappers().Frontend().Pages().PagesWrapperFrontend().GetPageContent();
            HtmlControl inlineEditingControl = frontendPageMainDiv.Find.ByExpression<HtmlControl>("class=sfFieldEditable", "name=" + controlName);

            inlineEditingControl.Focus();
            inlineEditingControl.MouseClick();

            ActiveBrowser.WaitUntilReady();
            ActiveBrowser.WaitForAsyncJQueryRequests();
            ActiveBrowser.RefreshDomTree();
        }

        public void EditControlOfDynamicContentViaInlineEditing(string newTitle, string controlName)
        {
            this.EditField(controlName);

            Manager.Desktop.KeyBoard.KeyDown(System.Windows.Forms.Keys.LControlKey);
            Manager.Desktop.KeyBoard.KeyPress(System.Windows.Forms.Keys.A);
            Manager.Desktop.KeyBoard.KeyUp(System.Windows.Forms.Keys.LControlKey);
            Manager.Desktop.KeyBoard.TypeText(newTitle, 20);
        }

        public void EditDropDownControlOfDynamicContentViaInlineEditing(string newValue, string controlName)
        {
            this.EditField(controlName);

            HtmlAnchor editButton = ActiveBrowser.Find.ByCustom<HtmlAnchor>(e => e.IsVisible() && e.CssClass.Equals("sfShowInlineEditDlgLnk"));
            editButton.Click();
            HtmlDiv choicesArea = ActiveBrowser.Find.ByCustom<HtmlDiv>(e => e.IsVisible() && e.CssClass.Equals("sfWindowBody"));

            List<HtmlListItem> listItem = choicesArea.Find.AllByExpression<HtmlListItem>("tagname=li").ToList<HtmlListItem>();
            HtmlListItem toCheck = null;

            for (int i = 0; i < listItem.Count; i++)
            {
                var choice = listItem[i].InnerText.Contains(newValue);

                if (choice == true)
                {
                    toCheck = listItem[i];
                }
            }

            var choices = toCheck.Find.ByExpression<HtmlInputCheckBox>("tagname=input");

            choices.Click();
            BAT.Wrappers().Frontend().InlineEditing().DynamicTypes().SelectDoneButtongWhenEditingChoices();
        }

        public void EditRadioButtonControlOfDynamicContentViaInlineEditing(string newValue, string controlName)
        {
            this.EditField(controlName);

            HtmlAnchor editButton = ActiveBrowser.Find.ByCustom<HtmlAnchor>(e => e.IsVisible() && e.CssClass.Equals("sfShowInlineEditDlgLnk"));
            editButton.Click();
            HtmlDiv choicesArea = ActiveBrowser.Find.ByExpression<HtmlDiv>("tagname=div", "data-template=radioButtonsViewTemplate"); 

            List<HtmlDiv> listItem = choicesArea.Find.AllByExpression<HtmlDiv>("tagname=div").ToList<HtmlDiv>();
            HtmlDiv toCheck = null;

            for (int i = 0; i < listItem.Count; i++)
            {
                var choice = listItem[i].InnerText.Contains(newValue);

                if (choice == true)
                {
                    toCheck = listItem[i];
                }
            }

            var choices = toCheck.Find.ByExpression<HtmlInputRadioButton>("tagname=input");

            choices.Click();
            ActiveBrowser.WaitUntilReady();

            BAT.Wrappers().Frontend().InlineEditing().DynamicTypes().SelectDoneButtongWhenEditingChoices();
        }

        public void EditYesNoControlOfDynamicContentViaInlineEditing(string controlName)
        {
            this.EditField(controlName);

            HtmlAnchor editButton = ActiveBrowser.Find.ByCustom<HtmlAnchor>(e => e.IsVisible() && e.CssClass.Equals("sfShowInlineEditDlgLnk"));
            editButton.Click();

            HtmlDiv workArea = ActiveBrowser.Find.ByCustom<HtmlDiv>(e => e.IsVisible() && e.ID.Equals("contentPlaceHolder"));
            HtmlInputCheckBox checkbox = workArea.Find.ByCustom<HtmlInputCheckBox>(i => i.IsVisible() && i.TagName.Equals("input"));
            checkbox.Click();
            BAT.Wrappers().Frontend().InlineEditing().DynamicTypes().SelectDoneButtongWhenEditingChoices();
        }

        public void EditDateTimeControlOfDynamicContentViaInlineEditing(string controlName, string date)
        {
            this.EditField(controlName);

            HtmlAnchor editButton = ActiveBrowser.Find.ByCustom<HtmlAnchor>(e => e.IsVisible() && e.CssClass.Equals("sfShowInlineEditDlgLnk"));
            editButton.Click();

            HtmlInputText inputDate = ActiveBrowser.Find.ByExpression<HtmlInputText>("tagname=input", "class=k-input")
               .AssertIsPresent("Checkbox");

            inputDate.Click();

            Manager.Desktop.KeyBoard.KeyDown(System.Windows.Forms.Keys.LControlKey);
            Manager.Desktop.KeyBoard.KeyPress(System.Windows.Forms.Keys.A);
            Manager.Desktop.KeyBoard.KeyUp(System.Windows.Forms.Keys.LControlKey);
            Manager.Desktop.KeyBoard.TypeText(date, 20);

            BAT.Wrappers().Frontend().InlineEditing().DynamicTypes().SelectDoneButtongWhenEditingChoices();
        }

        public void EditTagControlOfDynamicContentViaInlineEditing(string controlName, string tagName)
        {
            this.EditField(controlName);

            HtmlAnchor editButton = ActiveBrowser.Find.ByCustom<HtmlAnchor>(e => e.IsVisible() && e.CssClass.Equals("sfShowInlineEditDlgLnk"));
            editButton.Click();

            HtmlDiv workArea = ActiveBrowser.Find
            .ByCustom<HtmlDiv>(w => w.IsVisible() && w.CssClass.Equals("k-widget k-multiselect k-header"))
            .AssertIsPresent("Edit tags dialog");

            HtmlInputText tagsInput = workArea.Find.ByCssClass<HtmlInputText>("k-input").AssertIsPresent("tags input");
            tagsInput.Click();
            tagsInput.SimulateTextTyping(tagName);
            tagsInput.MouseClick();

            HtmlListItem tag2 = ActiveBrowser.WaitForElementWithCssClass("k-item k-state-focused")
                .As<HtmlListItem>().AssertIsPresent("tag2");
            tag2.Click();
            BAT.Wrappers().Frontend().InlineEditing().DynamicTypes().SelectDoneButtongWhenEditingChoices();
        }

        public void EditCategoryControlOfDynamicContentViaInlineEditing(string controlName, string categoryName)
        {
            this.EditField(controlName);

            HtmlAnchor editButton = ActiveBrowser.Find.ByCustom<HtmlAnchor>(e => e.IsVisible() && e.CssClass.Equals("sfShowInlineEditDlgLnk"));
            editButton.Click();

            HtmlUnorderedList allCats = ActiveBrowser.WaitForElementWithCssClass("k-group k-treeview-lines")
                .As<HtmlUnorderedList>()
                .AssertIsPresent("all categories");

            HtmlListItem listItem = allCats.Find.ByExpression<HtmlListItem>("InnerText=" + categoryName);

            var cats = listItem.Find.ByExpression<HtmlInputCheckBox>("tagname=input");

            cats.Click();
            BAT.Wrappers().Frontend().InlineEditing().DynamicTypes().SelectDoneButtongWhenEditingChoices();
        }
    }
}
