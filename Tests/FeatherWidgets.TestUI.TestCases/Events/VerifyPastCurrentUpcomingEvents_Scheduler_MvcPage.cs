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
    /// <summary>
    /// Create a page with MVC Page Template and add Mvc Events widget with Scheduler view.
    /// Verify events items that show in preview mode, publish the page. 
    /// Verify event items (past, current, upcoming) in the frontend page.
    /// </summary>
    [TestClass]
    public class VerifyPastCurrentUpcomingEvents_Scheduler_MvcPage_ : FeatherTestCase
    {
          /// <summary>
        /// Test Method that provides test steps for VerifyPastCurrentUpcomingEvents_Scheduler_MvcPage_Month_View UI Test.
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.SitefinityTeam8),
        TestCategory(FeatherTestCategories.PagesAndContent),
        TestCategory(FeatherTestCategories.Events)]
        public void VerifyPastCurrentUpcomingEvents_Scheduler_MvcPage_Month_View()
        {
            BAT.Macros().NavigateTo().Pages(this.Culture);
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(pageTitle);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget("Events");
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().ActivateSchedulerMode();
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();
            BAT.Macros().NavigateTo().CustomPage("~/" + pageTitle.ToLower(), true, this.Culture);
            Assert.AreEqual(BATFeather.Wrappers().Frontend().Events().EventsWrapper().GetSelectedSchedulerView(), monthSchedulerView);

            ////Monthly view verification
            this.EventVerification(currentEventTitle, currentEventStartDate, currentEventEndDate, pageTitle, activeCalendar);

            BAT.Macros().NavigateTo().CustomPage("~/" + pageTitle.ToLower(), true, this.Culture);
            this.EventVerification(basePastInOneDayEventTitle, basePastInOneDayEventStartDate, basePastInOneDayEventEndDate, pageTitle, activeCalendar);

            BAT.Macros().NavigateTo().CustomPage("~/" + pageTitle.ToLower(), true, this.Culture);
            this.EventVerification(baseUpcomingInOneDayEventTitle, baseUpcomingInOneDayEventStartDate, baseUpcomingInOneDayEventEndDate, pageTitle, activeCalendar);
        }

        /// <summary>
        /// Test Method that provides test steps for VerifyPastCurrentUpcomingEvents_Scheduler_MvcPage_Day_View UI Test.
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.SitefinityTeam8),
        TestCategory(FeatherTestCategories.PagesAndContent),
        TestCategory(FeatherTestCategories.Events)]
        public void VerifyPastCurrentUpcomingEvents_Scheduler_MvcPage_Day_View()
        {
            //// Day view verification
            BAT.Macros().NavigateTo().CustomPage("~/" + pageTitle.ToLower(), true, this.Culture);
            BATFeather.Wrappers().Frontend().Events().EventsWrapper().ChangeSchedulerView(SchedulerViewTypes.Day);
            Assert.AreEqual(BATFeather.Wrappers().Frontend().Events().EventsWrapper().GetSelectedSchedulerView(), daySchedulerView);
            this.VerifyEventDateInDayView(currentEventStartDate);
            this.EventVerification(currentEventTitle, currentEventStartDate, currentEventEndDate, pageTitle, activeCalendar);

            BAT.Macros().NavigateTo().CustomPage("~/" + pageTitle.ToLower(), true, this.Culture);
            BATFeather.Wrappers().Frontend().Events().EventsWrapper().ChangeSchedulerView(SchedulerViewTypes.Day);
            BATFeather.Wrappers().Frontend().Events().EventsWrapper().NavigateToPreviousDay();
            this.VerifyEventDateInDayView(basePastInOneDayEventStartDate);
            this.EventVerification(basePastInOneDayEventTitle, basePastInOneDayEventStartDate, basePastInOneDayEventEndDate, pageTitle, activeCalendar);

            BAT.Macros().NavigateTo().CustomPage("~/" + pageTitle.ToLower(), true, this.Culture);
            BATFeather.Wrappers().Frontend().Events().EventsWrapper().ChangeSchedulerView(SchedulerViewTypes.Day);
            BATFeather.Wrappers().Frontend().Events().EventsWrapper().NavigateToNextDay();
            this.VerifyEventDateInDayView(baseUpcomingInOneDayEventStartDate);
            this.EventVerification(baseUpcomingInOneDayEventTitle, baseUpcomingInOneDayEventStartDate, baseUpcomingInOneDayEventEndDate, pageTitle, activeCalendar);
        }

        /// <summary>
        /// Test Method that provides test steps for VerifyPastCurrentUpcomingEvents_Scheduler_MvcPage_Week_View UI Test.
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.SitefinityTeam8),
        TestCategory(FeatherTestCategories.PagesAndContent),
        TestCategory(FeatherTestCategories.Events)]
        public void VerifyPastCurrentUpcomingEvents_Scheduler_MvcPage_Week_View()
        {
            ////Week view verification
            BAT.Macros().NavigateTo().CustomPage("~/" + pageTitle.ToLower(), true, this.Culture);
            BATFeather.Wrappers().Frontend().Events().EventsWrapper().ChangeSchedulerView(SchedulerViewTypes.Week);
            Assert.AreEqual(BATFeather.Wrappers().Frontend().Events().EventsWrapper().GetSelectedSchedulerView(), weekSchedulerView);
            this.EventVerification(currentEventTitle, currentEventStartDate, currentEventEndDate, pageTitle, activeCalendar);

            BAT.Macros().NavigateTo().CustomPage("~/" + pageTitle.ToLower(), true, this.Culture);
            BATFeather.Wrappers().Frontend().Events().EventsWrapper().ChangeSchedulerView(SchedulerViewTypes.Week);
            Assert.AreEqual(BATFeather.Wrappers().Frontend().Events().EventsWrapper().GetSelectedSchedulerView(), weekSchedulerView);
            this.EventVerification(basePastInOneDayEventTitle, basePastInOneDayEventStartDate, basePastInOneDayEventEndDate, pageTitle, activeCalendar); ;

            BAT.Macros().NavigateTo().CustomPage("~/" + pageTitle.ToLower(), true, this.Culture);
            BATFeather.Wrappers().Frontend().Events().EventsWrapper().ChangeSchedulerView(SchedulerViewTypes.Week);
            Assert.AreEqual(BATFeather.Wrappers().Frontend().Events().EventsWrapper().GetSelectedSchedulerView(), weekSchedulerView);
            this.EventVerification(baseUpcomingInOneDayEventTitle, baseUpcomingInOneDayEventStartDate, baseUpcomingInOneDayEventEndDate, pageTitle, activeCalendar);
        }

        /// <summary>
        /// Test Method that provides test steps for VerifyPastCurrentUpcomingEvents_Scheduler_MvcPage_Agenda_View UI Test.
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.SitefinityTeam8),
        TestCategory(FeatherTestCategories.PagesAndContent),
        TestCategory(FeatherTestCategories.Events)]
        public void VerifyPastCurrentUpcomingEvents_Scheduler_MvcPage_Agenda_View()
        {
            ////Agenda view verification
            BAT.Macros().NavigateTo().CustomPage("~/" + pageTitle.ToLower(), true, this.Culture);
            BATFeather.Wrappers().Frontend().Events().EventsWrapper().ChangeSchedulerView(SchedulerViewTypes.Agenda);
            Assert.AreEqual(BATFeather.Wrappers().Frontend().Events().EventsWrapper().GetSelectedSchedulerView(), agendaSchedulerView);
            var expectedUpcomingEventTitle = BATFeather.Wrappers().Frontend().Events().EventsWrapper().GetEventTitleInScheduler(baseUpcomingInOneDayEventStartDate);
            Assert.AreEqual(expectedUpcomingEventTitle, baseUpcomingInOneDayEventTitle);
            this.EventVerification(currentEventTitle, currentEventStartDate, currentEventEndDate, pageTitle, activeCalendar);

            BAT.Macros().NavigateTo().CustomPage("~/" + pageTitle.ToLower(), true, this.Culture);
            BATFeather.Wrappers().Frontend().Events().EventsWrapper().ChangeSchedulerView(SchedulerViewTypes.Agenda);
            Assert.AreEqual(BATFeather.Wrappers().Frontend().Events().EventsWrapper().GetSelectedSchedulerView(), agendaSchedulerView);
            BATFeather.Wrappers().Frontend().Events().EventsWrapper().NavigateToNextDay();
            Assert.AreEqual(expectedUpcomingEventTitle, baseUpcomingInOneDayEventTitle);
            this.EventVerification(baseUpcomingInOneDayEventTitle, baseUpcomingInOneDayEventStartDate, baseUpcomingInOneDayEventEndDate, pageTitle, activeCalendar);

            BAT.Macros().NavigateTo().CustomPage("~/" + pageTitle.ToLower(), true, this.Culture);
            BATFeather.Wrappers().Frontend().Events().EventsWrapper().ChangeSchedulerView(SchedulerViewTypes.Agenda);
            Assert.AreEqual(BATFeather.Wrappers().Frontend().Events().EventsWrapper().GetSelectedSchedulerView(), agendaSchedulerView);
            BATFeather.Wrappers().Frontend().Events().EventsWrapper().NavigateToPreviousDay();
            Assert.AreEqual(expectedUpcomingEventTitle, baseUpcomingInOneDayEventTitle);
            var expectedCurrentEventTitle = BATFeather.Wrappers().Frontend().Events().EventsWrapper().GetEventTitleInScheduler(currentEventStartDate);
            Assert.AreEqual(expectedCurrentEventTitle, currentEventTitle);
            this.EventVerification(basePastInOneDayEventTitle, basePastInOneDayEventStartDate, basePastInOneDayEventEndDate, pageTitle, activeCalendar);
        }

        /// <summary>
        /// Test Method that provides test steps for VerifyPastCurrentUpcomingEvents_Scheduler_MvcPage_Timeline_View UI Test.
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.SitefinityTeam8),
        TestCategory(FeatherTestCategories.PagesAndContent),
        TestCategory(FeatherTestCategories.Events)]
        public void VerifyPastCurrentUpcomingEvents_Scheduler_MvcPage_Timeline_View()
        {
            ////Timeline view verification
            BAT.Macros().NavigateTo().CustomPage("~/" + pageTitle.ToLower(), true, this.Culture);
            BATFeather.Wrappers().Frontend().Events().EventsWrapper().ChangeSchedulerView(SchedulerViewTypes.Timeline);
            Assert.AreEqual(BATFeather.Wrappers().Frontend().Events().EventsWrapper().GetSelectedSchedulerView(), timelineSchedulerView);
            this.VerifyEventDateInDayView(currentEventStartDate);
            this.EventVerification(currentEventTitle, currentEventStartDate, currentEventEndDate, pageTitle, activeCalendar);

            BAT.Macros().NavigateTo().CustomPage("~/" + pageTitle.ToLower(), true, this.Culture);
            BATFeather.Wrappers().Frontend().Events().EventsWrapper().ChangeSchedulerView(SchedulerViewTypes.Timeline);
            BATFeather.Wrappers().Frontend().Events().EventsWrapper().NavigateToPreviousDay();
            this.VerifyEventDateInDayView(basePastInOneDayEventStartDate);
            this.EventVerification(basePastInOneDayEventTitle, basePastInOneDayEventStartDate, basePastInOneDayEventEndDate, pageTitle, activeCalendar);

            BAT.Macros().NavigateTo().CustomPage("~/" + pageTitle.ToLower(), true, this.Culture);
            BATFeather.Wrappers().Frontend().Events().EventsWrapper().ChangeSchedulerView(SchedulerViewTypes.Timeline);
            BATFeather.Wrappers().Frontend().Events().EventsWrapper().NavigateToNextDay();
            this.VerifyEventDateInDayView(baseUpcomingInOneDayEventStartDate);
            this.EventVerification(baseUpcomingInOneDayEventTitle, baseUpcomingInOneDayEventStartDate, baseUpcomingInOneDayEventEndDate, pageTitle, activeCalendar);
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
            var parsedDate1 = DateTime.Parse(eventstartDateTime);
            var expectedUrlDate1 = String.Format(CultureInfo.InvariantCulture, "{0:d MMMM, yyyy, h mm tt}", parsedDate1);
            var parsedDate2 = DateTime.Parse(eventEndDateTime);
            var expectedUrlDate2 = String.Format(CultureInfo.InvariantCulture, "{0:h mm tt}", parsedDate2);
            var expectedUrlDate = expectedUrlDate1 + "-" + expectedUrlDate2;
            return expectedUrlDate;
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
        /// Verifies event title in scheduler's view
        /// </summary>
        /// <param name="eventTitle">Event Title</param>
        /// <param name="eventStartDate">Event Start Date</param>
        /// <param name="eventEndDate">Event End Date</param>
        /// <param name="pageTitle">Page Title</param>
        /// <param name="activeCalendar">Active Calendar</param>
        private void EventVerification(string eventTitle, string eventStartDate, string eventEndDate, string pageTitle, string activeCalendar)
        {
            var expectedEventTitle = BATFeather.Wrappers().Frontend().Events().EventsWrapper().GetEventTitleInScheduler(eventStartDate);
            Assert.AreEqual(expectedEventTitle, eventTitle);
            BATFeather.Wrappers().Frontend().Events().EventsWrapper().OpenEventsDetailsInScheduler(eventTitle);
            var expectedEventTitleInDetailsView = BATFeather.Wrappers().Frontend().Events().EventsWrapper().GetEventTitleInDetailsView();
            Assert.AreEqual(expectedEventTitleInDetailsView, eventTitle);
            var eventUrlDate = this.ParseUrlDate(eventStartDate);
            BATFeather.Wrappers().Frontend().Events().EventsWrapper().VerifyDetailsEventsPageUrl(pageTitle, activeCalendar, eventUrlDate, eventTitle);
            var expectedEventDateDetailsView = this.ParseEventDateDetailsView(eventStartDate, eventEndDate);
            var actualEventDateDetailsView = BATFeather.Wrappers().Frontend().Events().EventsWrapper().GetEventDateTimeInDetailsView();
            Assert.AreEqual(expectedEventDateDetailsView, actualEventDateDetailsView);
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
    }
}
