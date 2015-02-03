using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Feather.Widgets.TestUI.Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FeatherWidgets.TestUI.TestCases.Navigation
{
    /// <summary>
    /// This is a test class with tests for Light navigation together with MVC navigation widget
    /// </summary>
    [TestClass]
    public class LightNavigationAndMvcNavigation : FeatherTestCase
    {
        [TestMethod,
        Owner("Feather team"),
        TestCategory(FeatherTestCategories.Navigation)]
        public void AddLightNavigationAndMvcNavigationOnTheSamePage()
        {
            BAT.Macros().NavigateTo().Pages();
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(PageName);
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().AddWidgetToDropZone(WidgetName);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().AddMvcNavigationWidgetToSelectedPlaceHolder(PlaceHolder);
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();

            string[] pages = new string[] { PageName, Page1, Page2 };

            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower());

            Assert.IsFalse(ActiveBrowser.ContainsText(ServerErrorMessage), ServerErrorMessage);
            BATFeather.Wrappers().Frontend().Navigation().NavigationWrapper().VerifyNavigationOnThePageFrontend(MvcNavClass, pages);
            BAT.Wrappers().Frontend().Navigation().NavigationFrontendWrapper().VerifyPagesFrontEndNavigation(LightNavClass, pages);
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

        private const string PageName = "FeatherPage";
        private const string Page1 = "Page1";
        private const string Page2 = "Page2";
        private const string WidgetName = "Navigation";
        private const string ServerErrorMessage = "Server Error";
        private const string MvcNavClass = "nav navbar-nav";
        private const string LightNavClass = "sfNavHorizontal sfNavList";
        private const string PlaceHolder = "Body";
    }
}
