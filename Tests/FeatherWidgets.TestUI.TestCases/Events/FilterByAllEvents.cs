using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Feather.Widgets.TestUI.Framework;

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

            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().CheckWidgetContent(WidgetName, EventsTestsCommon.BaseEventTitle);
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().CheckWidgetContent(WidgetName, EventsTestsCommon.BasePastEventTitle);
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().CheckWidgetContent(WidgetName, EventsTestsCommon.BaseUpcomingEventTitle);
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().CheckWidgetContent(WidgetName, EventsTestsCommon.BaseAllDayEventTitle);
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().CheckWidgetContent(WidgetName, EventsTestsCommon.BaseRepeatEventTitle);
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().CheckWidgetContentForNotExistingContent(WidgetName, EventsTestsCommon.BaseDraftEventTitle);
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();

            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower(), true, this.Culture);

            // Assert.IsTrue(BATFeather.Wrappers().Frontend().IsNewsTitlesPresentOnThePageFrontend(this.newsTitles));
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