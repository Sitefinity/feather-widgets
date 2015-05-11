using System;
using System.Drawing;
using System.Text;
using Feather.Widgets.TestUI.Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telerik.Sitefinity.Frontend.TestUtilities;

namespace FeatherWidgets.TestUI.TestCases.Navigation
{
    /// <summary>
    /// This is a test class with tests related to navigation widget responsive design options on Bootstrap template
    /// </summary>
    [TestClass]
    public class NavigationWidgetBootstrapResponsiveDesignTransformations : FeatherTestCase
    {
        /// <summary>
        /// UI test NavigationWidgetBootstrapTemplateVerifyDefaultTransformation
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.Team2),
        TestCategory(FeatherTestCategories.Navigation),
        TestCategory(FeatherTestCategories.Bootstrap)]
        public void NavigationWidgetBootstrapTemplateVerifyDefaultTransformation()
        {
            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower(), false);
            BATFeather.Wrappers().Frontend().Navigation().NavigationWrapper().ResizeBrowserWindow(500);
            BATFeather.Wrappers().Frontend().Navigation().NavigationWrapper().OpenNavigationToggleMenu();           
            BATFeather.Wrappers().Frontend().Navigation().NavigationWrapper().ClickOnPageLinkFromNavigationMenu(Page1, TemplateType.Bootstrap, NavClass);
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

        private string ArrangementMethod
        {
            get { return ArrangementMethodName; }
        }

        private const string ArrangementClassName = "NavigationWidgetBootstrapResponsiveDesignTransformations";
        private const string ArrangementMethodName = "ChangeCssClass";
        private const string PageName = "FeatherPage";
        private const string Page1 = "Page1";
        private const string WidgetName = "Navigation";
        private const string Key = "cssClass";
        private const string NavClass = "nav navbar-nav";

        private string cssClass;
        private string cssClassSmall;
        private string cssClassMedium;    
    }
}
