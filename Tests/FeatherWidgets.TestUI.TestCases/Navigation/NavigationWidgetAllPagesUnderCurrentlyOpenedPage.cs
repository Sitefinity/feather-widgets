using Feather.Widgets.TestUI.Framework;
using FeatherWidgets.TestUI.TestCases;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeatherWidgets.TestUI
{
    /// <summary>
    /// This is a sample test class.
    /// </summary>
    [TestClass]
    public class NavigationWidgetAllPagesUnderCurrentlyOpenedPage_ : FeatherTestCase
    {
        // <summary>
        /// Pefroms Server Setup and prepare the system with needed data.
        /// </summary>
        protected override void ServerSetup()
        {
            BAT.Macros().User().EnsureAdminLoggedIn();
            BAT.Arrange(this.TestName).ExecuteSetUp();
        }

        /// <summary>
        /// Performs clean up and clears all data created by the test.
        /// </summary>
        protected override void ServerCleanup()
        {
            BAT.Arrange(this.TestName).ExecuteTearDown();
        }

        [TestMethod,
       Microsoft.VisualStudio.TestTools.UnitTesting.Owner("Feather team"),
       TestCategory(FeatherTestCategories.PagesAndContent)]
        public void NavigationWidgetAllPagesUnderCurrentlyOpenedPage()
        {
            BAT.Macros().NavigateTo().Pages();
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(PageName);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().AddWidget(WidgetName);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(WidgetName);
            BATFeather.Wrappers().Backend().Navigation().SelectNavigationWidgetDisplayMode(NavWidgetDisplayMode);
            BATFeather.Wrappers().Backend().Navigation().SaveChanges();
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();
            this.NavigatePageOnTheFrontend(PageName);
            this.VerifyNavigationOnTheFrontend();
            this.SelectPageFromNavigation();
        }

        public void NavigatePageOnTheFrontend(string pageName)
        {
            BAT.Macros().NavigateTo().CustomPage("~/" + pageName.ToLower());
            ActiveBrowser.WaitUntilReady();
        }

        public void VerifyNavigationOnTheFrontend()
        {
            string[] parentPages = new string[] { PageName };
            string[] childPages = new string[] { ChildPage1, ChildPage2 };

            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower());
            ActiveBrowser.WaitUntilReady();

            BAT.Wrappers().Frontend().Navigation().NavigationFrontendWrapper().VerifyPagesNotPresentFrontEndNavigation(NavTemplateClass, parentPages);
            BAT.Wrappers().Frontend().Navigation().NavigationFrontendWrapper().VerifyPagesFrontEndNavigation(NavTemplateClass, childPages);
        }

        public void SelectPageFromNavigation()
        {
            BAT.Wrappers().Frontend().Navigation().NavigationFrontendWrapper().SelectPageFromNavigationByText(NavTemplateClass, ChildPage1);
            ActiveBrowser.WaitForUrl("/" + ChildPage1.ToLower(), true, 60000);
        }

        private const string PageName = "ParentPage";
        private const string ChildPage1 = "ChildPage1";
        private const string ChildPage2 = "ChildPage2";
        private const string WidgetName = "Navigation";
        private const string NavTemplateClass = "nav navbar-nav";
        private const string NavWidgetDisplayMode = "All pages under currently opened page";
    }
}
