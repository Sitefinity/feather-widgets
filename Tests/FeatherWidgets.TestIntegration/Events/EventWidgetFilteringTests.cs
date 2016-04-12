using System;
using System.Reflection;

using MbUnit.Framework;
using ServiceStack.Text;
using Telerik.Sitefinity.Frontend.Events.Mvc.Controllers;
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
                eventController.Model.SerializedNarrowSelectionFilters = this.GetSerializedQueryData("Tags", "Tags", methodName, tagId);

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
                eventController.Model.SerializedNarrowSelectionFilters = this.GetSerializedQueryData("Category", "Category", methodName, taxonId);

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
                eventController.Model.SerializedNarrowSelectionFilters = this.GetSerializedQueryData("Calendars", "Parent.Id.ToString()", "Parent.Id", calendarId, "System.String");

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
        [Description("Verifies that event widget is displaying only past events.")]
        public void EventWidget_FilterByDate_DisplayPastOnly()
        {
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.FeatherTeam)]
        [Description("Verifies that event widget is displaying only current events.")]
        public void EventWidget_FilterByDate_DisplayCurrentOnly()
        {
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.FeatherTeam)]
        [Description("Verifies that event widget is displaying only upcoming events.")]
        public void EventWidget_FilterByDate_DisplayUpcomingOnly()
        {
        }

        private string GetSerializedQueryData(string group, string field, string name, Guid taxonId, string type = "System.Guid")
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
    }
}
