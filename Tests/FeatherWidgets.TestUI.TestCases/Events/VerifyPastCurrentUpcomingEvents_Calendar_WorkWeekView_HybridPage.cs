using Feather.Widgets.TestUI.Framework;
using Feather.Widgets.TestUI.Framework.Framework.Wrappers.Frontend.Events;
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
    /// Create a page with MVC Page Template and add Mvc Events widget with Calendar view.
    /// Verify events items that show in preview mode, publish the page. 
    /// Verify event items (past, current, upcoming) in Work Week view.
    /// </summary>
    [TestClass]
    public class VerifyPastCurrentUpcomingEvents_Calendar_WorkWeekView_HybridPage_ : FeatherTestCase
    {
        /// <summary>
        /// Test Method that provides test steps for VerifyPastCurrentUpcomingEvents_Calendar_WorkWeekView_HybridPage_ UI Test.
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.SitefinityTeam8),
        TestCategory(FeatherTestCategories.PagesAndContent),
        TestCategory(FeatherTestCategories.Events)]
        public void VerifyPastCurrentUpcomingEvents_Calendar_WorkWeekView_HybridPage()
        {
            BAT.Macros().NavigateTo().CustomPage("~/" + pageTitle.ToLower(), true, this.Culture);
            BATFeather.Wrappers().Frontend().Events().EventsWrapper().ChangeSchedulerView(SchedulerViewTypes.WorkWeek);
            BATFeather.Wrappers().Frontend().Events().EventsWrapper().NavigateToDateInSchedulerWorkWeekView(DateTime.Now, targetWorkWeek);
            Assert.AreEqual(BATFeather.Wrappers().Frontend().Events().EventsWrapper().GetSelectedSchedulerView(), workweekSchedulerView);
            this.EventVerification(currentEventTitle, currentEventStartTime, currentEventEndTime, pageTitle, activeCalendar, currentEventId);

            this.EventVerification(basePastInOneDayEventTitle, basePastInOneDayEventStartTime, basePastInOneDayEventEndTime, pageTitle, activeCalendar, pastEventId);

            this.EventVerification(baseUpcomingInOneDayEventTitle, baseUpcomingInOneDayEventStartTime, baseUpcomingInOneDayEventEndTime, pageTitle, activeCalendar, upcomingEventId);
        }

        /// <summary>
        /// Performs Server Setup and prepare the system with needed data.
        /// </summary>
        protected override void ServerSetup()
        {
            BAT.Macros().User().EnsureAdminLoggedIn();
            var result = BAT.Arrange(this.TestName).ExecuteSetUp();
            timezoneId = result.Result.Values["timezoneId"];
            currentEventId = result.Result.Values["currentEventId"];
            pastEventId = result.Result.Values["pastEventId"];
            upcomingEventId = result.Result.Values["upcomingEventId"];
        }

        /// <summary>
        /// Executes the server clean up code.
        /// </summary>
        protected override void ServerCleanup()
        {
            BAT.Arrange(this.TestName).ExecuteTearDown();
        }

        /// <summary>
        /// Format Event date to URL date
        /// </summary>
        /// <param name="eventDateTime"></param>
        /// <returns>Expected URL date</returns>
        private string ParseUrlDate(string eventDateTime)
        {
            var parsedDate = DateTime.Parse(eventDateTime);
            var expectedUrlDate = String.Format(CultureInfo.InvariantCulture, "{0:yyyy/MM/dd}", parsedDate);
            return expectedUrlDate;
        }

        /// <summary>
        /// Format start datetime and end datetime to match format from detail's view
        /// </summary>
        /// <param name="eventstartDateTime">Event start datetime</param>
        /// <param name="eventEndDateTime">Event end datetime</param>
        /// <returns></returns>
        private string ParseEventDateDetailsView(string eventstartDateTime, string eventEndDateTime)
        {
            TimeZoneInfo timezoneInfo = TimeZoneInfo.FindSystemTimeZoneById(timezoneId);
            var parsedDate1 = DateTime.Parse(eventstartDateTime);
            var expectedUrlDate1 = TimeZoneInfo.ConvertTime(parsedDate1.ToUniversalTime(), timezoneInfo).ToString("d MMMM, yyyy, h mm tt");
            var parsedDate2 = DateTime.Parse(eventEndDateTime);
            var expectedUrlDate2 = TimeZoneInfo.ConvertTime(parsedDate2.ToUniversalTime(), timezoneInfo).ToString("h mm tt");
            var expectedUrlDate = expectedUrlDate1 + "-" + expectedUrlDate2;
            return expectedUrlDate;
        }

        /// <summary>
        /// Verifies event title in scheduler's view
        /// </summary>
        /// <param name="eventTitle">Event Title</param>
        /// <param name="eventStartDate">Event Start Date</param>
        /// <param name="eventEndDate">Event End Date</param>
        /// <param name="pageTitle">Page Title</param>
        /// <param name="activeCalendar">Active Calendar</param>
        private void EventVerification(string eventTitle, DateTime eventStartDate, DateTime eventEndDate, string pageTitle, string activeCalendar, string eventId)
        {
            TimeZoneInfo timezoneInfo = TimeZoneInfo.FindSystemTimeZoneById(timezoneId);
            var expectedEventTitle = BATFeather.Wrappers().Frontend().Events().EventsWrapper().GetEventTitleInScheduler(eventId);
            Assert.AreEqual(expectedEventTitle, eventTitle);
            string expectedStartDateFirstOccurrenceEvent = TimeZoneInfo.ConvertTime(eventStartDate.ToUniversalTime(), timezoneInfo).ToString("s") + ".000Z";
            string expectedEndDateFirstOccurrenceEvent = TimeZoneInfo.ConvertTime(eventEndDate.ToUniversalTime(), timezoneInfo).ToString("s") + ".000Z";
            BATFeather.Wrappers().Frontend().Events().EventsWrapper().VerifyEventDate(eventId, expectedStartDateFirstOccurrenceEvent, true, 0);
            BATFeather.Wrappers().Frontend().Events().EventsWrapper().VerifyEventDate(eventId, expectedEndDateFirstOccurrenceEvent, false, 0);
        }

        private const string currentEventTitle = "CurrentEvent_Title";
        private readonly DateTime currentEventStartTime = new DateTime(2016, 7, 12, 10, 0, 0);
        private readonly DateTime currentEventEndTime = new DateTime(2016, 7, 12, 11, 0, 0);

        private const string basePastInOneDayEventTitle = "PastInOneDayEvent_Title";
        private readonly DateTime basePastInOneDayEventStartTime = new DateTime(2016, 7, 11, 10, 0, 0);
        private readonly DateTime basePastInOneDayEventEndTime = new DateTime(2016, 7, 11, 11, 0, 0);

        private const string baseUpcomingInOneDayEventTitle = "UpcomingInOneDayEvent_Title";
        private readonly DateTime baseUpcomingInOneDayEventStartTime = new DateTime(2016, 7, 13, 10, 0, 0);
        private readonly DateTime baseUpcomingInOneDayEventEndTime = new DateTime(2016, 7, 13, 11, 0, 0);

        private readonly DateTime targetWorkWeek = new DateTime(2016, 7, 12, 0, 0, 0, DateTimeKind.Utc);
        private const string pageTitle = "EventsPage";
        private const string workweekSchedulerView = "Work Week";
        private string activeCalendar = "default-calendar";
        private string timezoneId = string.Empty;
        private string currentEventId = string.Empty;
        private string pastEventId = string.Empty;
        private string upcomingEventId = string.Empty;
    }
}
