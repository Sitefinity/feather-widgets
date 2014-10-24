using System;
using System.Drawing;
using Feather.Widgets.TestUI.Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FeatherWidgets.TestUI.TestCases.Navigation
{
    /// <summary>
    /// This is a test class with tests related to navigation widget responsive design options
    /// </summary>
    [TestClass]
    public class NavigationWidgetResponsiveDesignTransformations : FeatherTestCase
    {
        /// <summary>
        /// UI test NavigationWidgetBootstrapTemplateVerifyDefaultTransformation
        /// </summary>
        [TestMethod,
        Owner("Feather team"),
        TestCategory(FeatherTestCategories.PagesAndContent)]
        public void NavigationWidgetBootstrapTemplateVerifyDefaultTransformation()
        {
            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower(), false);
            this.ResizeBrowserWindow(500);
            BATFeather.Wrappers().Frontend().Navigation().NavigationWrapper().OpenNavigationToggleMenu();           
            BATFeather.Wrappers().Frontend().Navigation().NavigationWrapper().ClickOnPageLinkFromNavigationMenu(Page1);
        }

        /// <summary>
        /// UI test NavigationWidgetBootstrapTemplateVerifyHiddenTransformation
        /// </summary>
        [TestMethod,
        Owner("Feather team"),
        TestCategory(FeatherTestCategories.PagesAndContent)]
        public void NavigationWidgetBootstrapTemplateVerifyHiddenTransformation()
        {
            CssClass = "nav-xs-hidden";
            CssClassSmall = "nav-sm-hidden";
            CssClassMedium = "nav-md-hidden";
            CssClassLarge = "nav-lg-hidden";

            BAT.Macros().NavigateTo().Pages();
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(PageName);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(WidgetName);
            BATFeather.Wrappers().Backend().Navigation().NavigationWidgetEditWrapper().MoreOptions();
            BATFeather.Wrappers().Backend().Navigation().NavigationWidgetEditWrapper().FillCSSClass(CssClass);
            BATFeather.Wrappers().Backend().Navigation().NavigationWidgetEditWrapper().SaveChanges();
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();

            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower(), false);
            this.ResizeBrowserWindow(500);
            Assert.IsFalse(BATFeather.Wrappers().Frontend().Navigation().NavigationWrapper().AssertToggleButtonIsVisible(), "Toggle button is visible");
            Assert.IsFalse(BATFeather.Wrappers().Frontend().Navigation().NavigationWrapper().AssertNavigationIsVisible(), "Navigation is visible");

            BAT.Arrange(this.ArrangementClass).AddParameter(key, CssClassSmall).ExecuteArrangement(this.ArrangementMethod);
            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower(), false);
            this.ResizeBrowserWindow(800);
            Assert.IsFalse(BATFeather.Wrappers().Frontend().Navigation().NavigationWrapper().AssertNavigationIsVisible(), "Navigation is visible");

            BAT.Arrange(this.ArrangementClass).AddParameter(key, CssClassMedium).ExecuteArrangement(this.ArrangementMethod);
            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower(), false);
            this.ResizeBrowserWindow(1000);
            Assert.IsFalse(BATFeather.Wrappers().Frontend().Navigation().NavigationWrapper().AssertNavigationIsVisible(), "Navigation is visible");

            BAT.Arrange(this.ArrangementClass).AddParameter(key, CssClassLarge).ExecuteArrangement(this.ArrangementMethod);
            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower(), false);
            this.ResizeBrowserWindow(1300);
            Assert.IsFalse(BATFeather.Wrappers().Frontend().Navigation().NavigationWrapper().AssertNavigationIsVisible(), "Navigation is visible");
        }

        /// <summary>
        /// UI test NavigationWidgetBootstrapTemplateVerifyDropDownTransformation
        /// </summary>
        [TestMethod,
        Owner("Feather team"),
        TestCategory(FeatherTestCategories.PagesAndContent)]
        public void NavigationWidgetBootstrapTemplateVerifyDropDownTransformation()
        {
            CssClass = "nav-xs-dropdown";
            CssClassSmall = "nav-sm-dropdown";
            CssClassMedium = "nav-md-dropdown";
            CssClassLarge = "nav-lg-dropdown";

            BAT.Macros().NavigateTo().Pages();
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(PageName);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(WidgetName);
            BATFeather.Wrappers().Backend().Navigation().NavigationWidgetEditWrapper().MoreOptions();
            BATFeather.Wrappers().Backend().Navigation().NavigationWidgetEditWrapper().FillCSSClass(CssClass);
            BATFeather.Wrappers().Backend().Navigation().NavigationWidgetEditWrapper().SaveChanges();
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();

            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower(), false);
            this.ResizeBrowserWindow(500);
            Assert.IsTrue(
                BATFeather.Wrappers().Frontend().Navigation().NavigationWrapper().
                AsserNavigationDropDownMenuIsVisible(), "drop down menu not visible");
            
            BAT.Arrange(this.ArrangementClass).AddParameter(key, CssClassSmall).ExecuteArrangement(this.ArrangementMethod);
            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower(), false);
            this.ResizeBrowserWindow(800);
            Assert.IsTrue(
                BATFeather.Wrappers().Frontend().Navigation().NavigationWrapper().
                AsserNavigationDropDownMenuIsVisible(), "drop down menu not visible");
            

            BAT.Arrange(this.ArrangementClass).AddParameter(key, CssClassMedium).ExecuteArrangement(this.ArrangementMethod);
            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower(), false);
            this.ResizeBrowserWindow(1000);
            Assert.IsTrue(
                BATFeather.Wrappers().Frontend().Navigation().NavigationWrapper().
                AsserNavigationDropDownMenuIsVisible(), "drop down menu not visible");
            

            BAT.Arrange(this.ArrangementClass).AddParameter(key, CssClassLarge).ExecuteArrangement(this.ArrangementMethod);
            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower(), false);
            this.ResizeBrowserWindow(1300);
            Assert.IsTrue(
                BATFeather.Wrappers().Frontend().Navigation().NavigationWrapper().
                AsserNavigationDropDownMenuIsVisible(), "drop down menu not visible");           
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
            this.RestoreBrowserWindow();
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

        private void ResizeBrowserWindow(int width)
        {
            Rectangle rect = new Rectangle(200, 200, width, 500);

            ActiveBrowser.ResizeContent(rect);
            ActiveBrowser.RefreshDomTree();
        }

        private void RestoreBrowserWindow()
        {
            ActiveBrowser.Window.Restore();
            ActiveBrowser.Window.Maximize();
            ActiveBrowser.RefreshDomTree();
        }

        private const string ArrangementClassName = "NavigationWidgetResponsiveDesignTransformations";
        private const string ArrangementMethodName = "ChangeCssClass";
        private const string PageName = "FeatherPage";
        private const string Page1 = "Page1";
        private const string WidgetName = "Navigation";
        private const string key = "cssClass";

        private string CssClass;
        private string CssClassSmall;
        private string CssClassMedium;
        private string CssClassLarge;      
    }
}
