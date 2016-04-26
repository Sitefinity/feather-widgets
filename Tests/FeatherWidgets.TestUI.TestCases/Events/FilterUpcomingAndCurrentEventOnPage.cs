using System;
using Feather.Widgets.TestUI.Framework;
using FeatherWidgets.TestUI.TestCases;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FeatherWidgets.TestUI.TestCases.Events
{
    /// <summary>
    /// FilterUpcomingAndCurrentEventOnPage_ test class.
    /// </summary>
    [TestClass]
    public class FilterUpcomingAndCurrentEventOnPage_ : FeatherTestCase
    {
        /// <summary>
        /// UI test FilterUpcomingAndCurrentEventOnPage
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.SitefinityTeam7),
        TestCategory(FeatherTestCategories.PagesAndContent),
        TestCategory(FeatherTestCategories.Events),
        TestCategory(FeatherTestCategories.Selectors)]
        public void FilterUpcomingAndCurrentEventOnPage()
        {
            
            BAT.Macros().NavigateTo().Pages(this.Culture);
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(PageName);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(WidgetName);
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SelectWhichItemsToDisplay(1);

            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SelectCheckBox(UpcomingDateNameInput);
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SelectCheckBox(CurrentDateNameInput);

            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SaveChanges();
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().CheckWidgetContent(WidgetName, EventsTestsCommon.BaseAllDayEventTitle);
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().CheckWidgetContent(WidgetName, EventsTestsCommon.BaseUpcomingEventTitle);
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().CheckWidgetContent(WidgetName, EventsTestsCommon.BaseRepeatEventTitle);
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();
            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower(), true, this.Culture);
            Assert.IsTrue(BATFeather.Wrappers().Frontend().Events().EventsWrapper().AreEventTitlesPresentOnThePageFrontend(this.eventTitles));
            Assert.IsFalse(BATFeather.Wrappers().Frontend().Events().EventsWrapper().AreEventTitlesPresentOnThePageFrontend(new string[] { EventsTestsCommon.BasePastEventTitle }));
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

        private const string PageName = "EventsPage";
        private const string WidgetName = "Events";
        private const string WhichEventsToDisplay = "Selected events";
        private readonly string[] eventTitles = new string[] { EventsTestsCommon.BaseAllDayEventTitle, EventsTestsCommon.BaseUpcomingEventTitle, EventsTestsCommon.BaseRepeatEventTitle };
        private const string UpcomingDateNameInput = "sfUpcomingInput";
        private const string CurrentDateNameInput = "curentEventsInput";
    }
}