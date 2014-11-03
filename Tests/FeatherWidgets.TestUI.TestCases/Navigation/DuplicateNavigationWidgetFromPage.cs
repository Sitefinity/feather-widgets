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
    /// DuplicateNavigationWidgetFromPage test class.
    /// </summary>
    [TestClass]
    public class DuplicateNavigationWidgetFromPage_ : FeatherTestCase
    {
        /// <summary>
        /// UI test DuplicateNavigationWidgetFromPage
        /// </summary>
        [TestMethod,
       Microsoft.VisualStudio.TestTools.UnitTesting.Owner("Feather team"),
       TestCategory(FeatherTestCategories.Navigation)]
        public void DuplicateNavigationWidgetFromPage()
        {
            BAT.Macros().NavigateTo().Pages();
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(PageName);
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().SelectExtraOptionForWidget(OperationName);
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();
            this.VerifyNavigationOnTheFrontend();
        }

        /// <summary>
        /// Verify navigation widget on the frontend
        /// </summary>
        public void VerifyNavigationOnTheFrontend()
        {
            string[] parentPages = new string[] { PageName };

            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower());
            ActiveBrowser.WaitUntilReady();

            BATFeather.Wrappers().Frontend().Navigation().NavigationWrapper().VerifyNavigationCountOnThePageFrontend(ExpectedCount);
            BATFeather.Wrappers().Frontend().Navigation().NavigationWrapper().VerifyNavigationOnThePageFrontend(NavTemplateClass, parentPages);
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
        private const string WidgetName = "Navigation";
        private const string OperationName = "Duplicate";
        private const string NavTemplateClass = "nav navbar-nav";
        private const int ExpectedCount = 2;
    }
}
