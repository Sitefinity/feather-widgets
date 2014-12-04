using System;
using System.Collections.Generic;
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
    /// This is the entry point class for navigation widget edit wrapper.
    /// </summary>
    public class NavigationWidgetEditWrapper : BaseWrapper
    {
        /// <summary>
        /// Selects widget display mode in the widget designer
        /// </summary>
        /// <param name="mode">Navigation display mode</param>
        public void SelectNavigationWidgetDisplayMode(string mode)
        {
            int position = 0;
            HtmlDiv optionsDiv = EM.Navigation.NavigationWidgetEditScreen.DislayModeList 
                .AssertIsPresent("Navigation div");

            List<HtmlDiv> navDivs = optionsDiv.Find.AllByExpression<HtmlDiv>("tagname=div", "class=radio").ToList<HtmlDiv>();

            if (mode.Contains("All pages under"))
            {
                position = 1;
            }
            else if (mode.Contains("All sibling pages"))
            {
                position = 2;
            }

            HtmlInputRadioButton optionButton = navDivs[position].Find.ByExpression<HtmlInputRadioButton>("tagname=input")
                .AssertIsPresent("Display option radio button");

            optionButton.Click();
        }

        /// <summary>
        /// Save navigation widget
        /// </summary>
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

        /// <summary>
        /// Selects levels to include from the drop-down in the widget designer
        /// </summary>
        /// <param name="levelsToInclude">Levels to include value</param>
        public void SelectLevelsToInclude(string levelsToInclude)
        {
            var templateSelector = EM.Navigation.NavigationWidgetEditScreen.LevelesToIncludeSelector
                .AssertIsPresent("Levels to include selector");
            templateSelector.SelectByValue(levelsToInclude);
            templateSelector.AsjQueryControl().InvokejQueryEvent(jQueryControl.jQueryControlEvents.click);
            templateSelector.AsjQueryControl().InvokejQueryEvent(jQueryControl.jQueryControlEvents.change);
            ActiveBrowser.WaitForAsyncOperations();
        }

        /// <summary>
        /// Selects more options in the widget designer
        /// </summary>
        public void MoreOptions()
        {
            HtmlSpan moreOptions = EM.Navigation.NavigationWidgetEditScreen.MoreOptions
                .AssertIsPresent("Css class");

            moreOptions.ScrollToVisible();
            moreOptions.Focus();
            moreOptions.MouseClick();
        }

        /// <summary>
        /// Fill css class in the widget designer
        /// </summary>
        /// <param name="cssClass">The css class value</param>
        public void FillCSSClass(string cssClass)
        {
            HtmlInputText input = EM.Navigation.NavigationWidgetEditScreen.CSSClass
                .AssertIsPresent("Css class");

            input.ScrollToVisible();
            input.Focus();
            input.MouseClick();

            Manager.Current.Desktop.KeyBoard.TypeText(cssClass);
        }

        /// <summary>
        /// Verify css class in the widget designer
        /// </summary>
        /// <param name="expectedCssClass">The expected css class</param>
        public void VerifyCSSClass(string expectedCssClass)
        {
            HtmlInputText input = EM.Navigation.NavigationWidgetEditScreen.CSSClass
                .AssertIsPresent("Css class");

            string actualText = input.Text;
            Assert.AreEqual(actualText, expectedCssClass, "CSS classes are not equal");
        }

        /// <summary>
        /// Remove css class in the widget designer
        /// </summary>
        public void RemoveCSSClass()
        {
            HtmlInputText input = EM.Navigation.NavigationWidgetEditScreen.CSSClass
                .AssertIsPresent("Css class");

            input.ScrollToVisible();
            input.Focus();

            Manager.Current.Desktop.KeyBoard.KeyDown(System.Windows.Forms.Keys.Control);
            Manager.Current.Desktop.KeyBoard.KeyPress(System.Windows.Forms.Keys.A);
            Manager.Current.Desktop.KeyBoard.KeyUp(System.Windows.Forms.Keys.Control);
            Manager.Current.Desktop.KeyBoard.KeyPress(System.Windows.Forms.Keys.Delete);

            ActiveBrowser.WaitForAsyncOperations();
        }
    }
}
