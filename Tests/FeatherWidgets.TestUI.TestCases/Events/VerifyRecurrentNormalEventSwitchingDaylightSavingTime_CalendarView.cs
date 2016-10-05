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
    /// Create recurrent normal event switching daylight saving time.
    /// Verify that the event is displayed correctly in Calendar.
    /// </summary>
    [TestClass]
    public class VerifyRecurrentNormalEventSwitchingDaylightSavingTime_CalendarView_ : FeatherTestCase
    {
        // <summary>
        /// Test Method that provides test steps for VerifyRecurrentNormalEventSwitchingDaylightSavingTime_CalendarView UI Test.
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.SitefinityTeam8),
        TestCategory(FeatherTestCategories.PagesAndContent),
        TestCategory(FeatherTestCategories.Events)]
        public void VerifyRecurrentNormalEventSwitchingDaylightSavingTime_CalendarView()
        {
            BAT.Macros().NavigateTo().CustomPage("~/" + pageTitle.ToLower(), true, this.Culture);

            BATFeather.Wrappers().Frontend().Events().EventsWrapper().NavigateToDateInSchedulerMonthView(DateTime.Now, event1Start);
            TimeZoneInfo timezoneInfo = TimeZoneInfo.FindSystemTimeZoneById(timezoneId);

            string expectedStartDateFirstOccurrenceEvent1 = TimeZoneInfo.ConvertTime(this.event1Start.ToUniversalTime(), timezoneInfo).ToString("s") + ".000Z";
            string expectedEndDateFirstOccurrenceEvent1 = TimeZoneInfo.ConvertTime(this.expectedEndDateFirstOccurrenceEvent1.ToUniversalTime(), timezoneInfo).ToString("s") + ".000Z";
            BATFeather.Wrappers().Frontend().Events().EventsWrapper().VerifyEventDate(event1Id, expectedStartDateFirstOccurrenceEvent1, true, 0);
            BATFeather.Wrappers().Frontend().Events().EventsWrapper().VerifyEventDate(event1Id, expectedEndDateFirstOccurrenceEvent1, false, 0);

            BATFeather.Wrappers().Frontend().Events().EventsWrapper().NavigateToDateInSchedulerMonthView(event1Start, event1End);
            string expectedStartDateLastOccurrenceEvent1 = TimeZoneInfo.ConvertTime(this.event1End.ToUniversalTime(), timezoneInfo).ToString("s") + ".000Z";
            string expectedEndDateLastOccurrenceEvent1 = TimeZoneInfo.ConvertTime(this.expectedEndDateLastOccurrenceEvent1.ToUniversalTime(), timezoneInfo).ToString("s") + ".000Z";

            BATFeather.Wrappers().Frontend().Events().EventsWrapper().VerifyEventDate(event1Id, expectedStartDateLastOccurrenceEvent1, true, 14);
            BATFeather.Wrappers().Frontend().Events().EventsWrapper().VerifyEventDate(event1Id, expectedEndDateLastOccurrenceEvent1, false, 14);

        }

        /// <summary>
        /// Performs Server Setup and prepare the system with needed data.
        /// </summary>
        protected override void ServerSetup()
        {
            BAT.Macros().User().EnsureAdminLoggedIn();
            var result = BAT.Arrange(this.TestName).ExecuteSetUp();
            event1Id = result.Result.Values["event1Id"];
            timezoneId = result.Result.Values["timezoneId"];
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
        private readonly DateTime expectedEndDateFirstOccurrenceEvent1 = new DateTime(2016, 1, 10, 11, 0, 0);
        private readonly DateTime event1End = new DateTime(2016, 4, 10, 10, 0, 0);
        private readonly DateTime expectedEndDateLastOccurrenceEvent1 = new DateTime(2016, 4, 10, 11, 0, 0);

        private string event1Id = string.Empty;
        private string timezoneId = string.Empty;
    }
}
