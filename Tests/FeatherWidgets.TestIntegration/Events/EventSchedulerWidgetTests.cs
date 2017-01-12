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
            this.defaultCalendarId = ServerOperations.Events().GetDefaultCalendarId();

            this.ClearData();

            ServerOperations.Events().CreateEvent(EventSchedulerWidgetTests.BasePastEventTitle, string.Empty, false, DateTime.Now.AddDays(-2), DateTime.Now.AddDays(-1));
            ServerOperations.Events().CreateEvent(EventSchedulerWidgetTests.BaseUpcomingEventTitle, string.Empty, false, DateTime.Now.AddDays(1), DateTime.Now.AddDays(2));
            ServerOperations.Events().CreateEvent(EventSchedulerWidgetTests.BaseAllDayEventTitle, string.Empty, true, DateTime.Now, DateTime.Now.AddHours(1));
            ServerOperations.Events().CreateEvent(EventSchedulerWidgetTests.BasePastAllDayEventTitle, string.Empty, true, DateTime.Now.AddDays(-2), DateTime.Now.AddDays(-2).AddHours(1));
            ServerOperations.Events().CreateEvent(EventSchedulerWidgetTests.BaseUpcomingAllDayEventTitle, string.Empty, true, DateTime.Now.AddDays(2), DateTime.Now.AddDays(2).AddHours(1));
            ServerOperations.Events().CreateDailyRecurrentEvent(EventSchedulerWidgetTests.BaseRepeatEventTitle, string.Empty, DateTime.Now, DateTime.Now.AddHours(1), 60, 5, 1, TimeZoneInfo.Local.Id);
            ServerOperations.Events().CreateDailyRecurrentEvent(EventSchedulerWidgetTests.BaseUpcomingRepeatEventTitle, string.Empty, DateTime.Now.AddDays(2), DateTime.Now.AddDays(2).AddHours(1), 60, 2, 1, TimeZoneInfo.Local.Id);
            ServerOperations.Events().CreateDailyRecurrentEvent(EventSchedulerWidgetTests.BasePastRepeatEventTitle, string.Empty, DateTime.Now.AddDays(-2), DateTime.Now.AddDays(-2).AddHours(1), 60, 2, 1, TimeZoneInfo.Local.Id);
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
            this.ClearData();
        }

        #region Events

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.SitefinityTeam8)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events()
        {
            var filterList = new List<AdditionalFilter>() 
                {
                    new AdditionalFilter() 
                    {
                        Filter = DefaultFilterGroup,
                        FilterSection = FilterSection.Default
                    }
                };

            ServerOperations.Events().CreаteAllDayRecurrentEvent("RepeatEventTitle1", string.Empty, DateTime.Today, DateTime.Today, 60, 10000, 1, true);
            ServerOperations.Events().CreаteAllDayRecurrentEvent("RepeatEventTitle2", string.Empty, DateTime.Today, DateTime.Today, 60, 10000, 1, true);
            ServerOperations.Events().CreаteAllDayRecurrentEvent("RepeatEventTitle3", string.Empty, DateTime.Today, DateTime.Today, 60, 10000, 1, true);
            ServerOperations.Events().CreаteAllDayRecurrentEvent("RepeatEventTitle4", string.Empty, DateTime.Today, DateTime.Today, 60, 10000, 1, true);
            ServerOperations.Events().CreаteAllDayRecurrentEvent("RepeatEventTitle5", string.Empty, DateTime.Today, DateTime.Today, 60, 10000, 1, true);
            ServerOperations.Events().CreаteAllDayRecurrentEvent("RepeatEventTitle6", string.Empty, DateTime.Today, DateTime.Today, 60, 10000, 1, true);

            var widgetId = this.AddControlFilters(this.CreatePage(), SelectionMode.AllItems, filterList, SelectionMode.AllItems, DefaultNarrowSelectionFilters);

            this.AssertEvent(widgetId, DateTime.MinValue, DateTime.MaxValue, EventsManager.GetManager().GetEventsOccurrences(DateTime.MinValue, DateTime.MaxValue).Count());
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.SitefinityTeam8)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events_Upcoming()
        {
            var filterList = new List<AdditionalFilter>() 
                {
                    new AdditionalFilter() 
                    {
                        Filter = UpcomingFilterGroup,
                        FilterSection = FilterSection.Upcoming
                    }
                };

            var widgetId = this.AddControlFilters(this.CreatePage(), SelectionMode.FilteredItems, filterList, SelectionMode.AllItems, DefaultNarrowSelectionFilters);

            this.AssertEvent(widgetId, DateTime.MinValue, DateTime.MaxValue, 4);
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.SitefinityTeam8)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events_Past()
        {
            var filterList = new List<AdditionalFilter>() 
                {
                    new AdditionalFilter() 
                    {
                        Filter = PastFilterGroup,
                        FilterSection = FilterSection.Past
                    }
                };

            var widgetId = this.AddControlFilters(this.CreatePage(), SelectionMode.FilteredItems, filterList, SelectionMode.AllItems, DefaultNarrowSelectionFilters);

            this.AssertEvent(widgetId, DateTime.MinValue, DateTime.MaxValue, 4);
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.SitefinityTeam8)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events_Current()
        {
            var filterList = new List<AdditionalFilter>() 
            {
                new AdditionalFilter() 
                {
                    Filter = CurrentFilterGroup,
                    FilterSection = FilterSection.Current
                }
            };

            var widgetId = this.AddControlFilters(this.CreatePage(), SelectionMode.FilteredItems, filterList, SelectionMode.AllItems, DefaultNarrowSelectionFilters);

            this.AssertEvent(widgetId, DateTime.MinValue, DateTime.MaxValue, 6);
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.SitefinityTeam8)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events_UpcomingAndCurrent()
        {
            var filterList = new List<AdditionalFilter>() 
            {
                new AdditionalFilter() 
                {
                    Filter = CurrentFilterGroup,
                    FilterSection = FilterSection.Current
                },
                new AdditionalFilter() 
                {
                    Filter = UpcomingFilterGroup,
                    FilterSection = FilterSection.Upcoming
                }
            };

            var widgetId = this.AddControlFilters(this.CreatePage(), SelectionMode.FilteredItems, filterList, SelectionMode.AllItems, DefaultNarrowSelectionFilters);

            this.AssertEvent(widgetId, DateTime.MinValue, DateTime.MaxValue, 10);
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.SitefinityTeam8)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events_PastAndUpcoming()
        {
            var filterList = new List<AdditionalFilter>() 
            {
                new AdditionalFilter() 
                {
                    Filter = PastFilterGroup,
                    FilterSection = FilterSection.Past
                },
                new AdditionalFilter() 
                {
                    Filter = UpcomingFilterGroup,
                    FilterSection = FilterSection.Upcoming
                }
            };

            var widgetId = this.AddControlFilters(this.CreatePage(), SelectionMode.FilteredItems, filterList, SelectionMode.AllItems, DefaultNarrowSelectionFilters);

            this.AssertEvent(widgetId, DateTime.MinValue, DateTime.MaxValue, 8);
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.SitefinityTeam8)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events_PastAndCurrent()
        {
            var filterList = new List<AdditionalFilter>() 
            {
                new AdditionalFilter() 
                {
                    Filter = PastFilterGroup,
                    FilterSection = FilterSection.Past
                },
                new AdditionalFilter() 
                {
                    Filter = CurrentFilterGroup,
                    FilterSection = FilterSection.Current
                }
            };

            var widgetId = this.AddControlFilters(this.CreatePage(), SelectionMode.FilteredItems, filterList, SelectionMode.AllItems, DefaultNarrowSelectionFilters);

            this.AssertEvent(widgetId, DateTime.MinValue, DateTime.MaxValue, 10);
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.SitefinityTeam8)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events_UpcomingAndCurrentAndPast()
        {
            var filterList = new List<AdditionalFilter>() 
            {
                new AdditionalFilter() 
                {
                    Filter = UpcomingFilterGroup,
                    FilterSection = FilterSection.Upcoming
                },
                new AdditionalFilter() 
                {
                    Filter = CurrentFilterGroup,
                    FilterSection = FilterSection.Current
                },
                new AdditionalFilter() 
                {
                    Filter = PastFilterGroup,
                    FilterSection = FilterSection.Past
                }
            };

            var widgetId = this.AddControlFilters(this.CreatePage(), SelectionMode.FilteredItems, filterList, SelectionMode.AllItems, DefaultNarrowSelectionFilters);

            this.AssertEvent(widgetId, DateTime.MinValue, DateTime.MaxValue, 14);
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

            var filterList = new List<AdditionalFilter>() 
            {
                new AdditionalFilter() 
                {
                    Filter = DefaultFilterGroup,
                    FilterSection = FilterSection.Default
                }
            };

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

            var widgetId = this.AddControlFilters(this.CreatePage(bulgarian), SelectionMode.AllItems, filterList, SelectionMode.AllItems, DefaultNarrowSelectionFilters);
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

            var filterList = new List<AdditionalFilter>() 
            {
                new AdditionalFilter() 
                {
                    Filter = UpcomingFilterGroup,
                    FilterSection = FilterSection.Default
                }
            };

            var calendartDefault = manager.GetCalendars().FirstOrDefault();
            this.CreateLocalizedEvent(multiOperations, "Event 1 arabic", Guid.NewGuid(), DateTime.Now, DateTime.Now.AddHours(2), false, false, calendartDefault.Id, arabic);
            this.CreateLocalizedEvent(multiOperations, "Event 2 arabic", Guid.NewGuid(), DateTime.Now.AddDays(1), DateTime.Now.AddDays(3), false, false, calendartDefault.Id, arabic);

            var widgetId = this.AddControlFilters(this.CreatePage(arabic), SelectionMode.AllItems, filterList, SelectionMode.AllItems, DefaultNarrowSelectionFilters);
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
            var filterList = new List<AdditionalFilter>() 
            {
                new AdditionalFilter() 
                {
                    Filter = Upcoming3DaysFilterGroup,
                    FilterSection = FilterSection.Upcoming
                }
            };

            ServerOperations.Events().CreateEvent("UpcomingEventInNext3Days", string.Empty, false, DateTime.Now.AddDays(2), DateTime.Now.AddDays(3));
            ServerOperations.Events().CreateEvent("UpcomingEventInMoreThanNext3Days", string.Empty, false, DateTime.Now.AddDays(5), DateTime.Now.AddDays(7));

            var widgetId = this.AddControlFilters(this.CreatePage(), SelectionMode.FilteredItems, filterList, SelectionMode.AllItems, DefaultNarrowSelectionFilters);

            this.AssertEvent(widgetId, DateTime.MinValue, DateTime.MaxValue, 5);
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.SitefinityTeam8)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events_UpcomingNext3DaysOrCurrent()
        {
            var filterList = new List<AdditionalFilter>() 
            {
                new AdditionalFilter() 
                {
                    Filter = Upcoming3DaysFilterGroup,
                    FilterSection = FilterSection.Upcoming
                },
                new AdditionalFilter() 
                {
                    Filter = CurrentFilterGroup,
                    FilterSection = FilterSection.Current
                }
            };

            ServerOperations.Events().CreateEvent("UpcomingEventInNext3Days", string.Empty, false, DateTime.Now.AddDays(2), DateTime.Now.AddDays(3));
            ServerOperations.Events().CreateEvent("UpcomingEventInMoreThanNext3Days", string.Empty, false, DateTime.Now.AddDays(5), DateTime.Now.AddDays(7));

            var widgetId = this.AddControlFilters(this.CreatePage(), SelectionMode.FilteredItems, filterList, SelectionMode.AllItems, DefaultNarrowSelectionFilters);

            this.AssertEvent(widgetId, DateTime.MinValue, DateTime.MaxValue, 11);
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.SitefinityTeam8)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events_UpcomingNextWeek()
        {
            var filterList = new List<AdditionalFilter>() 
            {
                new AdditionalFilter() 
                {
                    Filter = UpcomingWeekFilterGroup,
                    FilterSection = FilterSection.Upcoming
                }
            };

            ServerOperations.Events().CreateEvent("UpcomingEventInNextWeek", string.Empty, false, DateTime.Now.AddDays(4), DateTime.Now.AddDays(6));
            ServerOperations.Events().CreateEvent("UpcomingEventInMoreThanNextWeek", string.Empty, false, DateTime.Now.AddDays(9), DateTime.Now.AddDays(12));

            var widgetId = this.AddControlFilters(this.CreatePage(), SelectionMode.FilteredItems, filterList, SelectionMode.AllItems, DefaultNarrowSelectionFilters);

            this.AssertEvent(widgetId, DateTime.MinValue, DateTime.MaxValue, 5);
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.SitefinityTeam8)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events_UpcomingNextWeekOrCurrent()
        {
            var filterList = new List<AdditionalFilter>() 
            {
                new AdditionalFilter() 
                {
                    Filter = UpcomingWeekFilterGroup,
                    FilterSection = FilterSection.Upcoming
                },
                new AdditionalFilter() 
                {
                    Filter = CurrentFilterGroup,
                    FilterSection = FilterSection.Current
                }
            };

            ServerOperations.Events().CreateEvent("UpcomingEventInNextWeek", string.Empty, false, DateTime.Now.AddDays(4), DateTime.Now.AddDays(6));
            ServerOperations.Events().CreateEvent("UpcomingEventInMoreThanNextWeek", string.Empty, false, DateTime.Now.AddDays(9), DateTime.Now.AddDays(12));

            var widgetId = this.AddControlFilters(this.CreatePage(), SelectionMode.FilteredItems, filterList, SelectionMode.AllItems, DefaultNarrowSelectionFilters);

            this.AssertEvent(widgetId, DateTime.MinValue, DateTime.MaxValue, 11);
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.SitefinityTeam8)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events_UpcomingNextMonth()
        {
            var filterList = new List<AdditionalFilter>() 
            {
                new AdditionalFilter() 
                {
                    Filter = UpcomingMonthFilterGroup,
                    FilterSection = FilterSection.Upcoming
                }
            };

            ServerOperations.Events().CreateEvent("UpcomingEventInNextMonth", string.Empty, false, DateTime.Now.AddDays(20), DateTime.Now.AddDays(22));
            ServerOperations.Events().CreateEvent("UpcomingEventInMoreThanNextMonth", string.Empty, false, DateTime.Now.AddDays(40), DateTime.Now.AddDays(42));

            var widgetId = this.AddControlFilters(this.CreatePage(), SelectionMode.FilteredItems, filterList, SelectionMode.AllItems, DefaultNarrowSelectionFilters);

            this.AssertEvent(widgetId, DateTime.MinValue, DateTime.MaxValue, 5);
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.SitefinityTeam8)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events_UpcomingNextMonthOrCurrent()
        {
            var filterList = new List<AdditionalFilter>() 
            {
                new AdditionalFilter() 
                {
                    Filter = UpcomingMonthFilterGroup,
                    FilterSection = FilterSection.Upcoming
                },
                new AdditionalFilter() 
                {
                    Filter = CurrentFilterGroup,
                    FilterSection = FilterSection.Current
                }
            };

            ServerOperations.Events().CreateEvent("UpcomingEventInNextMonth", string.Empty, false, DateTime.Now.AddDays(20), DateTime.Now.AddDays(22));
            ServerOperations.Events().CreateEvent("UpcomingEventInMoreThanNextMonth", string.Empty, false, DateTime.Now.AddDays(40), DateTime.Now.AddDays(42));

            var widgetId = this.AddControlFilters(this.CreatePage(), SelectionMode.FilteredItems, filterList, SelectionMode.AllItems, DefaultNarrowSelectionFilters);

            this.AssertEvent(widgetId, DateTime.MinValue, DateTime.MaxValue, 11);
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.SitefinityTeam8)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events_UpcomingNextYear()
        {
            var filterList = new List<AdditionalFilter>() 
            {
                new AdditionalFilter() 
                {
                    Filter = UpcomingYearFilterGroup,
                    FilterSection = FilterSection.Upcoming
                }
            };

            ServerOperations.Events().CreateEvent("UpcomingEventInNextYear", string.Empty, false, DateTime.Now.AddDays(365), DateTime.Now.AddDays(368));
            ServerOperations.Events().CreateEvent("UpcomingEventInMoreThanNextYear", string.Empty, false, DateTime.Now.AddDays(450), DateTime.Now.AddDays(453));

            var widgetId = this.AddControlFilters(this.CreatePage(), SelectionMode.FilteredItems, filterList, SelectionMode.AllItems, DefaultNarrowSelectionFilters);

            this.AssertEvent(widgetId, DateTime.MinValue, DateTime.MaxValue, 5);
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.SitefinityTeam8)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events_UpcomingNextYearOrCurrent()
        {
            var filterList = new List<AdditionalFilter>() 
            {
                new AdditionalFilter() 
                {
                    Filter = UpcomingYearFilterGroup,
                    FilterSection = FilterSection.Upcoming
                },
                new AdditionalFilter() 
                {
                    Filter = CurrentFilterGroup,
                    FilterSection = FilterSection.Current
                }
            };

            ServerOperations.Events().CreateEvent("UpcomingEventInNextYear", string.Empty, false, DateTime.Now.AddDays(365), DateTime.Now.AddDays(368));
            ServerOperations.Events().CreateEvent("UpcomingEventInMoreThanNextYear", string.Empty, false, DateTime.Now.AddDays(450), DateTime.Now.AddDays(453));

            var widgetId = this.AddControlFilters(this.CreatePage(), SelectionMode.FilteredItems, filterList, SelectionMode.AllItems, DefaultNarrowSelectionFilters);

            this.AssertEvent(widgetId, DateTime.MinValue, DateTime.MaxValue, 11);
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
            var filterList = new List<AdditionalFilter>() 
            {
                new AdditionalFilter() 
                {
                    Filter = Past3DaysFilterGroup,
                    FilterSection = FilterSection.Past
                }
            };

            ServerOperations.Events().CreateEvent("PastEventInLast3Days", string.Empty, false, DateTime.Now.AddDays(-2), DateTime.Now.AddDays(-1));
            ServerOperations.Events().CreateEvent("PastEventInMoreThanLast3Days", string.Empty, false, DateTime.Now.AddDays(-6), DateTime.Now.AddDays(-4));

            var widgetId = this.AddControlFilters(this.CreatePage(), SelectionMode.FilteredItems, filterList, SelectionMode.AllItems, DefaultNarrowSelectionFilters);

            this.AssertEvent(widgetId, DateTime.MinValue, DateTime.MaxValue, 5);
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.SitefinityTeam8)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events_PastLast3DaysOrCurrent()
        {
            var filterList = new List<AdditionalFilter>() 
            {
                new AdditionalFilter() 
                {
                    Filter = Past3DaysFilterGroup,
                    FilterSection = FilterSection.Past
                },
                new AdditionalFilter() 
                {
                    Filter = CurrentFilterGroup,
                    FilterSection = FilterSection.Current
                }
            };

            ServerOperations.Events().CreateEvent("PastEventInLast3Days", string.Empty, false, DateTime.Now.AddDays(-2), DateTime.Now.AddDays(-1));
            ServerOperations.Events().CreateEvent("PastEventInMoreThanLast3Days", string.Empty, false, DateTime.Now.AddDays(-6), DateTime.Now.AddDays(-4));

            var widgetId = this.AddControlFilters(this.CreatePage(), SelectionMode.FilteredItems, filterList, SelectionMode.AllItems, DefaultNarrowSelectionFilters);

            this.AssertEvent(widgetId, DateTime.MinValue, DateTime.MaxValue, 11);
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.SitefinityTeam8)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events_PastLastWeek()
        {
            var filterList = new List<AdditionalFilter>() 
            {
                new AdditionalFilter() 
                {
                    Filter = PastWeekFilterGroup,
                    FilterSection = FilterSection.Past
                }
            };

            ServerOperations.Events().CreateEvent("PastEventInLastWeek", string.Empty, false, DateTime.Now.AddDays(-6), DateTime.Now.AddDays(-4));
            ServerOperations.Events().CreateEvent("PastEventInMoreThanLastWeek", string.Empty, false, DateTime.Now.AddDays(-12), DateTime.Now.AddDays(-9));

            var widgetId = this.AddControlFilters(this.CreatePage(), SelectionMode.FilteredItems, filterList, SelectionMode.AllItems, DefaultNarrowSelectionFilters);

            this.AssertEvent(widgetId, DateTime.MinValue, DateTime.MaxValue, 5);
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.SitefinityTeam8)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events_PastLastWeekOrCurrent()
        {
            var filterList = new List<AdditionalFilter>() 
            {
                new AdditionalFilter() 
                {
                    Filter = PastWeekFilterGroup,
                    FilterSection = FilterSection.Past
                },
                new AdditionalFilter() 
                {
                    Filter = CurrentFilterGroup,
                    FilterSection = FilterSection.Current
                }
            };

            ServerOperations.Events().CreateEvent("PastEventInLastWeek", string.Empty, false, DateTime.Now.AddDays(-6), DateTime.Now.AddDays(-4));
            ServerOperations.Events().CreateEvent("PastEventInMoreThanLastWeek", string.Empty, false, DateTime.Now.AddDays(-12), DateTime.Now.AddDays(-9));

            var widgetId = this.AddControlFilters(this.CreatePage(), SelectionMode.FilteredItems, filterList, SelectionMode.AllItems, DefaultNarrowSelectionFilters);

            this.AssertEvent(widgetId, DateTime.MinValue, DateTime.MaxValue, 11);
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.SitefinityTeam8)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events_PastLastMonth()
        {
            var filterList = new List<AdditionalFilter>() 
            {
                new AdditionalFilter() 
                {
                    Filter = PastMonthFilterGroup,
                    FilterSection = FilterSection.Past
                }
            };

            ServerOperations.Events().CreateEvent("PastEventInLastMonth", string.Empty, false, DateTime.Now.AddDays(-22), DateTime.Now.AddDays(-20));
            ServerOperations.Events().CreateEvent("PastEventInMoreThanLastMonth", string.Empty, false, DateTime.Now.AddDays(-42), DateTime.Now.AddDays(-40));

            var widgetId = this.AddControlFilters(this.CreatePage(), SelectionMode.FilteredItems, filterList, SelectionMode.AllItems, DefaultNarrowSelectionFilters);

            this.AssertEvent(widgetId, DateTime.MinValue, DateTime.MaxValue, 5);
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.SitefinityTeam8)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events_PastLastMonthOrCurrent()
        {
            var filterList = new List<AdditionalFilter>() 
            {
                new AdditionalFilter() 
                {
                    Filter = PastMonthFilterGroup,
                    FilterSection = FilterSection.Past
                },
                new AdditionalFilter() 
                {
                    Filter = CurrentFilterGroup,
                    FilterSection = FilterSection.Current
                }
            };

            ServerOperations.Events().CreateEvent("PastEventInLastMonth", string.Empty, false, DateTime.Now.AddDays(-22), DateTime.Now.AddDays(-20));
            ServerOperations.Events().CreateEvent("PastEventInMoreThanLastMonth", string.Empty, false, DateTime.Now.AddDays(-42), DateTime.Now.AddDays(-40));

            var widgetId = this.AddControlFilters(this.CreatePage(), SelectionMode.FilteredItems, filterList, SelectionMode.AllItems, DefaultNarrowSelectionFilters);

            this.AssertEvent(widgetId, DateTime.MinValue, DateTime.MaxValue, 11);
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.SitefinityTeam8)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events_PastLastYear()
        {
            var filterList = new List<AdditionalFilter>() 
            {
                new AdditionalFilter() 
                {
                    Filter = PastYearFilterGroup,
                    FilterSection = FilterSection.Past
                }
            };

            ServerOperations.Events().CreateEvent("PastEventInLastYear", string.Empty, false, DateTime.Now.AddDays(-368), DateTime.Now.AddDays(-365));
            ServerOperations.Events().CreateEvent("PastEventInMoreThanLastYear", string.Empty, false, DateTime.Now.AddDays(-453), DateTime.Now.AddDays(-450));

            var widgetId = this.AddControlFilters(this.CreatePage(), SelectionMode.FilteredItems, filterList, SelectionMode.AllItems, DefaultNarrowSelectionFilters);

            this.AssertEvent(widgetId, DateTime.MinValue, DateTime.MaxValue, 5);
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.SitefinityTeam8)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events_PastLastYearOrCurrent()
        {
            var filterList = new List<AdditionalFilter>() 
            {
                new AdditionalFilter() 
                {
                    Filter = PastYearFilterGroup,
                    FilterSection = FilterSection.Past
                },
                new AdditionalFilter() 
                {
                    Filter = CurrentFilterGroup,
                    FilterSection = FilterSection.Current
                }
            };

            ServerOperations.Events().CreateEvent("PastEventInLastYear", string.Empty, false, DateTime.Now.AddDays(-368), DateTime.Now.AddDays(-365));
            ServerOperations.Events().CreateEvent("PastEventInMoreThanLastYear", string.Empty, false, DateTime.Now.AddDays(-453), DateTime.Now.AddDays(-450));

            var widgetId = this.AddControlFilters(this.CreatePage(), SelectionMode.FilteredItems, filterList, SelectionMode.AllItems, DefaultNarrowSelectionFilters);

            this.AssertEvent(widgetId, DateTime.MinValue, DateTime.MaxValue, 11);
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
            var filterList = new List<AdditionalFilter>() 
            {
                new AdditionalFilter() 
                {
                    Filter = Upcoming3DaysFilterGroup,
                    FilterSection = FilterSection.Upcoming
                },
                new AdditionalFilter() 
                {
                    Filter = Past3DaysFilterGroup,
                    FilterSection = FilterSection.Past
                }
            };

            ServerOperations.Events().CreateEvent("UpcomingEventInNext3Days", string.Empty, false, DateTime.Now.AddDays(2), DateTime.Now.AddDays(3));
            ServerOperations.Events().CreateEvent("UpcomingEventInMoreThanNext3Days", string.Empty, false, DateTime.Now.AddDays(5), DateTime.Now.AddDays(7));
            ServerOperations.Events().CreateEvent("PastEventInLast3Days", string.Empty, false, DateTime.Now.AddDays(-2), DateTime.Now.AddDays(-1));
            ServerOperations.Events().CreateEvent("PastEventInMoreThanLast3Days", string.Empty, false, DateTime.Now.AddDays(-6), DateTime.Now.AddDays(-4));

            var widgetId = this.AddControlFilters(this.CreatePage(), SelectionMode.FilteredItems, filterList, SelectionMode.AllItems, DefaultNarrowSelectionFilters);

            this.AssertEvent(widgetId, DateTime.MinValue, DateTime.MaxValue, 10);
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.SitefinityTeam8)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events_Combined_Next3DaysOrLast3DaysOrCurrent()
        {
            var filterList = new List<AdditionalFilter>() 
            {
                new AdditionalFilter() 
                {
                    Filter = Upcoming3DaysFilterGroup,
                    FilterSection = FilterSection.Upcoming
                },
                new AdditionalFilter() 
                {
                    Filter = Past3DaysFilterGroup,
                    FilterSection = FilterSection.Past
                },
                new AdditionalFilter() 
                {
                    Filter = CurrentFilterGroup,
                    FilterSection = FilterSection.Current
                }
            };

            ServerOperations.Events().CreateEvent("UpcomingEventInNext3Days", string.Empty, false, DateTime.Now.AddDays(2), DateTime.Now.AddDays(3));
            ServerOperations.Events().CreateEvent("UpcomingEventInMoreThanNext3Days", string.Empty, false, DateTime.Now.AddDays(5), DateTime.Now.AddDays(7));
            ServerOperations.Events().CreateEvent("PastEventInLast3Days", string.Empty, false, DateTime.Now.AddDays(-2), DateTime.Now.AddDays(-1));
            ServerOperations.Events().CreateEvent("PastEventInMoreThanLast3Days", string.Empty, false, DateTime.Now.AddDays(-6), DateTime.Now.AddDays(-4));

            var widgetId = this.AddControlFilters(this.CreatePage(), SelectionMode.FilteredItems, filterList, SelectionMode.AllItems, DefaultNarrowSelectionFilters);

            this.AssertEvent(widgetId, DateTime.MinValue, DateTime.MaxValue, 16);
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.SitefinityTeam8)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events_Combined_Next3DaysOrLastWeek()
        {
            var filterList = new List<AdditionalFilter>() 
            {
                new AdditionalFilter() 
                {
                    Filter = Upcoming3DaysFilterGroup,
                    FilterSection = FilterSection.Upcoming
                },
                new AdditionalFilter() 
                {
                    Filter = PastWeekFilterGroup,
                    FilterSection = FilterSection.Past
                }
            };

            ServerOperations.Events().CreateEvent("UpcomingEventInNext3Days", string.Empty, false, DateTime.Now.AddDays(2), DateTime.Now.AddDays(3));
            ServerOperations.Events().CreateEvent("UpcomingEventInMoreThanNext3Days", string.Empty, false, DateTime.Now.AddDays(5), DateTime.Now.AddDays(7));
            ServerOperations.Events().CreateEvent("PastEventInLastWeek", string.Empty, false, DateTime.Now.AddDays(-6), DateTime.Now.AddDays(-4));
            ServerOperations.Events().CreateEvent("PastEventInMoreThanLastWeek", string.Empty, false, DateTime.Now.AddDays(-12), DateTime.Now.AddDays(-9));

            var widgetId = this.AddControlFilters(this.CreatePage(), SelectionMode.FilteredItems, filterList, SelectionMode.AllItems, DefaultNarrowSelectionFilters);

            this.AssertEvent(widgetId, DateTime.MinValue, DateTime.MaxValue, 10);
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.SitefinityTeam8)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events_Combined_Next3DaysOrLastWeekOrCurrent()
        {
            var filterList = new List<AdditionalFilter>() 
            {
                new AdditionalFilter() 
                {
                    Filter = Upcoming3DaysFilterGroup,
                    FilterSection = FilterSection.Upcoming
                },
                new AdditionalFilter() 
                {
                    Filter = PastWeekFilterGroup,
                    FilterSection = FilterSection.Past
                },
                new AdditionalFilter() 
                {
                    Filter = CurrentFilterGroup,
                    FilterSection = FilterSection.Current
                }
            };

            ServerOperations.Events().CreateEvent("UpcomingEventInNext3Days", string.Empty, false, DateTime.Now.AddDays(2), DateTime.Now.AddDays(3));
            ServerOperations.Events().CreateEvent("UpcomingEventInMoreThanNext3Days", string.Empty, false, DateTime.Now.AddDays(5), DateTime.Now.AddDays(7));
            ServerOperations.Events().CreateEvent("PastEventInLastWeek", string.Empty, false, DateTime.Now.AddDays(-6), DateTime.Now.AddDays(-4));
            ServerOperations.Events().CreateEvent("PastEventInMoreThanLastWeek", string.Empty, false, DateTime.Now.AddDays(-12), DateTime.Now.AddDays(-9));

            var widgetId = this.AddControlFilters(this.CreatePage(), SelectionMode.FilteredItems, filterList, SelectionMode.AllItems, DefaultNarrowSelectionFilters);

            this.AssertEvent(widgetId, DateTime.MinValue, DateTime.MaxValue, 16);
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.SitefinityTeam8)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events_Combined_Next3DaysOrLastMonth()
        {
            var filterList = new List<AdditionalFilter>() 
            {
                new AdditionalFilter() 
                {
                    Filter = Upcoming3DaysFilterGroup,
                    FilterSection = FilterSection.Upcoming
                },
                new AdditionalFilter() 
                {
                    Filter = PastMonthFilterGroup,
                    FilterSection = FilterSection.Past
                }
            };

            ServerOperations.Events().CreateEvent("UpcomingEventInNext3Days", string.Empty, false, DateTime.Now.AddDays(2), DateTime.Now.AddDays(3));
            ServerOperations.Events().CreateEvent("UpcomingEventInMoreThanNext3Days", string.Empty, false, DateTime.Now.AddDays(5), DateTime.Now.AddDays(7));
            ServerOperations.Events().CreateEvent("PastEventInLastMonth", string.Empty, false, DateTime.Now.AddDays(-22), DateTime.Now.AddDays(-20));
            ServerOperations.Events().CreateEvent("PastEventInMoreThanLastMonth", string.Empty, false, DateTime.Now.AddDays(-42), DateTime.Now.AddDays(-40));

            var widgetId = this.AddControlFilters(this.CreatePage(), SelectionMode.FilteredItems, filterList, SelectionMode.AllItems, DefaultNarrowSelectionFilters);

            this.AssertEvent(widgetId, DateTime.MinValue, DateTime.MaxValue, 10);
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.SitefinityTeam8)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events_Combined_Next3DaysOrLastMonthOrCurrent()
        {
            var filterList = new List<AdditionalFilter>() 
            {
                new AdditionalFilter() 
                {
                    Filter = Upcoming3DaysFilterGroup,
                    FilterSection = FilterSection.Upcoming
                },
                new AdditionalFilter() 
                {
                    Filter = PastMonthFilterGroup,
                    FilterSection = FilterSection.Past
                },
                new AdditionalFilter() 
                {
                    Filter = CurrentFilterGroup,
                    FilterSection = FilterSection.Current
                }
            };

            ServerOperations.Events().CreateEvent("UpcomingEventInNext3Days", string.Empty, false, DateTime.Now.AddDays(2), DateTime.Now.AddDays(3));
            ServerOperations.Events().CreateEvent("UpcomingEventInMoreThanNext3Days", string.Empty, false, DateTime.Now.AddDays(5), DateTime.Now.AddDays(7));
            ServerOperations.Events().CreateEvent("PastEventInLastMonth", string.Empty, false, DateTime.Now.AddDays(-22), DateTime.Now.AddDays(-20));
            ServerOperations.Events().CreateEvent("PastEventInMoreThanLastMonth", string.Empty, false, DateTime.Now.AddDays(-42), DateTime.Now.AddDays(-40));

            var widgetId = this.AddControlFilters(this.CreatePage(), SelectionMode.FilteredItems, filterList, SelectionMode.AllItems, DefaultNarrowSelectionFilters);

            this.AssertEvent(widgetId, DateTime.MinValue, DateTime.MaxValue, 16);
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.SitefinityTeam8)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events_Combined_Next3DaysOrLastYear()
        {
            var filterList = new List<AdditionalFilter>() 
            {
                new AdditionalFilter() 
                {
                    Filter = Upcoming3DaysFilterGroup,
                    FilterSection = FilterSection.Upcoming
                },
                new AdditionalFilter() 
                {
                    Filter = PastYearFilterGroup,
                    FilterSection = FilterSection.Past
                }
            };

            ServerOperations.Events().CreateEvent("UpcomingEventInNext3Days", string.Empty, false, DateTime.Now.AddDays(2), DateTime.Now.AddDays(3));
            ServerOperations.Events().CreateEvent("UpcomingEventInMoreThanNext3Days", string.Empty, false, DateTime.Now.AddDays(5), DateTime.Now.AddDays(7));
            ServerOperations.Events().CreateEvent("PastEventInLastYear", string.Empty, false, DateTime.Now.AddDays(-368), DateTime.Now.AddDays(-365));
            ServerOperations.Events().CreateEvent("PastEventInMoreThanLastYear", string.Empty, false, DateTime.Now.AddDays(-453), DateTime.Now.AddDays(-450));

            var widgetId = this.AddControlFilters(this.CreatePage(), SelectionMode.FilteredItems, filterList, SelectionMode.AllItems, DefaultNarrowSelectionFilters);

            this.AssertEvent(widgetId, DateTime.MinValue, DateTime.MaxValue, 10);
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.SitefinityTeam8)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events_Combined_Next3DaysOrLastYearOrCurrent()
        {
            var filterList = new List<AdditionalFilter>() 
            {
                new AdditionalFilter() 
                {
                    Filter = Upcoming3DaysFilterGroup,
                    FilterSection = FilterSection.Upcoming
                },
                new AdditionalFilter() 
                {
                    Filter = PastYearFilterGroup,
                    FilterSection = FilterSection.Past
                },
                new AdditionalFilter() 
                {
                    Filter = CurrentFilterGroup,
                    FilterSection = FilterSection.Current
                }
            };

            ServerOperations.Events().CreateEvent("UpcomingEventInNext3Days", string.Empty, false, DateTime.Now.AddDays(2), DateTime.Now.AddDays(3));
            ServerOperations.Events().CreateEvent("UpcomingEventInMoreThanNext3Days", string.Empty, false, DateTime.Now.AddDays(5), DateTime.Now.AddDays(7));
            ServerOperations.Events().CreateEvent("PastEventInLastYear", string.Empty, false, DateTime.Now.AddDays(-368), DateTime.Now.AddDays(-365));
            ServerOperations.Events().CreateEvent("PastEventInMoreThanLastYear", string.Empty, false, DateTime.Now.AddDays(-453), DateTime.Now.AddDays(-450));

            var widgetId = this.AddControlFilters(this.CreatePage(), SelectionMode.FilteredItems, filterList, SelectionMode.AllItems, DefaultNarrowSelectionFilters);

            this.AssertEvent(widgetId, DateTime.MinValue, DateTime.MaxValue, 16);
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.SitefinityTeam8)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events_Combined_NextWeekOrLast3Days()
        {
            var filterList = new List<AdditionalFilter>() 
            {
                new AdditionalFilter() 
                {
                    Filter = UpcomingWeekFilterGroup,
                    FilterSection = FilterSection.Upcoming
                },
                new AdditionalFilter() 
                {
                    Filter = Past3DaysFilterGroup,
                    FilterSection = FilterSection.Past
                }
            };

            ServerOperations.Events().CreateEvent("UpcomingEventInNextWeek", string.Empty, false, DateTime.Now.AddDays(4), DateTime.Now.AddDays(6));
            ServerOperations.Events().CreateEvent("UpcomingEventInMoreThanNextWeek", string.Empty, false, DateTime.Now.AddDays(9), DateTime.Now.AddDays(12));
            ServerOperations.Events().CreateEvent("PastEventInLast3Days", string.Empty, false, DateTime.Now.AddDays(-2), DateTime.Now.AddDays(-1));
            ServerOperations.Events().CreateEvent("PastEventInMoreThanLast3Days", string.Empty, false, DateTime.Now.AddDays(-6), DateTime.Now.AddDays(-4));

            var widgetId = this.AddControlFilters(this.CreatePage(), SelectionMode.FilteredItems, filterList, SelectionMode.AllItems, DefaultNarrowSelectionFilters);

            this.AssertEvent(widgetId, DateTime.MinValue, DateTime.MaxValue, 10);
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.SitefinityTeam8)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events_Combined_NextWeekOrLast3DaysOrCurrent()
        {
            var filterList = new List<AdditionalFilter>() 
            {
                new AdditionalFilter() 
                {
                    Filter = UpcomingWeekFilterGroup,
                    FilterSection = FilterSection.Upcoming
                },
                new AdditionalFilter() 
                {
                    Filter = Past3DaysFilterGroup,
                    FilterSection = FilterSection.Past
                },
                new AdditionalFilter() 
                {
                    Filter = CurrentFilterGroup,
                    FilterSection = FilterSection.Current
                }
            };

            ServerOperations.Events().CreateEvent("UpcomingEventInNextWeek", string.Empty, false, DateTime.Now.AddDays(4), DateTime.Now.AddDays(6));
            ServerOperations.Events().CreateEvent("UpcomingEventInMoreThanNextWeek", string.Empty, false, DateTime.Now.AddDays(9), DateTime.Now.AddDays(12));
            ServerOperations.Events().CreateEvent("PastEventInLast3Days", string.Empty, false, DateTime.Now.AddDays(-2), DateTime.Now.AddDays(-1));
            ServerOperations.Events().CreateEvent("PastEventInMoreThanLast3Days", string.Empty, false, DateTime.Now.AddDays(-6), DateTime.Now.AddDays(-4));

            var widgetId = this.AddControlFilters(this.CreatePage(), SelectionMode.FilteredItems, filterList, SelectionMode.AllItems, DefaultNarrowSelectionFilters);

            this.AssertEvent(widgetId, DateTime.MinValue, DateTime.MaxValue, 16);
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.SitefinityTeam8)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events_Combined_NextWeekOrLastWeek()
        {
            var filterList = new List<AdditionalFilter>() 
            {
                new AdditionalFilter() 
                {
                    Filter = UpcomingWeekFilterGroup,
                    FilterSection = FilterSection.Upcoming
                },
                new AdditionalFilter() 
                {
                    Filter = PastWeekFilterGroup,
                    FilterSection = FilterSection.Past
                }
            };

            ServerOperations.Events().CreateEvent("UpcomingEventInNextWeek", string.Empty, false, DateTime.Now.AddDays(4), DateTime.Now.AddDays(6));
            ServerOperations.Events().CreateEvent("UpcomingEventInMoreThanNextWeek", string.Empty, false, DateTime.Now.AddDays(9), DateTime.Now.AddDays(12));
            ServerOperations.Events().CreateEvent("PastEventInLastWeek", string.Empty, false, DateTime.Now.AddDays(-6), DateTime.Now.AddDays(-4));
            ServerOperations.Events().CreateEvent("PastEventInMoreThanLastWeek", string.Empty, false, DateTime.Now.AddDays(-12), DateTime.Now.AddDays(-9));

            var widgetId = this.AddControlFilters(this.CreatePage(), SelectionMode.FilteredItems, filterList, SelectionMode.AllItems, DefaultNarrowSelectionFilters);

            this.AssertEvent(widgetId, DateTime.MinValue, DateTime.MaxValue, 10);
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.SitefinityTeam8)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events_Combined_NextWeekOrLastWeekOrCurrent()
        {
            var filterList = new List<AdditionalFilter>() 
            {
                new AdditionalFilter() 
                {
                    Filter = UpcomingWeekFilterGroup,
                    FilterSection = FilterSection.Upcoming
                },
                new AdditionalFilter() 
                {
                    Filter = PastWeekFilterGroup,
                    FilterSection = FilterSection.Past
                },
                new AdditionalFilter() 
                {
                    Filter = CurrentFilterGroup,
                    FilterSection = FilterSection.Current
                }
            };

            ServerOperations.Events().CreateEvent("UpcomingEventInNextWeek", string.Empty, false, DateTime.Now.AddDays(4), DateTime.Now.AddDays(6));
            ServerOperations.Events().CreateEvent("UpcomingEventInMoreThanNextWeek", string.Empty, false, DateTime.Now.AddDays(9), DateTime.Now.AddDays(12));
            ServerOperations.Events().CreateEvent("PastEventInLastWeek", string.Empty, false, DateTime.Now.AddDays(-6), DateTime.Now.AddDays(-4));
            ServerOperations.Events().CreateEvent("PastEventInMoreThanLastWeek", string.Empty, false, DateTime.Now.AddDays(-12), DateTime.Now.AddDays(-9));

            var widgetId = this.AddControlFilters(this.CreatePage(), SelectionMode.FilteredItems, filterList, SelectionMode.AllItems, DefaultNarrowSelectionFilters);

            this.AssertEvent(widgetId, DateTime.MinValue, DateTime.MaxValue, 16);
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.SitefinityTeam8)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events_Combined_NextWeekOrLastMonth()
        {
            var filterList = new List<AdditionalFilter>() 
            {
                new AdditionalFilter() 
                {
                    Filter = UpcomingWeekFilterGroup,
                    FilterSection = FilterSection.Upcoming
                },
                new AdditionalFilter() 
                {
                    Filter = PastMonthFilterGroup,
                    FilterSection = FilterSection.Past
                }
            };

            ServerOperations.Events().CreateEvent("UpcomingEventInNextWeek", string.Empty, false, DateTime.Now.AddDays(4), DateTime.Now.AddDays(6));
            ServerOperations.Events().CreateEvent("UpcomingEventInMoreThanNextWeek", string.Empty, false, DateTime.Now.AddDays(9), DateTime.Now.AddDays(12));
            ServerOperations.Events().CreateEvent("PastEventInLastMonth", string.Empty, false, DateTime.Now.AddDays(-22), DateTime.Now.AddDays(-20));
            ServerOperations.Events().CreateEvent("PastEventInMoreThanLastMonth", string.Empty, false, DateTime.Now.AddDays(-42), DateTime.Now.AddDays(-40));

            var widgetId = this.AddControlFilters(this.CreatePage(), SelectionMode.FilteredItems, filterList, SelectionMode.AllItems, DefaultNarrowSelectionFilters);

            this.AssertEvent(widgetId, DateTime.MinValue, DateTime.MaxValue, 10);
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.SitefinityTeam8)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events_Combined_NextWeekOrLastMonthOrCurrent()
        {
            var filterList = new List<AdditionalFilter>() 
            {
                new AdditionalFilter() 
                {
                    Filter = UpcomingWeekFilterGroup,
                    FilterSection = FilterSection.Upcoming
                },
                new AdditionalFilter() 
                {
                    Filter = PastMonthFilterGroup,
                    FilterSection = FilterSection.Past
                },
                new AdditionalFilter() 
                {
                    Filter = CurrentFilterGroup,
                    FilterSection = FilterSection.Current
                }
            };

            ServerOperations.Events().CreateEvent("UpcomingEventInNextWeek", string.Empty, false, DateTime.Now.AddDays(4), DateTime.Now.AddDays(6));
            ServerOperations.Events().CreateEvent("UpcomingEventInMoreThanNextWeek", string.Empty, false, DateTime.Now.AddDays(9), DateTime.Now.AddDays(12));
            ServerOperations.Events().CreateEvent("PastEventInLastMonth", string.Empty, false, DateTime.Now.AddDays(-22), DateTime.Now.AddDays(-20));
            ServerOperations.Events().CreateEvent("PastEventInMoreThanLastMonth", string.Empty, false, DateTime.Now.AddDays(-42), DateTime.Now.AddDays(-40));

            var widgetId = this.AddControlFilters(this.CreatePage(), SelectionMode.FilteredItems, filterList, SelectionMode.AllItems, DefaultNarrowSelectionFilters);

            this.AssertEvent(widgetId, DateTime.MinValue, DateTime.MaxValue, 16);
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.SitefinityTeam8)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events_Combined_NextWeekOrLastYear()
        {
            var filterList = new List<AdditionalFilter>() 
            {
                new AdditionalFilter() 
                {
                    Filter = UpcomingWeekFilterGroup,
                    FilterSection = FilterSection.Upcoming
                },
                new AdditionalFilter() 
                {
                    Filter = PastYearFilterGroup,
                    FilterSection = FilterSection.Past
                }
            };

            ServerOperations.Events().CreateEvent("UpcomingEventInNextWeek", string.Empty, false, DateTime.Now.AddDays(4), DateTime.Now.AddDays(6));
            ServerOperations.Events().CreateEvent("UpcomingEventInMoreThanNextWeek", string.Empty, false, DateTime.Now.AddDays(9), DateTime.Now.AddDays(12));
            ServerOperations.Events().CreateEvent("PastEventInLastYear", string.Empty, false, DateTime.Now.AddDays(-368), DateTime.Now.AddDays(-365));
            ServerOperations.Events().CreateEvent("PastEventInMoreThanLastYear", string.Empty, false, DateTime.Now.AddDays(-453), DateTime.Now.AddDays(-450));

            var widgetId = this.AddControlFilters(this.CreatePage(), SelectionMode.FilteredItems, filterList, SelectionMode.AllItems, DefaultNarrowSelectionFilters);

            this.AssertEvent(widgetId, DateTime.MinValue, DateTime.MaxValue, 10);
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.SitefinityTeam8)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events_Combined_NextWeekOrLastYearOrCurrent()
        {
            var filterList = new List<AdditionalFilter>() 
            {
                new AdditionalFilter() 
                {
                    Filter = UpcomingWeekFilterGroup,
                    FilterSection = FilterSection.Upcoming
                },
                new AdditionalFilter() 
                {
                    Filter = PastYearFilterGroup,
                    FilterSection = FilterSection.Past
                },
                new AdditionalFilter() 
                {
                    Filter = CurrentFilterGroup,
                    FilterSection = FilterSection.Current
                }
            };

            ServerOperations.Events().CreateEvent("UpcomingEventInNextWeek", string.Empty, false, DateTime.Now.AddDays(4), DateTime.Now.AddDays(6));
            ServerOperations.Events().CreateEvent("UpcomingEventInMoreThanNextWeek", string.Empty, false, DateTime.Now.AddDays(9), DateTime.Now.AddDays(12));
            ServerOperations.Events().CreateEvent("PastEventInLastYear", string.Empty, false, DateTime.Now.AddDays(-368), DateTime.Now.AddDays(-365));
            ServerOperations.Events().CreateEvent("PastEventInMoreThanLastYear", string.Empty, false, DateTime.Now.AddDays(-453), DateTime.Now.AddDays(-450));

            var widgetId = this.AddControlFilters(this.CreatePage(), SelectionMode.FilteredItems, filterList, SelectionMode.AllItems, DefaultNarrowSelectionFilters);

            this.AssertEvent(widgetId, DateTime.MinValue, DateTime.MaxValue, 16);
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.SitefinityTeam8)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events_Combined_NextMonthOrLast3Days()
        {
            var filterList = new List<AdditionalFilter>() 
            {
                new AdditionalFilter() 
                {
                    Filter = UpcomingMonthFilterGroup,
                    FilterSection = FilterSection.Upcoming
                },
                new AdditionalFilter() 
                {
                    Filter = Past3DaysFilterGroup,
                    FilterSection = FilterSection.Past
                }
            };

            ServerOperations.Events().CreateEvent("UpcomingEventInNextMonth", string.Empty, false, DateTime.Now.AddDays(20), DateTime.Now.AddDays(22));
            ServerOperations.Events().CreateEvent("UpcomingEventInMoreThanNextMonth", string.Empty, false, DateTime.Now.AddDays(40), DateTime.Now.AddDays(42));
            ServerOperations.Events().CreateEvent("PastEventInLast3Days", string.Empty, false, DateTime.Now.AddDays(-2), DateTime.Now.AddDays(-1));
            ServerOperations.Events().CreateEvent("PastEventInMoreThanLast3Days", string.Empty, false, DateTime.Now.AddDays(-6), DateTime.Now.AddDays(-4));

            var widgetId = this.AddControlFilters(this.CreatePage(), SelectionMode.FilteredItems, filterList, SelectionMode.AllItems, DefaultNarrowSelectionFilters);

            this.AssertEvent(widgetId, DateTime.MinValue, DateTime.MaxValue, 10);
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.SitefinityTeam8)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events_Combined_NextMonthOrLast3DaysOrCurrent()
        {
            var filterList = new List<AdditionalFilter>() 
            {
                new AdditionalFilter() 
                {
                    Filter = UpcomingMonthFilterGroup,
                    FilterSection = FilterSection.Upcoming
                },
                new AdditionalFilter() 
                {
                    Filter = Past3DaysFilterGroup,
                    FilterSection = FilterSection.Past
                },
                new AdditionalFilter() 
                {
                    Filter = CurrentFilterGroup,
                    FilterSection = FilterSection.Current
                }
            };

            ServerOperations.Events().CreateEvent("UpcomingEventInNextMonth", string.Empty, false, DateTime.Now.AddDays(20), DateTime.Now.AddDays(22));
            ServerOperations.Events().CreateEvent("UpcomingEventInMoreThanNextMonth", string.Empty, false, DateTime.Now.AddDays(40), DateTime.Now.AddDays(42));
            ServerOperations.Events().CreateEvent("PastEventInLast3Days", string.Empty, false, DateTime.Now.AddDays(-2), DateTime.Now.AddDays(-1));
            ServerOperations.Events().CreateEvent("PastEventInMoreThanLast3Days", string.Empty, false, DateTime.Now.AddDays(-6), DateTime.Now.AddDays(-4));

            var widgetId = this.AddControlFilters(this.CreatePage(), SelectionMode.FilteredItems, filterList, SelectionMode.AllItems, DefaultNarrowSelectionFilters);

            this.AssertEvent(widgetId, DateTime.MinValue, DateTime.MaxValue, 16);
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.SitefinityTeam8)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events_Combined_NextMonthOrLastWeek()
        {
            var filterList = new List<AdditionalFilter>() 
            {
                new AdditionalFilter() 
                {
                    Filter = UpcomingMonthFilterGroup,
                    FilterSection = FilterSection.Upcoming
                },
                new AdditionalFilter() 
                {
                    Filter = PastWeekFilterGroup,
                    FilterSection = FilterSection.Past
                }
            };

            ServerOperations.Events().CreateEvent("UpcomingEventInNextMonth", string.Empty, false, DateTime.Now.AddDays(20), DateTime.Now.AddDays(22));
            ServerOperations.Events().CreateEvent("UpcomingEventInMoreThanNextMonth", string.Empty, false, DateTime.Now.AddDays(40), DateTime.Now.AddDays(42));
            ServerOperations.Events().CreateEvent("PastEventInLastWeek", string.Empty, false, DateTime.Now.AddDays(-6), DateTime.Now.AddDays(-4));
            ServerOperations.Events().CreateEvent("PastEventInMoreThanLastWeek", string.Empty, false, DateTime.Now.AddDays(-12), DateTime.Now.AddDays(-9));

            var widgetId = this.AddControlFilters(this.CreatePage(), SelectionMode.FilteredItems, filterList, SelectionMode.AllItems, DefaultNarrowSelectionFilters);

            this.AssertEvent(widgetId, DateTime.MinValue, DateTime.MaxValue, 10);
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.SitefinityTeam8)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events_Combined_NextMonthOrLastWeekOrCurrent()
        {
            var filterList = new List<AdditionalFilter>() 
            {
                new AdditionalFilter() 
                {
                    Filter = UpcomingMonthFilterGroup,
                    FilterSection = FilterSection.Upcoming
                },
                new AdditionalFilter() 
                {
                    Filter = PastWeekFilterGroup,
                    FilterSection = FilterSection.Past
                },
                new AdditionalFilter() 
                {
                    Filter = CurrentFilterGroup,
                    FilterSection = FilterSection.Current
                }
            };

            ServerOperations.Events().CreateEvent("UpcomingEventInNextMonth", string.Empty, false, DateTime.Now.AddDays(20), DateTime.Now.AddDays(22));
            ServerOperations.Events().CreateEvent("UpcomingEventInMoreThanNextMonth", string.Empty, false, DateTime.Now.AddDays(40), DateTime.Now.AddDays(42));
            ServerOperations.Events().CreateEvent("PastEventInLastWeek", string.Empty, false, DateTime.Now.AddDays(-6), DateTime.Now.AddDays(-4));
            ServerOperations.Events().CreateEvent("PastEventInMoreThanLastWeek", string.Empty, false, DateTime.Now.AddDays(-12), DateTime.Now.AddDays(-9));

            var widgetId = this.AddControlFilters(this.CreatePage(), SelectionMode.FilteredItems, filterList, SelectionMode.AllItems, DefaultNarrowSelectionFilters);

            this.AssertEvent(widgetId, DateTime.MinValue, DateTime.MaxValue, 16);
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.SitefinityTeam8)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events_Combined_NextMonthOrLastMonth()
        {
            var filterList = new List<AdditionalFilter>() 
            {
                new AdditionalFilter() 
                {
                    Filter = UpcomingMonthFilterGroup,
                    FilterSection = FilterSection.Upcoming
                },
                new AdditionalFilter() 
                {
                    Filter = PastMonthFilterGroup,
                    FilterSection = FilterSection.Past
                }
            };

            ServerOperations.Events().CreateEvent("UpcomingEventInNextMonth", string.Empty, false, DateTime.Now.AddDays(20), DateTime.Now.AddDays(22));
            ServerOperations.Events().CreateEvent("UpcomingEventInMoreThanNextMonth", string.Empty, false, DateTime.Now.AddDays(40), DateTime.Now.AddDays(42));
            ServerOperations.Events().CreateEvent("PastEventInLastMonth", string.Empty, false, DateTime.Now.AddDays(-22), DateTime.Now.AddDays(-20));
            ServerOperations.Events().CreateEvent("PastEventInMoreThanLastMonth", string.Empty, false, DateTime.Now.AddDays(-42), DateTime.Now.AddDays(-40));

            var widgetId = this.AddControlFilters(this.CreatePage(), SelectionMode.FilteredItems, filterList, SelectionMode.AllItems, DefaultNarrowSelectionFilters);

            this.AssertEvent(widgetId, DateTime.MinValue, DateTime.MaxValue, 10);
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.SitefinityTeam8)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events_Combined_NextMonthOrLastMonthOrCurrent()
        {
            var filterList = new List<AdditionalFilter>() 
            {
                new AdditionalFilter() 
                {
                    Filter = UpcomingMonthFilterGroup,
                    FilterSection = FilterSection.Upcoming
                },
                new AdditionalFilter() 
                {
                    Filter = PastMonthFilterGroup,
                    FilterSection = FilterSection.Past
                },
                new AdditionalFilter() 
                {
                    Filter = CurrentFilterGroup,
                    FilterSection = FilterSection.Current
                }
            };

            ServerOperations.Events().CreateEvent("UpcomingEventInNextMonth", string.Empty, false, DateTime.Now.AddDays(20), DateTime.Now.AddDays(22));
            ServerOperations.Events().CreateEvent("UpcomingEventInMoreThanNextMonth", string.Empty, false, DateTime.Now.AddDays(40), DateTime.Now.AddDays(42));
            ServerOperations.Events().CreateEvent("PastEventInLastMonth", string.Empty, false, DateTime.Now.AddDays(-22), DateTime.Now.AddDays(-20));
            ServerOperations.Events().CreateEvent("PastEventInMoreThanLastMonth", string.Empty, false, DateTime.Now.AddDays(-42), DateTime.Now.AddDays(-40));

            var widgetId = this.AddControlFilters(this.CreatePage(), SelectionMode.FilteredItems, filterList, SelectionMode.AllItems, DefaultNarrowSelectionFilters);

            this.AssertEvent(widgetId, DateTime.MinValue, DateTime.MaxValue, 16);
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.SitefinityTeam8)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events_Combined_NextMonthOrLastYear()
        {
            var filterList = new List<AdditionalFilter>() 
            {
                new AdditionalFilter() 
                {
                    Filter = UpcomingMonthFilterGroup,
                    FilterSection = FilterSection.Upcoming
                },
                new AdditionalFilter() 
                {
                    Filter = PastYearFilterGroup,
                    FilterSection = FilterSection.Past
                }
            };

            ServerOperations.Events().CreateEvent("UpcomingEventInNextMonth", string.Empty, false, DateTime.Now.AddDays(20), DateTime.Now.AddDays(22));
            ServerOperations.Events().CreateEvent("UpcomingEventInMoreThanNextMonth", string.Empty, false, DateTime.Now.AddDays(40), DateTime.Now.AddDays(42));
            ServerOperations.Events().CreateEvent("PastEventInLastYear", string.Empty, false, DateTime.Now.AddDays(-368), DateTime.Now.AddDays(-365));
            ServerOperations.Events().CreateEvent("PastEventInMoreThanLastYear", string.Empty, false, DateTime.Now.AddDays(-453), DateTime.Now.AddDays(-450));

            var widgetId = this.AddControlFilters(this.CreatePage(), SelectionMode.FilteredItems, filterList, SelectionMode.AllItems, DefaultNarrowSelectionFilters);

            this.AssertEvent(widgetId, DateTime.MinValue, DateTime.MaxValue, 10);
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.SitefinityTeam8)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events_Combined_NextMonthOrLastYearOrCurrent()
        {
            var filterList = new List<AdditionalFilter>() 
            {
                new AdditionalFilter() 
                {
                    Filter = UpcomingMonthFilterGroup,
                    FilterSection = FilterSection.Upcoming
                },
                new AdditionalFilter() 
                {
                    Filter = PastYearFilterGroup,
                    FilterSection = FilterSection.Past
                },
                new AdditionalFilter() 
                {
                    Filter = CurrentFilterGroup,
                    FilterSection = FilterSection.Current
                }
            };

            ServerOperations.Events().CreateEvent("UpcomingEventInNextMonth", string.Empty, false, DateTime.Now.AddDays(20), DateTime.Now.AddDays(22));
            ServerOperations.Events().CreateEvent("UpcomingEventInMoreThanNextMonth", string.Empty, false, DateTime.Now.AddDays(40), DateTime.Now.AddDays(42));
            ServerOperations.Events().CreateEvent("PastEventInLastYear", string.Empty, false, DateTime.Now.AddDays(-368), DateTime.Now.AddDays(-365));
            ServerOperations.Events().CreateEvent("PastEventInMoreThanLastYear", string.Empty, false, DateTime.Now.AddDays(-453), DateTime.Now.AddDays(-450));

            var widgetId = this.AddControlFilters(this.CreatePage(), SelectionMode.FilteredItems, filterList, SelectionMode.AllItems, DefaultNarrowSelectionFilters);

            this.AssertEvent(widgetId, DateTime.MinValue, DateTime.MaxValue, 16);
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.SitefinityTeam8)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events_Combined_NextYearOrLast3Days()
        {
            var filterList = new List<AdditionalFilter>() 
            {
                new AdditionalFilter() 
                {
                    Filter = UpcomingYearFilterGroup,
                    FilterSection = FilterSection.Upcoming
                },
                new AdditionalFilter() 
                {
                    Filter = Past3DaysFilterGroup,
                    FilterSection = FilterSection.Past
                }
            };

            ServerOperations.Events().CreateEvent("UpcomingEventInNextYear", string.Empty, false, DateTime.Now.AddDays(365), DateTime.Now.AddDays(368));
            ServerOperations.Events().CreateEvent("UpcomingEventInMoreThanNextYear", string.Empty, false, DateTime.Now.AddDays(450), DateTime.Now.AddDays(453));
            ServerOperations.Events().CreateEvent("PastEventInLast3Days", string.Empty, false, DateTime.Now.AddDays(-2), DateTime.Now.AddDays(-1));
            ServerOperations.Events().CreateEvent("PastEventInMoreThanLast3Days", string.Empty, false, DateTime.Now.AddDays(-6), DateTime.Now.AddDays(-4));

            var widgetId = this.AddControlFilters(this.CreatePage(), SelectionMode.FilteredItems, filterList, SelectionMode.AllItems, DefaultNarrowSelectionFilters);

            this.AssertEvent(widgetId, DateTime.MinValue, DateTime.MaxValue, 10);
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.SitefinityTeam8)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events_Combined_NextYearOrLast3DaysOrCurrent()
        {
            var filterList = new List<AdditionalFilter>() 
            {
                new AdditionalFilter() 
                {
                    Filter = UpcomingYearFilterGroup,
                    FilterSection = FilterSection.Upcoming
                },
                new AdditionalFilter() 
                {
                    Filter = Past3DaysFilterGroup,
                    FilterSection = FilterSection.Past
                },
                new AdditionalFilter() 
                {
                    Filter = CurrentFilterGroup,
                    FilterSection = FilterSection.Current
                }
            };

            ServerOperations.Events().CreateEvent("UpcomingEventInNextYear", string.Empty, false, DateTime.Now.AddDays(365), DateTime.Now.AddDays(368));
            ServerOperations.Events().CreateEvent("UpcomingEventInMoreThanNextYear", string.Empty, false, DateTime.Now.AddDays(450), DateTime.Now.AddDays(453));
            ServerOperations.Events().CreateEvent("PastEventInLast3Days", string.Empty, false, DateTime.Now.AddDays(-2), DateTime.Now.AddDays(-1));
            ServerOperations.Events().CreateEvent("PastEventInMoreThanLast3Days", string.Empty, false, DateTime.Now.AddDays(-6), DateTime.Now.AddDays(-4));

            var widgetId = this.AddControlFilters(this.CreatePage(), SelectionMode.FilteredItems, filterList, SelectionMode.AllItems, DefaultNarrowSelectionFilters);

            this.AssertEvent(widgetId, DateTime.MinValue, DateTime.MaxValue, 16);
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.SitefinityTeam8)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events_Combined_NextYearOrLastWeek()
        {
            var filterList = new List<AdditionalFilter>() 
            {
                new AdditionalFilter() 
                {
                    Filter = UpcomingYearFilterGroup,
                    FilterSection = FilterSection.Upcoming
                },
                new AdditionalFilter() 
                {
                    Filter = PastWeekFilterGroup,
                    FilterSection = FilterSection.Past
                }
            };

            ServerOperations.Events().CreateEvent("UpcomingEventInNextYear", string.Empty, false, DateTime.Now.AddDays(365), DateTime.Now.AddDays(368));
            ServerOperations.Events().CreateEvent("UpcomingEventInMoreThanNextYear", string.Empty, false, DateTime.Now.AddDays(450), DateTime.Now.AddDays(453));
            ServerOperations.Events().CreateEvent("PastEventInLastWeek", string.Empty, false, DateTime.Now.AddDays(-6), DateTime.Now.AddDays(-4));
            ServerOperations.Events().CreateEvent("PastEventInMoreThanLastWeek", string.Empty, false, DateTime.Now.AddDays(-12), DateTime.Now.AddDays(-9));

            var widgetId = this.AddControlFilters(this.CreatePage(), SelectionMode.FilteredItems, filterList, SelectionMode.AllItems, DefaultNarrowSelectionFilters);

            this.AssertEvent(widgetId, DateTime.MinValue, DateTime.MaxValue, 10);
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.SitefinityTeam8)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events_Combined_NextYearOrLastWeekOrCurrent()
        {
            var filterList = new List<AdditionalFilter>() 
            {
                new AdditionalFilter() 
                {
                    Filter = UpcomingYearFilterGroup,
                    FilterSection = FilterSection.Upcoming
                },
                new AdditionalFilter() 
                {
                    Filter = PastWeekFilterGroup,
                    FilterSection = FilterSection.Past
                },
                new AdditionalFilter() 
                {
                    Filter = CurrentFilterGroup,
                    FilterSection = FilterSection.Current
                }
            };

            ServerOperations.Events().CreateEvent("UpcomingEventInNextYear", string.Empty, false, DateTime.Now.AddDays(365), DateTime.Now.AddDays(368));
            ServerOperations.Events().CreateEvent("UpcomingEventInMoreThanNextYear", string.Empty, false, DateTime.Now.AddDays(450), DateTime.Now.AddDays(453));
            ServerOperations.Events().CreateEvent("PastEventInLastWeek", string.Empty, false, DateTime.Now.AddDays(-6), DateTime.Now.AddDays(-4));
            ServerOperations.Events().CreateEvent("PastEventInMoreThanLastWeek", string.Empty, false, DateTime.Now.AddDays(-12), DateTime.Now.AddDays(-9));

            var widgetId = this.AddControlFilters(this.CreatePage(), SelectionMode.FilteredItems, filterList, SelectionMode.AllItems, DefaultNarrowSelectionFilters);

            this.AssertEvent(widgetId, DateTime.MinValue, DateTime.MaxValue, 16);
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.SitefinityTeam8)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events_Combined_NextYearOrLastMonth()
        {
            var filterList = new List<AdditionalFilter>() 
            {
                new AdditionalFilter() 
                {
                    Filter = UpcomingYearFilterGroup,
                    FilterSection = FilterSection.Upcoming
                },
                new AdditionalFilter() 
                {
                    Filter = PastMonthFilterGroup,
                    FilterSection = FilterSection.Past
                }
            };

            ServerOperations.Events().CreateEvent("UpcomingEventInNextYear", string.Empty, false, DateTime.Now.AddDays(365), DateTime.Now.AddDays(368));
            ServerOperations.Events().CreateEvent("UpcomingEventInMoreThanNextYear", string.Empty, false, DateTime.Now.AddDays(450), DateTime.Now.AddDays(453));
            ServerOperations.Events().CreateEvent("PastEventInLastMonth", string.Empty, false, DateTime.Now.AddDays(-22), DateTime.Now.AddDays(-20));
            ServerOperations.Events().CreateEvent("PastEventInMoreThanLastMonth", string.Empty, false, DateTime.Now.AddDays(-42), DateTime.Now.AddDays(-40));

            var widgetId = this.AddControlFilters(this.CreatePage(), SelectionMode.FilteredItems, filterList, SelectionMode.AllItems, DefaultNarrowSelectionFilters);

            this.AssertEvent(widgetId, DateTime.MinValue, DateTime.MaxValue, 10);
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.SitefinityTeam8)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events_Combined_NextYearOrLastMonthOrCurrent()
        {
            var filterList = new List<AdditionalFilter>() 
            {
                new AdditionalFilter() 
                {
                    Filter = UpcomingYearFilterGroup,
                    FilterSection = FilterSection.Upcoming
                },
                new AdditionalFilter() 
                {
                    Filter = PastMonthFilterGroup,
                    FilterSection = FilterSection.Past
                },
                new AdditionalFilter() 
                {
                    Filter = CurrentFilterGroup,
                    FilterSection = FilterSection.Current
                }
            };

            ServerOperations.Events().CreateEvent("UpcomingEventInNextYear", string.Empty, false, DateTime.Now.AddDays(365), DateTime.Now.AddDays(368));
            ServerOperations.Events().CreateEvent("UpcomingEventInMoreThanNextYear", string.Empty, false, DateTime.Now.AddDays(450), DateTime.Now.AddDays(453));
            ServerOperations.Events().CreateEvent("PastEventInLastMonth", string.Empty, false, DateTime.Now.AddDays(-22), DateTime.Now.AddDays(-20));
            ServerOperations.Events().CreateEvent("PastEventInMoreThanLastMonth", string.Empty, false, DateTime.Now.AddDays(-42), DateTime.Now.AddDays(-40));

            var widgetId = this.AddControlFilters(this.CreatePage(), SelectionMode.FilteredItems, filterList, SelectionMode.AllItems, DefaultNarrowSelectionFilters);

            this.AssertEvent(widgetId, DateTime.MinValue, DateTime.MaxValue, 16);
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.SitefinityTeam8)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events_Combined_NextYearOrLastYear()
        {
            var filterList = new List<AdditionalFilter>() 
            {
                new AdditionalFilter() 
                {
                    Filter = UpcomingYearFilterGroup,
                    FilterSection = FilterSection.Upcoming
                },
                new AdditionalFilter() 
                {
                    Filter = PastYearFilterGroup,
                    FilterSection = FilterSection.Past
                }
            };

            ServerOperations.Events().CreateEvent("UpcomingEventInNextYear", string.Empty, false, DateTime.Now.AddDays(365), DateTime.Now.AddDays(368));
            ServerOperations.Events().CreateEvent("UpcomingEventInMoreThanNextYear", string.Empty, false, DateTime.Now.AddDays(450), DateTime.Now.AddDays(453));
            ServerOperations.Events().CreateEvent("PastEventInLastYear", string.Empty, false, DateTime.Now.AddDays(-368), DateTime.Now.AddDays(-365));
            ServerOperations.Events().CreateEvent("PastEventInMoreThanLastYear", string.Empty, false, DateTime.Now.AddDays(-453), DateTime.Now.AddDays(-450));

            var widgetId = this.AddControlFilters(this.CreatePage(), SelectionMode.FilteredItems, filterList, SelectionMode.AllItems, DefaultNarrowSelectionFilters);

            this.AssertEvent(widgetId, DateTime.MinValue, DateTime.MaxValue, 10);
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.SitefinityTeam8)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events_Combined_NextYearOrLastYearOrCurrent()
        {
            var filterList = new List<AdditionalFilter>() 
            {
                new AdditionalFilter() 
                {
                    Filter = UpcomingYearFilterGroup,
                    FilterSection = FilterSection.Upcoming
                },
                new AdditionalFilter() 
                {
                    Filter = PastYearFilterGroup,
                    FilterSection = FilterSection.Past
                },
                new AdditionalFilter() 
                {
                    Filter = CurrentFilterGroup,
                    FilterSection = FilterSection.Current
                }
            };

            ServerOperations.Events().CreateEvent("UpcomingEventInNextYear", string.Empty, false, DateTime.Now.AddDays(365), DateTime.Now.AddDays(368));
            ServerOperations.Events().CreateEvent("UpcomingEventInMoreThanNextYear", string.Empty, false, DateTime.Now.AddDays(450), DateTime.Now.AddDays(453));
            ServerOperations.Events().CreateEvent("PastEventInLastYear", string.Empty, false, DateTime.Now.AddDays(-368), DateTime.Now.AddDays(-365));
            ServerOperations.Events().CreateEvent("PastEventInMoreThanLastYear", string.Empty, false, DateTime.Now.AddDays(-453), DateTime.Now.AddDays(-450));

            var widgetId = this.AddControlFilters(this.CreatePage(), SelectionMode.FilteredItems, filterList, SelectionMode.AllItems, DefaultNarrowSelectionFilters);

            this.AssertEvent(widgetId, DateTime.MinValue, DateTime.MaxValue, 16);
        }

        #endregion

        #region Specified combined events - Multilingual

        #endregion

        #region Events - Custom ranges

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.SitefinityTeam8)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events_CustomRange_Upcoming_FromDateAndToDate()
        {
            var filterList = new List<AdditionalFilter>() 
            {
                new AdditionalFilter() 
                {
                    Filter = string.Empty,
                    FilterSection = FilterSection.Upcoming,
                    StartDate = DateTime.UtcNow.AddDays(6),
                    EndDate = DateTime.UtcNow.AddDays(12)
                }
            };

            ServerOperations.Events().CreateEvent("UpcomingEventInNextCustomRange", string.Empty, false, DateTime.Now.AddDays(7), DateTime.Now.AddDays(8));
            ServerOperations.Events().CreateEvent("UpcomingEventInLessThanNextCustomRange", string.Empty, false, DateTime.Now.AddDays(3), DateTime.Now.AddDays(4));
            ServerOperations.Events().CreateEvent("UpcomingEventInMoreThanNextCustomRange", string.Empty, false, DateTime.Now.AddDays(15), DateTime.Now.AddDays(16));

            var widgetId = this.AddControlFilters(this.CreatePage(), SelectionMode.FilteredItems, filterList, SelectionMode.AllItems, DefaultNarrowSelectionFilters);

            this.AssertEvent(widgetId, DateTime.MinValue, DateTime.MaxValue, 1);
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.SitefinityTeam8)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events_CustomRange_Upcoming_FromDate()
        {
            var filterList = new List<AdditionalFilter>() 
            {
                new AdditionalFilter() 
                {
                    Filter = string.Empty,
                    FilterSection = FilterSection.Upcoming,
                    StartDate = DateTime.UtcNow.AddDays(6)
                }
            };

            ServerOperations.Events().CreateEvent("UpcomingEventInNextCustomRange", string.Empty, false, DateTime.Now.AddDays(7), DateTime.Now.AddDays(8));
            ServerOperations.Events().CreateEvent("UpcomingEventInLessThanNextCustomRange", string.Empty, false, DateTime.Now.AddDays(3), DateTime.Now.AddDays(4));
            ServerOperations.Events().CreateEvent("UpcomingEventInMoreThanNextCustomRange", string.Empty, false, DateTime.Now.AddDays(15), DateTime.Now.AddDays(16));

            var widgetId = this.AddControlFilters(this.CreatePage(), SelectionMode.FilteredItems, filterList, SelectionMode.AllItems, DefaultNarrowSelectionFilters);

            this.AssertEvent(widgetId, DateTime.MinValue, DateTime.MaxValue, 2);
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.SitefinityTeam8)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events_CustomRange_Upcoming_ToDate()
        {
            var filterList = new List<AdditionalFilter>() 
            {
                new AdditionalFilter() 
                {
                    Filter = string.Empty,
                    FilterSection = FilterSection.Upcoming,
                    EndDate = DateTime.UtcNow.AddDays(12)
                }
            };

            ServerOperations.Events().CreateEvent("UpcomingEventInNextCustomRange", string.Empty, false, DateTime.Now.AddDays(7), DateTime.Now.AddDays(8));
            ServerOperations.Events().CreateEvent("UpcomingEventInLessThanNextCustomRange", string.Empty, false, DateTime.Now.AddDays(3), DateTime.Now.AddDays(4));
            ServerOperations.Events().CreateEvent("UpcomingEventInMoreThanNextCustomRange", string.Empty, false, DateTime.Now.AddDays(15), DateTime.Now.AddDays(16));

            var widgetId = this.AddControlFilters(this.CreatePage(), SelectionMode.FilteredItems, filterList, SelectionMode.AllItems, DefaultNarrowSelectionFilters);

            this.AssertEvent(widgetId, DateTime.MinValue, DateTime.MaxValue, 6);
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.SitefinityTeam8)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events_CustomRange_Past_FromDate()
        {
            var filterList = new List<AdditionalFilter>() 
            {
                new AdditionalFilter() 
                {
                    Filter = string.Empty,
                    FilterSection = FilterSection.Past,
                    StartDate = DateTime.UtcNow.AddDays(-12)
                }
            };

            ServerOperations.Events().CreateEvent("PastEventInPastCustomRange", string.Empty, false, DateTime.Now.AddDays(-10), DateTime.Now.AddDays(-8));
            ServerOperations.Events().CreateEvent("PastEventInLessThanPastCustomRange", string.Empty, false, DateTime.Now.AddDays(-3), DateTime.Now.AddDays(-1));
            ServerOperations.Events().CreateEvent("PastEventInMoreThanPastCustomRange", string.Empty, false, DateTime.Now.AddDays(-18), DateTime.Now.AddDays(-16));

            var widgetId = this.AddControlFilters(this.CreatePage(), SelectionMode.FilteredItems, filterList, SelectionMode.AllItems, DefaultNarrowSelectionFilters);

            this.AssertEvent(widgetId, DateTime.MinValue, DateTime.MaxValue, 6);
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.SitefinityTeam8)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events_CustomRange_Past_ToDate()
        {
            var filterList = new List<AdditionalFilter>() 
            {
                new AdditionalFilter() 
                {
                    Filter = string.Empty,
                    FilterSection = FilterSection.Past,
                    EndDate = DateTime.UtcNow.AddDays(-6)
                }
            };

            ServerOperations.Events().CreateEvent("PastEventInPastCustomRange", string.Empty, false, DateTime.Now.AddDays(-10), DateTime.Now.AddDays(-8));
            ServerOperations.Events().CreateEvent("PastEventInLessThanPastCustomRange", string.Empty, false, DateTime.Now.AddDays(-3), DateTime.Now.AddDays(-1));
            ServerOperations.Events().CreateEvent("PastEventInMoreThanPastCustomRange", string.Empty, false, DateTime.Now.AddDays(-18), DateTime.Now.AddDays(-16));

            var widgetId = this.AddControlFilters(this.CreatePage(), SelectionMode.FilteredItems, filterList, SelectionMode.AllItems, DefaultNarrowSelectionFilters);

            this.AssertEvent(widgetId, DateTime.MinValue, DateTime.MaxValue, 2);
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.SitefinityTeam8)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events_CustomRange_Past_FromDateAndToDate()
        {
            var filterList = new List<AdditionalFilter>() 
            {
                new AdditionalFilter() 
                {
                    Filter = string.Empty,
                    FilterSection = FilterSection.Past,
                    StartDate = DateTime.UtcNow.AddDays(-12),
                    EndDate = DateTime.UtcNow.AddDays(-6)
                }
            };

            ServerOperations.Events().CreateEvent("PastEventInPastCustomRange", string.Empty, false, DateTime.Now.AddDays(-10), DateTime.Now.AddDays(-8));
            ServerOperations.Events().CreateEvent("PastEventInLessThanPastCustomRange", string.Empty, false, DateTime.Now.AddDays(-3), DateTime.Now.AddDays(-1));
            ServerOperations.Events().CreateEvent("PastEventInMoreThanPastCustomRange", string.Empty, false, DateTime.Now.AddDays(-18), DateTime.Now.AddDays(-16));

            var widgetId = this.AddControlFilters(this.CreatePage(), SelectionMode.FilteredItems, filterList, SelectionMode.AllItems, DefaultNarrowSelectionFilters);

            this.AssertEvent(widgetId, DateTime.MinValue, DateTime.MaxValue, 1);
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.SitefinityTeam8)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events_CustomRange_CurrentOrUpcoming_FromDateAndToDate()
        {
            var filterList = new List<AdditionalFilter>() 
            {
                new AdditionalFilter() 
                {
                    Filter = CurrentFilterGroup,
                    FilterSection = FilterSection.Current
                },
                new AdditionalFilter() 
                {
                    Filter = string.Empty,
                    FilterSection = FilterSection.Upcoming,
                    StartDate = DateTime.UtcNow.AddDays(6),
                    EndDate = DateTime.UtcNow.AddDays(12)
                }
            };

            ServerOperations.Events().CreateEvent("UpcomingEventInNextCustomRange", string.Empty, false, DateTime.Now.AddDays(7), DateTime.Now.AddDays(8));
            ServerOperations.Events().CreateEvent("UpcomingEventInLessThanNextCustomRange", string.Empty, false, DateTime.Now.AddDays(3), DateTime.Now.AddDays(4));
            ServerOperations.Events().CreateEvent("UpcomingEventInMoreThanNextCustomRange", string.Empty, false, DateTime.Now.AddDays(15), DateTime.Now.AddDays(16));

            var widgetId = this.AddControlFilters(this.CreatePage(), SelectionMode.FilteredItems, filterList, SelectionMode.AllItems, DefaultNarrowSelectionFilters);

            this.AssertEvent(widgetId, DateTime.MinValue, DateTime.MaxValue, 7);
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.SitefinityTeam8)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events_CustomRange_CurrentOrUpcoming_FromDate()
        {
            var filterList = new List<AdditionalFilter>() 
            {
                new AdditionalFilter() 
                {
                    Filter = CurrentFilterGroup,
                    FilterSection = FilterSection.Current
                },
                new AdditionalFilter() 
                {
                    Filter = string.Empty,
                    FilterSection = FilterSection.Upcoming,
                    StartDate = DateTime.UtcNow.AddDays(6)
                }
            };

            ServerOperations.Events().CreateEvent("UpcomingEventInNextCustomRange", string.Empty, false, DateTime.Now.AddDays(7), DateTime.Now.AddDays(8));
            ServerOperations.Events().CreateEvent("UpcomingEventInLessThanNextCustomRange", string.Empty, false, DateTime.Now.AddDays(3), DateTime.Now.AddDays(4));
            ServerOperations.Events().CreateEvent("UpcomingEventInMoreThanNextCustomRange", string.Empty, false, DateTime.Now.AddDays(15), DateTime.Now.AddDays(16));

            var widgetId = this.AddControlFilters(this.CreatePage(), SelectionMode.FilteredItems, filterList, SelectionMode.AllItems, DefaultNarrowSelectionFilters);

            this.AssertEvent(widgetId, DateTime.MinValue, DateTime.MaxValue, 8);
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.SitefinityTeam8)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events_CustomRange_CurrentOrUpcoming_ToDate()
        {
            var filterList = new List<AdditionalFilter>() 
            {
                new AdditionalFilter() 
                {
                    Filter = CurrentFilterGroup,
                    FilterSection = FilterSection.Current
                },
                new AdditionalFilter() 
                {
                    Filter = string.Empty,
                    FilterSection = FilterSection.Upcoming,
                    EndDate = DateTime.UtcNow.AddDays(12)
                }
            };

            ServerOperations.Events().CreateEvent("UpcomingEventInNextCustomRange", string.Empty, false, DateTime.Now.AddDays(7), DateTime.Now.AddDays(8));
            ServerOperations.Events().CreateEvent("UpcomingEventInLessThanNextCustomRange", string.Empty, false, DateTime.Now.AddDays(3), DateTime.Now.AddDays(4));
            ServerOperations.Events().CreateEvent("UpcomingEventInMoreThanNextCustomRange", string.Empty, false, DateTime.Now.AddDays(15), DateTime.Now.AddDays(16));

            var widgetId = this.AddControlFilters(this.CreatePage(), SelectionMode.FilteredItems, filterList, SelectionMode.AllItems, DefaultNarrowSelectionFilters);

            this.AssertEvent(widgetId, DateTime.MinValue, DateTime.MaxValue, 12);
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.SitefinityTeam8)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events_CustomRange_CurrentOrPast_FromDate()
        {
            var filterList = new List<AdditionalFilter>() 
            {
                new AdditionalFilter() 
                {
                    Filter = CurrentFilterGroup,
                    FilterSection = FilterSection.Current
                },
                new AdditionalFilter() 
                {
                    Filter = string.Empty,
                    FilterSection = FilterSection.Past,
                    StartDate = DateTime.UtcNow.AddDays(-12)
                }
            };

            ServerOperations.Events().CreateEvent("PastEventInPastCustomRange", string.Empty, false, DateTime.Now.AddDays(-10), DateTime.Now.AddDays(-8));
            ServerOperations.Events().CreateEvent("PastEventInLessThanPastCustomRange", string.Empty, false, DateTime.Now.AddDays(-3), DateTime.Now.AddDays(-1));
            ServerOperations.Events().CreateEvent("PastEventInMoreThanPastCustomRange", string.Empty, false, DateTime.Now.AddDays(-18), DateTime.Now.AddDays(-16));

            var widgetId = this.AddControlFilters(this.CreatePage(), SelectionMode.FilteredItems, filterList, SelectionMode.AllItems, DefaultNarrowSelectionFilters);

            this.AssertEvent(widgetId, DateTime.MinValue, DateTime.MaxValue, 12);
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.SitefinityTeam8)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events_CustomRange_CurrentOrPast_ToDate()
        {
            var filterList = new List<AdditionalFilter>() 
            {
                new AdditionalFilter() 
                {
                    Filter = CurrentFilterGroup,
                    FilterSection = FilterSection.Current
                },
                new AdditionalFilter() 
                {
                    Filter = string.Empty,
                    FilterSection = FilterSection.Past,
                    EndDate = DateTime.UtcNow.AddDays(-6)
                }
            };

            ServerOperations.Events().CreateEvent("PastEventInPastCustomRange", string.Empty, false, DateTime.Now.AddDays(-10), DateTime.Now.AddDays(-8));
            ServerOperations.Events().CreateEvent("PastEventInLessThanPastCustomRange", string.Empty, false, DateTime.Now.AddDays(-3), DateTime.Now.AddDays(-1));
            ServerOperations.Events().CreateEvent("PastEventInMoreThanPastCustomRange", string.Empty, false, DateTime.Now.AddDays(-18), DateTime.Now.AddDays(-16));

            var widgetId = this.AddControlFilters(this.CreatePage(), SelectionMode.FilteredItems, filterList, SelectionMode.AllItems, DefaultNarrowSelectionFilters);

            this.AssertEvent(widgetId, DateTime.MinValue, DateTime.MaxValue, 8);
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.SitefinityTeam8)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events_CustomRange_CurrentOrPast_FromDateAndToDate()
        {
            var filterList = new List<AdditionalFilter>() 
            {
                new AdditionalFilter() 
                {
                    Filter = CurrentFilterGroup,
                    FilterSection = FilterSection.Current
                },
                new AdditionalFilter() 
                {
                    Filter = string.Empty,
                    FilterSection = FilterSection.Past,
                    StartDate = DateTime.UtcNow.AddDays(-12),
                    EndDate = DateTime.UtcNow.AddDays(-6)
                }
            };

            ServerOperations.Events().CreateEvent("PastEventInPastCustomRange", string.Empty, false, DateTime.Now.AddDays(-10), DateTime.Now.AddDays(-8));
            ServerOperations.Events().CreateEvent("PastEventInLessThanPastCustomRange", string.Empty, false, DateTime.Now.AddDays(-3), DateTime.Now.AddDays(-1));
            ServerOperations.Events().CreateEvent("PastEventInMoreThanPastCustomRange", string.Empty, false, DateTime.Now.AddDays(-18), DateTime.Now.AddDays(-16));

            var widgetId = this.AddControlFilters(this.CreatePage(), SelectionMode.FilteredItems, filterList, SelectionMode.AllItems, DefaultNarrowSelectionFilters);

            this.AssertEvent(widgetId, DateTime.MinValue, DateTime.MaxValue, 7);
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.SitefinityTeam8)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events_CustomRange_UpcomingFromDateOrPastFromDate()
        {
            var filterList = new List<AdditionalFilter>() 
            {
                new AdditionalFilter() 
                {
                    Filter = string.Empty,
                    FilterSection = FilterSection.Upcoming,
                    StartDate = DateTime.UtcNow.AddDays(6)
                },
                new AdditionalFilter() 
                {
                    Filter = string.Empty,
                    FilterSection = FilterSection.Past,
                    StartDate = DateTime.UtcNow.AddDays(-12)
                }
            };

            ServerOperations.Events().CreateEvent("PastEventInPastCustomRange", string.Empty, false, DateTime.Now.AddDays(-10), DateTime.Now.AddDays(-8));
            ServerOperations.Events().CreateEvent("PastEventInLessThanPastCustomRange", string.Empty, false, DateTime.Now.AddDays(-3), DateTime.Now.AddDays(-1));
            ServerOperations.Events().CreateEvent("PastEventInMoreThanPastCustomRange", string.Empty, false, DateTime.Now.AddDays(-18), DateTime.Now.AddDays(-16));

            ServerOperations.Events().CreateEvent("UpcomingEventInNextCustomRange", string.Empty, false, DateTime.Now.AddDays(7), DateTime.Now.AddDays(8));
            ServerOperations.Events().CreateEvent("UpcomingEventInLessThanNextCustomRange", string.Empty, false, DateTime.Now.AddDays(3), DateTime.Now.AddDays(4));
            ServerOperations.Events().CreateEvent("UpcomingEventInMoreThanNextCustomRange", string.Empty, false, DateTime.Now.AddDays(15), DateTime.Now.AddDays(16));

            var widgetId = this.AddControlFilters(this.CreatePage(), SelectionMode.FilteredItems, filterList, SelectionMode.AllItems, DefaultNarrowSelectionFilters);

            this.AssertEvent(widgetId, DateTime.MinValue, DateTime.MaxValue, 8);
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.SitefinityTeam8)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events_CustomRange_UpcomingFromDateOrPastToDate()
        {
            var filterList = new List<AdditionalFilter>() 
            {
                new AdditionalFilter() 
                {
                    Filter = string.Empty,
                    FilterSection = FilterSection.Upcoming,
                    StartDate = DateTime.UtcNow.AddDays(6)
                },
                new AdditionalFilter() 
                {
                    Filter = string.Empty,
                    FilterSection = FilterSection.Past,
                    EndDate = DateTime.UtcNow.AddDays(-6)
                }
            };

            ServerOperations.Events().CreateEvent("PastEventInPastCustomRange", string.Empty, false, DateTime.Now.AddDays(-10), DateTime.Now.AddDays(-8));
            ServerOperations.Events().CreateEvent("PastEventInLessThanPastCustomRange", string.Empty, false, DateTime.Now.AddDays(-3), DateTime.Now.AddDays(-1));
            ServerOperations.Events().CreateEvent("PastEventInMoreThanPastCustomRange", string.Empty, false, DateTime.Now.AddDays(-18), DateTime.Now.AddDays(-16));

            ServerOperations.Events().CreateEvent("UpcomingEventInNextCustomRange", string.Empty, false, DateTime.Now.AddDays(7), DateTime.Now.AddDays(8));
            ServerOperations.Events().CreateEvent("UpcomingEventInLessThanNextCustomRange", string.Empty, false, DateTime.Now.AddDays(3), DateTime.Now.AddDays(4));
            ServerOperations.Events().CreateEvent("UpcomingEventInMoreThanNextCustomRange", string.Empty, false, DateTime.Now.AddDays(15), DateTime.Now.AddDays(16));

            var widgetId = this.AddControlFilters(this.CreatePage(), SelectionMode.FilteredItems, filterList, SelectionMode.AllItems, DefaultNarrowSelectionFilters);

            this.AssertEvent(widgetId, DateTime.MinValue, DateTime.MaxValue, 4);
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.SitefinityTeam8)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events_CustomRange_UpcomingFromDateOrPastFromDateToDate()
        {
            var filterList = new List<AdditionalFilter>() 
            {
                new AdditionalFilter() 
                {
                    Filter = string.Empty,
                    FilterSection = FilterSection.Upcoming,
                    StartDate = DateTime.UtcNow.AddDays(6)
                },
                new AdditionalFilter() 
                {
                    Filter = string.Empty,
                    FilterSection = FilterSection.Past,
                    StartDate = DateTime.UtcNow.AddDays(-12),
                    EndDate = DateTime.UtcNow.AddDays(-6)
                }
            };

            ServerOperations.Events().CreateEvent("PastEventInPastCustomRange", string.Empty, false, DateTime.Now.AddDays(-10), DateTime.Now.AddDays(-8));
            ServerOperations.Events().CreateEvent("PastEventInLessThanPastCustomRange", string.Empty, false, DateTime.Now.AddDays(-3), DateTime.Now.AddDays(-1));
            ServerOperations.Events().CreateEvent("PastEventInMoreThanPastCustomRange", string.Empty, false, DateTime.Now.AddDays(-18), DateTime.Now.AddDays(-16));

            ServerOperations.Events().CreateEvent("UpcomingEventInNextCustomRange", string.Empty, false, DateTime.Now.AddDays(7), DateTime.Now.AddDays(8));
            ServerOperations.Events().CreateEvent("UpcomingEventInLessThanNextCustomRange", string.Empty, false, DateTime.Now.AddDays(3), DateTime.Now.AddDays(4));
            ServerOperations.Events().CreateEvent("UpcomingEventInMoreThanNextCustomRange", string.Empty, false, DateTime.Now.AddDays(15), DateTime.Now.AddDays(16));

            var widgetId = this.AddControlFilters(this.CreatePage(), SelectionMode.FilteredItems, filterList, SelectionMode.AllItems, DefaultNarrowSelectionFilters);

            this.AssertEvent(widgetId, DateTime.MinValue, DateTime.MaxValue, 3);
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.SitefinityTeam8)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events_CustomRange_UpcomingFromDateOrLast3Days()
        {
            var filterList = new List<AdditionalFilter>() 
            {
                new AdditionalFilter() 
                {
                    Filter = string.Empty,
                    FilterSection = FilterSection.Upcoming,
                    StartDate = DateTime.UtcNow.AddDays(6)
                },
                new AdditionalFilter() 
                {
                    Filter = Past3DaysFilterGroup,
                    FilterSection = FilterSection.Past
                }
            };

            ServerOperations.Events().CreateEvent("PastEventInLast3Days", string.Empty, false, DateTime.Now.AddDays(-2), DateTime.Now.AddDays(-1));
            ServerOperations.Events().CreateEvent("PastEventInMoreThanLast3Days", string.Empty, false, DateTime.Now.AddDays(-6), DateTime.Now.AddDays(-4));

            ServerOperations.Events().CreateEvent("UpcomingEventInNextCustomRange", string.Empty, false, DateTime.Now.AddDays(7), DateTime.Now.AddDays(8));
            ServerOperations.Events().CreateEvent("UpcomingEventInLessThanNextCustomRange", string.Empty, false, DateTime.Now.AddDays(3), DateTime.Now.AddDays(4));
            ServerOperations.Events().CreateEvent("UpcomingEventInMoreThanNextCustomRange", string.Empty, false, DateTime.Now.AddDays(15), DateTime.Now.AddDays(16));

            var widgetId = this.AddControlFilters(this.CreatePage(), SelectionMode.FilteredItems, filterList, SelectionMode.AllItems, DefaultNarrowSelectionFilters);

            this.AssertEvent(widgetId, DateTime.MinValue, DateTime.MaxValue, 7);
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.SitefinityTeam8)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events_CustomRange_UpcomingFromDateOrLastWeek()
        {
            var filterList = new List<AdditionalFilter>() 
            {
                new AdditionalFilter() 
                {
                    Filter = string.Empty,
                    FilterSection = FilterSection.Upcoming,
                    StartDate = DateTime.UtcNow.AddDays(6)
                },
                new AdditionalFilter() 
                {
                    Filter = PastWeekFilterGroup,
                    FilterSection = FilterSection.Past
                }
            };

            ServerOperations.Events().CreateEvent("PastEventInLastWeek", string.Empty, false, DateTime.Now.AddDays(-6), DateTime.Now.AddDays(-4));
            ServerOperations.Events().CreateEvent("PastEventInMoreThanLastWeek", string.Empty, false, DateTime.Now.AddDays(-12), DateTime.Now.AddDays(-9));

            ServerOperations.Events().CreateEvent("UpcomingEventInNextCustomRange", string.Empty, false, DateTime.Now.AddDays(7), DateTime.Now.AddDays(8));
            ServerOperations.Events().CreateEvent("UpcomingEventInLessThanNextCustomRange", string.Empty, false, DateTime.Now.AddDays(3), DateTime.Now.AddDays(4));
            ServerOperations.Events().CreateEvent("UpcomingEventInMoreThanNextCustomRange", string.Empty, false, DateTime.Now.AddDays(15), DateTime.Now.AddDays(16));

            var widgetId = this.AddControlFilters(this.CreatePage(), SelectionMode.FilteredItems, filterList, SelectionMode.AllItems, DefaultNarrowSelectionFilters);

            this.AssertEvent(widgetId, DateTime.MinValue, DateTime.MaxValue, 7);
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.SitefinityTeam8)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events_CustomRange_UpcomingFromDateOrLastMonth()
        {
            var filterList = new List<AdditionalFilter>() 
            {
                new AdditionalFilter() 
                {
                    Filter = string.Empty,
                    FilterSection = FilterSection.Upcoming,
                    StartDate = DateTime.UtcNow.AddDays(6)
                },
                new AdditionalFilter() 
                {
                    Filter = PastMonthFilterGroup,
                    FilterSection = FilterSection.Past
                }
            };

            ServerOperations.Events().CreateEvent("PastEventInLastMonth", string.Empty, false, DateTime.Now.AddDays(-22), DateTime.Now.AddDays(-20));
            ServerOperations.Events().CreateEvent("PastEventInMoreThanLastMonth", string.Empty, false, DateTime.Now.AddDays(-42), DateTime.Now.AddDays(-40));

            ServerOperations.Events().CreateEvent("UpcomingEventInNextCustomRange", string.Empty, false, DateTime.Now.AddDays(7), DateTime.Now.AddDays(8));
            ServerOperations.Events().CreateEvent("UpcomingEventInLessThanNextCustomRange", string.Empty, false, DateTime.Now.AddDays(3), DateTime.Now.AddDays(4));
            ServerOperations.Events().CreateEvent("UpcomingEventInMoreThanNextCustomRange", string.Empty, false, DateTime.Now.AddDays(15), DateTime.Now.AddDays(16));

            var widgetId = this.AddControlFilters(this.CreatePage(), SelectionMode.FilteredItems, filterList, SelectionMode.AllItems, DefaultNarrowSelectionFilters);

            this.AssertEvent(widgetId, DateTime.MinValue, DateTime.MaxValue, 7);
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.SitefinityTeam8)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events_CustomRange_UpcomingFromDateOrLastYear()
        {
            var filterList = new List<AdditionalFilter>() 
            {
                new AdditionalFilter() 
                {
                    Filter = string.Empty,
                    FilterSection = FilterSection.Upcoming,
                    StartDate = DateTime.UtcNow.AddDays(6)
                },
                new AdditionalFilter() 
                {
                    Filter = PastYearFilterGroup,
                    FilterSection = FilterSection.Past
                }
            };

            ServerOperations.Events().CreateEvent("PastEventInLastYear", string.Empty, false, DateTime.Now.AddDays(-368), DateTime.Now.AddDays(-365));
            ServerOperations.Events().CreateEvent("PastEventInMoreThanLastYear", string.Empty, false, DateTime.Now.AddDays(-453), DateTime.Now.AddDays(-450));

            ServerOperations.Events().CreateEvent("UpcomingEventInNextCustomRange", string.Empty, false, DateTime.Now.AddDays(7), DateTime.Now.AddDays(8));
            ServerOperations.Events().CreateEvent("UpcomingEventInLessThanNextCustomRange", string.Empty, false, DateTime.Now.AddDays(3), DateTime.Now.AddDays(4));
            ServerOperations.Events().CreateEvent("UpcomingEventInMoreThanNextCustomRange", string.Empty, false, DateTime.Now.AddDays(15), DateTime.Now.AddDays(16));

            var widgetId = this.AddControlFilters(this.CreatePage(), SelectionMode.FilteredItems, filterList, SelectionMode.AllItems, DefaultNarrowSelectionFilters);

            this.AssertEvent(widgetId, DateTime.MinValue, DateTime.MaxValue, 7);
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.SitefinityTeam8)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events_CustomRange_UpcomingToDateOrPastFromDate()
        {
            var filterList = new List<AdditionalFilter>() 
            {
                new AdditionalFilter() 
                {
                    Filter = string.Empty,
                    FilterSection = FilterSection.Upcoming,
                    EndDate = DateTime.UtcNow.AddDays(12)
                },
                new AdditionalFilter() 
                {
                    Filter = string.Empty,
                    FilterSection = FilterSection.Past,
                    StartDate = DateTime.UtcNow.AddDays(-12)
                }
            };

            ServerOperations.Events().CreateEvent("PastEventInPastCustomRange", string.Empty, false, DateTime.Now.AddDays(-10), DateTime.Now.AddDays(-8));
            ServerOperations.Events().CreateEvent("PastEventInLessThanPastCustomRange", string.Empty, false, DateTime.Now.AddDays(-3), DateTime.Now.AddDays(-1));
            ServerOperations.Events().CreateEvent("PastEventInMoreThanPastCustomRange", string.Empty, false, DateTime.Now.AddDays(-18), DateTime.Now.AddDays(-16));

            ServerOperations.Events().CreateEvent("UpcomingEventInNextCustomRange", string.Empty, false, DateTime.Now.AddDays(7), DateTime.Now.AddDays(8));
            ServerOperations.Events().CreateEvent("UpcomingEventInLessThanNextCustomRange", string.Empty, false, DateTime.Now.AddDays(3), DateTime.Now.AddDays(4));
            ServerOperations.Events().CreateEvent("UpcomingEventInMoreThanNextCustomRange", string.Empty, false, DateTime.Now.AddDays(15), DateTime.Now.AddDays(16));

            var widgetId = this.AddControlFilters(this.CreatePage(), SelectionMode.FilteredItems, filterList, SelectionMode.AllItems, DefaultNarrowSelectionFilters);

            this.AssertEvent(widgetId, DateTime.MinValue, DateTime.MaxValue, 12);
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.SitefinityTeam8)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events_CustomRange_UpcomingToDateOrPastToDate()
        {
            var filterList = new List<AdditionalFilter>() 
            {
                new AdditionalFilter() 
                {
                    Filter = string.Empty,
                    FilterSection = FilterSection.Upcoming,
                    EndDate = DateTime.UtcNow.AddDays(12)
                },
                new AdditionalFilter() 
                {
                    Filter = string.Empty,
                    FilterSection = FilterSection.Past,
                    EndDate = DateTime.UtcNow.AddDays(-6)
                }
            };

            ServerOperations.Events().CreateEvent("PastEventInPastCustomRange", string.Empty, false, DateTime.Now.AddDays(-10), DateTime.Now.AddDays(-8));
            ServerOperations.Events().CreateEvent("PastEventInLessThanPastCustomRange", string.Empty, false, DateTime.Now.AddDays(-3), DateTime.Now.AddDays(-1));
            ServerOperations.Events().CreateEvent("PastEventInMoreThanPastCustomRange", string.Empty, false, DateTime.Now.AddDays(-18), DateTime.Now.AddDays(-16));

            ServerOperations.Events().CreateEvent("UpcomingEventInNextCustomRange", string.Empty, false, DateTime.Now.AddDays(7), DateTime.Now.AddDays(8));
            ServerOperations.Events().CreateEvent("UpcomingEventInLessThanNextCustomRange", string.Empty, false, DateTime.Now.AddDays(3), DateTime.Now.AddDays(4));
            ServerOperations.Events().CreateEvent("UpcomingEventInMoreThanNextCustomRange", string.Empty, false, DateTime.Now.AddDays(15), DateTime.Now.AddDays(16));

            var widgetId = this.AddControlFilters(this.CreatePage(), SelectionMode.FilteredItems, filterList, SelectionMode.AllItems, DefaultNarrowSelectionFilters);

            this.AssertEvent(widgetId, DateTime.MinValue, DateTime.MaxValue, 8);
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.SitefinityTeam8)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events_CustomRange_UpcomingToDateOrPastFromDateToDate()
        {
            var filterList = new List<AdditionalFilter>() 
            {
                new AdditionalFilter() 
                {
                    Filter = string.Empty,
                    FilterSection = FilterSection.Upcoming,
                    EndDate = DateTime.UtcNow.AddDays(12)
                },
                new AdditionalFilter() 
                {
                    Filter = string.Empty,
                    FilterSection = FilterSection.Past,
                    StartDate = DateTime.UtcNow.AddDays(-12),
                    EndDate = DateTime.UtcNow.AddDays(-6)
                }
            };

            ServerOperations.Events().CreateEvent("PastEventInPastCustomRange", string.Empty, false, DateTime.Now.AddDays(-10), DateTime.Now.AddDays(-8));
            ServerOperations.Events().CreateEvent("PastEventInLessThanPastCustomRange", string.Empty, false, DateTime.Now.AddDays(-3), DateTime.Now.AddDays(-1));
            ServerOperations.Events().CreateEvent("PastEventInMoreThanPastCustomRange", string.Empty, false, DateTime.Now.AddDays(-18), DateTime.Now.AddDays(-16));

            ServerOperations.Events().CreateEvent("UpcomingEventInNextCustomRange", string.Empty, false, DateTime.Now.AddDays(7), DateTime.Now.AddDays(8));
            ServerOperations.Events().CreateEvent("UpcomingEventInLessThanNextCustomRange", string.Empty, false, DateTime.Now.AddDays(3), DateTime.Now.AddDays(4));
            ServerOperations.Events().CreateEvent("UpcomingEventInMoreThanNextCustomRange", string.Empty, false, DateTime.Now.AddDays(15), DateTime.Now.AddDays(16));

            var widgetId = this.AddControlFilters(this.CreatePage(), SelectionMode.FilteredItems, filterList, SelectionMode.AllItems, DefaultNarrowSelectionFilters);

            this.AssertEvent(widgetId, DateTime.MinValue, DateTime.MaxValue, 7);
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.SitefinityTeam8)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events_CustomRange_UpcomingToDateOrLast3Days()
        {
            var filterList = new List<AdditionalFilter>() 
            {
                new AdditionalFilter() 
                {
                    Filter = string.Empty,
                    FilterSection = FilterSection.Upcoming,
                    EndDate = DateTime.UtcNow.AddDays(12)
                },
                new AdditionalFilter() 
                {
                    Filter = Past3DaysFilterGroup,
                    FilterSection = FilterSection.Past
                }
            };

            ServerOperations.Events().CreateEvent("PastEventInLast3Days", string.Empty, false, DateTime.Now.AddDays(-2), DateTime.Now.AddDays(-1));
            ServerOperations.Events().CreateEvent("PastEventInMoreThanLast3Days", string.Empty, false, DateTime.Now.AddDays(-6), DateTime.Now.AddDays(-4));

            ServerOperations.Events().CreateEvent("UpcomingEventInNextCustomRange", string.Empty, false, DateTime.Now.AddDays(7), DateTime.Now.AddDays(8));
            ServerOperations.Events().CreateEvent("UpcomingEventInLessThanNextCustomRange", string.Empty, false, DateTime.Now.AddDays(3), DateTime.Now.AddDays(4));
            ServerOperations.Events().CreateEvent("UpcomingEventInMoreThanNextCustomRange", string.Empty, false, DateTime.Now.AddDays(15), DateTime.Now.AddDays(16));

            var widgetId = this.AddControlFilters(this.CreatePage(), SelectionMode.FilteredItems, filterList, SelectionMode.AllItems, DefaultNarrowSelectionFilters);

            this.AssertEvent(widgetId, DateTime.MinValue, DateTime.MaxValue, 11);
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.SitefinityTeam8)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events_CustomRange_UpcomingToDateOrLastWeek()
        {
            var filterList = new List<AdditionalFilter>() 
            {
                new AdditionalFilter() 
                {
                    Filter = string.Empty,
                    FilterSection = FilterSection.Upcoming,
                    EndDate = DateTime.UtcNow.AddDays(12)
                },
                new AdditionalFilter() 
                {
                    Filter = PastWeekFilterGroup,
                    FilterSection = FilterSection.Past
                }
            };

            ServerOperations.Events().CreateEvent("PastEventInLastWeek", string.Empty, false, DateTime.Now.AddDays(-6), DateTime.Now.AddDays(-4));
            ServerOperations.Events().CreateEvent("PastEventInMoreThanLastWeek", string.Empty, false, DateTime.Now.AddDays(-12), DateTime.Now.AddDays(-9));

            ServerOperations.Events().CreateEvent("UpcomingEventInNextCustomRange", string.Empty, false, DateTime.Now.AddDays(7), DateTime.Now.AddDays(8));
            ServerOperations.Events().CreateEvent("UpcomingEventInLessThanNextCustomRange", string.Empty, false, DateTime.Now.AddDays(3), DateTime.Now.AddDays(4));
            ServerOperations.Events().CreateEvent("UpcomingEventInMoreThanNextCustomRange", string.Empty, false, DateTime.Now.AddDays(15), DateTime.Now.AddDays(16));

            var widgetId = this.AddControlFilters(this.CreatePage(), SelectionMode.FilteredItems, filterList, SelectionMode.AllItems, DefaultNarrowSelectionFilters);

            this.AssertEvent(widgetId, DateTime.MinValue, DateTime.MaxValue, 11);
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.SitefinityTeam8)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events_CustomRange_UpcomingToDateOrLastMonth()
        {
            var filterList = new List<AdditionalFilter>() 
            {
                new AdditionalFilter() 
                {
                    Filter = string.Empty,
                    FilterSection = FilterSection.Upcoming,
                    EndDate = DateTime.UtcNow.AddDays(12)
                },
                new AdditionalFilter() 
                {
                    Filter = PastMonthFilterGroup,
                    FilterSection = FilterSection.Past
                }
            };

            ServerOperations.Events().CreateEvent("PastEventInLastMonth", string.Empty, false, DateTime.Now.AddDays(-22), DateTime.Now.AddDays(-20));
            ServerOperations.Events().CreateEvent("PastEventInMoreThanLastMonth", string.Empty, false, DateTime.Now.AddDays(-42), DateTime.Now.AddDays(-40));

            ServerOperations.Events().CreateEvent("UpcomingEventInNextCustomRange", string.Empty, false, DateTime.Now.AddDays(7), DateTime.Now.AddDays(8));
            ServerOperations.Events().CreateEvent("UpcomingEventInLessThanNextCustomRange", string.Empty, false, DateTime.Now.AddDays(3), DateTime.Now.AddDays(4));
            ServerOperations.Events().CreateEvent("UpcomingEventInMoreThanNextCustomRange", string.Empty, false, DateTime.Now.AddDays(15), DateTime.Now.AddDays(16));

            var widgetId = this.AddControlFilters(this.CreatePage(), SelectionMode.FilteredItems, filterList, SelectionMode.AllItems, DefaultNarrowSelectionFilters);

            this.AssertEvent(widgetId, DateTime.MinValue, DateTime.MaxValue, 11);
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.SitefinityTeam8)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events_CustomRange_UpcomingToDateOrLastYear()
        {
            var filterList = new List<AdditionalFilter>() 
            {
                new AdditionalFilter() 
                {
                    Filter = string.Empty,
                    FilterSection = FilterSection.Upcoming,
                    EndDate = DateTime.UtcNow.AddDays(12)
                },
                new AdditionalFilter() 
                {
                    Filter = PastYearFilterGroup,
                    FilterSection = FilterSection.Past
                }
            };

            ServerOperations.Events().CreateEvent("PastEventInLastYear", string.Empty, false, DateTime.Now.AddDays(-368), DateTime.Now.AddDays(-365));
            ServerOperations.Events().CreateEvent("PastEventInMoreThanLastYear", string.Empty, false, DateTime.Now.AddDays(-453), DateTime.Now.AddDays(-450));

            ServerOperations.Events().CreateEvent("UpcomingEventInNextCustomRange", string.Empty, false, DateTime.Now.AddDays(7), DateTime.Now.AddDays(8));
            ServerOperations.Events().CreateEvent("UpcomingEventInLessThanNextCustomRange", string.Empty, false, DateTime.Now.AddDays(3), DateTime.Now.AddDays(4));
            ServerOperations.Events().CreateEvent("UpcomingEventInMoreThanNextCustomRange", string.Empty, false, DateTime.Now.AddDays(15), DateTime.Now.AddDays(16));

            var widgetId = this.AddControlFilters(this.CreatePage(), SelectionMode.FilteredItems, filterList, SelectionMode.AllItems, DefaultNarrowSelectionFilters);

            this.AssertEvent(widgetId, DateTime.MinValue, DateTime.MaxValue, 11);
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.SitefinityTeam8)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events_CustomRange_UpcomingFromDateToDateOrPastFromDate()
        {
            var filterList = new List<AdditionalFilter>() 
            {
                new AdditionalFilter() 
                {
                    Filter = string.Empty,
                    FilterSection = FilterSection.Upcoming,
                    StartDate = DateTime.UtcNow.AddDays(6),
                    EndDate = DateTime.UtcNow.AddDays(12)
                },
                new AdditionalFilter() 
                {
                    Filter = string.Empty,
                    FilterSection = FilterSection.Past,
                    StartDate = DateTime.UtcNow.AddDays(-12)
                }
            };

            ServerOperations.Events().CreateEvent("PastEventInPastCustomRange", string.Empty, false, DateTime.Now.AddDays(-10), DateTime.Now.AddDays(-8));
            ServerOperations.Events().CreateEvent("PastEventInLessThanPastCustomRange", string.Empty, false, DateTime.Now.AddDays(-3), DateTime.Now.AddDays(-1));
            ServerOperations.Events().CreateEvent("PastEventInMoreThanPastCustomRange", string.Empty, false, DateTime.Now.AddDays(-18), DateTime.Now.AddDays(-16));

            ServerOperations.Events().CreateEvent("UpcomingEventInNextCustomRange", string.Empty, false, DateTime.Now.AddDays(7), DateTime.Now.AddDays(8));
            ServerOperations.Events().CreateEvent("UpcomingEventInLessThanNextCustomRange", string.Empty, false, DateTime.Now.AddDays(3), DateTime.Now.AddDays(4));
            ServerOperations.Events().CreateEvent("UpcomingEventInMoreThanNextCustomRange", string.Empty, false, DateTime.Now.AddDays(15), DateTime.Now.AddDays(16));

            var widgetId = this.AddControlFilters(this.CreatePage(), SelectionMode.FilteredItems, filterList, SelectionMode.AllItems, DefaultNarrowSelectionFilters);

            this.AssertEvent(widgetId, DateTime.MinValue, DateTime.MaxValue, 7);
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.SitefinityTeam8)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events_CustomRange_UpcomingFromDateToDateOrPastToDate()
        {
            var filterList = new List<AdditionalFilter>() 
            {
                new AdditionalFilter() 
                {
                    Filter = string.Empty,
                    FilterSection = FilterSection.Upcoming,
                    StartDate = DateTime.UtcNow.AddDays(6),
                    EndDate = DateTime.UtcNow.AddDays(12)
                },
                new AdditionalFilter() 
                {
                    Filter = string.Empty,
                    FilterSection = FilterSection.Past,
                    EndDate = DateTime.UtcNow.AddDays(-6)
                }
            };

            ServerOperations.Events().CreateEvent("PastEventInPastCustomRange", string.Empty, false, DateTime.Now.AddDays(-10), DateTime.Now.AddDays(-8));
            ServerOperations.Events().CreateEvent("PastEventInLessThanPastCustomRange", string.Empty, false, DateTime.Now.AddDays(-3), DateTime.Now.AddDays(-1));
            ServerOperations.Events().CreateEvent("PastEventInMoreThanPastCustomRange", string.Empty, false, DateTime.Now.AddDays(-18), DateTime.Now.AddDays(-16));

            ServerOperations.Events().CreateEvent("UpcomingEventInNextCustomRange", string.Empty, false, DateTime.Now.AddDays(7), DateTime.Now.AddDays(8));
            ServerOperations.Events().CreateEvent("UpcomingEventInLessThanNextCustomRange", string.Empty, false, DateTime.Now.AddDays(3), DateTime.Now.AddDays(4));
            ServerOperations.Events().CreateEvent("UpcomingEventInMoreThanNextCustomRange", string.Empty, false, DateTime.Now.AddDays(15), DateTime.Now.AddDays(16));

            var widgetId = this.AddControlFilters(this.CreatePage(), SelectionMode.FilteredItems, filterList, SelectionMode.AllItems, DefaultNarrowSelectionFilters);

            this.AssertEvent(widgetId, DateTime.MinValue, DateTime.MaxValue, 3);
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.SitefinityTeam8)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events_CustomRange_UpcomingFromDateToDateOrPastFromDateToDate()
        {
            var filterList = new List<AdditionalFilter>() 
            {
                new AdditionalFilter() 
                {
                    Filter = string.Empty,
                    FilterSection = FilterSection.Upcoming,
                    StartDate = DateTime.UtcNow.AddDays(6),
                    EndDate = DateTime.UtcNow.AddDays(12)
                },
                new AdditionalFilter() 
                {
                    Filter = string.Empty,
                    FilterSection = FilterSection.Past,
                    StartDate = DateTime.UtcNow.AddDays(-12),
                    EndDate = DateTime.UtcNow.AddDays(-6)
                }
            };

            ServerOperations.Events().CreateEvent("PastEventInPastCustomRange", string.Empty, false, DateTime.Now.AddDays(-10), DateTime.Now.AddDays(-8));
            ServerOperations.Events().CreateEvent("PastEventInLessThanPastCustomRange", string.Empty, false, DateTime.Now.AddDays(-3), DateTime.Now.AddDays(-1));
            ServerOperations.Events().CreateEvent("PastEventInMoreThanPastCustomRange", string.Empty, false, DateTime.Now.AddDays(-18), DateTime.Now.AddDays(-16));

            ServerOperations.Events().CreateEvent("UpcomingEventInNextCustomRange", string.Empty, false, DateTime.Now.AddDays(7), DateTime.Now.AddDays(8));
            ServerOperations.Events().CreateEvent("UpcomingEventInLessThanNextCustomRange", string.Empty, false, DateTime.Now.AddDays(3), DateTime.Now.AddDays(4));
            ServerOperations.Events().CreateEvent("UpcomingEventInMoreThanNextCustomRange", string.Empty, false, DateTime.Now.AddDays(15), DateTime.Now.AddDays(16));

            var widgetId = this.AddControlFilters(this.CreatePage(), SelectionMode.FilteredItems, filterList, SelectionMode.AllItems, DefaultNarrowSelectionFilters);

            this.AssertEvent(widgetId, DateTime.MinValue, DateTime.MaxValue, 2);
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.SitefinityTeam8)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events_CustomRange_UpcomingFromDateToDateOrLast3Days()
        {
            var filterList = new List<AdditionalFilter>() 
            {
                new AdditionalFilter() 
                {
                    Filter = string.Empty,
                    FilterSection = FilterSection.Upcoming,
                    StartDate = DateTime.UtcNow.AddDays(6),
                    EndDate = DateTime.UtcNow.AddDays(12)
                },
                new AdditionalFilter() 
                {
                    Filter = Past3DaysFilterGroup,
                    FilterSection = FilterSection.Past
                }
            };

            ServerOperations.Events().CreateEvent("PastEventInLast3Days", string.Empty, false, DateTime.Now.AddDays(-2), DateTime.Now.AddDays(-1));
            ServerOperations.Events().CreateEvent("PastEventInMoreThanLast3Days", string.Empty, false, DateTime.Now.AddDays(-6), DateTime.Now.AddDays(-4));

            ServerOperations.Events().CreateEvent("UpcomingEventInNextCustomRange", string.Empty, false, DateTime.Now.AddDays(7), DateTime.Now.AddDays(8));
            ServerOperations.Events().CreateEvent("UpcomingEventInLessThanNextCustomRange", string.Empty, false, DateTime.Now.AddDays(3), DateTime.Now.AddDays(4));
            ServerOperations.Events().CreateEvent("UpcomingEventInMoreThanNextCustomRange", string.Empty, false, DateTime.Now.AddDays(15), DateTime.Now.AddDays(16));

            var widgetId = this.AddControlFilters(this.CreatePage(), SelectionMode.FilteredItems, filterList, SelectionMode.AllItems, DefaultNarrowSelectionFilters);

            this.AssertEvent(widgetId, DateTime.MinValue, DateTime.MaxValue, 6);
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.SitefinityTeam8)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events_CustomRange_UpcomingFromDateToDateOrLastWeek()
        {
            var filterList = new List<AdditionalFilter>() 
            {
                new AdditionalFilter() 
                {
                    Filter = string.Empty,
                    FilterSection = FilterSection.Upcoming,
                    StartDate = DateTime.UtcNow.AddDays(6),
                    EndDate = DateTime.UtcNow.AddDays(12)
                },
                new AdditionalFilter() 
                {
                    Filter = PastWeekFilterGroup,
                    FilterSection = FilterSection.Past
                }
            };

            ServerOperations.Events().CreateEvent("PastEventInLastWeek", string.Empty, false, DateTime.Now.AddDays(-6), DateTime.Now.AddDays(-4));
            ServerOperations.Events().CreateEvent("PastEventInMoreThanLastWeek", string.Empty, false, DateTime.Now.AddDays(-12), DateTime.Now.AddDays(-9));

            ServerOperations.Events().CreateEvent("UpcomingEventInNextCustomRange", string.Empty, false, DateTime.Now.AddDays(7), DateTime.Now.AddDays(8));
            ServerOperations.Events().CreateEvent("UpcomingEventInLessThanNextCustomRange", string.Empty, false, DateTime.Now.AddDays(3), DateTime.Now.AddDays(4));
            ServerOperations.Events().CreateEvent("UpcomingEventInMoreThanNextCustomRange", string.Empty, false, DateTime.Now.AddDays(15), DateTime.Now.AddDays(16));

            var widgetId = this.AddControlFilters(this.CreatePage(), SelectionMode.FilteredItems, filterList, SelectionMode.AllItems, DefaultNarrowSelectionFilters);

            this.AssertEvent(widgetId, DateTime.MinValue, DateTime.MaxValue, 6);
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.SitefinityTeam8)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events_CustomRange_UpcomingFromDateToDateOrLastMonth()
        {
            var filterList = new List<AdditionalFilter>() 
            {
                new AdditionalFilter() 
                {
                    Filter = string.Empty,
                    FilterSection = FilterSection.Upcoming,
                    StartDate = DateTime.UtcNow.AddDays(6),
                    EndDate = DateTime.UtcNow.AddDays(12)
                },
                new AdditionalFilter() 
                {
                    Filter = PastMonthFilterGroup,
                    FilterSection = FilterSection.Past
                }
            };

            ServerOperations.Events().CreateEvent("PastEventInLastMonth", string.Empty, false, DateTime.Now.AddDays(-22), DateTime.Now.AddDays(-20));
            ServerOperations.Events().CreateEvent("PastEventInMoreThanLastMonth", string.Empty, false, DateTime.Now.AddDays(-42), DateTime.Now.AddDays(-40));

            ServerOperations.Events().CreateEvent("UpcomingEventInNextCustomRange", string.Empty, false, DateTime.Now.AddDays(7), DateTime.Now.AddDays(8));
            ServerOperations.Events().CreateEvent("UpcomingEventInLessThanNextCustomRange", string.Empty, false, DateTime.Now.AddDays(3), DateTime.Now.AddDays(4));
            ServerOperations.Events().CreateEvent("UpcomingEventInMoreThanNextCustomRange", string.Empty, false, DateTime.Now.AddDays(15), DateTime.Now.AddDays(16));

            var widgetId = this.AddControlFilters(this.CreatePage(), SelectionMode.FilteredItems, filterList, SelectionMode.AllItems, DefaultNarrowSelectionFilters);

            this.AssertEvent(widgetId, DateTime.MinValue, DateTime.MaxValue, 6);
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.SitefinityTeam8)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events_CustomRange_UpcomingFromDateToDateOrLastYear()
        {
            var filterList = new List<AdditionalFilter>() 
            {
                new AdditionalFilter() 
                {
                    Filter = string.Empty,
                    FilterSection = FilterSection.Upcoming,
                    StartDate = DateTime.UtcNow.AddDays(6),
                    EndDate = DateTime.UtcNow.AddDays(12)
                },
                new AdditionalFilter() 
                {
                    Filter = PastYearFilterGroup,
                    FilterSection = FilterSection.Past
                }
            };

            ServerOperations.Events().CreateEvent("PastEventInLastYear", string.Empty, false, DateTime.Now.AddDays(-368), DateTime.Now.AddDays(-365));
            ServerOperations.Events().CreateEvent("PastEventInMoreThanLastYear", string.Empty, false, DateTime.Now.AddDays(-453), DateTime.Now.AddDays(-450));

            ServerOperations.Events().CreateEvent("UpcomingEventInNextCustomRange", string.Empty, false, DateTime.Now.AddDays(7), DateTime.Now.AddDays(8));
            ServerOperations.Events().CreateEvent("UpcomingEventInLessThanNextCustomRange", string.Empty, false, DateTime.Now.AddDays(3), DateTime.Now.AddDays(4));
            ServerOperations.Events().CreateEvent("UpcomingEventInMoreThanNextCustomRange", string.Empty, false, DateTime.Now.AddDays(15), DateTime.Now.AddDays(16));

            var widgetId = this.AddControlFilters(this.CreatePage(), SelectionMode.FilteredItems, filterList, SelectionMode.AllItems, DefaultNarrowSelectionFilters);

            this.AssertEvent(widgetId, DateTime.MinValue, DateTime.MaxValue, 6);
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.SitefinityTeam8)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events_CustomRange_Upcoming3DaysOrPastFromDate()
        {
            var filterList = new List<AdditionalFilter>() 
            {
                new AdditionalFilter() 
                {
                    Filter = Upcoming3DaysFilterGroup,
                    FilterSection = FilterSection.Upcoming
                },
                new AdditionalFilter() 
                {
                    Filter = string.Empty,
                    FilterSection = FilterSection.Past,
                    StartDate = DateTime.UtcNow.AddDays(-12)
                }
            };

            ServerOperations.Events().CreateEvent("PastEventInPastCustomRange", string.Empty, false, DateTime.Now.AddDays(-10), DateTime.Now.AddDays(-8));
            ServerOperations.Events().CreateEvent("PastEventInLessThanPastCustomRange", string.Empty, false, DateTime.Now.AddDays(-3), DateTime.Now.AddDays(-1));
            ServerOperations.Events().CreateEvent("PastEventInMoreThanPastCustomRange", string.Empty, false, DateTime.Now.AddDays(-18), DateTime.Now.AddDays(-16));

            ServerOperations.Events().CreateEvent("UpcomingEventInNext3Days", string.Empty, false, DateTime.Now.AddDays(2), DateTime.Now.AddDays(3));
            ServerOperations.Events().CreateEvent("UpcomingEventInMoreThanNext3Days", string.Empty, false, DateTime.Now.AddDays(5), DateTime.Now.AddDays(7));

            var widgetId = this.AddControlFilters(this.CreatePage(), SelectionMode.FilteredItems, filterList, SelectionMode.AllItems, DefaultNarrowSelectionFilters);

            this.AssertEvent(widgetId, DateTime.MinValue, DateTime.MaxValue, 11);
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.SitefinityTeam8)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events_CustomRange_UpcomingWeekOrPastFromDate()
        {
            var filterList = new List<AdditionalFilter>() 
            {
                new AdditionalFilter() 
                {
                    Filter = UpcomingWeekFilterGroup,
                    FilterSection = FilterSection.Upcoming
                },
                new AdditionalFilter() 
                {
                    Filter = string.Empty,
                    FilterSection = FilterSection.Past,
                    StartDate = DateTime.UtcNow.AddDays(-12)
                }
            };

            ServerOperations.Events().CreateEvent("PastEventInPastCustomRange", string.Empty, false, DateTime.Now.AddDays(-10), DateTime.Now.AddDays(-8));
            ServerOperations.Events().CreateEvent("PastEventInLessThanPastCustomRange", string.Empty, false, DateTime.Now.AddDays(-3), DateTime.Now.AddDays(-1));
            ServerOperations.Events().CreateEvent("PastEventInMoreThanPastCustomRange", string.Empty, false, DateTime.Now.AddDays(-18), DateTime.Now.AddDays(-16));

            ServerOperations.Events().CreateEvent("UpcomingEventInNextWeek", string.Empty, false, DateTime.Now.AddDays(4), DateTime.Now.AddDays(6));
            ServerOperations.Events().CreateEvent("UpcomingEventInMoreThanNextWeek", string.Empty, false, DateTime.Now.AddDays(9), DateTime.Now.AddDays(12));

            var widgetId = this.AddControlFilters(this.CreatePage(), SelectionMode.FilteredItems, filterList, SelectionMode.AllItems, DefaultNarrowSelectionFilters);

            this.AssertEvent(widgetId, DateTime.MinValue, DateTime.MaxValue, 11);
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.SitefinityTeam8)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events_CustomRange_UpcomingMonthOrPastFromDate()
        {
            var filterList = new List<AdditionalFilter>() 
            {
                new AdditionalFilter() 
                {
                    Filter = UpcomingMonthFilterGroup,
                    FilterSection = FilterSection.Upcoming
                },
                new AdditionalFilter() 
                {
                    Filter = string.Empty,
                    FilterSection = FilterSection.Past,
                    StartDate = DateTime.UtcNow.AddDays(-12)
                }
            };

            ServerOperations.Events().CreateEvent("PastEventInPastCustomRange", string.Empty, false, DateTime.Now.AddDays(-10), DateTime.Now.AddDays(-8));
            ServerOperations.Events().CreateEvent("PastEventInLessThanPastCustomRange", string.Empty, false, DateTime.Now.AddDays(-3), DateTime.Now.AddDays(-1));
            ServerOperations.Events().CreateEvent("PastEventInMoreThanPastCustomRange", string.Empty, false, DateTime.Now.AddDays(-18), DateTime.Now.AddDays(-16));

            ServerOperations.Events().CreateEvent("UpcomingEventInNextMonth", string.Empty, false, DateTime.Now.AddDays(20), DateTime.Now.AddDays(22));
            ServerOperations.Events().CreateEvent("UpcomingEventInMoreThanNextMonth", string.Empty, false, DateTime.Now.AddDays(40), DateTime.Now.AddDays(42));

            var widgetId = this.AddControlFilters(this.CreatePage(), SelectionMode.FilteredItems, filterList, SelectionMode.AllItems, DefaultNarrowSelectionFilters);

            this.AssertEvent(widgetId, DateTime.MinValue, DateTime.MaxValue, 11);
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.SitefinityTeam8)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events_CustomRange_UpcomingYearOrPastFromDate()
        {
            var filterList = new List<AdditionalFilter>() 
            {
                new AdditionalFilter() 
                {
                    Filter = UpcomingYearFilterGroup,
                    FilterSection = FilterSection.Upcoming
                },
                new AdditionalFilter() 
                {
                    Filter = string.Empty,
                    FilterSection = FilterSection.Past,
                    StartDate = DateTime.UtcNow.AddDays(-12)
                }
            };

            ServerOperations.Events().CreateEvent("PastEventInPastCustomRange", string.Empty, false, DateTime.Now.AddDays(-10), DateTime.Now.AddDays(-8));
            ServerOperations.Events().CreateEvent("PastEventInLessThanPastCustomRange", string.Empty, false, DateTime.Now.AddDays(-3), DateTime.Now.AddDays(-1));
            ServerOperations.Events().CreateEvent("PastEventInMoreThanPastCustomRange", string.Empty, false, DateTime.Now.AddDays(-18), DateTime.Now.AddDays(-16));

            ServerOperations.Events().CreateEvent("UpcomingEventInNextYear", string.Empty, false, DateTime.Now.AddDays(365), DateTime.Now.AddDays(368));
            ServerOperations.Events().CreateEvent("UpcomingEventInMoreThanNextYear", string.Empty, false, DateTime.Now.AddDays(450), DateTime.Now.AddDays(453));

            var widgetId = this.AddControlFilters(this.CreatePage(), SelectionMode.FilteredItems, filterList, SelectionMode.AllItems, DefaultNarrowSelectionFilters);

            this.AssertEvent(widgetId, DateTime.MinValue, DateTime.MaxValue, 11);
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.SitefinityTeam8)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events_CustomRange_Upcoming3DaysOrPastToDate()
        {
            var filterList = new List<AdditionalFilter>() 
            {
                new AdditionalFilter() 
                {
                    Filter = Upcoming3DaysFilterGroup,
                    FilterSection = FilterSection.Upcoming
                },
                new AdditionalFilter() 
                {
                    Filter = string.Empty,
                    FilterSection = FilterSection.Past,
                    EndDate = DateTime.UtcNow.AddDays(-6)
                }
            };

            ServerOperations.Events().CreateEvent("PastEventInPastCustomRange", string.Empty, false, DateTime.Now.AddDays(-10), DateTime.Now.AddDays(-8));
            ServerOperations.Events().CreateEvent("PastEventInLessThanPastCustomRange", string.Empty, false, DateTime.Now.AddDays(-3), DateTime.Now.AddDays(-1));
            ServerOperations.Events().CreateEvent("PastEventInMoreThanPastCustomRange", string.Empty, false, DateTime.Now.AddDays(-18), DateTime.Now.AddDays(-16));

            ServerOperations.Events().CreateEvent("UpcomingEventInNext3Days", string.Empty, false, DateTime.Now.AddDays(2), DateTime.Now.AddDays(3));
            ServerOperations.Events().CreateEvent("UpcomingEventInMoreThanNext3Days", string.Empty, false, DateTime.Now.AddDays(5), DateTime.Now.AddDays(7));

            var widgetId = this.AddControlFilters(this.CreatePage(), SelectionMode.FilteredItems, filterList, SelectionMode.AllItems, DefaultNarrowSelectionFilters);

            this.AssertEvent(widgetId, DateTime.MinValue, DateTime.MaxValue, 7);
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.SitefinityTeam8)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events_CustomRange_UpcomingWeekOrPastToDate()
        {
            var filterList = new List<AdditionalFilter>() 
            {
                new AdditionalFilter() 
                {
                    Filter = UpcomingWeekFilterGroup,
                    FilterSection = FilterSection.Upcoming
                },
                new AdditionalFilter() 
                {
                    Filter = string.Empty,
                    FilterSection = FilterSection.Past,
                    EndDate = DateTime.UtcNow.AddDays(-6)
                }
            };

            ServerOperations.Events().CreateEvent("PastEventInPastCustomRange", string.Empty, false, DateTime.Now.AddDays(-10), DateTime.Now.AddDays(-8));
            ServerOperations.Events().CreateEvent("PastEventInLessThanPastCustomRange", string.Empty, false, DateTime.Now.AddDays(-3), DateTime.Now.AddDays(-1));
            ServerOperations.Events().CreateEvent("PastEventInMoreThanPastCustomRange", string.Empty, false, DateTime.Now.AddDays(-18), DateTime.Now.AddDays(-16));

            ServerOperations.Events().CreateEvent("UpcomingEventInNextWeek", string.Empty, false, DateTime.Now.AddDays(4), DateTime.Now.AddDays(6));
            ServerOperations.Events().CreateEvent("UpcomingEventInMoreThanNextWeek", string.Empty, false, DateTime.Now.AddDays(9), DateTime.Now.AddDays(12));

            var widgetId = this.AddControlFilters(this.CreatePage(), SelectionMode.FilteredItems, filterList, SelectionMode.AllItems, DefaultNarrowSelectionFilters);

            this.AssertEvent(widgetId, DateTime.MinValue, DateTime.MaxValue, 7);
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.SitefinityTeam8)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events_CustomRange_UpcomingMonthOrPastToDate()
        {
            var filterList = new List<AdditionalFilter>() 
            {
                new AdditionalFilter() 
                {
                    Filter = UpcomingMonthFilterGroup,
                    FilterSection = FilterSection.Upcoming
                },
                new AdditionalFilter() 
                {
                    Filter = string.Empty,
                    FilterSection = FilterSection.Past,
                    EndDate = DateTime.UtcNow.AddDays(-6)
                }
            };

            ServerOperations.Events().CreateEvent("PastEventInPastCustomRange", string.Empty, false, DateTime.Now.AddDays(-10), DateTime.Now.AddDays(-8));
            ServerOperations.Events().CreateEvent("PastEventInLessThanPastCustomRange", string.Empty, false, DateTime.Now.AddDays(-3), DateTime.Now.AddDays(-1));
            ServerOperations.Events().CreateEvent("PastEventInMoreThanPastCustomRange", string.Empty, false, DateTime.Now.AddDays(-18), DateTime.Now.AddDays(-16));

            ServerOperations.Events().CreateEvent("UpcomingEventInNextMonth", string.Empty, false, DateTime.Now.AddDays(20), DateTime.Now.AddDays(22));
            ServerOperations.Events().CreateEvent("UpcomingEventInMoreThanNextMonth", string.Empty, false, DateTime.Now.AddDays(40), DateTime.Now.AddDays(42));

            var widgetId = this.AddControlFilters(this.CreatePage(), SelectionMode.FilteredItems, filterList, SelectionMode.AllItems, DefaultNarrowSelectionFilters);

            this.AssertEvent(widgetId, DateTime.MinValue, DateTime.MaxValue, 7);
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.SitefinityTeam8)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events_CustomRange_UpcomingYearOrPastToDate()
        {
            var filterList = new List<AdditionalFilter>() 
            {
                new AdditionalFilter() 
                {
                    Filter = UpcomingYearFilterGroup,
                    FilterSection = FilterSection.Upcoming
                },
                new AdditionalFilter() 
                {
                    Filter = string.Empty,
                    FilterSection = FilterSection.Past,
                    EndDate = DateTime.UtcNow.AddDays(-6)
                }
            };

            ServerOperations.Events().CreateEvent("PastEventInPastCustomRange", string.Empty, false, DateTime.Now.AddDays(-10), DateTime.Now.AddDays(-8));
            ServerOperations.Events().CreateEvent("PastEventInLessThanPastCustomRange", string.Empty, false, DateTime.Now.AddDays(-3), DateTime.Now.AddDays(-1));
            ServerOperations.Events().CreateEvent("PastEventInMoreThanPastCustomRange", string.Empty, false, DateTime.Now.AddDays(-18), DateTime.Now.AddDays(-16));

            ServerOperations.Events().CreateEvent("UpcomingEventInNextYear", string.Empty, false, DateTime.Now.AddDays(365), DateTime.Now.AddDays(368));
            ServerOperations.Events().CreateEvent("UpcomingEventInMoreThanNextYear", string.Empty, false, DateTime.Now.AddDays(450), DateTime.Now.AddDays(453));

            var widgetId = this.AddControlFilters(this.CreatePage(), SelectionMode.FilteredItems, filterList, SelectionMode.AllItems, DefaultNarrowSelectionFilters);

            this.AssertEvent(widgetId, DateTime.MinValue, DateTime.MaxValue, 7);
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.SitefinityTeam8)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events_CustomRange_Upcoming3DaysOrPastFromDateToDate()
        {
            var filterList = new List<AdditionalFilter>() 
            {
                new AdditionalFilter() 
                {
                    Filter = Upcoming3DaysFilterGroup,
                    FilterSection = FilterSection.Upcoming
                },
                new AdditionalFilter() 
                {
                    Filter = string.Empty,
                    FilterSection = FilterSection.Past,
                    StartDate = DateTime.UtcNow.AddDays(-12),
                    EndDate = DateTime.UtcNow.AddDays(-6)
                }
            };

            ServerOperations.Events().CreateEvent("PastEventInPastCustomRange", string.Empty, false, DateTime.Now.AddDays(-10), DateTime.Now.AddDays(-8));
            ServerOperations.Events().CreateEvent("PastEventInLessThanPastCustomRange", string.Empty, false, DateTime.Now.AddDays(-3), DateTime.Now.AddDays(-1));
            ServerOperations.Events().CreateEvent("PastEventInMoreThanPastCustomRange", string.Empty, false, DateTime.Now.AddDays(-18), DateTime.Now.AddDays(-16));

            ServerOperations.Events().CreateEvent("UpcomingEventInNext3Days", string.Empty, false, DateTime.Now.AddDays(2), DateTime.Now.AddDays(3));
            ServerOperations.Events().CreateEvent("UpcomingEventInMoreThanNext3Days", string.Empty, false, DateTime.Now.AddDays(5), DateTime.Now.AddDays(7));

            var widgetId = this.AddControlFilters(this.CreatePage(), SelectionMode.FilteredItems, filterList, SelectionMode.AllItems, DefaultNarrowSelectionFilters);

            this.AssertEvent(widgetId, DateTime.MinValue, DateTime.MaxValue, 6);
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.SitefinityTeam8)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events_CustomRange_UpcomingWeekOrPastFromDateToDate()
        {
            var filterList = new List<AdditionalFilter>() 
            {
                new AdditionalFilter() 
                {
                    Filter = UpcomingWeekFilterGroup,
                    FilterSection = FilterSection.Upcoming
                },
                new AdditionalFilter() 
                {
                    Filter = string.Empty,
                    FilterSection = FilterSection.Past,
                    StartDate = DateTime.UtcNow.AddDays(-12),
                    EndDate = DateTime.UtcNow.AddDays(-6)
                }
            };

            ServerOperations.Events().CreateEvent("PastEventInPastCustomRange", string.Empty, false, DateTime.Now.AddDays(-10), DateTime.Now.AddDays(-8));
            ServerOperations.Events().CreateEvent("PastEventInLessThanPastCustomRange", string.Empty, false, DateTime.Now.AddDays(-3), DateTime.Now.AddDays(-1));
            ServerOperations.Events().CreateEvent("PastEventInMoreThanPastCustomRange", string.Empty, false, DateTime.Now.AddDays(-18), DateTime.Now.AddDays(-16));

            ServerOperations.Events().CreateEvent("UpcomingEventInNextWeek", string.Empty, false, DateTime.Now.AddDays(4), DateTime.Now.AddDays(6));
            ServerOperations.Events().CreateEvent("UpcomingEventInMoreThanNextWeek", string.Empty, false, DateTime.Now.AddDays(9), DateTime.Now.AddDays(12));

            var widgetId = this.AddControlFilters(this.CreatePage(), SelectionMode.FilteredItems, filterList, SelectionMode.AllItems, DefaultNarrowSelectionFilters);

            this.AssertEvent(widgetId, DateTime.MinValue, DateTime.MaxValue, 6);
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.SitefinityTeam8)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events_CustomRange_UpcomingMonthOrPastFromDateToDate()
        {
            var filterList = new List<AdditionalFilter>() 
            {
                new AdditionalFilter() 
                {
                    Filter = UpcomingMonthFilterGroup,
                    FilterSection = FilterSection.Upcoming
                },
                new AdditionalFilter() 
                {
                    Filter = string.Empty,
                    FilterSection = FilterSection.Past,
                    StartDate = DateTime.UtcNow.AddDays(-12),
                    EndDate = DateTime.UtcNow.AddDays(-6)
                }
            };

            ServerOperations.Events().CreateEvent("PastEventInPastCustomRange", string.Empty, false, DateTime.Now.AddDays(-10), DateTime.Now.AddDays(-8));
            ServerOperations.Events().CreateEvent("PastEventInLessThanPastCustomRange", string.Empty, false, DateTime.Now.AddDays(-3), DateTime.Now.AddDays(-1));
            ServerOperations.Events().CreateEvent("PastEventInMoreThanPastCustomRange", string.Empty, false, DateTime.Now.AddDays(-18), DateTime.Now.AddDays(-16));

            ServerOperations.Events().CreateEvent("UpcomingEventInNextMonth", string.Empty, false, DateTime.Now.AddDays(20), DateTime.Now.AddDays(22));
            ServerOperations.Events().CreateEvent("UpcomingEventInMoreThanNextMonth", string.Empty, false, DateTime.Now.AddDays(40), DateTime.Now.AddDays(42));

            var widgetId = this.AddControlFilters(this.CreatePage(), SelectionMode.FilteredItems, filterList, SelectionMode.AllItems, DefaultNarrowSelectionFilters);

            this.AssertEvent(widgetId, DateTime.MinValue, DateTime.MaxValue, 6);
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.SitefinityTeam8)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events_CustomRange_UpcomingYearOrPastFromDateToDate()
        {
            var filterList = new List<AdditionalFilter>() 
            {
                new AdditionalFilter() 
                {
                    Filter = UpcomingYearFilterGroup,
                    FilterSection = FilterSection.Upcoming
                },
                new AdditionalFilter() 
                {
                    Filter = string.Empty,
                    FilterSection = FilterSection.Past,
                    StartDate = DateTime.UtcNow.AddDays(-12),
                    EndDate = DateTime.UtcNow.AddDays(-6)
                }
            };

            ServerOperations.Events().CreateEvent("PastEventInPastCustomRange", string.Empty, false, DateTime.Now.AddDays(-10), DateTime.Now.AddDays(-8));
            ServerOperations.Events().CreateEvent("PastEventInLessThanPastCustomRange", string.Empty, false, DateTime.Now.AddDays(-3), DateTime.Now.AddDays(-1));
            ServerOperations.Events().CreateEvent("PastEventInMoreThanPastCustomRange", string.Empty, false, DateTime.Now.AddDays(-18), DateTime.Now.AddDays(-16));

            ServerOperations.Events().CreateEvent("UpcomingEventInNextYear", string.Empty, false, DateTime.Now.AddDays(365), DateTime.Now.AddDays(368));
            ServerOperations.Events().CreateEvent("UpcomingEventInMoreThanNextYear", string.Empty, false, DateTime.Now.AddDays(450), DateTime.Now.AddDays(453));

            var widgetId = this.AddControlFilters(this.CreatePage(), SelectionMode.FilteredItems, filterList, SelectionMode.AllItems, DefaultNarrowSelectionFilters);

            this.AssertEvent(widgetId, DateTime.MinValue, DateTime.MaxValue, 6);
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.SitefinityTeam8)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events_CustomRange_CurrentOrUpcomingFromDateOrPastFromDate()
        {
            var filterList = new List<AdditionalFilter>() 
            {
                new AdditionalFilter() 
                {
                    Filter = string.Empty,
                    FilterSection = FilterSection.Upcoming,
                    StartDate = DateTime.UtcNow.AddDays(6)
                },
                new AdditionalFilter() 
                {
                    Filter = CurrentFilterGroup,
                    FilterSection = FilterSection.Current
                },
                new AdditionalFilter() 
                {
                    Filter = string.Empty,
                    FilterSection = FilterSection.Past,
                    StartDate = DateTime.UtcNow.AddDays(-12)
                }
            };

            ServerOperations.Events().CreateEvent("PastEventInPastCustomRange", string.Empty, false, DateTime.Now.AddDays(-10), DateTime.Now.AddDays(-8));
            ServerOperations.Events().CreateEvent("PastEventInLessThanPastCustomRange", string.Empty, false, DateTime.Now.AddDays(-3), DateTime.Now.AddDays(-1));
            ServerOperations.Events().CreateEvent("PastEventInMoreThanPastCustomRange", string.Empty, false, DateTime.Now.AddDays(-18), DateTime.Now.AddDays(-16));

            ServerOperations.Events().CreateEvent("UpcomingEventInNextCustomRange", string.Empty, false, DateTime.Now.AddDays(7), DateTime.Now.AddDays(8));
            ServerOperations.Events().CreateEvent("UpcomingEventInLessThanNextCustomRange", string.Empty, false, DateTime.Now.AddDays(3), DateTime.Now.AddDays(4));
            ServerOperations.Events().CreateEvent("UpcomingEventInMoreThanNextCustomRange", string.Empty, false, DateTime.Now.AddDays(15), DateTime.Now.AddDays(16));

            var widgetId = this.AddControlFilters(this.CreatePage(), SelectionMode.FilteredItems, filterList, SelectionMode.AllItems, DefaultNarrowSelectionFilters);

            this.AssertEvent(widgetId, DateTime.MinValue, DateTime.MaxValue, 14);
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.SitefinityTeam8)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events_CustomRange_CurrentOrUpcomingFromDateOrPastToDate()
        {
            var filterList = new List<AdditionalFilter>() 
            {
                new AdditionalFilter() 
                {
                    Filter = string.Empty,
                    FilterSection = FilterSection.Upcoming,
                    StartDate = DateTime.UtcNow.AddDays(6)
                },
                new AdditionalFilter() 
                {
                    Filter = CurrentFilterGroup,
                    FilterSection = FilterSection.Current
                },
                new AdditionalFilter() 
                {
                    Filter = string.Empty,
                    FilterSection = FilterSection.Past,
                    EndDate = DateTime.UtcNow.AddDays(-6)
                }
            };

            ServerOperations.Events().CreateEvent("PastEventInPastCustomRange", string.Empty, false, DateTime.Now.AddDays(-10), DateTime.Now.AddDays(-8));
            ServerOperations.Events().CreateEvent("PastEventInLessThanPastCustomRange", string.Empty, false, DateTime.Now.AddDays(-3), DateTime.Now.AddDays(-1));
            ServerOperations.Events().CreateEvent("PastEventInMoreThanPastCustomRange", string.Empty, false, DateTime.Now.AddDays(-18), DateTime.Now.AddDays(-16));

            ServerOperations.Events().CreateEvent("UpcomingEventInNextCustomRange", string.Empty, false, DateTime.Now.AddDays(7), DateTime.Now.AddDays(8));
            ServerOperations.Events().CreateEvent("UpcomingEventInLessThanNextCustomRange", string.Empty, false, DateTime.Now.AddDays(3), DateTime.Now.AddDays(4));
            ServerOperations.Events().CreateEvent("UpcomingEventInMoreThanNextCustomRange", string.Empty, false, DateTime.Now.AddDays(15), DateTime.Now.AddDays(16));

            var widgetId = this.AddControlFilters(this.CreatePage(), SelectionMode.FilteredItems, filterList, SelectionMode.AllItems, DefaultNarrowSelectionFilters);

            this.AssertEvent(widgetId, DateTime.MinValue, DateTime.MaxValue, 10);
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.SitefinityTeam8)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events_CustomRange_CurrentOrUpcomingFromDateOrPastFromDateToDate()
        {
            var filterList = new List<AdditionalFilter>() 
            {
                new AdditionalFilter() 
                {
                    Filter = string.Empty,
                    FilterSection = FilterSection.Upcoming,
                    StartDate = DateTime.UtcNow.AddDays(6)
                },
                new AdditionalFilter() 
                {
                    Filter = CurrentFilterGroup,
                    FilterSection = FilterSection.Current
                },
                new AdditionalFilter() 
                {
                    Filter = string.Empty,
                    FilterSection = FilterSection.Past,
                    StartDate = DateTime.UtcNow.AddDays(-12),
                    EndDate = DateTime.UtcNow.AddDays(-6)
                }
            };

            ServerOperations.Events().CreateEvent("PastEventInPastCustomRange", string.Empty, false, DateTime.Now.AddDays(-10), DateTime.Now.AddDays(-8));
            ServerOperations.Events().CreateEvent("PastEventInLessThanPastCustomRange", string.Empty, false, DateTime.Now.AddDays(-3), DateTime.Now.AddDays(-1));
            ServerOperations.Events().CreateEvent("PastEventInMoreThanPastCustomRange", string.Empty, false, DateTime.Now.AddDays(-18), DateTime.Now.AddDays(-16));

            ServerOperations.Events().CreateEvent("UpcomingEventInNextCustomRange", string.Empty, false, DateTime.Now.AddDays(7), DateTime.Now.AddDays(8));
            ServerOperations.Events().CreateEvent("UpcomingEventInLessThanNextCustomRange", string.Empty, false, DateTime.Now.AddDays(3), DateTime.Now.AddDays(4));
            ServerOperations.Events().CreateEvent("UpcomingEventInMoreThanNextCustomRange", string.Empty, false, DateTime.Now.AddDays(15), DateTime.Now.AddDays(16));

            var widgetId = this.AddControlFilters(this.CreatePage(), SelectionMode.FilteredItems, filterList, SelectionMode.AllItems, DefaultNarrowSelectionFilters);

            this.AssertEvent(widgetId, DateTime.MinValue, DateTime.MaxValue, 9);
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.SitefinityTeam8)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events_CustomRange_CurrentOrUpcomingFromDateOrLast3Days()
        {
            var filterList = new List<AdditionalFilter>() 
            {
                new AdditionalFilter() 
                {
                    Filter = string.Empty,
                    FilterSection = FilterSection.Upcoming,
                    StartDate = DateTime.UtcNow.AddDays(6)
                },
                new AdditionalFilter() 
                {
                    Filter = CurrentFilterGroup,
                    FilterSection = FilterSection.Current
                },
                new AdditionalFilter() 
                {
                    Filter = Past3DaysFilterGroup,
                    FilterSection = FilterSection.Past
                }
            };

            ServerOperations.Events().CreateEvent("PastEventInLast3Days", string.Empty, false, DateTime.Now.AddDays(-2), DateTime.Now.AddDays(-1));
            ServerOperations.Events().CreateEvent("PastEventInMoreThanLast3Days", string.Empty, false, DateTime.Now.AddDays(-6), DateTime.Now.AddDays(-4));

            ServerOperations.Events().CreateEvent("UpcomingEventInNextCustomRange", string.Empty, false, DateTime.Now.AddDays(7), DateTime.Now.AddDays(8));
            ServerOperations.Events().CreateEvent("UpcomingEventInLessThanNextCustomRange", string.Empty, false, DateTime.Now.AddDays(3), DateTime.Now.AddDays(4));
            ServerOperations.Events().CreateEvent("UpcomingEventInMoreThanNextCustomRange", string.Empty, false, DateTime.Now.AddDays(15), DateTime.Now.AddDays(16));

            var widgetId = this.AddControlFilters(this.CreatePage(), SelectionMode.FilteredItems, filterList, SelectionMode.AllItems, DefaultNarrowSelectionFilters);

            this.AssertEvent(widgetId, DateTime.MinValue, DateTime.MaxValue, 13);
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.SitefinityTeam8)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events_CustomRange_CurrentOrUpcomingFromDateOrLastWeek()
        {
            var filterList = new List<AdditionalFilter>() 
            {
                new AdditionalFilter() 
                {
                    Filter = string.Empty,
                    FilterSection = FilterSection.Upcoming,
                    StartDate = DateTime.UtcNow.AddDays(6)
                },
                new AdditionalFilter() 
                {
                    Filter = CurrentFilterGroup,
                    FilterSection = FilterSection.Current
                },
                new AdditionalFilter() 
                {
                    Filter = PastWeekFilterGroup,
                    FilterSection = FilterSection.Past
                }
            };

            ServerOperations.Events().CreateEvent("PastEventInLastWeek", string.Empty, false, DateTime.Now.AddDays(-6), DateTime.Now.AddDays(-4));
            ServerOperations.Events().CreateEvent("PastEventInMoreThanLastWeek", string.Empty, false, DateTime.Now.AddDays(-12), DateTime.Now.AddDays(-9));

            ServerOperations.Events().CreateEvent("UpcomingEventInNextCustomRange", string.Empty, false, DateTime.Now.AddDays(7), DateTime.Now.AddDays(8));
            ServerOperations.Events().CreateEvent("UpcomingEventInLessThanNextCustomRange", string.Empty, false, DateTime.Now.AddDays(3), DateTime.Now.AddDays(4));
            ServerOperations.Events().CreateEvent("UpcomingEventInMoreThanNextCustomRange", string.Empty, false, DateTime.Now.AddDays(15), DateTime.Now.AddDays(16));

            var widgetId = this.AddControlFilters(this.CreatePage(), SelectionMode.FilteredItems, filterList, SelectionMode.AllItems, DefaultNarrowSelectionFilters);

            this.AssertEvent(widgetId, DateTime.MinValue, DateTime.MaxValue, 13);
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.SitefinityTeam8)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events_CustomRange_CurrentOrUpcomingFromDateOrLastMonth()
        {
            var filterList = new List<AdditionalFilter>() 
            {
                new AdditionalFilter() 
                {
                    Filter = string.Empty,
                    FilterSection = FilterSection.Upcoming,
                    StartDate = DateTime.UtcNow.AddDays(6)
                },
                new AdditionalFilter() 
                {
                    Filter = CurrentFilterGroup,
                    FilterSection = FilterSection.Current
                },
                new AdditionalFilter() 
                {
                    Filter = PastMonthFilterGroup,
                    FilterSection = FilterSection.Past
                }
            };

            ServerOperations.Events().CreateEvent("PastEventInLastMonth", string.Empty, false, DateTime.Now.AddDays(-22), DateTime.Now.AddDays(-20));
            ServerOperations.Events().CreateEvent("PastEventInMoreThanLastMonth", string.Empty, false, DateTime.Now.AddDays(-42), DateTime.Now.AddDays(-40));

            ServerOperations.Events().CreateEvent("UpcomingEventInNextCustomRange", string.Empty, false, DateTime.Now.AddDays(7), DateTime.Now.AddDays(8));
            ServerOperations.Events().CreateEvent("UpcomingEventInLessThanNextCustomRange", string.Empty, false, DateTime.Now.AddDays(3), DateTime.Now.AddDays(4));
            ServerOperations.Events().CreateEvent("UpcomingEventInMoreThanNextCustomRange", string.Empty, false, DateTime.Now.AddDays(15), DateTime.Now.AddDays(16));

            var widgetId = this.AddControlFilters(this.CreatePage(), SelectionMode.FilteredItems, filterList, SelectionMode.AllItems, DefaultNarrowSelectionFilters);

            this.AssertEvent(widgetId, DateTime.MinValue, DateTime.MaxValue, 13);
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.SitefinityTeam8)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events_CustomRange_CurrentOrUpcomingFromDateOrLastYear()
        {
            var filterList = new List<AdditionalFilter>() 
            {
                new AdditionalFilter() 
                {
                    Filter = string.Empty,
                    FilterSection = FilterSection.Upcoming,
                    StartDate = DateTime.UtcNow.AddDays(6)
                },
                new AdditionalFilter() 
                {
                    Filter = CurrentFilterGroup,
                    FilterSection = FilterSection.Current
                },
                new AdditionalFilter() 
                {
                    Filter = PastYearFilterGroup,
                    FilterSection = FilterSection.Past
                }
            };

            ServerOperations.Events().CreateEvent("PastEventInLastYear", string.Empty, false, DateTime.Now.AddDays(-368), DateTime.Now.AddDays(-365));
            ServerOperations.Events().CreateEvent("PastEventInMoreThanLastYear", string.Empty, false, DateTime.Now.AddDays(-453), DateTime.Now.AddDays(-450));

            ServerOperations.Events().CreateEvent("UpcomingEventInNextCustomRange", string.Empty, false, DateTime.Now.AddDays(7), DateTime.Now.AddDays(8));
            ServerOperations.Events().CreateEvent("UpcomingEventInLessThanNextCustomRange", string.Empty, false, DateTime.Now.AddDays(3), DateTime.Now.AddDays(4));
            ServerOperations.Events().CreateEvent("UpcomingEventInMoreThanNextCustomRange", string.Empty, false, DateTime.Now.AddDays(15), DateTime.Now.AddDays(16));

            var widgetId = this.AddControlFilters(this.CreatePage(), SelectionMode.FilteredItems, filterList, SelectionMode.AllItems, DefaultNarrowSelectionFilters);

            this.AssertEvent(widgetId, DateTime.MinValue, DateTime.MaxValue, 13);
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.SitefinityTeam8)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events_CustomRange_CurrentOrUpcomingToDateOrPastFromDate()
        {
            var filterList = new List<AdditionalFilter>() 
            {
                new AdditionalFilter() 
                {
                    Filter = string.Empty,
                    FilterSection = FilterSection.Upcoming,
                    EndDate = DateTime.UtcNow.AddDays(12)
                },
                new AdditionalFilter() 
                {
                    Filter = CurrentFilterGroup,
                    FilterSection = FilterSection.Current
                },
                new AdditionalFilter() 
                {
                    Filter = string.Empty,
                    FilterSection = FilterSection.Past,
                    StartDate = DateTime.UtcNow.AddDays(-12)
                }
            };

            ServerOperations.Events().CreateEvent("PastEventInPastCustomRange", string.Empty, false, DateTime.Now.AddDays(-10), DateTime.Now.AddDays(-8));
            ServerOperations.Events().CreateEvent("PastEventInLessThanPastCustomRange", string.Empty, false, DateTime.Now.AddDays(-3), DateTime.Now.AddDays(-1));
            ServerOperations.Events().CreateEvent("PastEventInMoreThanPastCustomRange", string.Empty, false, DateTime.Now.AddDays(-18), DateTime.Now.AddDays(-16));

            ServerOperations.Events().CreateEvent("UpcomingEventInNextCustomRange", string.Empty, false, DateTime.Now.AddDays(7), DateTime.Now.AddDays(8));
            ServerOperations.Events().CreateEvent("UpcomingEventInLessThanNextCustomRange", string.Empty, false, DateTime.Now.AddDays(3), DateTime.Now.AddDays(4));
            ServerOperations.Events().CreateEvent("UpcomingEventInMoreThanNextCustomRange", string.Empty, false, DateTime.Now.AddDays(15), DateTime.Now.AddDays(16));

            var widgetId = this.AddControlFilters(this.CreatePage(), SelectionMode.FilteredItems, filterList, SelectionMode.AllItems, DefaultNarrowSelectionFilters);

            this.AssertEvent(widgetId, DateTime.MinValue, DateTime.MaxValue, 18);
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.SitefinityTeam8)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events_CustomRange_CurrentOrUpcomingToDateOrPastToDate()
        {
            var filterList = new List<AdditionalFilter>() 
            {
                new AdditionalFilter() 
                {
                    Filter = string.Empty,
                    FilterSection = FilterSection.Upcoming,
                    EndDate = DateTime.UtcNow.AddDays(12)
                },
                new AdditionalFilter() 
                {
                    Filter = CurrentFilterGroup,
                    FilterSection = FilterSection.Current
                },
                new AdditionalFilter() 
                {
                    Filter = string.Empty,
                    FilterSection = FilterSection.Past,
                    EndDate = DateTime.UtcNow.AddDays(-6)
                }
            };

            ServerOperations.Events().CreateEvent("PastEventInPastCustomRange", string.Empty, false, DateTime.Now.AddDays(-10), DateTime.Now.AddDays(-8));
            ServerOperations.Events().CreateEvent("PastEventInLessThanPastCustomRange", string.Empty, false, DateTime.Now.AddDays(-3), DateTime.Now.AddDays(-1));
            ServerOperations.Events().CreateEvent("PastEventInMoreThanPastCustomRange", string.Empty, false, DateTime.Now.AddDays(-18), DateTime.Now.AddDays(-16));

            ServerOperations.Events().CreateEvent("UpcomingEventInNextCustomRange", string.Empty, false, DateTime.Now.AddDays(7), DateTime.Now.AddDays(8));
            ServerOperations.Events().CreateEvent("UpcomingEventInLessThanNextCustomRange", string.Empty, false, DateTime.Now.AddDays(3), DateTime.Now.AddDays(4));
            ServerOperations.Events().CreateEvent("UpcomingEventInMoreThanNextCustomRange", string.Empty, false, DateTime.Now.AddDays(15), DateTime.Now.AddDays(16));

            var widgetId = this.AddControlFilters(this.CreatePage(), SelectionMode.FilteredItems, filterList, SelectionMode.AllItems, DefaultNarrowSelectionFilters);

            this.AssertEvent(widgetId, DateTime.MinValue, DateTime.MaxValue, 14);
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.SitefinityTeam8)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events_CustomRange_CurrentOrUpcomingToDateOrPastFromDateToDate()
        {
            var filterList = new List<AdditionalFilter>() 
            {
                new AdditionalFilter() 
                {
                    Filter = string.Empty,
                    FilterSection = FilterSection.Upcoming,
                    EndDate = DateTime.UtcNow.AddDays(12)
                },
                new AdditionalFilter() 
                {
                    Filter = CurrentFilterGroup,
                    FilterSection = FilterSection.Current
                },
                new AdditionalFilter() 
                {
                    Filter = string.Empty,
                    FilterSection = FilterSection.Past,
                    StartDate = DateTime.UtcNow.AddDays(-12),
                    EndDate = DateTime.UtcNow.AddDays(-6)
                }
            };

            ServerOperations.Events().CreateEvent("PastEventInPastCustomRange", string.Empty, false, DateTime.Now.AddDays(-10), DateTime.Now.AddDays(-8));
            ServerOperations.Events().CreateEvent("PastEventInLessThanPastCustomRange", string.Empty, false, DateTime.Now.AddDays(-3), DateTime.Now.AddDays(-1));
            ServerOperations.Events().CreateEvent("PastEventInMoreThanPastCustomRange", string.Empty, false, DateTime.Now.AddDays(-18), DateTime.Now.AddDays(-16));

            ServerOperations.Events().CreateEvent("UpcomingEventInNextCustomRange", string.Empty, false, DateTime.Now.AddDays(7), DateTime.Now.AddDays(8));
            ServerOperations.Events().CreateEvent("UpcomingEventInLessThanNextCustomRange", string.Empty, false, DateTime.Now.AddDays(3), DateTime.Now.AddDays(4));
            ServerOperations.Events().CreateEvent("UpcomingEventInMoreThanNextCustomRange", string.Empty, false, DateTime.Now.AddDays(15), DateTime.Now.AddDays(16));

            var widgetId = this.AddControlFilters(this.CreatePage(), SelectionMode.FilteredItems, filterList, SelectionMode.AllItems, DefaultNarrowSelectionFilters);

            this.AssertEvent(widgetId, DateTime.MinValue, DateTime.MaxValue, 13);
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.SitefinityTeam8)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events_CustomRange_CurrentOrUpcomingToDateOrLast3Days()
        {
            var filterList = new List<AdditionalFilter>() 
            {
                new AdditionalFilter() 
                {
                    Filter = string.Empty,
                    FilterSection = FilterSection.Upcoming,
                    EndDate = DateTime.UtcNow.AddDays(12)
                },
                new AdditionalFilter() 
                {
                    Filter = CurrentFilterGroup,
                    FilterSection = FilterSection.Current
                },
                new AdditionalFilter() 
                {
                    Filter = Past3DaysFilterGroup,
                    FilterSection = FilterSection.Past
                }
            };

            ServerOperations.Events().CreateEvent("PastEventInLast3Days", string.Empty, false, DateTime.Now.AddDays(-2), DateTime.Now.AddDays(-1));
            ServerOperations.Events().CreateEvent("PastEventInMoreThanLast3Days", string.Empty, false, DateTime.Now.AddDays(-6), DateTime.Now.AddDays(-4));

            ServerOperations.Events().CreateEvent("UpcomingEventInNextCustomRange", string.Empty, false, DateTime.Now.AddDays(7), DateTime.Now.AddDays(8));
            ServerOperations.Events().CreateEvent("UpcomingEventInLessThanNextCustomRange", string.Empty, false, DateTime.Now.AddDays(3), DateTime.Now.AddDays(4));
            ServerOperations.Events().CreateEvent("UpcomingEventInMoreThanNextCustomRange", string.Empty, false, DateTime.Now.AddDays(15), DateTime.Now.AddDays(16));

            var widgetId = this.AddControlFilters(this.CreatePage(), SelectionMode.FilteredItems, filterList, SelectionMode.AllItems, DefaultNarrowSelectionFilters);

            this.AssertEvent(widgetId, DateTime.MinValue, DateTime.MaxValue, 17);
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.SitefinityTeam8)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events_CustomRange_CurrentOrUpcomingToDateOrLastWeek()
        {
            var filterList = new List<AdditionalFilter>() 
            {
                new AdditionalFilter() 
                {
                    Filter = string.Empty,
                    FilterSection = FilterSection.Upcoming,
                    EndDate = DateTime.UtcNow.AddDays(12)
                },
                new AdditionalFilter() 
                {
                    Filter = CurrentFilterGroup,
                    FilterSection = FilterSection.Current
                },
                new AdditionalFilter() 
                {
                    Filter = PastWeekFilterGroup,
                    FilterSection = FilterSection.Past
                }
            };

            ServerOperations.Events().CreateEvent("PastEventInLastWeek", string.Empty, false, DateTime.Now.AddDays(-6), DateTime.Now.AddDays(-4));
            ServerOperations.Events().CreateEvent("PastEventInMoreThanLastWeek", string.Empty, false, DateTime.Now.AddDays(-12), DateTime.Now.AddDays(-9));

            ServerOperations.Events().CreateEvent("UpcomingEventInNextCustomRange", string.Empty, false, DateTime.Now.AddDays(7), DateTime.Now.AddDays(8));
            ServerOperations.Events().CreateEvent("UpcomingEventInLessThanNextCustomRange", string.Empty, false, DateTime.Now.AddDays(3), DateTime.Now.AddDays(4));
            ServerOperations.Events().CreateEvent("UpcomingEventInMoreThanNextCustomRange", string.Empty, false, DateTime.Now.AddDays(15), DateTime.Now.AddDays(16));

            var widgetId = this.AddControlFilters(this.CreatePage(), SelectionMode.FilteredItems, filterList, SelectionMode.AllItems, DefaultNarrowSelectionFilters);

            this.AssertEvent(widgetId, DateTime.MinValue, DateTime.MaxValue, 17);
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.SitefinityTeam8)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events_CustomRange_CurrentOrUpcomingToDateOrLastMonth()
        {
            var filterList = new List<AdditionalFilter>() 
            {
                new AdditionalFilter() 
                {
                    Filter = string.Empty,
                    FilterSection = FilterSection.Upcoming,
                    EndDate = DateTime.UtcNow.AddDays(12)
                },
                new AdditionalFilter() 
                {
                    Filter = CurrentFilterGroup,
                    FilterSection = FilterSection.Current
                },
                new AdditionalFilter() 
                {
                    Filter = PastMonthFilterGroup,
                    FilterSection = FilterSection.Past
                }
            };

            ServerOperations.Events().CreateEvent("PastEventInLastMonth", string.Empty, false, DateTime.Now.AddDays(-22), DateTime.Now.AddDays(-20));
            ServerOperations.Events().CreateEvent("PastEventInMoreThanLastMonth", string.Empty, false, DateTime.Now.AddDays(-42), DateTime.Now.AddDays(-40));

            ServerOperations.Events().CreateEvent("UpcomingEventInNextCustomRange", string.Empty, false, DateTime.Now.AddDays(7), DateTime.Now.AddDays(8));
            ServerOperations.Events().CreateEvent("UpcomingEventInLessThanNextCustomRange", string.Empty, false, DateTime.Now.AddDays(3), DateTime.Now.AddDays(4));
            ServerOperations.Events().CreateEvent("UpcomingEventInMoreThanNextCustomRange", string.Empty, false, DateTime.Now.AddDays(15), DateTime.Now.AddDays(16));

            var widgetId = this.AddControlFilters(this.CreatePage(), SelectionMode.FilteredItems, filterList, SelectionMode.AllItems, DefaultNarrowSelectionFilters);

            this.AssertEvent(widgetId, DateTime.MinValue, DateTime.MaxValue, 17);
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.SitefinityTeam8)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events_CustomRange_CurrentOrUpcomingToDateOrLastYear()
        {
            var filterList = new List<AdditionalFilter>() 
            {
                new AdditionalFilter() 
                {
                    Filter = string.Empty,
                    FilterSection = FilterSection.Upcoming,
                    EndDate = DateTime.UtcNow.AddDays(12)
                },
                new AdditionalFilter() 
                {
                    Filter = CurrentFilterGroup,
                    FilterSection = FilterSection.Current
                },
                new AdditionalFilter() 
                {
                    Filter = PastYearFilterGroup,
                    FilterSection = FilterSection.Past
                }
            };

            ServerOperations.Events().CreateEvent("PastEventInLastYear", string.Empty, false, DateTime.Now.AddDays(-368), DateTime.Now.AddDays(-365));
            ServerOperations.Events().CreateEvent("PastEventInMoreThanLastYear", string.Empty, false, DateTime.Now.AddDays(-453), DateTime.Now.AddDays(-450));

            ServerOperations.Events().CreateEvent("UpcomingEventInNextCustomRange", string.Empty, false, DateTime.Now.AddDays(7), DateTime.Now.AddDays(8));
            ServerOperations.Events().CreateEvent("UpcomingEventInLessThanNextCustomRange", string.Empty, false, DateTime.Now.AddDays(3), DateTime.Now.AddDays(4));
            ServerOperations.Events().CreateEvent("UpcomingEventInMoreThanNextCustomRange", string.Empty, false, DateTime.Now.AddDays(15), DateTime.Now.AddDays(16));

            var widgetId = this.AddControlFilters(this.CreatePage(), SelectionMode.FilteredItems, filterList, SelectionMode.AllItems, DefaultNarrowSelectionFilters);

            this.AssertEvent(widgetId, DateTime.MinValue, DateTime.MaxValue, 17);
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.SitefinityTeam8)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events_CustomRange_CurrentOrUpcomingFromDateToDateOrPastFromDate()
        {
            var filterList = new List<AdditionalFilter>() 
            {
                new AdditionalFilter() 
                {
                    Filter = string.Empty,
                    FilterSection = FilterSection.Upcoming,
                    StartDate = DateTime.UtcNow.AddDays(6),
                    EndDate = DateTime.UtcNow.AddDays(12)
                },
                new AdditionalFilter() 
                {
                    Filter = CurrentFilterGroup,
                    FilterSection = FilterSection.Current
                },
                new AdditionalFilter() 
                {
                    Filter = string.Empty,
                    FilterSection = FilterSection.Past,
                    StartDate = DateTime.UtcNow.AddDays(-12)
                }
            };

            ServerOperations.Events().CreateEvent("PastEventInPastCustomRange", string.Empty, false, DateTime.Now.AddDays(-10), DateTime.Now.AddDays(-8));
            ServerOperations.Events().CreateEvent("PastEventInLessThanPastCustomRange", string.Empty, false, DateTime.Now.AddDays(-3), DateTime.Now.AddDays(-1));
            ServerOperations.Events().CreateEvent("PastEventInMoreThanPastCustomRange", string.Empty, false, DateTime.Now.AddDays(-18), DateTime.Now.AddDays(-16));

            ServerOperations.Events().CreateEvent("UpcomingEventInNextCustomRange", string.Empty, false, DateTime.Now.AddDays(7), DateTime.Now.AddDays(8));
            ServerOperations.Events().CreateEvent("UpcomingEventInLessThanNextCustomRange", string.Empty, false, DateTime.Now.AddDays(3), DateTime.Now.AddDays(4));
            ServerOperations.Events().CreateEvent("UpcomingEventInMoreThanNextCustomRange", string.Empty, false, DateTime.Now.AddDays(15), DateTime.Now.AddDays(16));

            var widgetId = this.AddControlFilters(this.CreatePage(), SelectionMode.FilteredItems, filterList, SelectionMode.AllItems, DefaultNarrowSelectionFilters);

            this.AssertEvent(widgetId, DateTime.MinValue, DateTime.MaxValue, 13);
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.SitefinityTeam8)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events_CustomRange_CurrentOrUpcomingFromDateToDateOrPastToDate()
        {
            var filterList = new List<AdditionalFilter>() 
            {
                new AdditionalFilter() 
                {
                    Filter = string.Empty,
                    FilterSection = FilterSection.Upcoming,
                    StartDate = DateTime.UtcNow.AddDays(6),
                    EndDate = DateTime.UtcNow.AddDays(12)
                },
                new AdditionalFilter() 
                {
                    Filter = CurrentFilterGroup,
                    FilterSection = FilterSection.Current
                },
                new AdditionalFilter() 
                {
                    Filter = string.Empty,
                    FilterSection = FilterSection.Past,
                    EndDate = DateTime.UtcNow.AddDays(-6)
                }
            };

            ServerOperations.Events().CreateEvent("PastEventInPastCustomRange", string.Empty, false, DateTime.Now.AddDays(-10), DateTime.Now.AddDays(-8));
            ServerOperations.Events().CreateEvent("PastEventInLessThanPastCustomRange", string.Empty, false, DateTime.Now.AddDays(-3), DateTime.Now.AddDays(-1));
            ServerOperations.Events().CreateEvent("PastEventInMoreThanPastCustomRange", string.Empty, false, DateTime.Now.AddDays(-18), DateTime.Now.AddDays(-16));

            ServerOperations.Events().CreateEvent("UpcomingEventInNextCustomRange", string.Empty, false, DateTime.Now.AddDays(7), DateTime.Now.AddDays(8));
            ServerOperations.Events().CreateEvent("UpcomingEventInLessThanNextCustomRange", string.Empty, false, DateTime.Now.AddDays(3), DateTime.Now.AddDays(4));
            ServerOperations.Events().CreateEvent("UpcomingEventInMoreThanNextCustomRange", string.Empty, false, DateTime.Now.AddDays(15), DateTime.Now.AddDays(16));

            var widgetId = this.AddControlFilters(this.CreatePage(), SelectionMode.FilteredItems, filterList, SelectionMode.AllItems, DefaultNarrowSelectionFilters);

            this.AssertEvent(widgetId, DateTime.MinValue, DateTime.MaxValue, 9);
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.SitefinityTeam8)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events_CustomRange_CurrentOrUpcomingFromDateToDateOrPastFromDateToDate()
        {
            var filterList = new List<AdditionalFilter>() 
            {
                new AdditionalFilter() 
                {
                    Filter = string.Empty,
                    FilterSection = FilterSection.Upcoming,
                    StartDate = DateTime.UtcNow.AddDays(6),
                    EndDate = DateTime.UtcNow.AddDays(12)
                },
                new AdditionalFilter() 
                {
                    Filter = CurrentFilterGroup,
                    FilterSection = FilterSection.Current
                },
                new AdditionalFilter() 
                {
                    Filter = string.Empty,
                    FilterSection = FilterSection.Past,
                    StartDate = DateTime.UtcNow.AddDays(-12),
                    EndDate = DateTime.UtcNow.AddDays(-6)
                }
            };

            ServerOperations.Events().CreateEvent("PastEventInPastCustomRange", string.Empty, false, DateTime.Now.AddDays(-10), DateTime.Now.AddDays(-8));
            ServerOperations.Events().CreateEvent("PastEventInLessThanPastCustomRange", string.Empty, false, DateTime.Now.AddDays(-3), DateTime.Now.AddDays(-1));
            ServerOperations.Events().CreateEvent("PastEventInMoreThanPastCustomRange", string.Empty, false, DateTime.Now.AddDays(-18), DateTime.Now.AddDays(-16));

            ServerOperations.Events().CreateEvent("UpcomingEventInNextCustomRange", string.Empty, false, DateTime.Now.AddDays(7), DateTime.Now.AddDays(8));
            ServerOperations.Events().CreateEvent("UpcomingEventInLessThanNextCustomRange", string.Empty, false, DateTime.Now.AddDays(3), DateTime.Now.AddDays(4));
            ServerOperations.Events().CreateEvent("UpcomingEventInMoreThanNextCustomRange", string.Empty, false, DateTime.Now.AddDays(15), DateTime.Now.AddDays(16));

            var widgetId = this.AddControlFilters(this.CreatePage(), SelectionMode.FilteredItems, filterList, SelectionMode.AllItems, DefaultNarrowSelectionFilters);

            this.AssertEvent(widgetId, DateTime.MinValue, DateTime.MaxValue, 8);
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.SitefinityTeam8)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events_CustomRange_CurrentOrUpcomingFromDateToDateOrLast3Days()
        {
            var filterList = new List<AdditionalFilter>() 
            {
                new AdditionalFilter() 
                {
                    Filter = string.Empty,
                    FilterSection = FilterSection.Upcoming,
                    StartDate = DateTime.UtcNow.AddDays(6),
                    EndDate = DateTime.UtcNow.AddDays(12)
                },
                new AdditionalFilter() 
                {
                    Filter = CurrentFilterGroup,
                    FilterSection = FilterSection.Current
                },
                new AdditionalFilter() 
                {
                    Filter = Past3DaysFilterGroup,
                    FilterSection = FilterSection.Past
                }
            };

            ServerOperations.Events().CreateEvent("PastEventInLast3Days", string.Empty, false, DateTime.Now.AddDays(-2), DateTime.Now.AddDays(-1));
            ServerOperations.Events().CreateEvent("PastEventInMoreThanLast3Days", string.Empty, false, DateTime.Now.AddDays(-6), DateTime.Now.AddDays(-4));

            ServerOperations.Events().CreateEvent("UpcomingEventInNextCustomRange", string.Empty, false, DateTime.Now.AddDays(7), DateTime.Now.AddDays(8));
            ServerOperations.Events().CreateEvent("UpcomingEventInLessThanNextCustomRange", string.Empty, false, DateTime.Now.AddDays(3), DateTime.Now.AddDays(4));
            ServerOperations.Events().CreateEvent("UpcomingEventInMoreThanNextCustomRange", string.Empty, false, DateTime.Now.AddDays(15), DateTime.Now.AddDays(16));

            var widgetId = this.AddControlFilters(this.CreatePage(), SelectionMode.FilteredItems, filterList, SelectionMode.AllItems, DefaultNarrowSelectionFilters);

            this.AssertEvent(widgetId, DateTime.MinValue, DateTime.MaxValue, 12);
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.SitefinityTeam8)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events_CustomRange_CurrentOrUpcomingFromDateToDateOrLastWeek()
        {
            var filterList = new List<AdditionalFilter>() 
            {
                new AdditionalFilter() 
                {
                    Filter = string.Empty,
                    FilterSection = FilterSection.Upcoming,
                    StartDate = DateTime.UtcNow.AddDays(6),
                    EndDate = DateTime.UtcNow.AddDays(12)
                },
                new AdditionalFilter() 
                {
                    Filter = CurrentFilterGroup,
                    FilterSection = FilterSection.Current
                },
                new AdditionalFilter() 
                {
                    Filter = PastWeekFilterGroup,
                    FilterSection = FilterSection.Past
                }
            };

            ServerOperations.Events().CreateEvent("PastEventInLastWeek", string.Empty, false, DateTime.Now.AddDays(-6), DateTime.Now.AddDays(-4));
            ServerOperations.Events().CreateEvent("PastEventInMoreThanLastWeek", string.Empty, false, DateTime.Now.AddDays(-12), DateTime.Now.AddDays(-9));

            ServerOperations.Events().CreateEvent("UpcomingEventInNextCustomRange", string.Empty, false, DateTime.Now.AddDays(7), DateTime.Now.AddDays(8));
            ServerOperations.Events().CreateEvent("UpcomingEventInLessThanNextCustomRange", string.Empty, false, DateTime.Now.AddDays(3), DateTime.Now.AddDays(4));
            ServerOperations.Events().CreateEvent("UpcomingEventInMoreThanNextCustomRange", string.Empty, false, DateTime.Now.AddDays(15), DateTime.Now.AddDays(16));

            var widgetId = this.AddControlFilters(this.CreatePage(), SelectionMode.FilteredItems, filterList, SelectionMode.AllItems, DefaultNarrowSelectionFilters);

            this.AssertEvent(widgetId, DateTime.MinValue, DateTime.MaxValue, 12);
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.SitefinityTeam8)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events_CustomRange_CurrentOrUpcomingFromDateToDateOrLastMonth()
        {
            var filterList = new List<AdditionalFilter>() 
            {
                new AdditionalFilter() 
                {
                    Filter = string.Empty,
                    FilterSection = FilterSection.Upcoming,
                    StartDate = DateTime.UtcNow.AddDays(6),
                    EndDate = DateTime.UtcNow.AddDays(12)
                },
                new AdditionalFilter() 
                {
                    Filter = CurrentFilterGroup,
                    FilterSection = FilterSection.Current
                },
                new AdditionalFilter() 
                {
                    Filter = PastMonthFilterGroup,
                    FilterSection = FilterSection.Past
                }
            };

            ServerOperations.Events().CreateEvent("PastEventInLastMonth", string.Empty, false, DateTime.Now.AddDays(-22), DateTime.Now.AddDays(-20));
            ServerOperations.Events().CreateEvent("PastEventInMoreThanLastMonth", string.Empty, false, DateTime.Now.AddDays(-42), DateTime.Now.AddDays(-40));

            ServerOperations.Events().CreateEvent("UpcomingEventInNextCustomRange", string.Empty, false, DateTime.Now.AddDays(7), DateTime.Now.AddDays(8));
            ServerOperations.Events().CreateEvent("UpcomingEventInLessThanNextCustomRange", string.Empty, false, DateTime.Now.AddDays(3), DateTime.Now.AddDays(4));
            ServerOperations.Events().CreateEvent("UpcomingEventInMoreThanNextCustomRange", string.Empty, false, DateTime.Now.AddDays(15), DateTime.Now.AddDays(16));

            var widgetId = this.AddControlFilters(this.CreatePage(), SelectionMode.FilteredItems, filterList, SelectionMode.AllItems, DefaultNarrowSelectionFilters);

            this.AssertEvent(widgetId, DateTime.MinValue, DateTime.MaxValue, 12);
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.SitefinityTeam8)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events_CustomRange_CurrentOrUpcomingFromDateToDateOrLastYear()
        {
            var filterList = new List<AdditionalFilter>() 
            {
                new AdditionalFilter() 
                {
                    Filter = string.Empty,
                    FilterSection = FilterSection.Upcoming,
                    StartDate = DateTime.UtcNow.AddDays(6),
                    EndDate = DateTime.UtcNow.AddDays(12)
                },
                new AdditionalFilter() 
                {
                    Filter = CurrentFilterGroup,
                    FilterSection = FilterSection.Current
                },
                new AdditionalFilter() 
                {
                    Filter = PastYearFilterGroup,
                    FilterSection = FilterSection.Past
                }
            };

            ServerOperations.Events().CreateEvent("PastEventInLastYear", string.Empty, false, DateTime.Now.AddDays(-368), DateTime.Now.AddDays(-365));
            ServerOperations.Events().CreateEvent("PastEventInMoreThanLastYear", string.Empty, false, DateTime.Now.AddDays(-453), DateTime.Now.AddDays(-450));

            ServerOperations.Events().CreateEvent("UpcomingEventInNextCustomRange", string.Empty, false, DateTime.Now.AddDays(7), DateTime.Now.AddDays(8));
            ServerOperations.Events().CreateEvent("UpcomingEventInLessThanNextCustomRange", string.Empty, false, DateTime.Now.AddDays(3), DateTime.Now.AddDays(4));
            ServerOperations.Events().CreateEvent("UpcomingEventInMoreThanNextCustomRange", string.Empty, false, DateTime.Now.AddDays(15), DateTime.Now.AddDays(16));

            var widgetId = this.AddControlFilters(this.CreatePage(), SelectionMode.FilteredItems, filterList, SelectionMode.AllItems, DefaultNarrowSelectionFilters);

            this.AssertEvent(widgetId, DateTime.MinValue, DateTime.MaxValue, 12);
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.SitefinityTeam8)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events_CustomRange_CurrentOrUpcoming3DaysOrPastFromDate()
        {
            var filterList = new List<AdditionalFilter>() 
            {
                new AdditionalFilter() 
                {
                    Filter = Upcoming3DaysFilterGroup,
                    FilterSection = FilterSection.Upcoming
                },
                new AdditionalFilter() 
                {
                    Filter = CurrentFilterGroup,
                    FilterSection = FilterSection.Current
                },
                new AdditionalFilter() 
                {
                    Filter = string.Empty,
                    FilterSection = FilterSection.Past,
                    StartDate = DateTime.UtcNow.AddDays(-12)
                }
            };

            ServerOperations.Events().CreateEvent("PastEventInPastCustomRange", string.Empty, false, DateTime.Now.AddDays(-10), DateTime.Now.AddDays(-8));
            ServerOperations.Events().CreateEvent("PastEventInLessThanPastCustomRange", string.Empty, false, DateTime.Now.AddDays(-3), DateTime.Now.AddDays(-1));
            ServerOperations.Events().CreateEvent("PastEventInMoreThanPastCustomRange", string.Empty, false, DateTime.Now.AddDays(-18), DateTime.Now.AddDays(-16));

            ServerOperations.Events().CreateEvent("UpcomingEventInNext3Days", string.Empty, false, DateTime.Now.AddDays(2), DateTime.Now.AddDays(3));
            ServerOperations.Events().CreateEvent("UpcomingEventInMoreThanNext3Days", string.Empty, false, DateTime.Now.AddDays(5), DateTime.Now.AddDays(7));

            var widgetId = this.AddControlFilters(this.CreatePage(), SelectionMode.FilteredItems, filterList, SelectionMode.AllItems, DefaultNarrowSelectionFilters);

            this.AssertEvent(widgetId, DateTime.MinValue, DateTime.MaxValue, 17);
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.SitefinityTeam8)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events_CustomRange_CurrentOrUpcomingWeekOrPastFromDate()
        {
            var filterList = new List<AdditionalFilter>() 
            {
                new AdditionalFilter() 
                {
                    Filter = UpcomingWeekFilterGroup,
                    FilterSection = FilterSection.Upcoming
                },
                new AdditionalFilter() 
                {
                    Filter = CurrentFilterGroup,
                    FilterSection = FilterSection.Current
                },
                new AdditionalFilter() 
                {
                    Filter = string.Empty,
                    FilterSection = FilterSection.Past,
                    StartDate = DateTime.UtcNow.AddDays(-12)
                }
            };

            ServerOperations.Events().CreateEvent("PastEventInPastCustomRange", string.Empty, false, DateTime.Now.AddDays(-10), DateTime.Now.AddDays(-8));
            ServerOperations.Events().CreateEvent("PastEventInLessThanPastCustomRange", string.Empty, false, DateTime.Now.AddDays(-3), DateTime.Now.AddDays(-1));
            ServerOperations.Events().CreateEvent("PastEventInMoreThanPastCustomRange", string.Empty, false, DateTime.Now.AddDays(-18), DateTime.Now.AddDays(-16));

            ServerOperations.Events().CreateEvent("UpcomingEventInNextWeek", string.Empty, false, DateTime.Now.AddDays(4), DateTime.Now.AddDays(6));
            ServerOperations.Events().CreateEvent("UpcomingEventInMoreThanNextWeek", string.Empty, false, DateTime.Now.AddDays(9), DateTime.Now.AddDays(12));

            var widgetId = this.AddControlFilters(this.CreatePage(), SelectionMode.FilteredItems, filterList, SelectionMode.AllItems, DefaultNarrowSelectionFilters);

            this.AssertEvent(widgetId, DateTime.MinValue, DateTime.MaxValue, 17);
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.SitefinityTeam8)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events_CustomRange_CurrentOrUpcomingMonthOrPastFromDate()
        {
            var filterList = new List<AdditionalFilter>() 
            {
                new AdditionalFilter() 
                {
                    Filter = UpcomingMonthFilterGroup,
                    FilterSection = FilterSection.Upcoming
                },
                new AdditionalFilter() 
                {
                    Filter = CurrentFilterGroup,
                    FilterSection = FilterSection.Current
                },
                new AdditionalFilter() 
                {
                    Filter = string.Empty,
                    FilterSection = FilterSection.Past,
                    StartDate = DateTime.UtcNow.AddDays(-12)
                }
            };

            ServerOperations.Events().CreateEvent("PastEventInPastCustomRange", string.Empty, false, DateTime.Now.AddDays(-10), DateTime.Now.AddDays(-8));
            ServerOperations.Events().CreateEvent("PastEventInLessThanPastCustomRange", string.Empty, false, DateTime.Now.AddDays(-3), DateTime.Now.AddDays(-1));
            ServerOperations.Events().CreateEvent("PastEventInMoreThanPastCustomRange", string.Empty, false, DateTime.Now.AddDays(-18), DateTime.Now.AddDays(-16));

            ServerOperations.Events().CreateEvent("UpcomingEventInNextMonth", string.Empty, false, DateTime.Now.AddDays(20), DateTime.Now.AddDays(22));
            ServerOperations.Events().CreateEvent("UpcomingEventInMoreThanNextMonth", string.Empty, false, DateTime.Now.AddDays(40), DateTime.Now.AddDays(42));

            var widgetId = this.AddControlFilters(this.CreatePage(), SelectionMode.FilteredItems, filterList, SelectionMode.AllItems, DefaultNarrowSelectionFilters);

            this.AssertEvent(widgetId, DateTime.MinValue, DateTime.MaxValue, 17);
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.SitefinityTeam8)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events_CustomRange_CurrentOrUpcomingYearOrPastFromDate()
        {
            var filterList = new List<AdditionalFilter>() 
            {
                new AdditionalFilter() 
                {
                    Filter = UpcomingYearFilterGroup,
                    FilterSection = FilterSection.Upcoming
                },
                new AdditionalFilter() 
                {
                    Filter = CurrentFilterGroup,
                    FilterSection = FilterSection.Current
                },
                new AdditionalFilter() 
                {
                    Filter = string.Empty,
                    FilterSection = FilterSection.Past,
                    StartDate = DateTime.UtcNow.AddDays(-12)
                }
            };

            ServerOperations.Events().CreateEvent("PastEventInPastCustomRange", string.Empty, false, DateTime.Now.AddDays(-10), DateTime.Now.AddDays(-8));
            ServerOperations.Events().CreateEvent("PastEventInLessThanPastCustomRange", string.Empty, false, DateTime.Now.AddDays(-3), DateTime.Now.AddDays(-1));
            ServerOperations.Events().CreateEvent("PastEventInMoreThanPastCustomRange", string.Empty, false, DateTime.Now.AddDays(-18), DateTime.Now.AddDays(-16));

            ServerOperations.Events().CreateEvent("UpcomingEventInNextYear", string.Empty, false, DateTime.Now.AddDays(365), DateTime.Now.AddDays(368));
            ServerOperations.Events().CreateEvent("UpcomingEventInMoreThanNextYear", string.Empty, false, DateTime.Now.AddDays(450), DateTime.Now.AddDays(453));

            var widgetId = this.AddControlFilters(this.CreatePage(), SelectionMode.FilteredItems, filterList, SelectionMode.AllItems, DefaultNarrowSelectionFilters);

            this.AssertEvent(widgetId, DateTime.MinValue, DateTime.MaxValue, 17);
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.SitefinityTeam8)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events_CustomRange_CurrentOrUpcoming3DaysOrPastToDate()
        {
            var filterList = new List<AdditionalFilter>() 
            {
                new AdditionalFilter() 
                {
                    Filter = Upcoming3DaysFilterGroup,
                    FilterSection = FilterSection.Upcoming
                },
                new AdditionalFilter() 
                {
                    Filter = CurrentFilterGroup,
                    FilterSection = FilterSection.Current
                },
                new AdditionalFilter() 
                {
                    Filter = string.Empty,
                    FilterSection = FilterSection.Past,
                    EndDate = DateTime.UtcNow.AddDays(-6)
                }
            };

            ServerOperations.Events().CreateEvent("PastEventInPastCustomRange", string.Empty, false, DateTime.Now.AddDays(-10), DateTime.Now.AddDays(-8));
            ServerOperations.Events().CreateEvent("PastEventInLessThanPastCustomRange", string.Empty, false, DateTime.Now.AddDays(-3), DateTime.Now.AddDays(-1));
            ServerOperations.Events().CreateEvent("PastEventInMoreThanPastCustomRange", string.Empty, false, DateTime.Now.AddDays(-18), DateTime.Now.AddDays(-16));

            ServerOperations.Events().CreateEvent("UpcomingEventInNext3Days", string.Empty, false, DateTime.Now.AddDays(2), DateTime.Now.AddDays(3));
            ServerOperations.Events().CreateEvent("UpcomingEventInMoreThanNext3Days", string.Empty, false, DateTime.Now.AddDays(5), DateTime.Now.AddDays(7));

            var widgetId = this.AddControlFilters(this.CreatePage(), SelectionMode.FilteredItems, filterList, SelectionMode.AllItems, DefaultNarrowSelectionFilters);

            this.AssertEvent(widgetId, DateTime.MinValue, DateTime.MaxValue, 13);
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.SitefinityTeam8)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events_CustomRange_CurrentOrUpcomingWeekOrPastToDate()
        {
            var filterList = new List<AdditionalFilter>() 
            {
                new AdditionalFilter() 
                {
                    Filter = UpcomingWeekFilterGroup,
                    FilterSection = FilterSection.Upcoming
                },
                new AdditionalFilter() 
                {
                    Filter = CurrentFilterGroup,
                    FilterSection = FilterSection.Current
                },
                new AdditionalFilter() 
                {
                    Filter = string.Empty,
                    FilterSection = FilterSection.Past,
                    EndDate = DateTime.UtcNow.AddDays(-6)
                }
            };

            ServerOperations.Events().CreateEvent("PastEventInPastCustomRange", string.Empty, false, DateTime.Now.AddDays(-10), DateTime.Now.AddDays(-8));
            ServerOperations.Events().CreateEvent("PastEventInLessThanPastCustomRange", string.Empty, false, DateTime.Now.AddDays(-3), DateTime.Now.AddDays(-1));
            ServerOperations.Events().CreateEvent("PastEventInMoreThanPastCustomRange", string.Empty, false, DateTime.Now.AddDays(-18), DateTime.Now.AddDays(-16));

            ServerOperations.Events().CreateEvent("UpcomingEventInNextWeek", string.Empty, false, DateTime.Now.AddDays(4), DateTime.Now.AddDays(6));
            ServerOperations.Events().CreateEvent("UpcomingEventInMoreThanNextWeek", string.Empty, false, DateTime.Now.AddDays(9), DateTime.Now.AddDays(12));

            var widgetId = this.AddControlFilters(this.CreatePage(), SelectionMode.FilteredItems, filterList, SelectionMode.AllItems, DefaultNarrowSelectionFilters);

            this.AssertEvent(widgetId, DateTime.MinValue, DateTime.MaxValue, 13);
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.SitefinityTeam8)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events_CustomRange_CurrentOrUpcomingMonthOrPastToDate()
        {
            var filterList = new List<AdditionalFilter>() 
            {
                new AdditionalFilter() 
                {
                    Filter = UpcomingMonthFilterGroup,
                    FilterSection = FilterSection.Upcoming
                },
                new AdditionalFilter() 
                {
                    Filter = CurrentFilterGroup,
                    FilterSection = FilterSection.Current
                },
                new AdditionalFilter() 
                {
                    Filter = string.Empty,
                    FilterSection = FilterSection.Past,
                    EndDate = DateTime.UtcNow.AddDays(-6)
                }
            };

            ServerOperations.Events().CreateEvent("PastEventInPastCustomRange", string.Empty, false, DateTime.Now.AddDays(-10), DateTime.Now.AddDays(-8));
            ServerOperations.Events().CreateEvent("PastEventInLessThanPastCustomRange", string.Empty, false, DateTime.Now.AddDays(-3), DateTime.Now.AddDays(-1));
            ServerOperations.Events().CreateEvent("PastEventInMoreThanPastCustomRange", string.Empty, false, DateTime.Now.AddDays(-18), DateTime.Now.AddDays(-16));

            ServerOperations.Events().CreateEvent("UpcomingEventInNextMonth", string.Empty, false, DateTime.Now.AddDays(20), DateTime.Now.AddDays(22));
            ServerOperations.Events().CreateEvent("UpcomingEventInMoreThanNextMonth", string.Empty, false, DateTime.Now.AddDays(40), DateTime.Now.AddDays(42));

            var widgetId = this.AddControlFilters(this.CreatePage(), SelectionMode.FilteredItems, filterList, SelectionMode.AllItems, DefaultNarrowSelectionFilters);

            this.AssertEvent(widgetId, DateTime.MinValue, DateTime.MaxValue, 13);
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.SitefinityTeam8)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events_CustomRange_CurrentOrUpcomingYearOrPastToDate()
        {
            var filterList = new List<AdditionalFilter>() 
            {
                new AdditionalFilter() 
                {
                    Filter = UpcomingYearFilterGroup,
                    FilterSection = FilterSection.Upcoming
                },
                new AdditionalFilter() 
                {
                    Filter = CurrentFilterGroup,
                    FilterSection = FilterSection.Current
                },
                new AdditionalFilter() 
                {
                    Filter = string.Empty,
                    FilterSection = FilterSection.Past,
                    EndDate = DateTime.UtcNow.AddDays(-6)
                }
            };

            ServerOperations.Events().CreateEvent("PastEventInPastCustomRange", string.Empty, false, DateTime.Now.AddDays(-10), DateTime.Now.AddDays(-8));
            ServerOperations.Events().CreateEvent("PastEventInLessThanPastCustomRange", string.Empty, false, DateTime.Now.AddDays(-3), DateTime.Now.AddDays(-1));
            ServerOperations.Events().CreateEvent("PastEventInMoreThanPastCustomRange", string.Empty, false, DateTime.Now.AddDays(-18), DateTime.Now.AddDays(-16));

            ServerOperations.Events().CreateEvent("UpcomingEventInNextYear", string.Empty, false, DateTime.Now.AddDays(365), DateTime.Now.AddDays(368));
            ServerOperations.Events().CreateEvent("UpcomingEventInMoreThanNextYear", string.Empty, false, DateTime.Now.AddDays(450), DateTime.Now.AddDays(453));

            var widgetId = this.AddControlFilters(this.CreatePage(), SelectionMode.FilteredItems, filterList, SelectionMode.AllItems, DefaultNarrowSelectionFilters);

            this.AssertEvent(widgetId, DateTime.MinValue, DateTime.MaxValue, 13);
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.SitefinityTeam8)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events_CustomRange_CurrentOrUpcoming3DaysOrPastFromDateToDate()
        {
            var filterList = new List<AdditionalFilter>() 
            {
                new AdditionalFilter() 
                {
                    Filter = Upcoming3DaysFilterGroup,
                    FilterSection = FilterSection.Upcoming
                },
                new AdditionalFilter() 
                {
                    Filter = CurrentFilterGroup,
                    FilterSection = FilterSection.Current
                },
                new AdditionalFilter() 
                {
                    Filter = string.Empty,
                    FilterSection = FilterSection.Past,
                    StartDate = DateTime.UtcNow.AddDays(-12),
                    EndDate = DateTime.UtcNow.AddDays(-6)
                }
            };

            ServerOperations.Events().CreateEvent("PastEventInPastCustomRange", string.Empty, false, DateTime.Now.AddDays(-10), DateTime.Now.AddDays(-8));
            ServerOperations.Events().CreateEvent("PastEventInLessThanPastCustomRange", string.Empty, false, DateTime.Now.AddDays(-3), DateTime.Now.AddDays(-1));
            ServerOperations.Events().CreateEvent("PastEventInMoreThanPastCustomRange", string.Empty, false, DateTime.Now.AddDays(-18), DateTime.Now.AddDays(-16));

            ServerOperations.Events().CreateEvent("UpcomingEventInNext3Days", string.Empty, false, DateTime.Now.AddDays(2), DateTime.Now.AddDays(3));
            ServerOperations.Events().CreateEvent("UpcomingEventInMoreThanNext3Days", string.Empty, false, DateTime.Now.AddDays(5), DateTime.Now.AddDays(7));

            var widgetId = this.AddControlFilters(this.CreatePage(), SelectionMode.FilteredItems, filterList, SelectionMode.AllItems, DefaultNarrowSelectionFilters);

            this.AssertEvent(widgetId, DateTime.MinValue, DateTime.MaxValue, 12);
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.SitefinityTeam8)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events_CustomRange_CurrentOrUpcomingWeekOrPastFromDateToDate()
        {
            var filterList = new List<AdditionalFilter>() 
            {
                new AdditionalFilter() 
                {
                    Filter = UpcomingWeekFilterGroup,
                    FilterSection = FilterSection.Upcoming
                },
                new AdditionalFilter() 
                {
                    Filter = CurrentFilterGroup,
                    FilterSection = FilterSection.Current
                },
                new AdditionalFilter() 
                {
                    Filter = string.Empty,
                    FilterSection = FilterSection.Past,
                    StartDate = DateTime.UtcNow.AddDays(-12),
                    EndDate = DateTime.UtcNow.AddDays(-6)
                }
            };

            ServerOperations.Events().CreateEvent("PastEventInPastCustomRange", string.Empty, false, DateTime.Now.AddDays(-10), DateTime.Now.AddDays(-8));
            ServerOperations.Events().CreateEvent("PastEventInLessThanPastCustomRange", string.Empty, false, DateTime.Now.AddDays(-3), DateTime.Now.AddDays(-1));
            ServerOperations.Events().CreateEvent("PastEventInMoreThanPastCustomRange", string.Empty, false, DateTime.Now.AddDays(-18), DateTime.Now.AddDays(-16));

            ServerOperations.Events().CreateEvent("UpcomingEventInNextWeek", string.Empty, false, DateTime.Now.AddDays(4), DateTime.Now.AddDays(6));
            ServerOperations.Events().CreateEvent("UpcomingEventInMoreThanNextWeek", string.Empty, false, DateTime.Now.AddDays(9), DateTime.Now.AddDays(12));

            var widgetId = this.AddControlFilters(this.CreatePage(), SelectionMode.FilteredItems, filterList, SelectionMode.AllItems, DefaultNarrowSelectionFilters);

            this.AssertEvent(widgetId, DateTime.MinValue, DateTime.MaxValue, 12);
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.SitefinityTeam8)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events_CustomRange_CurrentOrUpcomingMonthOrPastFromDateToDate()
        {
            var filterList = new List<AdditionalFilter>() 
            {
                new AdditionalFilter() 
                {
                    Filter = UpcomingMonthFilterGroup,
                    FilterSection = FilterSection.Upcoming
                },
                new AdditionalFilter() 
                {
                    Filter = CurrentFilterGroup,
                    FilterSection = FilterSection.Current
                },
                new AdditionalFilter() 
                {
                    Filter = string.Empty,
                    FilterSection = FilterSection.Past,
                    StartDate = DateTime.UtcNow.AddDays(-12),
                    EndDate = DateTime.UtcNow.AddDays(-6)
                }
            };

            ServerOperations.Events().CreateEvent("PastEventInPastCustomRange", string.Empty, false, DateTime.Now.AddDays(-10), DateTime.Now.AddDays(-8));
            ServerOperations.Events().CreateEvent("PastEventInLessThanPastCustomRange", string.Empty, false, DateTime.Now.AddDays(-3), DateTime.Now.AddDays(-1));
            ServerOperations.Events().CreateEvent("PastEventInMoreThanPastCustomRange", string.Empty, false, DateTime.Now.AddDays(-18), DateTime.Now.AddDays(-16));

            ServerOperations.Events().CreateEvent("UpcomingEventInNextMonth", string.Empty, false, DateTime.Now.AddDays(20), DateTime.Now.AddDays(22));
            ServerOperations.Events().CreateEvent("UpcomingEventInMoreThanNextMonth", string.Empty, false, DateTime.Now.AddDays(40), DateTime.Now.AddDays(42));

            var widgetId = this.AddControlFilters(this.CreatePage(), SelectionMode.FilteredItems, filterList, SelectionMode.AllItems, DefaultNarrowSelectionFilters);

            this.AssertEvent(widgetId, DateTime.MinValue, DateTime.MaxValue, 12);
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.SitefinityTeam8)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events_CustomRange_CurrentOrUpcomingYearOrPastFromDateToDate()
        {
            var filterList = new List<AdditionalFilter>() 
            {
                new AdditionalFilter() 
                {
                    Filter = UpcomingYearFilterGroup,
                    FilterSection = FilterSection.Upcoming
                },
                new AdditionalFilter() 
                {
                    Filter = CurrentFilterGroup,
                    FilterSection = FilterSection.Current
                },
                new AdditionalFilter() 
                {
                    Filter = string.Empty,
                    FilterSection = FilterSection.Past,
                    StartDate = DateTime.UtcNow.AddDays(-12),
                    EndDate = DateTime.UtcNow.AddDays(-6)
                }
            };

            ServerOperations.Events().CreateEvent("PastEventInPastCustomRange", string.Empty, false, DateTime.Now.AddDays(-10), DateTime.Now.AddDays(-8));
            ServerOperations.Events().CreateEvent("PastEventInLessThanPastCustomRange", string.Empty, false, DateTime.Now.AddDays(-3), DateTime.Now.AddDays(-1));
            ServerOperations.Events().CreateEvent("PastEventInMoreThanPastCustomRange", string.Empty, false, DateTime.Now.AddDays(-18), DateTime.Now.AddDays(-16));

            ServerOperations.Events().CreateEvent("UpcomingEventInNextYear", string.Empty, false, DateTime.Now.AddDays(365), DateTime.Now.AddDays(368));
            ServerOperations.Events().CreateEvent("UpcomingEventInMoreThanNextYear", string.Empty, false, DateTime.Now.AddDays(450), DateTime.Now.AddDays(453));

            var widgetId = this.AddControlFilters(this.CreatePage(), SelectionMode.FilteredItems, filterList, SelectionMode.AllItems, DefaultNarrowSelectionFilters);

            this.AssertEvent(widgetId, DateTime.MinValue, DateTime.MaxValue, 12);
        }

        #endregion

        #region Events - Selected events

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.SitefinityTeam8)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events_SelectedEvents()
        {
            var filterList = new List<AdditionalFilter>() 
            {
                new AdditionalFilter() 
                {
                    Filter = DefaultFilterGroup,
                    FilterSection = FilterSection.Default
                }
            };

            List<Guid> eventListIDs = new List<Guid>() 
            { 
                ServerOperations.Events().CreateEvent("UpcomingEventInNext3Days", string.Empty, false, DateTime.Now.AddDays(2), DateTime.Now.AddDays(3)),
                ServerOperations.Events().CreateEvent("UpcomingEventInMoreThanNext3Days", string.Empty, false, DateTime.Now.AddDays(5), DateTime.Now.AddDays(7))
            };

            var widgetId = this.AddControlFilters(this.CreatePage(), SelectionMode.SelectedItems, filterList, SelectionMode.AllItems, DefaultNarrowSelectionFilters, eventListIDs);

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

            var filterList = new List<AdditionalFilter>() 
            {
                new AdditionalFilter() 
                {
                    Filter = DefaultFilterGroup,
                    FilterSection = FilterSection.Default
                }
            };

            var widgetId = this.AddControlFilters(this.CreatePage(), SelectionMode.AllItems, filterList, SelectionMode.FilteredItems, filter);

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

            var filterList = new List<AdditionalFilter>() 
            {
                new AdditionalFilter() 
                {
                    Filter = DefaultFilterGroup,
                    FilterSection = FilterSection.Default
                }
            };

            var widgetId = this.AddControlFilters(this.CreatePage(), SelectionMode.AllItems, filterList, SelectionMode.FilteredItems, filter);

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

            var filterList = new List<AdditionalFilter>() 
            {
                new AdditionalFilter() 
                {
                    Filter = DefaultFilterGroup,
                    FilterSection = FilterSection.Default
                }
            };

            var widgetId = this.AddControlFilters(this.CreatePage(), SelectionMode.AllItems, filterList, SelectionMode.FilteredItems, filter);

            this.AssertEvent(widgetId, DateTime.MinValue, DateTime.MaxValue, 2);
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.SitefinityTeam8)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        [Ignore("Unstable: Story id: 207129")]
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

            var filterList = new List<AdditionalFilter>() 
            {
                new AdditionalFilter() 
                {
                    Filter = DefaultFilterGroup,
                    FilterSection = FilterSection.Default
                }
            };

            var widgetId = this.AddControlFilters(this.CreatePage(), SelectionMode.AllItems, filterList, SelectionMode.FilteredItems, filter);

            this.AssertEvent(widgetId, DateTime.MinValue, DateTime.MaxValue, 2);
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.SitefinityTeam8)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        [Ignore("Unstable: Story id: 207133")]
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

            var filterList = new List<AdditionalFilter>() 
            {
                new AdditionalFilter() 
                {
                    Filter = DefaultFilterGroup,
                    FilterSection = FilterSection.Default
                }
            };

            var widgetId = this.AddControlFilters(this.CreatePage(), SelectionMode.AllItems, filterList, SelectionMode.FilteredItems, filter);

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

            var filterList = new List<AdditionalFilter>() 
            {
                new AdditionalFilter() 
                {
                    Filter = DefaultFilterGroup,
                    FilterSection = FilterSection.Default
                }
            };

            var widgetId = this.AddControlFilters(this.CreatePage(), SelectionMode.AllItems, filterList, SelectionMode.FilteredItems, filter);

            this.AssertEvent(widgetId, DateTime.MinValue, DateTime.MaxValue, 1);
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.SitefinityTeam8)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        [Ignore("Unstable: Story id: 207131")]
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

            var filterList = new List<AdditionalFilter>() 
            {
                new AdditionalFilter() 
                {
                    Filter = DefaultFilterGroup,
                    FilterSection = FilterSection.Default
                }
            };

            var widgetId = this.AddControlFilters(this.CreatePage(), SelectionMode.AllItems, filterList, SelectionMode.FilteredItems, filter);

            this.AssertEvent(widgetId, DateTime.MinValue, DateTime.MaxValue, 1);
        }

        #endregion

        #region Calendars

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.SitefinityTeam8)]
        [Description("Ensure that after calendar request, data is correctly retrieved as json content")]
        [Ignore("Unstable: Story id: 207132")]
        public void EventSchedulerWidget_Route_Calendar()
        {
            var manager = EventsManager.GetManager();
            var calendartDefault = manager.GetCalendars().FirstOrDefault(p => p.Id == this.defaultCalendarId);
            this.SetCalendarToEvent(manager, BaseUpcomingEventTitle, calendartDefault.Title);
            this.SetCalendarToEvent(manager, BaseRepeatEventTitle, EventSchedulerWidgetTests.Calendar1Title);
            this.SetCalendarToEvent(manager, BasePastEventTitle, EventSchedulerWidgetTests.Calendar2Title);

            var filterList = new List<AdditionalFilter>() 
            {
                new AdditionalFilter() 
                {
                    Filter = DefaultFilterGroup,
                    FilterSection = FilterSection.Default
                }
            };

            var widgetId = this.AddControlFilters(this.CreatePage(), SelectionMode.AllItems, filterList, SelectionMode.AllItems, DefaultNarrowSelectionFilters);

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

            var filterList = new List<AdditionalFilter>() 
            {
                new AdditionalFilter() 
                {
                    Filter = DefaultFilterGroup,
                    FilterSection = FilterSection.Default
                }
            };

            var widgetId = this.AddControlFilters(this.CreatePage(bulgarian), SelectionMode.AllItems, filterList, SelectionMode.AllItems, DefaultNarrowSelectionFilters);

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

        private void AssertCalendar(Guid widgetId, DateTime filterStartDate, DateTime filterEndDate, long expectedCalendarOccurrences, CultureInfo culture = null)
        {
            var filters = this.CreateFilterQueryString(widgetId, filterStartDate, filterEndDate, culture);
            var pageContent = WebRequestHelper.GetPageWebContent(UrlPath.ResolveAbsoluteUrl("~/web-interface/calendars/?") + filters);

            Assert.DoesNotContain(pageContent, JsonExceptionMessage, JsonExceptionMessage);
            List<EventCalendarViewModel> calendarOccurrenceList = JsonSerializer.DeserializeFromString<List<EventCalendarViewModel>>(pageContent);
            Assert.AreEqual(expectedCalendarOccurrences, calendarOccurrenceList.LongCount());
        }

        private void AssertEvent(Guid widgetId, DateTime filterStartDate, DateTime filterEndDate, long expectedEventOccurrences, CultureInfo culture = null)
        {
            var filters = this.CreateFilterQueryString(widgetId, filterStartDate, filterEndDate, culture);
            var pageContent = WebRequestHelper.GetPageWebContent(UrlPath.ResolveAbsoluteUrl("/web-interface/events/?") + filters);

            Assert.DoesNotContain(pageContent, JsonExceptionMessage, JsonExceptionMessage);
            List<EventOccurrenceViewModel> eventOccurrenceList = JsonSerializer.DeserializeFromString<List<EventOccurrenceViewModel>>(pageContent);
            Assert.AreEqual(expectedEventOccurrences, eventOccurrenceList.LongCount());
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
            var selectedEvent = manager.GetEvents().Where(i => i.Title == eventTitle).FirstOrDefault(p => p.Status == Telerik.Sitefinity.GenericContent.Model.ContentLifecycleStatus.Master);
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
        private Guid AddControlFilters(Guid pageId, SelectionMode additionalFiltersSelectionMode, List<AdditionalFilter> filterList, SelectionMode narrowFiltersSelectionMode, string narrowSelectionFilters, List<Guid> selectedItemsIds = null)
        {
            var mvcProxy = new MvcControllerProxy();
            mvcProxy.ControllerName = typeof(EventSchedulerController).FullName;
            var eventSchedulerController = new EventSchedulerController();
            this.ApplyFilters(eventSchedulerController, additionalFiltersSelectionMode, this.CreateAdditionalFilter(filterList), narrowFiltersSelectionMode, narrowSelectionFilters, selectedItemsIds);
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

        private void ClearData()
        {
            Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.Pages().DeleteAllPages();
            Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.Events().DeleteAllEvents();

            foreach (var item in EventsManager.GetManager().GetCalendars())
            {
                if (item.Id != this.defaultCalendarId)
                {
                    ServerOperations.Events().DeleteCalendar(item.Id);
                }
            }

            ServerOperations.Taxonomies().ClearAllCategories(TaxonomiesConstants.CategoriesTaxonomyId);
            ServerOperations.Taxonomies().ClearAllTags(TaxonomiesConstants.TagsTaxonomyId);
        }

        private string CreateAdditionalFilter(List<AdditionalFilter> filterList)
        {
            var staticTopSection = "{\"__msdisposeindex\":452,\"Title\":null,\"Id\":\"00000000-0000-0000-0000-000000000000\",\"QueryItems\":[";
            var staticBottomSection = "],\"TypeProperties\":[],\"_itemPathSeparator\":\"_\"}";

            var filter = staticTopSection;

            for (var i = 0; i < filterList.Count; i++)
            {
                if (filterList[i].StartDate != null || filterList[i].EndDate != null)
                {
                    var tempFilter = this.CreateFilterForEventWithCustomRange(filterList[i].StartDate, filterList[i].EndDate, filterList[i].FilterSection);
                    filter += tempFilter.Replace("$VARINDEXTOREPLACE", i.ToString(CultureInfo.InvariantCulture));
                }
                else
                {
                    filter += filterList[i].Filter.Replace("$VARINDEXTOREPLACE", i.ToString(CultureInfo.InvariantCulture));
                }

                if (i < (filterList.Count - 1))
                {
                    filter += ",";
                }
            }

            filter += staticBottomSection;

            return filter;
        }

        private string CreateFilterForEventWithCustomRange(DateTime? start, DateTime? end, FilterSection filterSection)
        {
            string firstPart;
            string lastPart;
            string lastPartForBothStartAndEnd;
            var middlePart = string.Empty;

            var middleStart = "{\"IsGroup\":false,\"Ordinal\":0,\"Join\":\"AND\",\"ItemPath\":\"_$VARINDEXTOREPLACE_0\",\"Value\":\"";
            string middleBetween1;
            string middleBetween2;
            var midleEnd = "\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":432},";

            var startOperator = ">=";
            var endOperator = "<=";

            if (filterSection == FilterSection.Upcoming)
            {
                firstPart = "{\"IsGroup\":true,\"Ordinal\":$VARINDEXTOREPLACE,\"Join\":\"OR\",\"ItemPath\":\"_$VARINDEXTOREPLACE\",\"Value\":null,\"Condition\":{\"FieldName\":null,\"FieldType\":null,\"Operator\":null,\"__msdisposeindex\":431},\"Name\":\"Upcoming\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":430},";
                lastPart = "{\"IsGroup\":false,\"Ordinal\":1,\"Join\":\"AND\",\"ItemPath\":\"_$VARINDEXTOREPLACE_1\",\"Value\":\"DateTime.UtcNow\",\"Condition\":{\"FieldName\":\"EventStart\",\"FieldType\":\"System.DateTime\",\"Operator\":\">=\",\"__msdisposeindex\":435},\"Name\":null,\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":434}";
                lastPartForBothStartAndEnd = "{\"IsGroup\":false,\"Ordinal\":2,\"Join\":\"AND\",\"ItemPath\":\"_$VARINDEXTOREPLACE_1\",\"Value\":\"DateTime.UtcNow\",\"Condition\":{\"FieldName\":\"EventStart\",\"FieldType\":\"System.DateTime\",\"Operator\":\">=\",\"__msdisposeindex\":435},\"Name\":null,\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":434}";
                middleBetween1 = "\",\"Condition\":{\"FieldName\":\"EventStart\",\"FieldType\":\"System.DateTime\",\"Operator\":\"";
                middleBetween2 = "\",\"__msdisposeindex\":433},\"Name\":\"EventStart.";
            }
            else
            {
                firstPart = "{\"IsGroup\":true,\"Ordinal\":$VARINDEXTOREPLACE,\"Join\":\"OR\",\"ItemPath\":\"_$VARINDEXTOREPLACE\",\"Value\":null,\"Condition\":{\"FieldName\":null,\"FieldType\":null,\"Operator\":null,\"__msdisposeindex\":431},\"Name\":\"Past\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":430},";
                lastPart = "{\"IsGroup\":false,\"Ordinal\":1,\"Join\":\"AND\",\"ItemPath\":\"_$VARINDEXTOREPLACE_1\",\"Value\":\"DateTime.UtcNow\",\"Condition\":{\"FieldName\":\"EventEnd\",\"FieldType\":\"System.DateTime\",\"Operator\":\"<=\",\"__msdisposeindex\":435},\"Name\":null,\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":434}";
                lastPartForBothStartAndEnd = "{\"IsGroup\":false,\"Ordinal\":2,\"Join\":\"AND\",\"ItemPath\":\"_$VARINDEXTOREPLACE_1\",\"Value\":\"DateTime.UtcNow\",\"Condition\":{\"FieldName\":\"EventEnd\",\"FieldType\":\"System.DateTime\",\"Operator\":\"<=\",\"__msdisposeindex\":435},\"Name\":null,\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":434}";
                middleBetween1 = "\",\"Condition\":{\"FieldName\":\"EventEnd\",\"FieldType\":\"System.DateTime\",\"Operator\":\"";
                middleBetween2 = "\",\"__msdisposeindex\":433},\"Name\":\"EventEnd.";
            }

            if (start.HasValue)
            {
                ////Sat, 08 Oct 2016 21:00:00 GMT - RFC1123 format
                var formatedStartDate = string.Format(CultureInfo.InvariantCulture, "{0:r}", start);

                middlePart += middleStart;
                middlePart += formatedStartDate;
                middlePart += middleBetween1 + startOperator + middleBetween2;
                middlePart += formatedStartDate;
                middlePart += midleEnd;
            }

            if (end.HasValue)
            {
                if (start.HasValue)
                {
                    if (end < start)
                    {
                        return firstPart + middlePart + lastPart;
                    }

                    ////Sat, 08 Oct 2016 21:00:00 GMT - RFC1123 format
                    var formatedEndDate = string.Format(CultureInfo.InvariantCulture, "{0:r}", end);

                    middlePart += middleStart;
                    middlePart += formatedEndDate;
                    middlePart += middleBetween1 + endOperator + middleBetween2;
                    middlePart += formatedEndDate;
                    middlePart += midleEnd;

                    return firstPart + middlePart + lastPartForBothStartAndEnd;
                }
                else
                {
                    ////Sat, 08 Oct 2016 21:00:00 GMT - RFC1123 format
                    var formatedEndDate = string.Format(CultureInfo.InvariantCulture, "{0:r}", end);

                    middlePart += middleStart;
                    middlePart += formatedEndDate;
                    middlePart += middleBetween1 + endOperator + middleBetween2;
                    middlePart += formatedEndDate;
                    middlePart += midleEnd;
                }
            }

            return firstPart + middlePart + lastPart;
        }

        #endregion

        #region Filters

        private const string DefaultNarrowSelectionFilters = "{\"__msdisposeindex\":257,\"Title\":null,\"Id\":\"00000000-0000-0000-0000-000000000000\",\"QueryItems\":[],\"TypeProperties\":[],\"_itemPathSeparator\":\"_\"}";
        private const string DefaultAdditionalFilters = "{\"__msdisposeindex\":256,\"Title\":null,\"Id\":\"00000000-0000-0000-0000-000000000000\",\"QueryItems\":[],\"TypeProperties\":[],\"_itemPathSeparator\":\"_\"}";

        private const string DefaultFilterGroup = "";
        private const string CurrentFilterGroup = "{\"IsGroup\":true,\"Ordinal\":$VARINDEXTOREPLACE,\"Join\":\"OR\",\"ItemPath\":\"_$VARINDEXTOREPLACE\",\"Value\":null,\"Condition\":{\"FieldName\":null,\"FieldType\":null,\"Operator\":null,\"__msdisposeindex\":269},\"Name\":\"Current\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":268},{\"IsGroup\":false,\"Ordinal\":0,\"Join\":\"AND\",\"ItemPath\":\"_$VARINDEXTOREPLACE_0\",\"Value\":\"DateTime.UtcNow\",\"Condition\":{\"FieldName\":\"EventStart\",\"FieldType\":\"System.DateTime\",\"Operator\":\"<=\",\"__msdisposeindex\":271},\"Name\":null,\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":270},{\"IsGroup\":false,\"Ordinal\":1,\"Join\":\"AND\",\"ItemPath\":\"_$VARINDEXTOREPLACE_1\",\"Value\":\"DateTime.UtcNow\",\"Condition\":{\"FieldName\":\"EventEnd\",\"FieldType\":\"System.DateTime\",\"Operator\":\">=\",\"__msdisposeindex\":273},\"Name\":null,\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":272}";
        private const string UpcomingFilterGroup = "{\"IsGroup\":true,\"Ordinal\":$VARINDEXTOREPLACE,\"Join\":\"OR\",\"ItemPath\":\"_$VARINDEXTOREPLACE\",\"Value\":null,\"Condition\":{\"FieldName\":null,\"FieldType\":null,\"Operator\":null,\"__msdisposeindex\":449},\"Name\":\"Upcoming\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":448},{\"IsGroup\":false,\"Ordinal\":0,\"Join\":\"AND\",\"ItemPath\":\"_$VARINDEXTOREPLACE_0\",\"Value\":\"DateTime.UtcNow\",\"Condition\":{\"FieldName\":\"EventStart\",\"FieldType\":\"System.DateTime\",\"Operator\":\">=\",\"__msdisposeindex\":451},\"Name\":null,\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":450}";
        private const string Upcoming3DaysFilterGroup = "{\"IsGroup\":true,\"Ordinal\":$VARINDEXTOREPLACE,\"Join\":\"OR\",\"ItemPath\":\"_$VARINDEXTOREPLACE\",\"Value\":null,\"Condition\":{\"FieldName\":null,\"FieldType\":null,\"Operator\":null,\"__msdisposeindex\":272},\"Name\":\"Upcoming\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":271},{\"IsGroup\":false,\"Ordinal\":0,\"Join\":\"AND\",\"ItemPath\":\"_$VARINDEXTOREPLACE_0\",\"Value\":\"DateTime.UtcNow.AddDays(3.0)\",\"Condition\":{\"FieldName\":\"EventStart\",\"FieldType\":\"System.DateTime\",\"Operator\":\"<=\",\"__msdisposeindex\":274},\"Name\":\"EventStart.DateTime.UtcNow.AddDays(3.0)\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":273},{\"IsGroup\":false,\"Ordinal\":1,\"Join\":\"AND\",\"ItemPath\":\"_$VARINDEXTOREPLACE_1\",\"Value\":\"DateTime.UtcNow\",\"Condition\":{\"FieldName\":\"EventStart\",\"FieldType\":\"System.DateTime\",\"Operator\":\">=\",\"__msdisposeindex\":276},\"Name\":null,\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":275}";
        private const string UpcomingWeekFilterGroup = "{\"IsGroup\":true,\"Ordinal\":$VARINDEXTOREPLACE,\"Join\":\"OR\",\"ItemPath\":\"_$VARINDEXTOREPLACE\",\"Value\":null,\"Condition\":{\"FieldName\":null,\"FieldType\":null,\"Operator\":null,\"__msdisposeindex\":293},\"Name\":\"Upcoming\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":292},{\"IsGroup\":false,\"Ordinal\":0,\"Join\":\"AND\",\"ItemPath\":\"_$VARINDEXTOREPLACE_0\",\"Value\":\"DateTime.UtcNow.AddDays(7.0)\",\"Condition\":{\"FieldName\":\"EventStart\",\"FieldType\":\"System.DateTime\",\"Operator\":\"<=\",\"__msdisposeindex\":295},\"Name\":\"EventStart.DateTime.UtcNow.AddDays(7.0)\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":294},{\"IsGroup\":false,\"Ordinal\":1,\"Join\":\"AND\",\"ItemPath\":\"_$VARINDEXTOREPLACE_1\",\"Value\":\"DateTime.UtcNow\",\"Condition\":{\"FieldName\":\"EventStart\",\"FieldType\":\"System.DateTime\",\"Operator\":\">=\",\"__msdisposeindex\":297},\"Name\":null,\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":296}";
        private const string UpcomingMonthFilterGroup = "{\"IsGroup\":true,\"Ordinal\":$VARINDEXTOREPLACE,\"Join\":\"OR\",\"ItemPath\":\"_$VARINDEXTOREPLACE\",\"Value\":null,\"Condition\":{\"FieldName\":null,\"FieldType\":null,\"Operator\":null,\"__msdisposeindex\":314},\"Name\":\"Upcoming\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":313},{\"IsGroup\":false,\"Ordinal\":0,\"Join\":\"AND\",\"ItemPath\":\"_$VARINDEXTOREPLACE_0\",\"Value\":\"DateTime.UtcNow.AddMonths(1)\",\"Condition\":{\"FieldName\":\"EventStart\",\"FieldType\":\"System.DateTime\",\"Operator\":\"<=\",\"__msdisposeindex\":316},\"Name\":\"EventStart.DateTime.UtcNow.AddMonths(1)\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":315},{\"IsGroup\":false,\"Ordinal\":1,\"Join\":\"AND\",\"ItemPath\":\"_$VARINDEXTOREPLACE_1\",\"Value\":\"DateTime.UtcNow\",\"Condition\":{\"FieldName\":\"EventStart\",\"FieldType\":\"System.DateTime\",\"Operator\":\">=\",\"__msdisposeindex\":318},\"Name\":null,\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":317}";
        private const string UpcomingYearFilterGroup = "{\"IsGroup\":true,\"Ordinal\":$VARINDEXTOREPLACE,\"Join\":\"OR\",\"ItemPath\":\"_$VARINDEXTOREPLACE\",\"Value\":null,\"Condition\":{\"FieldName\":null,\"FieldType\":null,\"Operator\":null,\"__msdisposeindex\":335},\"Name\":\"Upcoming\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":334},{\"IsGroup\":false,\"Ordinal\":0,\"Join\":\"AND\",\"ItemPath\":\"_$VARINDEXTOREPLACE_0\",\"Value\":\"DateTime.UtcNow.AddYears(1)\",\"Condition\":{\"FieldName\":\"EventStart\",\"FieldType\":\"System.DateTime\",\"Operator\":\"<=\",\"__msdisposeindex\":337},\"Name\":\"EventStart.DateTime.UtcNow.AddYears(1)\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":336},{\"IsGroup\":false,\"Ordinal\":1,\"Join\":\"AND\",\"ItemPath\":\"_$VARINDEXTOREPLACE_1\",\"Value\":\"DateTime.UtcNow\",\"Condition\":{\"FieldName\":\"EventStart\",\"FieldType\":\"System.DateTime\",\"Operator\":\">=\",\"__msdisposeindex\":339},\"Name\":null,\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":338}";
        private const string PastFilterGroup = "{\"IsGroup\":true,\"Ordinal\":$VARINDEXTOREPLACE,\"Join\":\"OR\",\"ItemPath\":\"_$VARINDEXTOREPLACE\",\"Value\":null,\"Condition\":{\"FieldName\":null,\"FieldType\":null,\"Operator\":null,\"__msdisposeindex\":464},\"Name\":\"Past\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":463},{\"IsGroup\":false,\"Ordinal\":0,\"Join\":\"AND\",\"ItemPath\":\"_$VARINDEXTOREPLACE_0\",\"Value\":\"DateTime.UtcNow\",\"Condition\":{\"FieldName\":\"EventEnd\",\"FieldType\":\"System.DateTime\",\"Operator\":\"<=\",\"__msdisposeindex\":466},\"Name\":null,\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":465}";
        private const string Past3DaysFilterGroup = "{\"IsGroup\":true,\"Ordinal\":$VARINDEXTOREPLACE,\"Join\":\"OR\",\"ItemPath\":\"_$VARINDEXTOREPLACE\",\"Value\":null,\"Condition\":{\"FieldName\":null,\"FieldType\":null,\"Operator\":null,\"__msdisposeindex\":359},\"Name\":\"Past\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":358},{\"IsGroup\":false,\"Ordinal\":0,\"Join\":\"AND\",\"ItemPath\":\"_$VARINDEXTOREPLACE_0\",\"Value\":\"DateTime.UtcNow.AddDays(-3.0)\",\"Condition\":{\"FieldName\":\"EventEnd\",\"FieldType\":\"System.DateTime\",\"Operator\":\">=\",\"__msdisposeindex\":361},\"Name\":\"EventEnd.DateTime.UtcNow.AddDays(-3.0)\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":360},{\"IsGroup\":false,\"Ordinal\":1,\"Join\":\"AND\",\"ItemPath\":\"_$VARINDEXTOREPLACE_1\",\"Value\":\"DateTime.UtcNow\",\"Condition\":{\"FieldName\":\"EventEnd\",\"FieldType\":\"System.DateTime\",\"Operator\":\"<=\",\"__msdisposeindex\":363},\"Name\":null,\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":362}";
        private const string PastWeekFilterGroup = "{\"IsGroup\":true,\"Ordinal\":$VARINDEXTOREPLACE,\"Join\":\"OR\",\"ItemPath\":\"_$VARINDEXTOREPLACE\",\"Value\":null,\"Condition\":{\"FieldName\":null,\"FieldType\":null,\"Operator\":null,\"__msdisposeindex\":380},\"Name\":\"Past\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":379},{\"IsGroup\":false,\"Ordinal\":0,\"Join\":\"AND\",\"ItemPath\":\"_$VARINDEXTOREPLACE_0\",\"Value\":\"DateTime.UtcNow.AddDays(-7.0)\",\"Condition\":{\"FieldName\":\"EventEnd\",\"FieldType\":\"System.DateTime\",\"Operator\":\">=\",\"__msdisposeindex\":382},\"Name\":\"EventEnd.DateTime.UtcNow.AddDays(-7.0)\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":381},{\"IsGroup\":false,\"Ordinal\":1,\"Join\":\"AND\",\"ItemPath\":\"_$VARINDEXTOREPLACE_1\",\"Value\":\"DateTime.UtcNow\",\"Condition\":{\"FieldName\":\"EventEnd\",\"FieldType\":\"System.DateTime\",\"Operator\":\"<=\",\"__msdisposeindex\":384},\"Name\":null,\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":383}";
        private const string PastMonthFilterGroup = "{\"IsGroup\":true,\"Ordinal\":$VARINDEXTOREPLACE,\"Join\":\"OR\",\"ItemPath\":\"_$VARINDEXTOREPLACE\",\"Value\":null,\"Condition\":{\"FieldName\":null,\"FieldType\":null,\"Operator\":null,\"__msdisposeindex\":401},\"Name\":\"Past\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":400},{\"IsGroup\":false,\"Ordinal\":0,\"Join\":\"AND\",\"ItemPath\":\"_$VARINDEXTOREPLACE_0\",\"Value\":\"DateTime.UtcNow.AddMonths(-1)\",\"Condition\":{\"FieldName\":\"EventEnd\",\"FieldType\":\"System.DateTime\",\"Operator\":\">=\",\"__msdisposeindex\":403},\"Name\":\"EventEnd.DateTime.UtcNow.AddMonths(-1)\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":402},{\"IsGroup\":false,\"Ordinal\":1,\"Join\":\"AND\",\"ItemPath\":\"_$VARINDEXTOREPLACE_1\",\"Value\":\"DateTime.UtcNow\",\"Condition\":{\"FieldName\":\"EventEnd\",\"FieldType\":\"System.DateTime\",\"Operator\":\"<=\",\"__msdisposeindex\":405},\"Name\":null,\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":404}";
        private const string PastYearFilterGroup = "{\"IsGroup\":true,\"Ordinal\":$VARINDEXTOREPLACE,\"Join\":\"OR\",\"ItemPath\":\"_$VARINDEXTOREPLACE\",\"Value\":null,\"Condition\":{\"FieldName\":null,\"FieldType\":null,\"Operator\":null,\"__msdisposeindex\":422},\"Name\":\"Past\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":421},{\"IsGroup\":false,\"Ordinal\":0,\"Join\":\"AND\",\"ItemPath\":\"_$VARINDEXTOREPLACE_0\",\"Value\":\"DateTime.UtcNow.AddYears(-1)\",\"Condition\":{\"FieldName\":\"EventEnd\",\"FieldType\":\"System.DateTime\",\"Operator\":\">=\",\"__msdisposeindex\":424},\"Name\":\"EventEnd.DateTime.UtcNow.AddYears(-1)\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":423},{\"IsGroup\":false,\"Ordinal\":1,\"Join\":\"AND\",\"ItemPath\":\"_$VARINDEXTOREPLACE_1\",\"Value\":\"DateTime.UtcNow\",\"Condition\":{\"FieldName\":\"EventEnd\",\"FieldType\":\"System.DateTime\",\"Operator\":\"<=\",\"__msdisposeindex\":426},\"Name\":null,\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":425}";

        #endregion

        protected class AdditionalFilter
        {
            public FilterSection FilterSection { get; set; }

            public string Filter { get; set; }

            public DateTime? StartDate { get; set; }

            public DateTime? EndDate { get; set; }
        }

        protected class NarrowFilter
        {
            public string Group { get; set; }

            public string FieldName { get; set; }

            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
            public Dictionary<Guid, string> DataList { get; set; }

            public string FieldType { get; set; }
        }

        protected enum FilterSection
        {
            Upcoming,
            Current,
            Past,
            Default
        }

        private const string JsonExceptionMessage = "[InvalidOperationException: Error during serialization or deserialization using the JSON JavaScriptSerializer. The length of the string exceeds the value set on the maxJsonLength property.]";
        private readonly Dictionary<string, CultureInfo> sitefinityLanguages = new Dictionary<string, CultureInfo>();

        private const string Caption = "Calendar";
        private const string PageNamePrefix = "EventsPage";

        private const string BasePastEventTitle = "PastTestEvent";
        private const string BaseUpcomingEventTitle = "UpcomingTestEvent";
        private const string BaseDraftEventTitle = "DraftTestEvent";
        private const string BaseAllDayEventTitle = "AllDayTestEvent";
        private const string BaseUpcomingAllDayEventTitle = "UpcomingAllDayTestEvent";
        private const string BasePastAllDayEventTitle = "PastAllDayTestEvent";
        private const string BaseRepeatEventTitle = "RepeatTestEvent";
        private const string BaseUpcomingRepeatEventTitle = "UpcomingRepeatTestEvent";
        private const string BasePastRepeatEventTitle = "PastRepeatTestEvent";

        private const string Calendar1Title = "Calendar 1";
        private const string Calendar2Title = "Calendar 2";
        private const string Calendar3Title = "Calendar 3";

        private const string Tag1Title = "Tag 1";
        private const string Tag2Title = "Tag 2";
        private const string Tag3Title = "Tag 3";

        private const string Category1Title = "Category 1";
        private const string Category2Title = "Category 2";
        private const string Category3Title = "Category 3";

        private Guid defaultCalendarId;
    }
}