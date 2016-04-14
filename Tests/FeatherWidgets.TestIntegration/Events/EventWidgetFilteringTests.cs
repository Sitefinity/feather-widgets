using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using MbUnit.Framework;
using ServiceStack.Text;
using Telerik.Sitefinity.Frontend.Events.Mvc.Controllers;
using Telerik.Sitefinity.Frontend.Mvc.Models;
using Telerik.Sitefinity.Frontend.TestUtilities;
using Telerik.Sitefinity.Mvc.Proxy;
using Telerik.Sitefinity.TestIntegration.Data.Content;
using Telerik.Sitefinity.TestIntegration.Helpers;
using Telerik.Sitefinity.TestUtilities.CommonOperations;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Web.Model;

namespace FeatherWidgets.TestIntegration.Events
{
    /// <summary>
    /// This is a test fixture with tests for filtering and narrow selection options of Event widget.
    /// </summary>
    [TestFixture]
    public class EventWidgetFilteringTests
    {
        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.FeatherTeam)]
        [Description("Verifies that event widget is displaying all events.")]
        public void EventWidget_AllEvents_DisplayAll()
        {
            var methodName = MethodInfo.GetCurrentMethod().Name;
            try
            {
                this.BuildEvents(methodName);

                var eventController = new EventController();
                eventController.Model.DisplayMode = ListDisplayMode.Paging;
                eventController.Model.SelectionMode = SelectionMode.AllItems;

                var mvcProxy = new MvcControllerProxy() { Settings = new ControllerSettings(eventController), ControllerName = typeof(EventController).FullName };
                var containedEvents = new string[] { CurrentEventNameFormat, NextWeekEventNameFormat, NextMonthEventNameFormat, PreviousMonthEventNameFormat, PreviousWeekEventNameFormat }.Select(s => string.Format(CultureInfo.InvariantCulture, s, methodName));
            
                using (var generator = new PageContentGenerator())
                {
                    generator.CreatePageWithWidget(mvcProxy, null, methodName, methodName, methodName, 0);
                    var pageContent = WebRequestHelper.GetPageWebContent(RouteHelper.GetAbsoluteUrl("~/" + methodName + "0"));

                    foreach (var title in containedEvents)
                    {
                        Assert.Contains(pageContent, title, StringComparison.Ordinal);
                    }
                }
            }
            finally
            {
                ServerOperations.Events().DeleteAllEvents();
            }
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.FeatherTeam)]
        [Description("Verifies that events widget is filtering events by tags.")]
        public void EventWidget_AllEvents_FilterByTags()
        {
            var methodName = MethodInfo.GetCurrentMethod().Name;
            ServerOperations.Events().CreateEvent(methodName + "_nottagged");
            var eventId = ServerOperations.Events().CreateEvent(methodName + "_tagged");
            var tagId = ServerOperations.Taxonomies().CreateTag(methodName, methodName);
            ServerOperations.Events().AssignTaxonToEventItem(eventId, "Tags", tagId);

            try
            {
                var mvcProxy = new MvcControllerProxy();
                mvcProxy.ControllerName = typeof(EventController).FullName;
                var eventController = new EventController();

                eventController.Model.NarrowSelectionMode = Telerik.Sitefinity.Frontend.Mvc.Models.SelectionMode.FilteredItems;
                eventController.Model.SerializedNarrowSelectionFilters = this.GetNarrowSelectionSerializedQueryData("Tags", "Tags", methodName, tagId);

                mvcProxy.Settings = new ControllerSettings(eventController);

                using (var generator = new PageContentGenerator())
                {
                    generator.CreatePageWithWidget(mvcProxy, null, methodName, methodName, methodName, 0);
                    var pageContent = WebRequestHelper.GetPageWebContent(RouteHelper.GetAbsoluteUrl("~/" + methodName + "0"));

                    Assert.Contains(pageContent, methodName + "_tagged", System.StringComparison.Ordinal);
                    Assert.DoesNotContain(pageContent, methodName + "_nottagged", System.StringComparison.Ordinal);
                }
            }
            finally
            {
                ServerOperations.Events().DeleteAllEvents();
                ServerOperations.Taxonomies().DeleteTags(methodName);
            }
        }
        
        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.FeatherTeam)]
        [Description("Verifies that event widget is filtering events by calendar.")]
        public void EventWidget_AllEvents_FilterByCalendar()
        {
            var methodName = MethodInfo.GetCurrentMethod().Name;
            var calendarId = ServerOperations.Events().CreateCalendar(Guid.NewGuid(), "custom_calendar");
            ServerOperations.Events().CreateEvent(methodName + "_fromdefault", "some content", false, DateTime.Now, DateTime.Now.AddHours(2), ServerOperations.Events().GetDefaultCalendarId());
            ServerOperations.Events().CreateEvent(methodName + "_fromcustom", "some content", false, DateTime.Now, DateTime.Now.AddHours(2), calendarId);

            try
            {
                var mvcProxy = new MvcControllerProxy();
                mvcProxy.ControllerName = typeof(EventController).FullName;
                var eventController = new EventController();

                eventController.Model.NarrowSelectionMode = Telerik.Sitefinity.Frontend.Mvc.Models.SelectionMode.FilteredItems;
                eventController.Model.SerializedNarrowSelectionFilters = this.GetNarrowSelectionSerializedQueryData("Calendars", "Parent.Id.ToString()", "Parent.Id", calendarId, "System.String");

                mvcProxy.Settings = new ControllerSettings(eventController);

                using (var generator = new PageContentGenerator())
                {
                    generator.CreatePageWithWidget(mvcProxy, null, methodName, methodName, methodName, 0);
                    var pageContent = WebRequestHelper.GetPageWebContent(RouteHelper.GetAbsoluteUrl("~/" + methodName + "0"));

                    Assert.Contains(pageContent, methodName + "_fromcustom", System.StringComparison.Ordinal);
                    Assert.DoesNotContain(pageContent, methodName + "_fromdefault", System.StringComparison.Ordinal);
                }
            }
            finally
            {
                ServerOperations.Events().DeleteAllEvents();
                ServerOperations.Events().DeleteCalendar(calendarId);
            }
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.FeatherTeam)]
        [Description("Verifies that event widget is filtering events by categories.")]
        public void EventWidget_AllEvents_FilterByCategories()
        {
            var methodName = MethodInfo.GetCurrentMethod().Name;
            ServerOperations.Events().CreateEvent(methodName + "_notclassified");
            var eventId = ServerOperations.Events().CreateEvent(methodName + "_classified");
            var taxonId = ServerOperations.Taxonomies().CreateHierarchicalTaxon(methodName, null, "Categories");
            ServerOperations.Events().AssignTaxonToEventItem(eventId, "Category", taxonId);

            try
            {
                var mvcProxy = new MvcControllerProxy();
                mvcProxy.ControllerName = typeof(EventController).FullName;
                var eventController = new EventController();

                eventController.Model.NarrowSelectionMode = Telerik.Sitefinity.Frontend.Mvc.Models.SelectionMode.FilteredItems;
                eventController.Model.SerializedNarrowSelectionFilters = this.GetNarrowSelectionSerializedQueryData("Category", "Category", methodName, taxonId);

                mvcProxy.Settings = new ControllerSettings(eventController);

                using (var generator = new PageContentGenerator())
                {
                    generator.CreatePageWithWidget(mvcProxy, null, methodName, methodName, methodName, 0);
                    var pageContent = WebRequestHelper.GetPageWebContent(RouteHelper.GetAbsoluteUrl("~/" + methodName + "0"));

                    Assert.Contains(pageContent, methodName + "_classified", System.StringComparison.Ordinal);
                    Assert.DoesNotContain(pageContent, methodName + "_notclassified", System.StringComparison.Ordinal);
                }
            }
            finally
            {
                ServerOperations.Events().DeleteAllEvents();
                ServerOperations.Taxonomies().DeleteTags(methodName);
            }
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.FeatherTeam)]
        [Description("Verifies that event widget is filtering events by hierarchical categories.")]
        public void EventWidget_AllEvents_FilterByHierarchicalCategories()
        {
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.FeatherTeam)]
        [Description("Verifies that event widget is filtering events by custom categories.")]
        public void EventWidget_AllEvents_FilterByCustomCategories()
        {
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.FeatherTeam)]
        [Description("Verifies that event widget is filtering events by custom hierarchical categories.")]
        public void EventWidget_AllEvents_FilterByCustomHierarchicalCategories()
        {
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.FeatherTeam)]
        [Description("Verifies that event widget is displaying only current events.")]
        public void EventWidget_FilterByDate_DisplayCurrentOnly()
        {
            const string SerializedAdditionalFilters = "{\"__msdisposeindex\":189,\"Title\":null,\"Id\":\"00000000-0000-0000-0000-000000000000\",\"QueryItems\":[{\"IsGroup\":true,\"Ordinal\":0,\"Join\":\"OR\",\"ItemPath\":\"_0\",\"Value\":null,\"Condition\":null,\"Name\":\"Current\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":191},{\"IsGroup\":false,\"Ordinal\":0,\"Join\":\"AND\",\"ItemPath\":\"_0_0\",\"Value\":\"DateTime.UtcNow\",\"Condition\":{\"FieldName\":\"EventStart\",\"FieldType\":\"System.DateTime\",\"Operator\":\"<=\",\"__msdisposeindex\":192},\"Name\":null,\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":193},{\"IsGroup\":false,\"Ordinal\":1,\"Join\":\"AND\",\"ItemPath\":\"_0_1\",\"Value\":\"DateTime.UtcNow\",\"Condition\":{\"FieldName\":\"EventEnd\",\"FieldType\":\"System.DateTime\",\"Operator\":\">=\",\"__msdisposeindex\":194},\"Name\":null,\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":195}],\"TypeProperties\":[],\"_itemPathSeparator\":\"_\"}";
            
            var methodName = MethodInfo.GetCurrentMethod().Name;
            var containedEvents = new string[] { CurrentEventNameFormat }.Select(s => string.Format(CultureInfo.InvariantCulture, s, methodName));
            var notContainedEvents = new string[] { NextWeekEventNameFormat, NextMonthEventNameFormat, PreviousMonthEventNameFormat, PreviousWeekEventNameFormat }.Select(s => string.Format(CultureInfo.InvariantCulture, s, methodName));

            this.TestFilterByDate(methodName, SerializedAdditionalFilters, containedEvents, notContainedEvents);
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.FeatherTeam)]
        [Description("Verifies that event widget is displaying only upcoming events.")]
        public void EventWidget_FilterByDate_DisplayUpcomingOnly()
        {
            const string SerializedAdditionalFilters = "{\"__msdisposeindex\":196,\"Title\":null,\"Id\":\"00000000-0000-0000-0000-000000000000\",\"QueryItems\":[{\"IsGroup\":true,\"Ordinal\":0,\"Join\":\"OR\",\"ItemPath\":\"_0\",\"Value\":null,\"Condition\":null,\"Name\":\"Upcoming\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":204},{\"IsGroup\":false,\"Ordinal\":0,\"Join\":\"AND\",\"ItemPath\":\"_0_0\",\"Value\":\"DateTime.UtcNow\",\"Condition\":{\"FieldName\":\"EventStart\",\"FieldType\":\"System.DateTime\",\"Operator\":\">=\",\"__msdisposeindex\":205},\"Name\":null,\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":206}],\"TypeProperties\":[],\"_itemPathSeparator\":\"_\"}";

            var methodName = MethodInfo.GetCurrentMethod().Name;
            var containedEvents = new string[] { NextWeekEventNameFormat, NextMonthEventNameFormat }.Select(s => string.Format(CultureInfo.InvariantCulture, s, methodName));
            var notContainedEvents = new string[] { CurrentEventNameFormat, PreviousMonthEventNameFormat, PreviousWeekEventNameFormat }.Select(s => string.Format(CultureInfo.InvariantCulture, s, methodName));

            this.TestFilterByDate(methodName, SerializedAdditionalFilters, containedEvents, notContainedEvents);
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.FeatherTeam)]
        [Description("Verifies that event widget is displaying only upcoming events narrowed by date.")]
        public void EventWidget_FilterByDate_DisplayUpcomingOnly_NarrowByDate()
        {
            const string SerializedAdditionalFilters = "{\"__msdisposeindex\":307,\"Title\":null,\"Id\":\"00000000-0000-0000-0000-000000000000\",\"QueryItems\":[{\"IsGroup\":true,\"Ordinal\":0,\"Join\":\"OR\",\"ItemPath\":\"_0\",\"Value\":null,\"Condition\":null,\"Name\":\"Upcoming\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":313},{\"IsGroup\":false,\"Ordinal\":0,\"Join\":\"AND\",\"ItemPath\":\"_0_0\",\"Value\":\"DateTime.UtcNow.AddDays(7.0)\",\"Condition\":{\"FieldName\":\"EventStart\",\"FieldType\":\"System.DateTime\",\"Operator\":\"<\",\"__msdisposeindex\":314},\"Name\":\"EventStart.DateTime.UtcNow.AddDays(7.0)\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":315},{\"IsGroup\":false,\"Ordinal\":1,\"Join\":\"AND\",\"ItemPath\":\"_0_1\",\"Value\":\"DateTime.UtcNow\",\"Condition\":{\"FieldName\":\"EventStart\",\"FieldType\":\"System.DateTime\",\"Operator\":\">=\",\"__msdisposeindex\":316},\"Name\":null,\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":317}],\"TypeProperties\":[],\"_itemPathSeparator\":\"_\"}";

            var methodName = MethodInfo.GetCurrentMethod().Name;
            var containedEvents = new string[] { NextWeekEventNameFormat }.Select(s => string.Format(CultureInfo.InvariantCulture, s, methodName));
            var notContainedEvents = new string[] { CurrentEventNameFormat, NextMonthEventNameFormat, PreviousMonthEventNameFormat, PreviousWeekEventNameFormat }.Select(s => string.Format(CultureInfo.InvariantCulture, s, methodName));

            this.TestFilterByDate(methodName, SerializedAdditionalFilters, containedEvents, notContainedEvents);
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.FeatherTeam)]
        [Description("Verifies that event widget is displaying only past events.")]
        public void EventWidget_FilterByDate_DisplayPastOnly()
        {
            const string SerializedAdditionalFilters = "{\"__msdisposeindex\":218,\"Title\":null,\"Id\":\"00000000-0000-0000-0000-000000000000\",\"QueryItems\":[{\"IsGroup\":true,\"Ordinal\":0,\"Join\":\"OR\",\"ItemPath\":\"_0\",\"Value\":null,\"Condition\":null,\"Name\":\"Past\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":226},{\"IsGroup\":false,\"Ordinal\":0,\"Join\":\"AND\",\"ItemPath\":\"_0_0\",\"Value\":\"DateTime.UtcNow\",\"Condition\":{\"FieldName\":\"EventEnd\",\"FieldType\":\"System.DateTime\",\"Operator\":\"<=\",\"__msdisposeindex\":227},\"Name\":null,\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":228}],\"TypeProperties\":[],\"_itemPathSeparator\":\"_\"}";

            var methodName = MethodInfo.GetCurrentMethod().Name;
            var containedEvents = new string[] { PreviousMonthEventNameFormat, PreviousWeekEventNameFormat }.Select(s => string.Format(CultureInfo.InvariantCulture, s, methodName));
            var notContainedEvents = new string[] { CurrentEventNameFormat, NextWeekEventNameFormat, NextMonthEventNameFormat }.Select(s => string.Format(CultureInfo.InvariantCulture, s, methodName));

            this.TestFilterByDate(methodName, SerializedAdditionalFilters, containedEvents, notContainedEvents);
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.FeatherTeam)]
        [Description("Verifies that event widget is displaying only past events narrowed by date.")]
        public void EventWidget_FilterByDate_DisplayPastOnly_NarrowByDate()
        {
            const string SerializedAdditionalFilters = "{\"__msdisposeindex\":279,\"Title\":null,\"Id\":\"00000000-0000-0000-0000-000000000000\",\"QueryItems\":[{\"IsGroup\":true,\"Ordinal\":0,\"Join\":\"OR\",\"ItemPath\":\"_0\",\"Value\":null,\"Condition\":null,\"Name\":\"Past\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":291},{\"IsGroup\":false,\"Ordinal\":0,\"Join\":\"AND\",\"ItemPath\":\"_0_0\",\"Value\":\"DateTime.UtcNow.AddDays(-7.0)\",\"Condition\":{\"FieldName\":\"EventStart\",\"FieldType\":\"System.DateTime\",\"Operator\":\">\",\"__msdisposeindex\":292},\"Name\":\"EventStart.DateTime.UtcNow.AddDays(-7.0)\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":293},{\"IsGroup\":false,\"Ordinal\":1,\"Join\":\"AND\",\"ItemPath\":\"_0_1\",\"Value\":\"DateTime.UtcNow\",\"Condition\":{\"FieldName\":\"EventEnd\",\"FieldType\":\"System.DateTime\",\"Operator\":\"<=\",\"__msdisposeindex\":294},\"Name\":null,\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":295}],\"TypeProperties\":[],\"_itemPathSeparator\":\"_\"}";

            var methodName = MethodInfo.GetCurrentMethod().Name;
            var containedEvents = new string[] { PreviousWeekEventNameFormat }.Select(s => string.Format(CultureInfo.InvariantCulture, s, methodName));
            var notContainedEvents = new string[] { CurrentEventNameFormat, PreviousMonthEventNameFormat, NextWeekEventNameFormat, NextMonthEventNameFormat }.Select(s => string.Format(CultureInfo.InvariantCulture, s, methodName));

            this.TestFilterByDate(methodName, SerializedAdditionalFilters, containedEvents, notContainedEvents);
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.FeatherTeam)]
        [Description("Verifies that event widget is displaying only past and current events.")]
        public void EventWidget_FilterByDate_DisplayPastAndCurrentOnly()
        {
            const string SerializedAdditionalFilters = "{\"__msdisposeindex\":269,\"Title\":null,\"Id\":\"00000000-0000-0000-0000-000000000000\",\"QueryItems\":[{\"IsGroup\":true,\"Ordinal\":0,\"Join\":\"OR\",\"ItemPath\":\"_0\",\"Value\":null,\"Condition\":null,\"Name\":\"Current\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":271},{\"IsGroup\":false,\"Ordinal\":0,\"Join\":\"AND\",\"ItemPath\":\"_0_0\",\"Value\":\"DateTime.UtcNow\",\"Condition\":{\"FieldName\":\"EventStart\",\"FieldType\":\"System.DateTime\",\"Operator\":\"<=\",\"__msdisposeindex\":272},\"Name\":null,\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":273},{\"IsGroup\":false,\"Ordinal\":1,\"Join\":\"AND\",\"ItemPath\":\"_0_1\",\"Value\":\"DateTime.UtcNow\",\"Condition\":{\"FieldName\":\"EventEnd\",\"FieldType\":\"System.DateTime\",\"Operator\":\">=\",\"__msdisposeindex\":274},\"Name\":null,\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":275},{\"IsGroup\":true,\"Ordinal\":1,\"Join\":\"OR\",\"ItemPath\":\"_1\",\"Value\":null,\"Condition\":null,\"Name\":\"Past\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":276},{\"IsGroup\":false,\"Ordinal\":0,\"Join\":\"AND\",\"ItemPath\":\"_1_0\",\"Value\":\"DateTime.UtcNow\",\"Condition\":{\"FieldName\":\"EventEnd\",\"FieldType\":\"System.DateTime\",\"Operator\":\"<=\",\"__msdisposeindex\":277},\"Name\":null,\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":278}],\"TypeProperties\":[],\"_itemPathSeparator\":\"_\"}";

            var methodName = MethodInfo.GetCurrentMethod().Name;
            var containedEvents = new string[] { CurrentEventNameFormat, PreviousMonthEventNameFormat, PreviousWeekEventNameFormat }.Select(s => string.Format(CultureInfo.InvariantCulture, s, methodName));
            var notContainedEvents = new string[] { NextWeekEventNameFormat, NextMonthEventNameFormat }.Select(s => string.Format(CultureInfo.InvariantCulture, s, methodName));

            this.TestFilterByDate(methodName, SerializedAdditionalFilters, containedEvents, notContainedEvents);
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.FeatherTeam)]
        [Description("Verifies that event widget is displaying only upcoming and current events.")]
        public void EventWidget_FilterByDate_DisplayUpcomingAndCurrentOnly()
        {
            const string SerializedAdditionalFilters = "{\"__msdisposeindex\":248,\"Title\":null,\"Id\":\"00000000-0000-0000-0000-000000000000\",\"QueryItems\":[{\"IsGroup\":true,\"Ordinal\":0,\"Join\":\"OR\",\"ItemPath\":\"_0\",\"Value\":null,\"Condition\":null,\"Name\":\"Upcoming\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":250},{\"IsGroup\":false,\"Ordinal\":0,\"Join\":\"AND\",\"ItemPath\":\"_0_0\",\"Value\":\"DateTime.UtcNow\",\"Condition\":{\"FieldName\":\"EventStart\",\"FieldType\":\"System.DateTime\",\"Operator\":\">=\",\"__msdisposeindex\":251},\"Name\":null,\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":252},{\"IsGroup\":true,\"Ordinal\":1,\"Join\":\"OR\",\"ItemPath\":\"_1\",\"Value\":null,\"Condition\":null,\"Name\":\"Current\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":253},{\"IsGroup\":false,\"Ordinal\":0,\"Join\":\"AND\",\"ItemPath\":\"_1_0\",\"Value\":\"DateTime.UtcNow\",\"Condition\":{\"FieldName\":\"EventStart\",\"FieldType\":\"System.DateTime\",\"Operator\":\"<=\",\"__msdisposeindex\":254},\"Name\":null,\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":255},{\"IsGroup\":false,\"Ordinal\":1,\"Join\":\"AND\",\"ItemPath\":\"_1_1\",\"Value\":\"DateTime.UtcNow\",\"Condition\":{\"FieldName\":\"EventEnd\",\"FieldType\":\"System.DateTime\",\"Operator\":\">=\",\"__msdisposeindex\":256},\"Name\":null,\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":257}],\"TypeProperties\":[],\"_itemPathSeparator\":\"_\"}";

            var methodName = MethodInfo.GetCurrentMethod().Name;
            var containedEvents = new string[] { CurrentEventNameFormat, NextWeekEventNameFormat, NextMonthEventNameFormat }.Select(s => string.Format(CultureInfo.InvariantCulture, s, methodName));
            var notContainedEvents = new string[] { PreviousMonthEventNameFormat, PreviousWeekEventNameFormat }.Select(s => string.Format(CultureInfo.InvariantCulture, s, methodName));

            this.TestFilterByDate(methodName, SerializedAdditionalFilters, containedEvents, notContainedEvents);
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.FeatherTeam)]
        [Description("Verifies that event widget is displaying only upcoming and past events.")]
        public void EventWidget_FilterByDate_DisplayUpcomingAndPastOnly()
        {
            const string SerializedAdditionalFilters = "{\"__msdisposeindex\":258,\"Title\":null,\"Id\":\"00000000-0000-0000-0000-000000000000\",\"QueryItems\":[{\"IsGroup\":true,\"Ordinal\":0,\"Join\":\"OR\",\"ItemPath\":\"_0\",\"Value\":null,\"Condition\":null,\"Name\":\"Past\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":270},{\"IsGroup\":false,\"Ordinal\":0,\"Join\":\"AND\",\"ItemPath\":\"_0_0\",\"Value\":\"DateTime.UtcNow\",\"Condition\":{\"FieldName\":\"EventEnd\",\"FieldType\":\"System.DateTime\",\"Operator\":\"<=\",\"__msdisposeindex\":271},\"Name\":null,\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":272},{\"IsGroup\":true,\"Ordinal\":1,\"Join\":\"OR\",\"ItemPath\":\"_1\",\"Value\":null,\"Condition\":null,\"Name\":\"Upcoming\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":273},{\"IsGroup\":false,\"Ordinal\":0,\"Join\":\"AND\",\"ItemPath\":\"_1_0\",\"Value\":\"DateTime.UtcNow\",\"Condition\":{\"FieldName\":\"EventStart\",\"FieldType\":\"System.DateTime\",\"Operator\":\">=\",\"__msdisposeindex\":274},\"Name\":null,\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":275}],\"TypeProperties\":[],\"_itemPathSeparator\":\"_\"}";

            var methodName = MethodInfo.GetCurrentMethod().Name;
            var containedEvents = new string[] { NextWeekEventNameFormat, NextMonthEventNameFormat, PreviousMonthEventNameFormat, PreviousWeekEventNameFormat }.Select(s => string.Format(CultureInfo.InvariantCulture, s, methodName));
            var notContainedEvents = new string[] { CurrentEventNameFormat }.Select(s => string.Format(CultureInfo.InvariantCulture, s, methodName));

            this.TestFilterByDate(methodName, SerializedAdditionalFilters, containedEvents, notContainedEvents);
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.FeatherTeam)]
        [Description("Verifies that event widget is displaying upcoming, current and past events.")]
        public void EventWidget_FilterByDate_DisplayUpcomingCurrentAndPast()
        {
            const string SerializedAdditionalFilters = "{\"__msdisposeindex\":253,\"Title\":null,\"Id\":\"00000000-0000-0000-0000-000000000000\",\"QueryItems\":[{\"IsGroup\":true,\"Ordinal\":0,\"Join\":\"OR\",\"ItemPath\":\"_0\",\"Value\":null,\"Condition\":{\"FieldName\":null,\"FieldType\":null,\"Operator\":null,\"__msdisposeindex\":255},\"Name\":\"Current\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":254},{\"IsGroup\":false,\"Ordinal\":0,\"Join\":\"AND\",\"ItemPath\":\"_0_0\",\"Value\":\"DateTime.UtcNow\",\"Condition\":{\"FieldName\":\"EventStart\",\"FieldType\":\"System.DateTime\",\"Operator\":\"<=\",\"__msdisposeindex\":257},\"Name\":null,\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":256},{\"IsGroup\":false,\"Ordinal\":1,\"Join\":\"AND\",\"ItemPath\":\"_0_1\",\"Value\":\"DateTime.UtcNow\",\"Condition\":{\"FieldName\":\"EventEnd\",\"FieldType\":\"System.DateTime\",\"Operator\":\">=\",\"__msdisposeindex\":259},\"Name\":null,\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":258},{\"IsGroup\":true,\"Ordinal\":1,\"Join\":\"OR\",\"ItemPath\":\"_1\",\"Value\":null,\"Condition\":null,\"Name\":\"Upcoming\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":260},{\"IsGroup\":false,\"Ordinal\":0,\"Join\":\"AND\",\"ItemPath\":\"_1_0\",\"Value\":\"DateTime.UtcNow\",\"Condition\":{\"FieldName\":\"EventStart\",\"FieldType\":\"System.DateTime\",\"Operator\":\">=\",\"__msdisposeindex\":261},\"Name\":null,\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":262},{\"IsGroup\":true,\"Ordinal\":2,\"Join\":\"OR\",\"ItemPath\":\"_2\",\"Value\":null,\"Condition\":null,\"Name\":\"Past\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":263},{\"IsGroup\":false,\"Ordinal\":0,\"Join\":\"AND\",\"ItemPath\":\"_2_0\",\"Value\":\"DateTime.UtcNow\",\"Condition\":{\"FieldName\":\"EventEnd\",\"FieldType\":\"System.DateTime\",\"Operator\":\"<=\",\"__msdisposeindex\":264},\"Name\":null,\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":265}],\"TypeProperties\":[],\"_itemPathSeparator\":\"_\"}";

            var methodName = MethodInfo.GetCurrentMethod().Name;
            var containedEvents = new string[] { CurrentEventNameFormat, NextWeekEventNameFormat, NextMonthEventNameFormat, PreviousMonthEventNameFormat, PreviousWeekEventNameFormat }.Select(s => string.Format(CultureInfo.InvariantCulture, s, methodName));
            var notContainedEvents = new string[] { };

            this.TestFilterByDate(methodName, SerializedAdditionalFilters, containedEvents, notContainedEvents);
        }

        private void TestFilterByDate(string methodName, string serializedAdditionalFilters, IEnumerable<string> containedEvents, IEnumerable<string> notContainedEvents)
        {
            try
            {
                this.BuildEvents(methodName);

                var eventController = new EventController();
                eventController.Model.DisplayMode = ListDisplayMode.Paging;
                eventController.Model.SelectionMode = SelectionMode.FilteredItems;
                eventController.Model.NarrowSelectionMode = SelectionMode.AllItems;
                eventController.Model.SerializedAdditionalFilters = serializedAdditionalFilters;

                var mvcProxy = new MvcControllerProxy() { Settings = new ControllerSettings(eventController), ControllerName = typeof(EventController).FullName };

                using (var generator = new PageContentGenerator())
                {
                    generator.CreatePageWithWidget(mvcProxy, null, methodName, methodName, methodName, 0);
                    var pageContent = WebRequestHelper.GetPageWebContent(RouteHelper.GetAbsoluteUrl("~/" + methodName + "0"));

                    foreach (var title in containedEvents)
                    {
                        Assert.Contains(pageContent, title, StringComparison.Ordinal);
                    }

                    foreach (var title in notContainedEvents)
                    {
                        Assert.DoesNotContain(pageContent, title, StringComparison.Ordinal);
                    }
                }
            }
            finally
            {
                ServerOperations.Events().DeleteAllEvents();
            }
        }

        private string GetNarrowSelectionSerializedQueryData(string group, string field, string name, Guid taxonId, string type = "System.Guid")
        {
            var queryData = new QueryData();
            queryData.QueryItems = new QueryItem[2] 
                {
                    new QueryItem()
                    {
                        IsGroup = true,
                        Join = "AND",
                        ItemPath = "_0",
                        Name = group
                    },
                    new QueryItem()
                    {
                        IsGroup = false,
                        Join = "OR",
                        ItemPath = "_0_0",
                        Value = taxonId.ToString("D"),
                        Condition = new Condition()
                        {
                            FieldName = field,
                            FieldType = type,
                            Operator = "Contains"
                        },
                        Name = name
                    }
                };

            return JsonSerializer.SerializeToString(queryData, typeof(QueryData));
        }

        private void BuildEvents(string methodName)
        {
            var calendarId = ServerOperations.Events().GetDefaultCalendarId();

            ServerOperations.Events().CreateEvent(string.Format(CultureInfo.InvariantCulture, CurrentEventNameFormat, methodName), "some content", false, DateTime.UtcNow.AddMinutes(-1), DateTime.UtcNow.AddMinutes(1), calendarId);
            ServerOperations.Events().CreateEvent(string.Format(CultureInfo.InvariantCulture, NextWeekEventNameFormat, methodName), "some content", false, DateTime.UtcNow.AddDays(5), DateTime.UtcNow.AddDays(5).AddMinutes(2), calendarId);
            ServerOperations.Events().CreateEvent(string.Format(CultureInfo.InvariantCulture, NextMonthEventNameFormat, methodName), "some content", false, DateTime.UtcNow.AddMonths(1), DateTime.UtcNow.AddMonths(1).AddMinutes(2), calendarId);
            ServerOperations.Events().CreateEvent(string.Format(CultureInfo.InvariantCulture, PreviousWeekEventNameFormat, methodName), "some content", false, DateTime.UtcNow.AddDays(-5), DateTime.UtcNow.AddDays(-5).AddMinutes(2), calendarId);
            ServerOperations.Events().CreateEvent(string.Format(CultureInfo.InvariantCulture, PreviousMonthEventNameFormat, methodName), "some content", false, DateTime.UtcNow.AddMonths(-1), DateTime.UtcNow.AddMonths(-1).AddMinutes(2), calendarId);
        }

        public const string CurrentEventNameFormat = "{0}_current_event_name";
        public const string NextWeekEventNameFormat = "{0}_next_week_event_name";
        public const string NextMonthEventNameFormat = "{0}_next_month_event_name";
        public const string PreviousWeekEventNameFormat = "{0}_previous_week_event_name";
        public const string PreviousMonthEventNameFormat = "{0}_previous_month_event_name";
    }
}