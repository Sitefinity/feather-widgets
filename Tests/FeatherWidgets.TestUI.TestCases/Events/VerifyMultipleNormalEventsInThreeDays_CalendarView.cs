using Feather.Widgets.TestUI.Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.WebAii.Controls.Html;

namespace FeatherWidgets.TestUI.TestCases.Events
{
    /// <summary>
    /// Create multiple normal event in three days and verify its details in Calendar.
    /// </summary>
    [TestClass]
    public class VerifyMultipleNormalEventsInThreeDays_CalendarView_ : FeatherTestCase
    {
        /// <summary>
        /// Test Method that provides test steps for VerifyMultipleNormalEventsInThreeDays_CalendarView_ UI Test.
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.SitefinityTeam8),
        TestCategory(FeatherTestCategories.PagesAndContent),
        TestCategory(FeatherTestCategories.Events)]
        public void VerifyMultipleNormalEventsInThreeDays_CalendarView()
        {
            BAT.Macros().NavigateTo().CustomPage("~/" + pageTitle.ToLower(), true, this.Culture);
           
            BATFeather.Wrappers().Frontend().Events().EventsWrapper().NavigateToDateInSchedulerMonthView(DateTime.Now, event1Start);
            var actualEvent1Occurence = BATFeather.Wrappers().Frontend().Events().EventsWrapper().EventOccurenceCountInCurrentView(event1Id);
            Assert.AreEqual(1, actualEvent1Occurence);

            var localTimeZone = BATFeather.Wrappers().Frontend().Events().EventsWrapper().LocalTimeZoneOffset();
            TimeZoneInfo timezoneInfo = TimeZoneInfo.FindSystemTimeZoneById(timezoneId);

            string expectedStartDateEvent1 = TimeZoneInfo.ConvertTime(this.event1Start.ToUniversalTime(), timezoneInfo).ToString("s") + ".000Z";
            string expectedEndDateEvent1 = TimeZoneInfo.ConvertTime(this.event1End.ToUniversalTime(), timezoneInfo).ToString("s") + ".000Z";

            BATFeather.Wrappers().Frontend().Events().EventsWrapper().VerifyEventDate(event1Id, expectedStartDateEvent1, true, 0);
            BATFeather.Wrappers().Frontend().Events().EventsWrapper().VerifyEventDate(event1Id, expectedEndDateEvent1, false, 0);

            BATFeather.Wrappers().Frontend().Events().EventsWrapper().NavigateToDateInSchedulerMonthView(event1Start, event2Start);
            var actualEvent2Occurence = BATFeather.Wrappers().Frontend().Events().EventsWrapper().EventOccurenceCountInCurrentView(event2Id);
            Assert.AreEqual(1, actualEvent2Occurence);

            string expectedStartDateEvent2 = TimeZoneInfo.ConvertTime(this.event2Start.ToUniversalTime(), timezoneInfo).ToString("s") + ".000Z";
            string expectedEndDateEvent2 = TimeZoneInfo.ConvertTime(this.event2End.ToUniversalTime(), timezoneInfo).ToString("s") + ".000Z";

            BATFeather.Wrappers().Frontend().Events().EventsWrapper().VerifyEventDate(event2Id, expectedStartDateEvent2, true, 0);
            BATFeather.Wrappers().Frontend().Events().EventsWrapper().VerifyEventDate(event2Id, expectedEndDateEvent2, false, 0);
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
        private const string Event1Title = "Event1Title";
        private readonly DateTime event1Start = new DateTime(2016, 1, 10, 10, 0, 0);
        private readonly DateTime event1End = new DateTime(2016, 1, 12, 11, 0, 0);
        
        private const string Event2Title = "Event2Title";
        private readonly DateTime event2Start = new DateTime(2016, 4, 10, 10, 0, 0);
        private readonly DateTime event2End = new DateTime(2016, 4, 12, 11, 0, 0);
 
        private string event1Id = string.Empty;
        private string event2Id = string.Empty;
        private string timezoneId = string.Empty;
    }
}
