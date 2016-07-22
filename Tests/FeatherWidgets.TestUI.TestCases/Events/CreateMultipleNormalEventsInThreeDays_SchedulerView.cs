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
    /// Create multiple normal event in three days and verify its details in Scheduler.
    /// </summary>
    [TestClass]
    public class CreateMultipleNormalEventsInThreeDays_SchedulerView_ : FeatherTestCase
    {
        /// <summary>
        /// Test Method that provides test steps for CreateMultipleNormalEventsInThreeDays_SchedulerView_ UI Test.
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.SitefinityTeam8),
        TestCategory(FeatherTestCategories.PagesAndContent),
        TestCategory(FeatherTestCategories.Events)]
        public void CreateMultipleNormalEventsInThreeDays_SchedulerView()
        {
            BAT.Macros().NavigateTo().Pages(this.Culture);
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(pageTitle);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget("Events");
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().ActivateSchedulerMode();
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();
            BAT.Macros().NavigateTo().CustomPage("~/" + pageTitle.ToLower(), true, this.Culture);
           
            BATFeather.Wrappers().Frontend().Events().EventsWrapper().NavigateToDateInSchedulerMonthView(DateTime.Now, event1Start);
            var actualEvent1Occurence = BATFeather.Wrappers().Frontend().Events().EventsWrapper().EventOccurenceCountInCurrentView(event1Id);
            Assert.AreEqual(1, actualEvent1Occurence);
            BATFeather.Wrappers().Frontend().Events().EventsWrapper().VerifyEventDate(event1Id, expectedStartDateEvent1, true, 0);
            BATFeather.Wrappers().Frontend().Events().EventsWrapper().VerifyEventDate(event1Id, expectedEndDateEvent1, false, 0);

            BATFeather.Wrappers().Frontend().Events().EventsWrapper().NavigateToDateInSchedulerMonthView(event1Start, event2Start);
            var actualEvent2Occurence = BATFeather.Wrappers().Frontend().Events().EventsWrapper().EventOccurenceCountInCurrentView(event2Id);
            Assert.AreEqual(1, actualEvent2Occurence);
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
        string expectedStartDateEvent1 = "Sun Jan 10 2016 10:00:00";
        string expectedEndDateEvent1 = "Tue Jan 12 2016 11:00:00";
        
        private const string Event2Title = "Event2Title";
        private readonly DateTime event2Start = new DateTime(2016, 4, 10, 10, 0, 0);
        string expectedStartDateEvent2 = "Sun Apr 10 2016 10:00:00";
        string expectedEndDateEvent2 = "Tue Apr 12 2016 11:00:00";

        private string event1Id = string.Empty;
        private string event2Id = string.Empty;
    }
}
