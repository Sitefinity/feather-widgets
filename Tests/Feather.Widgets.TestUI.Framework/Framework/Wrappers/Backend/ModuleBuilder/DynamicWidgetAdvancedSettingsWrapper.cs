using System;
using System.Collections.Generic;
using System.Linq;
using ArtOfTest.Common.UnitTesting;
using ArtOfTest.WebAii.Controls.HtmlControls;
using ArtOfTest.WebAii.Core;

namespace Feather.Widgets.TestUI.Framework.Framework.Wrappers.Backend.ModuleBuilder
{
    /// <summary>
    /// This class wraps the base operations related to edit list settings screen.
    /// </summary>
    public class DynamicWidgetAdvancedSettingsWrapper : BaseWrapper
    {
        /// <summary>
        /// Opens the advanced settings tab.
        /// </summary>
        public void ClickAdvancedSettingsButton()
        {
            HtmlDiv dynamicFooter = EM.ModuleBuilder.DynamicWidgetAdvancedSettings.DynamicWidgetFooter
                .AssertIsPresent("Footer");

            HtmlAnchor advanceButton = dynamicFooter.Find.ByExpression<HtmlAnchor>("class=btn btn-default btn-xs m-top-xs designer-btn-PropertyGrid ng-scope", "InnerText=Advanced")
            .AssertIsPresent("Advance selecting button");

            advanceButton.Click();
            ActiveBrowser.WaitUntilReady();
            ActiveBrowser.WaitForAsyncRequests();
            ActiveBrowser.RefreshDomTree();
        }
        
        /// <summary>
        /// Opens the model tab.
        /// </summary>
        public void ClickModelButton()
        {
            HtmlButton modelButton = EM.ModuleBuilder.DynamicWidgetAdvancedSettings.ModelButton
                                    .AssertIsPresent("Model button");

            modelButton.Click();
            ActiveBrowser.WaitUntilReady();
            ActiveBrowser.WaitForAsyncRequests();
        }

        /// <summary>
        /// Set items per page.
        /// </summary>
        public void SetItemsPerPage(string itemsPerPage)
        {
            HtmlInputControl input = EM.ModuleBuilder.DynamicWidgetAdvancedSettings.ItemsPerPage
                                    .AssertIsPresent("Items per page");

            input.ScrollToVisible();
            input.Focus();
            input.MouseClick();

            Manager.Current.Desktop.KeyBoard.KeyDown(System.Windows.Forms.Keys.Control);
            Manager.Current.Desktop.KeyBoard.KeyPress(System.Windows.Forms.Keys.A);
            Manager.Current.Desktop.KeyBoard.KeyUp(System.Windows.Forms.Keys.Control);
            Manager.Current.Desktop.KeyBoard.KeyPress(System.Windows.Forms.Keys.Delete);

            ActiveBrowser.WaitForAsyncOperations();

            Manager.Current.Desktop.KeyBoard.TypeText(itemsPerPage);
        }

        /// <summary>
        /// Set sort expression
        /// </summary>
        public void SetSortExpression(string sortExpression)
        {
            HtmlInputControl input = EM.ModuleBuilder.DynamicWidgetAdvancedSettings.SortExpression
                                    .AssertIsPresent("Items per page");

            input.ScrollToVisible();
            input.Focus();
            input.MouseClick();

            Manager.Current.Desktop.KeyBoard.KeyDown(System.Windows.Forms.Keys.Control);
            Manager.Current.Desktop.KeyBoard.KeyPress(System.Windows.Forms.Keys.A);
            Manager.Current.Desktop.KeyBoard.KeyUp(System.Windows.Forms.Keys.Control);
            Manager.Current.Desktop.KeyBoard.KeyPress(System.Windows.Forms.Keys.Delete);

            ActiveBrowser.WaitForAsyncOperations();

            Manager.Current.Desktop.KeyBoard.TypeText(sortExpression);
        }
    }
}
