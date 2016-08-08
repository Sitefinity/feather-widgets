using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Feather.Widgets.TestUI.Framework;
using Feather.Widgets.TestUI.Framework.Framework.Wrappers.Frontend.Events;
using System.Globalization;

namespace FeatherWidgets.TestUI.TestCases.Events
{
    /// <summary>
    /// Create all day event in 1 day.
    /// Verify that it is displayed in Day\Weekk\Month\Agenda\Timeline view
    /// </summary>
    [TestClass]
    public class VerifyAllDayEventsInOneDay_SchedulerView_ : FeatherTestCase
    {
        /// <summary>
        /// Test Method that provides test steps for VerifyAllDayEventsInOneDay_SchedulerView_ UI Test.
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.SitefinityTeam8),
        TestCategory(FeatherTestCategories.PagesAndContent),
        TestCategory(FeatherTestCategories.Events)]
        public void VerifyAllDayEventsInOneDay_SchedulerView()
        {
            BAT.Macros().NavigateTo().Pages(this.Culture);
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(pageTitle);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget("Events");
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().ActivateSchedulerMode();
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();
            BAT.Macros().NavigateTo().CustomPage("~/" + pageTitle.ToLower(), true, this.Culture);

            //Event1
            BATFeather.Wrappers().Frontend().Events().EventsWrapper().NavigateToDateInSchedulerMonthView(DateTime.Now, event1Start);
            this.VerifyAllDayEventVisibilityInAllView(event1Id);

            //Event2
            BAT.Macros().NavigateTo().CustomPage("~/" + pageTitle.ToLower(), true, this.Culture);
            BATFeather.Wrappers().Frontend().Events().EventsWrapper().NavigateToDateInSchedulerMonthView(DateTime.Now, event2Start);
            this.VerifyAllDayEventVisibilityInAllView(event2Id);
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
        private void VerifyAllDayEventVisibilityInCurrentView(string eventId)
        {
            var list = BATFeather.Wrappers().Frontend().Events().EventsWrapper().GetVisibleEventInCurrentView(eventId, true);
            Assert.IsTrue(list.Count() == 1, "The event is not visible");
        }

        /// <summary>
        /// Verify all day event visibility in all views
        /// </summary>
        /// <param name="eventId">Event ID</param>
        private void VerifyAllDayEventVisibilityInAllView(string eventId)
        {
            //Month view
            this.VerifyAllDayEventVisibilityInCurrentView(eventId);

            //Day view
            BATFeather.Wrappers().Frontend().Events().EventsWrapper().OpenTargetDayInDayView("10");
            this.VerifyAllDayEventVisibilityInCurrentView(eventId);

            //Week view
            BATFeather.Wrappers().Frontend().Events().EventsWrapper().ChangeSchedulerView(SchedulerViewTypes.Week);
            this.VerifyAllDayEventVisibilityInCurrentView(eventId);

            //Agenda view
            BATFeather.Wrappers().Frontend().Events().EventsWrapper().ChangeSchedulerView(SchedulerViewTypes.Agenda);
            this.VerifyAllDayEventVisibilityInCurrentView(eventId);

            //Timeline vies
            BATFeather.Wrappers().Frontend().Events().EventsWrapper().ChangeSchedulerView(SchedulerViewTypes.Timeline);
            this.VerifyAllDayEventVisibilityInCurrentView(eventId);
        }

        private const string pageTitle = "EventsPage";
        private const string Event1Title = "Event1Title";
        private readonly DateTime event1Start = new DateTime(2016, 1, 10, 10, 0, 0);

        private const string Event2Title = "Event2Title";
        private readonly DateTime event2Start = new DateTime(2016, 4, 10, 10, 0, 0);

        private string event1Id = string.Empty;
        private string event2Id = string.Empty;
    }
}
