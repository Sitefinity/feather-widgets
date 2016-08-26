using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Feather.Widgets.TestUI.Framework;
using Feather.Widgets.TestUI.Framework.Framework.Wrappers.Frontend.Events;

namespace FeatherWidgets.TestUI.TestCases.Events
{
    /// <summary>
    /// Create recurrent all day event with five repeats and verify its details in Calendar.
    /// </summary>
    [TestClass]
    public class VerifyRecurrentAllDayEventFiveRepeats_CalendarView_ : FeatherTestCase
    {
        // <summary>
        /// Test Method that provides test steps for VerifyMultipleAllDayEventsInThreeDays_CalendarView_ UI Test.
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.SitefinityTeam8),
        TestCategory(FeatherTestCategories.PagesAndContent),
        TestCategory(FeatherTestCategories.Events)]
        public void VerifyRecurrentAllDayEventFiveRepeats_CalendarView()
        {
            BAT.Macros().NavigateTo().CustomPage("~/" + pageTitle.ToLower(), true, this.Culture);

            var localTimeZone = BATFeather.Wrappers().Frontend().Events().EventsWrapper().LocalTimeZoneOffset();
            TimeZoneInfo timezoneInfo = TimeZoneInfo.FindSystemTimeZoneById(timezoneId);

            string expectedStartDateEvent1 = TimeZoneInfo.ConvertTime(this.event1Start.ToUniversalTime(), timezoneInfo).ToString("s") + ".000Z";
            string expectedEndDateEvent1 = TimeZoneInfo.ConvertTime(this.event1End.ToUniversalTime(), timezoneInfo).ToString("s") + ".000Z";

            //Event1
            //Month verification
            BATFeather.Wrappers().Frontend().Events().EventsWrapper().NavigateToDateInSchedulerMonthView(DateTime.Now, event1Start);
            this.VerifyAllDayEventVisibilityInCurrentView(event1Id, 5);
            BATFeather.Wrappers().Frontend().Events().EventsWrapper().VerifyEventDate(event1Id, expectedStartDateEvent1, true, 0);
            BATFeather.Wrappers().Frontend().Events().EventsWrapper().VerifyEventDate(event1Id, expectedEndDateEvent1, false, 4);

            //Day view
            BATFeather.Wrappers().Frontend().Events().EventsWrapper().OpenTargetDayInDayView("09");
            this.VerifyAllDayEventVisibilityInCurrentView(event1Id, 0);

            foreach (var expectedDate in expectedDates)
            {
                string expectedConvertedDate = TimeZoneInfo.ConvertTime(expectedDate.ToUniversalTime(), timezoneInfo).ToString("s") + ".000Z";
                BATFeather.Wrappers().Frontend().Events().EventsWrapper().GoNext();
                this.VerifyAllDayEventVisibilityInCurrentView(event1Id, 1);
                BATFeather.Wrappers().Frontend().Events().EventsWrapper().VerifyEventDate(event1Id, expectedConvertedDate, true, 0);
                BATFeather.Wrappers().Frontend().Events().EventsWrapper().VerifyEventDate(event1Id, expectedConvertedDate, false, 0);
            }

            BATFeather.Wrappers().Frontend().Events().EventsWrapper().GoNext();
            this.VerifyAllDayEventVisibilityInCurrentView(event1Id, 0);

            //Week view
            BATFeather.Wrappers().Frontend().Events().EventsWrapper().ChangeSchedulerView(SchedulerViewTypes.Week);
            this.VerifyAllDayEventVisibilityInCurrentView(event1Id, 5);
            BATFeather.Wrappers().Frontend().Events().EventsWrapper().VerifyEventDate(event1Id, expectedStartDateEvent1, true, 0);
            BATFeather.Wrappers().Frontend().Events().EventsWrapper().VerifyEventDate(event1Id, expectedEndDateEvent1, false, 4);

            //Agenda view
            BATFeather.Wrappers().Frontend().Events().EventsWrapper().ChangeSchedulerView(SchedulerViewTypes.Month);
            BATFeather.Wrappers().Frontend().Events().EventsWrapper().OpenTargetDayInDayView("10");
            BATFeather.Wrappers().Frontend().Events().EventsWrapper().ChangeSchedulerView(SchedulerViewTypes.Agenda);
            this.VerifyAllDayEventVisibilityInCurrentView(event1Id, 5);
            //Add 1 day to expectedEndDateEvent because of Kendo Agenda issue. 
            //Must be removed when Kendo fixes its issue.
            string expectedEndDateEventAgendaView = TimeZoneInfo.ConvertTime(this.event1End.AddDays(1), timezoneInfo).ToString("s") + ".000Z";
            BATFeather.Wrappers().Frontend().Events().EventsWrapper().VerifyEventDate(event1Id, expectedStartDateEvent1, true, 0);
            BATFeather.Wrappers().Frontend().Events().EventsWrapper().VerifyEventDate(event1Id, expectedEndDateEventAgendaView, false, 4);

            //Timeline view
            BATFeather.Wrappers().Frontend().Events().EventsWrapper().ChangeSchedulerView(SchedulerViewTypes.Month);
            BATFeather.Wrappers().Frontend().Events().EventsWrapper().OpenTargetDayInDayView("09");
            BATFeather.Wrappers().Frontend().Events().EventsWrapper().ChangeSchedulerView(SchedulerViewTypes.Timeline);
            foreach (var expectedDate in expectedDates)
            {
                string expectedConvertedDate = TimeZoneInfo.ConvertTime(expectedDate.ToUniversalTime(), timezoneInfo).ToString("s") + ".000Z";
                BATFeather.Wrappers().Frontend().Events().EventsWrapper().GoNext();
                this.VerifyAllDayEventVisibilityInCurrentView(event1Id, 1);
                BATFeather.Wrappers().Frontend().Events().EventsWrapper().VerifyEventDate(event1Id, expectedConvertedDate, true, 0);
                BATFeather.Wrappers().Frontend().Events().EventsWrapper().VerifyEventDate(event1Id, expectedConvertedDate, false, 0);
            }
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

        /// <summary>
        /// Verify all day event visibility in current view
        /// </summary>
        /// <param name="eventId">Event ID</param>
        private void VerifyAllDayEventVisibilityInCurrentView(string eventId, int expectedVisibleEvents)
        {
            var list = BATFeather.Wrappers().Frontend().Events().EventsWrapper().GetVisibleEventInCurrentView(eventId, true);
            Assert.IsTrue(list.Count() == expectedVisibleEvents, "The event is not visible");
        }

        private const string pageTitle = "EventsPage";
        private const string Event1Title = "Event1Title";
        private readonly DateTime event1Start = new DateTime(2016, 1, 10, 0, 0, 0);
        private readonly DateTime event1End = new DateTime(2016, 1, 14, 0, 0, 0);

        private readonly List<DateTime> expectedDates =
            new List<DateTime>()
            {
                new DateTime(2016, 1, 10, 0, 0, 0),
                new DateTime(2016, 1, 11, 0, 0, 0),
                new DateTime(2016, 1, 12, 0, 0, 0),
                new DateTime(2016, 1, 13, 0, 0, 0),
                new DateTime(2016, 1, 14, 0, 0, 0),
            };

        private string event1Id = string.Empty;
        private string timezoneId = string.Empty;
    }
}
