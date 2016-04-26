using System;
using System.Collections.Generic;
using Feather.Widgets.TestUI.Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FeatherWidgets.TestUI.TestCases.Events
{
    /// <summary>
    /// FilterByAllEvents test class.
    /// </summary>
    [TestClass]
    public class FilterByAllEvents_ : FeatherTestCase
    {
        /// <summary>
        /// UI test FilterByAllEvents
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.SitefinityTeam7),
        TestCategory(FeatherTestCategories.Events)]
        public void FilterByAllEvents()
        {
            BAT.Macros().NavigateTo().Pages(this.Culture);
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(PageName);

            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().AddMvcWidgetHybridModePage(WidgetName);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(WidgetName);
            BATFeather.Wrappers().Backend().Widgets().SelectorsWrapper().SelectAllItemsRadioButton();
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SaveChanges();

            var expectedEvents = new List<string>() { EventsTestsCommon.BaseEventTitle, EventsTestsCommon.BasePastEventTitle, EventsTestsCommon.BaseUpcomingEventTitle, EventsTestsCommon.BaseAllDayEventTitle, EventsTestsCommon.BaseRepeatEventTitle };
            var unexpectedEvents = new List<string>() { EventsTestsCommon.BaseDraftEventTitle };

            expectedEvents.ForEach(e => BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().CheckWidgetContent(WidgetName, e));
            unexpectedEvents.ForEach(e => BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().CheckWidgetContentForNotExistingContent(WidgetName, e));

            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();
            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower(), true, this.Culture);
            
            Assert.IsTrue(BATFeather.Wrappers().Frontend().Events().EventsWrapper().AreEventTitlesPresentOnThePageFrontend(expectedEvents));
            Assert.IsFalse(BATFeather.Wrappers().Frontend().Events().EventsWrapper().AreEventTitlesPresentOnThePageFrontend(unexpectedEvents));
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
        /// Performs clean up and clears all data created by the test.
        /// </summary>
        protected override void ServerCleanup()
        {
            BAT.Arrange(this.TestName).ExecuteTearDown();
        }

        private const string PageName = "FilterByAllEvents";
        private const string WidgetName = "Events";
    }
}