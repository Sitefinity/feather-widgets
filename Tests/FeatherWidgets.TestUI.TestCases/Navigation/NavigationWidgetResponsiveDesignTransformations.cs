using Feather.Widgets.TestUI.Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeatherWidgets.TestUI.TestCases.Navigation
{
    /// <summary>
    /// This is a test class with tests related to navigation widget responsive design options
    /// </summary>
    [TestClass]
    public class NavigationWidgetResponsiveDesignTransformations : FeatherTestCase
    {
        [TestMethod,
        Owner("Feather team"),
        TestCategory(FeatherTestCategories.PagesAndContent)]
        public void NavigationWidgetBootstrapTemplateVerifyDefaultTransformation()
        {
            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower(), false);
            this.ResizeBrowserWindow();
            BATFeather.Wrappers().Frontend().Navigation().NavigationWrapper().OpenNavigationToggleMenu();           
            BATFeather.Wrappers().Frontend().Navigation().NavigationWrapper().ClickOnPageLinkFromNavigationMenu(Page1);
            this.RestoreBrowserWindow();
        }

        /// <summary>
        /// Performs Server Setup and prepare the system with needed data.
        /// </summary>
        protected override void ServerSetup()
        {
            BAT.Macros().User().EnsureAdminLoggedIn();
            BAT.Arrange("NavigationWidgetResponsiveDesignTransformations").ExecuteSetUp();
        }

        /// <summary>
        /// Performs clean up and clears all data created by the test.
        /// </summary>
        protected override void ServerCleanup()
        {
            BAT.Arrange("NavigationWidgetResponsiveDesignTransformations").ExecuteTearDown();
        }

        private void ResizeBrowserWindow()
        {
            Rectangle rect = new Rectangle(350, 350, 500, 500);

            ActiveBrowser.ResizeContent(rect);
            ActiveBrowser.RefreshDomTree();
        }

        private void RestoreBrowserWindow()
        {
            ActiveBrowser.Window.Restore();
            ActiveBrowser.Window.Maximize();
            ActiveBrowser.RefreshDomTree();
        }

        private const string PageName = "FeatherPage";
        private const string Page1 = "Page1";
    }
}
