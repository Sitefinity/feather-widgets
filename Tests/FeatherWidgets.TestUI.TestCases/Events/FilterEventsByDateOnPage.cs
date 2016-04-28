using System;
using System.Linq;
using Feather.Widgets.TestUI.Framework;
using FeatherWidgets.TestUI.TestCases;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace FeatherWidgets.TestUI.TestCases.Events
{
    /// <summary>
    /// FilterEventsByDateOnPage_ test class.
    /// </summary>
    [TestClass]
    public class FilterEventsByDateOnPage_ : FeatherTestCase
    {
        /// <summary>
        /// UI test FilterCurrentEventsOnPage
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.SitefinityTeam7),
        TestCategory(FeatherTestCategories.PagesAndContent),
        TestCategory(FeatherTestCategories.Events),
        TestCategory(FeatherTestCategories.Selectors)]
        public void FilterCurrentEventsOnPage()
        {
            this.FilterByDate(FilterEventsByDateOnPage_.CurrentDateNameInput, this.currentEventTitles);
        }

        /// <summary>
        /// UI test FilterUpcomingEventsOnPage
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.SitefinityTeam7),
        TestCategory(FeatherTestCategories.PagesAndContent),
        TestCategory(FeatherTestCategories.Events),
        TestCategory(FeatherTestCategories.Selectors)]
        public void FilterUpcomingEventsOnPage()
        {
            this.FilterByDate(FilterEventsByDateOnPage_.UpcomingDateNameInput, this.upcomingEventTitles);
        }

        /// <summary>
        /// UI test FilterPastEventsOnPage
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.SitefinityTeam7),
        TestCategory(FeatherTestCategories.PagesAndContent),
        TestCategory(FeatherTestCategories.Events),
        TestCategory(FeatherTestCategories.Selectors)]
        public void FilterPastEventsOnPage()
        {
            this.FilterByDate(FilterEventsByDateOnPage_.PastDateNameInput, this.pastEventTitles);
        }

        /// <summary>
        /// UI test FilterUpcomingEventsSelectDateOnPage
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.SitefinityTeam7),
        TestCategory(FeatherTestCategories.PagesAndContent),
        TestCategory(FeatherTestCategories.Events),
        TestCategory(FeatherTestCategories.Selectors)]
        public void FilterUpcomingEventsSelectDateOnPage()
        {
            var expectedEventTitles = new string[] { EventsTestsCommon.BaseUpcomingInThreeDaysEventTitle };

            Action selectUpcomingDate = () =>
            {
                BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().ClickSelectButtonByDate();
                BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SelectDisplayItemsPublishedIn("Custom range...");
                BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SetFromDateByTyping(2);
                BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().AddHour("10", true);
                BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().AddMinute("20", true);
                BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SetToDateByTyping(4);
                BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().AddHour("13", false);
                BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().AddMinute("40", false);
                BATFeather.Wrappers().Backend().Widgets().SelectorsWrapper().DoneSelecting();
            };

            this.FilterByDate(FilterEventsByDateOnPage_.UpcomingDateNameInput, expectedEventTitles, selectUpcomingDate);
        }

        /// <summary>
        /// UI test FilterPastEventsSelectDateOnPage
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.SitefinityTeam7),
        TestCategory(FeatherTestCategories.PagesAndContent),
        TestCategory(FeatherTestCategories.Events),
        TestCategory(FeatherTestCategories.Selectors)]
        public void FilterPastEventsSelectDateOnPage()
        {
            var expectedEventTitles = new string[] { EventsTestsCommon.BasePastInTwoDaysEventTitle };

            Action selectEventEndDateRange = () =>
            {
                BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().ClickSelectButtonByDate();
                BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SelectDisplayItemsPublishedIn("Custom range...");
                BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SetFromDateByTyping(-3);
                BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().AddHour("10", true);
                BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().AddMinute("20", true);
                BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SetToDateByTyping(-1);
                BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().AddHour("13", false);
                BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().AddMinute("40", false);
                BATFeather.Wrappers().Backend().Widgets().SelectorsWrapper().DoneSelecting();
            };

            this.FilterByDate(FilterEventsByDateOnPage_.PastDateNameInput, expectedEventTitles, selectEventEndDateRange);
        }

        /// <summary>
        /// Performs Server Setup and prepare the system with needed data.
        /// </summary>
        protected override void ServerSetup()
        {
            BAT.Macros().User().EnsureAdminLoggedIn();
            BAT.Arrange(EventsTestsCommon.BaseArrangementName).ExecuteSetUp();
        }

        /// <summary>
        /// Performs clean up and clears all data created by the test.
        /// </summary>
        protected override void ServerCleanup()
        {
            BAT.Arrange(EventsTestsCommon.BaseArrangementName).ExecuteTearDown();
        }

        private void FilterByDate(string filterCheckboxId, IList<string> expectedEventTitles, Action narrowFilterByDate = null)
        {
            BAT.Macros().NavigateTo().Pages(this.Culture);
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(PageName);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(WidgetName);
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SelectWhichItemsToDisplay(1);
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SelectCheckBox(filterCheckboxId);

            if (narrowFilterByDate!= null)
                narrowFilterByDate();

            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SaveChanges();

            foreach (var eventTitle in expectedEventTitles)
            {
                BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().CheckWidgetContent(WidgetName, eventTitle);
            }
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();
            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower(), true, this.Culture);
            Assert.IsTrue(BATFeather.Wrappers().Frontend().Events().EventsWrapper().AreEventTitlesPresentOnThePageFrontend(expectedEventTitles));
            var unexpectedEventTitles = EventsTestsCommon.ExpectedEvents.Except(expectedEventTitles);
            Assert.IsTrue(BATFeather.Wrappers().Frontend().Events().EventsWrapper().AreEventTitlesMissingOnThePageFrontend(unexpectedEventTitles));
        }

        private const string PageName = "EventsPage";
        private const string WidgetName = "Events";
        private const string WhichEventsToDisplay = "Selected events";
        private readonly string[] currentEventTitles = new string[] { EventsTestsCommon.BaseAllDayEventTitle, EventsTestsCommon.BaseRepeatEventTitle };
        private readonly string[] pastEventTitles = new string[] { EventsTestsCommon.BasePastInFourDaysEventTitle, EventsTestsCommon.BasePastInTwoDaysEventTitle };
        private readonly string[] upcomingEventTitles = new string[] { EventsTestsCommon.BaseEventTitle, EventsTestsCommon.BaseUpcomingInOneDayEventTitle, EventsTestsCommon.BaseUpcomingInThreeDaysEventTitle };
        private const string PastDateNameInput = "sfPastInput";
        private const string UpcomingDateNameInput = "sfUpcomingInput";
        private const string CurrentDateNameInput = "curentEventsInput";
    }
}