using Feather.Widgets.TestUI.Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeatherWidgets.TestUI.TestCases.Events
{
    /// <summary>
    /// Create recurrent normal all day event with no end date and verify its details in Calendar.
    /// </summary>
    [TestClass]
    public class VerifyRecurrentAllDayEventsWithNoEndDate_CalendarView_ : FeatherTestCase
    {
        /// <summary>
        /// Test Method that provides test steps for VerifyRecurrentAllDayEventsWithNoEndDate_CalendarView_ UI Test.
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.SitefinityTeam8),
        TestCategory(FeatherTestCategories.PagesAndContent),
        TestCategory(FeatherTestCategories.Events)]
        public void VerifyRecurrentAllDayEventsWithNoEndDate_CalendarView()
        {
            BAT.Macros().NavigateTo().CustomPage("~/" + pageTitle.ToLower(), true, this.Culture);
            int currentEventYear = Int32.Parse(еventStartDate);
            int targetEventYear = currentEventYear + 5;
            BATFeather.Wrappers().Frontend().Events().EventsWrapper().FastNavigationByYearInCalendarSelector(monthName, currentEventYear, targetEventYear);
            var actualEventOccurence = BATFeather.Wrappers().Frontend().Events().EventsWrapper().EventOccurenceCountInCurrentView(event1Id);
            Assert.AreEqual(expectedEventOccurence, actualEventOccurence);

            var monthList = BATFeather.Wrappers().Frontend().Events().EventsWrapper().GetVisibleEventInCurrentView(event1Id, true);
            Assert.IsTrue(monthList.Count() == expectedEventOccurence, "The event is not visible");
        }

        /// <summary>
        /// Performs Server Setup and prepare the system with needed data.
        /// </summary>
        protected override void ServerSetup()
        {
            BAT.Macros().User().EnsureAdminLoggedIn();
            var result = BAT.Arrange(this.TestName).ExecuteSetUp();
            еventStartDate = result.Result.Values["еventStartTime"];
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
        private string еventStartDate = string.Empty;
        private string event1Id = string.Empty;
        private const string monthName = "Jun";
        private readonly int expectedEventOccurence = 42;
    }
}
