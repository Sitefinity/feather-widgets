using ArtOfTest.WebAii.Controls.HtmlControls;
using ArtOfTest.WebAii.jQuery;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArtOfTest.WebAii.Core;
using ArtOfTest.Common.UnitTesting;

namespace Feather.Widgets.TestUI.Framework.Framework.Wrappers.Backend
{
    public class NavigationWidgetEditWrapper : BaseWrapper
    {
        public void SelectNavigationWidgetDisplayMode(string mode)
        {
            HtmlUnorderedList optionsList = EM.Navigation.NavigationWidgetEditScreen.DislayModeList 
                .AssertIsPresent("Display options list");

            HtmlListItem option = optionsList.AllItems.Where(a => a.InnerText.Contains(mode)).FirstOrDefault()
                .AssertIsPresent("Display option" + mode);

            HtmlInputRadioButton optionButton = option.Find.ByExpression<HtmlInputRadioButton>("tagname=input")
                .AssertIsPresent("Display option radio button");

            optionButton.Click();
        }

        public void SaveChanges()
        {
            HtmlButton saveButton = EM.Navigation.NavigationWidgetEditScreen.SaveChangesButton
            .AssertIsPresent("Save button");
            saveButton.Click();
        }

        /// <summary>
        /// Selects widget template from the drop-down in the widget designer
        /// </summary>
        /// <param name="templateTitle">widget template title</param>
        public void SelectWidgetListTemplate(string templateTitle)
        {
            var templateSelector = EM.Navigation.NavigationWidgetEditScreen.TemplateSelector
              .AssertIsPresent("Template selector drop-down");
            templateSelector.SelectByValue(templateTitle);
            templateSelector.AsjQueryControl().InvokejQueryEvent(jQueryControl.jQueryControlEvents.click);
            templateSelector.AsjQueryControl().InvokejQueryEvent(jQueryControl.jQueryControlEvents.change);
            ActiveBrowser.WaitForAsyncOperations();
        }

        public void SelectLevelsToInclude(string levelsToInclude)
        {
            var templateSelector = EM.Navigation.NavigationWidgetEditScreen.LevelesToIncludeSelector
              .AssertIsPresent("Template selector drop-down");
            templateSelector.SelectByValue(levelsToInclude);
            templateSelector.AsjQueryControl().InvokejQueryEvent(jQueryControl.jQueryControlEvents.click);
            templateSelector.AsjQueryControl().InvokejQueryEvent(jQueryControl.jQueryControlEvents.change);
            ActiveBrowser.WaitForAsyncOperations();
        }

        public void MoreOptions()
        {
            HtmlSpan moreOptions = EM.Navigation.NavigationWidgetEditScreen.MoreOptions
                .AssertIsPresent("Css class");

            moreOptions.ScrollToVisible();
            moreOptions.Focus();
            moreOptions.MouseClick();
        }

        public void FillCSSClass(string cssClass)
        {
            HtmlInputText input = EM.Navigation.NavigationWidgetEditScreen.CSSClass
                .AssertIsPresent("Css class");

            input.ScrollToVisible();
            input.Focus();
            input.MouseClick();

            Manager.Current.Desktop.KeyBoard.TypeText(cssClass);
        }

        public void VerifyCSSClass(string expectedCssClass)
        {
            HtmlInputText input = EM.Navigation.NavigationWidgetEditScreen.CSSClass
                .AssertIsPresent("Css class");

            string actualText = input.Text;
            Assert.AreEqual(actualText, expectedCssClass, "CSS classes are not equal");
        }

        public void RemoveCSSClass()
        {
            HtmlInputText input = EM.Navigation.NavigationWidgetEditScreen.CSSClass
                .AssertIsPresent("Css class");

            input.Text = "";
            input.AsjQueryControl().InvokejQueryEvent(jQueryControl.jQueryControlEvents.change);
            ActiveBrowser.WaitForAsyncOperations();
        }
    }
}
