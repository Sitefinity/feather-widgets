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
        /// UI test NavigationWidgetBootstrapTemplateVerifyHiddenTransformation
        /// Ignored due to infrastructural changes
        /// Css classes related to transfomartions should be added directly to the css file
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.Team2),
        TestCategory(FeatherTestCategories.Navigation),
        Ignore]
        public void NavigationWidgetBootstrapTemplateVerifyHiddenTransformation()
        {
            this.cssClass = "nav-xs-hidden";
            this.cssClassSmall = "nav-sm-hidden";
            this.cssClassMedium = "nav-md-hidden";

            BAT.Macros().NavigateTo().Pages();
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(PageName);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(WidgetName);
            BATFeather.Wrappers().Backend().Navigation().NavigationWidgetEditWrapper().MoreOptions();
            BATFeather.Wrappers().Backend().Navigation().NavigationWidgetEditWrapper().FillCSSClass(this.cssClass);
            BATFeather.Wrappers().Backend().Navigation().NavigationWidgetEditWrapper().SaveChanges();
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();

            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower(), false);
            BATFeather.Wrappers().Frontend().Navigation().NavigationWrapper().ResizeBrowserWindow(500);
            Assert.IsFalse(BATFeather.Wrappers().Frontend().Navigation().NavigationWrapper().AssertToggleButtonIsVisible(), this.cssClass + ":Toggle button is visible");
            Assert.IsFalse(BATFeather.Wrappers().Frontend().Navigation().NavigationWrapper().AssertNavigationIsVisible(NavClass, TemplateType.Bootstrap), this.cssClass + ":Navigation is visible");

            BAT.Arrange(this.ArrangementClass).AddParameter(Key, this.cssClassSmall).ExecuteArrangement(this.ArrangementMethod);
            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower(), false);
            BATFeather.Wrappers().Frontend().Navigation().NavigationWrapper().ResizeBrowserWindow(800);
            Assert.IsFalse(BATFeather.Wrappers().Frontend().Navigation().NavigationWrapper().AssertNavigationIsVisible(NavClass, TemplateType.Bootstrap), this.cssClassSmall + ":Navigation is visible");

            BAT.Arrange(this.ArrangementClass).AddParameter(Key, this.cssClassMedium).ExecuteArrangement(this.ArrangementMethod);
            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower(), false);
            BATFeather.Wrappers().Frontend().Navigation().NavigationWrapper().ResizeBrowserWindow(1000);
            Assert.IsFalse(BATFeather.Wrappers().Frontend().Navigation().NavigationWrapper().AssertNavigationIsVisible(NavClass, TemplateType.Bootstrap), this.cssClassMedium + ":Navigation is visible");
        }

        /// <summary>
        /// UI test NavigationWidgetBootstrapTemplateVerifyDropDownTransformation
        /// Ignored due to infrastructural changes
        /// Css classes related to transfomartions should be added directly to the css file
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.Team2),
        TestCategory(FeatherTestCategories.Navigation),
        Ignore]
        public void NavigationWidgetBootstrapTemplateVerifyDropDownTransformation()
        {
            this.cssClass = "nav-xs-dropdown";
            this.cssClassSmall = "nav-sm-dropdown";
            this.cssClassMedium = "nav-md-dropdown";

            BAT.Macros().NavigateTo().Pages();
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(PageName);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(WidgetName);
            BATFeather.Wrappers().Backend().Navigation().NavigationWidgetEditWrapper().MoreOptions();
            BATFeather.Wrappers().Backend().Navigation().NavigationWidgetEditWrapper().FillCSSClass(this.cssClass);
            BATFeather.Wrappers().Backend().Navigation().NavigationWidgetEditWrapper().SaveChanges();
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();

            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower(), false);
            BATFeather.Wrappers().Frontend().Navigation().NavigationWrapper().ResizeBrowserWindow(500);
            Assert.IsTrue(
                BATFeather.Wrappers().Frontend().Navigation().NavigationWrapper().AsserNavigationDropDownMenuIsVisible(), this.cssClass + ":drop down menu not visible");
            
            BAT.Arrange(this.ArrangementClass).AddParameter(Key, this.cssClassSmall).ExecuteArrangement(this.ArrangementMethod);
            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower(), false);
            BATFeather.Wrappers().Frontend().Navigation().NavigationWrapper().ResizeBrowserWindow(800);
            Assert.IsTrue(
                BATFeather.Wrappers().Frontend().Navigation().NavigationWrapper().AsserNavigationDropDownMenuIsVisible(), this.cssClassSmall + ":drop down menu not visible");
            
            BAT.Arrange(this.ArrangementClass).AddParameter(Key, this.cssClassMedium).ExecuteArrangement(this.ArrangementMethod);
            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower(), false);
            BATFeather.Wrappers().Frontend().Navigation().NavigationWrapper().ResizeBrowserWindow(1000);
            Assert.IsTrue(
                BATFeather.Wrappers().Frontend().Navigation().NavigationWrapper().AsserNavigationDropDownMenuIsVisible(), this.cssClassMedium + ":drop down menu not visible");                     
        }

        /// <summary>
        /// UI test NavigationWidgetBootstrapTemplateVerifyTransformationsWithSeveralCssClasses
        /// Ignored due to infrastructural changes
        /// Css classes related to transfomartions should be added directly to the css file
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.Team2),
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
                BATFeather.Wrappers().Frontend().Navigation().NavigationWrapper().AsserNavigationDropDownMenuIsVisible(), "drop down menu not visible");

            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower(), false);
            BATFeather.Wrappers().Frontend().Navigation().NavigationWrapper().ResizeBrowserWindow(1000);
            Assert.IsFalse(
                BATFeather.Wrappers().Frontend().Navigation().NavigationWrapper().AssertNavigationIsVisible(NavClass, TemplateType.Bootstrap), "navigation is visible but it shouldn't be");
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
