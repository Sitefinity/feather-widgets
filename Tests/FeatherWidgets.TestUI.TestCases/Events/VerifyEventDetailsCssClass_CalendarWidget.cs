using Feather.Widgets.TestUI.Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeatherWidgets.TestUI.TestCases.Events
{
    /// <summary>
    /// Create a page with Calendar widget.
    /// Apply CSS class name in Single item settings tab and verify it on event details page.
    /// </summary>
    [TestClass]
    public class VerifyEventDetailsCssClass_CalendarWidget_ : FeatherTestCase
    {
        /// <summary>
        /// Test Method that provides test steps for VerifyWeekStartDay_CalendarView_ UI Test.
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.SitefinityTeam8),
        TestCategory(FeatherTestCategories.PagesAndContent),
        TestCategory(FeatherTestCategories.Events)]
        public void VerifyEventDetailsCssClass_CalendarWidget()
        {
            BAT.Macros().NavigateTo().Pages(this.Culture);
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(PageTitle);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget("Calendar");
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().OpenSingleItemSettingsView();
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().OpenMoreOptions();
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().ApplyCssClassInCalendarWidgetSingleItemSettingsTab(cssClassName);
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SaveChanges();
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();
            BAT.Macros().NavigateTo().CustomPage("~/" + PageTitle.ToLower(), true, this.Culture);
            BATFeather.Wrappers().Frontend().Events().EventsWrapper().OpenEventsDetailsInScheduler(еventTitle);
            BATFeather.Wrappers().Frontend().Events().EventsWrapper().VerifyCssClassInCalendarWidgetOnPage(cssClassName);
        }

        /// <summary>
        /// Performs Server Setup and prepare the system with needed data.
        /// </summary>
        protected override void ServerSetup()
        {
            BAT.Macros().User().EnsureAdminLoggedIn();
            var result = BAT.Arrange(this.TestName).ExecuteSetUp();
            currentEventId = result.Result.Values["currentEventId"];
        }

        /// <summary>
        /// Performs clean up and clears all data created by the test.
        /// </summary>
        protected override void ServerCleanup()
        {
            BAT.Arrange(this.TestName).ExecuteTearDown();
        }

        private const string PageTitle = "EventsPage";
        private const string cssClassName = "CssClassTestName";
        private const string еventTitle = "EventTitle";
        private string currentEventId = string.Empty;
    }
}
