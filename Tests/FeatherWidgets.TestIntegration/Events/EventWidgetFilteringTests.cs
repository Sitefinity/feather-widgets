using System.Reflection;
using MbUnit.Framework;
using Telerik.Sitefinity.Frontend.Events.Mvc.Controllers;
using Telerik.Sitefinity.Frontend.TestUtilities;
using Telerik.Sitefinity.Mvc.Proxy;
using Telerik.Sitefinity.TestIntegration.Data.Content;
using Telerik.Sitefinity.TestIntegration.Helpers;
using Telerik.Sitefinity.TestUtilities.CommonOperations;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Web.Model;
using ServiceStack.Text;

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

                var queryData = new QueryData();
                queryData.QueryItems = new QueryItem[2] {
                    new QueryItem()
                    {
                        IsGroup = true,
                        Join = "AND",
                        ItemPath = "_0",
                        Name = "Tags"
                    },
                    new QueryItem()
                    {
                        IsGroup = false,
                        Join = "OR",
                        ItemPath = "_0_0",
                        Value = tagId.ToString("D"),
                        Condition = new Condition()
                        {
                            FieldName = "Tags",
                            FieldType = "System.Guid",
                            Operator = "Contains"
                        },
                        Name = methodName
                    }
                };

                eventController.Model.SerializedNarrowSelectionFilters = JsonSerializer.SerializeToString(queryData, typeof(QueryData));

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
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.FeatherTeam)]
        [Description("Verifies that event widget is filtering events by calendar.")]
        public void EventWidget_AllEvents_FilterByCalendar()
        {
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
    }
}
