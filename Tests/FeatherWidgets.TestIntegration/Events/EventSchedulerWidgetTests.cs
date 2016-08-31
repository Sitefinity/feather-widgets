using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using MbUnit.Framework;
using ServiceStack.Text;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Frontend.Events.Mvc.Controllers;
using Telerik.Sitefinity.Frontend.Events.Mvc.Models.EventScheduler;
using Telerik.Sitefinity.Frontend.Mvc.Models;
using Telerik.Sitefinity.Frontend.TestUtilities;
using Telerik.Sitefinity.Modules.Events;
using Telerik.Sitefinity.Mvc.TestUtilities.Helpers;
using Telerik.Sitefinity.RecurrentRules;
using Telerik.Sitefinity.TestUtilities.CommonOperations;
using Telerik.Sitefinity.TestUtilities.CommonOperations.Multilingual;
using Telerik.Sitefinity.Web;
using Telerik.WebTestRunner.Server.Attributes;

namespace FeatherWidgets.TestIntegration.Events
{
    /// <summary>
    /// This class contains test related to basic functionality of Event Scheduler Widget.
    /// </summary>
    [TestFixture]
    public class EventSchedulerWidgetTests
    {
        /// <summary>
        /// The setup.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            ServerOperations.Events().CreateEvent(EventSchedulerWidgetTests.BaseEventTitle);
            ServerOperations.Events().CreateEvent(EventSchedulerWidgetTests.BasePastEventTitle, string.Empty, false, DateTime.Now.AddDays(-2), DateTime.Now.AddDays(-1));
            ServerOperations.Events().CreateEvent(EventSchedulerWidgetTests.BaseUpcomingEventTitle, string.Empty, false, DateTime.Now.AddDays(1), DateTime.Now.AddDays(2));
            ServerOperations.Events().CreateEvent(EventSchedulerWidgetTests.BaseAllDayEventTitle, string.Empty, true, DateTime.Now, DateTime.Now.AddHours(1));
            ServerOperations.Events().CreateDailyRecurrentEvent(EventSchedulerWidgetTests.BaseRepeatEventTitle, string.Empty, DateTime.Now, DateTime.Now.AddHours(1), 60, 5, 1, TimeZoneInfo.Local.Id);
            ServerOperations.Events().CreateDraftEvent(EventSchedulerWidgetTests.BaseDraftEventTitle, string.Empty, false, DateTime.Now, DateTime.Now.AddHours(1));

            this.InitializeSitefinityLanguages();
        }

        /// <summary>
        /// The tear down.
        /// </summary>
        [TearDown]
        public void TearDown()
        {
            Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.Pages().DeleteAllPages();
            Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.Events().DeleteAllEvents();

            foreach (var item in this.calendarList)
            {
                ServerOperations.Events().DeleteCalendar(item);
            }
        }
        
        #region Events

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.FeatherTeam)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events()
        {
            ServerOperations.Events().CreаteAllDayRecurrentEvent("RepeatEventTitle1", string.Empty, DateTime.Today, DateTime.Today, 60, 10000, 1, true);
            ServerOperations.Events().CreаteAllDayRecurrentEvent("RepeatEventTitle2", string.Empty, DateTime.Today, DateTime.Today, 60, 10000, 1, true);
            ServerOperations.Events().CreаteAllDayRecurrentEvent("RepeatEventTitle3", string.Empty, DateTime.Today, DateTime.Today, 60, 10000, 1, true);
            ServerOperations.Events().CreаteAllDayRecurrentEvent("RepeatEventTitle4", string.Empty, DateTime.Today, DateTime.Today, 60, 10000, 1, true);
            ServerOperations.Events().CreаteAllDayRecurrentEvent("RepeatEventTitle5", string.Empty, DateTime.Today, DateTime.Today, 60, 10000, 1, true);
            ServerOperations.Events().CreаteAllDayRecurrentEvent("RepeatEventTitle6", string.Empty, DateTime.Today, DateTime.Today, 60, 10000, 1, true);

            var eventController = new EventSchedulerController();
            this.ApplyFilters(eventController, SelectionMode.AllItems, DefaultAdditionalFilters, SelectionMode.AllItems, DefaultNarrowSelectionFilters);

            this.AssertEvent(eventController, DateTime.MinValue, DateTime.MaxValue, EventsManager.GetManager().GetEventsOccurrences(DateTime.MinValue, DateTime.MaxValue).Count());
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.FeatherTeam)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events_Upcoming()
        {
            var eventController = new EventSchedulerController();
            this.ApplyFilters(eventController, SelectionMode.FilteredItems, UpcomingAdditionalFilters, SelectionMode.AllItems, DefaultNarrowSelectionFilters);

            this.AssertEvent(eventController, DateTime.MinValue, DateTime.MaxValue, 2);
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.FeatherTeam)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events_Past()
        {
            var eventController = new EventSchedulerController();
            this.ApplyFilters(eventController, SelectionMode.FilteredItems, PastAdditionalFilters, SelectionMode.AllItems, DefaultNarrowSelectionFilters);

            this.AssertEvent(eventController, DateTime.MinValue, DateTime.MaxValue, 1);
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.FeatherTeam)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events_Current()
        {
            var eventController = new EventSchedulerController();
            this.ApplyFilters(eventController, SelectionMode.FilteredItems, CurrentAdditionalFilters, SelectionMode.AllItems, DefaultNarrowSelectionFilters);

            this.AssertEvent(eventController, DateTime.MinValue, DateTime.MaxValue, 6);
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.FeatherTeam)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events_UpcomingAndCurrent()
        {
            var eventController = new EventSchedulerController();
            this.ApplyFilters(eventController, SelectionMode.FilteredItems, UpcomingAndCurrentAdditionalFilters, SelectionMode.AllItems, DefaultNarrowSelectionFilters);

            this.AssertEvent(eventController, DateTime.MinValue, DateTime.MaxValue, 8);
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.FeatherTeam)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events_PastAndUpcoming()
        {
            var eventController = new EventSchedulerController();
            this.ApplyFilters(eventController, SelectionMode.FilteredItems, PastAndUpcomingAdditionalFilters, SelectionMode.AllItems, DefaultNarrowSelectionFilters);

            this.AssertEvent(eventController, DateTime.MinValue, DateTime.MaxValue, 3);
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.FeatherTeam)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events_PastAndCurrent()
        {
            var eventController = new EventSchedulerController();
            this.ApplyFilters(eventController, SelectionMode.FilteredItems, PastAndCurrentAdditionalFilters, SelectionMode.AllItems, DefaultNarrowSelectionFilters);

            this.AssertEvent(eventController, DateTime.MinValue, DateTime.MaxValue, 7);
        }

        #endregion

        #region Events - Multilingual

        [Test]
        [Ignore("Events are not created correctly with current recurrenceExpression setup")]
        [Multilingual]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.FeatherTeam)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events_Multilingual()
        {
            var manager = EventsManager.GetManager();
            var multiOperations = new MultilingualEventOperations();
            var bulgarian = AppSettings.CurrentSettings.DefinedFrontendLanguages.Where(x => x.Name == "bg-BG").FirstOrDefault();

            string recurrenceExpression = new RecurrenceRuleBuilder().CreateDailyRecurrenceExpression(DateTime.Today, TimeSpan.FromMinutes(60), DateTime.MaxValue, int.MaxValue, 1, TimeZoneInfo.Local.Id);

            var calendartDefault = manager.GetCalendars().FirstOrDefault();
            this.CreateLocalizedEvent(multiOperations, "Event 1 bg", Guid.NewGuid(), DateTime.Today, DateTime.Today.AddHours(1), false, true, calendartDefault, bulgarian, recurrenceExpression);
            this.CreateLocalizedEvent(multiOperations, "Event 2 bg", Guid.NewGuid(), DateTime.Today, DateTime.Today.AddHours(1), false, true, calendartDefault, bulgarian, recurrenceExpression);
            this.CreateLocalizedEvent(multiOperations, "Event 3 bg", Guid.NewGuid(), DateTime.Today, DateTime.Today.AddHours(1), false, true, calendartDefault, bulgarian, recurrenceExpression);
            this.CreateLocalizedEvent(multiOperations, "Event 4 bg", Guid.NewGuid(), DateTime.Today, DateTime.Today.AddHours(1), false, true, calendartDefault, bulgarian, recurrenceExpression);
            this.CreateLocalizedEvent(multiOperations, "Event 5 bg", Guid.NewGuid(), DateTime.Today, DateTime.Today.AddHours(1), false, true, calendartDefault, bulgarian, recurrenceExpression);
            this.CreateLocalizedEvent(multiOperations, "Event 6 bg", Guid.NewGuid(), DateTime.Today, DateTime.Today.AddHours(1), false, true, calendartDefault, bulgarian, recurrenceExpression);
            this.CreateLocalizedEvent(multiOperations, "Event 7 bg", Guid.NewGuid(), DateTime.Today, DateTime.Today.AddHours(1), false, true, calendartDefault, bulgarian, recurrenceExpression);
            this.CreateLocalizedEvent(multiOperations, "Event 8 bg", Guid.NewGuid(), DateTime.Today, DateTime.Today.AddHours(1), false, true, calendartDefault, bulgarian, recurrenceExpression);
            this.CreateLocalizedEvent(multiOperations, "Event 9 bg", Guid.NewGuid(), DateTime.Today, DateTime.Today.AddHours(1), false, true, calendartDefault, bulgarian, recurrenceExpression);
            this.CreateLocalizedEvent(multiOperations, "Event 10 bg", Guid.NewGuid(), DateTime.Today, DateTime.Today.AddHours(1), false, true, calendartDefault, bulgarian, recurrenceExpression);

            var eventController = new EventSchedulerController();
            this.ApplyFilters(eventController, SelectionMode.AllItems, DefaultAdditionalFilters, SelectionMode.AllItems, DefaultNarrowSelectionFilters);

            this.AssertEvent(eventController, DateTime.MinValue, DateTime.MaxValue, EventsManager.GetManager().GetEventsOccurrences(EventsManager.GetManager().GetEvents(), DateTime.MinValue, DateTime.Now.AddYears(4000)).Where(p => p.Event.LanguageData.Where(t => t.Language == bulgarian.Name).LongCount() > 0).Count(), bulgarian);
        }

        [Test]
        [Multilingual]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.FeatherTeam)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events_Upcoming_Multilingual()
        {
            var manager = EventsManager.GetManager();
            var multiOperations = new MultilingualEventOperations();
            var arabic = AppSettings.CurrentSettings.DefinedFrontendLanguages.Where(x => x.Name == "ar-MA").FirstOrDefault();

            var calendartDefault = manager.GetCalendars().FirstOrDefault();
            this.CreateLocalizedEvent(multiOperations, "Event 1 arabic", Guid.NewGuid(), DateTime.Now, DateTime.Now.AddHours(2), false, false, calendartDefault, arabic);
            this.CreateLocalizedEvent(multiOperations, "Event 2 arabic", Guid.NewGuid(), DateTime.Now.AddDays(1), DateTime.Now.AddDays(3), false, false, calendartDefault, arabic);

            var eventController = new EventSchedulerController();
            this.ApplyFilters(eventController, SelectionMode.AllItems, UpcomingAdditionalFilters, SelectionMode.AllItems, DefaultNarrowSelectionFilters);

            this.AssertEvent(eventController, DateTime.MinValue, DateTime.MaxValue, 2, arabic);
        }

        #endregion

        #region Specified upcoming events

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.FeatherTeam)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events_UpcomingNext3Days()
        {
            ServerOperations.Events().CreateEvent("UpcomingEventInNext3Days", string.Empty, false, DateTime.Now.AddDays(2), DateTime.Now.AddDays(3));
            ServerOperations.Events().CreateEvent("UpcomingEventInMoreThanNext3Days", string.Empty, false, DateTime.Now.AddDays(5), DateTime.Now.AddDays(7));

            var eventController = new EventSchedulerController();
            this.ApplyFilters(eventController, SelectionMode.FilteredItems, Next3DaysAdditionalFilters, SelectionMode.AllItems, DefaultNarrowSelectionFilters);

            this.AssertEvent(eventController, DateTime.MinValue, DateTime.MaxValue, 3);
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.FeatherTeam)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events_UpcomingNextWeek()
        {
            ServerOperations.Events().CreateEvent("UpcomingEventInNextWeek", string.Empty, false, DateTime.Now.AddDays(4), DateTime.Now.AddDays(6));
            ServerOperations.Events().CreateEvent("UpcomingEventInMoreThanNextWeek", string.Empty, false, DateTime.Now.AddDays(9), DateTime.Now.AddDays(12));

            var eventController = new EventSchedulerController();
            this.ApplyFilters(eventController, SelectionMode.FilteredItems, NextWeekAdditionalFilters, SelectionMode.AllItems, DefaultNarrowSelectionFilters);

            this.AssertEvent(eventController, DateTime.MinValue, DateTime.MaxValue, 3);
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.FeatherTeam)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events_UpcomingNextMonth()
        {
            ServerOperations.Events().CreateEvent("UpcomingEventInNextMonth", string.Empty, false, DateTime.Now.AddDays(20), DateTime.Now.AddDays(22));
            ServerOperations.Events().CreateEvent("UpcomingEventInMoreThanNextMonth", string.Empty, false, DateTime.Now.AddDays(40), DateTime.Now.AddDays(42));

            var eventController = new EventSchedulerController();
            this.ApplyFilters(eventController, SelectionMode.FilteredItems, NextMonthAdditionalFilters, SelectionMode.AllItems, DefaultNarrowSelectionFilters);

            this.AssertEvent(eventController, DateTime.MinValue, DateTime.MaxValue, 3);
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.FeatherTeam)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events_UpcomingNextYear()
        {
            ServerOperations.Events().CreateEvent("UpcomingEventInNextYear", string.Empty, false, DateTime.Now.AddDays(365), DateTime.Now.AddDays(368));
            ServerOperations.Events().CreateEvent("UpcomingEventInMoreThanNextYear", string.Empty, false, DateTime.Now.AddDays(450), DateTime.Now.AddDays(453));

            var eventController = new EventSchedulerController();
            this.ApplyFilters(eventController, SelectionMode.FilteredItems, NextYearAdditionalFilters, SelectionMode.AllItems, DefaultNarrowSelectionFilters);

            this.AssertEvent(eventController, DateTime.MinValue, DateTime.MaxValue, 3);
        }

        #endregion

        #region Specified upcoming events - Multilingual

        #endregion

        #region Specified past events

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.FeatherTeam)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events_PastLast3Days()
        {
            ServerOperations.Events().CreateEvent("PastEventInLast3Days", string.Empty, false, DateTime.Now.AddDays(-2), DateTime.Now.AddDays(-1));
            ServerOperations.Events().CreateEvent("PastEventInMoreThanLast3Days", string.Empty, false, DateTime.Now.AddDays(-6), DateTime.Now.AddDays(-4));

            var eventController = new EventSchedulerController();
            this.ApplyFilters(eventController, SelectionMode.FilteredItems, Last3DaysAdditionalFilters, SelectionMode.AllItems, DefaultNarrowSelectionFilters);

            this.AssertEvent(eventController, DateTime.MinValue, DateTime.MaxValue, 2);
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.FeatherTeam)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events_PastLastWeek()
        {
            ServerOperations.Events().CreateEvent("PastEventInLastWeek", string.Empty, false, DateTime.Now.AddDays(-6), DateTime.Now.AddDays(-4));
            ServerOperations.Events().CreateEvent("PastEventInMoreThanLastWeek", string.Empty, false, DateTime.Now.AddDays(-12), DateTime.Now.AddDays(-9));

            var eventController = new EventSchedulerController();
            this.ApplyFilters(eventController, SelectionMode.FilteredItems, LastWeekAdditionalFilters, SelectionMode.AllItems, DefaultNarrowSelectionFilters);

            this.AssertEvent(eventController, DateTime.MinValue, DateTime.MaxValue, 2);
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.FeatherTeam)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events_PastLastMonth()
        {
            ServerOperations.Events().CreateEvent("PastEventInLastMonth", string.Empty, false, DateTime.Now.AddDays(-22), DateTime.Now.AddDays(-20));
            ServerOperations.Events().CreateEvent("PastEventInMoreThanLastMonth", string.Empty, false, DateTime.Now.AddDays(-42), DateTime.Now.AddDays(-40));

            var eventController = new EventSchedulerController();
            this.ApplyFilters(eventController, SelectionMode.FilteredItems, LastMonthAdditionalFilters, SelectionMode.AllItems, DefaultNarrowSelectionFilters);

            this.AssertEvent(eventController, DateTime.MinValue, DateTime.MaxValue, 2);
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.FeatherTeam)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events_PastLastYear()
        {
            ServerOperations.Events().CreateEvent("PastEventInLastYear", string.Empty, false, DateTime.Now.AddDays(-368), DateTime.Now.AddDays(-365));
            ServerOperations.Events().CreateEvent("PastEventInMoreThanLastYear", string.Empty, false, DateTime.Now.AddDays(-453), DateTime.Now.AddDays(-450));

            var eventController = new EventSchedulerController();
            this.ApplyFilters(eventController, SelectionMode.FilteredItems, LastYearAdditionalFilters, SelectionMode.AllItems, DefaultNarrowSelectionFilters);

            this.AssertEvent(eventController, DateTime.MinValue, DateTime.MaxValue, 2);
        }

        #endregion

        #region Specified past events - Multilingual

        #endregion

        #region Specified combined events

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.FeatherTeam)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events_Combined_Next3DaysOrLast3Days()
        {
            ServerOperations.Events().CreateEvent("UpcomingEventInNext3Days", string.Empty, false, DateTime.Now.AddDays(2), DateTime.Now.AddDays(3));
            ServerOperations.Events().CreateEvent("UpcomingEventInMoreThanNext3Days", string.Empty, false, DateTime.Now.AddDays(5), DateTime.Now.AddDays(7));
            ServerOperations.Events().CreateEvent("PastEventInLast3Days", string.Empty, false, DateTime.Now.AddDays(-2), DateTime.Now.AddDays(-1));
            ServerOperations.Events().CreateEvent("PastEventInMoreThanLast3Days", string.Empty, false, DateTime.Now.AddDays(-6), DateTime.Now.AddDays(-4));

            var eventController = new EventSchedulerController();
            this.ApplyFilters(eventController, SelectionMode.FilteredItems, Next3DaysOrLast3DaysAdditionalFilters, SelectionMode.AllItems, DefaultNarrowSelectionFilters);

            this.AssertEvent(eventController, DateTime.MinValue, DateTime.MaxValue, 5);
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.FeatherTeam)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events_Combined_Next3DaysOrLastWeek()
        {
            ServerOperations.Events().CreateEvent("UpcomingEventInNext3Days", string.Empty, false, DateTime.Now.AddDays(2), DateTime.Now.AddDays(3));
            ServerOperations.Events().CreateEvent("UpcomingEventInMoreThanNext3Days", string.Empty, false, DateTime.Now.AddDays(5), DateTime.Now.AddDays(7));
            ServerOperations.Events().CreateEvent("PastEventInLastWeek", string.Empty, false, DateTime.Now.AddDays(-6), DateTime.Now.AddDays(-4));
            ServerOperations.Events().CreateEvent("PastEventInMoreThanLastWeek", string.Empty, false, DateTime.Now.AddDays(-12), DateTime.Now.AddDays(-9));

            var eventController = new EventSchedulerController();
            this.ApplyFilters(eventController, SelectionMode.FilteredItems, Next3DaysOrLastWeekAdditionalFilters, SelectionMode.AllItems, DefaultNarrowSelectionFilters);

            this.AssertEvent(eventController, DateTime.MinValue, DateTime.MaxValue, 5);
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.FeatherTeam)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events_Combined_Next3DaysOrLastMonth()
        {
            ServerOperations.Events().CreateEvent("UpcomingEventInNext3Days", string.Empty, false, DateTime.Now.AddDays(2), DateTime.Now.AddDays(3));
            ServerOperations.Events().CreateEvent("UpcomingEventInMoreThanNext3Days", string.Empty, false, DateTime.Now.AddDays(5), DateTime.Now.AddDays(7));
            ServerOperations.Events().CreateEvent("PastEventInLastMonth", string.Empty, false, DateTime.Now.AddDays(-22), DateTime.Now.AddDays(-20));
            ServerOperations.Events().CreateEvent("PastEventInMoreThanLastMonth", string.Empty, false, DateTime.Now.AddDays(-42), DateTime.Now.AddDays(-40));

            var eventController = new EventSchedulerController();
            this.ApplyFilters(eventController, SelectionMode.FilteredItems, Next3DaysOrLastMonthAdditionalFilters, SelectionMode.AllItems, DefaultNarrowSelectionFilters);

            this.AssertEvent(eventController, DateTime.MinValue, DateTime.MaxValue, 5);
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.FeatherTeam)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events_Combined_Next3DaysOrLastYear()
        {
            ServerOperations.Events().CreateEvent("UpcomingEventInNext3Days", string.Empty, false, DateTime.Now.AddDays(2), DateTime.Now.AddDays(3));
            ServerOperations.Events().CreateEvent("UpcomingEventInMoreThanNext3Days", string.Empty, false, DateTime.Now.AddDays(5), DateTime.Now.AddDays(7));
            ServerOperations.Events().CreateEvent("PastEventInLastYear", string.Empty, false, DateTime.Now.AddDays(-368), DateTime.Now.AddDays(-365));
            ServerOperations.Events().CreateEvent("PastEventInMoreThanLastYear", string.Empty, false, DateTime.Now.AddDays(-453), DateTime.Now.AddDays(-450));

            var eventController = new EventSchedulerController();
            this.ApplyFilters(eventController, SelectionMode.FilteredItems, Next3DaysOrLastYearAdditionalFilters, SelectionMode.AllItems, DefaultNarrowSelectionFilters);

            this.AssertEvent(eventController, DateTime.MinValue, DateTime.MaxValue, 5);
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.FeatherTeam)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events_Combined_NextWeekOrLast3Days()
        {
            ServerOperations.Events().CreateEvent("UpcomingEventInNextWeek", string.Empty, false, DateTime.Now.AddDays(4), DateTime.Now.AddDays(6));
            ServerOperations.Events().CreateEvent("UpcomingEventInMoreThanNextWeek", string.Empty, false, DateTime.Now.AddDays(9), DateTime.Now.AddDays(12));
            ServerOperations.Events().CreateEvent("PastEventInLast3Days", string.Empty, false, DateTime.Now.AddDays(-2), DateTime.Now.AddDays(-1));
            ServerOperations.Events().CreateEvent("PastEventInMoreThanLast3Days", string.Empty, false, DateTime.Now.AddDays(-6), DateTime.Now.AddDays(-4));

            var eventController = new EventSchedulerController();
            this.ApplyFilters(eventController, SelectionMode.FilteredItems, NextWeekOrLast3DaysAdditionalFilters, SelectionMode.AllItems, DefaultNarrowSelectionFilters);

            this.AssertEvent(eventController, DateTime.MinValue, DateTime.MaxValue, 5);
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.FeatherTeam)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events_Combined_NextWeekOrLastWeek()
        {
            ServerOperations.Events().CreateEvent("UpcomingEventInNextWeek", string.Empty, false, DateTime.Now.AddDays(4), DateTime.Now.AddDays(6));
            ServerOperations.Events().CreateEvent("UpcomingEventInMoreThanNextWeek", string.Empty, false, DateTime.Now.AddDays(9), DateTime.Now.AddDays(12));
            ServerOperations.Events().CreateEvent("PastEventInLastWeek", string.Empty, false, DateTime.Now.AddDays(-6), DateTime.Now.AddDays(-4));
            ServerOperations.Events().CreateEvent("PastEventInMoreThanLastWeek", string.Empty, false, DateTime.Now.AddDays(-12), DateTime.Now.AddDays(-9));

            var eventController = new EventSchedulerController();
            this.ApplyFilters(eventController, SelectionMode.FilteredItems, NextWeekOrLastWeekAdditionalFilters, SelectionMode.AllItems, DefaultNarrowSelectionFilters);

            this.AssertEvent(eventController, DateTime.MinValue, DateTime.MaxValue, 5);
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.FeatherTeam)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events_Combined_NextWeekOrLastMonth()
        {
            ServerOperations.Events().CreateEvent("UpcomingEventInNextWeek", string.Empty, false, DateTime.Now.AddDays(4), DateTime.Now.AddDays(6));
            ServerOperations.Events().CreateEvent("UpcomingEventInMoreThanNextWeek", string.Empty, false, DateTime.Now.AddDays(9), DateTime.Now.AddDays(12));
            ServerOperations.Events().CreateEvent("PastEventInLastMonth", string.Empty, false, DateTime.Now.AddDays(-22), DateTime.Now.AddDays(-20));
            ServerOperations.Events().CreateEvent("PastEventInMoreThanLastMonth", string.Empty, false, DateTime.Now.AddDays(-42), DateTime.Now.AddDays(-40));

            var eventController = new EventSchedulerController();
            this.ApplyFilters(eventController, SelectionMode.FilteredItems, NextWeekOrLastMonthAdditionalFilters, SelectionMode.AllItems, DefaultNarrowSelectionFilters);

            this.AssertEvent(eventController, DateTime.MinValue, DateTime.MaxValue, 5);
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.FeatherTeam)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events_Combined_NextWeekOrLastYear()
        {
            ServerOperations.Events().CreateEvent("UpcomingEventInNextWeek", string.Empty, false, DateTime.Now.AddDays(4), DateTime.Now.AddDays(6));
            ServerOperations.Events().CreateEvent("UpcomingEventInMoreThanNextWeek", string.Empty, false, DateTime.Now.AddDays(9), DateTime.Now.AddDays(12));
            ServerOperations.Events().CreateEvent("PastEventInLastYear", string.Empty, false, DateTime.Now.AddDays(-368), DateTime.Now.AddDays(-365));
            ServerOperations.Events().CreateEvent("PastEventInMoreThanLastYear", string.Empty, false, DateTime.Now.AddDays(-453), DateTime.Now.AddDays(-450));

            var eventController = new EventSchedulerController();
            this.ApplyFilters(eventController, SelectionMode.FilteredItems, NextWeekOrLastYearAdditionalFilters, SelectionMode.AllItems, DefaultNarrowSelectionFilters);

            this.AssertEvent(eventController, DateTime.MinValue, DateTime.MaxValue, 5);
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.FeatherTeam)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events_Combined_NextMonthOrLast3Days()
        {
            ServerOperations.Events().CreateEvent("UpcomingEventInNextMonth", string.Empty, false, DateTime.Now.AddDays(20), DateTime.Now.AddDays(22));
            ServerOperations.Events().CreateEvent("UpcomingEventInMoreThanNextMonth", string.Empty, false, DateTime.Now.AddDays(40), DateTime.Now.AddDays(42));
            ServerOperations.Events().CreateEvent("PastEventInLast3Days", string.Empty, false, DateTime.Now.AddDays(-2), DateTime.Now.AddDays(-1));
            ServerOperations.Events().CreateEvent("PastEventInMoreThanLast3Days", string.Empty, false, DateTime.Now.AddDays(-6), DateTime.Now.AddDays(-4));

            var eventController = new EventSchedulerController();
            this.ApplyFilters(eventController, SelectionMode.FilteredItems, NextMonthOrLast3DaysAdditionalFilters, SelectionMode.AllItems, DefaultNarrowSelectionFilters);

            this.AssertEvent(eventController, DateTime.MinValue, DateTime.MaxValue, 5);
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.FeatherTeam)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events_Combined_NextMonthOrLastWeek()
        {
            ServerOperations.Events().CreateEvent("UpcomingEventInNextMonth", string.Empty, false, DateTime.Now.AddDays(20), DateTime.Now.AddDays(22));
            ServerOperations.Events().CreateEvent("UpcomingEventInMoreThanNextMonth", string.Empty, false, DateTime.Now.AddDays(40), DateTime.Now.AddDays(42));
            ServerOperations.Events().CreateEvent("PastEventInLastWeek", string.Empty, false, DateTime.Now.AddDays(-6), DateTime.Now.AddDays(-4));
            ServerOperations.Events().CreateEvent("PastEventInMoreThanLastWeek", string.Empty, false, DateTime.Now.AddDays(-12), DateTime.Now.AddDays(-9));

            var eventController = new EventSchedulerController();
            this.ApplyFilters(eventController, SelectionMode.FilteredItems, NextMonthOrLastWeekAdditionalFilters, SelectionMode.AllItems, DefaultNarrowSelectionFilters);

            this.AssertEvent(eventController, DateTime.MinValue, DateTime.MaxValue, 5);
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.FeatherTeam)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events_Combined_NextMonthOrLastMonth()
        {
            ServerOperations.Events().CreateEvent("UpcomingEventInNextMonth", string.Empty, false, DateTime.Now.AddDays(20), DateTime.Now.AddDays(22));
            ServerOperations.Events().CreateEvent("UpcomingEventInMoreThanNextMonth", string.Empty, false, DateTime.Now.AddDays(40), DateTime.Now.AddDays(42));
            ServerOperations.Events().CreateEvent("PastEventInLastMonth", string.Empty, false, DateTime.Now.AddDays(-22), DateTime.Now.AddDays(-20));
            ServerOperations.Events().CreateEvent("PastEventInMoreThanLastMonth", string.Empty, false, DateTime.Now.AddDays(-42), DateTime.Now.AddDays(-40));

            var eventController = new EventSchedulerController();
            this.ApplyFilters(eventController, SelectionMode.FilteredItems, NextMonthOrLastMonthAdditionalFilters, SelectionMode.AllItems, DefaultNarrowSelectionFilters);

            this.AssertEvent(eventController, DateTime.MinValue, DateTime.MaxValue, 5);
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.FeatherTeam)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events_Combined_NextMonthOrLastYear()
        {
            ServerOperations.Events().CreateEvent("UpcomingEventInNextMonth", string.Empty, false, DateTime.Now.AddDays(20), DateTime.Now.AddDays(22));
            ServerOperations.Events().CreateEvent("UpcomingEventInMoreThanNextMonth", string.Empty, false, DateTime.Now.AddDays(40), DateTime.Now.AddDays(42));
            ServerOperations.Events().CreateEvent("PastEventInLastYear", string.Empty, false, DateTime.Now.AddDays(-368), DateTime.Now.AddDays(-365));
            ServerOperations.Events().CreateEvent("PastEventInMoreThanLastYear", string.Empty, false, DateTime.Now.AddDays(-453), DateTime.Now.AddDays(-450));

            var eventController = new EventSchedulerController();
            this.ApplyFilters(eventController, SelectionMode.FilteredItems, NextMonthOrLastYearAdditionalFilters, SelectionMode.AllItems, DefaultNarrowSelectionFilters);

            this.AssertEvent(eventController, DateTime.MinValue, DateTime.MaxValue, 5);
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.FeatherTeam)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events_Combined_NextYearOrLast3Days()
        {
            ServerOperations.Events().CreateEvent("UpcomingEventInNextYear", string.Empty, false, DateTime.Now.AddDays(365), DateTime.Now.AddDays(368));
            ServerOperations.Events().CreateEvent("UpcomingEventInMoreThanNextYear", string.Empty, false, DateTime.Now.AddDays(450), DateTime.Now.AddDays(453));
            ServerOperations.Events().CreateEvent("PastEventInLast3Days", string.Empty, false, DateTime.Now.AddDays(-2), DateTime.Now.AddDays(-1));
            ServerOperations.Events().CreateEvent("PastEventInMoreThanLast3Days", string.Empty, false, DateTime.Now.AddDays(-6), DateTime.Now.AddDays(-4));

            var eventController = new EventSchedulerController();
            this.ApplyFilters(eventController, SelectionMode.FilteredItems, NextYearOrLast3DaysAdditionalFilters, SelectionMode.AllItems, DefaultNarrowSelectionFilters);

            this.AssertEvent(eventController, DateTime.MinValue, DateTime.MaxValue, 5);
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.FeatherTeam)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events_Combined_NextYearOrLastWeek()
        {
            ServerOperations.Events().CreateEvent("UpcomingEventInNextYear", string.Empty, false, DateTime.Now.AddDays(365), DateTime.Now.AddDays(368));
            ServerOperations.Events().CreateEvent("UpcomingEventInMoreThanNextYear", string.Empty, false, DateTime.Now.AddDays(450), DateTime.Now.AddDays(453));
            ServerOperations.Events().CreateEvent("PastEventInLastWeek", string.Empty, false, DateTime.Now.AddDays(-6), DateTime.Now.AddDays(-4));
            ServerOperations.Events().CreateEvent("PastEventInMoreThanLastWeek", string.Empty, false, DateTime.Now.AddDays(-12), DateTime.Now.AddDays(-9));

            var eventController = new EventSchedulerController();
            this.ApplyFilters(eventController, SelectionMode.FilteredItems, NextYearOrLastWeekAdditionalFilters, SelectionMode.AllItems, DefaultNarrowSelectionFilters);

            this.AssertEvent(eventController, DateTime.MinValue, DateTime.MaxValue, 5);
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.FeatherTeam)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events_Combined_NextYearOrLastMonth()
        {
            ServerOperations.Events().CreateEvent("UpcomingEventInNextYear", string.Empty, false, DateTime.Now.AddDays(365), DateTime.Now.AddDays(368));
            ServerOperations.Events().CreateEvent("UpcomingEventInMoreThanNextYear", string.Empty, false, DateTime.Now.AddDays(450), DateTime.Now.AddDays(453));
            ServerOperations.Events().CreateEvent("PastEventInLastMonth", string.Empty, false, DateTime.Now.AddDays(-22), DateTime.Now.AddDays(-20));
            ServerOperations.Events().CreateEvent("PastEventInMoreThanLastMonth", string.Empty, false, DateTime.Now.AddDays(-42), DateTime.Now.AddDays(-40));

            var eventController = new EventSchedulerController();
            this.ApplyFilters(eventController, SelectionMode.FilteredItems, NextYearOrLastMonthAdditionalFilters, SelectionMode.AllItems, DefaultNarrowSelectionFilters);

            this.AssertEvent(eventController, DateTime.MinValue, DateTime.MaxValue, 5);
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.FeatherTeam)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events_Combined_NextYearOrLastYear()
        {
            ServerOperations.Events().CreateEvent("UpcomingEventInNextYear", string.Empty, false, DateTime.Now.AddDays(365), DateTime.Now.AddDays(368));
            ServerOperations.Events().CreateEvent("UpcomingEventInMoreThanNextYear", string.Empty, false, DateTime.Now.AddDays(450), DateTime.Now.AddDays(453));
            ServerOperations.Events().CreateEvent("PastEventInLastYear", string.Empty, false, DateTime.Now.AddDays(-368), DateTime.Now.AddDays(-365));
            ServerOperations.Events().CreateEvent("PastEventInMoreThanLastYear", string.Empty, false, DateTime.Now.AddDays(-453), DateTime.Now.AddDays(-450));

            var eventController = new EventSchedulerController();
            this.ApplyFilters(eventController, SelectionMode.FilteredItems, NextYearOrLastYearAdditionalFilters, SelectionMode.AllItems, DefaultNarrowSelectionFilters);

            this.AssertEvent(eventController, DateTime.MinValue, DateTime.MaxValue, 5);
        }

        #endregion

        #region Specified combined events - Multilingual

        #endregion

        #region Calendars

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.FeatherTeam)]
        [Description("Ensure that after calendar request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Calendar()
        {
            var manager = EventsManager.GetManager();
            var calendarCount = 0;
            var calendarTitle1 = "New custom calendar 1";
            var calendarTitle2 = "New custom calendar 2";

            var calendartDefault = manager.GetCalendars().FirstOrDefault();
            if (this.SetCalendarToEvent(manager, BaseUpcomingEventTitle, calendartDefault.Id))
            {
                calendarCount++;
            }

            var calendar1Id = ServerOperations.Events().CreateCalendar(Guid.NewGuid(), calendarTitle1);
            if (this.SetCalendarToEvent(manager, BaseRepeatEventTitle, calendar1Id))
            {
                this.calendarList.Add(calendar1Id);
                calendarCount++;
            }

            var calendar2Id = ServerOperations.Events().CreateCalendar(Guid.NewGuid(), calendarTitle2);
            if (this.SetCalendarToEvent(manager, BasePastEventTitle, calendar2Id))
            {
                this.calendarList.Add(calendar2Id);
                calendarCount++;
            }

            var eventController = new EventSchedulerController();
            this.ApplyFilters(eventController, SelectionMode.AllItems, DefaultAdditionalFilters, SelectionMode.AllItems, DefaultNarrowSelectionFilters);

            this.AssertCalendar(eventController, DateTime.MinValue, DateTime.MaxValue, calendarCount);
        }

        [Test]
        [Multilingual]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.FeatherTeam)]
        [Description("Ensure that after calendar request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Calendar_Multilingual()
        {
            var multiOperations = new MultilingualEventOperations();
            var calendarCount = 0;
            var calendarTitle1 = "New custom calendar 1";
            var calendarTitle2 = "New custom calendar 2";
            var eventTitle1 = "Event 1 bg";
            var eventTitle2 = "Event 2 bg";
            var event1Id = Guid.NewGuid();
            var event2Id = Guid.NewGuid();

            var bulgarian = AppSettings.CurrentSettings.DefinedFrontendLanguages.Where(x => x.Name == "bg-BG").FirstOrDefault();

            var calendar1Id = ServerOperations.Events().CreateCalendar(Guid.NewGuid(), calendarTitle1, bulgarian.Name);
            multiOperations.CreateLocalizedEvent(event1Id, calendar1Id, eventTitle1, string.Empty, string.Empty, DateTime.Now, DateTime.Now.AddDays(1), string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, false, false, bulgarian.Name);
            if (calendar1Id != Guid.Empty)
            {
                this.calendarList.Add(calendar1Id);
                calendarCount++;
            }

            var calendar2Id = ServerOperations.Events().CreateCalendar(Guid.NewGuid(), calendarTitle2, bulgarian.Name);
            multiOperations.CreateLocalizedEvent(event2Id, calendar2Id, eventTitle2, string.Empty, string.Empty, DateTime.Now, DateTime.Now.AddDays(1), string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, false, false, bulgarian.Name);
            if (calendar2Id != Guid.Empty)
            {
                this.calendarList.Add(calendar2Id);
                calendarCount++;
            }

            var eventController = new EventSchedulerController();
            this.ApplyFilters(eventController, SelectionMode.AllItems, DefaultAdditionalFilters, SelectionMode.AllItems, DefaultNarrowSelectionFilters);

            this.AssertCalendar(eventController, DateTime.MinValue, DateTime.MaxValue, calendarCount, bulgarian);
        }

        #endregion

        #region Calendars - Multilingual

        #endregion

        #region Helper functions

        private void AssertCalendar(EventSchedulerController controller, DateTime startDate, DateTime endDate, long expectedCalendarCount, CultureInfo culture = null)
        {
            var queryStringValue = this.CreateModelQueryString(controller.Model, startDate, endDate, culture);
            var pageContent = WebRequestHelper.GetPageWebContent(UrlPath.ResolveAbsoluteUrl("~/web-interface/calendars/?") + queryStringValue);
            Assert.DoesNotContain(pageContent, JsonExceptionMessage, JsonExceptionMessage);
            List<EventCalendarViewModel> jsonCalendarsList = JsonSerializer.DeserializeFromString<List<EventCalendarViewModel>>(pageContent);
            Assert.AreEqual(jsonCalendarsList.LongCount(), expectedCalendarCount);
        }

        private void AssertEvent(EventSchedulerController controller, DateTime startDate, DateTime endDate, long expectedEventOccurrences, CultureInfo culture = null)
        {
            var queryStringValue = this.CreateModelQueryString(controller.Model, startDate, endDate, culture);
            var pageContent = WebRequestHelper.GetPageWebContent(UrlPath.ResolveAbsoluteUrl("~/web-interface/events/?") + queryStringValue);
            Assert.DoesNotContain(pageContent, JsonExceptionMessage, JsonExceptionMessage);
            List<EventOccurrenceViewModel> eventOccurrenceList = JsonSerializer.DeserializeFromString<List<EventOccurrenceViewModel>>(pageContent);
            Assert.AreEqual(eventOccurrenceList.LongCount(), expectedEventOccurrences);
        }

        private string CreateModelQueryString(IEventSchedulerModel model, DateTime startDate, DateTime endDate, CultureInfo culture = null)
        {
            var queryStringDictionary = this.CreateQueryStringDictionary(model);
            queryStringDictionary["StartDate"] = HttpUtility.UrlEncode(startDate.ToString("o", System.Globalization.CultureInfo.InvariantCulture));
            queryStringDictionary["EndDate"] = HttpUtility.UrlEncode((endDate == DateTime.MaxValue ? DateTime.MaxValue.AddDays(-1) : endDate).ToString("o", System.Globalization.CultureInfo.InvariantCulture));

            if (culture != null)
            {
                queryStringDictionary["UiCulture"] = culture.Name;
            }

            var queryStringValue = string.Join("&", queryStringDictionary.Select(t => t.Key + "=" + t.Value).ToArray());
            return queryStringValue;
        }

        private void ApplyFilters(EventSchedulerController eventController, SelectionMode additionalFiltersSelectionMode, string additionalFilters, SelectionMode narrowFiltersSelectionMode, string narrowSelectionFilters, List<Guid> selectedItemsIds = null)
        {
            eventController.Model.SelectionMode = additionalFiltersSelectionMode;
            eventController.Model.SerializedAdditionalFilters = additionalFilters;
            eventController.Model.NarrowSelectionMode = narrowFiltersSelectionMode;
            eventController.Model.SerializedNarrowSelectionFilters = narrowSelectionFilters;

            if (selectedItemsIds != null)
            {
                eventController.Model.SerializedSelectedItemsIds = JsonSerializer.SerializeToString<List<Guid>>(selectedItemsIds);
            }
        }

        private void CreateLocalizedEvent(MultilingualEventOperations multiOperations, string eventTitle, Guid eventId, DateTime start, DateTime end, bool allDay, bool isRecurrent, Telerik.Sitefinity.Events.Model.Calendar calendar, CultureInfo culture = null, string recurrenceExpression = null)
        {
            multiOperations.CreateLocalizedEvent(eventId, calendar.Id, eventTitle, string.Empty, string.Empty, start, end, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, isRecurrent, false, culture == null ? null : culture.Name);

            var eventsManager = EventsManager.GetManager();
            var eventItem = eventsManager.GetEvent(eventId);

            if (eventItem == null)
            {
                return;
            }

            if (allDay)
            {
                eventItem.AllDayEvent = true;
                eventItem.TimeZoneId = TimeZoneInfo.Utc.Id;
            }

            if (isRecurrent && !string.IsNullOrWhiteSpace(recurrenceExpression))
            {               
                eventItem.RecurrenceExpression = recurrenceExpression;
            }

            eventsManager.Lifecycle.Publish(eventItem, culture);
            eventsManager.SaveChanges();
        }

        private bool SetCalendarToEvent(EventsManager manager, string eventTitle, Guid calendarId)
        {
            var selectedEvent = manager.GetEvents().Where(i => i.Title == eventTitle).FirstOrDefault();
            var selectedCalendar = manager.GetCalendar(calendarId);
            if (selectedEvent != null && selectedCalendar != null)
            {
                selectedEvent.Parent = selectedCalendar;
                manager.Lifecycle.Publish(selectedEvent);
                manager.SaveChanges();
                return true;
            }

            return false;
        }

        private Dictionary<string, string> CreateQueryStringDictionary(IEventSchedulerModel model)
        {
            var queryStringList = model.GetType().GetProperties()
                .Where(p => p.GetValue(model, null) != null)
                .Select(property => new { Key = property.Name, Value = HttpUtility.UrlEncode(property.GetValue(model, null).ToString()) })
                .ToDictionary(t => t.Key, t => t.Value);

            return queryStringList;
        }

        private void InitializeSitefinityLanguages()
        {
            var english = AppSettings.CurrentSettings.DefinedFrontendLanguages.Where(x => x.Name == "en-US").FirstOrDefault();
            var turkish = AppSettings.CurrentSettings.DefinedFrontendLanguages.Where(x => x.Name == "tr-TR").FirstOrDefault();
            var arabic = AppSettings.CurrentSettings.DefinedFrontendLanguages.Where(x => x.Name == "ar-MA").FirstOrDefault();
            var serbian = AppSettings.CurrentSettings.DefinedFrontendLanguages.Where(x => x.Name == "sr-Cyrl-BA").FirstOrDefault();
            var bulgarian = AppSettings.CurrentSettings.DefinedFrontendLanguages.Where(x => x.Name == "bg-BG").FirstOrDefault();

            this.sitefinityLanguages.Add("English", english);
            this.sitefinityLanguages.Add("Turkish", turkish);
            this.sitefinityLanguages.Add("Arabic", arabic);
            this.sitefinityLanguages.Add("Serbian", serbian);
            this.sitefinityLanguages.Add("Bulgarian", bulgarian);
        }

        #endregion

        #region Filters

        private const string DefaultNarrowSelectionFilters = "{\"__msdisposeindex\":257,\"Title\":null,\"Id\":\"00000000-0000-0000-0000-000000000000\",\"QueryItems\":[],\"TypeProperties\":[],\"_itemPathSeparator\":\"_\"}";
        private const string DefaultAdditionalFilters = "{\"__msdisposeindex\":256,\"Title\":null,\"Id\":\"00000000-0000-0000-0000-000000000000\",\"QueryItems\":[],\"TypeProperties\":[],\"_itemPathSeparator\":\"_\"}";
        private const string UpcomingAdditionalFilters = "{\"__msdisposeindex\":198,\"Title\":null,\"Id\":\"00000000-0000-0000-0000-000000000000\",\"QueryItems\":[{\"IsGroup\":true,\"Ordinal\":0,\"Join\":\"OR\",\"ItemPath\":\"_0\",\"Value\":null,\"Condition\":{\"FieldName\":null,\"FieldType\":null,\"Operator\":null,\"__msdisposeindex\":200},\"Name\":\"Upcoming\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":199},{\"IsGroup\":false,\"Ordinal\":0,\"Join\":\"AND\",\"ItemPath\":\"_0_0\",\"Value\":\"DateTime.UtcNow\",\"Condition\":{\"FieldName\":\"EventStart\",\"FieldType\":\"System.DateTime\",\"Operator\":\">=\",\"__msdisposeindex\":202},\"Name\":null,\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":201}],\"TypeProperties\":[],\"_itemPathSeparator\":\"_\"}";
        private const string PastAdditionalFilters = "{\"__msdisposeindex\":283,\"Title\":null,\"Id\":\"00000000-0000-0000-0000-000000000000\",\"QueryItems\":[{\"IsGroup\":true,\"Ordinal\":0,\"Join\":\"OR\",\"ItemPath\":\"_0\",\"Value\":null,\"Condition\":{\"FieldName\":null,\"FieldType\":null,\"Operator\":null,\"__msdisposeindex\":285},\"Name\":\"Past\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":284},{\"IsGroup\":false,\"Ordinal\":0,\"Join\":\"AND\",\"ItemPath\":\"_0_0\",\"Value\":\"DateTime.UtcNow\",\"Condition\":{\"FieldName\":\"EventEnd\",\"FieldType\":\"System.DateTime\",\"Operator\":\"<=\",\"__msdisposeindex\":287},\"Name\":null,\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":286}],\"TypeProperties\":[],\"_itemPathSeparator\":\"_\"}";
        private const string CurrentAdditionalFilters = "{\"__msdisposeindex\":270,\"Title\":null,\"Id\":\"00000000-0000-0000-0000-000000000000\",\"QueryItems\":[{\"IsGroup\":true,\"Ordinal\":0,\"Join\":\"OR\",\"ItemPath\":\"_0\",\"Value\":null,\"Condition\":{\"FieldName\":null,\"FieldType\":null,\"Operator\":null,\"__msdisposeindex\":272},\"Name\":\"Current\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":271},{\"IsGroup\":false,\"Ordinal\":0,\"Join\":\"AND\",\"ItemPath\":\"_0_0\",\"Value\":\"DateTime.UtcNow\",\"Condition\":{\"FieldName\":\"EventStart\",\"FieldType\":\"System.DateTime\",\"Operator\":\"<=\",\"__msdisposeindex\":274},\"Name\":null,\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":273},{\"IsGroup\":false,\"Ordinal\":1,\"Join\":\"AND\",\"ItemPath\":\"_0_1\",\"Value\":\"DateTime.UtcNow\",\"Condition\":{\"FieldName\":\"EventEnd\",\"FieldType\":\"System.DateTime\",\"Operator\":\">=\",\"__msdisposeindex\":276},\"Name\":null,\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":275}],\"TypeProperties\":[],\"_itemPathSeparator\":\"_\"}";
        private const string UpcomingAndCurrentAdditionalFilters = "{\"__msdisposeindex\":289,\"Title\":null,\"Id\":\"00000000-0000-0000-0000-000000000000\",\"QueryItems\":[{\"IsGroup\":true,\"Ordinal\":0,\"Join\":\"OR\",\"ItemPath\":\"_0\",\"Value\":null,\"Condition\":{\"FieldName\":null,\"FieldType\":null,\"Operator\":null,\"__msdisposeindex\":291},\"Name\":\"Current\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":290},{\"IsGroup\":false,\"Ordinal\":0,\"Join\":\"AND\",\"ItemPath\":\"_0_0\",\"Value\":\"DateTime.UtcNow\",\"Condition\":{\"FieldName\":\"EventStart\",\"FieldType\":\"System.DateTime\",\"Operator\":\"<=\",\"__msdisposeindex\":293},\"Name\":null,\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":292},{\"IsGroup\":false,\"Ordinal\":1,\"Join\":\"AND\",\"ItemPath\":\"_0_1\",\"Value\":\"DateTime.UtcNow\",\"Condition\":{\"FieldName\":\"EventEnd\",\"FieldType\":\"System.DateTime\",\"Operator\":\">=\",\"__msdisposeindex\":295},\"Name\":null,\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":294},{\"IsGroup\":true,\"Ordinal\":1,\"Join\":\"OR\",\"ItemPath\":\"_1\",\"Value\":null,\"Condition\":{\"FieldName\":null,\"FieldType\":null,\"Operator\":null,\"__msdisposeindex\":297},\"Name\":\"Upcoming\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":296},{\"IsGroup\":false,\"Ordinal\":0,\"Join\":\"AND\",\"ItemPath\":\"_1_0\",\"Value\":\"DateTime.UtcNow\",\"Condition\":{\"FieldName\":\"EventStart\",\"FieldType\":\"System.DateTime\",\"Operator\":\">=\",\"__msdisposeindex\":299},\"Name\":null,\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":298}],\"TypeProperties\":[],\"_itemPathSeparator\":\"_\"}";
        private const string PastAndUpcomingAdditionalFilters = "{\"__msdisposeindex\":316,\"Title\":null,\"Id\":\"00000000-0000-0000-0000-000000000000\",\"QueryItems\":[{\"IsGroup\":true,\"Ordinal\":0,\"Join\":\"OR\",\"ItemPath\":\"_0\",\"Value\":null,\"Condition\":{\"FieldName\":null,\"FieldType\":null,\"Operator\":null,\"__msdisposeindex\":318},\"Name\":\"Upcoming\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":317},{\"IsGroup\":false,\"Ordinal\":0,\"Join\":\"AND\",\"ItemPath\":\"_0_0\",\"Value\":\"DateTime.UtcNow\",\"Condition\":{\"FieldName\":\"EventStart\",\"FieldType\":\"System.DateTime\",\"Operator\":\">=\",\"__msdisposeindex\":320},\"Name\":null,\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":319},{\"IsGroup\":true,\"Ordinal\":1,\"Join\":\"OR\",\"ItemPath\":\"_1\",\"Value\":null,\"Condition\":{\"FieldName\":null,\"FieldType\":null,\"Operator\":null,\"__msdisposeindex\":322},\"Name\":\"Past\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":321},{\"IsGroup\":false,\"Ordinal\":0,\"Join\":\"AND\",\"ItemPath\":\"_1_0\",\"Value\":\"DateTime.UtcNow\",\"Condition\":{\"FieldName\":\"EventEnd\",\"FieldType\":\"System.DateTime\",\"Operator\":\"<=\",\"__msdisposeindex\":324},\"Name\":null,\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":323}],\"TypeProperties\":[],\"_itemPathSeparator\":\"_\"}";
        private const string PastAndCurrentAdditionalFilters = "{\"__msdisposeindex\":341,\"Title\":null,\"Id\":\"00000000-0000-0000-0000-000000000000\",\"QueryItems\":[{\"IsGroup\":true,\"Ordinal\":0,\"Join\":\"OR\",\"ItemPath\":\"_0\",\"Value\":null,\"Condition\":{\"FieldName\":null,\"FieldType\":null,\"Operator\":null,\"__msdisposeindex\":343},\"Name\":\"Past\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":342},{\"IsGroup\":false,\"Ordinal\":0,\"Join\":\"AND\",\"ItemPath\":\"_0_0\",\"Value\":\"DateTime.UtcNow\",\"Condition\":{\"FieldName\":\"EventEnd\",\"FieldType\":\"System.DateTime\",\"Operator\":\"<=\",\"__msdisposeindex\":345},\"Name\":null,\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":344},{\"IsGroup\":true,\"Ordinal\":1,\"Join\":\"OR\",\"ItemPath\":\"_1\",\"Value\":null,\"Condition\":{\"FieldName\":null,\"FieldType\":null,\"Operator\":null,\"__msdisposeindex\":347},\"Name\":\"Current\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":346},{\"IsGroup\":false,\"Ordinal\":0,\"Join\":\"AND\",\"ItemPath\":\"_1_0\",\"Value\":\"DateTime.UtcNow\",\"Condition\":{\"FieldName\":\"EventStart\",\"FieldType\":\"System.DateTime\",\"Operator\":\"<=\",\"__msdisposeindex\":349},\"Name\":null,\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":348},{\"IsGroup\":false,\"Ordinal\":1,\"Join\":\"AND\",\"ItemPath\":\"_1_1\",\"Value\":\"DateTime.UtcNow\",\"Condition\":{\"FieldName\":\"EventEnd\",\"FieldType\":\"System.DateTime\",\"Operator\":\">=\",\"__msdisposeindex\":351},\"Name\":null,\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":350}],\"TypeProperties\":[],\"_itemPathSeparator\":\"_\"}";

        private const string Next3DaysAdditionalFilters = "{\"__msdisposeindex\":343,\"Title\":null,\"Id\":\"00000000-0000-0000-0000-000000000000\",\"QueryItems\":[{\"IsGroup\":true,\"Ordinal\":0,\"Join\":\"OR\",\"ItemPath\":\"_0\",\"Value\":null,\"Condition\":{\"FieldName\":null,\"FieldType\":null,\"Operator\":null,\"__msdisposeindex\":345},\"Name\":\"Upcoming\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":344},{\"IsGroup\":false,\"Ordinal\":0,\"Join\":\"AND\",\"ItemPath\":\"_0_0\",\"Value\":\"DateTime.UtcNow.AddDays(3.0)\",\"Condition\":{\"FieldName\":\"EventStart\",\"FieldType\":\"System.DateTime\",\"Operator\":\"<=\",\"__msdisposeindex\":347},\"Name\":\"EventStart.DateTime.UtcNow.AddDays(3.0)\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":346},{\"IsGroup\":false,\"Ordinal\":1,\"Join\":\"AND\",\"ItemPath\":\"_0_1\",\"Value\":\"DateTime.UtcNow\",\"Condition\":{\"FieldName\":\"EventStart\",\"FieldType\":\"System.DateTime\",\"Operator\":\">=\",\"__msdisposeindex\":349},\"Name\":null,\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":348}],\"TypeProperties\":[],\"_itemPathSeparator\":\"_\"}";
        private const string NextWeekAdditionalFilters = "{\"__msdisposeindex\":364,\"Title\":null,\"Id\":\"00000000-0000-0000-0000-000000000000\",\"QueryItems\":[{\"IsGroup\":true,\"Ordinal\":0,\"Join\":\"OR\",\"ItemPath\":\"_0\",\"Value\":null,\"Condition\":{\"FieldName\":null,\"FieldType\":null,\"Operator\":null,\"__msdisposeindex\":366},\"Name\":\"Upcoming\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":365},{\"IsGroup\":false,\"Ordinal\":0,\"Join\":\"AND\",\"ItemPath\":\"_0_0\",\"Value\":\"DateTime.UtcNow.AddDays(7.0)\",\"Condition\":{\"FieldName\":\"EventStart\",\"FieldType\":\"System.DateTime\",\"Operator\":\"<=\",\"__msdisposeindex\":368},\"Name\":\"EventStart.DateTime.UtcNow.AddDays(7.0)\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":367},{\"IsGroup\":false,\"Ordinal\":1,\"Join\":\"AND\",\"ItemPath\":\"_0_1\",\"Value\":\"DateTime.UtcNow\",\"Condition\":{\"FieldName\":\"EventStart\",\"FieldType\":\"System.DateTime\",\"Operator\":\">=\",\"__msdisposeindex\":370},\"Name\":null,\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":369}],\"TypeProperties\":[],\"_itemPathSeparator\":\"_\"}";
        private const string NextMonthAdditionalFilters = "{\"__msdisposeindex\":385,\"Title\":null,\"Id\":\"00000000-0000-0000-0000-000000000000\",\"QueryItems\":[{\"IsGroup\":true,\"Ordinal\":0,\"Join\":\"OR\",\"ItemPath\":\"_0\",\"Value\":null,\"Condition\":{\"FieldName\":null,\"FieldType\":null,\"Operator\":null,\"__msdisposeindex\":387},\"Name\":\"Upcoming\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":386},{\"IsGroup\":false,\"Ordinal\":0,\"Join\":\"AND\",\"ItemPath\":\"_0_0\",\"Value\":\"DateTime.UtcNow.AddMonths(1)\",\"Condition\":{\"FieldName\":\"EventStart\",\"FieldType\":\"System.DateTime\",\"Operator\":\"<=\",\"__msdisposeindex\":389},\"Name\":\"EventStart.DateTime.UtcNow.AddMonths(1)\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":388},{\"IsGroup\":false,\"Ordinal\":1,\"Join\":\"AND\",\"ItemPath\":\"_0_1\",\"Value\":\"DateTime.UtcNow\",\"Condition\":{\"FieldName\":\"EventStart\",\"FieldType\":\"System.DateTime\",\"Operator\":\">=\",\"__msdisposeindex\":391},\"Name\":null,\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":390}],\"TypeProperties\":[],\"_itemPathSeparator\":\"_\"}";
        private const string NextYearAdditionalFilters = "{\"__msdisposeindex\":406,\"Title\":null,\"Id\":\"00000000-0000-0000-0000-000000000000\",\"QueryItems\":[{\"IsGroup\":true,\"Ordinal\":0,\"Join\":\"OR\",\"ItemPath\":\"_0\",\"Value\":null,\"Condition\":{\"FieldName\":null,\"FieldType\":null,\"Operator\":null,\"__msdisposeindex\":408},\"Name\":\"Upcoming\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":407},{\"IsGroup\":false,\"Ordinal\":0,\"Join\":\"AND\",\"ItemPath\":\"_0_0\",\"Value\":\"DateTime.UtcNow.AddYears(1)\",\"Condition\":{\"FieldName\":\"EventStart\",\"FieldType\":\"System.DateTime\",\"Operator\":\"<=\",\"__msdisposeindex\":410},\"Name\":\"EventStart.DateTime.UtcNow.AddYears(1)\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":409},{\"IsGroup\":false,\"Ordinal\":1,\"Join\":\"AND\",\"ItemPath\":\"_0_1\",\"Value\":\"DateTime.UtcNow\",\"Condition\":{\"FieldName\":\"EventStart\",\"FieldType\":\"System.DateTime\",\"Operator\":\">=\",\"__msdisposeindex\":412},\"Name\":null,\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":411}],\"TypeProperties\":[],\"_itemPathSeparator\":\"_\"}";
        private const string NextCustomRangeAdditionalFilters = "";

        private const string Last3DaysAdditionalFilters = "{\"__msdisposeindex\":278,\"Title\":null,\"Id\":\"00000000-0000-0000-0000-000000000000\",\"QueryItems\":[{\"IsGroup\":true,\"Ordinal\":0,\"Join\":\"OR\",\"ItemPath\":\"_0\",\"Value\":null,\"Condition\":{\"FieldName\":null,\"FieldType\":null,\"Operator\":null,\"__msdisposeindex\":280},\"Name\":\"Past\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":279},{\"IsGroup\":false,\"Ordinal\":0,\"Join\":\"AND\",\"ItemPath\":\"_0_0\",\"Value\":\"DateTime.UtcNow.AddDays(-3.0)\",\"Condition\":{\"FieldName\":\"EventEnd\",\"FieldType\":\"System.DateTime\",\"Operator\":\">=\",\"__msdisposeindex\":282},\"Name\":\"EventEnd.DateTime.UtcNow.AddDays(-3.0)\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":281},{\"IsGroup\":false,\"Ordinal\":1,\"Join\":\"AND\",\"ItemPath\":\"_0_1\",\"Value\":\"DateTime.UtcNow\",\"Condition\":{\"FieldName\":\"EventEnd\",\"FieldType\":\"System.DateTime\",\"Operator\":\"<=\",\"__msdisposeindex\":284},\"Name\":null,\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":283}],\"TypeProperties\":[],\"_itemPathSeparator\":\"_\"}";
        private const string LastWeekAdditionalFilters = "{\"__msdisposeindex\":299,\"Title\":null,\"Id\":\"00000000-0000-0000-0000-000000000000\",\"QueryItems\":[{\"IsGroup\":true,\"Ordinal\":0,\"Join\":\"OR\",\"ItemPath\":\"_0\",\"Value\":null,\"Condition\":{\"FieldName\":null,\"FieldType\":null,\"Operator\":null,\"__msdisposeindex\":301},\"Name\":\"Past\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":300},{\"IsGroup\":false,\"Ordinal\":0,\"Join\":\"AND\",\"ItemPath\":\"_0_0\",\"Value\":\"DateTime.UtcNow.AddDays(-7.0)\",\"Condition\":{\"FieldName\":\"EventEnd\",\"FieldType\":\"System.DateTime\",\"Operator\":\">=\",\"__msdisposeindex\":303},\"Name\":\"EventEnd.DateTime.UtcNow.AddDays(-7.0)\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":302},{\"IsGroup\":false,\"Ordinal\":1,\"Join\":\"AND\",\"ItemPath\":\"_0_1\",\"Value\":\"DateTime.UtcNow\",\"Condition\":{\"FieldName\":\"EventEnd\",\"FieldType\":\"System.DateTime\",\"Operator\":\"<=\",\"__msdisposeindex\":305},\"Name\":null,\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":304}],\"TypeProperties\":[],\"_itemPathSeparator\":\"_\"}";
        private const string LastMonthAdditionalFilters = "{\"__msdisposeindex\":320,\"Title\":null,\"Id\":\"00000000-0000-0000-0000-000000000000\",\"QueryItems\":[{\"IsGroup\":true,\"Ordinal\":0,\"Join\":\"OR\",\"ItemPath\":\"_0\",\"Value\":null,\"Condition\":{\"FieldName\":null,\"FieldType\":null,\"Operator\":null,\"__msdisposeindex\":322},\"Name\":\"Past\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":321},{\"IsGroup\":false,\"Ordinal\":0,\"Join\":\"AND\",\"ItemPath\":\"_0_0\",\"Value\":\"DateTime.UtcNow.AddMonths(-1)\",\"Condition\":{\"FieldName\":\"EventEnd\",\"FieldType\":\"System.DateTime\",\"Operator\":\">=\",\"__msdisposeindex\":324},\"Name\":\"EventEnd.DateTime.UtcNow.AddMonths(-1)\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":323},{\"IsGroup\":false,\"Ordinal\":1,\"Join\":\"AND\",\"ItemPath\":\"_0_1\",\"Value\":\"DateTime.UtcNow\",\"Condition\":{\"FieldName\":\"EventEnd\",\"FieldType\":\"System.DateTime\",\"Operator\":\"<=\",\"__msdisposeindex\":326},\"Name\":null,\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":325}],\"TypeProperties\":[],\"_itemPathSeparator\":\"_\"}";
        private const string LastYearAdditionalFilters = "{\"__msdisposeindex\":341,\"Title\":null,\"Id\":\"00000000-0000-0000-0000-000000000000\",\"QueryItems\":[{\"IsGroup\":true,\"Ordinal\":0,\"Join\":\"OR\",\"ItemPath\":\"_0\",\"Value\":null,\"Condition\":{\"FieldName\":null,\"FieldType\":null,\"Operator\":null,\"__msdisposeindex\":343},\"Name\":\"Past\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":342},{\"IsGroup\":false,\"Ordinal\":0,\"Join\":\"AND\",\"ItemPath\":\"_0_0\",\"Value\":\"DateTime.UtcNow.AddYears(-1)\",\"Condition\":{\"FieldName\":\"EventEnd\",\"FieldType\":\"System.DateTime\",\"Operator\":\">=\",\"__msdisposeindex\":345},\"Name\":\"EventEnd.DateTime.UtcNow.AddYears(-1)\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":344},{\"IsGroup\":false,\"Ordinal\":1,\"Join\":\"AND\",\"ItemPath\":\"_0_1\",\"Value\":\"DateTime.UtcNow\",\"Condition\":{\"FieldName\":\"EventEnd\",\"FieldType\":\"System.DateTime\",\"Operator\":\"<=\",\"__msdisposeindex\":347},\"Name\":null,\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":346}],\"TypeProperties\":[],\"_itemPathSeparator\":\"_\"}";
        private const string LastCustomRangeAdditionalFilters = "";

        private const string Next3DaysOrLast3DaysAdditionalFilters = "{\"__msdisposeindex\":285,\"Title\":null,\"Id\":\"00000000-0000-0000-0000-000000000000\",\"QueryItems\":[{\"IsGroup\":true,\"Ordinal\":0,\"Join\":\"OR\",\"ItemPath\":\"_0\",\"Value\":null,\"Condition\":{\"FieldName\":null,\"FieldType\":null,\"Operator\":null,\"__msdisposeindex\":287},\"Name\":\"Upcoming\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":286},{\"IsGroup\":false,\"Ordinal\":0,\"Join\":\"AND\",\"ItemPath\":\"_0_0\",\"Value\":\"DateTime.UtcNow.AddDays(3.0)\",\"Condition\":{\"FieldName\":\"EventStart\",\"FieldType\":\"System.DateTime\",\"Operator\":\"<=\",\"__msdisposeindex\":289},\"Name\":\"EventStart.DateTime.UtcNow.AddDays(3.0)\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":288},{\"IsGroup\":false,\"Ordinal\":1,\"Join\":\"AND\",\"ItemPath\":\"_0_1\",\"Value\":\"DateTime.UtcNow\",\"Condition\":{\"FieldName\":\"EventStart\",\"FieldType\":\"System.DateTime\",\"Operator\":\">=\",\"__msdisposeindex\":291},\"Name\":null,\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":290},{\"IsGroup\":true,\"Ordinal\":1,\"Join\":\"OR\",\"ItemPath\":\"_1\",\"Value\":null,\"Condition\":{\"FieldName\":null,\"FieldType\":null,\"Operator\":null,\"__msdisposeindex\":293},\"Name\":\"Past\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":292},{\"IsGroup\":false,\"Ordinal\":0,\"Join\":\"AND\",\"ItemPath\":\"_1_0\",\"Value\":\"DateTime.UtcNow.AddDays(-3.0)\",\"Condition\":{\"FieldName\":\"EventEnd\",\"FieldType\":\"System.DateTime\",\"Operator\":\">=\",\"__msdisposeindex\":295},\"Name\":\"EventEnd.DateTime.UtcNow.AddDays(-3.0)\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":294},{\"IsGroup\":false,\"Ordinal\":1,\"Join\":\"AND\",\"ItemPath\":\"_1_1\",\"Value\":\"DateTime.UtcNow\",\"Condition\":{\"FieldName\":\"EventEnd\",\"FieldType\":\"System.DateTime\",\"Operator\":\"<=\",\"__msdisposeindex\":297},\"Name\":null,\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":296}],\"TypeProperties\":[],\"_itemPathSeparator\":\"_\"}";
        private const string Next3DaysOrLastWeekAdditionalFilters = "{\"__msdisposeindex\":318,\"Title\":null,\"Id\":\"00000000-0000-0000-0000-000000000000\",\"QueryItems\":[{\"IsGroup\":true,\"Ordinal\":0,\"Join\":\"OR\",\"ItemPath\":\"_0\",\"Value\":null,\"Condition\":{\"FieldName\":null,\"FieldType\":null,\"Operator\":null,\"__msdisposeindex\":320},\"Name\":\"Upcoming\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":319},{\"IsGroup\":false,\"Ordinal\":0,\"Join\":\"AND\",\"ItemPath\":\"_0_0\",\"Value\":\"DateTime.UtcNow.AddDays(3.0)\",\"Condition\":{\"FieldName\":\"EventStart\",\"FieldType\":\"System.DateTime\",\"Operator\":\"<=\",\"__msdisposeindex\":322},\"Name\":\"EventStart.DateTime.UtcNow.AddDays(3.0)\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":321},{\"IsGroup\":false,\"Ordinal\":1,\"Join\":\"AND\",\"ItemPath\":\"_0_1\",\"Value\":\"DateTime.UtcNow\",\"Condition\":{\"FieldName\":\"EventStart\",\"FieldType\":\"System.DateTime\",\"Operator\":\">=\",\"__msdisposeindex\":324},\"Name\":null,\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":323},{\"IsGroup\":true,\"Ordinal\":1,\"Join\":\"OR\",\"ItemPath\":\"_1\",\"Value\":null,\"Condition\":{\"FieldName\":null,\"FieldType\":null,\"Operator\":null,\"__msdisposeindex\":326},\"Name\":\"Past\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":325},{\"IsGroup\":false,\"Ordinal\":0,\"Join\":\"AND\",\"ItemPath\":\"_1_0\",\"Value\":\"DateTime.UtcNow.AddDays(-7.0)\",\"Condition\":{\"FieldName\":\"EventEnd\",\"FieldType\":\"System.DateTime\",\"Operator\":\">=\",\"__msdisposeindex\":328},\"Name\":\"EventEnd.DateTime.UtcNow.AddDays(-7.0)\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":327},{\"IsGroup\":false,\"Ordinal\":1,\"Join\":\"AND\",\"ItemPath\":\"_1_1\",\"Value\":\"DateTime.UtcNow\",\"Condition\":{\"FieldName\":\"EventEnd\",\"FieldType\":\"System.DateTime\",\"Operator\":\"<=\",\"__msdisposeindex\":330},\"Name\":null,\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":329}],\"TypeProperties\":[],\"_itemPathSeparator\":\"_\"}";
        private const string Next3DaysOrLastMonthAdditionalFilters = "{\"__msdisposeindex\":351,\"Title\":null,\"Id\":\"00000000-0000-0000-0000-000000000000\",\"QueryItems\":[{\"IsGroup\":true,\"Ordinal\":0,\"Join\":\"OR\",\"ItemPath\":\"_0\",\"Value\":null,\"Condition\":{\"FieldName\":null,\"FieldType\":null,\"Operator\":null,\"__msdisposeindex\":353},\"Name\":\"Upcoming\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":352},{\"IsGroup\":false,\"Ordinal\":0,\"Join\":\"AND\",\"ItemPath\":\"_0_0\",\"Value\":\"DateTime.UtcNow.AddDays(3.0)\",\"Condition\":{\"FieldName\":\"EventStart\",\"FieldType\":\"System.DateTime\",\"Operator\":\"<=\",\"__msdisposeindex\":355},\"Name\":\"EventStart.DateTime.UtcNow.AddDays(3.0)\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":354},{\"IsGroup\":false,\"Ordinal\":1,\"Join\":\"AND\",\"ItemPath\":\"_0_1\",\"Value\":\"DateTime.UtcNow\",\"Condition\":{\"FieldName\":\"EventStart\",\"FieldType\":\"System.DateTime\",\"Operator\":\">=\",\"__msdisposeindex\":357},\"Name\":null,\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":356},{\"IsGroup\":true,\"Ordinal\":1,\"Join\":\"OR\",\"ItemPath\":\"_1\",\"Value\":null,\"Condition\":{\"FieldName\":null,\"FieldType\":null,\"Operator\":null,\"__msdisposeindex\":359},\"Name\":\"Past\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":358},{\"IsGroup\":false,\"Ordinal\":0,\"Join\":\"AND\",\"ItemPath\":\"_1_0\",\"Value\":\"DateTime.UtcNow.AddMonths(-1)\",\"Condition\":{\"FieldName\":\"EventEnd\",\"FieldType\":\"System.DateTime\",\"Operator\":\">=\",\"__msdisposeindex\":361},\"Name\":\"EventEnd.DateTime.UtcNow.AddMonths(-1)\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":360},{\"IsGroup\":false,\"Ordinal\":1,\"Join\":\"AND\",\"ItemPath\":\"_1_1\",\"Value\":\"DateTime.UtcNow\",\"Condition\":{\"FieldName\":\"EventEnd\",\"FieldType\":\"System.DateTime\",\"Operator\":\"<=\",\"__msdisposeindex\":363},\"Name\":null,\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":362}],\"TypeProperties\":[],\"_itemPathSeparator\":\"_\"}";
        private const string Next3DaysOrLastYearAdditionalFilters = "{\"__msdisposeindex\":384,\"Title\":null,\"Id\":\"00000000-0000-0000-0000-000000000000\",\"QueryItems\":[{\"IsGroup\":true,\"Ordinal\":0,\"Join\":\"OR\",\"ItemPath\":\"_0\",\"Value\":null,\"Condition\":{\"FieldName\":null,\"FieldType\":null,\"Operator\":null,\"__msdisposeindex\":386},\"Name\":\"Upcoming\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":385},{\"IsGroup\":false,\"Ordinal\":0,\"Join\":\"AND\",\"ItemPath\":\"_0_0\",\"Value\":\"DateTime.UtcNow.AddDays(3.0)\",\"Condition\":{\"FieldName\":\"EventStart\",\"FieldType\":\"System.DateTime\",\"Operator\":\"<=\",\"__msdisposeindex\":388},\"Name\":\"EventStart.DateTime.UtcNow.AddDays(3.0)\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":387},{\"IsGroup\":false,\"Ordinal\":1,\"Join\":\"AND\",\"ItemPath\":\"_0_1\",\"Value\":\"DateTime.UtcNow\",\"Condition\":{\"FieldName\":\"EventStart\",\"FieldType\":\"System.DateTime\",\"Operator\":\">=\",\"__msdisposeindex\":390},\"Name\":null,\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":389},{\"IsGroup\":true,\"Ordinal\":1,\"Join\":\"OR\",\"ItemPath\":\"_1\",\"Value\":null,\"Condition\":{\"FieldName\":null,\"FieldType\":null,\"Operator\":null,\"__msdisposeindex\":392},\"Name\":\"Past\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":391},{\"IsGroup\":false,\"Ordinal\":0,\"Join\":\"AND\",\"ItemPath\":\"_1_0\",\"Value\":\"DateTime.UtcNow.AddYears(-1)\",\"Condition\":{\"FieldName\":\"EventEnd\",\"FieldType\":\"System.DateTime\",\"Operator\":\">=\",\"__msdisposeindex\":394},\"Name\":\"EventEnd.DateTime.UtcNow.AddYears(-1)\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":393},{\"IsGroup\":false,\"Ordinal\":1,\"Join\":\"AND\",\"ItemPath\":\"_1_1\",\"Value\":\"DateTime.UtcNow\",\"Condition\":{\"FieldName\":\"EventEnd\",\"FieldType\":\"System.DateTime\",\"Operator\":\"<=\",\"__msdisposeindex\":396},\"Name\":null,\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":395}],\"TypeProperties\":[],\"_itemPathSeparator\":\"_\"}";

        private const string NextWeekOrLast3DaysAdditionalFilters = "{\"__msdisposeindex\":422,\"Title\":null,\"Id\":\"00000000-0000-0000-0000-000000000000\",\"QueryItems\":[{\"IsGroup\":true,\"Ordinal\":0,\"Join\":\"OR\",\"ItemPath\":\"_0\",\"Value\":null,\"Condition\":{\"FieldName\":null,\"FieldType\":null,\"Operator\":null,\"__msdisposeindex\":424},\"Name\":\"Upcoming\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":423},{\"IsGroup\":false,\"Ordinal\":0,\"Join\":\"AND\",\"ItemPath\":\"_0_0\",\"Value\":\"DateTime.UtcNow.AddDays(7.0)\",\"Condition\":{\"FieldName\":\"EventStart\",\"FieldType\":\"System.DateTime\",\"Operator\":\"<=\",\"__msdisposeindex\":426},\"Name\":\"EventStart.DateTime.UtcNow.AddDays(7.0)\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":425},{\"IsGroup\":false,\"Ordinal\":1,\"Join\":\"AND\",\"ItemPath\":\"_0_1\",\"Value\":\"DateTime.UtcNow\",\"Condition\":{\"FieldName\":\"EventStart\",\"FieldType\":\"System.DateTime\",\"Operator\":\">=\",\"__msdisposeindex\":428},\"Name\":null,\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":427},{\"IsGroup\":true,\"Ordinal\":1,\"Join\":\"OR\",\"ItemPath\":\"_1\",\"Value\":null,\"Condition\":{\"FieldName\":null,\"FieldType\":null,\"Operator\":null,\"__msdisposeindex\":430},\"Name\":\"Past\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":429},{\"IsGroup\":false,\"Ordinal\":0,\"Join\":\"AND\",\"ItemPath\":\"_1_0\",\"Value\":\"DateTime.UtcNow.AddDays(-3.0)\",\"Condition\":{\"FieldName\":\"EventEnd\",\"FieldType\":\"System.DateTime\",\"Operator\":\">=\",\"__msdisposeindex\":432},\"Name\":\"EventEnd.DateTime.UtcNow.AddDays(-3.0)\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":431},{\"IsGroup\":false,\"Ordinal\":1,\"Join\":\"AND\",\"ItemPath\":\"_1_1\",\"Value\":\"DateTime.UtcNow\",\"Condition\":{\"FieldName\":\"EventEnd\",\"FieldType\":\"System.DateTime\",\"Operator\":\"<=\",\"__msdisposeindex\":434},\"Name\":null,\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":433}],\"TypeProperties\":[],\"_itemPathSeparator\":\"_\"}";
        private const string NextWeekOrLastWeekAdditionalFilters = "{\"__msdisposeindex\":455,\"Title\":null,\"Id\":\"00000000-0000-0000-0000-000000000000\",\"QueryItems\":[{\"IsGroup\":true,\"Ordinal\":0,\"Join\":\"OR\",\"ItemPath\":\"_0\",\"Value\":null,\"Condition\":{\"FieldName\":null,\"FieldType\":null,\"Operator\":null,\"__msdisposeindex\":457},\"Name\":\"Upcoming\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":456},{\"IsGroup\":false,\"Ordinal\":0,\"Join\":\"AND\",\"ItemPath\":\"_0_0\",\"Value\":\"DateTime.UtcNow.AddDays(7.0)\",\"Condition\":{\"FieldName\":\"EventStart\",\"FieldType\":\"System.DateTime\",\"Operator\":\"<=\",\"__msdisposeindex\":459},\"Name\":\"EventStart.DateTime.UtcNow.AddDays(7.0)\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":458},{\"IsGroup\":false,\"Ordinal\":1,\"Join\":\"AND\",\"ItemPath\":\"_0_1\",\"Value\":\"DateTime.UtcNow\",\"Condition\":{\"FieldName\":\"EventStart\",\"FieldType\":\"System.DateTime\",\"Operator\":\">=\",\"__msdisposeindex\":461},\"Name\":null,\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":460},{\"IsGroup\":true,\"Ordinal\":1,\"Join\":\"OR\",\"ItemPath\":\"_1\",\"Value\":null,\"Condition\":{\"FieldName\":null,\"FieldType\":null,\"Operator\":null,\"__msdisposeindex\":463},\"Name\":\"Past\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":462},{\"IsGroup\":false,\"Ordinal\":0,\"Join\":\"AND\",\"ItemPath\":\"_1_0\",\"Value\":\"DateTime.UtcNow.AddDays(-7.0)\",\"Condition\":{\"FieldName\":\"EventEnd\",\"FieldType\":\"System.DateTime\",\"Operator\":\">=\",\"__msdisposeindex\":465},\"Name\":\"EventEnd.DateTime.UtcNow.AddDays(-7.0)\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":464},{\"IsGroup\":false,\"Ordinal\":1,\"Join\":\"AND\",\"ItemPath\":\"_1_1\",\"Value\":\"DateTime.UtcNow\",\"Condition\":{\"FieldName\":\"EventEnd\",\"FieldType\":\"System.DateTime\",\"Operator\":\"<=\",\"__msdisposeindex\":467},\"Name\":null,\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":466}],\"TypeProperties\":[],\"_itemPathSeparator\":\"_\"}";
        private const string NextWeekOrLastMonthAdditionalFilters = "{\"__msdisposeindex\":502,\"Title\":null,\"Id\":\"00000000-0000-0000-0000-000000000000\",\"QueryItems\":[{\"IsGroup\":true,\"Ordinal\":0,\"Join\":\"OR\",\"ItemPath\":\"_0\",\"Value\":null,\"Condition\":{\"FieldName\":null,\"FieldType\":null,\"Operator\":null,\"__msdisposeindex\":504},\"Name\":\"Upcoming\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":503},{\"IsGroup\":false,\"Ordinal\":0,\"Join\":\"AND\",\"ItemPath\":\"_0_0\",\"Value\":\"DateTime.UtcNow.AddDays(7.0)\",\"Condition\":{\"FieldName\":\"EventStart\",\"FieldType\":\"System.DateTime\",\"Operator\":\"<=\",\"__msdisposeindex\":506},\"Name\":\"EventStart.DateTime.UtcNow.AddDays(7.0)\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":505},{\"IsGroup\":false,\"Ordinal\":1,\"Join\":\"AND\",\"ItemPath\":\"_0_1\",\"Value\":\"DateTime.UtcNow\",\"Condition\":{\"FieldName\":\"EventStart\",\"FieldType\":\"System.DateTime\",\"Operator\":\">=\",\"__msdisposeindex\":508},\"Name\":null,\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":507},{\"IsGroup\":true,\"Ordinal\":1,\"Join\":\"OR\",\"ItemPath\":\"_1\",\"Value\":null,\"Condition\":{\"FieldName\":null,\"FieldType\":null,\"Operator\":null,\"__msdisposeindex\":510},\"Name\":\"Past\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":509},{\"IsGroup\":false,\"Ordinal\":0,\"Join\":\"AND\",\"ItemPath\":\"_1_0\",\"Value\":\"DateTime.UtcNow.AddMonths(-1)\",\"Condition\":{\"FieldName\":\"EventEnd\",\"FieldType\":\"System.DateTime\",\"Operator\":\">=\",\"__msdisposeindex\":512},\"Name\":\"EventEnd.DateTime.UtcNow.AddMonths(-1)\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":511},{\"IsGroup\":false,\"Ordinal\":1,\"Join\":\"AND\",\"ItemPath\":\"_1_1\",\"Value\":\"DateTime.UtcNow\",\"Condition\":{\"FieldName\":\"EventEnd\",\"FieldType\":\"System.DateTime\",\"Operator\":\"<=\",\"__msdisposeindex\":514},\"Name\":null,\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":513}],\"TypeProperties\":[],\"_itemPathSeparator\":\"_\"}";
        private const string NextWeekOrLastYearAdditionalFilters = "{\"__msdisposeindex\":535,\"Title\":null,\"Id\":\"00000000-0000-0000-0000-000000000000\",\"QueryItems\":[{\"IsGroup\":true,\"Ordinal\":0,\"Join\":\"OR\",\"ItemPath\":\"_0\",\"Value\":null,\"Condition\":{\"FieldName\":null,\"FieldType\":null,\"Operator\":null,\"__msdisposeindex\":537},\"Name\":\"Upcoming\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":536},{\"IsGroup\":false,\"Ordinal\":0,\"Join\":\"AND\",\"ItemPath\":\"_0_0\",\"Value\":\"DateTime.UtcNow.AddDays(7.0)\",\"Condition\":{\"FieldName\":\"EventStart\",\"FieldType\":\"System.DateTime\",\"Operator\":\"<=\",\"__msdisposeindex\":539},\"Name\":\"EventStart.DateTime.UtcNow.AddDays(7.0)\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":538},{\"IsGroup\":false,\"Ordinal\":1,\"Join\":\"AND\",\"ItemPath\":\"_0_1\",\"Value\":\"DateTime.UtcNow\",\"Condition\":{\"FieldName\":\"EventStart\",\"FieldType\":\"System.DateTime\",\"Operator\":\">=\",\"__msdisposeindex\":541},\"Name\":null,\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":540},{\"IsGroup\":true,\"Ordinal\":1,\"Join\":\"OR\",\"ItemPath\":\"_1\",\"Value\":null,\"Condition\":{\"FieldName\":null,\"FieldType\":null,\"Operator\":null,\"__msdisposeindex\":543},\"Name\":\"Past\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":542},{\"IsGroup\":false,\"Ordinal\":0,\"Join\":\"AND\",\"ItemPath\":\"_1_0\",\"Value\":\"DateTime.UtcNow.AddYears(-1)\",\"Condition\":{\"FieldName\":\"EventEnd\",\"FieldType\":\"System.DateTime\",\"Operator\":\">=\",\"__msdisposeindex\":545},\"Name\":\"EventEnd.DateTime.UtcNow.AddYears(-1)\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":544},{\"IsGroup\":false,\"Ordinal\":1,\"Join\":\"AND\",\"ItemPath\":\"_1_1\",\"Value\":\"DateTime.UtcNow\",\"Condition\":{\"FieldName\":\"EventEnd\",\"FieldType\":\"System.DateTime\",\"Operator\":\"<=\",\"__msdisposeindex\":547},\"Name\":null,\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":546}],\"TypeProperties\":[],\"_itemPathSeparator\":\"_\"}";

        private const string NextMonthOrLast3DaysAdditionalFilters = "{\"__msdisposeindex\":573,\"Title\":null,\"Id\":\"00000000-0000-0000-0000-000000000000\",\"QueryItems\":[{\"IsGroup\":true,\"Ordinal\":0,\"Join\":\"OR\",\"ItemPath\":\"_0\",\"Value\":null,\"Condition\":{\"FieldName\":null,\"FieldType\":null,\"Operator\":null,\"__msdisposeindex\":575},\"Name\":\"Upcoming\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":574},{\"IsGroup\":false,\"Ordinal\":0,\"Join\":\"AND\",\"ItemPath\":\"_0_0\",\"Value\":\"DateTime.UtcNow.AddMonths(1)\",\"Condition\":{\"FieldName\":\"EventStart\",\"FieldType\":\"System.DateTime\",\"Operator\":\"<=\",\"__msdisposeindex\":577},\"Name\":\"EventStart.DateTime.UtcNow.AddMonths(1)\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":576},{\"IsGroup\":false,\"Ordinal\":1,\"Join\":\"AND\",\"ItemPath\":\"_0_1\",\"Value\":\"DateTime.UtcNow\",\"Condition\":{\"FieldName\":\"EventStart\",\"FieldType\":\"System.DateTime\",\"Operator\":\">=\",\"__msdisposeindex\":579},\"Name\":null,\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":578},{\"IsGroup\":true,\"Ordinal\":1,\"Join\":\"OR\",\"ItemPath\":\"_1\",\"Value\":null,\"Condition\":{\"FieldName\":null,\"FieldType\":null,\"Operator\":null,\"__msdisposeindex\":581},\"Name\":\"Past\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":580},{\"IsGroup\":false,\"Ordinal\":0,\"Join\":\"AND\",\"ItemPath\":\"_1_0\",\"Value\":\"DateTime.UtcNow.AddDays(-3.0)\",\"Condition\":{\"FieldName\":\"EventEnd\",\"FieldType\":\"System.DateTime\",\"Operator\":\">=\",\"__msdisposeindex\":583},\"Name\":\"EventEnd.DateTime.UtcNow.AddDays(-3.0)\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":582},{\"IsGroup\":false,\"Ordinal\":1,\"Join\":\"AND\",\"ItemPath\":\"_1_1\",\"Value\":\"DateTime.UtcNow\",\"Condition\":{\"FieldName\":\"EventEnd\",\"FieldType\":\"System.DateTime\",\"Operator\":\"<=\",\"__msdisposeindex\":585},\"Name\":null,\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":584}],\"TypeProperties\":[],\"_itemPathSeparator\":\"_\"}";
        private const string NextMonthOrLastWeekAdditionalFilters = "{\"__msdisposeindex\":620,\"Title\":null,\"Id\":\"00000000-0000-0000-0000-000000000000\",\"QueryItems\":[{\"IsGroup\":true,\"Ordinal\":0,\"Join\":\"OR\",\"ItemPath\":\"_0\",\"Value\":null,\"Condition\":{\"FieldName\":null,\"FieldType\":null,\"Operator\":null,\"__msdisposeindex\":622},\"Name\":\"Upcoming\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":621},{\"IsGroup\":false,\"Ordinal\":0,\"Join\":\"AND\",\"ItemPath\":\"_0_0\",\"Value\":\"DateTime.UtcNow.AddMonths(1)\",\"Condition\":{\"FieldName\":\"EventStart\",\"FieldType\":\"System.DateTime\",\"Operator\":\"<=\",\"__msdisposeindex\":624},\"Name\":\"EventStart.DateTime.UtcNow.AddMonths(1)\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":623},{\"IsGroup\":false,\"Ordinal\":1,\"Join\":\"AND\",\"ItemPath\":\"_0_1\",\"Value\":\"DateTime.UtcNow\",\"Condition\":{\"FieldName\":\"EventStart\",\"FieldType\":\"System.DateTime\",\"Operator\":\">=\",\"__msdisposeindex\":626},\"Name\":null,\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":625},{\"IsGroup\":true,\"Ordinal\":1,\"Join\":\"OR\",\"ItemPath\":\"_1\",\"Value\":null,\"Condition\":{\"FieldName\":null,\"FieldType\":null,\"Operator\":null,\"__msdisposeindex\":628},\"Name\":\"Past\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":627},{\"IsGroup\":false,\"Ordinal\":0,\"Join\":\"AND\",\"ItemPath\":\"_1_0\",\"Value\":\"DateTime.UtcNow.AddDays(-7.0)\",\"Condition\":{\"FieldName\":\"EventEnd\",\"FieldType\":\"System.DateTime\",\"Operator\":\">=\",\"__msdisposeindex\":630},\"Name\":\"EventEnd.DateTime.UtcNow.AddDays(-7.0)\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":629},{\"IsGroup\":false,\"Ordinal\":1,\"Join\":\"AND\",\"ItemPath\":\"_1_1\",\"Value\":\"DateTime.UtcNow\",\"Condition\":{\"FieldName\":\"EventEnd\",\"FieldType\":\"System.DateTime\",\"Operator\":\"<=\",\"__msdisposeindex\":632},\"Name\":null,\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":631}],\"TypeProperties\":[],\"_itemPathSeparator\":\"_\"}";
        private const string NextMonthOrLastMonthAdditionalFilters = "{\"__msdisposeindex\":653,\"Title\":null,\"Id\":\"00000000-0000-0000-0000-000000000000\",\"QueryItems\":[{\"IsGroup\":true,\"Ordinal\":0,\"Join\":\"OR\",\"ItemPath\":\"_0\",\"Value\":null,\"Condition\":{\"FieldName\":null,\"FieldType\":null,\"Operator\":null,\"__msdisposeindex\":655},\"Name\":\"Upcoming\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":654},{\"IsGroup\":false,\"Ordinal\":0,\"Join\":\"AND\",\"ItemPath\":\"_0_0\",\"Value\":\"DateTime.UtcNow.AddMonths(1)\",\"Condition\":{\"FieldName\":\"EventStart\",\"FieldType\":\"System.DateTime\",\"Operator\":\"<=\",\"__msdisposeindex\":657},\"Name\":\"EventStart.DateTime.UtcNow.AddMonths(1)\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":656},{\"IsGroup\":false,\"Ordinal\":1,\"Join\":\"AND\",\"ItemPath\":\"_0_1\",\"Value\":\"DateTime.UtcNow\",\"Condition\":{\"FieldName\":\"EventStart\",\"FieldType\":\"System.DateTime\",\"Operator\":\">=\",\"__msdisposeindex\":659},\"Name\":null,\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":658},{\"IsGroup\":true,\"Ordinal\":1,\"Join\":\"OR\",\"ItemPath\":\"_1\",\"Value\":null,\"Condition\":{\"FieldName\":null,\"FieldType\":null,\"Operator\":null,\"__msdisposeindex\":661},\"Name\":\"Past\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":660},{\"IsGroup\":false,\"Ordinal\":0,\"Join\":\"AND\",\"ItemPath\":\"_1_0\",\"Value\":\"DateTime.UtcNow.AddMonths(-1)\",\"Condition\":{\"FieldName\":\"EventEnd\",\"FieldType\":\"System.DateTime\",\"Operator\":\">=\",\"__msdisposeindex\":663},\"Name\":\"EventEnd.DateTime.UtcNow.AddMonths(-1)\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":662},{\"IsGroup\":false,\"Ordinal\":1,\"Join\":\"AND\",\"ItemPath\":\"_1_1\",\"Value\":\"DateTime.UtcNow\",\"Condition\":{\"FieldName\":\"EventEnd\",\"FieldType\":\"System.DateTime\",\"Operator\":\"<=\",\"__msdisposeindex\":665},\"Name\":null,\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":664}],\"TypeProperties\":[],\"_itemPathSeparator\":\"_\"}";
        private const string NextMonthOrLastYearAdditionalFilters = "{\"__msdisposeindex\":686,\"Title\":null,\"Id\":\"00000000-0000-0000-0000-000000000000\",\"QueryItems\":[{\"IsGroup\":true,\"Ordinal\":0,\"Join\":\"OR\",\"ItemPath\":\"_0\",\"Value\":null,\"Condition\":{\"FieldName\":null,\"FieldType\":null,\"Operator\":null,\"__msdisposeindex\":688},\"Name\":\"Upcoming\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":687},{\"IsGroup\":false,\"Ordinal\":0,\"Join\":\"AND\",\"ItemPath\":\"_0_0\",\"Value\":\"DateTime.UtcNow.AddMonths(1)\",\"Condition\":{\"FieldName\":\"EventStart\",\"FieldType\":\"System.DateTime\",\"Operator\":\"<=\",\"__msdisposeindex\":690},\"Name\":\"EventStart.DateTime.UtcNow.AddMonths(1)\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":689},{\"IsGroup\":false,\"Ordinal\":1,\"Join\":\"AND\",\"ItemPath\":\"_0_1\",\"Value\":\"DateTime.UtcNow\",\"Condition\":{\"FieldName\":\"EventStart\",\"FieldType\":\"System.DateTime\",\"Operator\":\">=\",\"__msdisposeindex\":692},\"Name\":null,\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":691},{\"IsGroup\":true,\"Ordinal\":1,\"Join\":\"OR\",\"ItemPath\":\"_1\",\"Value\":null,\"Condition\":{\"FieldName\":null,\"FieldType\":null,\"Operator\":null,\"__msdisposeindex\":694},\"Name\":\"Past\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":693},{\"IsGroup\":false,\"Ordinal\":0,\"Join\":\"AND\",\"ItemPath\":\"_1_0\",\"Value\":\"DateTime.UtcNow.AddYears(-1)\",\"Condition\":{\"FieldName\":\"EventEnd\",\"FieldType\":\"System.DateTime\",\"Operator\":\">=\",\"__msdisposeindex\":696},\"Name\":\"EventEnd.DateTime.UtcNow.AddYears(-1)\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":695},{\"IsGroup\":false,\"Ordinal\":1,\"Join\":\"AND\",\"ItemPath\":\"_1_1\",\"Value\":\"DateTime.UtcNow\",\"Condition\":{\"FieldName\":\"EventEnd\",\"FieldType\":\"System.DateTime\",\"Operator\":\"<=\",\"__msdisposeindex\":698},\"Name\":null,\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":697}],\"TypeProperties\":[],\"_itemPathSeparator\":\"_\"}";

        private const string NextYearOrLast3DaysAdditionalFilters = "{\"__msdisposeindex\":724,\"Title\":null,\"Id\":\"00000000-0000-0000-0000-000000000000\",\"QueryItems\":[{\"IsGroup\":true,\"Ordinal\":0,\"Join\":\"OR\",\"ItemPath\":\"_0\",\"Value\":null,\"Condition\":{\"FieldName\":null,\"FieldType\":null,\"Operator\":null,\"__msdisposeindex\":726},\"Name\":\"Upcoming\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":725},{\"IsGroup\":false,\"Ordinal\":0,\"Join\":\"AND\",\"ItemPath\":\"_0_0\",\"Value\":\"DateTime.UtcNow.AddYears(1)\",\"Condition\":{\"FieldName\":\"EventStart\",\"FieldType\":\"System.DateTime\",\"Operator\":\"<=\",\"__msdisposeindex\":728},\"Name\":\"EventStart.DateTime.UtcNow.AddYears(1)\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":727},{\"IsGroup\":false,\"Ordinal\":1,\"Join\":\"AND\",\"ItemPath\":\"_0_1\",\"Value\":\"DateTime.UtcNow\",\"Condition\":{\"FieldName\":\"EventStart\",\"FieldType\":\"System.DateTime\",\"Operator\":\">=\",\"__msdisposeindex\":730},\"Name\":null,\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":729},{\"IsGroup\":true,\"Ordinal\":1,\"Join\":\"OR\",\"ItemPath\":\"_1\",\"Value\":null,\"Condition\":{\"FieldName\":null,\"FieldType\":null,\"Operator\":null,\"__msdisposeindex\":732},\"Name\":\"Past\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":731},{\"IsGroup\":false,\"Ordinal\":0,\"Join\":\"AND\",\"ItemPath\":\"_1_0\",\"Value\":\"DateTime.UtcNow.AddDays(-3.0)\",\"Condition\":{\"FieldName\":\"EventEnd\",\"FieldType\":\"System.DateTime\",\"Operator\":\">=\",\"__msdisposeindex\":734},\"Name\":\"EventEnd.DateTime.UtcNow.AddDays(-3.0)\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":733},{\"IsGroup\":false,\"Ordinal\":1,\"Join\":\"AND\",\"ItemPath\":\"_1_1\",\"Value\":\"DateTime.UtcNow\",\"Condition\":{\"FieldName\":\"EventEnd\",\"FieldType\":\"System.DateTime\",\"Operator\":\"<=\",\"__msdisposeindex\":736},\"Name\":null,\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":735}],\"TypeProperties\":[],\"_itemPathSeparator\":\"_\"}";
        private const string NextYearOrLastWeekAdditionalFilters = "{\"__msdisposeindex\":757,\"Title\":null,\"Id\":\"00000000-0000-0000-0000-000000000000\",\"QueryItems\":[{\"IsGroup\":true,\"Ordinal\":0,\"Join\":\"OR\",\"ItemPath\":\"_0\",\"Value\":null,\"Condition\":{\"FieldName\":null,\"FieldType\":null,\"Operator\":null,\"__msdisposeindex\":759},\"Name\":\"Upcoming\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":758},{\"IsGroup\":false,\"Ordinal\":0,\"Join\":\"AND\",\"ItemPath\":\"_0_0\",\"Value\":\"DateTime.UtcNow.AddYears(1)\",\"Condition\":{\"FieldName\":\"EventStart\",\"FieldType\":\"System.DateTime\",\"Operator\":\"<=\",\"__msdisposeindex\":761},\"Name\":\"EventStart.DateTime.UtcNow.AddYears(1)\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":760},{\"IsGroup\":false,\"Ordinal\":1,\"Join\":\"AND\",\"ItemPath\":\"_0_1\",\"Value\":\"DateTime.UtcNow\",\"Condition\":{\"FieldName\":\"EventStart\",\"FieldType\":\"System.DateTime\",\"Operator\":\">=\",\"__msdisposeindex\":763},\"Name\":null,\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":762},{\"IsGroup\":true,\"Ordinal\":1,\"Join\":\"OR\",\"ItemPath\":\"_1\",\"Value\":null,\"Condition\":{\"FieldName\":null,\"FieldType\":null,\"Operator\":null,\"__msdisposeindex\":765},\"Name\":\"Past\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":764},{\"IsGroup\":false,\"Ordinal\":0,\"Join\":\"AND\",\"ItemPath\":\"_1_0\",\"Value\":\"DateTime.UtcNow.AddDays(-7.0)\",\"Condition\":{\"FieldName\":\"EventEnd\",\"FieldType\":\"System.DateTime\",\"Operator\":\">=\",\"__msdisposeindex\":767},\"Name\":\"EventEnd.DateTime.UtcNow.AddDays(-7.0)\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":766},{\"IsGroup\":false,\"Ordinal\":1,\"Join\":\"AND\",\"ItemPath\":\"_1_1\",\"Value\":\"DateTime.UtcNow\",\"Condition\":{\"FieldName\":\"EventEnd\",\"FieldType\":\"System.DateTime\",\"Operator\":\"<=\",\"__msdisposeindex\":769},\"Name\":null,\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":768}],\"TypeProperties\":[],\"_itemPathSeparator\":\"_\"}";
        private const string NextYearOrLastMonthAdditionalFilters = "{\"__msdisposeindex\":790,\"Title\":null,\"Id\":\"00000000-0000-0000-0000-000000000000\",\"QueryItems\":[{\"IsGroup\":true,\"Ordinal\":0,\"Join\":\"OR\",\"ItemPath\":\"_0\",\"Value\":null,\"Condition\":{\"FieldName\":null,\"FieldType\":null,\"Operator\":null,\"__msdisposeindex\":792},\"Name\":\"Upcoming\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":791},{\"IsGroup\":false,\"Ordinal\":0,\"Join\":\"AND\",\"ItemPath\":\"_0_0\",\"Value\":\"DateTime.UtcNow.AddYears(1)\",\"Condition\":{\"FieldName\":\"EventStart\",\"FieldType\":\"System.DateTime\",\"Operator\":\"<=\",\"__msdisposeindex\":794},\"Name\":\"EventStart.DateTime.UtcNow.AddYears(1)\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":793},{\"IsGroup\":false,\"Ordinal\":1,\"Join\":\"AND\",\"ItemPath\":\"_0_1\",\"Value\":\"DateTime.UtcNow\",\"Condition\":{\"FieldName\":\"EventStart\",\"FieldType\":\"System.DateTime\",\"Operator\":\">=\",\"__msdisposeindex\":796},\"Name\":null,\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":795},{\"IsGroup\":true,\"Ordinal\":1,\"Join\":\"OR\",\"ItemPath\":\"_1\",\"Value\":null,\"Condition\":{\"FieldName\":null,\"FieldType\":null,\"Operator\":null,\"__msdisposeindex\":798},\"Name\":\"Past\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":797},{\"IsGroup\":false,\"Ordinal\":0,\"Join\":\"AND\",\"ItemPath\":\"_1_0\",\"Value\":\"DateTime.UtcNow.AddMonths(-1)\",\"Condition\":{\"FieldName\":\"EventEnd\",\"FieldType\":\"System.DateTime\",\"Operator\":\">=\",\"__msdisposeindex\":800},\"Name\":\"EventEnd.DateTime.UtcNow.AddMonths(-1)\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":799},{\"IsGroup\":false,\"Ordinal\":1,\"Join\":\"AND\",\"ItemPath\":\"_1_1\",\"Value\":\"DateTime.UtcNow\",\"Condition\":{\"FieldName\":\"EventEnd\",\"FieldType\":\"System.DateTime\",\"Operator\":\"<=\",\"__msdisposeindex\":802},\"Name\":null,\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":801}],\"TypeProperties\":[],\"_itemPathSeparator\":\"_\"}";
        private const string NextYearOrLastYearAdditionalFilters = "{\"__msdisposeindex\":823,\"Title\":null,\"Id\":\"00000000-0000-0000-0000-000000000000\",\"QueryItems\":[{\"IsGroup\":true,\"Ordinal\":0,\"Join\":\"OR\",\"ItemPath\":\"_0\",\"Value\":null,\"Condition\":{\"FieldName\":null,\"FieldType\":null,\"Operator\":null,\"__msdisposeindex\":825},\"Name\":\"Upcoming\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":824},{\"IsGroup\":false,\"Ordinal\":0,\"Join\":\"AND\",\"ItemPath\":\"_0_0\",\"Value\":\"DateTime.UtcNow.AddYears(1)\",\"Condition\":{\"FieldName\":\"EventStart\",\"FieldType\":\"System.DateTime\",\"Operator\":\"<=\",\"__msdisposeindex\":827},\"Name\":\"EventStart.DateTime.UtcNow.AddYears(1)\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":826},{\"IsGroup\":false,\"Ordinal\":1,\"Join\":\"AND\",\"ItemPath\":\"_0_1\",\"Value\":\"DateTime.UtcNow\",\"Condition\":{\"FieldName\":\"EventStart\",\"FieldType\":\"System.DateTime\",\"Operator\":\">=\",\"__msdisposeindex\":829},\"Name\":null,\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":828},{\"IsGroup\":true,\"Ordinal\":1,\"Join\":\"OR\",\"ItemPath\":\"_1\",\"Value\":null,\"Condition\":{\"FieldName\":null,\"FieldType\":null,\"Operator\":null,\"__msdisposeindex\":831},\"Name\":\"Past\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":830},{\"IsGroup\":false,\"Ordinal\":0,\"Join\":\"AND\",\"ItemPath\":\"_1_0\",\"Value\":\"DateTime.UtcNow.AddYears(-1)\",\"Condition\":{\"FieldName\":\"EventEnd\",\"FieldType\":\"System.DateTime\",\"Operator\":\">=\",\"__msdisposeindex\":833},\"Name\":\"EventEnd.DateTime.UtcNow.AddYears(-1)\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":832},{\"IsGroup\":false,\"Ordinal\":1,\"Join\":\"AND\",\"ItemPath\":\"_1_1\",\"Value\":\"DateTime.UtcNow\",\"Condition\":{\"FieldName\":\"EventEnd\",\"FieldType\":\"System.DateTime\",\"Operator\":\"<=\",\"__msdisposeindex\":835},\"Name\":null,\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":834}],\"TypeProperties\":[],\"_itemPathSeparator\":\"_\"}";

        #endregion
        
        private const string JsonExceptionMessage = "[InvalidOperationException: Error during serialization or deserialization using the JSON JavaScriptSerializer. The length of the string exceeds the value set on the maxJsonLength property.]";
        private readonly Dictionary<string, CultureInfo> sitefinityLanguages = new Dictionary<string, CultureInfo>();
        private List<Guid> calendarList = new List<Guid>();
        private const string BaseEventTitle = "TestEvent";
        private const string BasePastEventTitle = "PastTestEvent";
        private const string BaseUpcomingEventTitle = "UpcomingTestEvent";
        private const string BaseDraftEventTitle = "DraftTestEvent";
        private const string BaseAllDayEventTitle = "AllDayTestEvent";
        private const string BaseRepeatEventTitle = "RepeatTestEvent";
    }
}