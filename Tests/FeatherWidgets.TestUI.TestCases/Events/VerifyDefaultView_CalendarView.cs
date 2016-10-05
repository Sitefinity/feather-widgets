using System;
using ArtOfTest.WebAii.Core;
using Feather.Widgets.TestUI.Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telerik.Sitefinity.TestUI.Framework.Utilities;

namespace FeatherWidgets.TestUI.TestCases.Events
{
    /// <summary>
    /// Create a page with Calendar widget.
    /// Change default view and verify that the correct default view is displayed.
    /// </summary>
    [TestClass]
    public class VerifyDefaultView_CalendarView_ : FeatherTestCase
    {
        /// <summary>
        /// Test Method that provides test steps for VerifyDefaultView_CalendarView_ UI Test.
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.SitefinityTeam8),
        TestCategory(FeatherTestCategories.PagesAndContent),
        TestCategory(FeatherTestCategories.Events)]
        public void VerifyDefaultView_CalendarView()
        {
            ////Default Month view
            RuntimeSettingsModificator.ExecuteWithClientTimeout(800000, () => BAT.Macros().NavigateTo().CustomPage("~/sitefinity/pages", true, null, new HtmlFindExpression("class=~sfMain")));
            BAT.Macros().NavigateTo().Pages(this.Culture);
            BAT.Macros().NavigateTo().CustomPage("~/" + PageTitle.ToLower(), true, this.Culture);
            Assert.AreEqual(BATFeather.Wrappers().Frontend().Events().EventsWrapper().GetSelectedSchedulerView(), MonthSchedulerView);

            ////Default Week view
            BAT.Macros().NavigateTo().Pages(this.Culture);
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(PageTitle);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(WidgetCalendar);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().OpenListSettingsView();
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SelectOptionInDefaultCalendarViewSelector(WeekSchedulerView);
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SaveChanges();
            var actualDefaultWeekView = BATFeather.Wrappers().Frontend().Events().EventsWrapper().GetSelectedSchedulerView();
            Assert.AreEqual(WeekSchedulerView, actualDefaultWeekView);
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();
            BAT.Macros().NavigateTo().CustomPage("~/" + PageTitle.ToLower(), true, this.Culture);
            Assert.AreEqual(BATFeather.Wrappers().Frontend().Events().EventsWrapper().GetSelectedSchedulerView(), WeekSchedulerView);

            ////Default Work Week view
            BAT.Macros().NavigateTo().Pages(this.Culture);
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(PageTitle);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(WidgetCalendar);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().OpenListSettingsView();
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SelectOptionInDefaultCalendarViewSelector("WorkWeek");
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SaveChanges();
            var actualDefaultWorkWeekView = BATFeather.Wrappers().Frontend().Events().EventsWrapper().GetSelectedSchedulerView();
            Assert.AreEqual(WorkWeekSchedulerView, actualDefaultWorkWeekView);
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();
            BAT.Macros().NavigateTo().CustomPage("~/" + PageTitle.ToLower(), true, this.Culture);
            Assert.AreEqual(BATFeather.Wrappers().Frontend().Events().EventsWrapper().GetSelectedSchedulerView(), WorkWeekSchedulerView);

            ////Default Day view
            BAT.Macros().NavigateTo().Pages(this.Culture);
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(PageTitle);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(WidgetCalendar);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().OpenListSettingsView();
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SelectOptionInDefaultCalendarViewSelector(DaySchedulerView);
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SaveChanges();
            var actualDefaultDayView = BATFeather.Wrappers().Frontend().Events().EventsWrapper().GetSelectedSchedulerView();
            Assert.AreEqual(DaySchedulerView, actualDefaultDayView);
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();
            BAT.Macros().NavigateTo().CustomPage("~/" + PageTitle.ToLower(), true, this.Culture);
            Assert.AreEqual(BATFeather.Wrappers().Frontend().Events().EventsWrapper().GetSelectedSchedulerView(), DaySchedulerView);

            ////Default Agenda view
            BAT.Macros().NavigateTo().Pages(this.Culture);
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(PageTitle);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(WidgetCalendar);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().OpenListSettingsView();
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SelectOptionInDefaultCalendarViewSelector(AgendaSchedulerView);
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SaveChanges();
            var actualDefaultAgendaView = BATFeather.Wrappers().Frontend().Events().EventsWrapper().GetSelectedSchedulerView();
            Assert.AreEqual(AgendaSchedulerView, actualDefaultAgendaView);
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();
            BAT.Macros().NavigateTo().CustomPage("~/" + PageTitle.ToLower(), true, this.Culture);
            Assert.AreEqual(BATFeather.Wrappers().Frontend().Events().EventsWrapper().GetSelectedSchedulerView(), AgendaSchedulerView);

            ////Default Timeline view
            BAT.Macros().NavigateTo().Pages(this.Culture);
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(PageTitle);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(WidgetCalendar);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().OpenListSettingsView();
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SelectOptionInDefaultCalendarViewSelector(TimelineSchedulerView);
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SaveChanges();
            var actualDefaultTimelineView = BATFeather.Wrappers().Frontend().Events().EventsWrapper().GetSelectedSchedulerView();
            Assert.AreEqual(TimelineSchedulerView, actualDefaultTimelineView);
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();
            BAT.Macros().NavigateTo().CustomPage("~/" + PageTitle.ToLower(), true, this.Culture);
            Assert.AreEqual(BATFeather.Wrappers().Frontend().Events().EventsWrapper().GetSelectedSchedulerView(), TimelineSchedulerView);

            ////Default Month view
            BAT.Macros().NavigateTo().Pages(this.Culture);
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(PageTitle);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(WidgetCalendar);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().OpenListSettingsView();
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SelectOptionInDefaultCalendarViewSelector(MonthSchedulerView);
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SaveChanges();
            var actualDefaultMonthView = BATFeather.Wrappers().Frontend().Events().EventsWrapper().GetSelectedSchedulerView();
            Assert.AreEqual(MonthSchedulerView, actualDefaultMonthView);
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();
            BAT.Macros().NavigateTo().CustomPage("~/" + PageTitle.ToLower(), true, this.Culture);
            Assert.AreEqual(BATFeather.Wrappers().Frontend().Events().EventsWrapper().GetSelectedSchedulerView(), MonthSchedulerView);
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

        private const string PageTitle = "EventsPage";
        private const string MonthSchedulerView = "Month";
        private const string DaySchedulerView = "Day";
        private const string WeekSchedulerView = "Week";
        private const string WorkWeekSchedulerView = "Work Week";
        private const string AgendaSchedulerView = "Agenda";
        private const string TimelineSchedulerView = "Timeline";
        private const string WidgetCalendar = "Calendar";
    }
}
