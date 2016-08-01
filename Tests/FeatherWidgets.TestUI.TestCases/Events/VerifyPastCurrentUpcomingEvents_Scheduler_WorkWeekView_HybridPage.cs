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
    /// Create a page with MVC Page Template and add Mvc Events widget with Scheduler view.
    /// Verify events items that show in preview mode, publish the page. 
    /// Verify event items (past, current, upcoming) in Work Week view.
    /// </summary>
    [TestClass]
    public class VerifyPastCurrentUpcomingEvents_Scheduler_WorkWeekView_HybridPage_ : FeatherTestCase
    {
        /// <summary>
        /// Test Method that provides test steps for VerifyPastCurrentUpcomingEvents_Scheduler_WorkWeekView_HybridPage_ UI Test.
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.SitefinityTeam8),
        TestCategory(FeatherTestCategories.PagesAndContent),
        TestCategory(FeatherTestCategories.Events)]
        public void VerifyPastCurrentUpcomingEvents_Scheduler_WorkWeekView_HybridPage()
        {
            BAT.Macros().NavigateTo().Pages(this.Culture);
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(pageTitle);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget("Events");
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().ActivateSchedulerMode();
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();

            BAT.Macros().NavigateTo().CustomPage("~/" + pageTitle.ToLower(), true, this.Culture);
            BATFeather.Wrappers().Frontend().Events().EventsWrapper().ChangeSchedulerView(SchedulerViewTypes.WorkWeek);
            BATFeather.Wrappers().Frontend().Events().EventsWrapper().NavigateToDateInSchedulerWorkWeekView(DateTime.Now, targetWorkWeek);
            Assert.AreEqual(BATFeather.Wrappers().Frontend().Events().EventsWrapper().GetSelectedSchedulerView(), workweekSchedulerView);
            this.EventVerification(currentEventTitle, currentEventStartDate, currentEventEndDate, pageTitle, activeCalendar);

            BAT.Macros().NavigateTo().CustomPage("~/" + pageTitle.ToLower(), true, this.Culture);
            BATFeather.Wrappers().Frontend().Events().EventsWrapper().ChangeSchedulerView(SchedulerViewTypes.WorkWeek);
            BATFeather.Wrappers().Frontend().Events().EventsWrapper().NavigateToDateInSchedulerWorkWeekView(DateTime.Now, targetWorkWeek);
            Assert.AreEqual(BATFeather.Wrappers().Frontend().Events().EventsWrapper().GetSelectedSchedulerView(), workweekSchedulerView);
            this.EventVerification(basePastInOneDayEventTitle, basePastInOneDayEventStartDate, basePastInOneDayEventEndDate, pageTitle, activeCalendar);

            BAT.Macros().NavigateTo().CustomPage("~/" + pageTitle.ToLower(), true, this.Culture);
            BATFeather.Wrappers().Frontend().Events().EventsWrapper().ChangeSchedulerView(SchedulerViewTypes.WorkWeek);
            BATFeather.Wrappers().Frontend().Events().EventsWrapper().NavigateToDateInSchedulerWorkWeekView(DateTime.Now, targetWorkWeek);
            Assert.AreEqual(BATFeather.Wrappers().Frontend().Events().EventsWrapper().GetSelectedSchedulerView(), workweekSchedulerView);
            this.EventVerification(baseUpcomingInOneDayEventTitle, baseUpcomingInOneDayEventStartDate, baseUpcomingInOneDayEventEndDate, pageTitle, activeCalendar);
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
            var expectedUrlDate1 = String.Format(CultureInfo.InvariantCulture, "{0:d MMMM, yyyy, h tt}", parsedDate1);
            var parsedDate2 = DateTime.Parse(eventEndDateTime);
            var expectedUrlDate2 = String.Format(CultureInfo.InvariantCulture, "{0:h tt}", parsedDate2);
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

        private const string currentEventTitle = "CurrentEvent_Title";
        private string currentEventStartDate = "Tue Jul 12 2016 13:00:00";
        private string currentEventEndDate = "Tue Jul 12 2016 14:00:00";

        private const string basePastInOneDayEventTitle = "PastInOneDayEvent_Title";
        private string basePastInOneDayEventStartDate = "Mon Jul 11 2016 13:00:00";
        private string basePastInOneDayEventEndDate = "Mon Jul 11 2016 14:00:00";

        private const string baseUpcomingInOneDayEventTitle = "UpcomingInOneDayEvent_Title";
        private string baseUpcomingInOneDayEventStartDate = "Wed Jul 13 2016 13:00:00";
        private string baseUpcomingInOneDayEventEndDate = "Wed Jul 13 2016 14:00:00";

        private readonly DateTime targetWorkWeek = new DateTime(2016, 7, 12, 0, 0, 0, DateTimeKind.Utc);
        private const string pageTitle = "EventsPage";
        private const string workweekSchedulerView = "Work Week";
        private string activeCalendar = "default-calendar";
    }
}
