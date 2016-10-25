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
    /// Creates 3 calendars with 1 event each.
    /// Filter by Calendar and verify visible events in each view of Calendar widget.
    /// </summary>
    [TestClass]
    public class FilterEventsByCalendar_ : FeatherTestCase
    {
        /// <summary>
        /// Test Method that provides test steps for FilterEventsByCalendar_ UI Test.
        /// </summary>
        [TestMethod, 
        Owner(FeatherTeams.SitefinityTeam8),
        TestCategory(FeatherTestCategories.PagesAndContent),
        TestCategory(FeatherTestCategories.Events)]
        public void FilterEventsByCalendar()
        {
            BAT.Macros().NavigateTo().CustomPage("~/" + PageTitle.ToLower(), true, this.Culture);
            BATFeather.Wrappers().Frontend().Events().EventsWrapper().FilterEventsByCalendar(Calendar1Title);
            this.VerifyEventVisibilityInAllViews(1, 0, 0, false);

            BAT.Macros().NavigateTo().CustomPage("~/" + PageTitle.ToLower(), true, this.Culture);
            BATFeather.Wrappers().Frontend().Events().EventsWrapper().FilterEventsByCalendar(Calendar2Title);
            this.VerifyEventVisibilityInAllViews(0, 1, 0, false);

            BAT.Macros().NavigateTo().CustomPage("~/" + PageTitle.ToLower(), true, this.Culture);
            BATFeather.Wrappers().Frontend().Events().EventsWrapper().FilterEventsByCalendar(Calendar3Title);
            this.VerifyEventVisibilityInAllViews(0, 0, 1, false);
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
            event3Id = result.Result.Values["event3Id"];
        }

        /// <summary>
        /// Performs clean up and clears all data created by the test.
        /// </summary>
        protected override void ServerCleanup()
        {
            BAT.Arrange(this.TestName).ExecuteTearDown();
        }

        /// <summary>
        /// Verify event visibility in all views
        /// </summary>
        /// <param name="expectedCountEvent1">expected count of event1 in current view</param>
        /// <param name="expectedCountEvent2">expected count of event2 in current view</param>
        /// <param name="expectedCountEvent3">expected count of event3 in current view</param>
        private void VerifyEventVisibilityInAllViews(int expectedCountEvent1, int expectedCountEvent2, int expectedCountEvent3, bool allDayEvent)
        {
            //Month view
            BATFeather.Wrappers().Frontend().Events().EventsWrapper().VerifyEventVisibilityInCurrentView(event1Id, expectedCountEvent1, allDayEvent);
            BATFeather.Wrappers().Frontend().Events().EventsWrapper().VerifyEventVisibilityInCurrentView(event2Id, expectedCountEvent2, allDayEvent);
            BATFeather.Wrappers().Frontend().Events().EventsWrapper().VerifyEventVisibilityInCurrentView(event3Id, expectedCountEvent3, allDayEvent);

            //Day view
            BATFeather.Wrappers().Frontend().Events().EventsWrapper().ChangeSchedulerView(SchedulerViewTypes.Day);
            BATFeather.Wrappers().Frontend().Events().EventsWrapper().VerifyEventVisibilityInCurrentView(event1Id, expectedCountEvent1, allDayEvent);
            BATFeather.Wrappers().Frontend().Events().EventsWrapper().VerifyEventVisibilityInCurrentView(event2Id, expectedCountEvent2, allDayEvent);
            BATFeather.Wrappers().Frontend().Events().EventsWrapper().VerifyEventVisibilityInCurrentView(event3Id, expectedCountEvent3, allDayEvent);

            //Week view
            BATFeather.Wrappers().Frontend().Events().EventsWrapper().ChangeSchedulerView(SchedulerViewTypes.Week);
            BATFeather.Wrappers().Frontend().Events().EventsWrapper().VerifyEventVisibilityInCurrentView(event1Id, expectedCountEvent1, allDayEvent);
            BATFeather.Wrappers().Frontend().Events().EventsWrapper().VerifyEventVisibilityInCurrentView(event2Id, expectedCountEvent2, allDayEvent);
            BATFeather.Wrappers().Frontend().Events().EventsWrapper().VerifyEventVisibilityInCurrentView(event3Id, expectedCountEvent3, allDayEvent);

            //Agenda view
            BATFeather.Wrappers().Frontend().Events().EventsWrapper().ChangeSchedulerView(SchedulerViewTypes.Agenda);
            BATFeather.Wrappers().Frontend().Events().EventsWrapper().VerifyEventVisibilityInCurrentView(event1Id, expectedCountEvent1, allDayEvent);
            BATFeather.Wrappers().Frontend().Events().EventsWrapper().VerifyEventVisibilityInCurrentView(event2Id, expectedCountEvent2, allDayEvent);
            BATFeather.Wrappers().Frontend().Events().EventsWrapper().VerifyEventVisibilityInCurrentView(event3Id, expectedCountEvent3, allDayEvent);

            //Timeline view
            BATFeather.Wrappers().Frontend().Events().EventsWrapper().ChangeSchedulerView(SchedulerViewTypes.Timeline);
            BATFeather.Wrappers().Frontend().Events().EventsWrapper().VerifyEventVisibilityInCurrentView(event1Id, expectedCountEvent1, allDayEvent);
            BATFeather.Wrappers().Frontend().Events().EventsWrapper().VerifyEventVisibilityInCurrentView(event2Id, expectedCountEvent2, allDayEvent);
            BATFeather.Wrappers().Frontend().Events().EventsWrapper().VerifyEventVisibilityInCurrentView(event3Id, expectedCountEvent3, allDayEvent);
        }

        private const string PageTitle = "EventsPage";
        private const string Calendar1Title = "Calendar1";
        private const string Event1Title = "Event1Title";
        private string event1Id = string.Empty;

        private const string Calendar2Title = "Calendar2";
        private const string Event2Title = "Event2Title";
        private string event2Id = string.Empty;

        private const string Calendar3Title = "Calendar3";
        private const string Event3Title = "Event3Title";
        private string event3Id = string.Empty;
    }
}

