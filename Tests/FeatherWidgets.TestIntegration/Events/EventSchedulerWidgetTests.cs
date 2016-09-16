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
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Mvc.Proxy;
using Telerik.Sitefinity.Mvc.TestUtilities.Helpers;
using Telerik.Sitefinity.RecurrentRules;
using Telerik.Sitefinity.TestUtilities.CommonOperations;
using Telerik.Sitefinity.TestUtilities.CommonOperations.Multilingual;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Web.Model;
using Telerik.WebTestRunner.Server.Attributes;

namespace FeatherWidgets.TestIntegration.Events
{
    /// <summary>
    /// This class contains test related to basic functionality of Event Scheduler Widget.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling"), TestFixture]
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

            ServerOperations.Events().CreateCalendar(Guid.NewGuid(), EventSchedulerWidgetTests.Calendar1Title, "en-US");
            ServerOperations.Events().CreateCalendar(Guid.NewGuid(), EventSchedulerWidgetTests.Calendar2Title, "en-US");
            ServerOperations.Events().CreateCalendar(Guid.NewGuid(), EventSchedulerWidgetTests.Calendar3Title, "en-US");

            ServerOperations.Taxonomies().CreateTag(EventSchedulerWidgetTests.Tag1Title);
            ServerOperations.Taxonomies().CreateTag(EventSchedulerWidgetTests.Tag2Title);
            ServerOperations.Taxonomies().CreateTag(EventSchedulerWidgetTests.Tag3Title);

            ServerOperations.Taxonomies().CreateCategory(EventSchedulerWidgetTests.Category1Title);
            ServerOperations.Taxonomies().CreateCategory(EventSchedulerWidgetTests.Category2Title);
            ServerOperations.Taxonomies().CreateCategory(EventSchedulerWidgetTests.Category3Title);

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

            var defaultCalendarId = ServerOperations.Events().GetDefaultCalendarId();
            foreach (var item in EventsManager.GetManager().GetCalendars())
            {
                if (item.Id != defaultCalendarId)
                {
                    ServerOperations.Events().DeleteCalendar(item.Id);
                }
            }

            ServerOperations.Taxonomies().ClearAllCategories(TaxonomiesConstants.CategoriesTaxonomyId);
            ServerOperations.Taxonomies().ClearAllTags(TaxonomiesConstants.TagsTaxonomyId);
        }

        #region Events

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.SitefinityTeam8)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events()
        {
            ServerOperations.Events().CreаteAllDayRecurrentEvent("RepeatEventTitle1", string.Empty, DateTime.Today, DateTime.Today, 60, 10000, 1, true);
            ServerOperations.Events().CreаteAllDayRecurrentEvent("RepeatEventTitle2", string.Empty, DateTime.Today, DateTime.Today, 60, 10000, 1, true);
            ServerOperations.Events().CreаteAllDayRecurrentEvent("RepeatEventTitle3", string.Empty, DateTime.Today, DateTime.Today, 60, 10000, 1, true);
            ServerOperations.Events().CreаteAllDayRecurrentEvent("RepeatEventTitle4", string.Empty, DateTime.Today, DateTime.Today, 60, 10000, 1, true);
            ServerOperations.Events().CreаteAllDayRecurrentEvent("RepeatEventTitle5", string.Empty, DateTime.Today, DateTime.Today, 60, 10000, 1, true);
            ServerOperations.Events().CreаteAllDayRecurrentEvent("RepeatEventTitle6", string.Empty, DateTime.Today, DateTime.Today, 60, 10000, 1, true);

            var widgetId = this.AddControl(this.CreatePage(), SelectionMode.AllItems, DefaultAdditionalFilters, SelectionMode.AllItems, DefaultNarrowSelectionFilters);

            this.AssertEvent(widgetId, DateTime.MinValue, DateTime.MaxValue, EventsManager.GetManager().GetEventsOccurrences(DateTime.MinValue, DateTime.MaxValue).Count());
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.SitefinityTeam8)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events_Upcoming()
        {
            var widgetId = this.AddControl(this.CreatePage(), SelectionMode.FilteredItems, UpcomingAdditionalFilters, SelectionMode.AllItems, DefaultNarrowSelectionFilters);

            this.AssertEvent(widgetId, DateTime.MinValue, DateTime.MaxValue, 2);
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.SitefinityTeam8)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events_Past()
        {
            var widgetId = this.AddControl(this.CreatePage(), SelectionMode.FilteredItems, PastAdditionalFilters, SelectionMode.AllItems, DefaultNarrowSelectionFilters);

            this.AssertEvent(widgetId, DateTime.MinValue, DateTime.MaxValue, 1);
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.SitefinityTeam8)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events_Current()
        {
            var widgetId = this.AddControl(this.CreatePage(), SelectionMode.FilteredItems, CurrentAdditionalFilters, SelectionMode.AllItems, DefaultNarrowSelectionFilters);

            this.AssertEvent(widgetId, DateTime.MinValue, DateTime.MaxValue, 6);
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.SitefinityTeam8)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events_UpcomingAndCurrent()
        {
            var widgetId = this.AddControl(this.CreatePage(), SelectionMode.FilteredItems, UpcomingAndCurrentAdditionalFilters, SelectionMode.AllItems, DefaultNarrowSelectionFilters);

            this.AssertEvent(widgetId, DateTime.MinValue, DateTime.MaxValue, 8);
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.SitefinityTeam8)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events_PastAndUpcoming()
        {
            var widgetId = this.AddControl(this.CreatePage(), SelectionMode.FilteredItems, PastAndUpcomingAdditionalFilters, SelectionMode.AllItems, DefaultNarrowSelectionFilters);

            this.AssertEvent(widgetId, DateTime.MinValue, DateTime.MaxValue, 3);
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.SitefinityTeam8)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events_PastAndCurrent()
        {
            var widgetId = this.AddControl(this.CreatePage(), SelectionMode.FilteredItems, PastAndCurrentAdditionalFilters, SelectionMode.AllItems, DefaultNarrowSelectionFilters);

            this.AssertEvent(widgetId, DateTime.MinValue, DateTime.MaxValue, 7);
        }

        #endregion

        #region Events - Multilingual

        [Test]
        [Multilingual]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.SitefinityTeam8)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events_Multilingual()
        {
            var manager = EventsManager.GetManager();
            var multiOperations = new MultilingualEventOperations();
            var bulgarian = AppSettings.CurrentSettings.DefinedFrontendLanguages.Where(x => x.Name == "bg-BG").FirstOrDefault();

            string recurrenceExpression = new RecurrenceRuleBuilder().CreateDailyRecurrenceExpression(DateTime.Today, TimeSpan.FromMinutes(60), DateTime.MaxValue, int.MaxValue, 1, TimeZoneInfo.Local.Id);

            var calendartDefault = manager.GetCalendars().FirstOrDefault();
            this.CreateLocalizedEvent(multiOperations, "Event 1 bg", Guid.NewGuid(), DateTime.SpecifyKind(DateTime.Today, DateTimeKind.Utc), DateTime.MaxValue, false, true, calendartDefault.Id, bulgarian, recurrenceExpression);
            this.CreateLocalizedEvent(multiOperations, "Event 2 bg", Guid.NewGuid(), DateTime.Today, DateTime.MaxValue, false, true, calendartDefault.Id, bulgarian, recurrenceExpression);
            this.CreateLocalizedEvent(multiOperations, "Event 3 bg", Guid.NewGuid(), DateTime.Today, DateTime.MaxValue, false, true, calendartDefault.Id, bulgarian, recurrenceExpression);
            this.CreateLocalizedEvent(multiOperations, "Event 4 bg", Guid.NewGuid(), DateTime.Today, DateTime.MaxValue, false, true, calendartDefault.Id, bulgarian, recurrenceExpression);
            this.CreateLocalizedEvent(multiOperations, "Event 5 bg", Guid.NewGuid(), DateTime.Today, DateTime.MaxValue, false, true, calendartDefault.Id, bulgarian, recurrenceExpression);
            this.CreateLocalizedEvent(multiOperations, "Event 6 bg", Guid.NewGuid(), DateTime.Today, DateTime.MaxValue, false, true, calendartDefault.Id, bulgarian, recurrenceExpression);
            this.CreateLocalizedEvent(multiOperations, "Event 7 bg", Guid.NewGuid(), DateTime.Today, DateTime.MaxValue, false, true, calendartDefault.Id, bulgarian, recurrenceExpression);
            this.CreateLocalizedEvent(multiOperations, "Event 8 bg", Guid.NewGuid(), DateTime.Today, DateTime.MaxValue, false, true, calendartDefault.Id, bulgarian, recurrenceExpression);
            this.CreateLocalizedEvent(multiOperations, "Event 9 bg", Guid.NewGuid(), DateTime.Today, DateTime.MaxValue, false, true, calendartDefault.Id, bulgarian, recurrenceExpression);
            this.CreateLocalizedEvent(multiOperations, "Event 10 bg", Guid.NewGuid(), DateTime.Today, DateTime.MaxValue, false, true, calendartDefault.Id, bulgarian, recurrenceExpression);

            var widgetId = this.AddControl(this.CreatePage(bulgarian), SelectionMode.AllItems, DefaultAdditionalFilters, SelectionMode.AllItems, DefaultNarrowSelectionFilters);
            this.AssertEvent(widgetId, DateTime.MinValue, DateTime.MaxValue, EventsManager.GetManager().GetEventsOccurrences(EventsManager.GetManager().GetEvents(), DateTime.MinValue, DateTime.Now.AddYears(4000)).Where(p => p.Event.LanguageData.Where(t => t.Language == bulgarian.Name).LongCount() > 0).Count(), bulgarian);
        }

        [Test]
        [Multilingual]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.SitefinityTeam8)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events_Upcoming_Multilingual()
        {
            var manager = EventsManager.GetManager();
            var multiOperations = new MultilingualEventOperations();
            var arabic = AppSettings.CurrentSettings.DefinedFrontendLanguages.Where(x => x.Name == "ar-MA").FirstOrDefault();

            var calendartDefault = manager.GetCalendars().FirstOrDefault();
            this.CreateLocalizedEvent(multiOperations, "Event 1 arabic", Guid.NewGuid(), DateTime.Now, DateTime.Now.AddHours(2), false, false, calendartDefault.Id, arabic);
            this.CreateLocalizedEvent(multiOperations, "Event 2 arabic", Guid.NewGuid(), DateTime.Now.AddDays(1), DateTime.Now.AddDays(3), false, false, calendartDefault.Id, arabic);

            var widgetId = this.AddControl(this.CreatePage(arabic), SelectionMode.AllItems, UpcomingAdditionalFilters, SelectionMode.AllItems, DefaultNarrowSelectionFilters);
            this.AssertEvent(widgetId, DateTime.MinValue, DateTime.MaxValue, 2, arabic);
        }

        #endregion

        #region Specified upcoming events

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.SitefinityTeam8)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events_UpcomingNext3Days()
        {
            ServerOperations.Events().CreateEvent("UpcomingEventInNext3Days", string.Empty, false, DateTime.Now.AddDays(2), DateTime.Now.AddDays(3));
            ServerOperations.Events().CreateEvent("UpcomingEventInMoreThanNext3Days", string.Empty, false, DateTime.Now.AddDays(5), DateTime.Now.AddDays(7));

            var widgetId = this.AddControl(this.CreatePage(), SelectionMode.FilteredItems, Next3DaysAdditionalFilters, SelectionMode.AllItems, DefaultNarrowSelectionFilters);

            this.AssertEvent(widgetId, DateTime.MinValue, DateTime.MaxValue, 3);
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.SitefinityTeam8)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events_UpcomingNext3DaysOrCurrent()
        {
            ServerOperations.Events().CreateEvent("UpcomingEventInNext3Days", string.Empty, false, DateTime.Now.AddDays(2), DateTime.Now.AddDays(3));
            ServerOperations.Events().CreateEvent("UpcomingEventInMoreThanNext3Days", string.Empty, false, DateTime.Now.AddDays(5), DateTime.Now.AddDays(7));

            var widgetId = this.AddControl(this.CreatePage(), SelectionMode.FilteredItems, Next3DaysAndCurrentAdditionalFilters, SelectionMode.AllItems, DefaultNarrowSelectionFilters);

            this.AssertEvent(widgetId, DateTime.MinValue, DateTime.MaxValue, 9);
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.SitefinityTeam8)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events_UpcomingNextWeek()
        {
            ServerOperations.Events().CreateEvent("UpcomingEventInNextWeek", string.Empty, false, DateTime.Now.AddDays(4), DateTime.Now.AddDays(6));
            ServerOperations.Events().CreateEvent("UpcomingEventInMoreThanNextWeek", string.Empty, false, DateTime.Now.AddDays(9), DateTime.Now.AddDays(12));

            var widgetId = this.AddControl(this.CreatePage(), SelectionMode.FilteredItems, NextWeekAdditionalFilters, SelectionMode.AllItems, DefaultNarrowSelectionFilters);

            this.AssertEvent(widgetId, DateTime.MinValue, DateTime.MaxValue, 3);
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.SitefinityTeam8)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events_UpcomingNextWeekOrCurrent()
        {
            ServerOperations.Events().CreateEvent("UpcomingEventInNextWeek", string.Empty, false, DateTime.Now.AddDays(4), DateTime.Now.AddDays(6));
            ServerOperations.Events().CreateEvent("UpcomingEventInMoreThanNextWeek", string.Empty, false, DateTime.Now.AddDays(9), DateTime.Now.AddDays(12));

            var widgetId = this.AddControl(this.CreatePage(), SelectionMode.FilteredItems, NextWeekAndCurrentAdditionalFilters, SelectionMode.AllItems, DefaultNarrowSelectionFilters);

            this.AssertEvent(widgetId, DateTime.MinValue, DateTime.MaxValue, 9);
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.SitefinityTeam8)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events_UpcomingNextMonth()
        {
            ServerOperations.Events().CreateEvent("UpcomingEventInNextMonth", string.Empty, false, DateTime.Now.AddDays(20), DateTime.Now.AddDays(22));
            ServerOperations.Events().CreateEvent("UpcomingEventInMoreThanNextMonth", string.Empty, false, DateTime.Now.AddDays(40), DateTime.Now.AddDays(42));

            var widgetId = this.AddControl(this.CreatePage(), SelectionMode.FilteredItems, NextMonthAdditionalFilters, SelectionMode.AllItems, DefaultNarrowSelectionFilters);

            this.AssertEvent(widgetId, DateTime.MinValue, DateTime.MaxValue, 3);
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.SitefinityTeam8)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events_UpcomingNextMonthOrCurrent()
        {
            ServerOperations.Events().CreateEvent("UpcomingEventInNextMonth", string.Empty, false, DateTime.Now.AddDays(20), DateTime.Now.AddDays(22));
            ServerOperations.Events().CreateEvent("UpcomingEventInMoreThanNextMonth", string.Empty, false, DateTime.Now.AddDays(40), DateTime.Now.AddDays(42));

            var widgetId = this.AddControl(this.CreatePage(), SelectionMode.FilteredItems, NextMonthAndCurrentAdditionalFilters, SelectionMode.AllItems, DefaultNarrowSelectionFilters);

            this.AssertEvent(widgetId, DateTime.MinValue, DateTime.MaxValue, 9);
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.SitefinityTeam8)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events_UpcomingNextYear()
        {
            ServerOperations.Events().CreateEvent("UpcomingEventInNextYear", string.Empty, false, DateTime.Now.AddDays(365), DateTime.Now.AddDays(368));
            ServerOperations.Events().CreateEvent("UpcomingEventInMoreThanNextYear", string.Empty, false, DateTime.Now.AddDays(450), DateTime.Now.AddDays(453));

            var widgetId = this.AddControl(this.CreatePage(), SelectionMode.FilteredItems, NextYearAdditionalFilters, SelectionMode.AllItems, DefaultNarrowSelectionFilters);

            this.AssertEvent(widgetId, DateTime.MinValue, DateTime.MaxValue, 3);
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.SitefinityTeam8)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events_UpcomingNextYearOrCurrent()
        {
            ServerOperations.Events().CreateEvent("UpcomingEventInNextYear", string.Empty, false, DateTime.Now.AddDays(365), DateTime.Now.AddDays(368));
            ServerOperations.Events().CreateEvent("UpcomingEventInMoreThanNextYear", string.Empty, false, DateTime.Now.AddDays(450), DateTime.Now.AddDays(453));

            var widgetId = this.AddControl(this.CreatePage(), SelectionMode.FilteredItems, NextYearAndCurrentAdditionalFilters, SelectionMode.AllItems, DefaultNarrowSelectionFilters);

            this.AssertEvent(widgetId, DateTime.MinValue, DateTime.MaxValue, 9);
        }

        #endregion

        #region Specified upcoming events - Multilingual

        #endregion

        #region Specified past events

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.SitefinityTeam8)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events_PastLast3Days()
        {
            ServerOperations.Events().CreateEvent("PastEventInLast3Days", string.Empty, false, DateTime.Now.AddDays(-2), DateTime.Now.AddDays(-1));
            ServerOperations.Events().CreateEvent("PastEventInMoreThanLast3Days", string.Empty, false, DateTime.Now.AddDays(-6), DateTime.Now.AddDays(-4));

            var widgetId = this.AddControl(this.CreatePage(), SelectionMode.FilteredItems, Last3DaysAdditionalFilters, SelectionMode.AllItems, DefaultNarrowSelectionFilters);

            this.AssertEvent(widgetId, DateTime.MinValue, DateTime.MaxValue, 2);
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.SitefinityTeam8)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events_PastLast3DaysOrCurrent()
        {
            ServerOperations.Events().CreateEvent("PastEventInLast3Days", string.Empty, false, DateTime.Now.AddDays(-2), DateTime.Now.AddDays(-1));
            ServerOperations.Events().CreateEvent("PastEventInMoreThanLast3Days", string.Empty, false, DateTime.Now.AddDays(-6), DateTime.Now.AddDays(-4));

            var widgetId = this.AddControl(this.CreatePage(), SelectionMode.FilteredItems, Last3DaysAndCurrentAdditionalFilters, SelectionMode.AllItems, DefaultNarrowSelectionFilters);

            this.AssertEvent(widgetId, DateTime.MinValue, DateTime.MaxValue, 8);
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.SitefinityTeam8)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events_PastLastWeek()
        {
            ServerOperations.Events().CreateEvent("PastEventInLastWeek", string.Empty, false, DateTime.Now.AddDays(-6), DateTime.Now.AddDays(-4));
            ServerOperations.Events().CreateEvent("PastEventInMoreThanLastWeek", string.Empty, false, DateTime.Now.AddDays(-12), DateTime.Now.AddDays(-9));

            var widgetId = this.AddControl(this.CreatePage(), SelectionMode.FilteredItems, LastWeekAdditionalFilters, SelectionMode.AllItems, DefaultNarrowSelectionFilters);

            this.AssertEvent(widgetId, DateTime.MinValue, DateTime.MaxValue, 2);
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.SitefinityTeam8)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events_PastLastWeekOrCurrent()
        {
            ServerOperations.Events().CreateEvent("PastEventInLastWeek", string.Empty, false, DateTime.Now.AddDays(-6), DateTime.Now.AddDays(-4));
            ServerOperations.Events().CreateEvent("PastEventInMoreThanLastWeek", string.Empty, false, DateTime.Now.AddDays(-12), DateTime.Now.AddDays(-9));

            var widgetId = this.AddControl(this.CreatePage(), SelectionMode.FilteredItems, LastWeekAndCurrentAdditionalFilters, SelectionMode.AllItems, DefaultNarrowSelectionFilters);

            this.AssertEvent(widgetId, DateTime.MinValue, DateTime.MaxValue, 8);
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.SitefinityTeam8)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events_PastLastMonth()
        {
            ServerOperations.Events().CreateEvent("PastEventInLastMonth", string.Empty, false, DateTime.Now.AddDays(-22), DateTime.Now.AddDays(-20));
            ServerOperations.Events().CreateEvent("PastEventInMoreThanLastMonth", string.Empty, false, DateTime.Now.AddDays(-42), DateTime.Now.AddDays(-40));

            var widgetId = this.AddControl(this.CreatePage(), SelectionMode.FilteredItems, LastMonthAdditionalFilters, SelectionMode.AllItems, DefaultNarrowSelectionFilters);

            this.AssertEvent(widgetId, DateTime.MinValue, DateTime.MaxValue, 2);
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.SitefinityTeam8)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events_PastLastMonthOrCurrent()
        {
            ServerOperations.Events().CreateEvent("PastEventInLastMonth", string.Empty, false, DateTime.Now.AddDays(-22), DateTime.Now.AddDays(-20));
            ServerOperations.Events().CreateEvent("PastEventInMoreThanLastMonth", string.Empty, false, DateTime.Now.AddDays(-42), DateTime.Now.AddDays(-40));

            var widgetId = this.AddControl(this.CreatePage(), SelectionMode.FilteredItems, LastMonthAndCurrentAdditionalFilters, SelectionMode.AllItems, DefaultNarrowSelectionFilters);

            this.AssertEvent(widgetId, DateTime.MinValue, DateTime.MaxValue, 8);
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.SitefinityTeam8)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events_PastLastYear()
        {
            ServerOperations.Events().CreateEvent("PastEventInLastYear", string.Empty, false, DateTime.Now.AddDays(-368), DateTime.Now.AddDays(-365));
            ServerOperations.Events().CreateEvent("PastEventInMoreThanLastYear", string.Empty, false, DateTime.Now.AddDays(-453), DateTime.Now.AddDays(-450));

            var widgetId = this.AddControl(this.CreatePage(), SelectionMode.FilteredItems, LastYearAdditionalFilters, SelectionMode.AllItems, DefaultNarrowSelectionFilters);

            this.AssertEvent(widgetId, DateTime.MinValue, DateTime.MaxValue, 2);
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.SitefinityTeam8)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events_PastLastYearOrCurrent()
        {
            ServerOperations.Events().CreateEvent("PastEventInLastYear", string.Empty, false, DateTime.Now.AddDays(-368), DateTime.Now.AddDays(-365));
            ServerOperations.Events().CreateEvent("PastEventInMoreThanLastYear", string.Empty, false, DateTime.Now.AddDays(-453), DateTime.Now.AddDays(-450));

            var widgetId = this.AddControl(this.CreatePage(), SelectionMode.FilteredItems, LastYearAndCurrentAdditionalFilters, SelectionMode.AllItems, DefaultNarrowSelectionFilters);

            this.AssertEvent(widgetId, DateTime.MinValue, DateTime.MaxValue, 8);
        }

        #endregion

        #region Specified past events - Multilingual

        #endregion

        #region Specified combined events

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.SitefinityTeam8)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events_Combined_Next3DaysOrLast3Days()
        {
            ServerOperations.Events().CreateEvent("UpcomingEventInNext3Days", string.Empty, false, DateTime.Now.AddDays(2), DateTime.Now.AddDays(3));
            ServerOperations.Events().CreateEvent("UpcomingEventInMoreThanNext3Days", string.Empty, false, DateTime.Now.AddDays(5), DateTime.Now.AddDays(7));
            ServerOperations.Events().CreateEvent("PastEventInLast3Days", string.Empty, false, DateTime.Now.AddDays(-2), DateTime.Now.AddDays(-1));
            ServerOperations.Events().CreateEvent("PastEventInMoreThanLast3Days", string.Empty, false, DateTime.Now.AddDays(-6), DateTime.Now.AddDays(-4));

            var widgetId = this.AddControl(this.CreatePage(), SelectionMode.FilteredItems, Next3DaysOrLast3DaysAdditionalFilters, SelectionMode.AllItems, DefaultNarrowSelectionFilters);

            this.AssertEvent(widgetId, DateTime.MinValue, DateTime.MaxValue, 5);
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.SitefinityTeam8)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events_Combined_Next3DaysOrLast3DaysOrCurrent()
        {
            ServerOperations.Events().CreateEvent("UpcomingEventInNext3Days", string.Empty, false, DateTime.Now.AddDays(2), DateTime.Now.AddDays(3));
            ServerOperations.Events().CreateEvent("UpcomingEventInMoreThanNext3Days", string.Empty, false, DateTime.Now.AddDays(5), DateTime.Now.AddDays(7));
            ServerOperations.Events().CreateEvent("PastEventInLast3Days", string.Empty, false, DateTime.Now.AddDays(-2), DateTime.Now.AddDays(-1));
            ServerOperations.Events().CreateEvent("PastEventInMoreThanLast3Days", string.Empty, false, DateTime.Now.AddDays(-6), DateTime.Now.AddDays(-4));

            var widgetId = this.AddControl(this.CreatePage(), SelectionMode.FilteredItems, Next3DaysOrLast3DaysOrCurrentAdditionalFilters, SelectionMode.AllItems, DefaultNarrowSelectionFilters);

            this.AssertEvent(widgetId, DateTime.MinValue, DateTime.MaxValue, 11);
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.SitefinityTeam8)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events_Combined_Next3DaysOrLastWeek()
        {
            ServerOperations.Events().CreateEvent("UpcomingEventInNext3Days", string.Empty, false, DateTime.Now.AddDays(2), DateTime.Now.AddDays(3));
            ServerOperations.Events().CreateEvent("UpcomingEventInMoreThanNext3Days", string.Empty, false, DateTime.Now.AddDays(5), DateTime.Now.AddDays(7));
            ServerOperations.Events().CreateEvent("PastEventInLastWeek", string.Empty, false, DateTime.Now.AddDays(-6), DateTime.Now.AddDays(-4));
            ServerOperations.Events().CreateEvent("PastEventInMoreThanLastWeek", string.Empty, false, DateTime.Now.AddDays(-12), DateTime.Now.AddDays(-9));

            var widgetId = this.AddControl(this.CreatePage(), SelectionMode.FilteredItems, Next3DaysOrLastWeekAdditionalFilters, SelectionMode.AllItems, DefaultNarrowSelectionFilters);

            this.AssertEvent(widgetId, DateTime.MinValue, DateTime.MaxValue, 5);
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.SitefinityTeam8)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events_Combined_Next3DaysOrLastWeekOrCurrent()
        {
            ServerOperations.Events().CreateEvent("UpcomingEventInNext3Days", string.Empty, false, DateTime.Now.AddDays(2), DateTime.Now.AddDays(3));
            ServerOperations.Events().CreateEvent("UpcomingEventInMoreThanNext3Days", string.Empty, false, DateTime.Now.AddDays(5), DateTime.Now.AddDays(7));
            ServerOperations.Events().CreateEvent("PastEventInLastWeek", string.Empty, false, DateTime.Now.AddDays(-6), DateTime.Now.AddDays(-4));
            ServerOperations.Events().CreateEvent("PastEventInMoreThanLastWeek", string.Empty, false, DateTime.Now.AddDays(-12), DateTime.Now.AddDays(-9));

            var widgetId = this.AddControl(this.CreatePage(), SelectionMode.FilteredItems, Next3DaysOrLastWeekOrCurrentAdditionalFilters, SelectionMode.AllItems, DefaultNarrowSelectionFilters);

            this.AssertEvent(widgetId, DateTime.MinValue, DateTime.MaxValue, 11);
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.SitefinityTeam8)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events_Combined_Next3DaysOrLastMonth()
        {
            ServerOperations.Events().CreateEvent("UpcomingEventInNext3Days", string.Empty, false, DateTime.Now.AddDays(2), DateTime.Now.AddDays(3));
            ServerOperations.Events().CreateEvent("UpcomingEventInMoreThanNext3Days", string.Empty, false, DateTime.Now.AddDays(5), DateTime.Now.AddDays(7));
            ServerOperations.Events().CreateEvent("PastEventInLastMonth", string.Empty, false, DateTime.Now.AddDays(-22), DateTime.Now.AddDays(-20));
            ServerOperations.Events().CreateEvent("PastEventInMoreThanLastMonth", string.Empty, false, DateTime.Now.AddDays(-42), DateTime.Now.AddDays(-40));

            var widgetId = this.AddControl(this.CreatePage(), SelectionMode.FilteredItems, Next3DaysOrLastMonthAdditionalFilters, SelectionMode.AllItems, DefaultNarrowSelectionFilters);

            this.AssertEvent(widgetId, DateTime.MinValue, DateTime.MaxValue, 5);
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.SitefinityTeam8)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events_Combined_Next3DaysOrLastMonthOrCurrent()
        {
            ServerOperations.Events().CreateEvent("UpcomingEventInNext3Days", string.Empty, false, DateTime.Now.AddDays(2), DateTime.Now.AddDays(3));
            ServerOperations.Events().CreateEvent("UpcomingEventInMoreThanNext3Days", string.Empty, false, DateTime.Now.AddDays(5), DateTime.Now.AddDays(7));
            ServerOperations.Events().CreateEvent("PastEventInLastMonth", string.Empty, false, DateTime.Now.AddDays(-22), DateTime.Now.AddDays(-20));
            ServerOperations.Events().CreateEvent("PastEventInMoreThanLastMonth", string.Empty, false, DateTime.Now.AddDays(-42), DateTime.Now.AddDays(-40));

            var widgetId = this.AddControl(this.CreatePage(), SelectionMode.FilteredItems, Next3DaysOrLastMonthOrCurrentAdditionalFilters, SelectionMode.AllItems, DefaultNarrowSelectionFilters);

            this.AssertEvent(widgetId, DateTime.MinValue, DateTime.MaxValue, 11);
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.SitefinityTeam8)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events_Combined_Next3DaysOrLastYear()
        {
            ServerOperations.Events().CreateEvent("UpcomingEventInNext3Days", string.Empty, false, DateTime.Now.AddDays(2), DateTime.Now.AddDays(3));
            ServerOperations.Events().CreateEvent("UpcomingEventInMoreThanNext3Days", string.Empty, false, DateTime.Now.AddDays(5), DateTime.Now.AddDays(7));
            ServerOperations.Events().CreateEvent("PastEventInLastYear", string.Empty, false, DateTime.Now.AddDays(-368), DateTime.Now.AddDays(-365));
            ServerOperations.Events().CreateEvent("PastEventInMoreThanLastYear", string.Empty, false, DateTime.Now.AddDays(-453), DateTime.Now.AddDays(-450));

            var widgetId = this.AddControl(this.CreatePage(), SelectionMode.FilteredItems, Next3DaysOrLastYearAdditionalFilters, SelectionMode.AllItems, DefaultNarrowSelectionFilters);

            this.AssertEvent(widgetId, DateTime.MinValue, DateTime.MaxValue, 5);
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.SitefinityTeam8)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events_Combined_Next3DaysOrLastYearOrCurrent()
        {
            ServerOperations.Events().CreateEvent("UpcomingEventInNext3Days", string.Empty, false, DateTime.Now.AddDays(2), DateTime.Now.AddDays(3));
            ServerOperations.Events().CreateEvent("UpcomingEventInMoreThanNext3Days", string.Empty, false, DateTime.Now.AddDays(5), DateTime.Now.AddDays(7));
            ServerOperations.Events().CreateEvent("PastEventInLastYear", string.Empty, false, DateTime.Now.AddDays(-368), DateTime.Now.AddDays(-365));
            ServerOperations.Events().CreateEvent("PastEventInMoreThanLastYear", string.Empty, false, DateTime.Now.AddDays(-453), DateTime.Now.AddDays(-450));

            var widgetId = this.AddControl(this.CreatePage(), SelectionMode.FilteredItems, Next3DaysOrLastYearOrCurrentAdditionalFilters, SelectionMode.AllItems, DefaultNarrowSelectionFilters);

            this.AssertEvent(widgetId, DateTime.MinValue, DateTime.MaxValue, 11);
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.SitefinityTeam8)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events_Combined_NextWeekOrLast3Days()
        {
            ServerOperations.Events().CreateEvent("UpcomingEventInNextWeek", string.Empty, false, DateTime.Now.AddDays(4), DateTime.Now.AddDays(6));
            ServerOperations.Events().CreateEvent("UpcomingEventInMoreThanNextWeek", string.Empty, false, DateTime.Now.AddDays(9), DateTime.Now.AddDays(12));
            ServerOperations.Events().CreateEvent("PastEventInLast3Days", string.Empty, false, DateTime.Now.AddDays(-2), DateTime.Now.AddDays(-1));
            ServerOperations.Events().CreateEvent("PastEventInMoreThanLast3Days", string.Empty, false, DateTime.Now.AddDays(-6), DateTime.Now.AddDays(-4));

            var widgetId = this.AddControl(this.CreatePage(), SelectionMode.FilteredItems, NextWeekOrLast3DaysAdditionalFilters, SelectionMode.AllItems, DefaultNarrowSelectionFilters);

            this.AssertEvent(widgetId, DateTime.MinValue, DateTime.MaxValue, 5);
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.SitefinityTeam8)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events_Combined_NextWeekOrLast3DaysOrCurrent()
        {
            ServerOperations.Events().CreateEvent("UpcomingEventInNextWeek", string.Empty, false, DateTime.Now.AddDays(4), DateTime.Now.AddDays(6));
            ServerOperations.Events().CreateEvent("UpcomingEventInMoreThanNextWeek", string.Empty, false, DateTime.Now.AddDays(9), DateTime.Now.AddDays(12));
            ServerOperations.Events().CreateEvent("PastEventInLast3Days", string.Empty, false, DateTime.Now.AddDays(-2), DateTime.Now.AddDays(-1));
            ServerOperations.Events().CreateEvent("PastEventInMoreThanLast3Days", string.Empty, false, DateTime.Now.AddDays(-6), DateTime.Now.AddDays(-4));

            var widgetId = this.AddControl(this.CreatePage(), SelectionMode.FilteredItems, NextWeekOrLast3DaysOrCurrentAdditionalFilters, SelectionMode.AllItems, DefaultNarrowSelectionFilters);

            this.AssertEvent(widgetId, DateTime.MinValue, DateTime.MaxValue, 11);
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.SitefinityTeam8)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events_Combined_NextWeekOrLastWeek()
        {
            ServerOperations.Events().CreateEvent("UpcomingEventInNextWeek", string.Empty, false, DateTime.Now.AddDays(4), DateTime.Now.AddDays(6));
            ServerOperations.Events().CreateEvent("UpcomingEventInMoreThanNextWeek", string.Empty, false, DateTime.Now.AddDays(9), DateTime.Now.AddDays(12));
            ServerOperations.Events().CreateEvent("PastEventInLastWeek", string.Empty, false, DateTime.Now.AddDays(-6), DateTime.Now.AddDays(-4));
            ServerOperations.Events().CreateEvent("PastEventInMoreThanLastWeek", string.Empty, false, DateTime.Now.AddDays(-12), DateTime.Now.AddDays(-9));

            var widgetId = this.AddControl(this.CreatePage(), SelectionMode.FilteredItems, NextWeekOrLastWeekAdditionalFilters, SelectionMode.AllItems, DefaultNarrowSelectionFilters);

            this.AssertEvent(widgetId, DateTime.MinValue, DateTime.MaxValue, 5);
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.SitefinityTeam8)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events_Combined_NextWeekOrLastWeekOrCurrent()
        {
            ServerOperations.Events().CreateEvent("UpcomingEventInNextWeek", string.Empty, false, DateTime.Now.AddDays(4), DateTime.Now.AddDays(6));
            ServerOperations.Events().CreateEvent("UpcomingEventInMoreThanNextWeek", string.Empty, false, DateTime.Now.AddDays(9), DateTime.Now.AddDays(12));
            ServerOperations.Events().CreateEvent("PastEventInLastWeek", string.Empty, false, DateTime.Now.AddDays(-6), DateTime.Now.AddDays(-4));
            ServerOperations.Events().CreateEvent("PastEventInMoreThanLastWeek", string.Empty, false, DateTime.Now.AddDays(-12), DateTime.Now.AddDays(-9));

            var widgetId = this.AddControl(this.CreatePage(), SelectionMode.FilteredItems, NextWeekOrLastWeekOrCurrentAdditionalFilters, SelectionMode.AllItems, DefaultNarrowSelectionFilters);

            this.AssertEvent(widgetId, DateTime.MinValue, DateTime.MaxValue, 11);
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.SitefinityTeam8)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events_Combined_NextWeekOrLastMonth()
        {
            ServerOperations.Events().CreateEvent("UpcomingEventInNextWeek", string.Empty, false, DateTime.Now.AddDays(4), DateTime.Now.AddDays(6));
            ServerOperations.Events().CreateEvent("UpcomingEventInMoreThanNextWeek", string.Empty, false, DateTime.Now.AddDays(9), DateTime.Now.AddDays(12));
            ServerOperations.Events().CreateEvent("PastEventInLastMonth", string.Empty, false, DateTime.Now.AddDays(-22), DateTime.Now.AddDays(-20));
            ServerOperations.Events().CreateEvent("PastEventInMoreThanLastMonth", string.Empty, false, DateTime.Now.AddDays(-42), DateTime.Now.AddDays(-40));

            var widgetId = this.AddControl(this.CreatePage(), SelectionMode.FilteredItems, NextWeekOrLastMonthAdditionalFilters, SelectionMode.AllItems, DefaultNarrowSelectionFilters);

            this.AssertEvent(widgetId, DateTime.MinValue, DateTime.MaxValue, 5);
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.SitefinityTeam8)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events_Combined_NextWeekOrLastMonthOrCurrent()
        {
            ServerOperations.Events().CreateEvent("UpcomingEventInNextWeek", string.Empty, false, DateTime.Now.AddDays(4), DateTime.Now.AddDays(6));
            ServerOperations.Events().CreateEvent("UpcomingEventInMoreThanNextWeek", string.Empty, false, DateTime.Now.AddDays(9), DateTime.Now.AddDays(12));
            ServerOperations.Events().CreateEvent("PastEventInLastMonth", string.Empty, false, DateTime.Now.AddDays(-22), DateTime.Now.AddDays(-20));
            ServerOperations.Events().CreateEvent("PastEventInMoreThanLastMonth", string.Empty, false, DateTime.Now.AddDays(-42), DateTime.Now.AddDays(-40));

            var widgetId = this.AddControl(this.CreatePage(), SelectionMode.FilteredItems, NextWeekOrLastMonthOrCurrentAdditionalFilters, SelectionMode.AllItems, DefaultNarrowSelectionFilters);

            this.AssertEvent(widgetId, DateTime.MinValue, DateTime.MaxValue, 11);
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.SitefinityTeam8)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events_Combined_NextWeekOrLastYear()
        {
            ServerOperations.Events().CreateEvent("UpcomingEventInNextWeek", string.Empty, false, DateTime.Now.AddDays(4), DateTime.Now.AddDays(6));
            ServerOperations.Events().CreateEvent("UpcomingEventInMoreThanNextWeek", string.Empty, false, DateTime.Now.AddDays(9), DateTime.Now.AddDays(12));
            ServerOperations.Events().CreateEvent("PastEventInLastYear", string.Empty, false, DateTime.Now.AddDays(-368), DateTime.Now.AddDays(-365));
            ServerOperations.Events().CreateEvent("PastEventInMoreThanLastYear", string.Empty, false, DateTime.Now.AddDays(-453), DateTime.Now.AddDays(-450));

            var widgetId = this.AddControl(this.CreatePage(), SelectionMode.FilteredItems, NextWeekOrLastYearAdditionalFilters, SelectionMode.AllItems, DefaultNarrowSelectionFilters);

            this.AssertEvent(widgetId, DateTime.MinValue, DateTime.MaxValue, 5);
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.SitefinityTeam8)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events_Combined_NextWeekOrLastYearOrCurrent()
        {
            ServerOperations.Events().CreateEvent("UpcomingEventInNextWeek", string.Empty, false, DateTime.Now.AddDays(4), DateTime.Now.AddDays(6));
            ServerOperations.Events().CreateEvent("UpcomingEventInMoreThanNextWeek", string.Empty, false, DateTime.Now.AddDays(9), DateTime.Now.AddDays(12));
            ServerOperations.Events().CreateEvent("PastEventInLastYear", string.Empty, false, DateTime.Now.AddDays(-368), DateTime.Now.AddDays(-365));
            ServerOperations.Events().CreateEvent("PastEventInMoreThanLastYear", string.Empty, false, DateTime.Now.AddDays(-453), DateTime.Now.AddDays(-450));

            var widgetId = this.AddControl(this.CreatePage(), SelectionMode.FilteredItems, NextWeekOrLastYearOrCurrentAdditionalFilters, SelectionMode.AllItems, DefaultNarrowSelectionFilters);

            this.AssertEvent(widgetId, DateTime.MinValue, DateTime.MaxValue, 11);
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.SitefinityTeam8)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events_Combined_NextMonthOrLast3Days()
        {
            ServerOperations.Events().CreateEvent("UpcomingEventInNextMonth", string.Empty, false, DateTime.Now.AddDays(20), DateTime.Now.AddDays(22));
            ServerOperations.Events().CreateEvent("UpcomingEventInMoreThanNextMonth", string.Empty, false, DateTime.Now.AddDays(40), DateTime.Now.AddDays(42));
            ServerOperations.Events().CreateEvent("PastEventInLast3Days", string.Empty, false, DateTime.Now.AddDays(-2), DateTime.Now.AddDays(-1));
            ServerOperations.Events().CreateEvent("PastEventInMoreThanLast3Days", string.Empty, false, DateTime.Now.AddDays(-6), DateTime.Now.AddDays(-4));

            var widgetId = this.AddControl(this.CreatePage(), SelectionMode.FilteredItems, NextMonthOrLast3DaysAdditionalFilters, SelectionMode.AllItems, DefaultNarrowSelectionFilters);

            this.AssertEvent(widgetId, DateTime.MinValue, DateTime.MaxValue, 5);
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.SitefinityTeam8)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events_Combined_NextMonthOrLast3DaysOrCurrent()
        {
            ServerOperations.Events().CreateEvent("UpcomingEventInNextMonth", string.Empty, false, DateTime.Now.AddDays(20), DateTime.Now.AddDays(22));
            ServerOperations.Events().CreateEvent("UpcomingEventInMoreThanNextMonth", string.Empty, false, DateTime.Now.AddDays(40), DateTime.Now.AddDays(42));
            ServerOperations.Events().CreateEvent("PastEventInLast3Days", string.Empty, false, DateTime.Now.AddDays(-2), DateTime.Now.AddDays(-1));
            ServerOperations.Events().CreateEvent("PastEventInMoreThanLast3Days", string.Empty, false, DateTime.Now.AddDays(-6), DateTime.Now.AddDays(-4));

            var widgetId = this.AddControl(this.CreatePage(), SelectionMode.FilteredItems, NextMonthOrLast3DaysOrCurrentAdditionalFilters, SelectionMode.AllItems, DefaultNarrowSelectionFilters);

            this.AssertEvent(widgetId, DateTime.MinValue, DateTime.MaxValue, 11);
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.SitefinityTeam8)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events_Combined_NextMonthOrLastWeek()
        {
            ServerOperations.Events().CreateEvent("UpcomingEventInNextMonth", string.Empty, false, DateTime.Now.AddDays(20), DateTime.Now.AddDays(22));
            ServerOperations.Events().CreateEvent("UpcomingEventInMoreThanNextMonth", string.Empty, false, DateTime.Now.AddDays(40), DateTime.Now.AddDays(42));
            ServerOperations.Events().CreateEvent("PastEventInLastWeek", string.Empty, false, DateTime.Now.AddDays(-6), DateTime.Now.AddDays(-4));
            ServerOperations.Events().CreateEvent("PastEventInMoreThanLastWeek", string.Empty, false, DateTime.Now.AddDays(-12), DateTime.Now.AddDays(-9));

            var widgetId = this.AddControl(this.CreatePage(), SelectionMode.FilteredItems, NextMonthOrLastWeekAdditionalFilters, SelectionMode.AllItems, DefaultNarrowSelectionFilters);

            this.AssertEvent(widgetId, DateTime.MinValue, DateTime.MaxValue, 5);
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.SitefinityTeam8)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events_Combined_NextMonthOrLastWeekOrCurrent()
        {
            ServerOperations.Events().CreateEvent("UpcomingEventInNextMonth", string.Empty, false, DateTime.Now.AddDays(20), DateTime.Now.AddDays(22));
            ServerOperations.Events().CreateEvent("UpcomingEventInMoreThanNextMonth", string.Empty, false, DateTime.Now.AddDays(40), DateTime.Now.AddDays(42));
            ServerOperations.Events().CreateEvent("PastEventInLastWeek", string.Empty, false, DateTime.Now.AddDays(-6), DateTime.Now.AddDays(-4));
            ServerOperations.Events().CreateEvent("PastEventInMoreThanLastWeek", string.Empty, false, DateTime.Now.AddDays(-12), DateTime.Now.AddDays(-9));

            var widgetId = this.AddControl(this.CreatePage(), SelectionMode.FilteredItems, NextMonthOrLastWeekOrCurrentAdditionalFilters, SelectionMode.AllItems, DefaultNarrowSelectionFilters);

            this.AssertEvent(widgetId, DateTime.MinValue, DateTime.MaxValue, 11);
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.SitefinityTeam8)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events_Combined_NextMonthOrLastMonth()
        {
            ServerOperations.Events().CreateEvent("UpcomingEventInNextMonth", string.Empty, false, DateTime.Now.AddDays(20), DateTime.Now.AddDays(22));
            ServerOperations.Events().CreateEvent("UpcomingEventInMoreThanNextMonth", string.Empty, false, DateTime.Now.AddDays(40), DateTime.Now.AddDays(42));
            ServerOperations.Events().CreateEvent("PastEventInLastMonth", string.Empty, false, DateTime.Now.AddDays(-22), DateTime.Now.AddDays(-20));
            ServerOperations.Events().CreateEvent("PastEventInMoreThanLastMonth", string.Empty, false, DateTime.Now.AddDays(-42), DateTime.Now.AddDays(-40));

            var widgetId = this.AddControl(this.CreatePage(), SelectionMode.FilteredItems, NextMonthOrLastMonthAdditionalFilters, SelectionMode.AllItems, DefaultNarrowSelectionFilters);

            this.AssertEvent(widgetId, DateTime.MinValue, DateTime.MaxValue, 5);
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.SitefinityTeam8)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events_Combined_NextMonthOrLastMonthOrCurrent()
        {
            ServerOperations.Events().CreateEvent("UpcomingEventInNextMonth", string.Empty, false, DateTime.Now.AddDays(20), DateTime.Now.AddDays(22));
            ServerOperations.Events().CreateEvent("UpcomingEventInMoreThanNextMonth", string.Empty, false, DateTime.Now.AddDays(40), DateTime.Now.AddDays(42));
            ServerOperations.Events().CreateEvent("PastEventInLastMonth", string.Empty, false, DateTime.Now.AddDays(-22), DateTime.Now.AddDays(-20));
            ServerOperations.Events().CreateEvent("PastEventInMoreThanLastMonth", string.Empty, false, DateTime.Now.AddDays(-42), DateTime.Now.AddDays(-40));

            var widgetId = this.AddControl(this.CreatePage(), SelectionMode.FilteredItems, NextMonthOrLastMonthOrCurrentAdditionalFilters, SelectionMode.AllItems, DefaultNarrowSelectionFilters);

            this.AssertEvent(widgetId, DateTime.MinValue, DateTime.MaxValue, 11);
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.SitefinityTeam8)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events_Combined_NextMonthOrLastYear()
        {
            ServerOperations.Events().CreateEvent("UpcomingEventInNextMonth", string.Empty, false, DateTime.Now.AddDays(20), DateTime.Now.AddDays(22));
            ServerOperations.Events().CreateEvent("UpcomingEventInMoreThanNextMonth", string.Empty, false, DateTime.Now.AddDays(40), DateTime.Now.AddDays(42));
            ServerOperations.Events().CreateEvent("PastEventInLastYear", string.Empty, false, DateTime.Now.AddDays(-368), DateTime.Now.AddDays(-365));
            ServerOperations.Events().CreateEvent("PastEventInMoreThanLastYear", string.Empty, false, DateTime.Now.AddDays(-453), DateTime.Now.AddDays(-450));

            var widgetId = this.AddControl(this.CreatePage(), SelectionMode.FilteredItems, NextMonthOrLastYearAdditionalFilters, SelectionMode.AllItems, DefaultNarrowSelectionFilters);

            this.AssertEvent(widgetId, DateTime.MinValue, DateTime.MaxValue, 5);
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.SitefinityTeam8)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events_Combined_NextMonthOrLastYearOrCurrent()
        {
            ServerOperations.Events().CreateEvent("UpcomingEventInNextMonth", string.Empty, false, DateTime.Now.AddDays(20), DateTime.Now.AddDays(22));
            ServerOperations.Events().CreateEvent("UpcomingEventInMoreThanNextMonth", string.Empty, false, DateTime.Now.AddDays(40), DateTime.Now.AddDays(42));
            ServerOperations.Events().CreateEvent("PastEventInLastYear", string.Empty, false, DateTime.Now.AddDays(-368), DateTime.Now.AddDays(-365));
            ServerOperations.Events().CreateEvent("PastEventInMoreThanLastYear", string.Empty, false, DateTime.Now.AddDays(-453), DateTime.Now.AddDays(-450));

            var widgetId = this.AddControl(this.CreatePage(), SelectionMode.FilteredItems, NextMonthOrLastYearOrCurrentAdditionalFilters, SelectionMode.AllItems, DefaultNarrowSelectionFilters);

            this.AssertEvent(widgetId, DateTime.MinValue, DateTime.MaxValue, 11);
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.SitefinityTeam8)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events_Combined_NextYearOrLast3Days()
        {
            ServerOperations.Events().CreateEvent("UpcomingEventInNextYear", string.Empty, false, DateTime.Now.AddDays(365), DateTime.Now.AddDays(368));
            ServerOperations.Events().CreateEvent("UpcomingEventInMoreThanNextYear", string.Empty, false, DateTime.Now.AddDays(450), DateTime.Now.AddDays(453));
            ServerOperations.Events().CreateEvent("PastEventInLast3Days", string.Empty, false, DateTime.Now.AddDays(-2), DateTime.Now.AddDays(-1));
            ServerOperations.Events().CreateEvent("PastEventInMoreThanLast3Days", string.Empty, false, DateTime.Now.AddDays(-6), DateTime.Now.AddDays(-4));

            var widgetId = this.AddControl(this.CreatePage(), SelectionMode.FilteredItems, NextYearOrLast3DaysAdditionalFilters, SelectionMode.AllItems, DefaultNarrowSelectionFilters);

            this.AssertEvent(widgetId, DateTime.MinValue, DateTime.MaxValue, 5);
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.SitefinityTeam8)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events_Combined_NextYearOrLast3DaysOrCurrent()
        {
            ServerOperations.Events().CreateEvent("UpcomingEventInNextYear", string.Empty, false, DateTime.Now.AddDays(365), DateTime.Now.AddDays(368));
            ServerOperations.Events().CreateEvent("UpcomingEventInMoreThanNextYear", string.Empty, false, DateTime.Now.AddDays(450), DateTime.Now.AddDays(453));
            ServerOperations.Events().CreateEvent("PastEventInLast3Days", string.Empty, false, DateTime.Now.AddDays(-2), DateTime.Now.AddDays(-1));
            ServerOperations.Events().CreateEvent("PastEventInMoreThanLast3Days", string.Empty, false, DateTime.Now.AddDays(-6), DateTime.Now.AddDays(-4));

            var widgetId = this.AddControl(this.CreatePage(), SelectionMode.FilteredItems, NextYearOrLast3DaysOrCurrentAdditionalFilters, SelectionMode.AllItems, DefaultNarrowSelectionFilters);

            this.AssertEvent(widgetId, DateTime.MinValue, DateTime.MaxValue, 11);
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.SitefinityTeam8)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events_Combined_NextYearOrLastWeek()
        {
            ServerOperations.Events().CreateEvent("UpcomingEventInNextYear", string.Empty, false, DateTime.Now.AddDays(365), DateTime.Now.AddDays(368));
            ServerOperations.Events().CreateEvent("UpcomingEventInMoreThanNextYear", string.Empty, false, DateTime.Now.AddDays(450), DateTime.Now.AddDays(453));
            ServerOperations.Events().CreateEvent("PastEventInLastWeek", string.Empty, false, DateTime.Now.AddDays(-6), DateTime.Now.AddDays(-4));
            ServerOperations.Events().CreateEvent("PastEventInMoreThanLastWeek", string.Empty, false, DateTime.Now.AddDays(-12), DateTime.Now.AddDays(-9));

            var widgetId = this.AddControl(this.CreatePage(), SelectionMode.FilteredItems, NextYearOrLastWeekAdditionalFilters, SelectionMode.AllItems, DefaultNarrowSelectionFilters);

            this.AssertEvent(widgetId, DateTime.MinValue, DateTime.MaxValue, 5);
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.SitefinityTeam8)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events_Combined_NextYearOrLastWeekOrCurrent()
        {
            ServerOperations.Events().CreateEvent("UpcomingEventInNextYear", string.Empty, false, DateTime.Now.AddDays(365), DateTime.Now.AddDays(368));
            ServerOperations.Events().CreateEvent("UpcomingEventInMoreThanNextYear", string.Empty, false, DateTime.Now.AddDays(450), DateTime.Now.AddDays(453));
            ServerOperations.Events().CreateEvent("PastEventInLastWeek", string.Empty, false, DateTime.Now.AddDays(-6), DateTime.Now.AddDays(-4));
            ServerOperations.Events().CreateEvent("PastEventInMoreThanLastWeek", string.Empty, false, DateTime.Now.AddDays(-12), DateTime.Now.AddDays(-9));

            var widgetId = this.AddControl(this.CreatePage(), SelectionMode.FilteredItems, NextYearOrLastWeekOrCurrentAdditionalFilters, SelectionMode.AllItems, DefaultNarrowSelectionFilters);

            this.AssertEvent(widgetId, DateTime.MinValue, DateTime.MaxValue, 11);
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.SitefinityTeam8)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events_Combined_NextYearOrLastMonth()
        {
            ServerOperations.Events().CreateEvent("UpcomingEventInNextYear", string.Empty, false, DateTime.Now.AddDays(365), DateTime.Now.AddDays(368));
            ServerOperations.Events().CreateEvent("UpcomingEventInMoreThanNextYear", string.Empty, false, DateTime.Now.AddDays(450), DateTime.Now.AddDays(453));
            ServerOperations.Events().CreateEvent("PastEventInLastMonth", string.Empty, false, DateTime.Now.AddDays(-22), DateTime.Now.AddDays(-20));
            ServerOperations.Events().CreateEvent("PastEventInMoreThanLastMonth", string.Empty, false, DateTime.Now.AddDays(-42), DateTime.Now.AddDays(-40));

            var widgetId = this.AddControl(this.CreatePage(), SelectionMode.FilteredItems, NextYearOrLastMonthAdditionalFilters, SelectionMode.AllItems, DefaultNarrowSelectionFilters);

            this.AssertEvent(widgetId, DateTime.MinValue, DateTime.MaxValue, 5);
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.SitefinityTeam8)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events_Combined_NextYearOrLastMonthOrCurrent()
        {
            ServerOperations.Events().CreateEvent("UpcomingEventInNextYear", string.Empty, false, DateTime.Now.AddDays(365), DateTime.Now.AddDays(368));
            ServerOperations.Events().CreateEvent("UpcomingEventInMoreThanNextYear", string.Empty, false, DateTime.Now.AddDays(450), DateTime.Now.AddDays(453));
            ServerOperations.Events().CreateEvent("PastEventInLastMonth", string.Empty, false, DateTime.Now.AddDays(-22), DateTime.Now.AddDays(-20));
            ServerOperations.Events().CreateEvent("PastEventInMoreThanLastMonth", string.Empty, false, DateTime.Now.AddDays(-42), DateTime.Now.AddDays(-40));

            var widgetId = this.AddControl(this.CreatePage(), SelectionMode.FilteredItems, NextYearOrLastMonthOrCurrentAdditionalFilters, SelectionMode.AllItems, DefaultNarrowSelectionFilters);

            this.AssertEvent(widgetId, DateTime.MinValue, DateTime.MaxValue, 11);
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.SitefinityTeam8)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events_Combined_NextYearOrLastYear()
        {
            ServerOperations.Events().CreateEvent("UpcomingEventInNextYear", string.Empty, false, DateTime.Now.AddDays(365), DateTime.Now.AddDays(368));
            ServerOperations.Events().CreateEvent("UpcomingEventInMoreThanNextYear", string.Empty, false, DateTime.Now.AddDays(450), DateTime.Now.AddDays(453));
            ServerOperations.Events().CreateEvent("PastEventInLastYear", string.Empty, false, DateTime.Now.AddDays(-368), DateTime.Now.AddDays(-365));
            ServerOperations.Events().CreateEvent("PastEventInMoreThanLastYear", string.Empty, false, DateTime.Now.AddDays(-453), DateTime.Now.AddDays(-450));

            var widgetId = this.AddControl(this.CreatePage(), SelectionMode.FilteredItems, NextYearOrLastYearAdditionalFilters, SelectionMode.AllItems, DefaultNarrowSelectionFilters);

            this.AssertEvent(widgetId, DateTime.MinValue, DateTime.MaxValue, 5);
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.SitefinityTeam8)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events_Combined_NextYearOrLastYearOrCurrent()
        {
            ServerOperations.Events().CreateEvent("UpcomingEventInNextYear", string.Empty, false, DateTime.Now.AddDays(365), DateTime.Now.AddDays(368));
            ServerOperations.Events().CreateEvent("UpcomingEventInMoreThanNextYear", string.Empty, false, DateTime.Now.AddDays(450), DateTime.Now.AddDays(453));
            ServerOperations.Events().CreateEvent("PastEventInLastYear", string.Empty, false, DateTime.Now.AddDays(-368), DateTime.Now.AddDays(-365));
            ServerOperations.Events().CreateEvent("PastEventInMoreThanLastYear", string.Empty, false, DateTime.Now.AddDays(-453), DateTime.Now.AddDays(-450));

            var widgetId = this.AddControl(this.CreatePage(), SelectionMode.FilteredItems, NextYearOrLastYearOrCurrentAdditionalFilters, SelectionMode.AllItems, DefaultNarrowSelectionFilters);

            this.AssertEvent(widgetId, DateTime.MinValue, DateTime.MaxValue, 11);
        }

        #endregion

        #region Specified combined events - Multilingual

        #endregion

        #region Events - Selected events

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.SitefinityTeam8)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events_SelectedEvents()
        {
            List<Guid> eventListIDs = new List<Guid>() 
            { 
                ServerOperations.Events().CreateEvent("UpcomingEventInNext3Days", string.Empty, false, DateTime.Now.AddDays(2), DateTime.Now.AddDays(3)),
                ServerOperations.Events().CreateEvent("UpcomingEventInMoreThanNext3Days", string.Empty, false, DateTime.Now.AddDays(5), DateTime.Now.AddDays(7))
            };

            var widgetId = this.AddControl(this.CreatePage(), SelectionMode.SelectedItems, DefaultAdditionalFilters, SelectionMode.AllItems, DefaultNarrowSelectionFilters, eventListIDs);

            this.AssertEvent(widgetId, DateTime.MinValue, DateTime.MaxValue, 2);
        }

        #endregion

        #region Events - Filters

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.SitefinityTeam8)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events_Filters_Calendars()
        {
            var manager = EventsManager.GetManager();
            ServerOperations.Events().CreateEvent("UpcomingEventInNextWeek", string.Empty, false, DateTime.Now.AddDays(4), DateTime.Now.AddDays(6));
            ServerOperations.Events().CreateEvent("UpcomingEventInMoreThanNextWeek", string.Empty, false, DateTime.Now.AddDays(9), DateTime.Now.AddDays(12));

            var calendarId1 = this.SetCalendarToEvent(manager, "UpcomingEventInNextWeek", EventSchedulerWidgetTests.Calendar1Title);
            var calendarId2 = this.SetCalendarToEvent(manager, "UpcomingEventInMoreThanNextWeek", EventSchedulerWidgetTests.Calendar2Title);

            string filter = this.GetNarrowSelectionFilter(new Dictionary<Guid, string>() { { calendarId1, "Parent.Id" }, { calendarId2, "Parent.Id" } }, null, null);

            var widgetId = this.AddControl(this.CreatePage(), SelectionMode.AllItems, DefaultAdditionalFilters, SelectionMode.FilteredItems, filter);

            this.AssertEvent(widgetId, DateTime.MinValue, DateTime.MaxValue, 2);
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.SitefinityTeam8)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events_Filters_Tags()
        {
            Guid event1Id = ServerOperations.Events().CreateEvent("PastEventInLastWeek", string.Empty, false, DateTime.Now.AddDays(-6), DateTime.Now.AddDays(-4));
            Guid event2Id = ServerOperations.Events().CreateEvent("PastEventInMoreThanLastWeek", string.Empty, false, DateTime.Now.AddDays(-12), DateTime.Now.AddDays(-9));

            var tag1Id = ServerOperations.Taxonomies().CreateTag("Tag custom 1", "tagcustom1");
            ServerOperations.Events().AssignTaxonToEventItem(event1Id, "Tags", tag1Id);

            var tag2Id = ServerOperations.Taxonomies().CreateTag("Tag custom 2", "tagcustom2");
            ServerOperations.Events().AssignTaxonToEventItem(event2Id, "Tags", tag2Id);

            string filter = this.GetNarrowSelectionFilter(null, new Dictionary<Guid, string>() { { tag1Id, "Tag custom 1" }, { tag2Id, "Tag custom 2" } }, null);

            var widgetId = this.AddControl(this.CreatePage(), SelectionMode.AllItems, DefaultAdditionalFilters, SelectionMode.FilteredItems, filter);

            this.AssertEvent(widgetId, DateTime.MinValue, DateTime.MaxValue, 2);
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.SitefinityTeam8)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events_Filters_Categories()
        {
            Guid event1Id = ServerOperations.Events().CreateEvent("UpcomingEventInNext3Days", string.Empty, false, DateTime.Now.AddDays(2), DateTime.Now.AddDays(3));
            Guid event2Id = ServerOperations.Events().CreateEvent("UpcomingEventInMoreThanNext3Days", string.Empty, false, DateTime.Now.AddDays(5), DateTime.Now.AddDays(7));

            var category1Id = ServerOperations.Taxonomies().CreateCategory("Category custom 1");
            ServerOperations.Events().AssignTaxonToEventItem(event1Id, "Category", category1Id);

            var category2Id = ServerOperations.Taxonomies().CreateCategory("Category custom 2");
            ServerOperations.Events().AssignTaxonToEventItem(event2Id, "Category", category2Id);

            string filter = this.GetNarrowSelectionFilter(null, null, new Dictionary<Guid, string>() { { category1Id, "Category custom 1" }, { category2Id, "Category custom 2" } });

            var widgetId = this.AddControl(this.CreatePage(), SelectionMode.AllItems, DefaultAdditionalFilters, SelectionMode.FilteredItems, filter);

            this.AssertEvent(widgetId, DateTime.MinValue, DateTime.MaxValue, 2);
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.SitefinityTeam8)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events_Filters_CalendarsCategories()
        {
            var manager = EventsManager.GetManager();
            Guid event1Id = ServerOperations.Events().CreateEvent("UpcomingEventInNext3Days", string.Empty, false, DateTime.Now.AddDays(2), DateTime.Now.AddDays(3));
            Guid event2Id = ServerOperations.Events().CreateEvent("UpcomingEventInMoreThanNext3Days", string.Empty, false, DateTime.Now.AddDays(5), DateTime.Now.AddDays(7));
            Guid event3Id = ServerOperations.Events().CreateEvent("UpcomingEventInNextWeek", string.Empty, false, DateTime.Now.AddDays(4), DateTime.Now.AddDays(6));

            // set calendars
            var calendarId1 = this.SetCalendarToEvent(manager, "UpcomingEventInNext3Days", EventSchedulerWidgetTests.Calendar1Title);
            this.SetCalendarToEvent(manager, "UpcomingEventInMoreThanNext3Days", EventSchedulerWidgetTests.Calendar2Title);
            var calendarId3 = this.SetCalendarToEvent(manager, "UpcomingEventInNextWeek", EventSchedulerWidgetTests.Calendar3Title);

            // set categories
            var category1Id = ServerOperations.Taxonomies().CreateCategory("Category custom 1");
            ServerOperations.Events().AssignTaxonToEventItem(event1Id, "Category", category1Id);

            var category2Id = ServerOperations.Taxonomies().CreateCategory("Category custom 2");
            ServerOperations.Events().AssignTaxonToEventItem(event2Id, "Category", category2Id);

            var category3Id = ServerOperations.Taxonomies().CreateCategory("Category custom 3");
            ServerOperations.Events().AssignTaxonToEventItem(event3Id, "Category", category3Id);

            string filter = this.GetNarrowSelectionFilter(
                new Dictionary<Guid, string>() { { calendarId1, "Parent.Id" }, { calendarId3, "Parent.Id" } },
                null,
                new Dictionary<Guid, string>() { { category1Id, "Category custom 1" }, { category2Id, "Category custom 2" }, { category3Id, "Category custom 3" } });

            var widgetId = this.AddControl(this.CreatePage(), SelectionMode.AllItems, DefaultAdditionalFilters, SelectionMode.FilteredItems, filter);

            this.AssertEvent(widgetId, DateTime.MinValue, DateTime.MaxValue, 2);
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.SitefinityTeam8)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events_Filters_CalendarsTags()
        {
            var manager = EventsManager.GetManager();
            Guid event1Id = ServerOperations.Events().CreateEvent("UpcomingEventInNext3Days", string.Empty, false, DateTime.Now.AddDays(2), DateTime.Now.AddDays(3));
            Guid event2Id = ServerOperations.Events().CreateEvent("UpcomingEventInMoreThanNext3Days", string.Empty, false, DateTime.Now.AddDays(5), DateTime.Now.AddDays(7));
            Guid event3Id = ServerOperations.Events().CreateEvent("UpcomingEventInNextWeek", string.Empty, false, DateTime.Now.AddDays(4), DateTime.Now.AddDays(6));

            // set calendars
            this.SetCalendarToEvent(manager, "UpcomingEventInNext3Days", EventSchedulerWidgetTests.Calendar1Title);
            var calendar2Id = this.SetCalendarToEvent(manager, "UpcomingEventInMoreThanNext3Days", EventSchedulerWidgetTests.Calendar2Title);
            var calendar3Id = this.SetCalendarToEvent(manager, "UpcomingEventInNextWeek", EventSchedulerWidgetTests.Calendar3Title);

            // set tags
            var tag1Id = ServerOperations.Taxonomies().CreateTag("Tag custom 1", "tagcustom1");
            ServerOperations.Events().AssignTaxonToEventItem(event1Id, "Tags", tag1Id);

            var tag2Id = ServerOperations.Taxonomies().CreateTag("Tag custom 2", "tagcustom2");
            ServerOperations.Events().AssignTaxonToEventItem(event2Id, "Tags", tag2Id);

            var tag3Id = ServerOperations.Taxonomies().CreateTag("Tag custom 3", "tagcustom3");
            ServerOperations.Events().AssignTaxonToEventItem(event3Id, "Tags", tag3Id);

            string filter = this.GetNarrowSelectionFilter(
                new Dictionary<Guid, string>() { { calendar2Id, "Parent.Id" }, { calendar3Id, "Parent.Id" } },
                new Dictionary<Guid, string>() { { tag1Id, "Tag custom 1" }, { tag2Id, "Tag custom 2" } },
                null);

            var widgetId = this.AddControl(this.CreatePage(), SelectionMode.AllItems, DefaultAdditionalFilters, SelectionMode.FilteredItems, filter);

            this.AssertEvent(widgetId, DateTime.MinValue, DateTime.MaxValue, 1);
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.SitefinityTeam8)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events_Filters_CategoriesTags()
        {
            Guid event1Id = ServerOperations.Events().CreateEvent("UpcomingEventInNext3Days", string.Empty, false, DateTime.Now.AddDays(2), DateTime.Now.AddDays(3));
            Guid event2Id = ServerOperations.Events().CreateEvent("UpcomingEventInMoreThanNext3Days", string.Empty, false, DateTime.Now.AddDays(5), DateTime.Now.AddDays(7));
            Guid event3Id = ServerOperations.Events().CreateEvent("PastEventInLast3Days", string.Empty, false, DateTime.Now.AddDays(-2), DateTime.Now.AddDays(-1));

            // set categories
            var category1Id = ServerOperations.Taxonomies().CreateCategory("Category custom 1");
            ServerOperations.Events().AssignTaxonToEventItem(event1Id, "Category", category1Id);

            var category2Id = ServerOperations.Taxonomies().CreateCategory("Category custom 2");
            ServerOperations.Events().AssignTaxonToEventItem(event2Id, "Category", category2Id);

            var category3Id = ServerOperations.Taxonomies().CreateCategory("Category custom 3");
            ServerOperations.Events().AssignTaxonToEventItem(event3Id, "Category", category3Id);

            // set tags
            var tag1Id = ServerOperations.Taxonomies().CreateTag("Tag custom 1", "tagcustom1");
            ServerOperations.Events().AssignTaxonToEventItem(event1Id, "Tags", tag1Id);

            var tag2Id = ServerOperations.Taxonomies().CreateTag("Tag custom 2", "tagcustom2");
            ServerOperations.Events().AssignTaxonToEventItem(event2Id, "Tags", tag2Id);

            var tag3Id = ServerOperations.Taxonomies().CreateTag("Tag custom 3", "tagcustom3");
            ServerOperations.Events().AssignTaxonToEventItem(event3Id, "Tags", tag3Id);

            string filter = this.GetNarrowSelectionFilter(
                null,
                new Dictionary<Guid, string>() { { tag1Id, "Tag custom 1" }, { tag2Id, "Tag custom 2" } },
                new Dictionary<Guid, string>() { { category2Id, "Category custom 2" }, { category3Id, "Category custom 3" } });

            var widgetId = this.AddControl(this.CreatePage(), SelectionMode.AllItems, DefaultAdditionalFilters, SelectionMode.FilteredItems, filter);

            this.AssertEvent(widgetId, DateTime.MinValue, DateTime.MaxValue, 1);
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.SitefinityTeam8)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events_Filters_CalendarsCategoriesTags()
        {
            var manager = EventsManager.GetManager();
            Guid event1Id = ServerOperations.Events().CreateEvent("UpcomingEventInNext3Days", string.Empty, false, DateTime.Now.AddDays(2), DateTime.Now.AddDays(3));
            Guid event2Id = ServerOperations.Events().CreateEvent("UpcomingEventInMoreThanNext3Days", string.Empty, false, DateTime.Now.AddDays(5), DateTime.Now.AddDays(7));
            Guid event3Id = ServerOperations.Events().CreateEvent("PastEventInLast3Days", string.Empty, false, DateTime.Now.AddDays(-2), DateTime.Now.AddDays(-1));

            // set calendars
            var calendar1Id = this.SetCalendarToEvent(manager, "UpcomingEventInNext3Days", EventSchedulerWidgetTests.Calendar1Title);
            var calendar2Id = this.SetCalendarToEvent(manager, "UpcomingEventInMoreThanNext3Days", EventSchedulerWidgetTests.Calendar2Title);
            this.SetCalendarToEvent(manager, "UpcomingEventInNextWeek", EventSchedulerWidgetTests.Calendar3Title);

            // set categories
            var category1Id = ServerOperations.Taxonomies().CreateCategory("Category custom 1");
            ServerOperations.Events().AssignTaxonToEventItem(event1Id, "Category", category1Id);

            var category2Id = ServerOperations.Taxonomies().CreateCategory("Category custom 2");
            ServerOperations.Events().AssignTaxonToEventItem(event2Id, "Category", category2Id);

            var category3Id = ServerOperations.Taxonomies().CreateCategory("Category custom 3");
            ServerOperations.Events().AssignTaxonToEventItem(event3Id, "Category", category3Id);

            // set tags
            var tag1Id = ServerOperations.Taxonomies().CreateTag("Tag custom 1", "tagcustom1");
            ServerOperations.Events().AssignTaxonToEventItem(event1Id, "Tags", tag1Id);

            var tag2Id = ServerOperations.Taxonomies().CreateTag("Tag custom 2", "tagcustom2");
            ServerOperations.Events().AssignTaxonToEventItem(event2Id, "Tags", tag2Id);

            var tag3Id = ServerOperations.Taxonomies().CreateTag("Tag custom 3", "tagcustom3");
            ServerOperations.Events().AssignTaxonToEventItem(event3Id, "Tags", tag3Id);

            string filter = this.GetNarrowSelectionFilter(
                new Dictionary<Guid, string>() { { calendar1Id, "Parent.Id" }, { calendar2Id, "Parent.Id" } },
                new Dictionary<Guid, string>() { { tag2Id, "Tag custom 2" }, { tag3Id, "Tag custom 3" } },
                new Dictionary<Guid, string>() { { category2Id, "Category custom 2" } });

            var widgetId = this.AddControl(this.CreatePage(), SelectionMode.AllItems, DefaultAdditionalFilters, SelectionMode.FilteredItems, filter);

            this.AssertEvent(widgetId, DateTime.MinValue, DateTime.MaxValue, 1);
        }

        #endregion

        #region Calendars

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.SitefinityTeam8)]
        [Description("Ensure that after calendar request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Calendar()
        {
            var manager = EventsManager.GetManager();

            var calendartDefault = manager.GetCalendars().FirstOrDefault();
            this.SetCalendarToEvent(manager, BaseUpcomingEventTitle, calendartDefault.Title);
            this.SetCalendarToEvent(manager, BaseRepeatEventTitle, EventSchedulerWidgetTests.Calendar1Title);
            this.SetCalendarToEvent(manager, BasePastEventTitle, EventSchedulerWidgetTests.Calendar2Title);

            var widgetId = this.AddControl(this.CreatePage(), SelectionMode.AllItems, DefaultAdditionalFilters, SelectionMode.AllItems, DefaultNarrowSelectionFilters);

            this.AssertCalendar(widgetId, DateTime.MinValue, DateTime.MaxValue, 3);
        }

        #endregion

        #region Calendars - Multilingual

        [Test]
        [Multilingual]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.SitefinityTeam8)]
        [Description("Ensure that after calendar request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Calendar_Multilingual()
        {
            var multiOperations = new MultilingualEventOperations();
            var bulgarian = AppSettings.CurrentSettings.DefinedFrontendLanguages.Where(x => x.Name == "bg-BG").FirstOrDefault();

            var calendar1Id = this.CreateLocalizedCalendar(Calendar1Title, Calendar1Title + " bg", bulgarian);
            this.CreateLocalizedEvent(multiOperations, "Event 1 bg", Guid.NewGuid(), DateTime.Now, DateTime.Now.AddDays(1), false, false, calendar1Id, bulgarian);

            var calendar2Id = this.CreateLocalizedCalendar(Calendar2Title, Calendar1Title + " bg", bulgarian);
            this.CreateLocalizedEvent(multiOperations, "Event 2 bg", Guid.NewGuid(), DateTime.Now, DateTime.Now.AddDays(1), false, false, calendar2Id, bulgarian);

            var widgetId = this.AddControl(this.CreatePage(bulgarian), SelectionMode.AllItems, DefaultAdditionalFilters, SelectionMode.AllItems, DefaultNarrowSelectionFilters);

            this.AssertCalendar(widgetId, DateTime.MinValue, DateTime.MaxValue, 2, bulgarian);
        }

        #endregion

        #region Helper functions

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

        private void AssertCalendar(Guid widgetId, DateTime filterStartDate, DateTime filterEndDate, long expectedCalendarCount, CultureInfo culture = null)
        {
            var filters = this.CreateFilterQueryString(widgetId, filterStartDate, filterEndDate, culture);
            var pageContent = WebRequestHelper.GetPageWebContent(UrlPath.ResolveAbsoluteUrl("~/web-interface/calendars/?") + filters);

            Assert.DoesNotContain(pageContent, JsonExceptionMessage, JsonExceptionMessage);
            List<EventCalendarViewModel> calendarOccurrenceList = JsonSerializer.DeserializeFromString<List<EventCalendarViewModel>>(pageContent);
            Assert.AreEqual(calendarOccurrenceList.LongCount(), expectedCalendarCount);
        }

        private void AssertEvent(Guid widgetId, DateTime filterStartDate, DateTime filterEndDate, long expectedEventOccurrences, CultureInfo culture = null)
        {
            var filters = this.CreateFilterQueryString(widgetId, filterStartDate, filterEndDate, culture);
            var pageContent = WebRequestHelper.GetPageWebContent(UrlPath.ResolveAbsoluteUrl("/web-interface/events/?") + filters);

            Assert.DoesNotContain(pageContent, JsonExceptionMessage, JsonExceptionMessage);
            List<EventOccurrenceViewModel> eventOccurrenceList = JsonSerializer.DeserializeFromString<List<EventOccurrenceViewModel>>(pageContent);
            Assert.AreEqual(eventOccurrenceList.LongCount(), expectedEventOccurrences);
        }

        private void CreateLocalizedEvent(MultilingualEventOperations multiOperations, string eventTitle, Guid eventId, DateTime start, DateTime end, bool allDay, bool isRecurrent, Guid calendarId, CultureInfo culture = null, string recurrenceExpression = null)
        {
            multiOperations.CreateLocalizedEvent(eventId, calendarId, eventTitle, string.Empty, string.Empty, start, end, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, isRecurrent, false, culture == null ? null : culture.Name);

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

            if (end == DateTime.MaxValue)
            {
                eventItem.EventEnd = null;
            }

            eventsManager.Lifecycle.Publish(eventItem, culture);
            eventsManager.SaveChanges();
        }

        private Guid CreateLocalizedCalendar(string calendarTitle, string localizedTitle, CultureInfo culture)
        {
            var id = Guid.Empty;

            var calendar = EventsManager.GetManager().GetCalendars().Where(p => p.Title == calendarTitle).FirstOrDefault();
            if (calendar == null)
            {
                id = ServerOperations.Events().CreateCalendar(Guid.NewGuid(), localizedTitle, culture.Name);
            }
            else
            {
                id = ServerOperations.Events().CreateCalendar(calendar.Id, localizedTitle, culture.Name);
            }

            return id;
        }

        private string CreateFilterQueryString(Guid widgetId, DateTime startDate, DateTime endDate, CultureInfo culture)
        {
            var queryStringDictionary = typeof(EventsFilter).GetProperties()
                .Where(p => p.GetValue(new EventsFilter(), null) != null)
                .Select(property => new { Key = property.Name, Value = HttpUtility.UrlEncode(property.GetValue(new EventsFilter(), null).ToString()) })
                .ToDictionary(t => t.Key, t => t.Value);

            queryStringDictionary["Id"] = widgetId.ToString();
            queryStringDictionary["StartDate"] = HttpUtility.UrlEncode(startDate.ToString("u", System.Globalization.CultureInfo.InvariantCulture));
            queryStringDictionary["EndDate"] = HttpUtility.UrlEncode((endDate == DateTime.MaxValue ? DateTime.MaxValue.AddDays(-1) : endDate).ToString("u", System.Globalization.CultureInfo.InvariantCulture));

            if (culture != null)
            {
                queryStringDictionary["UICulture"] = culture.Name;
            }

            var queryStringValue = string.Join("&", queryStringDictionary.Select(t => t.Key + "=" + t.Value).ToArray());
            return queryStringValue;
        }

        private string GetNarrowSelectionFilter(Dictionary<Guid, string> calendarData, Dictionary<Guid, string> tagsData, Dictionary<Guid, string> categoryData)
        {
            NarrowFilter calendarFilter = new NarrowFilter();
            calendarFilter.DataList = calendarData;
            calendarFilter.FieldName = "Parent.Id.ToString()";
            calendarFilter.Group = "Calendars";
            calendarFilter.FieldType = "System.String";

            NarrowFilter tagFilter = new NarrowFilter();
            tagFilter.DataList = tagsData;
            tagFilter.FieldName = "Tags";
            tagFilter.Group = "Tags";
            tagFilter.FieldType = "System.Guid";

            NarrowFilter categoryFilter = new NarrowFilter();
            categoryFilter.DataList = categoryData;
            categoryFilter.FieldName = "Category";
            categoryFilter.Group = "Category";
            categoryFilter.FieldType = "System.Guid";

            var narrowFilterList = new List<NarrowFilter> { calendarFilter, tagFilter, categoryFilter }.Where(p => p.DataList != null).ToList();

            var queryData = new QueryData();
            var queryItemsList = new List<QueryItem>();

            for (int filterIndex = 0; filterIndex < narrowFilterList.Count(); filterIndex++)
            {
                queryItemsList.Add(
                new QueryItem()
                {
                    IsGroup = true,
                    Join = "AND",
                    ItemPath = "_" + filterIndex,
                    Name = narrowFilterList[filterIndex].Group
                });

                var dataList = narrowFilterList[filterIndex].DataList.Where(p => p.Key != Guid.Empty).ToList();

                for (int dataIndex = 0; dataIndex < dataList.Count(); dataIndex++)
                {
                    queryItemsList.Add(
                    new QueryItem()
                    {
                        IsGroup = false,
                        Join = "OR",
                        ItemPath = string.Format(CultureInfo.InvariantCulture, "_{0}_{1}", filterIndex, dataIndex),
                        Value = dataList[dataIndex].Key.ToString("D"),
                        Condition = new Condition()
                        {
                            FieldName = narrowFilterList[filterIndex].FieldName,
                            FieldType = narrowFilterList[filterIndex].FieldType,
                            Operator = "Contains"
                        },
                        Name = dataList[dataIndex].Value
                    });
                }
            }

            queryData.QueryItems = queryItemsList.ToArray();

            return JsonSerializer.SerializeToString(queryData, typeof(QueryData));
        }

        private Guid SetCalendarToEvent(EventsManager manager, string eventTitle, string calendarTitle)
        {
            var selectedEvent = manager.GetEvents().Where(i => i.Title == eventTitle).FirstOrDefault();
            var selectedCalendar = manager.GetCalendars().Where(i => i.Title == calendarTitle).FirstOrDefault();

            if (selectedEvent != null && selectedCalendar != null)
            {
                selectedEvent.Parent = selectedCalendar;
                manager.Lifecycle.Publish(selectedEvent);
                manager.SaveChanges();
                return selectedCalendar.Id;
            }

            return Guid.Empty;
        }

        private Guid CreatePage(CultureInfo culture = null, string pageName = null)
        {
            culture = culture ?? CultureInfo.CurrentUICulture;
            return new MultilingualPageOperations().CreatePageMultilingual(Guid.Empty, pageName ?? PageNamePrefix + " " + culture.Name, false, culture.Name);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "Telerik.Sitefinity.Mvc.TestUtilities.Data.PageContentGenerator.AddControlToPage(System.Guid,System.Web.UI.Control,System.String,System.String,System.Action<Telerik.Sitefinity.Pages.Model.PageDraftControl>)")]
        private Guid AddControl(Guid pageId, SelectionMode additionalFiltersSelectionMode, string additionalFilters, SelectionMode narrowFiltersSelectionMode, string narrowSelectionFilters, List<Guid> selectedItemsIds = null)
        {
            var mvcProxy = new MvcControllerProxy();
            mvcProxy.ControllerName = typeof(EventSchedulerController).FullName;
            var eventSchedulerController = new EventSchedulerController();
            this.ApplyFilters(eventSchedulerController, additionalFiltersSelectionMode, additionalFilters, narrowFiltersSelectionMode, narrowSelectionFilters, selectedItemsIds);
            mvcProxy.Settings = new ControllerSettings(eventSchedulerController);
            string controlId = Telerik.Sitefinity.Mvc.TestUtilities.Data.PageContentGenerator.AddControlToPage(pageId, mvcProxy, Caption);

            return this.GetWidgetId(pageId, controlId);
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

        /// <summary>
        /// Get controller widget id
        /// </summary>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1307:SpecifyStringComparison", MessageId = "System.String.EndsWith(System.String)")]
        private Guid GetWidgetId(Guid pageId, string controlId)
        {
            var page = PageManager.GetManager().GetPageNode(pageId).Page;

            var control = page.Controls.FirstOrDefault(p => p.Properties.FirstOrDefault(t => t.Name == "ID" && controlId.EndsWith(controlId)) != null);
            if (control != null)
            {
                return control.Id;
            }

            return Guid.Empty;
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

        private const string Next3DaysAndCurrentAdditionalFilters = "{\"__msdisposeindex\":300,\"Title\":null,\"Id\":\"00000000-0000-0000-0000-000000000000\",\"QueryItems\":[{\"IsGroup\":true,\"Ordinal\":0,\"Join\":\"OR\",\"ItemPath\":\"_0\",\"Value\":null,\"Condition\":{\"FieldName\":null,\"FieldType\":null,\"Operator\":null,\"__msdisposeindex\":302},\"Name\":\"Upcoming\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":301},{\"IsGroup\":false,\"Ordinal\":0,\"Join\":\"AND\",\"ItemPath\":\"_0_0\",\"Value\":\"DateTime.UtcNow.AddDays(3.0)\",\"Condition\":{\"FieldName\":\"EventStart\",\"FieldType\":\"System.DateTime\",\"Operator\":\"<=\",\"__msdisposeindex\":304},\"Name\":\"EventStart.DateTime.UtcNow.AddDays(3.0)\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":303},{\"IsGroup\":false,\"Ordinal\":1,\"Join\":\"AND\",\"ItemPath\":\"_0_1\",\"Value\":\"DateTime.UtcNow\",\"Condition\":{\"FieldName\":\"EventStart\",\"FieldType\":\"System.DateTime\",\"Operator\":\">=\",\"__msdisposeindex\":306},\"Name\":null,\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":305},{\"IsGroup\":true,\"Ordinal\":1,\"Join\":\"OR\",\"ItemPath\":\"_1\",\"Value\":null,\"Condition\":{\"FieldName\":null,\"FieldType\":null,\"Operator\":null,\"__msdisposeindex\":308},\"Name\":\"Current\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":307},{\"IsGroup\":false,\"Ordinal\":0,\"Join\":\"AND\",\"ItemPath\":\"_1_0\",\"Value\":\"DateTime.UtcNow\",\"Condition\":{\"FieldName\":\"EventStart\",\"FieldType\":\"System.DateTime\",\"Operator\":\"<=\",\"__msdisposeindex\":310},\"Name\":null,\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":309},{\"IsGroup\":false,\"Ordinal\":1,\"Join\":\"AND\",\"ItemPath\":\"_1_1\",\"Value\":\"DateTime.UtcNow\",\"Condition\":{\"FieldName\":\"EventEnd\",\"FieldType\":\"System.DateTime\",\"Operator\":\">=\",\"__msdisposeindex\":312},\"Name\":null,\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":311}],\"TypeProperties\":[],\"_itemPathSeparator\":\"_\"}";
        private const string NextWeekAndCurrentAdditionalFilters = "{\"__msdisposeindex\":333,\"Title\":null,\"Id\":\"00000000-0000-0000-0000-000000000000\",\"QueryItems\":[{\"IsGroup\":true,\"Ordinal\":0,\"Join\":\"OR\",\"ItemPath\":\"_0\",\"Value\":null,\"Condition\":{\"FieldName\":null,\"FieldType\":null,\"Operator\":null,\"__msdisposeindex\":335},\"Name\":\"Current\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":334},{\"IsGroup\":false,\"Ordinal\":0,\"Join\":\"AND\",\"ItemPath\":\"_0_0\",\"Value\":\"DateTime.UtcNow\",\"Condition\":{\"FieldName\":\"EventStart\",\"FieldType\":\"System.DateTime\",\"Operator\":\"<=\",\"__msdisposeindex\":337},\"Name\":null,\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":336},{\"IsGroup\":false,\"Ordinal\":1,\"Join\":\"AND\",\"ItemPath\":\"_0_1\",\"Value\":\"DateTime.UtcNow\",\"Condition\":{\"FieldName\":\"EventEnd\",\"FieldType\":\"System.DateTime\",\"Operator\":\">=\",\"__msdisposeindex\":339},\"Name\":null,\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":338},{\"IsGroup\":true,\"Ordinal\":1,\"Join\":\"OR\",\"ItemPath\":\"_1\",\"Value\":null,\"Condition\":{\"FieldName\":null,\"FieldType\":null,\"Operator\":null,\"__msdisposeindex\":341},\"Name\":\"Upcoming\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":340},{\"IsGroup\":false,\"Ordinal\":0,\"Join\":\"AND\",\"ItemPath\":\"_1_0\",\"Value\":\"DateTime.UtcNow.AddDays(7.0)\",\"Condition\":{\"FieldName\":\"EventStart\",\"FieldType\":\"System.DateTime\",\"Operator\":\"<=\",\"__msdisposeindex\":343},\"Name\":\"EventStart.DateTime.UtcNow.AddDays(7.0)\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":342},{\"IsGroup\":false,\"Ordinal\":1,\"Join\":\"AND\",\"ItemPath\":\"_1_1\",\"Value\":\"DateTime.UtcNow\",\"Condition\":{\"FieldName\":\"EventStart\",\"FieldType\":\"System.DateTime\",\"Operator\":\">=\",\"__msdisposeindex\":345},\"Name\":null,\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":344}],\"TypeProperties\":[],\"_itemPathSeparator\":\"_\"}";
        private const string NextMonthAndCurrentAdditionalFilters = "{\"__msdisposeindex\":366,\"Title\":null,\"Id\":\"00000000-0000-0000-0000-000000000000\",\"QueryItems\":[{\"IsGroup\":true,\"Ordinal\":0,\"Join\":\"OR\",\"ItemPath\":\"_0\",\"Value\":null,\"Condition\":{\"FieldName\":null,\"FieldType\":null,\"Operator\":null,\"__msdisposeindex\":368},\"Name\":\"Current\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":367},{\"IsGroup\":false,\"Ordinal\":0,\"Join\":\"AND\",\"ItemPath\":\"_0_0\",\"Value\":\"DateTime.UtcNow\",\"Condition\":{\"FieldName\":\"EventStart\",\"FieldType\":\"System.DateTime\",\"Operator\":\"<=\",\"__msdisposeindex\":370},\"Name\":null,\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":369},{\"IsGroup\":false,\"Ordinal\":1,\"Join\":\"AND\",\"ItemPath\":\"_0_1\",\"Value\":\"DateTime.UtcNow\",\"Condition\":{\"FieldName\":\"EventEnd\",\"FieldType\":\"System.DateTime\",\"Operator\":\">=\",\"__msdisposeindex\":372},\"Name\":null,\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":371},{\"IsGroup\":true,\"Ordinal\":1,\"Join\":\"OR\",\"ItemPath\":\"_1\",\"Value\":null,\"Condition\":{\"FieldName\":null,\"FieldType\":null,\"Operator\":null,\"__msdisposeindex\":374},\"Name\":\"Upcoming\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":373},{\"IsGroup\":false,\"Ordinal\":0,\"Join\":\"AND\",\"ItemPath\":\"_1_0\",\"Value\":\"DateTime.UtcNow.AddMonths(1)\",\"Condition\":{\"FieldName\":\"EventStart\",\"FieldType\":\"System.DateTime\",\"Operator\":\"<=\",\"__msdisposeindex\":376},\"Name\":\"EventStart.DateTime.UtcNow.AddMonths(1)\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":375},{\"IsGroup\":false,\"Ordinal\":1,\"Join\":\"AND\",\"ItemPath\":\"_1_1\",\"Value\":\"DateTime.UtcNow\",\"Condition\":{\"FieldName\":\"EventStart\",\"FieldType\":\"System.DateTime\",\"Operator\":\">=\",\"__msdisposeindex\":378},\"Name\":null,\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":377}],\"TypeProperties\":[],\"_itemPathSeparator\":\"_\"}";
        private const string NextYearAndCurrentAdditionalFilters = "{\"__msdisposeindex\":399,\"Title\":null,\"Id\":\"00000000-0000-0000-0000-000000000000\",\"QueryItems\":[{\"IsGroup\":true,\"Ordinal\":0,\"Join\":\"OR\",\"ItemPath\":\"_0\",\"Value\":null,\"Condition\":{\"FieldName\":null,\"FieldType\":null,\"Operator\":null,\"__msdisposeindex\":401},\"Name\":\"Current\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":400},{\"IsGroup\":false,\"Ordinal\":0,\"Join\":\"AND\",\"ItemPath\":\"_0_0\",\"Value\":\"DateTime.UtcNow\",\"Condition\":{\"FieldName\":\"EventStart\",\"FieldType\":\"System.DateTime\",\"Operator\":\"<=\",\"__msdisposeindex\":403},\"Name\":null,\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":402},{\"IsGroup\":false,\"Ordinal\":1,\"Join\":\"AND\",\"ItemPath\":\"_0_1\",\"Value\":\"DateTime.UtcNow\",\"Condition\":{\"FieldName\":\"EventEnd\",\"FieldType\":\"System.DateTime\",\"Operator\":\">=\",\"__msdisposeindex\":405},\"Name\":null,\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":404},{\"IsGroup\":true,\"Ordinal\":1,\"Join\":\"OR\",\"ItemPath\":\"_1\",\"Value\":null,\"Condition\":{\"FieldName\":null,\"FieldType\":null,\"Operator\":null,\"__msdisposeindex\":407},\"Name\":\"Upcoming\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":406},{\"IsGroup\":false,\"Ordinal\":0,\"Join\":\"AND\",\"ItemPath\":\"_1_0\",\"Value\":\"DateTime.UtcNow.AddYears(1)\",\"Condition\":{\"FieldName\":\"EventStart\",\"FieldType\":\"System.DateTime\",\"Operator\":\"<=\",\"__msdisposeindex\":409},\"Name\":\"EventStart.DateTime.UtcNow.AddYears(1)\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":408},{\"IsGroup\":false,\"Ordinal\":1,\"Join\":\"AND\",\"ItemPath\":\"_1_1\",\"Value\":\"DateTime.UtcNow\",\"Condition\":{\"FieldName\":\"EventStart\",\"FieldType\":\"System.DateTime\",\"Operator\":\">=\",\"__msdisposeindex\":411},\"Name\":null,\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":410}],\"TypeProperties\":[],\"_itemPathSeparator\":\"_\"}";

        private const string Last3DaysAdditionalFilters = "{\"__msdisposeindex\":278,\"Title\":null,\"Id\":\"00000000-0000-0000-0000-000000000000\",\"QueryItems\":[{\"IsGroup\":true,\"Ordinal\":0,\"Join\":\"OR\",\"ItemPath\":\"_0\",\"Value\":null,\"Condition\":{\"FieldName\":null,\"FieldType\":null,\"Operator\":null,\"__msdisposeindex\":280},\"Name\":\"Past\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":279},{\"IsGroup\":false,\"Ordinal\":0,\"Join\":\"AND\",\"ItemPath\":\"_0_0\",\"Value\":\"DateTime.UtcNow.AddDays(-3.0)\",\"Condition\":{\"FieldName\":\"EventEnd\",\"FieldType\":\"System.DateTime\",\"Operator\":\">=\",\"__msdisposeindex\":282},\"Name\":\"EventEnd.DateTime.UtcNow.AddDays(-3.0)\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":281},{\"IsGroup\":false,\"Ordinal\":1,\"Join\":\"AND\",\"ItemPath\":\"_0_1\",\"Value\":\"DateTime.UtcNow\",\"Condition\":{\"FieldName\":\"EventEnd\",\"FieldType\":\"System.DateTime\",\"Operator\":\"<=\",\"__msdisposeindex\":284},\"Name\":null,\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":283}],\"TypeProperties\":[],\"_itemPathSeparator\":\"_\"}";
        private const string LastWeekAdditionalFilters = "{\"__msdisposeindex\":299,\"Title\":null,\"Id\":\"00000000-0000-0000-0000-000000000000\",\"QueryItems\":[{\"IsGroup\":true,\"Ordinal\":0,\"Join\":\"OR\",\"ItemPath\":\"_0\",\"Value\":null,\"Condition\":{\"FieldName\":null,\"FieldType\":null,\"Operator\":null,\"__msdisposeindex\":301},\"Name\":\"Past\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":300},{\"IsGroup\":false,\"Ordinal\":0,\"Join\":\"AND\",\"ItemPath\":\"_0_0\",\"Value\":\"DateTime.UtcNow.AddDays(-7.0)\",\"Condition\":{\"FieldName\":\"EventEnd\",\"FieldType\":\"System.DateTime\",\"Operator\":\">=\",\"__msdisposeindex\":303},\"Name\":\"EventEnd.DateTime.UtcNow.AddDays(-7.0)\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":302},{\"IsGroup\":false,\"Ordinal\":1,\"Join\":\"AND\",\"ItemPath\":\"_0_1\",\"Value\":\"DateTime.UtcNow\",\"Condition\":{\"FieldName\":\"EventEnd\",\"FieldType\":\"System.DateTime\",\"Operator\":\"<=\",\"__msdisposeindex\":305},\"Name\":null,\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":304}],\"TypeProperties\":[],\"_itemPathSeparator\":\"_\"}";
        private const string LastMonthAdditionalFilters = "{\"__msdisposeindex\":320,\"Title\":null,\"Id\":\"00000000-0000-0000-0000-000000000000\",\"QueryItems\":[{\"IsGroup\":true,\"Ordinal\":0,\"Join\":\"OR\",\"ItemPath\":\"_0\",\"Value\":null,\"Condition\":{\"FieldName\":null,\"FieldType\":null,\"Operator\":null,\"__msdisposeindex\":322},\"Name\":\"Past\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":321},{\"IsGroup\":false,\"Ordinal\":0,\"Join\":\"AND\",\"ItemPath\":\"_0_0\",\"Value\":\"DateTime.UtcNow.AddMonths(-1)\",\"Condition\":{\"FieldName\":\"EventEnd\",\"FieldType\":\"System.DateTime\",\"Operator\":\">=\",\"__msdisposeindex\":324},\"Name\":\"EventEnd.DateTime.UtcNow.AddMonths(-1)\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":323},{\"IsGroup\":false,\"Ordinal\":1,\"Join\":\"AND\",\"ItemPath\":\"_0_1\",\"Value\":\"DateTime.UtcNow\",\"Condition\":{\"FieldName\":\"EventEnd\",\"FieldType\":\"System.DateTime\",\"Operator\":\"<=\",\"__msdisposeindex\":326},\"Name\":null,\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":325}],\"TypeProperties\":[],\"_itemPathSeparator\":\"_\"}";
        private const string LastYearAdditionalFilters = "{\"__msdisposeindex\":341,\"Title\":null,\"Id\":\"00000000-0000-0000-0000-000000000000\",\"QueryItems\":[{\"IsGroup\":true,\"Ordinal\":0,\"Join\":\"OR\",\"ItemPath\":\"_0\",\"Value\":null,\"Condition\":{\"FieldName\":null,\"FieldType\":null,\"Operator\":null,\"__msdisposeindex\":343},\"Name\":\"Past\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":342},{\"IsGroup\":false,\"Ordinal\":0,\"Join\":\"AND\",\"ItemPath\":\"_0_0\",\"Value\":\"DateTime.UtcNow.AddYears(-1)\",\"Condition\":{\"FieldName\":\"EventEnd\",\"FieldType\":\"System.DateTime\",\"Operator\":\">=\",\"__msdisposeindex\":345},\"Name\":\"EventEnd.DateTime.UtcNow.AddYears(-1)\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":344},{\"IsGroup\":false,\"Ordinal\":1,\"Join\":\"AND\",\"ItemPath\":\"_0_1\",\"Value\":\"DateTime.UtcNow\",\"Condition\":{\"FieldName\":\"EventEnd\",\"FieldType\":\"System.DateTime\",\"Operator\":\"<=\",\"__msdisposeindex\":347},\"Name\":null,\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":346}],\"TypeProperties\":[],\"_itemPathSeparator\":\"_\"}";
        private const string LastCustomRangeAdditionalFiltersFormat = "";

        private const string Last3DaysAndCurrentAdditionalFilters = "{\"__msdisposeindex\":435,\"Title\":null,\"Id\":\"00000000-0000-0000-0000-000000000000\",\"QueryItems\":[{\"IsGroup\":true,\"Ordinal\":0,\"Join\":\"OR\",\"ItemPath\":\"_0\",\"Value\":null,\"Condition\":{\"FieldName\":null,\"FieldType\":null,\"Operator\":null,\"__msdisposeindex\":437},\"Name\":\"Current\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":436},{\"IsGroup\":false,\"Ordinal\":0,\"Join\":\"AND\",\"ItemPath\":\"_0_0\",\"Value\":\"DateTime.UtcNow\",\"Condition\":{\"FieldName\":\"EventStart\",\"FieldType\":\"System.DateTime\",\"Operator\":\"<=\",\"__msdisposeindex\":439},\"Name\":null,\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":438},{\"IsGroup\":false,\"Ordinal\":1,\"Join\":\"AND\",\"ItemPath\":\"_0_1\",\"Value\":\"DateTime.UtcNow\",\"Condition\":{\"FieldName\":\"EventEnd\",\"FieldType\":\"System.DateTime\",\"Operator\":\">=\",\"__msdisposeindex\":441},\"Name\":null,\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":440},{\"IsGroup\":true,\"Ordinal\":1,\"Join\":\"OR\",\"ItemPath\":\"_1\",\"Value\":null,\"Condition\":{\"FieldName\":null,\"FieldType\":null,\"Operator\":null,\"__msdisposeindex\":443},\"Name\":\"Past\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":442},{\"IsGroup\":false,\"Ordinal\":0,\"Join\":\"AND\",\"ItemPath\":\"_1_0\",\"Value\":\"DateTime.UtcNow.AddDays(-3.0)\",\"Condition\":{\"FieldName\":\"EventEnd\",\"FieldType\":\"System.DateTime\",\"Operator\":\">=\",\"__msdisposeindex\":445},\"Name\":\"EventEnd.DateTime.UtcNow.AddDays(-3.0)\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":444},{\"IsGroup\":false,\"Ordinal\":1,\"Join\":\"AND\",\"ItemPath\":\"_1_1\",\"Value\":\"DateTime.UtcNow\",\"Condition\":{\"FieldName\":\"EventEnd\",\"FieldType\":\"System.DateTime\",\"Operator\":\"<=\",\"__msdisposeindex\":447},\"Name\":null,\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":446}],\"TypeProperties\":[],\"_itemPathSeparator\":\"_\"}";
        private const string LastWeekAndCurrentAdditionalFilters = "{\"__msdisposeindex\":468,\"Title\":null,\"Id\":\"00000000-0000-0000-0000-000000000000\",\"QueryItems\":[{\"IsGroup\":true,\"Ordinal\":0,\"Join\":\"OR\",\"ItemPath\":\"_0\",\"Value\":null,\"Condition\":{\"FieldName\":null,\"FieldType\":null,\"Operator\":null,\"__msdisposeindex\":470},\"Name\":\"Current\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":469},{\"IsGroup\":false,\"Ordinal\":0,\"Join\":\"AND\",\"ItemPath\":\"_0_0\",\"Value\":\"DateTime.UtcNow\",\"Condition\":{\"FieldName\":\"EventStart\",\"FieldType\":\"System.DateTime\",\"Operator\":\"<=\",\"__msdisposeindex\":472},\"Name\":null,\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":471},{\"IsGroup\":false,\"Ordinal\":1,\"Join\":\"AND\",\"ItemPath\":\"_0_1\",\"Value\":\"DateTime.UtcNow\",\"Condition\":{\"FieldName\":\"EventEnd\",\"FieldType\":\"System.DateTime\",\"Operator\":\">=\",\"__msdisposeindex\":474},\"Name\":null,\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":473},{\"IsGroup\":true,\"Ordinal\":1,\"Join\":\"OR\",\"ItemPath\":\"_1\",\"Value\":null,\"Condition\":{\"FieldName\":null,\"FieldType\":null,\"Operator\":null,\"__msdisposeindex\":476},\"Name\":\"Past\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":475},{\"IsGroup\":false,\"Ordinal\":0,\"Join\":\"AND\",\"ItemPath\":\"_1_0\",\"Value\":\"DateTime.UtcNow.AddDays(-7.0)\",\"Condition\":{\"FieldName\":\"EventEnd\",\"FieldType\":\"System.DateTime\",\"Operator\":\">=\",\"__msdisposeindex\":478},\"Name\":\"EventEnd.DateTime.UtcNow.AddDays(-7.0)\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":477},{\"IsGroup\":false,\"Ordinal\":1,\"Join\":\"AND\",\"ItemPath\":\"_1_1\",\"Value\":\"DateTime.UtcNow\",\"Condition\":{\"FieldName\":\"EventEnd\",\"FieldType\":\"System.DateTime\",\"Operator\":\"<=\",\"__msdisposeindex\":480},\"Name\":null,\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":479}],\"TypeProperties\":[],\"_itemPathSeparator\":\"_\"}";
        private const string LastMonthAndCurrentAdditionalFilters = "{\"__msdisposeindex\":501,\"Title\":null,\"Id\":\"00000000-0000-0000-0000-000000000000\",\"QueryItems\":[{\"IsGroup\":true,\"Ordinal\":0,\"Join\":\"OR\",\"ItemPath\":\"_0\",\"Value\":null,\"Condition\":{\"FieldName\":null,\"FieldType\":null,\"Operator\":null,\"__msdisposeindex\":503},\"Name\":\"Current\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":502},{\"IsGroup\":false,\"Ordinal\":0,\"Join\":\"AND\",\"ItemPath\":\"_0_0\",\"Value\":\"DateTime.UtcNow\",\"Condition\":{\"FieldName\":\"EventStart\",\"FieldType\":\"System.DateTime\",\"Operator\":\"<=\",\"__msdisposeindex\":505},\"Name\":null,\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":504},{\"IsGroup\":false,\"Ordinal\":1,\"Join\":\"AND\",\"ItemPath\":\"_0_1\",\"Value\":\"DateTime.UtcNow\",\"Condition\":{\"FieldName\":\"EventEnd\",\"FieldType\":\"System.DateTime\",\"Operator\":\">=\",\"__msdisposeindex\":507},\"Name\":null,\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":506},{\"IsGroup\":true,\"Ordinal\":1,\"Join\":\"OR\",\"ItemPath\":\"_1\",\"Value\":null,\"Condition\":{\"FieldName\":null,\"FieldType\":null,\"Operator\":null,\"__msdisposeindex\":509},\"Name\":\"Past\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":508},{\"IsGroup\":false,\"Ordinal\":0,\"Join\":\"AND\",\"ItemPath\":\"_1_0\",\"Value\":\"DateTime.UtcNow.AddMonths(-1)\",\"Condition\":{\"FieldName\":\"EventEnd\",\"FieldType\":\"System.DateTime\",\"Operator\":\">=\",\"__msdisposeindex\":511},\"Name\":\"EventEnd.DateTime.UtcNow.AddMonths(-1)\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":510},{\"IsGroup\":false,\"Ordinal\":1,\"Join\":\"AND\",\"ItemPath\":\"_1_1\",\"Value\":\"DateTime.UtcNow\",\"Condition\":{\"FieldName\":\"EventEnd\",\"FieldType\":\"System.DateTime\",\"Operator\":\"<=\",\"__msdisposeindex\":513},\"Name\":null,\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":512}],\"TypeProperties\":[],\"_itemPathSeparator\":\"_\"}";
        private const string LastYearAndCurrentAdditionalFilters = "{\"__msdisposeindex\":534,\"Title\":null,\"Id\":\"00000000-0000-0000-0000-000000000000\",\"QueryItems\":[{\"IsGroup\":true,\"Ordinal\":0,\"Join\":\"OR\",\"ItemPath\":\"_0\",\"Value\":null,\"Condition\":{\"FieldName\":null,\"FieldType\":null,\"Operator\":null,\"__msdisposeindex\":536},\"Name\":\"Current\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":535},{\"IsGroup\":false,\"Ordinal\":0,\"Join\":\"AND\",\"ItemPath\":\"_0_0\",\"Value\":\"DateTime.UtcNow\",\"Condition\":{\"FieldName\":\"EventStart\",\"FieldType\":\"System.DateTime\",\"Operator\":\"<=\",\"__msdisposeindex\":538},\"Name\":null,\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":537},{\"IsGroup\":false,\"Ordinal\":1,\"Join\":\"AND\",\"ItemPath\":\"_0_1\",\"Value\":\"DateTime.UtcNow\",\"Condition\":{\"FieldName\":\"EventEnd\",\"FieldType\":\"System.DateTime\",\"Operator\":\">=\",\"__msdisposeindex\":540},\"Name\":null,\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":539},{\"IsGroup\":true,\"Ordinal\":1,\"Join\":\"OR\",\"ItemPath\":\"_1\",\"Value\":null,\"Condition\":{\"FieldName\":null,\"FieldType\":null,\"Operator\":null,\"__msdisposeindex\":542},\"Name\":\"Past\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":541},{\"IsGroup\":false,\"Ordinal\":0,\"Join\":\"AND\",\"ItemPath\":\"_1_0\",\"Value\":\"DateTime.UtcNow.AddYears(-1)\",\"Condition\":{\"FieldName\":\"EventEnd\",\"FieldType\":\"System.DateTime\",\"Operator\":\">=\",\"__msdisposeindex\":544},\"Name\":\"EventEnd.DateTime.UtcNow.AddYears(-1)\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":543},{\"IsGroup\":false,\"Ordinal\":1,\"Join\":\"AND\",\"ItemPath\":\"_1_1\",\"Value\":\"DateTime.UtcNow\",\"Condition\":{\"FieldName\":\"EventEnd\",\"FieldType\":\"System.DateTime\",\"Operator\":\"<=\",\"__msdisposeindex\":546},\"Name\":null,\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":545}],\"TypeProperties\":[],\"_itemPathSeparator\":\"_\"}";

        private const string Next3DaysOrLast3DaysAdditionalFilters = "{\"__msdisposeindex\":285,\"Title\":null,\"Id\":\"00000000-0000-0000-0000-000000000000\",\"QueryItems\":[{\"IsGroup\":true,\"Ordinal\":0,\"Join\":\"OR\",\"ItemPath\":\"_0\",\"Value\":null,\"Condition\":{\"FieldName\":null,\"FieldType\":null,\"Operator\":null,\"__msdisposeindex\":287},\"Name\":\"Upcoming\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":286},{\"IsGroup\":false,\"Ordinal\":0,\"Join\":\"AND\",\"ItemPath\":\"_0_0\",\"Value\":\"DateTime.UtcNow.AddDays(3.0)\",\"Condition\":{\"FieldName\":\"EventStart\",\"FieldType\":\"System.DateTime\",\"Operator\":\"<=\",\"__msdisposeindex\":289},\"Name\":\"EventStart.DateTime.UtcNow.AddDays(3.0)\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":288},{\"IsGroup\":false,\"Ordinal\":1,\"Join\":\"AND\",\"ItemPath\":\"_0_1\",\"Value\":\"DateTime.UtcNow\",\"Condition\":{\"FieldName\":\"EventStart\",\"FieldType\":\"System.DateTime\",\"Operator\":\">=\",\"__msdisposeindex\":291},\"Name\":null,\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":290},{\"IsGroup\":true,\"Ordinal\":1,\"Join\":\"OR\",\"ItemPath\":\"_1\",\"Value\":null,\"Condition\":{\"FieldName\":null,\"FieldType\":null,\"Operator\":null,\"__msdisposeindex\":293},\"Name\":\"Past\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":292},{\"IsGroup\":false,\"Ordinal\":0,\"Join\":\"AND\",\"ItemPath\":\"_1_0\",\"Value\":\"DateTime.UtcNow.AddDays(-3.0)\",\"Condition\":{\"FieldName\":\"EventEnd\",\"FieldType\":\"System.DateTime\",\"Operator\":\">=\",\"__msdisposeindex\":295},\"Name\":\"EventEnd.DateTime.UtcNow.AddDays(-3.0)\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":294},{\"IsGroup\":false,\"Ordinal\":1,\"Join\":\"AND\",\"ItemPath\":\"_1_1\",\"Value\":\"DateTime.UtcNow\",\"Condition\":{\"FieldName\":\"EventEnd\",\"FieldType\":\"System.DateTime\",\"Operator\":\"<=\",\"__msdisposeindex\":297},\"Name\":null,\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":296}],\"TypeProperties\":[],\"_itemPathSeparator\":\"_\"}";
        private const string Next3DaysOrLastWeekAdditionalFilters = "{\"__msdisposeindex\":318,\"Title\":null,\"Id\":\"00000000-0000-0000-0000-000000000000\",\"QueryItems\":[{\"IsGroup\":true,\"Ordinal\":0,\"Join\":\"OR\",\"ItemPath\":\"_0\",\"Value\":null,\"Condition\":{\"FieldName\":null,\"FieldType\":null,\"Operator\":null,\"__msdisposeindex\":320},\"Name\":\"Upcoming\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":319},{\"IsGroup\":false,\"Ordinal\":0,\"Join\":\"AND\",\"ItemPath\":\"_0_0\",\"Value\":\"DateTime.UtcNow.AddDays(3.0)\",\"Condition\":{\"FieldName\":\"EventStart\",\"FieldType\":\"System.DateTime\",\"Operator\":\"<=\",\"__msdisposeindex\":322},\"Name\":\"EventStart.DateTime.UtcNow.AddDays(3.0)\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":321},{\"IsGroup\":false,\"Ordinal\":1,\"Join\":\"AND\",\"ItemPath\":\"_0_1\",\"Value\":\"DateTime.UtcNow\",\"Condition\":{\"FieldName\":\"EventStart\",\"FieldType\":\"System.DateTime\",\"Operator\":\">=\",\"__msdisposeindex\":324},\"Name\":null,\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":323},{\"IsGroup\":true,\"Ordinal\":1,\"Join\":\"OR\",\"ItemPath\":\"_1\",\"Value\":null,\"Condition\":{\"FieldName\":null,\"FieldType\":null,\"Operator\":null,\"__msdisposeindex\":326},\"Name\":\"Past\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":325},{\"IsGroup\":false,\"Ordinal\":0,\"Join\":\"AND\",\"ItemPath\":\"_1_0\",\"Value\":\"DateTime.UtcNow.AddDays(-7.0)\",\"Condition\":{\"FieldName\":\"EventEnd\",\"FieldType\":\"System.DateTime\",\"Operator\":\">=\",\"__msdisposeindex\":328},\"Name\":\"EventEnd.DateTime.UtcNow.AddDays(-7.0)\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":327},{\"IsGroup\":false,\"Ordinal\":1,\"Join\":\"AND\",\"ItemPath\":\"_1_1\",\"Value\":\"DateTime.UtcNow\",\"Condition\":{\"FieldName\":\"EventEnd\",\"FieldType\":\"System.DateTime\",\"Operator\":\"<=\",\"__msdisposeindex\":330},\"Name\":null,\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":329}],\"TypeProperties\":[],\"_itemPathSeparator\":\"_\"}";
        private const string Next3DaysOrLastMonthAdditionalFilters = "{\"__msdisposeindex\":351,\"Title\":null,\"Id\":\"00000000-0000-0000-0000-000000000000\",\"QueryItems\":[{\"IsGroup\":true,\"Ordinal\":0,\"Join\":\"OR\",\"ItemPath\":\"_0\",\"Value\":null,\"Condition\":{\"FieldName\":null,\"FieldType\":null,\"Operator\":null,\"__msdisposeindex\":353},\"Name\":\"Upcoming\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":352},{\"IsGroup\":false,\"Ordinal\":0,\"Join\":\"AND\",\"ItemPath\":\"_0_0\",\"Value\":\"DateTime.UtcNow.AddDays(3.0)\",\"Condition\":{\"FieldName\":\"EventStart\",\"FieldType\":\"System.DateTime\",\"Operator\":\"<=\",\"__msdisposeindex\":355},\"Name\":\"EventStart.DateTime.UtcNow.AddDays(3.0)\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":354},{\"IsGroup\":false,\"Ordinal\":1,\"Join\":\"AND\",\"ItemPath\":\"_0_1\",\"Value\":\"DateTime.UtcNow\",\"Condition\":{\"FieldName\":\"EventStart\",\"FieldType\":\"System.DateTime\",\"Operator\":\">=\",\"__msdisposeindex\":357},\"Name\":null,\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":356},{\"IsGroup\":true,\"Ordinal\":1,\"Join\":\"OR\",\"ItemPath\":\"_1\",\"Value\":null,\"Condition\":{\"FieldName\":null,\"FieldType\":null,\"Operator\":null,\"__msdisposeindex\":359},\"Name\":\"Past\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":358},{\"IsGroup\":false,\"Ordinal\":0,\"Join\":\"AND\",\"ItemPath\":\"_1_0\",\"Value\":\"DateTime.UtcNow.AddMonths(-1)\",\"Condition\":{\"FieldName\":\"EventEnd\",\"FieldType\":\"System.DateTime\",\"Operator\":\">=\",\"__msdisposeindex\":361},\"Name\":\"EventEnd.DateTime.UtcNow.AddMonths(-1)\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":360},{\"IsGroup\":false,\"Ordinal\":1,\"Join\":\"AND\",\"ItemPath\":\"_1_1\",\"Value\":\"DateTime.UtcNow\",\"Condition\":{\"FieldName\":\"EventEnd\",\"FieldType\":\"System.DateTime\",\"Operator\":\"<=\",\"__msdisposeindex\":363},\"Name\":null,\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":362}],\"TypeProperties\":[],\"_itemPathSeparator\":\"_\"}";
        private const string Next3DaysOrLastYearAdditionalFilters = "{\"__msdisposeindex\":384,\"Title\":null,\"Id\":\"00000000-0000-0000-0000-000000000000\",\"QueryItems\":[{\"IsGroup\":true,\"Ordinal\":0,\"Join\":\"OR\",\"ItemPath\":\"_0\",\"Value\":null,\"Condition\":{\"FieldName\":null,\"FieldType\":null,\"Operator\":null,\"__msdisposeindex\":386},\"Name\":\"Upcoming\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":385},{\"IsGroup\":false,\"Ordinal\":0,\"Join\":\"AND\",\"ItemPath\":\"_0_0\",\"Value\":\"DateTime.UtcNow.AddDays(3.0)\",\"Condition\":{\"FieldName\":\"EventStart\",\"FieldType\":\"System.DateTime\",\"Operator\":\"<=\",\"__msdisposeindex\":388},\"Name\":\"EventStart.DateTime.UtcNow.AddDays(3.0)\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":387},{\"IsGroup\":false,\"Ordinal\":1,\"Join\":\"AND\",\"ItemPath\":\"_0_1\",\"Value\":\"DateTime.UtcNow\",\"Condition\":{\"FieldName\":\"EventStart\",\"FieldType\":\"System.DateTime\",\"Operator\":\">=\",\"__msdisposeindex\":390},\"Name\":null,\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":389},{\"IsGroup\":true,\"Ordinal\":1,\"Join\":\"OR\",\"ItemPath\":\"_1\",\"Value\":null,\"Condition\":{\"FieldName\":null,\"FieldType\":null,\"Operator\":null,\"__msdisposeindex\":392},\"Name\":\"Past\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":391},{\"IsGroup\":false,\"Ordinal\":0,\"Join\":\"AND\",\"ItemPath\":\"_1_0\",\"Value\":\"DateTime.UtcNow.AddYears(-1)\",\"Condition\":{\"FieldName\":\"EventEnd\",\"FieldType\":\"System.DateTime\",\"Operator\":\">=\",\"__msdisposeindex\":394},\"Name\":\"EventEnd.DateTime.UtcNow.AddYears(-1)\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":393},{\"IsGroup\":false,\"Ordinal\":1,\"Join\":\"AND\",\"ItemPath\":\"_1_1\",\"Value\":\"DateTime.UtcNow\",\"Condition\":{\"FieldName\":\"EventEnd\",\"FieldType\":\"System.DateTime\",\"Operator\":\"<=\",\"__msdisposeindex\":396},\"Name\":null,\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":395}],\"TypeProperties\":[],\"_itemPathSeparator\":\"_\"}";

        private const string Next3DaysOrLast3DaysOrCurrentAdditionalFilters = "{\"__msdisposeindex\":298,\"Title\":null,\"Id\":\"00000000-0000-0000-0000-000000000000\",\"QueryItems\":[{\"IsGroup\":true,\"Ordinal\":0,\"Join\":\"OR\",\"ItemPath\":\"_0\",\"Value\":null,\"Condition\":{\"FieldName\":null,\"FieldType\":null,\"Operator\":null,\"__msdisposeindex\":300},\"Name\":\"Current\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":299},{\"IsGroup\":false,\"Ordinal\":0,\"Join\":\"AND\",\"ItemPath\":\"_0_0\",\"Value\":\"DateTime.UtcNow\",\"Condition\":{\"FieldName\":\"EventStart\",\"FieldType\":\"System.DateTime\",\"Operator\":\"<=\",\"__msdisposeindex\":302},\"Name\":null,\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":301},{\"IsGroup\":false,\"Ordinal\":1,\"Join\":\"AND\",\"ItemPath\":\"_0_1\",\"Value\":\"DateTime.UtcNow\",\"Condition\":{\"FieldName\":\"EventEnd\",\"FieldType\":\"System.DateTime\",\"Operator\":\">=\",\"__msdisposeindex\":304},\"Name\":null,\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":303},{\"IsGroup\":true,\"Ordinal\":1,\"Join\":\"OR\",\"ItemPath\":\"_1\",\"Value\":null,\"Condition\":{\"FieldName\":null,\"FieldType\":null,\"Operator\":null,\"__msdisposeindex\":306},\"Name\":\"Upcoming\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":305},{\"IsGroup\":false,\"Ordinal\":0,\"Join\":\"AND\",\"ItemPath\":\"_1_0\",\"Value\":\"DateTime.UtcNow.AddDays(3.0)\",\"Condition\":{\"FieldName\":\"EventStart\",\"FieldType\":\"System.DateTime\",\"Operator\":\"<=\",\"__msdisposeindex\":308},\"Name\":\"EventStart.DateTime.UtcNow.AddDays(3.0)\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":307},{\"IsGroup\":false,\"Ordinal\":1,\"Join\":\"AND\",\"ItemPath\":\"_1_1\",\"Value\":\"DateTime.UtcNow\",\"Condition\":{\"FieldName\":\"EventStart\",\"FieldType\":\"System.DateTime\",\"Operator\":\">=\",\"__msdisposeindex\":310},\"Name\":null,\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":309},{\"IsGroup\":true,\"Ordinal\":2,\"Join\":\"OR\",\"ItemPath\":\"_2\",\"Value\":null,\"Condition\":{\"FieldName\":null,\"FieldType\":null,\"Operator\":null,\"__msdisposeindex\":312},\"Name\":\"Past\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":311},{\"IsGroup\":false,\"Ordinal\":0,\"Join\":\"AND\",\"ItemPath\":\"_2_0\",\"Value\":\"DateTime.UtcNow.AddDays(-3.0)\",\"Condition\":{\"FieldName\":\"EventEnd\",\"FieldType\":\"System.DateTime\",\"Operator\":\">=\",\"__msdisposeindex\":314},\"Name\":\"EventEnd.DateTime.UtcNow.AddDays(-3.0)\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":313},{\"IsGroup\":false,\"Ordinal\":1,\"Join\":\"AND\",\"ItemPath\":\"_2_1\",\"Value\":\"DateTime.UtcNow\",\"Condition\":{\"FieldName\":\"EventEnd\",\"FieldType\":\"System.DateTime\",\"Operator\":\"<=\",\"__msdisposeindex\":316},\"Name\":null,\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":315}],\"TypeProperties\":[],\"_itemPathSeparator\":\"_\"}";
        private const string Next3DaysOrLastWeekOrCurrentAdditionalFilters = "{\"__msdisposeindex\":343,\"Title\":null,\"Id\":\"00000000-0000-0000-0000-000000000000\",\"QueryItems\":[{\"IsGroup\":true,\"Ordinal\":0,\"Join\":\"OR\",\"ItemPath\":\"_0\",\"Value\":null,\"Condition\":{\"FieldName\":null,\"FieldType\":null,\"Operator\":null,\"__msdisposeindex\":345},\"Name\":\"Current\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":344},{\"IsGroup\":false,\"Ordinal\":0,\"Join\":\"AND\",\"ItemPath\":\"_0_0\",\"Value\":\"DateTime.UtcNow\",\"Condition\":{\"FieldName\":\"EventStart\",\"FieldType\":\"System.DateTime\",\"Operator\":\"<=\",\"__msdisposeindex\":347},\"Name\":null,\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":346},{\"IsGroup\":false,\"Ordinal\":1,\"Join\":\"AND\",\"ItemPath\":\"_0_1\",\"Value\":\"DateTime.UtcNow\",\"Condition\":{\"FieldName\":\"EventEnd\",\"FieldType\":\"System.DateTime\",\"Operator\":\">=\",\"__msdisposeindex\":349},\"Name\":null,\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":348},{\"IsGroup\":true,\"Ordinal\":1,\"Join\":\"OR\",\"ItemPath\":\"_1\",\"Value\":null,\"Condition\":{\"FieldName\":null,\"FieldType\":null,\"Operator\":null,\"__msdisposeindex\":351},\"Name\":\"Upcoming\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":350},{\"IsGroup\":false,\"Ordinal\":0,\"Join\":\"AND\",\"ItemPath\":\"_1_0\",\"Value\":\"DateTime.UtcNow.AddDays(3.0)\",\"Condition\":{\"FieldName\":\"EventStart\",\"FieldType\":\"System.DateTime\",\"Operator\":\"<=\",\"__msdisposeindex\":353},\"Name\":\"EventStart.DateTime.UtcNow.AddDays(3.0)\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":352},{\"IsGroup\":false,\"Ordinal\":1,\"Join\":\"AND\",\"ItemPath\":\"_1_1\",\"Value\":\"DateTime.UtcNow\",\"Condition\":{\"FieldName\":\"EventStart\",\"FieldType\":\"System.DateTime\",\"Operator\":\">=\",\"__msdisposeindex\":355},\"Name\":null,\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":354},{\"IsGroup\":true,\"Ordinal\":2,\"Join\":\"OR\",\"ItemPath\":\"_2\",\"Value\":null,\"Condition\":{\"FieldName\":null,\"FieldType\":null,\"Operator\":null,\"__msdisposeindex\":357},\"Name\":\"Past\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":356},{\"IsGroup\":false,\"Ordinal\":0,\"Join\":\"AND\",\"ItemPath\":\"_2_0\",\"Value\":\"DateTime.UtcNow.AddDays(-7.0)\",\"Condition\":{\"FieldName\":\"EventEnd\",\"FieldType\":\"System.DateTime\",\"Operator\":\">=\",\"__msdisposeindex\":359},\"Name\":\"EventEnd.DateTime.UtcNow.AddDays(-7.0)\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":358},{\"IsGroup\":false,\"Ordinal\":1,\"Join\":\"AND\",\"ItemPath\":\"_2_1\",\"Value\":\"DateTime.UtcNow\",\"Condition\":{\"FieldName\":\"EventEnd\",\"FieldType\":\"System.DateTime\",\"Operator\":\"<=\",\"__msdisposeindex\":361},\"Name\":null,\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":360}],\"TypeProperties\":[],\"_itemPathSeparator\":\"_\"}";
        private const string Next3DaysOrLastMonthOrCurrentAdditionalFilters = "{\"__msdisposeindex\":296,\"Title\":null,\"Id\":\"00000000-0000-0000-0000-000000000000\",\"QueryItems\":[{\"IsGroup\":true,\"Ordinal\":0,\"Join\":\"OR\",\"ItemPath\":\"_0\",\"Value\":null,\"Condition\":{\"FieldName\":null,\"FieldType\":null,\"Operator\":null,\"__msdisposeindex\":298},\"Name\":\"Current\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":297},{\"IsGroup\":false,\"Ordinal\":0,\"Join\":\"AND\",\"ItemPath\":\"_0_0\",\"Value\":\"DateTime.UtcNow\",\"Condition\":{\"FieldName\":\"EventStart\",\"FieldType\":\"System.DateTime\",\"Operator\":\"<=\",\"__msdisposeindex\":300},\"Name\":null,\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":299},{\"IsGroup\":false,\"Ordinal\":1,\"Join\":\"AND\",\"ItemPath\":\"_0_1\",\"Value\":\"DateTime.UtcNow\",\"Condition\":{\"FieldName\":\"EventEnd\",\"FieldType\":\"System.DateTime\",\"Operator\":\">=\",\"__msdisposeindex\":302},\"Name\":null,\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":301},{\"IsGroup\":true,\"Ordinal\":1,\"Join\":\"OR\",\"ItemPath\":\"_1\",\"Value\":null,\"Condition\":{\"FieldName\":null,\"FieldType\":null,\"Operator\":null,\"__msdisposeindex\":304},\"Name\":\"Upcoming\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":303},{\"IsGroup\":false,\"Ordinal\":0,\"Join\":\"AND\",\"ItemPath\":\"_1_0\",\"Value\":\"DateTime.UtcNow.AddDays(3.0)\",\"Condition\":{\"FieldName\":\"EventStart\",\"FieldType\":\"System.DateTime\",\"Operator\":\"<=\",\"__msdisposeindex\":306},\"Name\":\"EventStart.DateTime.UtcNow.AddDays(3.0)\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":305},{\"IsGroup\":false,\"Ordinal\":1,\"Join\":\"AND\",\"ItemPath\":\"_1_1\",\"Value\":\"DateTime.UtcNow\",\"Condition\":{\"FieldName\":\"EventStart\",\"FieldType\":\"System.DateTime\",\"Operator\":\">=\",\"__msdisposeindex\":308},\"Name\":null,\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":307},{\"IsGroup\":true,\"Ordinal\":2,\"Join\":\"OR\",\"ItemPath\":\"_2\",\"Value\":null,\"Condition\":{\"FieldName\":null,\"FieldType\":null,\"Operator\":null,\"__msdisposeindex\":310},\"Name\":\"Past\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":309},{\"IsGroup\":false,\"Ordinal\":0,\"Join\":\"AND\",\"ItemPath\":\"_2_0\",\"Value\":\"DateTime.UtcNow.AddMonths(-1)\",\"Condition\":{\"FieldName\":\"EventEnd\",\"FieldType\":\"System.DateTime\",\"Operator\":\">=\",\"__msdisposeindex\":312},\"Name\":\"EventEnd.DateTime.UtcNow.AddMonths(-1)\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":311},{\"IsGroup\":false,\"Ordinal\":1,\"Join\":\"AND\",\"ItemPath\":\"_2_1\",\"Value\":\"DateTime.UtcNow\",\"Condition\":{\"FieldName\":\"EventEnd\",\"FieldType\":\"System.DateTime\",\"Operator\":\"<=\",\"__msdisposeindex\":314},\"Name\":null,\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":313}],\"TypeProperties\":[],\"_itemPathSeparator\":\"_\"}";
        private const string Next3DaysOrLastYearOrCurrentAdditionalFilters = "{\"__msdisposeindex\":341,\"Title\":null,\"Id\":\"00000000-0000-0000-0000-000000000000\",\"QueryItems\":[{\"IsGroup\":true,\"Ordinal\":0,\"Join\":\"OR\",\"ItemPath\":\"_0\",\"Value\":null,\"Condition\":{\"FieldName\":null,\"FieldType\":null,\"Operator\":null,\"__msdisposeindex\":343},\"Name\":\"Current\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":342},{\"IsGroup\":false,\"Ordinal\":0,\"Join\":\"AND\",\"ItemPath\":\"_0_0\",\"Value\":\"DateTime.UtcNow\",\"Condition\":{\"FieldName\":\"EventStart\",\"FieldType\":\"System.DateTime\",\"Operator\":\"<=\",\"__msdisposeindex\":345},\"Name\":null,\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":344},{\"IsGroup\":false,\"Ordinal\":1,\"Join\":\"AND\",\"ItemPath\":\"_0_1\",\"Value\":\"DateTime.UtcNow\",\"Condition\":{\"FieldName\":\"EventEnd\",\"FieldType\":\"System.DateTime\",\"Operator\":\">=\",\"__msdisposeindex\":347},\"Name\":null,\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":346},{\"IsGroup\":true,\"Ordinal\":1,\"Join\":\"OR\",\"ItemPath\":\"_1\",\"Value\":null,\"Condition\":{\"FieldName\":null,\"FieldType\":null,\"Operator\":null,\"__msdisposeindex\":349},\"Name\":\"Upcoming\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":348},{\"IsGroup\":false,\"Ordinal\":0,\"Join\":\"AND\",\"ItemPath\":\"_1_0\",\"Value\":\"DateTime.UtcNow.AddDays(3.0)\",\"Condition\":{\"FieldName\":\"EventStart\",\"FieldType\":\"System.DateTime\",\"Operator\":\"<=\",\"__msdisposeindex\":351},\"Name\":\"EventStart.DateTime.UtcNow.AddDays(3.0)\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":350},{\"IsGroup\":false,\"Ordinal\":1,\"Join\":\"AND\",\"ItemPath\":\"_1_1\",\"Value\":\"DateTime.UtcNow\",\"Condition\":{\"FieldName\":\"EventStart\",\"FieldType\":\"System.DateTime\",\"Operator\":\">=\",\"__msdisposeindex\":353},\"Name\":null,\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":352},{\"IsGroup\":true,\"Ordinal\":2,\"Join\":\"OR\",\"ItemPath\":\"_2\",\"Value\":null,\"Condition\":{\"FieldName\":null,\"FieldType\":null,\"Operator\":null,\"__msdisposeindex\":355},\"Name\":\"Past\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":354},{\"IsGroup\":false,\"Ordinal\":0,\"Join\":\"AND\",\"ItemPath\":\"_2_0\",\"Value\":\"DateTime.UtcNow.AddYears(-1)\",\"Condition\":{\"FieldName\":\"EventEnd\",\"FieldType\":\"System.DateTime\",\"Operator\":\">=\",\"__msdisposeindex\":357},\"Name\":\"EventEnd.DateTime.UtcNow.AddYears(-1)\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":356},{\"IsGroup\":false,\"Ordinal\":1,\"Join\":\"AND\",\"ItemPath\":\"_2_1\",\"Value\":\"DateTime.UtcNow\",\"Condition\":{\"FieldName\":\"EventEnd\",\"FieldType\":\"System.DateTime\",\"Operator\":\"<=\",\"__msdisposeindex\":359},\"Name\":null,\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":358}],\"TypeProperties\":[],\"_itemPathSeparator\":\"_\"}";

        private const string NextWeekOrLast3DaysAdditionalFilters = "{\"__msdisposeindex\":422,\"Title\":null,\"Id\":\"00000000-0000-0000-0000-000000000000\",\"QueryItems\":[{\"IsGroup\":true,\"Ordinal\":0,\"Join\":\"OR\",\"ItemPath\":\"_0\",\"Value\":null,\"Condition\":{\"FieldName\":null,\"FieldType\":null,\"Operator\":null,\"__msdisposeindex\":424},\"Name\":\"Upcoming\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":423},{\"IsGroup\":false,\"Ordinal\":0,\"Join\":\"AND\",\"ItemPath\":\"_0_0\",\"Value\":\"DateTime.UtcNow.AddDays(7.0)\",\"Condition\":{\"FieldName\":\"EventStart\",\"FieldType\":\"System.DateTime\",\"Operator\":\"<=\",\"__msdisposeindex\":426},\"Name\":\"EventStart.DateTime.UtcNow.AddDays(7.0)\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":425},{\"IsGroup\":false,\"Ordinal\":1,\"Join\":\"AND\",\"ItemPath\":\"_0_1\",\"Value\":\"DateTime.UtcNow\",\"Condition\":{\"FieldName\":\"EventStart\",\"FieldType\":\"System.DateTime\",\"Operator\":\">=\",\"__msdisposeindex\":428},\"Name\":null,\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":427},{\"IsGroup\":true,\"Ordinal\":1,\"Join\":\"OR\",\"ItemPath\":\"_1\",\"Value\":null,\"Condition\":{\"FieldName\":null,\"FieldType\":null,\"Operator\":null,\"__msdisposeindex\":430},\"Name\":\"Past\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":429},{\"IsGroup\":false,\"Ordinal\":0,\"Join\":\"AND\",\"ItemPath\":\"_1_0\",\"Value\":\"DateTime.UtcNow.AddDays(-3.0)\",\"Condition\":{\"FieldName\":\"EventEnd\",\"FieldType\":\"System.DateTime\",\"Operator\":\">=\",\"__msdisposeindex\":432},\"Name\":\"EventEnd.DateTime.UtcNow.AddDays(-3.0)\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":431},{\"IsGroup\":false,\"Ordinal\":1,\"Join\":\"AND\",\"ItemPath\":\"_1_1\",\"Value\":\"DateTime.UtcNow\",\"Condition\":{\"FieldName\":\"EventEnd\",\"FieldType\":\"System.DateTime\",\"Operator\":\"<=\",\"__msdisposeindex\":434},\"Name\":null,\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":433}],\"TypeProperties\":[],\"_itemPathSeparator\":\"_\"}";
        private const string NextWeekOrLastWeekAdditionalFilters = "{\"__msdisposeindex\":455,\"Title\":null,\"Id\":\"00000000-0000-0000-0000-000000000000\",\"QueryItems\":[{\"IsGroup\":true,\"Ordinal\":0,\"Join\":\"OR\",\"ItemPath\":\"_0\",\"Value\":null,\"Condition\":{\"FieldName\":null,\"FieldType\":null,\"Operator\":null,\"__msdisposeindex\":457},\"Name\":\"Upcoming\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":456},{\"IsGroup\":false,\"Ordinal\":0,\"Join\":\"AND\",\"ItemPath\":\"_0_0\",\"Value\":\"DateTime.UtcNow.AddDays(7.0)\",\"Condition\":{\"FieldName\":\"EventStart\",\"FieldType\":\"System.DateTime\",\"Operator\":\"<=\",\"__msdisposeindex\":459},\"Name\":\"EventStart.DateTime.UtcNow.AddDays(7.0)\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":458},{\"IsGroup\":false,\"Ordinal\":1,\"Join\":\"AND\",\"ItemPath\":\"_0_1\",\"Value\":\"DateTime.UtcNow\",\"Condition\":{\"FieldName\":\"EventStart\",\"FieldType\":\"System.DateTime\",\"Operator\":\">=\",\"__msdisposeindex\":461},\"Name\":null,\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":460},{\"IsGroup\":true,\"Ordinal\":1,\"Join\":\"OR\",\"ItemPath\":\"_1\",\"Value\":null,\"Condition\":{\"FieldName\":null,\"FieldType\":null,\"Operator\":null,\"__msdisposeindex\":463},\"Name\":\"Past\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":462},{\"IsGroup\":false,\"Ordinal\":0,\"Join\":\"AND\",\"ItemPath\":\"_1_0\",\"Value\":\"DateTime.UtcNow.AddDays(-7.0)\",\"Condition\":{\"FieldName\":\"EventEnd\",\"FieldType\":\"System.DateTime\",\"Operator\":\">=\",\"__msdisposeindex\":465},\"Name\":\"EventEnd.DateTime.UtcNow.AddDays(-7.0)\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":464},{\"IsGroup\":false,\"Ordinal\":1,\"Join\":\"AND\",\"ItemPath\":\"_1_1\",\"Value\":\"DateTime.UtcNow\",\"Condition\":{\"FieldName\":\"EventEnd\",\"FieldType\":\"System.DateTime\",\"Operator\":\"<=\",\"__msdisposeindex\":467},\"Name\":null,\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":466}],\"TypeProperties\":[],\"_itemPathSeparator\":\"_\"}";
        private const string NextWeekOrLastMonthAdditionalFilters = "{\"__msdisposeindex\":502,\"Title\":null,\"Id\":\"00000000-0000-0000-0000-000000000000\",\"QueryItems\":[{\"IsGroup\":true,\"Ordinal\":0,\"Join\":\"OR\",\"ItemPath\":\"_0\",\"Value\":null,\"Condition\":{\"FieldName\":null,\"FieldType\":null,\"Operator\":null,\"__msdisposeindex\":504},\"Name\":\"Upcoming\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":503},{\"IsGroup\":false,\"Ordinal\":0,\"Join\":\"AND\",\"ItemPath\":\"_0_0\",\"Value\":\"DateTime.UtcNow.AddDays(7.0)\",\"Condition\":{\"FieldName\":\"EventStart\",\"FieldType\":\"System.DateTime\",\"Operator\":\"<=\",\"__msdisposeindex\":506},\"Name\":\"EventStart.DateTime.UtcNow.AddDays(7.0)\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":505},{\"IsGroup\":false,\"Ordinal\":1,\"Join\":\"AND\",\"ItemPath\":\"_0_1\",\"Value\":\"DateTime.UtcNow\",\"Condition\":{\"FieldName\":\"EventStart\",\"FieldType\":\"System.DateTime\",\"Operator\":\">=\",\"__msdisposeindex\":508},\"Name\":null,\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":507},{\"IsGroup\":true,\"Ordinal\":1,\"Join\":\"OR\",\"ItemPath\":\"_1\",\"Value\":null,\"Condition\":{\"FieldName\":null,\"FieldType\":null,\"Operator\":null,\"__msdisposeindex\":510},\"Name\":\"Past\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":509},{\"IsGroup\":false,\"Ordinal\":0,\"Join\":\"AND\",\"ItemPath\":\"_1_0\",\"Value\":\"DateTime.UtcNow.AddMonths(-1)\",\"Condition\":{\"FieldName\":\"EventEnd\",\"FieldType\":\"System.DateTime\",\"Operator\":\">=\",\"__msdisposeindex\":512},\"Name\":\"EventEnd.DateTime.UtcNow.AddMonths(-1)\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":511},{\"IsGroup\":false,\"Ordinal\":1,\"Join\":\"AND\",\"ItemPath\":\"_1_1\",\"Value\":\"DateTime.UtcNow\",\"Condition\":{\"FieldName\":\"EventEnd\",\"FieldType\":\"System.DateTime\",\"Operator\":\"<=\",\"__msdisposeindex\":514},\"Name\":null,\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":513}],\"TypeProperties\":[],\"_itemPathSeparator\":\"_\"}";
        private const string NextWeekOrLastYearAdditionalFilters = "{\"__msdisposeindex\":535,\"Title\":null,\"Id\":\"00000000-0000-0000-0000-000000000000\",\"QueryItems\":[{\"IsGroup\":true,\"Ordinal\":0,\"Join\":\"OR\",\"ItemPath\":\"_0\",\"Value\":null,\"Condition\":{\"FieldName\":null,\"FieldType\":null,\"Operator\":null,\"__msdisposeindex\":537},\"Name\":\"Upcoming\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":536},{\"IsGroup\":false,\"Ordinal\":0,\"Join\":\"AND\",\"ItemPath\":\"_0_0\",\"Value\":\"DateTime.UtcNow.AddDays(7.0)\",\"Condition\":{\"FieldName\":\"EventStart\",\"FieldType\":\"System.DateTime\",\"Operator\":\"<=\",\"__msdisposeindex\":539},\"Name\":\"EventStart.DateTime.UtcNow.AddDays(7.0)\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":538},{\"IsGroup\":false,\"Ordinal\":1,\"Join\":\"AND\",\"ItemPath\":\"_0_1\",\"Value\":\"DateTime.UtcNow\",\"Condition\":{\"FieldName\":\"EventStart\",\"FieldType\":\"System.DateTime\",\"Operator\":\">=\",\"__msdisposeindex\":541},\"Name\":null,\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":540},{\"IsGroup\":true,\"Ordinal\":1,\"Join\":\"OR\",\"ItemPath\":\"_1\",\"Value\":null,\"Condition\":{\"FieldName\":null,\"FieldType\":null,\"Operator\":null,\"__msdisposeindex\":543},\"Name\":\"Past\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":542},{\"IsGroup\":false,\"Ordinal\":0,\"Join\":\"AND\",\"ItemPath\":\"_1_0\",\"Value\":\"DateTime.UtcNow.AddYears(-1)\",\"Condition\":{\"FieldName\":\"EventEnd\",\"FieldType\":\"System.DateTime\",\"Operator\":\">=\",\"__msdisposeindex\":545},\"Name\":\"EventEnd.DateTime.UtcNow.AddYears(-1)\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":544},{\"IsGroup\":false,\"Ordinal\":1,\"Join\":\"AND\",\"ItemPath\":\"_1_1\",\"Value\":\"DateTime.UtcNow\",\"Condition\":{\"FieldName\":\"EventEnd\",\"FieldType\":\"System.DateTime\",\"Operator\":\"<=\",\"__msdisposeindex\":547},\"Name\":null,\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":546}],\"TypeProperties\":[],\"_itemPathSeparator\":\"_\"}";

        private const string NextWeekOrLast3DaysOrCurrentAdditionalFilters = "{\"__msdisposeindex\":301,\"Title\":null,\"Id\":\"00000000-0000-0000-0000-000000000000\",\"QueryItems\":[{\"IsGroup\":true,\"Ordinal\":0,\"Join\":\"OR\",\"ItemPath\":\"_0\",\"Value\":null,\"Condition\":{\"FieldName\":null,\"FieldType\":null,\"Operator\":null,\"__msdisposeindex\":303},\"Name\":\"Current\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":302},{\"IsGroup\":false,\"Ordinal\":0,\"Join\":\"AND\",\"ItemPath\":\"_0_0\",\"Value\":\"DateTime.UtcNow\",\"Condition\":{\"FieldName\":\"EventStart\",\"FieldType\":\"System.DateTime\",\"Operator\":\"<=\",\"__msdisposeindex\":305},\"Name\":null,\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":304},{\"IsGroup\":false,\"Ordinal\":1,\"Join\":\"AND\",\"ItemPath\":\"_0_1\",\"Value\":\"DateTime.UtcNow\",\"Condition\":{\"FieldName\":\"EventEnd\",\"FieldType\":\"System.DateTime\",\"Operator\":\">=\",\"__msdisposeindex\":307},\"Name\":null,\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":306},{\"IsGroup\":true,\"Ordinal\":1,\"Join\":\"OR\",\"ItemPath\":\"_1\",\"Value\":null,\"Condition\":{\"FieldName\":null,\"FieldType\":null,\"Operator\":null,\"__msdisposeindex\":309},\"Name\":\"Upcoming\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":308},{\"IsGroup\":false,\"Ordinal\":0,\"Join\":\"AND\",\"ItemPath\":\"_1_0\",\"Value\":\"DateTime.UtcNow.AddDays(7.0)\",\"Condition\":{\"FieldName\":\"EventStart\",\"FieldType\":\"System.DateTime\",\"Operator\":\"<=\",\"__msdisposeindex\":311},\"Name\":\"EventStart.DateTime.UtcNow.AddDays(7.0)\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":310},{\"IsGroup\":false,\"Ordinal\":1,\"Join\":\"AND\",\"ItemPath\":\"_1_1\",\"Value\":\"DateTime.UtcNow\",\"Condition\":{\"FieldName\":\"EventStart\",\"FieldType\":\"System.DateTime\",\"Operator\":\">=\",\"__msdisposeindex\":313},\"Name\":null,\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":312},{\"IsGroup\":true,\"Ordinal\":2,\"Join\":\"OR\",\"ItemPath\":\"_2\",\"Value\":null,\"Condition\":{\"FieldName\":null,\"FieldType\":null,\"Operator\":null,\"__msdisposeindex\":315},\"Name\":\"Past\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":314},{\"IsGroup\":false,\"Ordinal\":0,\"Join\":\"AND\",\"ItemPath\":\"_2_0\",\"Value\":\"DateTime.UtcNow.AddDays(-3.0)\",\"Condition\":{\"FieldName\":\"EventEnd\",\"FieldType\":\"System.DateTime\",\"Operator\":\">=\",\"__msdisposeindex\":317},\"Name\":\"EventEnd.DateTime.UtcNow.AddDays(-3.0)\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":316},{\"IsGroup\":false,\"Ordinal\":1,\"Join\":\"AND\",\"ItemPath\":\"_2_1\",\"Value\":\"DateTime.UtcNow\",\"Condition\":{\"FieldName\":\"EventEnd\",\"FieldType\":\"System.DateTime\",\"Operator\":\"<=\",\"__msdisposeindex\":319},\"Name\":null,\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":318}],\"TypeProperties\":[],\"_itemPathSeparator\":\"_\"}";
        private const string NextWeekOrLastWeekOrCurrentAdditionalFilters = "{\"__msdisposeindex\":346,\"Title\":null,\"Id\":\"00000000-0000-0000-0000-000000000000\",\"QueryItems\":[{\"IsGroup\":true,\"Ordinal\":0,\"Join\":\"OR\",\"ItemPath\":\"_0\",\"Value\":null,\"Condition\":{\"FieldName\":null,\"FieldType\":null,\"Operator\":null,\"__msdisposeindex\":348},\"Name\":\"Current\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":347},{\"IsGroup\":false,\"Ordinal\":0,\"Join\":\"AND\",\"ItemPath\":\"_0_0\",\"Value\":\"DateTime.UtcNow\",\"Condition\":{\"FieldName\":\"EventStart\",\"FieldType\":\"System.DateTime\",\"Operator\":\"<=\",\"__msdisposeindex\":350},\"Name\":null,\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":349},{\"IsGroup\":false,\"Ordinal\":1,\"Join\":\"AND\",\"ItemPath\":\"_0_1\",\"Value\":\"DateTime.UtcNow\",\"Condition\":{\"FieldName\":\"EventEnd\",\"FieldType\":\"System.DateTime\",\"Operator\":\">=\",\"__msdisposeindex\":352},\"Name\":null,\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":351},{\"IsGroup\":true,\"Ordinal\":1,\"Join\":\"OR\",\"ItemPath\":\"_1\",\"Value\":null,\"Condition\":{\"FieldName\":null,\"FieldType\":null,\"Operator\":null,\"__msdisposeindex\":354},\"Name\":\"Upcoming\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":353},{\"IsGroup\":false,\"Ordinal\":0,\"Join\":\"AND\",\"ItemPath\":\"_1_0\",\"Value\":\"DateTime.UtcNow.AddDays(7.0)\",\"Condition\":{\"FieldName\":\"EventStart\",\"FieldType\":\"System.DateTime\",\"Operator\":\"<=\",\"__msdisposeindex\":356},\"Name\":\"EventStart.DateTime.UtcNow.AddDays(7.0)\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":355},{\"IsGroup\":false,\"Ordinal\":1,\"Join\":\"AND\",\"ItemPath\":\"_1_1\",\"Value\":\"DateTime.UtcNow\",\"Condition\":{\"FieldName\":\"EventStart\",\"FieldType\":\"System.DateTime\",\"Operator\":\">=\",\"__msdisposeindex\":358},\"Name\":null,\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":357},{\"IsGroup\":true,\"Ordinal\":2,\"Join\":\"OR\",\"ItemPath\":\"_2\",\"Value\":null,\"Condition\":{\"FieldName\":null,\"FieldType\":null,\"Operator\":null,\"__msdisposeindex\":360},\"Name\":\"Past\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":359},{\"IsGroup\":false,\"Ordinal\":0,\"Join\":\"AND\",\"ItemPath\":\"_2_0\",\"Value\":\"DateTime.UtcNow.AddDays(-7.0)\",\"Condition\":{\"FieldName\":\"EventEnd\",\"FieldType\":\"System.DateTime\",\"Operator\":\">=\",\"__msdisposeindex\":362},\"Name\":\"EventEnd.DateTime.UtcNow.AddDays(-7.0)\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":361},{\"IsGroup\":false,\"Ordinal\":1,\"Join\":\"AND\",\"ItemPath\":\"_2_1\",\"Value\":\"DateTime.UtcNow\",\"Condition\":{\"FieldName\":\"EventEnd\",\"FieldType\":\"System.DateTime\",\"Operator\":\"<=\",\"__msdisposeindex\":364},\"Name\":null,\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":363}],\"TypeProperties\":[],\"_itemPathSeparator\":\"_\"}";
        private const string NextWeekOrLastMonthOrCurrentAdditionalFilters = "{\"__msdisposeindex\":391,\"Title\":null,\"Id\":\"00000000-0000-0000-0000-000000000000\",\"QueryItems\":[{\"IsGroup\":true,\"Ordinal\":0,\"Join\":\"OR\",\"ItemPath\":\"_0\",\"Value\":null,\"Condition\":{\"FieldName\":null,\"FieldType\":null,\"Operator\":null,\"__msdisposeindex\":393},\"Name\":\"Current\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":392},{\"IsGroup\":false,\"Ordinal\":0,\"Join\":\"AND\",\"ItemPath\":\"_0_0\",\"Value\":\"DateTime.UtcNow\",\"Condition\":{\"FieldName\":\"EventStart\",\"FieldType\":\"System.DateTime\",\"Operator\":\"<=\",\"__msdisposeindex\":395},\"Name\":null,\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":394},{\"IsGroup\":false,\"Ordinal\":1,\"Join\":\"AND\",\"ItemPath\":\"_0_1\",\"Value\":\"DateTime.UtcNow\",\"Condition\":{\"FieldName\":\"EventEnd\",\"FieldType\":\"System.DateTime\",\"Operator\":\">=\",\"__msdisposeindex\":397},\"Name\":null,\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":396},{\"IsGroup\":true,\"Ordinal\":1,\"Join\":\"OR\",\"ItemPath\":\"_1\",\"Value\":null,\"Condition\":{\"FieldName\":null,\"FieldType\":null,\"Operator\":null,\"__msdisposeindex\":399},\"Name\":\"Upcoming\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":398},{\"IsGroup\":false,\"Ordinal\":0,\"Join\":\"AND\",\"ItemPath\":\"_1_0\",\"Value\":\"DateTime.UtcNow.AddDays(7.0)\",\"Condition\":{\"FieldName\":\"EventStart\",\"FieldType\":\"System.DateTime\",\"Operator\":\"<=\",\"__msdisposeindex\":401},\"Name\":\"EventStart.DateTime.UtcNow.AddDays(7.0)\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":400},{\"IsGroup\":false,\"Ordinal\":1,\"Join\":\"AND\",\"ItemPath\":\"_1_1\",\"Value\":\"DateTime.UtcNow\",\"Condition\":{\"FieldName\":\"EventStart\",\"FieldType\":\"System.DateTime\",\"Operator\":\">=\",\"__msdisposeindex\":403},\"Name\":null,\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":402},{\"IsGroup\":true,\"Ordinal\":2,\"Join\":\"OR\",\"ItemPath\":\"_2\",\"Value\":null,\"Condition\":{\"FieldName\":null,\"FieldType\":null,\"Operator\":null,\"__msdisposeindex\":405},\"Name\":\"Past\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":404},{\"IsGroup\":false,\"Ordinal\":0,\"Join\":\"AND\",\"ItemPath\":\"_2_0\",\"Value\":\"DateTime.UtcNow.AddMonths(-1)\",\"Condition\":{\"FieldName\":\"EventEnd\",\"FieldType\":\"System.DateTime\",\"Operator\":\">=\",\"__msdisposeindex\":407},\"Name\":\"EventEnd.DateTime.UtcNow.AddMonths(-1)\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":406},{\"IsGroup\":false,\"Ordinal\":1,\"Join\":\"AND\",\"ItemPath\":\"_2_1\",\"Value\":\"DateTime.UtcNow\",\"Condition\":{\"FieldName\":\"EventEnd\",\"FieldType\":\"System.DateTime\",\"Operator\":\"<=\",\"__msdisposeindex\":409},\"Name\":null,\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":408}],\"TypeProperties\":[],\"_itemPathSeparator\":\"_\"}";
        private const string NextWeekOrLastYearOrCurrentAdditionalFilters = "{\"__msdisposeindex\":436,\"Title\":null,\"Id\":\"00000000-0000-0000-0000-000000000000\",\"QueryItems\":[{\"IsGroup\":true,\"Ordinal\":0,\"Join\":\"OR\",\"ItemPath\":\"_0\",\"Value\":null,\"Condition\":{\"FieldName\":null,\"FieldType\":null,\"Operator\":null,\"__msdisposeindex\":438},\"Name\":\"Current\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":437},{\"IsGroup\":false,\"Ordinal\":0,\"Join\":\"AND\",\"ItemPath\":\"_0_0\",\"Value\":\"DateTime.UtcNow\",\"Condition\":{\"FieldName\":\"EventStart\",\"FieldType\":\"System.DateTime\",\"Operator\":\"<=\",\"__msdisposeindex\":440},\"Name\":null,\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":439},{\"IsGroup\":false,\"Ordinal\":1,\"Join\":\"AND\",\"ItemPath\":\"_0_1\",\"Value\":\"DateTime.UtcNow\",\"Condition\":{\"FieldName\":\"EventEnd\",\"FieldType\":\"System.DateTime\",\"Operator\":\">=\",\"__msdisposeindex\":442},\"Name\":null,\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":441},{\"IsGroup\":true,\"Ordinal\":1,\"Join\":\"OR\",\"ItemPath\":\"_1\",\"Value\":null,\"Condition\":{\"FieldName\":null,\"FieldType\":null,\"Operator\":null,\"__msdisposeindex\":444},\"Name\":\"Upcoming\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":443},{\"IsGroup\":false,\"Ordinal\":0,\"Join\":\"AND\",\"ItemPath\":\"_1_0\",\"Value\":\"DateTime.UtcNow.AddDays(7.0)\",\"Condition\":{\"FieldName\":\"EventStart\",\"FieldType\":\"System.DateTime\",\"Operator\":\"<=\",\"__msdisposeindex\":446},\"Name\":\"EventStart.DateTime.UtcNow.AddDays(7.0)\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":445},{\"IsGroup\":false,\"Ordinal\":1,\"Join\":\"AND\",\"ItemPath\":\"_1_1\",\"Value\":\"DateTime.UtcNow\",\"Condition\":{\"FieldName\":\"EventStart\",\"FieldType\":\"System.DateTime\",\"Operator\":\">=\",\"__msdisposeindex\":448},\"Name\":null,\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":447},{\"IsGroup\":true,\"Ordinal\":2,\"Join\":\"OR\",\"ItemPath\":\"_2\",\"Value\":null,\"Condition\":{\"FieldName\":null,\"FieldType\":null,\"Operator\":null,\"__msdisposeindex\":450},\"Name\":\"Past\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":449},{\"IsGroup\":false,\"Ordinal\":0,\"Join\":\"AND\",\"ItemPath\":\"_2_0\",\"Value\":\"DateTime.UtcNow.AddYears(-1)\",\"Condition\":{\"FieldName\":\"EventEnd\",\"FieldType\":\"System.DateTime\",\"Operator\":\">=\",\"__msdisposeindex\":452},\"Name\":\"EventEnd.DateTime.UtcNow.AddYears(-1)\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":451},{\"IsGroup\":false,\"Ordinal\":1,\"Join\":\"AND\",\"ItemPath\":\"_2_1\",\"Value\":\"DateTime.UtcNow\",\"Condition\":{\"FieldName\":\"EventEnd\",\"FieldType\":\"System.DateTime\",\"Operator\":\"<=\",\"__msdisposeindex\":454},\"Name\":null,\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":453}],\"TypeProperties\":[],\"_itemPathSeparator\":\"_\"}";

        private const string NextMonthOrLast3DaysAdditionalFilters = "{\"__msdisposeindex\":573,\"Title\":null,\"Id\":\"00000000-0000-0000-0000-000000000000\",\"QueryItems\":[{\"IsGroup\":true,\"Ordinal\":0,\"Join\":\"OR\",\"ItemPath\":\"_0\",\"Value\":null,\"Condition\":{\"FieldName\":null,\"FieldType\":null,\"Operator\":null,\"__msdisposeindex\":575},\"Name\":\"Upcoming\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":574},{\"IsGroup\":false,\"Ordinal\":0,\"Join\":\"AND\",\"ItemPath\":\"_0_0\",\"Value\":\"DateTime.UtcNow.AddMonths(1)\",\"Condition\":{\"FieldName\":\"EventStart\",\"FieldType\":\"System.DateTime\",\"Operator\":\"<=\",\"__msdisposeindex\":577},\"Name\":\"EventStart.DateTime.UtcNow.AddMonths(1)\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":576},{\"IsGroup\":false,\"Ordinal\":1,\"Join\":\"AND\",\"ItemPath\":\"_0_1\",\"Value\":\"DateTime.UtcNow\",\"Condition\":{\"FieldName\":\"EventStart\",\"FieldType\":\"System.DateTime\",\"Operator\":\">=\",\"__msdisposeindex\":579},\"Name\":null,\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":578},{\"IsGroup\":true,\"Ordinal\":1,\"Join\":\"OR\",\"ItemPath\":\"_1\",\"Value\":null,\"Condition\":{\"FieldName\":null,\"FieldType\":null,\"Operator\":null,\"__msdisposeindex\":581},\"Name\":\"Past\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":580},{\"IsGroup\":false,\"Ordinal\":0,\"Join\":\"AND\",\"ItemPath\":\"_1_0\",\"Value\":\"DateTime.UtcNow.AddDays(-3.0)\",\"Condition\":{\"FieldName\":\"EventEnd\",\"FieldType\":\"System.DateTime\",\"Operator\":\">=\",\"__msdisposeindex\":583},\"Name\":\"EventEnd.DateTime.UtcNow.AddDays(-3.0)\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":582},{\"IsGroup\":false,\"Ordinal\":1,\"Join\":\"AND\",\"ItemPath\":\"_1_1\",\"Value\":\"DateTime.UtcNow\",\"Condition\":{\"FieldName\":\"EventEnd\",\"FieldType\":\"System.DateTime\",\"Operator\":\"<=\",\"__msdisposeindex\":585},\"Name\":null,\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":584}],\"TypeProperties\":[],\"_itemPathSeparator\":\"_\"}";
        private const string NextMonthOrLastWeekAdditionalFilters = "{\"__msdisposeindex\":620,\"Title\":null,\"Id\":\"00000000-0000-0000-0000-000000000000\",\"QueryItems\":[{\"IsGroup\":true,\"Ordinal\":0,\"Join\":\"OR\",\"ItemPath\":\"_0\",\"Value\":null,\"Condition\":{\"FieldName\":null,\"FieldType\":null,\"Operator\":null,\"__msdisposeindex\":622},\"Name\":\"Upcoming\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":621},{\"IsGroup\":false,\"Ordinal\":0,\"Join\":\"AND\",\"ItemPath\":\"_0_0\",\"Value\":\"DateTime.UtcNow.AddMonths(1)\",\"Condition\":{\"FieldName\":\"EventStart\",\"FieldType\":\"System.DateTime\",\"Operator\":\"<=\",\"__msdisposeindex\":624},\"Name\":\"EventStart.DateTime.UtcNow.AddMonths(1)\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":623},{\"IsGroup\":false,\"Ordinal\":1,\"Join\":\"AND\",\"ItemPath\":\"_0_1\",\"Value\":\"DateTime.UtcNow\",\"Condition\":{\"FieldName\":\"EventStart\",\"FieldType\":\"System.DateTime\",\"Operator\":\">=\",\"__msdisposeindex\":626},\"Name\":null,\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":625},{\"IsGroup\":true,\"Ordinal\":1,\"Join\":\"OR\",\"ItemPath\":\"_1\",\"Value\":null,\"Condition\":{\"FieldName\":null,\"FieldType\":null,\"Operator\":null,\"__msdisposeindex\":628},\"Name\":\"Past\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":627},{\"IsGroup\":false,\"Ordinal\":0,\"Join\":\"AND\",\"ItemPath\":\"_1_0\",\"Value\":\"DateTime.UtcNow.AddDays(-7.0)\",\"Condition\":{\"FieldName\":\"EventEnd\",\"FieldType\":\"System.DateTime\",\"Operator\":\">=\",\"__msdisposeindex\":630},\"Name\":\"EventEnd.DateTime.UtcNow.AddDays(-7.0)\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":629},{\"IsGroup\":false,\"Ordinal\":1,\"Join\":\"AND\",\"ItemPath\":\"_1_1\",\"Value\":\"DateTime.UtcNow\",\"Condition\":{\"FieldName\":\"EventEnd\",\"FieldType\":\"System.DateTime\",\"Operator\":\"<=\",\"__msdisposeindex\":632},\"Name\":null,\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":631}],\"TypeProperties\":[],\"_itemPathSeparator\":\"_\"}";
        private const string NextMonthOrLastMonthAdditionalFilters = "{\"__msdisposeindex\":653,\"Title\":null,\"Id\":\"00000000-0000-0000-0000-000000000000\",\"QueryItems\":[{\"IsGroup\":true,\"Ordinal\":0,\"Join\":\"OR\",\"ItemPath\":\"_0\",\"Value\":null,\"Condition\":{\"FieldName\":null,\"FieldType\":null,\"Operator\":null,\"__msdisposeindex\":655},\"Name\":\"Upcoming\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":654},{\"IsGroup\":false,\"Ordinal\":0,\"Join\":\"AND\",\"ItemPath\":\"_0_0\",\"Value\":\"DateTime.UtcNow.AddMonths(1)\",\"Condition\":{\"FieldName\":\"EventStart\",\"FieldType\":\"System.DateTime\",\"Operator\":\"<=\",\"__msdisposeindex\":657},\"Name\":\"EventStart.DateTime.UtcNow.AddMonths(1)\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":656},{\"IsGroup\":false,\"Ordinal\":1,\"Join\":\"AND\",\"ItemPath\":\"_0_1\",\"Value\":\"DateTime.UtcNow\",\"Condition\":{\"FieldName\":\"EventStart\",\"FieldType\":\"System.DateTime\",\"Operator\":\">=\",\"__msdisposeindex\":659},\"Name\":null,\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":658},{\"IsGroup\":true,\"Ordinal\":1,\"Join\":\"OR\",\"ItemPath\":\"_1\",\"Value\":null,\"Condition\":{\"FieldName\":null,\"FieldType\":null,\"Operator\":null,\"__msdisposeindex\":661},\"Name\":\"Past\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":660},{\"IsGroup\":false,\"Ordinal\":0,\"Join\":\"AND\",\"ItemPath\":\"_1_0\",\"Value\":\"DateTime.UtcNow.AddMonths(-1)\",\"Condition\":{\"FieldName\":\"EventEnd\",\"FieldType\":\"System.DateTime\",\"Operator\":\">=\",\"__msdisposeindex\":663},\"Name\":\"EventEnd.DateTime.UtcNow.AddMonths(-1)\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":662},{\"IsGroup\":false,\"Ordinal\":1,\"Join\":\"AND\",\"ItemPath\":\"_1_1\",\"Value\":\"DateTime.UtcNow\",\"Condition\":{\"FieldName\":\"EventEnd\",\"FieldType\":\"System.DateTime\",\"Operator\":\"<=\",\"__msdisposeindex\":665},\"Name\":null,\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":664}],\"TypeProperties\":[],\"_itemPathSeparator\":\"_\"}";
        private const string NextMonthOrLastYearAdditionalFilters = "{\"__msdisposeindex\":686,\"Title\":null,\"Id\":\"00000000-0000-0000-0000-000000000000\",\"QueryItems\":[{\"IsGroup\":true,\"Ordinal\":0,\"Join\":\"OR\",\"ItemPath\":\"_0\",\"Value\":null,\"Condition\":{\"FieldName\":null,\"FieldType\":null,\"Operator\":null,\"__msdisposeindex\":688},\"Name\":\"Upcoming\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":687},{\"IsGroup\":false,\"Ordinal\":0,\"Join\":\"AND\",\"ItemPath\":\"_0_0\",\"Value\":\"DateTime.UtcNow.AddMonths(1)\",\"Condition\":{\"FieldName\":\"EventStart\",\"FieldType\":\"System.DateTime\",\"Operator\":\"<=\",\"__msdisposeindex\":690},\"Name\":\"EventStart.DateTime.UtcNow.AddMonths(1)\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":689},{\"IsGroup\":false,\"Ordinal\":1,\"Join\":\"AND\",\"ItemPath\":\"_0_1\",\"Value\":\"DateTime.UtcNow\",\"Condition\":{\"FieldName\":\"EventStart\",\"FieldType\":\"System.DateTime\",\"Operator\":\">=\",\"__msdisposeindex\":692},\"Name\":null,\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":691},{\"IsGroup\":true,\"Ordinal\":1,\"Join\":\"OR\",\"ItemPath\":\"_1\",\"Value\":null,\"Condition\":{\"FieldName\":null,\"FieldType\":null,\"Operator\":null,\"__msdisposeindex\":694},\"Name\":\"Past\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":693},{\"IsGroup\":false,\"Ordinal\":0,\"Join\":\"AND\",\"ItemPath\":\"_1_0\",\"Value\":\"DateTime.UtcNow.AddYears(-1)\",\"Condition\":{\"FieldName\":\"EventEnd\",\"FieldType\":\"System.DateTime\",\"Operator\":\">=\",\"__msdisposeindex\":696},\"Name\":\"EventEnd.DateTime.UtcNow.AddYears(-1)\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":695},{\"IsGroup\":false,\"Ordinal\":1,\"Join\":\"AND\",\"ItemPath\":\"_1_1\",\"Value\":\"DateTime.UtcNow\",\"Condition\":{\"FieldName\":\"EventEnd\",\"FieldType\":\"System.DateTime\",\"Operator\":\"<=\",\"__msdisposeindex\":698},\"Name\":null,\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":697}],\"TypeProperties\":[],\"_itemPathSeparator\":\"_\"}";

        private const string NextMonthOrLast3DaysOrCurrentAdditionalFilters = "{\"__msdisposeindex\":301,\"Title\":null,\"Id\":\"00000000-0000-0000-0000-000000000000\",\"QueryItems\":[{\"IsGroup\":true,\"Ordinal\":0,\"Join\":\"OR\",\"ItemPath\":\"_0\",\"Value\":null,\"Condition\":{\"FieldName\":null,\"FieldType\":null,\"Operator\":null,\"__msdisposeindex\":303},\"Name\":\"Current\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":302},{\"IsGroup\":false,\"Ordinal\":0,\"Join\":\"AND\",\"ItemPath\":\"_0_0\",\"Value\":\"DateTime.UtcNow\",\"Condition\":{\"FieldName\":\"EventStart\",\"FieldType\":\"System.DateTime\",\"Operator\":\"<=\",\"__msdisposeindex\":305},\"Name\":null,\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":304},{\"IsGroup\":false,\"Ordinal\":1,\"Join\":\"AND\",\"ItemPath\":\"_0_1\",\"Value\":\"DateTime.UtcNow\",\"Condition\":{\"FieldName\":\"EventEnd\",\"FieldType\":\"System.DateTime\",\"Operator\":\">=\",\"__msdisposeindex\":307},\"Name\":null,\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":306},{\"IsGroup\":true,\"Ordinal\":1,\"Join\":\"OR\",\"ItemPath\":\"_1\",\"Value\":null,\"Condition\":{\"FieldName\":null,\"FieldType\":null,\"Operator\":null,\"__msdisposeindex\":309},\"Name\":\"Upcoming\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":308},{\"IsGroup\":false,\"Ordinal\":0,\"Join\":\"AND\",\"ItemPath\":\"_1_0\",\"Value\":\"DateTime.UtcNow.AddMonths(1)\",\"Condition\":{\"FieldName\":\"EventStart\",\"FieldType\":\"System.DateTime\",\"Operator\":\"<=\",\"__msdisposeindex\":311},\"Name\":\"EventStart.DateTime.UtcNow.AddMonths(1)\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":310},{\"IsGroup\":false,\"Ordinal\":1,\"Join\":\"AND\",\"ItemPath\":\"_1_1\",\"Value\":\"DateTime.UtcNow\",\"Condition\":{\"FieldName\":\"EventStart\",\"FieldType\":\"System.DateTime\",\"Operator\":\">=\",\"__msdisposeindex\":313},\"Name\":null,\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":312},{\"IsGroup\":true,\"Ordinal\":2,\"Join\":\"OR\",\"ItemPath\":\"_2\",\"Value\":null,\"Condition\":{\"FieldName\":null,\"FieldType\":null,\"Operator\":null,\"__msdisposeindex\":315},\"Name\":\"Past\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":314},{\"IsGroup\":false,\"Ordinal\":0,\"Join\":\"AND\",\"ItemPath\":\"_2_0\",\"Value\":\"DateTime.UtcNow.AddDays(-3.0)\",\"Condition\":{\"FieldName\":\"EventEnd\",\"FieldType\":\"System.DateTime\",\"Operator\":\">=\",\"__msdisposeindex\":317},\"Name\":\"EventEnd.DateTime.UtcNow.AddDays(-3.0)\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":316},{\"IsGroup\":false,\"Ordinal\":1,\"Join\":\"AND\",\"ItemPath\":\"_2_1\",\"Value\":\"DateTime.UtcNow\",\"Condition\":{\"FieldName\":\"EventEnd\",\"FieldType\":\"System.DateTime\",\"Operator\":\"<=\",\"__msdisposeindex\":319},\"Name\":null,\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":318}],\"TypeProperties\":[],\"_itemPathSeparator\":\"_\"}";
        private const string NextMonthOrLastWeekOrCurrentAdditionalFilters = "{\"__msdisposeindex\":346,\"Title\":null,\"Id\":\"00000000-0000-0000-0000-000000000000\",\"QueryItems\":[{\"IsGroup\":true,\"Ordinal\":0,\"Join\":\"OR\",\"ItemPath\":\"_0\",\"Value\":null,\"Condition\":{\"FieldName\":null,\"FieldType\":null,\"Operator\":null,\"__msdisposeindex\":348},\"Name\":\"Current\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":347},{\"IsGroup\":false,\"Ordinal\":0,\"Join\":\"AND\",\"ItemPath\":\"_0_0\",\"Value\":\"DateTime.UtcNow\",\"Condition\":{\"FieldName\":\"EventStart\",\"FieldType\":\"System.DateTime\",\"Operator\":\"<=\",\"__msdisposeindex\":350},\"Name\":null,\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":349},{\"IsGroup\":false,\"Ordinal\":1,\"Join\":\"AND\",\"ItemPath\":\"_0_1\",\"Value\":\"DateTime.UtcNow\",\"Condition\":{\"FieldName\":\"EventEnd\",\"FieldType\":\"System.DateTime\",\"Operator\":\">=\",\"__msdisposeindex\":352},\"Name\":null,\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":351},{\"IsGroup\":true,\"Ordinal\":1,\"Join\":\"OR\",\"ItemPath\":\"_1\",\"Value\":null,\"Condition\":{\"FieldName\":null,\"FieldType\":null,\"Operator\":null,\"__msdisposeindex\":354},\"Name\":\"Upcoming\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":353},{\"IsGroup\":false,\"Ordinal\":0,\"Join\":\"AND\",\"ItemPath\":\"_1_0\",\"Value\":\"DateTime.UtcNow.AddMonths(1)\",\"Condition\":{\"FieldName\":\"EventStart\",\"FieldType\":\"System.DateTime\",\"Operator\":\"<=\",\"__msdisposeindex\":356},\"Name\":\"EventStart.DateTime.UtcNow.AddMonths(1)\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":355},{\"IsGroup\":false,\"Ordinal\":1,\"Join\":\"AND\",\"ItemPath\":\"_1_1\",\"Value\":\"DateTime.UtcNow\",\"Condition\":{\"FieldName\":\"EventStart\",\"FieldType\":\"System.DateTime\",\"Operator\":\">=\",\"__msdisposeindex\":358},\"Name\":null,\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":357},{\"IsGroup\":true,\"Ordinal\":2,\"Join\":\"OR\",\"ItemPath\":\"_2\",\"Value\":null,\"Condition\":{\"FieldName\":null,\"FieldType\":null,\"Operator\":null,\"__msdisposeindex\":360},\"Name\":\"Past\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":359},{\"IsGroup\":false,\"Ordinal\":0,\"Join\":\"AND\",\"ItemPath\":\"_2_0\",\"Value\":\"DateTime.UtcNow.AddDays(-7.0)\",\"Condition\":{\"FieldName\":\"EventEnd\",\"FieldType\":\"System.DateTime\",\"Operator\":\">=\",\"__msdisposeindex\":362},\"Name\":\"EventEnd.DateTime.UtcNow.AddDays(-7.0)\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":361},{\"IsGroup\":false,\"Ordinal\":1,\"Join\":\"AND\",\"ItemPath\":\"_2_1\",\"Value\":\"DateTime.UtcNow\",\"Condition\":{\"FieldName\":\"EventEnd\",\"FieldType\":\"System.DateTime\",\"Operator\":\"<=\",\"__msdisposeindex\":364},\"Name\":null,\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":363}],\"TypeProperties\":[],\"_itemPathSeparator\":\"_\"}";
        private const string NextMonthOrLastMonthOrCurrentAdditionalFilters = "{\"__msdisposeindex\":391,\"Title\":null,\"Id\":\"00000000-0000-0000-0000-000000000000\",\"QueryItems\":[{\"IsGroup\":true,\"Ordinal\":0,\"Join\":\"OR\",\"ItemPath\":\"_0\",\"Value\":null,\"Condition\":{\"FieldName\":null,\"FieldType\":null,\"Operator\":null,\"__msdisposeindex\":393},\"Name\":\"Current\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":392},{\"IsGroup\":false,\"Ordinal\":0,\"Join\":\"AND\",\"ItemPath\":\"_0_0\",\"Value\":\"DateTime.UtcNow\",\"Condition\":{\"FieldName\":\"EventStart\",\"FieldType\":\"System.DateTime\",\"Operator\":\"<=\",\"__msdisposeindex\":395},\"Name\":null,\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":394},{\"IsGroup\":false,\"Ordinal\":1,\"Join\":\"AND\",\"ItemPath\":\"_0_1\",\"Value\":\"DateTime.UtcNow\",\"Condition\":{\"FieldName\":\"EventEnd\",\"FieldType\":\"System.DateTime\",\"Operator\":\">=\",\"__msdisposeindex\":397},\"Name\":null,\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":396},{\"IsGroup\":true,\"Ordinal\":1,\"Join\":\"OR\",\"ItemPath\":\"_1\",\"Value\":null,\"Condition\":{\"FieldName\":null,\"FieldType\":null,\"Operator\":null,\"__msdisposeindex\":399},\"Name\":\"Upcoming\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":398},{\"IsGroup\":false,\"Ordinal\":0,\"Join\":\"AND\",\"ItemPath\":\"_1_0\",\"Value\":\"DateTime.UtcNow.AddMonths(1)\",\"Condition\":{\"FieldName\":\"EventStart\",\"FieldType\":\"System.DateTime\",\"Operator\":\"<=\",\"__msdisposeindex\":401},\"Name\":\"EventStart.DateTime.UtcNow.AddMonths(1)\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":400},{\"IsGroup\":false,\"Ordinal\":1,\"Join\":\"AND\",\"ItemPath\":\"_1_1\",\"Value\":\"DateTime.UtcNow\",\"Condition\":{\"FieldName\":\"EventStart\",\"FieldType\":\"System.DateTime\",\"Operator\":\">=\",\"__msdisposeindex\":403},\"Name\":null,\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":402},{\"IsGroup\":true,\"Ordinal\":2,\"Join\":\"OR\",\"ItemPath\":\"_2\",\"Value\":null,\"Condition\":{\"FieldName\":null,\"FieldType\":null,\"Operator\":null,\"__msdisposeindex\":405},\"Name\":\"Past\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":404},{\"IsGroup\":false,\"Ordinal\":0,\"Join\":\"AND\",\"ItemPath\":\"_2_0\",\"Value\":\"DateTime.UtcNow.AddMonths(-1)\",\"Condition\":{\"FieldName\":\"EventEnd\",\"FieldType\":\"System.DateTime\",\"Operator\":\">=\",\"__msdisposeindex\":407},\"Name\":\"EventEnd.DateTime.UtcNow.AddMonths(-1)\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":406},{\"IsGroup\":false,\"Ordinal\":1,\"Join\":\"AND\",\"ItemPath\":\"_2_1\",\"Value\":\"DateTime.UtcNow\",\"Condition\":{\"FieldName\":\"EventEnd\",\"FieldType\":\"System.DateTime\",\"Operator\":\"<=\",\"__msdisposeindex\":409},\"Name\":null,\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":408}],\"TypeProperties\":[],\"_itemPathSeparator\":\"_\"}";
        private const string NextMonthOrLastYearOrCurrentAdditionalFilters = "{\"__msdisposeindex\":436,\"Title\":null,\"Id\":\"00000000-0000-0000-0000-000000000000\",\"QueryItems\":[{\"IsGroup\":true,\"Ordinal\":0,\"Join\":\"OR\",\"ItemPath\":\"_0\",\"Value\":null,\"Condition\":{\"FieldName\":null,\"FieldType\":null,\"Operator\":null,\"__msdisposeindex\":438},\"Name\":\"Current\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":437},{\"IsGroup\":false,\"Ordinal\":0,\"Join\":\"AND\",\"ItemPath\":\"_0_0\",\"Value\":\"DateTime.UtcNow\",\"Condition\":{\"FieldName\":\"EventStart\",\"FieldType\":\"System.DateTime\",\"Operator\":\"<=\",\"__msdisposeindex\":440},\"Name\":null,\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":439},{\"IsGroup\":false,\"Ordinal\":1,\"Join\":\"AND\",\"ItemPath\":\"_0_1\",\"Value\":\"DateTime.UtcNow\",\"Condition\":{\"FieldName\":\"EventEnd\",\"FieldType\":\"System.DateTime\",\"Operator\":\">=\",\"__msdisposeindex\":442},\"Name\":null,\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":441},{\"IsGroup\":true,\"Ordinal\":1,\"Join\":\"OR\",\"ItemPath\":\"_1\",\"Value\":null,\"Condition\":{\"FieldName\":null,\"FieldType\":null,\"Operator\":null,\"__msdisposeindex\":444},\"Name\":\"Upcoming\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":443},{\"IsGroup\":false,\"Ordinal\":0,\"Join\":\"AND\",\"ItemPath\":\"_1_0\",\"Value\":\"DateTime.UtcNow.AddMonths(1)\",\"Condition\":{\"FieldName\":\"EventStart\",\"FieldType\":\"System.DateTime\",\"Operator\":\"<=\",\"__msdisposeindex\":446},\"Name\":\"EventStart.DateTime.UtcNow.AddMonths(1)\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":445},{\"IsGroup\":false,\"Ordinal\":1,\"Join\":\"AND\",\"ItemPath\":\"_1_1\",\"Value\":\"DateTime.UtcNow\",\"Condition\":{\"FieldName\":\"EventStart\",\"FieldType\":\"System.DateTime\",\"Operator\":\">=\",\"__msdisposeindex\":448},\"Name\":null,\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":447},{\"IsGroup\":true,\"Ordinal\":2,\"Join\":\"OR\",\"ItemPath\":\"_2\",\"Value\":null,\"Condition\":{\"FieldName\":null,\"FieldType\":null,\"Operator\":null,\"__msdisposeindex\":450},\"Name\":\"Past\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":449},{\"IsGroup\":false,\"Ordinal\":0,\"Join\":\"AND\",\"ItemPath\":\"_2_0\",\"Value\":\"DateTime.UtcNow.AddYears(-1)\",\"Condition\":{\"FieldName\":\"EventEnd\",\"FieldType\":\"System.DateTime\",\"Operator\":\">=\",\"__msdisposeindex\":452},\"Name\":\"EventEnd.DateTime.UtcNow.AddYears(-1)\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":451},{\"IsGroup\":false,\"Ordinal\":1,\"Join\":\"AND\",\"ItemPath\":\"_2_1\",\"Value\":\"DateTime.UtcNow\",\"Condition\":{\"FieldName\":\"EventEnd\",\"FieldType\":\"System.DateTime\",\"Operator\":\"<=\",\"__msdisposeindex\":454},\"Name\":null,\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":453}],\"TypeProperties\":[],\"_itemPathSeparator\":\"_\"}";

        private const string NextYearOrLast3DaysAdditionalFilters = "{\"__msdisposeindex\":724,\"Title\":null,\"Id\":\"00000000-0000-0000-0000-000000000000\",\"QueryItems\":[{\"IsGroup\":true,\"Ordinal\":0,\"Join\":\"OR\",\"ItemPath\":\"_0\",\"Value\":null,\"Condition\":{\"FieldName\":null,\"FieldType\":null,\"Operator\":null,\"__msdisposeindex\":726},\"Name\":\"Upcoming\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":725},{\"IsGroup\":false,\"Ordinal\":0,\"Join\":\"AND\",\"ItemPath\":\"_0_0\",\"Value\":\"DateTime.UtcNow.AddYears(1)\",\"Condition\":{\"FieldName\":\"EventStart\",\"FieldType\":\"System.DateTime\",\"Operator\":\"<=\",\"__msdisposeindex\":728},\"Name\":\"EventStart.DateTime.UtcNow.AddYears(1)\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":727},{\"IsGroup\":false,\"Ordinal\":1,\"Join\":\"AND\",\"ItemPath\":\"_0_1\",\"Value\":\"DateTime.UtcNow\",\"Condition\":{\"FieldName\":\"EventStart\",\"FieldType\":\"System.DateTime\",\"Operator\":\">=\",\"__msdisposeindex\":730},\"Name\":null,\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":729},{\"IsGroup\":true,\"Ordinal\":1,\"Join\":\"OR\",\"ItemPath\":\"_1\",\"Value\":null,\"Condition\":{\"FieldName\":null,\"FieldType\":null,\"Operator\":null,\"__msdisposeindex\":732},\"Name\":\"Past\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":731},{\"IsGroup\":false,\"Ordinal\":0,\"Join\":\"AND\",\"ItemPath\":\"_1_0\",\"Value\":\"DateTime.UtcNow.AddDays(-3.0)\",\"Condition\":{\"FieldName\":\"EventEnd\",\"FieldType\":\"System.DateTime\",\"Operator\":\">=\",\"__msdisposeindex\":734},\"Name\":\"EventEnd.DateTime.UtcNow.AddDays(-3.0)\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":733},{\"IsGroup\":false,\"Ordinal\":1,\"Join\":\"AND\",\"ItemPath\":\"_1_1\",\"Value\":\"DateTime.UtcNow\",\"Condition\":{\"FieldName\":\"EventEnd\",\"FieldType\":\"System.DateTime\",\"Operator\":\"<=\",\"__msdisposeindex\":736},\"Name\":null,\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":735}],\"TypeProperties\":[],\"_itemPathSeparator\":\"_\"}";
        private const string NextYearOrLastWeekAdditionalFilters = "{\"__msdisposeindex\":757,\"Title\":null,\"Id\":\"00000000-0000-0000-0000-000000000000\",\"QueryItems\":[{\"IsGroup\":true,\"Ordinal\":0,\"Join\":\"OR\",\"ItemPath\":\"_0\",\"Value\":null,\"Condition\":{\"FieldName\":null,\"FieldType\":null,\"Operator\":null,\"__msdisposeindex\":759},\"Name\":\"Upcoming\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":758},{\"IsGroup\":false,\"Ordinal\":0,\"Join\":\"AND\",\"ItemPath\":\"_0_0\",\"Value\":\"DateTime.UtcNow.AddYears(1)\",\"Condition\":{\"FieldName\":\"EventStart\",\"FieldType\":\"System.DateTime\",\"Operator\":\"<=\",\"__msdisposeindex\":761},\"Name\":\"EventStart.DateTime.UtcNow.AddYears(1)\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":760},{\"IsGroup\":false,\"Ordinal\":1,\"Join\":\"AND\",\"ItemPath\":\"_0_1\",\"Value\":\"DateTime.UtcNow\",\"Condition\":{\"FieldName\":\"EventStart\",\"FieldType\":\"System.DateTime\",\"Operator\":\">=\",\"__msdisposeindex\":763},\"Name\":null,\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":762},{\"IsGroup\":true,\"Ordinal\":1,\"Join\":\"OR\",\"ItemPath\":\"_1\",\"Value\":null,\"Condition\":{\"FieldName\":null,\"FieldType\":null,\"Operator\":null,\"__msdisposeindex\":765},\"Name\":\"Past\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":764},{\"IsGroup\":false,\"Ordinal\":0,\"Join\":\"AND\",\"ItemPath\":\"_1_0\",\"Value\":\"DateTime.UtcNow.AddDays(-7.0)\",\"Condition\":{\"FieldName\":\"EventEnd\",\"FieldType\":\"System.DateTime\",\"Operator\":\">=\",\"__msdisposeindex\":767},\"Name\":\"EventEnd.DateTime.UtcNow.AddDays(-7.0)\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":766},{\"IsGroup\":false,\"Ordinal\":1,\"Join\":\"AND\",\"ItemPath\":\"_1_1\",\"Value\":\"DateTime.UtcNow\",\"Condition\":{\"FieldName\":\"EventEnd\",\"FieldType\":\"System.DateTime\",\"Operator\":\"<=\",\"__msdisposeindex\":769},\"Name\":null,\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":768}],\"TypeProperties\":[],\"_itemPathSeparator\":\"_\"}";
        private const string NextYearOrLastMonthAdditionalFilters = "{\"__msdisposeindex\":790,\"Title\":null,\"Id\":\"00000000-0000-0000-0000-000000000000\",\"QueryItems\":[{\"IsGroup\":true,\"Ordinal\":0,\"Join\":\"OR\",\"ItemPath\":\"_0\",\"Value\":null,\"Condition\":{\"FieldName\":null,\"FieldType\":null,\"Operator\":null,\"__msdisposeindex\":792},\"Name\":\"Upcoming\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":791},{\"IsGroup\":false,\"Ordinal\":0,\"Join\":\"AND\",\"ItemPath\":\"_0_0\",\"Value\":\"DateTime.UtcNow.AddYears(1)\",\"Condition\":{\"FieldName\":\"EventStart\",\"FieldType\":\"System.DateTime\",\"Operator\":\"<=\",\"__msdisposeindex\":794},\"Name\":\"EventStart.DateTime.UtcNow.AddYears(1)\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":793},{\"IsGroup\":false,\"Ordinal\":1,\"Join\":\"AND\",\"ItemPath\":\"_0_1\",\"Value\":\"DateTime.UtcNow\",\"Condition\":{\"FieldName\":\"EventStart\",\"FieldType\":\"System.DateTime\",\"Operator\":\">=\",\"__msdisposeindex\":796},\"Name\":null,\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":795},{\"IsGroup\":true,\"Ordinal\":1,\"Join\":\"OR\",\"ItemPath\":\"_1\",\"Value\":null,\"Condition\":{\"FieldName\":null,\"FieldType\":null,\"Operator\":null,\"__msdisposeindex\":798},\"Name\":\"Past\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":797},{\"IsGroup\":false,\"Ordinal\":0,\"Join\":\"AND\",\"ItemPath\":\"_1_0\",\"Value\":\"DateTime.UtcNow.AddMonths(-1)\",\"Condition\":{\"FieldName\":\"EventEnd\",\"FieldType\":\"System.DateTime\",\"Operator\":\">=\",\"__msdisposeindex\":800},\"Name\":\"EventEnd.DateTime.UtcNow.AddMonths(-1)\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":799},{\"IsGroup\":false,\"Ordinal\":1,\"Join\":\"AND\",\"ItemPath\":\"_1_1\",\"Value\":\"DateTime.UtcNow\",\"Condition\":{\"FieldName\":\"EventEnd\",\"FieldType\":\"System.DateTime\",\"Operator\":\"<=\",\"__msdisposeindex\":802},\"Name\":null,\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":801}],\"TypeProperties\":[],\"_itemPathSeparator\":\"_\"}";
        private const string NextYearOrLastYearAdditionalFilters = "{\"__msdisposeindex\":823,\"Title\":null,\"Id\":\"00000000-0000-0000-0000-000000000000\",\"QueryItems\":[{\"IsGroup\":true,\"Ordinal\":0,\"Join\":\"OR\",\"ItemPath\":\"_0\",\"Value\":null,\"Condition\":{\"FieldName\":null,\"FieldType\":null,\"Operator\":null,\"__msdisposeindex\":825},\"Name\":\"Upcoming\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":824},{\"IsGroup\":false,\"Ordinal\":0,\"Join\":\"AND\",\"ItemPath\":\"_0_0\",\"Value\":\"DateTime.UtcNow.AddYears(1)\",\"Condition\":{\"FieldName\":\"EventStart\",\"FieldType\":\"System.DateTime\",\"Operator\":\"<=\",\"__msdisposeindex\":827},\"Name\":\"EventStart.DateTime.UtcNow.AddYears(1)\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":826},{\"IsGroup\":false,\"Ordinal\":1,\"Join\":\"AND\",\"ItemPath\":\"_0_1\",\"Value\":\"DateTime.UtcNow\",\"Condition\":{\"FieldName\":\"EventStart\",\"FieldType\":\"System.DateTime\",\"Operator\":\">=\",\"__msdisposeindex\":829},\"Name\":null,\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":828},{\"IsGroup\":true,\"Ordinal\":1,\"Join\":\"OR\",\"ItemPath\":\"_1\",\"Value\":null,\"Condition\":{\"FieldName\":null,\"FieldType\":null,\"Operator\":null,\"__msdisposeindex\":831},\"Name\":\"Past\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":830},{\"IsGroup\":false,\"Ordinal\":0,\"Join\":\"AND\",\"ItemPath\":\"_1_0\",\"Value\":\"DateTime.UtcNow.AddYears(-1)\",\"Condition\":{\"FieldName\":\"EventEnd\",\"FieldType\":\"System.DateTime\",\"Operator\":\">=\",\"__msdisposeindex\":833},\"Name\":\"EventEnd.DateTime.UtcNow.AddYears(-1)\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":832},{\"IsGroup\":false,\"Ordinal\":1,\"Join\":\"AND\",\"ItemPath\":\"_1_1\",\"Value\":\"DateTime.UtcNow\",\"Condition\":{\"FieldName\":\"EventEnd\",\"FieldType\":\"System.DateTime\",\"Operator\":\"<=\",\"__msdisposeindex\":835},\"Name\":null,\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":834}],\"TypeProperties\":[],\"_itemPathSeparator\":\"_\"}";

        private const string NextYearOrLast3DaysOrCurrentAdditionalFilters = "{\"__msdisposeindex\":486,\"Title\":null,\"Id\":\"00000000-0000-0000-0000-000000000000\",\"QueryItems\":[{\"IsGroup\":true,\"Ordinal\":0,\"Join\":\"OR\",\"ItemPath\":\"_0\",\"Value\":null,\"Condition\":{\"FieldName\":null,\"FieldType\":null,\"Operator\":null,\"__msdisposeindex\":488},\"Name\":\"Current\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":487},{\"IsGroup\":false,\"Ordinal\":0,\"Join\":\"AND\",\"ItemPath\":\"_0_0\",\"Value\":\"DateTime.UtcNow\",\"Condition\":{\"FieldName\":\"EventStart\",\"FieldType\":\"System.DateTime\",\"Operator\":\"<=\",\"__msdisposeindex\":490},\"Name\":null,\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":489},{\"IsGroup\":false,\"Ordinal\":1,\"Join\":\"AND\",\"ItemPath\":\"_0_1\",\"Value\":\"DateTime.UtcNow\",\"Condition\":{\"FieldName\":\"EventEnd\",\"FieldType\":\"System.DateTime\",\"Operator\":\">=\",\"__msdisposeindex\":492},\"Name\":null,\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":491},{\"IsGroup\":true,\"Ordinal\":1,\"Join\":\"OR\",\"ItemPath\":\"_1\",\"Value\":null,\"Condition\":{\"FieldName\":null,\"FieldType\":null,\"Operator\":null,\"__msdisposeindex\":494},\"Name\":\"Upcoming\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":493},{\"IsGroup\":false,\"Ordinal\":0,\"Join\":\"AND\",\"ItemPath\":\"_1_0\",\"Value\":\"DateTime.UtcNow.AddYears(1)\",\"Condition\":{\"FieldName\":\"EventStart\",\"FieldType\":\"System.DateTime\",\"Operator\":\"<=\",\"__msdisposeindex\":496},\"Name\":\"EventStart.DateTime.UtcNow.AddYears(1)\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":495},{\"IsGroup\":false,\"Ordinal\":1,\"Join\":\"AND\",\"ItemPath\":\"_1_1\",\"Value\":\"DateTime.UtcNow\",\"Condition\":{\"FieldName\":\"EventStart\",\"FieldType\":\"System.DateTime\",\"Operator\":\">=\",\"__msdisposeindex\":498},\"Name\":null,\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":497},{\"IsGroup\":true,\"Ordinal\":2,\"Join\":\"OR\",\"ItemPath\":\"_2\",\"Value\":null,\"Condition\":{\"FieldName\":null,\"FieldType\":null,\"Operator\":null,\"__msdisposeindex\":500},\"Name\":\"Past\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":499},{\"IsGroup\":false,\"Ordinal\":0,\"Join\":\"AND\",\"ItemPath\":\"_2_0\",\"Value\":\"DateTime.UtcNow.AddDays(-3.0)\",\"Condition\":{\"FieldName\":\"EventEnd\",\"FieldType\":\"System.DateTime\",\"Operator\":\">=\",\"__msdisposeindex\":502},\"Name\":\"EventEnd.DateTime.UtcNow.AddDays(-3.0)\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":501},{\"IsGroup\":false,\"Ordinal\":1,\"Join\":\"AND\",\"ItemPath\":\"_2_1\",\"Value\":\"DateTime.UtcNow\",\"Condition\":{\"FieldName\":\"EventEnd\",\"FieldType\":\"System.DateTime\",\"Operator\":\"<=\",\"__msdisposeindex\":504},\"Name\":null,\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":503}],\"TypeProperties\":[],\"_itemPathSeparator\":\"_\"}";
        private const string NextYearOrLastWeekOrCurrentAdditionalFilters = "{\"__msdisposeindex\":531,\"Title\":null,\"Id\":\"00000000-0000-0000-0000-000000000000\",\"QueryItems\":[{\"IsGroup\":true,\"Ordinal\":0,\"Join\":\"OR\",\"ItemPath\":\"_0\",\"Value\":null,\"Condition\":{\"FieldName\":null,\"FieldType\":null,\"Operator\":null,\"__msdisposeindex\":533},\"Name\":\"Current\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":532},{\"IsGroup\":false,\"Ordinal\":0,\"Join\":\"AND\",\"ItemPath\":\"_0_0\",\"Value\":\"DateTime.UtcNow\",\"Condition\":{\"FieldName\":\"EventStart\",\"FieldType\":\"System.DateTime\",\"Operator\":\"<=\",\"__msdisposeindex\":535},\"Name\":null,\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":534},{\"IsGroup\":false,\"Ordinal\":1,\"Join\":\"AND\",\"ItemPath\":\"_0_1\",\"Value\":\"DateTime.UtcNow\",\"Condition\":{\"FieldName\":\"EventEnd\",\"FieldType\":\"System.DateTime\",\"Operator\":\">=\",\"__msdisposeindex\":537},\"Name\":null,\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":536},{\"IsGroup\":true,\"Ordinal\":1,\"Join\":\"OR\",\"ItemPath\":\"_1\",\"Value\":null,\"Condition\":{\"FieldName\":null,\"FieldType\":null,\"Operator\":null,\"__msdisposeindex\":539},\"Name\":\"Upcoming\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":538},{\"IsGroup\":false,\"Ordinal\":0,\"Join\":\"AND\",\"ItemPath\":\"_1_0\",\"Value\":\"DateTime.UtcNow.AddYears(1)\",\"Condition\":{\"FieldName\":\"EventStart\",\"FieldType\":\"System.DateTime\",\"Operator\":\"<=\",\"__msdisposeindex\":541},\"Name\":\"EventStart.DateTime.UtcNow.AddYears(1)\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":540},{\"IsGroup\":false,\"Ordinal\":1,\"Join\":\"AND\",\"ItemPath\":\"_1_1\",\"Value\":\"DateTime.UtcNow\",\"Condition\":{\"FieldName\":\"EventStart\",\"FieldType\":\"System.DateTime\",\"Operator\":\">=\",\"__msdisposeindex\":543},\"Name\":null,\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":542},{\"IsGroup\":true,\"Ordinal\":2,\"Join\":\"OR\",\"ItemPath\":\"_2\",\"Value\":null,\"Condition\":{\"FieldName\":null,\"FieldType\":null,\"Operator\":null,\"__msdisposeindex\":545},\"Name\":\"Past\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":544},{\"IsGroup\":false,\"Ordinal\":0,\"Join\":\"AND\",\"ItemPath\":\"_2_0\",\"Value\":\"DateTime.UtcNow.AddDays(-7.0)\",\"Condition\":{\"FieldName\":\"EventEnd\",\"FieldType\":\"System.DateTime\",\"Operator\":\">=\",\"__msdisposeindex\":547},\"Name\":\"EventEnd.DateTime.UtcNow.AddDays(-7.0)\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":546},{\"IsGroup\":false,\"Ordinal\":1,\"Join\":\"AND\",\"ItemPath\":\"_2_1\",\"Value\":\"DateTime.UtcNow\",\"Condition\":{\"FieldName\":\"EventEnd\",\"FieldType\":\"System.DateTime\",\"Operator\":\"<=\",\"__msdisposeindex\":549},\"Name\":null,\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":548}],\"TypeProperties\":[],\"_itemPathSeparator\":\"_\"}";
        private const string NextYearOrLastMonthOrCurrentAdditionalFilters = "{\"__msdisposeindex\":576,\"Title\":null,\"Id\":\"00000000-0000-0000-0000-000000000000\",\"QueryItems\":[{\"IsGroup\":true,\"Ordinal\":0,\"Join\":\"OR\",\"ItemPath\":\"_0\",\"Value\":null,\"Condition\":{\"FieldName\":null,\"FieldType\":null,\"Operator\":null,\"__msdisposeindex\":578},\"Name\":\"Current\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":577},{\"IsGroup\":false,\"Ordinal\":0,\"Join\":\"AND\",\"ItemPath\":\"_0_0\",\"Value\":\"DateTime.UtcNow\",\"Condition\":{\"FieldName\":\"EventStart\",\"FieldType\":\"System.DateTime\",\"Operator\":\"<=\",\"__msdisposeindex\":580},\"Name\":null,\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":579},{\"IsGroup\":false,\"Ordinal\":1,\"Join\":\"AND\",\"ItemPath\":\"_0_1\",\"Value\":\"DateTime.UtcNow\",\"Condition\":{\"FieldName\":\"EventEnd\",\"FieldType\":\"System.DateTime\",\"Operator\":\">=\",\"__msdisposeindex\":582},\"Name\":null,\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":581},{\"IsGroup\":true,\"Ordinal\":1,\"Join\":\"OR\",\"ItemPath\":\"_1\",\"Value\":null,\"Condition\":{\"FieldName\":null,\"FieldType\":null,\"Operator\":null,\"__msdisposeindex\":584},\"Name\":\"Upcoming\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":583},{\"IsGroup\":false,\"Ordinal\":0,\"Join\":\"AND\",\"ItemPath\":\"_1_0\",\"Value\":\"DateTime.UtcNow.AddYears(1)\",\"Condition\":{\"FieldName\":\"EventStart\",\"FieldType\":\"System.DateTime\",\"Operator\":\"<=\",\"__msdisposeindex\":586},\"Name\":\"EventStart.DateTime.UtcNow.AddYears(1)\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":585},{\"IsGroup\":false,\"Ordinal\":1,\"Join\":\"AND\",\"ItemPath\":\"_1_1\",\"Value\":\"DateTime.UtcNow\",\"Condition\":{\"FieldName\":\"EventStart\",\"FieldType\":\"System.DateTime\",\"Operator\":\">=\",\"__msdisposeindex\":588},\"Name\":null,\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":587},{\"IsGroup\":true,\"Ordinal\":2,\"Join\":\"OR\",\"ItemPath\":\"_2\",\"Value\":null,\"Condition\":{\"FieldName\":null,\"FieldType\":null,\"Operator\":null,\"__msdisposeindex\":590},\"Name\":\"Past\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":589},{\"IsGroup\":false,\"Ordinal\":0,\"Join\":\"AND\",\"ItemPath\":\"_2_0\",\"Value\":\"DateTime.UtcNow.AddMonths(-1)\",\"Condition\":{\"FieldName\":\"EventEnd\",\"FieldType\":\"System.DateTime\",\"Operator\":\">=\",\"__msdisposeindex\":592},\"Name\":\"EventEnd.DateTime.UtcNow.AddMonths(-1)\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":591},{\"IsGroup\":false,\"Ordinal\":1,\"Join\":\"AND\",\"ItemPath\":\"_2_1\",\"Value\":\"DateTime.UtcNow\",\"Condition\":{\"FieldName\":\"EventEnd\",\"FieldType\":\"System.DateTime\",\"Operator\":\"<=\",\"__msdisposeindex\":594},\"Name\":null,\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":593}],\"TypeProperties\":[],\"_itemPathSeparator\":\"_\"}";
        private const string NextYearOrLastYearOrCurrentAdditionalFilters = "{\"__msdisposeindex\":621,\"Title\":null,\"Id\":\"00000000-0000-0000-0000-000000000000\",\"QueryItems\":[{\"IsGroup\":true,\"Ordinal\":0,\"Join\":\"OR\",\"ItemPath\":\"_0\",\"Value\":null,\"Condition\":{\"FieldName\":null,\"FieldType\":null,\"Operator\":null,\"__msdisposeindex\":623},\"Name\":\"Current\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":622},{\"IsGroup\":false,\"Ordinal\":0,\"Join\":\"AND\",\"ItemPath\":\"_0_0\",\"Value\":\"DateTime.UtcNow\",\"Condition\":{\"FieldName\":\"EventStart\",\"FieldType\":\"System.DateTime\",\"Operator\":\"<=\",\"__msdisposeindex\":625},\"Name\":null,\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":624},{\"IsGroup\":false,\"Ordinal\":1,\"Join\":\"AND\",\"ItemPath\":\"_0_1\",\"Value\":\"DateTime.UtcNow\",\"Condition\":{\"FieldName\":\"EventEnd\",\"FieldType\":\"System.DateTime\",\"Operator\":\">=\",\"__msdisposeindex\":627},\"Name\":null,\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":626},{\"IsGroup\":true,\"Ordinal\":1,\"Join\":\"OR\",\"ItemPath\":\"_1\",\"Value\":null,\"Condition\":{\"FieldName\":null,\"FieldType\":null,\"Operator\":null,\"__msdisposeindex\":629},\"Name\":\"Upcoming\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":628},{\"IsGroup\":false,\"Ordinal\":0,\"Join\":\"AND\",\"ItemPath\":\"_1_0\",\"Value\":\"DateTime.UtcNow.AddYears(1)\",\"Condition\":{\"FieldName\":\"EventStart\",\"FieldType\":\"System.DateTime\",\"Operator\":\"<=\",\"__msdisposeindex\":631},\"Name\":\"EventStart.DateTime.UtcNow.AddYears(1)\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":630},{\"IsGroup\":false,\"Ordinal\":1,\"Join\":\"AND\",\"ItemPath\":\"_1_1\",\"Value\":\"DateTime.UtcNow\",\"Condition\":{\"FieldName\":\"EventStart\",\"FieldType\":\"System.DateTime\",\"Operator\":\">=\",\"__msdisposeindex\":633},\"Name\":null,\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":632},{\"IsGroup\":true,\"Ordinal\":2,\"Join\":\"OR\",\"ItemPath\":\"_2\",\"Value\":null,\"Condition\":{\"FieldName\":null,\"FieldType\":null,\"Operator\":null,\"__msdisposeindex\":635},\"Name\":\"Past\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":634},{\"IsGroup\":false,\"Ordinal\":0,\"Join\":\"AND\",\"ItemPath\":\"_2_0\",\"Value\":\"DateTime.UtcNow.AddYears(-1)\",\"Condition\":{\"FieldName\":\"EventEnd\",\"FieldType\":\"System.DateTime\",\"Operator\":\">=\",\"__msdisposeindex\":637},\"Name\":\"EventEnd.DateTime.UtcNow.AddYears(-1)\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":636},{\"IsGroup\":false,\"Ordinal\":1,\"Join\":\"AND\",\"ItemPath\":\"_2_1\",\"Value\":\"DateTime.UtcNow\",\"Condition\":{\"FieldName\":\"EventEnd\",\"FieldType\":\"System.DateTime\",\"Operator\":\"<=\",\"__msdisposeindex\":639},\"Name\":null,\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":638}],\"TypeProperties\":[],\"_itemPathSeparator\":\"_\"}";

        #endregion

        protected class NarrowFilter
        {
            public string Group { get; set; }

            public string FieldName { get; set; }

            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
            public Dictionary<Guid, string> DataList { get; set; }

            public string FieldType { get; set; }
        }

        private const string JsonExceptionMessage = "[InvalidOperationException: Error during serialization or deserialization using the JSON JavaScriptSerializer. The length of the string exceeds the value set on the maxJsonLength property.]";
        private readonly Dictionary<string, CultureInfo> sitefinityLanguages = new Dictionary<string, CultureInfo>();

        private const string Caption = "Calendar";
        private const string PageNamePrefix = "EventsPage";

        private const string BaseEventTitle = "TestEvent";
        private const string BasePastEventTitle = "PastTestEvent";
        private const string BaseUpcomingEventTitle = "UpcomingTestEvent";
        private const string BaseDraftEventTitle = "DraftTestEvent";
        private const string BaseAllDayEventTitle = "AllDayTestEvent";
        private const string BaseRepeatEventTitle = "RepeatTestEvent";

        private const string Calendar1Title = "Calendar 1";
        private const string Calendar2Title = "Calendar 2";
        private const string Calendar3Title = "Calendar 3";

        private const string Tag1Title = "Tag 1";
        private const string Tag2Title = "Tag 2";
        private const string Tag3Title = "Tag 3";

        private const string Category1Title = "Category 1";
        private const string Category2Title = "Category 2";
        private const string Category3Title = "Category 3";
    }
}