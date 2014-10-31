using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Feather.Widgets.TestUI.Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FeatherWidgets.TestUI.TestCases.Navigation
{
    /// <summary>
    /// This is a test class with tests related to navigation widget responsive design options on Foundation template
    /// </summary>
    [TestClass]
    public class NavigationWidgetFoundationResponsiveDesignTransformations: FeatherTestCase
    {
        /// <summary>
        /// UI test NavigationWidgetFoundationTemplateVerifyDefaultTransformation
        /// </summary>
        [TestMethod,
        Owner("Feather team"),
        TestCategory(FeatherTestCategories.Navigation)]
        public void NavigationWidgetFoundationTemplateVerifyDefaultTransformation()
        {
            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower(), false);
            BATFeather.Wrappers().Frontend().Navigation().NavigationWrapper().ResizeBrowserWindow(500);

            BATFeather.Wrappers().Frontend().Navigation().NavigationWrapper().OpenToggleMenuForFoundationTemplate();
            BATFeather.Wrappers().Frontend().Navigation().NavigationWrapper().ClickOnPageLinkFromNavigationMenu(Page1, TemplateType.Foundation, NavClass);
        }

        /// <summary>
        /// Performs Server Setup and prepare the system with needed data.
        /// </summary>
        protected override void ServerSetup()
        {
            BAT.Macros().User().EnsureAdminLoggedIn();
            BAT.Arrange(this.ArrangementClass).ExecuteSetUp();
        }

        /// <summary>
        /// Performs clean up and clears all data created by the test.
        /// </summary>
        protected override void ServerCleanup()
        {
            BATFeather.Wrappers().Frontend().Navigation().NavigationWrapper().RestoreBrowserWindow();
            BAT.Arrange(this.ArrangementClass).ExecuteTearDown();
        }

        private string ArrangementClass
        {
            get { return ArrangementClassName; }
        }

        private const string ArrangementClassName = "NavigationWidgetFoundationResponsiveDesignTransformations";
        private const string PageName = "FeatherPage";
        private const string Page1 = "Page1";
        private const string NavClass = "top-bar-section";
    }
}
