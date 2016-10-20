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
    ///
    /// </summary>
    [TestClass]
    public class VerifyTwoCalendarsWidgetsOnPage_ : FeatherTestCase
    {
        /// <summary>
        /// Test Method that provides test steps for VerifyTwoCalendarsWidgetsOnPage_ UI Test.
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.SitefinityTeam8)]
        public void VerifyTwoCalendarsWidgetsOnPage()
        {
            BAT.Macros().NavigateTo().Pages(this.Culture);
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(PageTitle);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().AddWidgetToPlaceHolderPureMvcMode(WidgetName);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(WidgetName);
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SelectWhichItemsToDisplay(2);
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().ClickSelectButton();
            BATFeather.Wrappers().Backend().Widgets().SelectorsWrapper().SelectItemsInFlatSelector(Event2Title);
            BATFeather.Wrappers().Backend().Widgets().SelectorsWrapper().DoneSelecting();
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SaveChanges();
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().CheckWidgetContent(WidgetName, Event2Title);
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();

            BAT.Macros().NavigateTo().CustomPage("~/" + PageTitle.ToLower(), true, this.Culture);
            var EventTitle1 = BATFeather.Wrappers().Frontend().Events().EventsWrapper().GetEventTitleInScheduler(event1Id);
            Assert.AreEqual(EventTitle1, Event1Title);
            this.VerifyEventVisibilityInCurrentView(event1Id, 1);
            var EventTitle2 = BATFeather.Wrappers().Frontend().Events().EventsWrapper().GetEventTitleInScheduler(event2Id);
            Assert.AreEqual(EventTitle2, Event2Title);
            this.VerifyEventVisibilityInCurrentView(event2Id, 2);
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

        private void VerifyEventVisibilityInCurrentView(string eventId, int expectedCount)
        {
            var list = BATFeather.Wrappers().Frontend().Events().EventsWrapper().GetVisibleEventInCurrentView(eventId, false);
            Assert.IsTrue(list.Count() == expectedCount, "The event is not visible");
        }

        private const string PageTitle = "EventsPage";
        private const string Calendar1Title = "Calendar1";
        private const string Event1Title = "Event1Title";
        private const string Event2Title = "Event2Title";
        private const string Calendar2Title = "Calendar2";
        private string event1Id = string.Empty;
        private string event2Id = string.Empty;
        private const string WidgetName = "Calendar";
    }
}
