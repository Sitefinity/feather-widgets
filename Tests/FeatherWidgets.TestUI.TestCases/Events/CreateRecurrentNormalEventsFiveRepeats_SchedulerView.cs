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
    /// Create recurrent normal event with five repeats and verify its details in Scheduler.
    /// </summary>
    [TestClass]
    public class CreateRecurrentNormalEventsFiveRepeats_SchedulerView_ : FeatherTestCase
    {
        /// <summary>
        /// Test Method that provides test steps for CreateRecurrentNormalEventsFiveRepeats_SchedulerView_ UI Test.
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.SitefinityTeam8),
        TestCategory(FeatherTestCategories.PagesAndContent),
        TestCategory(FeatherTestCategories.Events)]
        public void CreateRecurrentNormalEventsFiveRepeats_SchedulerView()
        {
            BAT.Macros().NavigateTo().Pages(this.Culture);
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(pageTitle);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget("Events");
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().ActivateSchedulerMode();
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();
            BAT.Macros().NavigateTo().CustomPage("~/" + pageTitle.ToLower(), true, this.Culture);

            //Verify Event1 occurrence count and start/end date for firs and last event occurrence
            BATFeather.Wrappers().Frontend().Events().EventsWrapper().NavigateToDateInSchedulerMonthView(DateTime.Now, event1Start);
            var actualEvent1Occurence = BATFeather.Wrappers().Frontend().Events().EventsWrapper().EventOccurenceCountInCurrentView(event1Id);
            Assert.AreEqual(expectedEventOccurence, actualEvent1Occurence);
            BATFeather.Wrappers().Frontend().Events().EventsWrapper().VerifyEventDate(event1Id, expectedStartDateFirstOccurrenceEvent1, true, 0);
            BATFeather.Wrappers().Frontend().Events().EventsWrapper().VerifyEventDate(event1Id, expectedEndDateFirstOccurrenceEvent1, false, 0);
            BATFeather.Wrappers().Frontend().Events().EventsWrapper().VerifyEventDate(event1Id, expectedStartDateLastOccurrenceEvent1, true, 4);
            BATFeather.Wrappers().Frontend().Events().EventsWrapper().VerifyEventDate(event1Id, expectedEndDateLastOccurrenceEvent1, false, 4);

            //Verify Event2 occurrence count and start/end date for firs and last event occurrence
            BATFeather.Wrappers().Frontend().Events().EventsWrapper().NavigateToDateInSchedulerMonthView(event1Start, event2Start);
            var actualEvent2Occurence = BATFeather.Wrappers().Frontend().Events().EventsWrapper().EventOccurenceCountInCurrentView(event2Id);
            Assert.AreEqual(expectedEventOccurence, actualEvent2Occurence);
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
        string expectedStartDateFirstOccurrenceEvent1 = "Sun Jan 10 2016 10:00:00";
        string expectedEndDateFirstOccurrenceEvent1 = "Sun Jan 10 2016 11:00:00";
        string expectedStartDateLastOccurrenceEvent1 = "Thu Jan 14 2016 10:00:00";
        string expectedEndDateLastOccurrenceEvent1 = "Thu Jan 14 2016 11:00:00";
        

        private const string Event2Title = "Event2Title";
        private string event2Id = string.Empty;
        private readonly DateTime event2Start = new DateTime(2016, 4, 10, 10, 0, 0);
        string expectedStartDateFirstOccurrenceEvent2 = "Sun Apr 10 2016 10:00:00";
        string expectedEndDateFirstOccurrenceEvent2 = "Sun Apr 10 2016 11:00:00";
        string expectedStartDateLastOccurrenceEvent2 = "Thu Apr 14 2016 10:00:00";
        string expectedEndDateLastOccurrenceEvent2 = "Thu Apr 14 2016 11:00:00";
    }
}
