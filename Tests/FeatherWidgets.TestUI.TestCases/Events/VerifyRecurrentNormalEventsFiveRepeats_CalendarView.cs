using Feather.Widgets.TestUI.Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeatherWidgets.TestUI.TestCases.Events
{
    /// <summary>
    /// Create recurrent normal event with five repeats and verify its details in Calendar.
    /// </summary>
    [TestClass]
    public class VerifyRecurrentNormalEventsFiveRepeats_CalendarView_ : FeatherTestCase
    {
        /// <summary>
        /// Test Method that provides test steps for VerifyRecurrentNormalEventsFiveRepeats_CalendarView_ UI Test.
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.SitefinityTeam8),
        TestCategory(FeatherTestCategories.PagesAndContent),
        TestCategory(FeatherTestCategories.Events)]
        public void VerifyRecurrentNormalEventsFiveRepeats_CalendarView()
        {
            BAT.Macros().NavigateTo().CustomPage("~/" + pageTitle.ToLower(), true, this.Culture);

            //Verify Event1 occurrence count and start/end date for firs and last event occurrence
            BATFeather.Wrappers().Frontend().Events().EventsWrapper().NavigateToDateInSchedulerMonthView(DateTime.Now, event1Start);
            var actualEvent1Occurence = BATFeather.Wrappers().Frontend().Events().EventsWrapper().EventOccurenceCountInCurrentView(event1Id);
            Assert.AreEqual(expectedEventOccurence, actualEvent1Occurence);
            TimeZoneInfo timezoneInfo = TimeZoneInfo.FindSystemTimeZoneById(timezoneId);

            string expectedStartDateFirstOccurrenceEvent1 = TimeZoneInfo.ConvertTime(this.event1Start.ToUniversalTime(), timezoneInfo).ToString("s") + ".000Z";
            string expectedEndDateFirstOccurrenceEvent1 = TimeZoneInfo.ConvertTime(this.expectedEndDateFirstOccurrenceEvent1.ToUniversalTime(), timezoneInfo).ToString("s") + ".000Z";
            string expectedStartDateLastOccurrenceEvent1 = TimeZoneInfo.ConvertTime(this.expectedStartDateLastOccurrenceEvent1.ToUniversalTime(), timezoneInfo).ToString("s") + ".000Z";
            string expectedEndDateLastOccurrenceEvent1 = TimeZoneInfo.ConvertTime(this.expectedEndDateLastOccurrenceEvent1.ToUniversalTime(), timezoneInfo).ToString("s") + ".000Z";
            BATFeather.Wrappers().Frontend().Events().EventsWrapper().VerifyEventDate(event1Id, expectedStartDateFirstOccurrenceEvent1, true, 0);
            BATFeather.Wrappers().Frontend().Events().EventsWrapper().VerifyEventDate(event1Id, expectedEndDateFirstOccurrenceEvent1, false, 0);
            BATFeather.Wrappers().Frontend().Events().EventsWrapper().VerifyEventDate(event1Id, expectedStartDateLastOccurrenceEvent1, true, 4);
            BATFeather.Wrappers().Frontend().Events().EventsWrapper().VerifyEventDate(event1Id, expectedEndDateLastOccurrenceEvent1, false, 4);

            //Verify Event2 occurrence count and start/end date for firs and last event occurrence
            BATFeather.Wrappers().Frontend().Events().EventsWrapper().NavigateToDateInSchedulerMonthView(event1Start, event2Start);
            var actualEvent2Occurence = BATFeather.Wrappers().Frontend().Events().EventsWrapper().EventOccurenceCountInCurrentView(event2Id);
            Assert.AreEqual(expectedEventOccurence, actualEvent2Occurence);
            string expectedStartDateFirstOccurrenceEvent2 = TimeZoneInfo.ConvertTime(this.event2Start.ToUniversalTime(), timezoneInfo).ToString("s") + ".000Z";
            string expectedEndDateFirstOccurrenceEvent2 = TimeZoneInfo.ConvertTime(this.expectedEndDateFirstOccurrenceEvent2.ToUniversalTime(), timezoneInfo).ToString("s") + ".000Z";
            string expectedStartDateLastOccurrenceEvent2 = TimeZoneInfo.ConvertTime(this.expectedStartDateLastOccurrenceEvent2.ToUniversalTime(), timezoneInfo).ToString("s") + ".000Z";
            string expectedEndDateLastOccurrenceEvent2 = TimeZoneInfo.ConvertTime(this.expectedEndDateLastOccurrenceEvent2.ToUniversalTime(), timezoneInfo).ToString("s") + ".000Z";
            BATFeather.Wrappers().Frontend().Events().EventsWrapper().VerifyEventDate(event2Id, expectedStartDateFirstOccurrenceEvent2, true, 0);
            BATFeather.Wrappers().Frontend().Events().EventsWrapper().VerifyEventDate(event2Id, expectedEndDateFirstOccurrenceEvent2, false, 0);
            BATFeather.Wrappers().Frontend().Events().EventsWrapper().VerifyEventDate(event2Id, expectedStartDateLastOccurrenceEvent2, true, 4);
            BATFeather.Wrappers().Frontend().Events().EventsWrapper().VerifyEventDate(event2Id, expectedEndDateLastOccurrenceEvent2, false, 4);
        }

        /// <summary>
        /// Performs Server Setup and prepare the system with needed data.
        /// </summary>
        protected override void ServerSetup()
        {
            BAT.Macros().User().EnsureAdminLoggedIn();
            var result = BAT.Arrange(this.TestName).ExecuteSetUp();
            event1Id = result.Result.Values["event1Id"];
            event2Id = result.Result.Values["event2Id"];
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
        private int expectedEventOccurence = 5;

        
        private string event1Id = string.Empty;
        private readonly DateTime event1Start = new DateTime(2016, 1, 10, 10, 0, 0);
        private readonly DateTime expectedEndDateFirstOccurrenceEvent1 = new DateTime(2016, 1, 10, 11, 0, 0);
        private readonly DateTime expectedStartDateLastOccurrenceEvent1 = new DateTime(2016, 1, 14, 10, 0, 0);
        private readonly DateTime expectedEndDateLastOccurrenceEvent1 = new DateTime(2016, 1, 14, 11, 0, 0);

        private const string Event2Title = "Event2Title";
        private string event2Id = string.Empty;
        private readonly DateTime event2Start = new DateTime(2016, 4, 10, 10, 0, 0);
        private readonly DateTime expectedEndDateFirstOccurrenceEvent2 = new DateTime(2016, 4, 10, 11, 0, 0);
        private readonly DateTime expectedStartDateLastOccurrenceEvent2 = new DateTime(2016, 4, 14, 10, 0, 0);
        private readonly DateTime expectedEndDateLastOccurrenceEvent2 = new DateTime(2016, 4, 14, 11, 0, 0);
  
        private string timezoneId = string.Empty;
    }
}
