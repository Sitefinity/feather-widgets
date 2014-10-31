using System;
using System.Drawing;
using Feather.Widgets.TestUI.Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text;

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
        Owner("Feather team"),
        TestCategory(FeatherTestCategories.Navigation)]
        public void NavigationWidgetBootstrapTemplateVerifyDefaultTransformation()
        {
            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower(), false);
            BATFeather.Wrappers().Frontend().Navigation().NavigationWrapper().ResizeBrowserWindow(500);
            BATFeather.Wrappers().Frontend().Navigation().NavigationWrapper().OpenNavigationToggleMenu();           
            BATFeather.Wrappers().Frontend().Navigation().NavigationWrapper().ClickOnPageLinkFromNavigationMenu(Page1, TemplateType.Bootstrap, NavClass);
        }

        /// <summary>
        /// UI test NavigationWidgetBootstrapTemplateVerifyHiddenTransformation
        /// Ignored due to infrastructural changes
        /// Css classes related to transfomartions should be added directly to the css file
        /// </summary>
        [TestMethod,
        Owner("Feather team"),
        TestCategory(FeatherTestCategories.Navigation),
        Ignore]
        public void NavigationWidgetBootstrapTemplateVerifyHiddenTransformation()
        {
            CssClass = "nav-xs-hidden";
            CssClassSmall = "nav-sm-hidden";
            CssClassMedium = "nav-md-hidden";

            BAT.Macros().NavigateTo().Pages();
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(PageName);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(WidgetName);
            BATFeather.Wrappers().Backend().Navigation().NavigationWidgetEditWrapper().MoreOptions();
            BATFeather.Wrappers().Backend().Navigation().NavigationWidgetEditWrapper().FillCSSClass(CssClass);
            BATFeather.Wrappers().Backend().Navigation().NavigationWidgetEditWrapper().SaveChanges();
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();

            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower(), false);
            BATFeather.Wrappers().Frontend().Navigation().NavigationWrapper().ResizeBrowserWindow(500);
            Assert.IsFalse(BATFeather.Wrappers().Frontend().Navigation().NavigationWrapper().AssertToggleButtonIsVisible(), CssClass + ":Toggle button is visible");
            Assert.IsFalse(BATFeather.Wrappers().Frontend().Navigation().NavigationWrapper().AssertNavigationIsVisible(NavClass), CssClass + ":Navigation is visible");

            BAT.Arrange(this.ArrangementClass).AddParameter(key, CssClassSmall).ExecuteArrangement(this.ArrangementMethod);
            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower(), false);
            BATFeather.Wrappers().Frontend().Navigation().NavigationWrapper().ResizeBrowserWindow(800);
            Assert.IsFalse(BATFeather.Wrappers().Frontend().Navigation().NavigationWrapper().AssertNavigationIsVisible(NavClass), CssClassSmall + ":Navigation is visible");

            BAT.Arrange(this.ArrangementClass).AddParameter(key, CssClassMedium).ExecuteArrangement(this.ArrangementMethod);
            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower(), false);
            BATFeather.Wrappers().Frontend().Navigation().NavigationWrapper().ResizeBrowserWindow(1000);
            Assert.IsFalse(BATFeather.Wrappers().Frontend().Navigation().NavigationWrapper().AssertNavigationIsVisible(NavClass), CssClassMedium + ":Navigation is visible");
        }

        /// <summary>
        /// UI test NavigationWidgetBootstrapTemplateVerifyDropDownTransformation
        /// Ignored due to infrastructural changes
        /// Css classes related to transfomartions should be added directly to the css file
        /// </summary>
        [TestMethod,
        Owner("Feather team"),
        TestCategory(FeatherTestCategories.Navigation),
        Ignore]
        public void NavigationWidgetBootstrapTemplateVerifyDropDownTransformation()
        {
            CssClass = "nav-xs-dropdown";
            CssClassSmall = "nav-sm-dropdown";
            CssClassMedium = "nav-md-dropdown";

            BAT.Macros().NavigateTo().Pages();
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(PageName);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(WidgetName);
            BATFeather.Wrappers().Backend().Navigation().NavigationWidgetEditWrapper().MoreOptions();
            BATFeather.Wrappers().Backend().Navigation().NavigationWidgetEditWrapper().FillCSSClass(CssClass);
            BATFeather.Wrappers().Backend().Navigation().NavigationWidgetEditWrapper().SaveChanges();
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();

            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower(), false);
            BATFeather.Wrappers().Frontend().Navigation().NavigationWrapper().ResizeBrowserWindow(500);
            Assert.IsTrue(
                BATFeather.Wrappers().Frontend().Navigation().NavigationWrapper().
                AsserNavigationDropDownMenuIsVisible(), CssClass + ":drop down menu not visible");
            
            BAT.Arrange(this.ArrangementClass).AddParameter(key, CssClassSmall).ExecuteArrangement(this.ArrangementMethod);
            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower(), false);
            BATFeather.Wrappers().Frontend().Navigation().NavigationWrapper().ResizeBrowserWindow(800);
            Assert.IsTrue(
                BATFeather.Wrappers().Frontend().Navigation().NavigationWrapper().
                AsserNavigationDropDownMenuIsVisible(), CssClassSmall + ":drop down menu not visible");
            

            BAT.Arrange(this.ArrangementClass).AddParameter(key, CssClassMedium).ExecuteArrangement(this.ArrangementMethod);
            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower(), false);
            BATFeather.Wrappers().Frontend().Navigation().NavigationWrapper().ResizeBrowserWindow(1000);
            Assert.IsTrue(
                BATFeather.Wrappers().Frontend().Navigation().NavigationWrapper().
                AsserNavigationDropDownMenuIsVisible(), CssClassMedium + ":drop down menu not visible");                     
        }

        /// <summary>
        /// UI test NavigationWidgetBootstrapTemplateVerifyTransformationsWithSeveralCssClasses
        /// Ignored due to infrastructural changes
        /// Css classes related to transfomartions should be added directly to the css file
        /// </summary>
        [TestMethod,
        Owner("Feather team"),
        TestCategory(FeatherTestCategories.Navigation),
        Ignore]
        public void NavigationWidgetBootstrapTemplateVerifyTransformationsWithSeveralCssClasses()
        {
            StringBuilder allClasses = new StringBuilder()
                .Append("nav-xs-dropdown")
                .Append(" nav-md-hidden");

            BAT.Macros().NavigateTo().Pages();
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(PageName);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(WidgetName);
            BATFeather.Wrappers().Backend().Navigation().NavigationWidgetEditWrapper().MoreOptions();
            BATFeather.Wrappers().Backend().Navigation().NavigationWidgetEditWrapper().FillCSSClass(allClasses.ToString());
            BATFeather.Wrappers().Backend().Navigation().NavigationWidgetEditWrapper().SaveChanges();
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();

            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower(), false);
            BATFeather.Wrappers().Frontend().Navigation().NavigationWrapper().ResizeBrowserWindow(500);
            Assert.IsTrue(
                BATFeather.Wrappers().Frontend().Navigation().NavigationWrapper().
                AsserNavigationDropDownMenuIsVisible(), "drop down menu not visible");

            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower(), false);
            BATFeather.Wrappers().Frontend().Navigation().NavigationWrapper().ResizeBrowserWindow(1000);
            Assert.IsFalse(
                BATFeather.Wrappers().Frontend().Navigation().NavigationWrapper()
                .AssertNavigationIsVisible(NavClass), "navigation is visible but it shouldn't be");
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
        private const string key = "cssClass";
        private const string NavClass = "nav navbar-nav";

        private string CssClass;
        private string CssClassSmall;
        private string CssClassMedium;    
    }
}
