using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Feather.Widgets.TestUI.Framework;
using FeatherWidgets.TestUI.TestCases;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FeatherWidgets.TestUI
{
    /// <summary>
    /// NavigationWidgetAllLevelsToInclude test class.
    /// </summary>
    [TestClass]
    public class NavigationWidgetAllLevelsToInclude_ : FeatherTestCase
    {
        /// <summary>
        /// UI test NavigationWidgetAllLevelsToInclude
        /// </summary>
        [TestMethod,
       Microsoft.VisualStudio.TestTools.UnitTesting.Owner("Feather team"),
       TestCategory(FeatherTestCategories.Navigation)]
        public void NavigationWidgetAllLevelsToInclude()
        {
            BAT.Macros().NavigateTo().Pages();
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(PageName);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(WidgetName);
            BATFeather.Wrappers().Backend().Navigation().NavigationWidgetEditWrapper().SelectLevelsToInclude(LevelsToInclude);
            BATFeather.Wrappers().Backend().Navigation().NavigationWidgetEditWrapper().SelectWidgetListTemplate(NavWidgetTemplate);
            BATFeather.Wrappers().Backend().Navigation().NavigationWidgetEditWrapper().SaveChanges();
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();
            this.VerifyNavigationOnTheFrontend();
            this.SelectPageFromNavigation();
        }

        /// <summary>
        /// Verify navigation widget on the frontend
        /// </summary>
        public void VerifyNavigationOnTheFrontend()
        {
            string[] parentPages = new string[] { PageName };
            string[] childPages = new string[] { ChildPage1, ChildPage2 };

            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower());
            ActiveBrowser.WaitUntilReady();

            BATFeather.Wrappers().Frontend().Navigation().NavigationWrapper().VerifyNavigationOnThePageFrontend(NavTemplateClass, parentPages);
            BATFeather.Wrappers().Frontend().Navigation().NavigationWrapper().VerifyChildPagesFrontEndNavigation(NavTemplateChildClass, childPages);
        }

        /// <summary>
        /// Select page from navigation widget
        /// </summary>
        public void SelectPageFromNavigation()
        {
            BAT.Wrappers().Frontend().Navigation().NavigationFrontendWrapper().SelectPageFromNavigationByText(NavTemplateClass, ChildPage1);
            ActiveBrowser.WaitForUrl("/" + ChildPage1.ToLower(), true, 60000);
        }

        /// <summary>
        /// Performs Server Setup and prepare the system with needed data.
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

        private const string PageName = "ParentPage";
        private const string ChildPage1 = "ChildPage1";
        private const string ChildPage2 = "ChildPage2";
        private const string WidgetName = "Navigation";
        private const string LevelsToInclude = "-1";
        private const string NavWidgetTemplate = "Vertical";
        private const string NavTemplateClass = "nav nav-pills nav-stacked";
        private const string NavTemplateChildClass = "group nav nav-pills nav-stacked";
    }
}
