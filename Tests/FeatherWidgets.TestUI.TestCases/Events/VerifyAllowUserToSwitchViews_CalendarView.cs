using System;
using ArtOfTest.WebAii.Core;
using Feather.Widgets.TestUI.Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telerik.Sitefinity.TestUI.Framework.Utilities;

namespace FeatherWidgets.TestUI.TestCases.Events
{
    /// <summary>
    /// Create a page with Calendar widget.
    /// Change default view and verify that allow user to switch views work correct.
    /// </summary>
    [TestClass]
    public class VerifyAllowUserToSwitchViews_CalendarView_ : FeatherTestCase
    {
        /// <summary>
        /// Test Method that provides test steps for VerifyAllowUserToSwitchViews_CalendarView_ UI Test.
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.SitefinityTeam8),
        TestCategory(FeatherTestCategories.PagesAndContent),
        TestCategory(FeatherTestCategories.Events)]
        public void VerifyAllowUserToSwitchViews_CalendarView()
        {
            RuntimeSettingsModificator.ExecuteWithClientTimeout(800000, () => BAT.Macros().NavigateTo().CustomPage("~/sitefinity/pages", true, null, new HtmlFindExpression("class=~sfMain")));      
            ////Default Month view
            BAT.Macros().NavigateTo().Pages(this.Culture);
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(PageTitle);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(WidgetCalendar);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().OpenListSettingsView();
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SelectOptionInDefaultCalendarViewSelector(MonthSchedulerView);
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().VerifyDefaultViewOptionsInCalendar(this.defaultViewOptions);
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().UnSelectAllowUsersToSiwtchViewsCheckBoxInCalendarWidget();
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SaveChanges();
            Assert.IsFalse(BATFeather.Wrappers().Frontend().Events().EventsWrapper().IsItemPresent(MonthSchedulerView), MonthSchedulerView + TextNotVisible);
            Assert.IsFalse(BATFeather.Wrappers().Frontend().Events().EventsWrapper().IsItemPresent(WeekSchedulerView), WeekSchedulerView + TextNotVisible);
            Assert.IsFalse(BATFeather.Wrappers().Frontend().Events().EventsWrapper().IsItemPresent(DaySchedulerView), DaySchedulerView + TextNotVisible);
            Assert.IsFalse(BATFeather.Wrappers().Frontend().Events().EventsWrapper().IsItemPresent(WorkWeekSchedulerView), WorkWeekSchedulerView + TextNotVisible);
            Assert.IsFalse(BATFeather.Wrappers().Frontend().Events().EventsWrapper().IsItemPresent(AgendaSchedulerView), AgendaSchedulerView + TextNotVisible);
            Assert.IsFalse(BATFeather.Wrappers().Frontend().Events().EventsWrapper().IsItemPresent(TimelineSchedulerView), TimelineSchedulerView + TextNotVisible);
            Assert.IsTrue(BATFeather.Wrappers().Frontend().Events().EventsWrapper().IsItemPresent(""), RefreshText + TextVisible);
            Assert.IsTrue(BATFeather.Wrappers().Frontend().Events().EventsWrapper().IsTableViewPresent(ClassMonthView), MonthSchedulerView + TextVisible);
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();
            BAT.Macros().NavigateTo().CustomPage("~/" + PageTitle.ToLower(), true, this.Culture);
            Assert.IsFalse(BATFeather.Wrappers().Frontend().Events().EventsWrapper().IsItemPresent(MonthSchedulerView), MonthSchedulerView + TextNotVisible);
            Assert.IsFalse(BATFeather.Wrappers().Frontend().Events().EventsWrapper().IsItemPresent(WeekSchedulerView), WeekSchedulerView + TextNotVisible);
            Assert.IsFalse(BATFeather.Wrappers().Frontend().Events().EventsWrapper().IsItemPresent(DaySchedulerView), DaySchedulerView + TextNotVisible);
            Assert.IsFalse(BATFeather.Wrappers().Frontend().Events().EventsWrapper().IsItemPresent(WorkWeekSchedulerView), WorkWeekSchedulerView + TextNotVisible);
            Assert.IsFalse(BATFeather.Wrappers().Frontend().Events().EventsWrapper().IsItemPresent(AgendaSchedulerView), AgendaSchedulerView + TextNotVisible);
            Assert.IsFalse(BATFeather.Wrappers().Frontend().Events().EventsWrapper().IsItemPresent(TimelineSchedulerView), TimelineSchedulerView + TextNotVisible);
            Assert.IsTrue(BATFeather.Wrappers().Frontend().Events().EventsWrapper().IsItemPresent(""), RefreshText + TextVisible);
            Assert.IsTrue(BATFeather.Wrappers().Frontend().Events().EventsWrapper().IsTableViewPresent(ClassMonthView), MonthSchedulerView  + TextVisible);
            
            ////Default Week view
            BAT.Macros().NavigateTo().Pages(this.Culture);
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(PageTitle);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(WidgetCalendar);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().OpenListSettingsView();
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SelectOptionInDefaultCalendarViewSelector(WeekSchedulerView);
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SaveChanges();
            Assert.IsFalse(BATFeather.Wrappers().Frontend().Events().EventsWrapper().IsItemPresent(MonthSchedulerView), MonthSchedulerView + TextNotVisible);
            Assert.IsFalse(BATFeather.Wrappers().Frontend().Events().EventsWrapper().IsItemPresent(WeekSchedulerView), WeekSchedulerView + TextNotVisible);
            Assert.IsFalse(BATFeather.Wrappers().Frontend().Events().EventsWrapper().IsItemPresent(DaySchedulerView), DaySchedulerView + TextNotVisible);
            Assert.IsFalse(BATFeather.Wrappers().Frontend().Events().EventsWrapper().IsItemPresent(WorkWeekSchedulerView), WorkWeekSchedulerView + TextNotVisible);
            Assert.IsFalse(BATFeather.Wrappers().Frontend().Events().EventsWrapper().IsItemPresent(AgendaSchedulerView), AgendaSchedulerView + TextNotVisible);
            Assert.IsFalse(BATFeather.Wrappers().Frontend().Events().EventsWrapper().IsItemPresent(TimelineSchedulerView), TimelineSchedulerView + TextNotVisible);
            Assert.IsTrue(BATFeather.Wrappers().Frontend().Events().EventsWrapper().IsItemPresent(""), RefreshText + TextVisible);
            Assert.IsTrue(BATFeather.Wrappers().Frontend().Events().EventsWrapper().IsTableViewPresent(ClassWeekView), WeekSchedulerView + TextVisible);
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();
            BAT.Macros().NavigateTo().CustomPage("~/" + PageTitle.ToLower(), true, this.Culture);
            Assert.IsFalse(BATFeather.Wrappers().Frontend().Events().EventsWrapper().IsItemPresent(MonthSchedulerView), MonthSchedulerView + TextNotVisible);
            Assert.IsFalse(BATFeather.Wrappers().Frontend().Events().EventsWrapper().IsItemPresent(WeekSchedulerView), WeekSchedulerView + TextNotVisible);
            Assert.IsFalse(BATFeather.Wrappers().Frontend().Events().EventsWrapper().IsItemPresent(DaySchedulerView), "Day isn't visibld");
            Assert.IsFalse(BATFeather.Wrappers().Frontend().Events().EventsWrapper().IsItemPresent(WorkWeekSchedulerView), WorkWeekSchedulerView + TextNotVisible);
            Assert.IsFalse(BATFeather.Wrappers().Frontend().Events().EventsWrapper().IsItemPresent(AgendaSchedulerView), AgendaSchedulerView + TextNotVisible);
            Assert.IsFalse(BATFeather.Wrappers().Frontend().Events().EventsWrapper().IsItemPresent(TimelineSchedulerView), TimelineSchedulerView + TextNotVisible);
            Assert.IsTrue(BATFeather.Wrappers().Frontend().Events().EventsWrapper().IsItemPresent(""), RefreshText + TextVisible);
            Assert.IsTrue(BATFeather.Wrappers().Frontend().Events().EventsWrapper().IsTableViewPresent(ClassWeekView), WeekSchedulerView + TextVisible);

            ////Default Work Week view
            BAT.Macros().NavigateTo().Pages(this.Culture);
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(PageTitle);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(WidgetCalendar);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().OpenListSettingsView();
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SelectOptionInDefaultCalendarViewSelector("WorkWeek");
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SaveChanges();
            Assert.IsFalse(BATFeather.Wrappers().Frontend().Events().EventsWrapper().IsItemPresent(MonthSchedulerView), MonthSchedulerView + TextNotVisible);
            Assert.IsFalse(BATFeather.Wrappers().Frontend().Events().EventsWrapper().IsItemPresent(WeekSchedulerView), WeekSchedulerView + TextNotVisible);
            Assert.IsFalse(BATFeather.Wrappers().Frontend().Events().EventsWrapper().IsItemPresent(DaySchedulerView), DaySchedulerView + TextNotVisible);
            Assert.IsFalse(BATFeather.Wrappers().Frontend().Events().EventsWrapper().IsItemPresent(WorkWeekSchedulerView), WorkWeekSchedulerView + TextNotVisible);
            Assert.IsFalse(BATFeather.Wrappers().Frontend().Events().EventsWrapper().IsItemPresent(AgendaSchedulerView), AgendaSchedulerView + TextNotVisible);
            Assert.IsFalse(BATFeather.Wrappers().Frontend().Events().EventsWrapper().IsItemPresent(TimelineSchedulerView), TimelineSchedulerView + TextNotVisible);
            Assert.IsTrue(BATFeather.Wrappers().Frontend().Events().EventsWrapper().IsItemPresent(""), RefreshText + TextVisible);
            Assert.IsTrue(BATFeather.Wrappers().Frontend().Events().EventsWrapper().IsTableViewPresent(ClassWorkWeekView), WorkWeekSchedulerView + TextVisible);
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();
            BAT.Macros().NavigateTo().CustomPage("~/" + PageTitle.ToLower(), true, this.Culture);
            Assert.IsFalse(BATFeather.Wrappers().Frontend().Events().EventsWrapper().IsItemPresent(MonthSchedulerView), MonthSchedulerView + TextNotVisible);
            Assert.IsFalse(BATFeather.Wrappers().Frontend().Events().EventsWrapper().IsItemPresent(WeekSchedulerView), WeekSchedulerView + TextNotVisible);
            Assert.IsFalse(BATFeather.Wrappers().Frontend().Events().EventsWrapper().IsItemPresent(DaySchedulerView), DaySchedulerView + TextNotVisible);
            Assert.IsFalse(BATFeather.Wrappers().Frontend().Events().EventsWrapper().IsItemPresent(WorkWeekSchedulerView), WorkWeekSchedulerView + TextNotVisible);
            Assert.IsFalse(BATFeather.Wrappers().Frontend().Events().EventsWrapper().IsItemPresent(AgendaSchedulerView), AgendaSchedulerView + TextNotVisible);
            Assert.IsFalse(BATFeather.Wrappers().Frontend().Events().EventsWrapper().IsItemPresent(TimelineSchedulerView), TimelineSchedulerView + TextNotVisible);
            Assert.IsTrue(BATFeather.Wrappers().Frontend().Events().EventsWrapper().IsItemPresent(""), RefreshText + TextVisible);
            Assert.IsTrue(BATFeather.Wrappers().Frontend().Events().EventsWrapper().IsTableViewPresent(ClassWorkWeekView), WorkWeekSchedulerView + TextVisible);

            ////Default Day view
            BAT.Macros().NavigateTo().Pages(this.Culture);
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(PageTitle);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(WidgetCalendar);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().OpenListSettingsView();
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SelectOptionInDefaultCalendarViewSelector(DaySchedulerView);
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SaveChanges();
            Assert.IsFalse(BATFeather.Wrappers().Frontend().Events().EventsWrapper().IsItemPresent(MonthSchedulerView), MonthSchedulerView + TextNotVisible);
            Assert.IsFalse(BATFeather.Wrappers().Frontend().Events().EventsWrapper().IsItemPresent(WeekSchedulerView), WeekSchedulerView + TextNotVisible);
            Assert.IsFalse(BATFeather.Wrappers().Frontend().Events().EventsWrapper().IsItemPresent(DaySchedulerView), DaySchedulerView + TextNotVisible);
            Assert.IsFalse(BATFeather.Wrappers().Frontend().Events().EventsWrapper().IsItemPresent(WorkWeekSchedulerView), WorkWeekSchedulerView + TextNotVisible);
            Assert.IsFalse(BATFeather.Wrappers().Frontend().Events().EventsWrapper().IsItemPresent(AgendaSchedulerView), AgendaSchedulerView + TextNotVisible);
            Assert.IsFalse(BATFeather.Wrappers().Frontend().Events().EventsWrapper().IsItemPresent(TimelineSchedulerView), TimelineSchedulerView + TextNotVisible);
            Assert.IsTrue(BATFeather.Wrappers().Frontend().Events().EventsWrapper().IsItemPresent(""), RefreshText + TextVisible);
            Assert.IsTrue(BATFeather.Wrappers().Frontend().Events().EventsWrapper().IsTableViewPresent(ClassDayView), DaySchedulerView + TextVisible);
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();
            BAT.Macros().NavigateTo().CustomPage("~/" + PageTitle.ToLower(), true, this.Culture);
            Assert.IsFalse(BATFeather.Wrappers().Frontend().Events().EventsWrapper().IsItemPresent(MonthSchedulerView), MonthSchedulerView + TextNotVisible);
            Assert.IsFalse(BATFeather.Wrappers().Frontend().Events().EventsWrapper().IsItemPresent(WeekSchedulerView), WeekSchedulerView + TextNotVisible);
            Assert.IsFalse(BATFeather.Wrappers().Frontend().Events().EventsWrapper().IsItemPresent(DaySchedulerView), DaySchedulerView + TextNotVisible);
            Assert.IsFalse(BATFeather.Wrappers().Frontend().Events().EventsWrapper().IsItemPresent(WorkWeekSchedulerView), WorkWeekSchedulerView + TextNotVisible);
            Assert.IsFalse(BATFeather.Wrappers().Frontend().Events().EventsWrapper().IsItemPresent(AgendaSchedulerView), AgendaSchedulerView + TextNotVisible);
            Assert.IsFalse(BATFeather.Wrappers().Frontend().Events().EventsWrapper().IsItemPresent(TimelineSchedulerView), TimelineSchedulerView + TextNotVisible);
            Assert.IsTrue(BATFeather.Wrappers().Frontend().Events().EventsWrapper().IsItemPresent(""), RefreshText + TextVisible);
            Assert.IsTrue(BATFeather.Wrappers().Frontend().Events().EventsWrapper().IsTableViewPresent(ClassDayView), DaySchedulerView + TextVisible);

            ////Default Agenda view
            BAT.Macros().NavigateTo().Pages(this.Culture);
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(PageTitle);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(WidgetCalendar);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().OpenListSettingsView();
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SelectOptionInDefaultCalendarViewSelector(AgendaSchedulerView);
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SaveChanges();
            Assert.IsFalse(BATFeather.Wrappers().Frontend().Events().EventsWrapper().IsItemPresent(MonthSchedulerView), MonthSchedulerView + TextNotVisible);
            Assert.IsFalse(BATFeather.Wrappers().Frontend().Events().EventsWrapper().IsItemPresent(WeekSchedulerView), WeekSchedulerView + TextNotVisible);
            Assert.IsFalse(BATFeather.Wrappers().Frontend().Events().EventsWrapper().IsItemPresent(DaySchedulerView), DaySchedulerView + TextNotVisible);
            Assert.IsFalse(BATFeather.Wrappers().Frontend().Events().EventsWrapper().IsItemPresent(WorkWeekSchedulerView), WorkWeekSchedulerView + TextNotVisible);
            Assert.IsFalse(BATFeather.Wrappers().Frontend().Events().EventsWrapper().IsItemPresent(AgendaSchedulerView), AgendaSchedulerView + TextNotVisible);
            Assert.IsFalse(BATFeather.Wrappers().Frontend().Events().EventsWrapper().IsItemPresent(TimelineSchedulerView), TimelineSchedulerView + TextNotVisible);
            Assert.IsTrue(BATFeather.Wrappers().Frontend().Events().EventsWrapper().IsItemPresent(""), RefreshText + TextVisible);
            Assert.IsTrue(BATFeather.Wrappers().Frontend().Events().EventsWrapper().IsTableViewPresent(ClassAgendaView), AgendaSchedulerView + TextVisible);
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();
            BAT.Macros().NavigateTo().CustomPage("~/" + PageTitle.ToLower(), true, this.Culture);
            Assert.IsFalse(BATFeather.Wrappers().Frontend().Events().EventsWrapper().IsItemPresent(MonthSchedulerView), MonthSchedulerView + TextNotVisible);
            Assert.IsFalse(BATFeather.Wrappers().Frontend().Events().EventsWrapper().IsItemPresent(WeekSchedulerView), WeekSchedulerView + TextNotVisible);
            Assert.IsFalse(BATFeather.Wrappers().Frontend().Events().EventsWrapper().IsItemPresent(DaySchedulerView), DaySchedulerView + TextNotVisible);
            Assert.IsFalse(BATFeather.Wrappers().Frontend().Events().EventsWrapper().IsItemPresent(WorkWeekSchedulerView), WorkWeekSchedulerView + TextNotVisible);
            Assert.IsFalse(BATFeather.Wrappers().Frontend().Events().EventsWrapper().IsItemPresent(AgendaSchedulerView), AgendaSchedulerView + TextNotVisible);
            Assert.IsFalse(BATFeather.Wrappers().Frontend().Events().EventsWrapper().IsItemPresent(TimelineSchedulerView), TimelineSchedulerView + TextNotVisible);
            Assert.IsTrue(BATFeather.Wrappers().Frontend().Events().EventsWrapper().IsItemPresent(""), RefreshText + TextVisible);
            Assert.IsTrue(BATFeather.Wrappers().Frontend().Events().EventsWrapper().IsTableViewPresent(ClassAgendaView), AgendaSchedulerView + TextVisible);

            ////Default Timeline view
            BAT.Macros().NavigateTo().Pages(this.Culture);
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(PageTitle);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(WidgetCalendar);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().OpenListSettingsView();
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SelectOptionInDefaultCalendarViewSelector(TimelineSchedulerView);
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SaveChanges();
            Assert.IsFalse(BATFeather.Wrappers().Frontend().Events().EventsWrapper().IsItemPresent(MonthSchedulerView), MonthSchedulerView + TextNotVisible);
            Assert.IsFalse(BATFeather.Wrappers().Frontend().Events().EventsWrapper().IsItemPresent(WeekSchedulerView), WeekSchedulerView + TextNotVisible);
            Assert.IsFalse(BATFeather.Wrappers().Frontend().Events().EventsWrapper().IsItemPresent(DaySchedulerView), DaySchedulerView + TextNotVisible);
            Assert.IsFalse(BATFeather.Wrappers().Frontend().Events().EventsWrapper().IsItemPresent(WorkWeekSchedulerView), WorkWeekSchedulerView + TextNotVisible);
            Assert.IsFalse(BATFeather.Wrappers().Frontend().Events().EventsWrapper().IsItemPresent(AgendaSchedulerView), AgendaSchedulerView + TextNotVisible);
            Assert.IsFalse(BATFeather.Wrappers().Frontend().Events().EventsWrapper().IsItemPresent(TimelineSchedulerView), TimelineSchedulerView + TextNotVisible);
            Assert.IsTrue(BATFeather.Wrappers().Frontend().Events().EventsWrapper().IsItemPresent(""), RefreshText + TextVisible);
            Assert.IsTrue(BATFeather.Wrappers().Frontend().Events().EventsWrapper().IsTableViewPresent(ClassTimelineView), TimelineSchedulerView + TextVisible);
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();
            BAT.Macros().NavigateTo().CustomPage("~/" + PageTitle.ToLower(), true, this.Culture);
            Assert.IsFalse(BATFeather.Wrappers().Frontend().Events().EventsWrapper().IsItemPresent(MonthSchedulerView), MonthSchedulerView + TextNotVisible);
            Assert.IsFalse(BATFeather.Wrappers().Frontend().Events().EventsWrapper().IsItemPresent(WeekSchedulerView), WeekSchedulerView + TextNotVisible);
            Assert.IsFalse(BATFeather.Wrappers().Frontend().Events().EventsWrapper().IsItemPresent(DaySchedulerView), DaySchedulerView + TextNotVisible);
            Assert.IsFalse(BATFeather.Wrappers().Frontend().Events().EventsWrapper().IsItemPresent(WorkWeekSchedulerView), WorkWeekSchedulerView + TextNotVisible);
            Assert.IsFalse(BATFeather.Wrappers().Frontend().Events().EventsWrapper().IsItemPresent(AgendaSchedulerView), AgendaSchedulerView + TextNotVisible);
            Assert.IsFalse(BATFeather.Wrappers().Frontend().Events().EventsWrapper().IsItemPresent(TimelineSchedulerView), TimelineSchedulerView + TextNotVisible);
            Assert.IsTrue(BATFeather.Wrappers().Frontend().Events().EventsWrapper().IsItemPresent(""), RefreshText + TextVisible);
            Assert.IsTrue(BATFeather.Wrappers().Frontend().Events().EventsWrapper().IsTableViewPresent(ClassTimelineView), TimelineSchedulerView + TextVisible);

            ////Allow user to switch views
            BAT.Macros().NavigateTo().Pages(this.Culture);
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(PageTitle);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(WidgetCalendar);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().OpenListSettingsView();
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SelectAllowUsersToSiwtchViewsCheckBoxInCalendarWidget();
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SaveChanges();
            Assert.IsTrue(BATFeather.Wrappers().Frontend().Events().EventsWrapper().IsItemPresent(MonthSchedulerView), MonthSchedulerView + TextVisible);
            Assert.IsTrue(BATFeather.Wrappers().Frontend().Events().EventsWrapper().IsItemPresent(WeekSchedulerView), WeekSchedulerView + TextVisible);
            Assert.IsTrue(BATFeather.Wrappers().Frontend().Events().EventsWrapper().IsItemPresent(DaySchedulerView), DaySchedulerView + TextVisible);
            Assert.IsTrue(BATFeather.Wrappers().Frontend().Events().EventsWrapper().IsItemPresent(WorkWeekSchedulerView), WorkWeekSchedulerView + TextVisible);
            Assert.IsTrue(BATFeather.Wrappers().Frontend().Events().EventsWrapper().IsItemPresent(AgendaSchedulerView), AgendaSchedulerView + TextVisible);
            var actualDefaultTimelineView = BATFeather.Wrappers().Frontend().Events().EventsWrapper().GetSelectedSchedulerView();
            Assert.AreEqual(TimelineSchedulerView, actualDefaultTimelineView);
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();
            BAT.Macros().NavigateTo().CustomPage("~/" + PageTitle.ToLower(), true, this.Culture);
            Assert.IsTrue(BATFeather.Wrappers().Frontend().Events().EventsWrapper().IsItemPresent(MonthSchedulerView), MonthSchedulerView + TextVisible);
            Assert.IsTrue(BATFeather.Wrappers().Frontend().Events().EventsWrapper().IsItemPresent(WeekSchedulerView), WeekSchedulerView + TextVisible);
            Assert.IsTrue(BATFeather.Wrappers().Frontend().Events().EventsWrapper().IsItemPresent(DaySchedulerView), DaySchedulerView + TextVisible);
            Assert.IsTrue(BATFeather.Wrappers().Frontend().Events().EventsWrapper().IsItemPresent(WorkWeekSchedulerView), WorkWeekSchedulerView + TextVisible);
            Assert.IsTrue(BATFeather.Wrappers().Frontend().Events().EventsWrapper().IsItemPresent(AgendaSchedulerView), AgendaSchedulerView + TextVisible);
            Assert.AreEqual(BATFeather.Wrappers().Frontend().Events().EventsWrapper().GetSelectedSchedulerView(), TimelineSchedulerView);
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
        private const string TextVisible = " is visible";
        private const string TextNotVisible = " isn't visible";
        private const string ClassMonthView = "k-scheduler-layout k-scheduler-monthview";
        private const string ClassWeekView = "k-scheduler-layout k-scheduler-weekview";
        private const string ClassWorkWeekView = "k-scheduler-layout k-scheduler-workWeekview";
        private const string ClassDayView = "k-scheduler-layout k-scheduler-dayview";
        private const string ClassAgendaView = "k-scheduler-layout k-scheduler-agendaview k-scheduler-agenda";
        private const string ClassTimelineView = "k-scheduler-layout k-scheduler-timelineview";
        private const string RefreshText = "Refresh";
        private readonly string[] defaultViewOptions = new string[] { "Month", "Week", "Work Week", "Day", "Agenda", "Timeline" };
    }
}
