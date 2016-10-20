using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Feather.Widgets.TestUI.Framework;
using Feather.Widgets.TestUI.Framework.Framework.Wrappers.Frontend.Events;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FeatherWidgets.TestUI.TestCases.Events
{
    /// <summary>
    /// Creates 2 events each assigned to a calendar.
    /// Creates Mvc page with Calendar widget.
    /// Uncheck "Allow users to filter by calendars" and publish page.
    /// Verify that on frontend Calendar list is not visible.
    /// </summary>
    [TestClass]
    public class VerifyAllowUsersToFilterByCalendars_CalendarView_ : FeatherTestCase
    {
        /// <summary>
        /// Test Method that provides test steps for VerifyAllowUsersToFilterByCalendars_CalendarView_ UI Test.
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.SitefinityTeam8),
        TestCategory(FeatherTestCategories.PagesAndContent),
        TestCategory(FeatherTestCategories.Events)]
        public void VerifyAllowUsersToFilterByCalendars_CalendarView()
        {
            BAT.Macros().NavigateTo().Pages(this.Culture);
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(PageTitle);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget("Calendar");
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().OpenListSettingsView();
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().ClickAllowUsersToFilterByCalendars();
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SaveChanges();
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();
            BAT.Macros().NavigateTo().CustomPage("~/" + PageTitle.ToLower(), true, this.Culture);
            BATFeather.Wrappers().Frontend().Events().EventsWrapper().VerifyCalendarListIsNotVisible();
        }

        /// <summary>
        /// Performs Server Setup and prepare the system with needed data.
        /// </summary>
        protected override void ServerSetup()
        {
            BAT.Macros().User().EnsureAdminLoggedIn();
            var result = BAT.Arrange(this.TestName).ExecuteSetUp();
            event1Id = result.Result.Values["event1Id"];
        }

        /// <summary>
        /// Performs clean up and clears all data created by the test.
        /// </summary>
        protected override void ServerCleanup()
        {
            BAT.Arrange(this.TestName).ExecuteTearDown();
        }

        private const string PageTitle = "EventsPage";
        private const string Calendar1Title = "Calendar1";
        private const string Event1Title = "Event1Title";
        private string event1Id = string.Empty;
    }
}
