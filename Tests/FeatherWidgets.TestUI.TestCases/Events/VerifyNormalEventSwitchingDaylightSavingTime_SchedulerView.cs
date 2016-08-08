using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Feather.Widgets.TestUI.Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FeatherWidgets.TestUI.TestCases.Events
{
    /// <summary>
    /// Create normal event switching daylight saving time.
    /// Verify that the event is displayed correctly in Scheduler.
    /// </summary>
    [TestClass]
    public class CreateNormalEventSwitchingDaylightSavingTime_SchedulerView_ : FeatherTestCase
    {
        // <summary>
        /// Test Method that provides test steps for CreateNormalEventSwitchingDaylightSavingTime_SchedulerView UI Test.
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.SitefinityTeam8),
        TestCategory(FeatherTestCategories.PagesAndContent),
        TestCategory(FeatherTestCategories.Events)]
        public void CreateNormalEventSwitchingDaylightSavingTime_SchedulerView()
        {
            BAT.Macros().NavigateTo().Pages(this.Culture);
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(pageTitle);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget("Events");
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().ActivateSchedulerMode();
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();
            BAT.Macros().NavigateTo().CustomPage("~/" + pageTitle.ToLower(), true, this.Culture);

            BATFeather.Wrappers().Frontend().Events().EventsWrapper().NavigateToDateInSchedulerMonthView(DateTime.Now, event1Start);
            BATFeather.Wrappers().Frontend().Events().EventsWrapper().VerifyEventDate(event1Id, expectedStartDateEvent1, true, 0);
            BATFeather.Wrappers().Frontend().Events().EventsWrapper().NavigateToDateInSchedulerMonthView(event1Start, event1End);
            BATFeather.Wrappers().Frontend().Events().EventsWrapper().VerifyEventDate(event1Id, expectedEndDateEvent1, false, 2);
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

        private const string pageTitle = "EventsPage";
        private const string Event1Title = "Event1Title";
        private readonly DateTime event1Start = new DateTime(2016, 1, 10, 10, 0, 0);
        private readonly DateTime event1End = new DateTime(2016, 4, 10, 11, 0, 0);
        string expectedStartDateEvent1 = "Sun Jan 10 2016 10:00:00 GMT+0200 (FLE Standard Time)";
        string expectedEndDateEvent1 = "Sun Apr 10 2016 11:00:00 GMT+0300 (FLE Daylight Time)";
        private string event1Id = string.Empty;
    }
}
