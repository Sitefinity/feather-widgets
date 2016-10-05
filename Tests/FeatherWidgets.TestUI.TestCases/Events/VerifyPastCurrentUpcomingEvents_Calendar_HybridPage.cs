using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Feather.Widgets.TestUI.Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telerik.Sitefinity.TestUI.Framework.Wrappers.Backend.PageEditor;
using System.Globalization;


using Feather.Widgets.TestUI.Framework.Framework.Wrappers.Frontend.Events;

namespace FeatherWidgets.TestUI.TestCases.Events
{
    [TestClass]
    public class VerifyPastCurrentUpcomingEvents_Calendar_HybridPage_ : FeatherTestCase
    {
         /// <summary>
        /// Test Method that provides test steps for VerifyPastCurrentUpcomingEvents_Calendar_HybridPage_ UI Test.
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.SitefinityTeam8),
        TestCategory(FeatherTestCategories.PagesAndContent),
        TestCategory(FeatherTestCategories.Events)]
        public void VerifyPastCurrentUpcomingEvents_Calendar_HybridPage_Month_View()
        {
            //Monthly view verification
            BAT.Macros().NavigateTo().CustomPage("~/" + pageTitle.ToLower(), true, this.Culture);
            Assert.AreEqual(BATFeather.Wrappers().Frontend().Events().EventsWrapper().GetSelectedSchedulerView(), monthSchedulerView);
            this.EventVerification(currentEventTitle, currentEventStartDate, currentEventEndDate, pageTitle, activeCalendar, currentEventId);

            BAT.Macros().NavigateTo().CustomPage("~/" + pageTitle.ToLower(), true, this.Culture);
            this.EventVerification(basePastInOneDayEventTitle, basePastInOneDayEventStartDate, basePastInOneDayEventEndDate, pageTitle, activeCalendar, pastEventId);

            BAT.Macros().NavigateTo().CustomPage("~/" + pageTitle.ToLower(), true, this.Culture);
            this.EventVerification(baseUpcomingInOneDayEventTitle, baseUpcomingInOneDayEventStartDate, baseUpcomingInOneDayEventEndDate, pageTitle, activeCalendar, upcomingEventId);
        }
        /// <summary>
        /// Test Method that provides test steps for VerifyPastCurrentUpcomingEvents_Calendar_HybridPage_Day_View UI Test.
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.SitefinityTeam8),
        TestCategory(FeatherTestCategories.PagesAndContent),
        TestCategory(FeatherTestCategories.Events)]
        public void VerifyPastCurrentUpcomingEvents_Calendar_HybridPage_Day_View()
        {
            //// Day view verification
            BAT.Macros().NavigateTo().CustomPage("~/" + pageTitle.ToLower(), true, this.Culture);
            BATFeather.Wrappers().Frontend().Events().EventsWrapper().ChangeSchedulerView(SchedulerViewTypes.Day);
            Assert.AreEqual(BATFeather.Wrappers().Frontend().Events().EventsWrapper().GetSelectedSchedulerView(), daySchedulerView);
            this.VerifyEventDateInDayView(currentEventStartDate);
            this.EventVerification(currentEventTitle, currentEventStartDate, currentEventEndDate, pageTitle, activeCalendar, currentEventId);
            this.EventDetailsVerification(currentEventTitle);

            BAT.Macros().NavigateTo().CustomPage("~/" + pageTitle.ToLower(), true, this.Culture);
            BATFeather.Wrappers().Frontend().Events().EventsWrapper().ChangeSchedulerView(SchedulerViewTypes.Day);
            BATFeather.Wrappers().Frontend().Events().EventsWrapper().GoPrevious();
            this.VerifyEventDateInDayView(basePastInOneDayEventStartDate);
            this.EventVerification(basePastInOneDayEventTitle, basePastInOneDayEventStartDate, basePastInOneDayEventEndDate, pageTitle, activeCalendar, pastEventId);
            this.EventDetailsVerification(basePastInOneDayEventTitle);

            BAT.Macros().NavigateTo().CustomPage("~/" + pageTitle.ToLower(), true, this.Culture);
            BATFeather.Wrappers().Frontend().Events().EventsWrapper().ChangeSchedulerView(SchedulerViewTypes.Day);
            BATFeather.Wrappers().Frontend().Events().EventsWrapper().GoNext();
            this.VerifyEventDateInDayView(baseUpcomingInOneDayEventStartDate);
            this.EventVerification(baseUpcomingInOneDayEventTitle, baseUpcomingInOneDayEventStartDate, baseUpcomingInOneDayEventEndDate, pageTitle, activeCalendar, upcomingEventId);
            this.EventDetailsVerification(baseUpcomingInOneDayEventTitle);
        }

        /// <summary>
        /// Test Method that provides test steps for VerifyPastCurrentUpcomingEvents_Calendar_HybridPage_Week_View UI Test.
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.SitefinityTeam8),
        TestCategory(FeatherTestCategories.PagesAndContent),
        TestCategory(FeatherTestCategories.Events)]
        public void VerifyPastCurrentUpcomingEvents_Calendar_HybridPage_Week_View()
        {
            ////Week view verification
            BAT.Macros().NavigateTo().CustomPage("~/" + pageTitle.ToLower(), true, this.Culture);
            BATFeather.Wrappers().Frontend().Events().EventsWrapper().ChangeSchedulerView(SchedulerViewTypes.Week);
            Assert.AreEqual(BATFeather.Wrappers().Frontend().Events().EventsWrapper().GetSelectedSchedulerView(), weekSchedulerView);
            this.EventVerification(currentEventTitle, currentEventStartDate, currentEventEndDate, pageTitle, activeCalendar, currentEventId);

            BAT.Macros().NavigateTo().CustomPage("~/" + pageTitle.ToLower(), true, this.Culture);
            BATFeather.Wrappers().Frontend().Events().EventsWrapper().ChangeSchedulerView(SchedulerViewTypes.Week);
            Assert.AreEqual(BATFeather.Wrappers().Frontend().Events().EventsWrapper().GetSelectedSchedulerView(), weekSchedulerView);
            this.EventVerification(basePastInOneDayEventTitle, basePastInOneDayEventStartDate, basePastInOneDayEventEndDate, pageTitle, activeCalendar, pastEventId); ;

            BAT.Macros().NavigateTo().CustomPage("~/" + pageTitle.ToLower(), true, this.Culture);
            BATFeather.Wrappers().Frontend().Events().EventsWrapper().ChangeSchedulerView(SchedulerViewTypes.Week);
            Assert.AreEqual(BATFeather.Wrappers().Frontend().Events().EventsWrapper().GetSelectedSchedulerView(), weekSchedulerView);
            this.EventVerification(baseUpcomingInOneDayEventTitle, baseUpcomingInOneDayEventStartDate, baseUpcomingInOneDayEventEndDate, pageTitle, activeCalendar, upcomingEventId);
        }

        /// <summary>
        /// Test Method that provides test steps for VerifyPastCurrentUpcomingEvents_Calendar_HybridPage_Agenda_View UI Test.
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.SitefinityTeam8),
        TestCategory(FeatherTestCategories.PagesAndContent),
        TestCategory(FeatherTestCategories.Events)]
        public void VerifyPastCurrentUpcomingEvents_Calendar_HybridPage_Agenda_View()
        {
            ////Agenda view verification
            BAT.Macros().NavigateTo().CustomPage("~/" + pageTitle.ToLower(), true, this.Culture);
            BATFeather.Wrappers().Frontend().Events().EventsWrapper().ChangeSchedulerView(SchedulerViewTypes.Agenda);
            Assert.AreEqual(BATFeather.Wrappers().Frontend().Events().EventsWrapper().GetSelectedSchedulerView(), agendaSchedulerView);
            var expectedUpcomingEventTitle = BATFeather.Wrappers().Frontend().Events().EventsWrapper().GetEventTitleInScheduler(upcomingEventId);
            Assert.AreEqual(expectedUpcomingEventTitle, baseUpcomingInOneDayEventTitle);
            this.EventVerification(currentEventTitle, currentEventStartDate, currentEventEndDate, pageTitle, activeCalendar, currentEventId);

            BAT.Macros().NavigateTo().CustomPage("~/" + pageTitle.ToLower(), true, this.Culture);
            BATFeather.Wrappers().Frontend().Events().EventsWrapper().ChangeSchedulerView(SchedulerViewTypes.Agenda);
            Assert.AreEqual(BATFeather.Wrappers().Frontend().Events().EventsWrapper().GetSelectedSchedulerView(), agendaSchedulerView);
            BATFeather.Wrappers().Frontend().Events().EventsWrapper().GoNext();
            Assert.AreEqual(expectedUpcomingEventTitle, baseUpcomingInOneDayEventTitle);
            this.EventVerification(baseUpcomingInOneDayEventTitle, baseUpcomingInOneDayEventStartDate, baseUpcomingInOneDayEventEndDate, pageTitle, activeCalendar, upcomingEventId);

            BAT.Macros().NavigateTo().CustomPage("~/" + pageTitle.ToLower(), true, this.Culture);
            BATFeather.Wrappers().Frontend().Events().EventsWrapper().ChangeSchedulerView(SchedulerViewTypes.Agenda);
            Assert.AreEqual(BATFeather.Wrappers().Frontend().Events().EventsWrapper().GetSelectedSchedulerView(), agendaSchedulerView);
            BATFeather.Wrappers().Frontend().Events().EventsWrapper().GoPrevious();
            Assert.AreEqual(expectedUpcomingEventTitle, baseUpcomingInOneDayEventTitle);
            var expectedCurrentEventTitle = BATFeather.Wrappers().Frontend().Events().EventsWrapper().GetEventTitleInScheduler(currentEventId);
            Assert.AreEqual(expectedCurrentEventTitle, currentEventTitle);
            this.EventVerification(basePastInOneDayEventTitle, basePastInOneDayEventStartDate, basePastInOneDayEventEndDate, pageTitle, activeCalendar, pastEventId);
        }

        /// <summary>
        /// Test Method that provides test steps for VerifyPastCurrentUpcomingEvents_Calendar_HybridPage_Timeline_View UI Test.
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.SitefinityTeam8),
        TestCategory(FeatherTestCategories.PagesAndContent),
        TestCategory(FeatherTestCategories.Events)]
        public void VerifyPastCurrentUpcomingEvents_Calendar_HybridPage_Timeline_View()
        {
            ////Timeline view verification
            BAT.Macros().NavigateTo().CustomPage("~/" + pageTitle.ToLower(), true, this.Culture);
            BATFeather.Wrappers().Frontend().Events().EventsWrapper().ChangeSchedulerView(SchedulerViewTypes.Timeline);
            Assert.AreEqual(BATFeather.Wrappers().Frontend().Events().EventsWrapper().GetSelectedSchedulerView(), timelineSchedulerView);
            this.VerifyEventDateInDayView(currentEventStartDate);
            this.EventVerification(currentEventTitle, currentEventStartDate, currentEventEndDate, pageTitle, activeCalendar, currentEventId);

            BAT.Macros().NavigateTo().CustomPage("~/" + pageTitle.ToLower(), true, this.Culture);
            BATFeather.Wrappers().Frontend().Events().EventsWrapper().ChangeSchedulerView(SchedulerViewTypes.Timeline);
            BATFeather.Wrappers().Frontend().Events().EventsWrapper().GoPrevious();
            this.VerifyEventDateInDayView(basePastInOneDayEventStartDate);
            this.EventVerification(basePastInOneDayEventTitle, basePastInOneDayEventStartDate, basePastInOneDayEventEndDate, pageTitle, activeCalendar, pastEventId);

            BAT.Macros().NavigateTo().CustomPage("~/" + pageTitle.ToLower(), true, this.Culture);
            BATFeather.Wrappers().Frontend().Events().EventsWrapper().ChangeSchedulerView(SchedulerViewTypes.Timeline);
            BATFeather.Wrappers().Frontend().Events().EventsWrapper().GoNext();
            this.VerifyEventDateInDayView(baseUpcomingInOneDayEventStartDate);
            this.EventVerification(baseUpcomingInOneDayEventTitle, baseUpcomingInOneDayEventStartDate, baseUpcomingInOneDayEventEndDate, pageTitle, activeCalendar, upcomingEventId);
        }

        /// <summary>
        /// Performs Server Setup and prepare the system with needed data.
        /// </summary>
        protected override void ServerSetup()
        {
            BAT.Macros().User().EnsureAdminLoggedIn();
            var result = BAT.Arrange(this.TestName).ExecuteSetUp();
            currentEventStartDate = result.Result.Values["currentEventStartTime"];
            currentEventEndDate = result.Result.Values["currentEventEndTime"];
            basePastInOneDayEventStartDate = result.Result.Values["basePastInOneDayEventStartTime"];
            basePastInOneDayEventEndDate = result.Result.Values["basePastInOneDayEventEndTime"];
            baseUpcomingInOneDayEventStartDate = result.Result.Values["baseUpcomingInOneDayEventStartTime"];
            baseUpcomingInOneDayEventEndDate = result.Result.Values["baseUpcomingInOneDayEventEndTime"];
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
        /// Parse event date in day view
        /// </summary>
        /// <param name="eventstartDateTime"></param>
        /// <returns>Expected day date</returns>
        private string ParseEventDateDayView(string eventstartDateTime)
        {
            var parsedDate = DateTime.Parse(eventstartDateTime);
            var expectedDayDate = String.Format(CultureInfo.InvariantCulture, "{0:dddd, MMMM dd, yyyy}", parsedDate);
            return expectedDayDate;
        }

        /// <summary>
        /// Verify event day in day view
        /// </summary>
        private void VerifyEventDateInDayView(string eventStartDate)
        {
            var actualDate = this.ParseEventDateDayView(eventStartDate);
            var expectedDate = BATFeather.Wrappers().Frontend().Events().EventsWrapper().GetDateInDayView();
            Assert.AreEqual(expectedDate, actualDate);
        }

        /// <summary>
        /// Verifies event title in event details view
        /// </summary>
        /// <param name="eventTitle">event Title</param>
        private void EventDetailsVerification(string eventTitle)
        {
            BATFeather.Wrappers().Frontend().Events().EventsWrapper().OpenEventsDetailsInScheduler(eventTitle);
            var expectedEventTitleInDetailsView = BATFeather.Wrappers().Frontend().Events().EventsWrapper().GetEventTitleInDetailsView();
            Assert.AreEqual(expectedEventTitleInDetailsView, eventTitle);
        }

        /// <summary>
        /// Verifies event title in scheduler's view
        /// </summary>
        /// <param name="eventTitle">Event Title</param>
        /// <param name="eventStartDate">Event Start Date</param>
        /// <param name="eventEndDate">Event End Date</param>
        /// <param name="pageTitle">Page Title</param>
        /// <param name="activeCalendar">Active Calendar</param>
        private void EventVerification(string eventTitle, string eventStartDate, string eventEndDate, string pageTitle, string activeCalendar, string eventId)
        {
            DateTime eventStartDateTime = Convert.ToDateTime(eventStartDate);
            DateTime eventEndDateTime = Convert.ToDateTime(eventEndDate);
            TimeZoneInfo timezoneInfo = TimeZoneInfo.FindSystemTimeZoneById(timezoneId);
            var expectedEventTitle = BATFeather.Wrappers().Frontend().Events().EventsWrapper().GetEventTitleInScheduler(eventId);
            Assert.AreEqual(expectedEventTitle, eventTitle);
            string expectedStartDateFirstOccurrenceEvent = TimeZoneInfo.ConvertTime(eventStartDateTime.ToUniversalTime(), timezoneInfo).ToString("s") + ".000Z";
            string expectedEndDateFirstOccurrenceEvent = TimeZoneInfo.ConvertTime(eventEndDateTime.ToUniversalTime(), timezoneInfo).ToString("s") + ".000Z";
            BATFeather.Wrappers().Frontend().Events().EventsWrapper().VerifyEventDate(eventId, expectedStartDateFirstOccurrenceEvent, true, 0);
            BATFeather.Wrappers().Frontend().Events().EventsWrapper().VerifyEventDate(eventId, expectedEndDateFirstOccurrenceEvent, false, 0);
        }

        private const string pageTitle = "EventsPage";
        private const string monthSchedulerView = "Month";
        private const string daySchedulerView = "Day";
        private const string weekSchedulerView = "Week";
        private const string agendaSchedulerView = "Agenda";
        private const string timelineSchedulerView = "Timeline";
        private string currentEventStartDate = string.Empty;
        private string currentEventEndDate = string.Empty;
        private string basePastInOneDayEventStartDate = string.Empty;
        private string basePastInOneDayEventEndDate = string.Empty;
        private string baseUpcomingInOneDayEventStartDate = string.Empty;
        private string baseUpcomingInOneDayEventEndDate = string.Empty;
        private string activeCalendar = "default-calendar";
        private string currentEventTitle = "CurrentEvent_Title";
        private string basePastInOneDayEventTitle = "PastInOneDayEvent_Title";
        private string baseUpcomingInOneDayEventTitle = "UpcomingInOneDayEvent_Title";
        private string timezoneId = string.Empty;
        private string currentEventId = string.Empty;
        private string pastEventId = string.Empty;
        private string upcomingEventId = string.Empty;
    }
}
