using System;
using System.Linq;
using MbUnit.Framework;
using Telerik.Sitefinity.Events.Model;
using Telerik.Sitefinity.Frontend.Events.Mvc.Controllers;
using Telerik.Sitefinity.Frontend.Events.Mvc.Helpers;
using Telerik.Sitefinity.Frontend.Mvc.Models;
using Telerik.Sitefinity.Frontend.TestUtilities;
using Telerik.Sitefinity.Modules.Events;
using Telerik.Sitefinity.Mvc.Proxy;
using Telerik.Sitefinity.TestUtilities.CommonOperations;

namespace FeatherWidgets.TestIntegration.Events
{
    /// <summary>
    /// This class contains test related to basic functionality of Event Widget.
    /// </summary>
    [TestFixture]
    public class EventWidgetTests
    {
        /// <summary>
        /// The setup.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            this.pageOperations = new FeatherWidgets.TestUtilities.CommonOperations.PagesOperations();

            ServerOperations.Events().CreateEvent(EventWidgetTests.BaseEventTitle);
            ServerOperations.Events().CreateEvent(EventWidgetTests.BasePastEventTitle, string.Empty, false, DateTime.Now.AddDays(-2), DateTime.Now.AddDays(-1));
            ServerOperations.Events().CreateEvent(EventWidgetTests.BaseUpcomingEventTitle, string.Empty, false, DateTime.Now.AddDays(1), DateTime.Now.AddDays(2));
            ServerOperations.Events().CreateEvent(EventWidgetTests.BaseAllDayEventTitle, string.Empty, true, DateTime.Now, DateTime.Now.AddHours(1));
            ServerOperations.Events().CreateDailyRecurrentEvent(EventWidgetTests.BaseRepeatEventTitle, string.Empty, DateTime.Now, DateTime.Now.AddHours(1), 60, 5, 1, TimeZoneInfo.Local.StandardName);
            ServerOperations.Events().CreateDraftEvent(EventWidgetTests.BaseDraftEventTitle, string.Empty, false, DateTime.Now, DateTime.Now.AddHours(1));
        }

        /// <summary>
        /// The tear down.
        /// </summary>
        [TearDown]
        public void TearDown()
        {
            Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.Pages().DeleteAllPages();
            Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.Events().DeleteAllEvents();
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.FeatherTeam)]
        [Description("Add Event widget to a page and display selected events only.")]
        public void EventWidget_DisplaySelectedEventsOnly()
        {
            var selectedEventTitle = EventWidgetTests.BaseEventTitle;

            var mvcProxy = new MvcControllerProxy();
            mvcProxy.ControllerName = typeof(EventController).FullName;
            var eventController = new EventController();
            eventController.Model.SelectionMode = SelectionMode.SelectedItems;

            var eventsManager = EventsManager.GetManager();
            var selectedEvent = eventsManager.GetEvents().FirstOrDefault(n => n.Title == selectedEventTitle && n.OriginalContentId != Guid.Empty);
            eventController.Model.SerializedSelectedItemsIds = "[\"" + selectedEvent.Id.ToString() + "\"]";

            mvcProxy.Settings = new Telerik.Sitefinity.Mvc.Proxy.ControllerSettings(eventController);

            string responseContent = this.pageOperations.AddWidgetToPageAndRequest(mvcProxy);

            Assert.IsTrue(responseContent.Contains(selectedEventTitle), "The event with this title was not found!");
            Assert.IsFalse(responseContent.Contains(EventWidgetTests.BaseAllDayEventTitle), "The event with this title was found!");
            Assert.IsFalse(responseContent.Contains(EventWidgetTests.BaseDraftEventTitle), "The event with this title was found!");
            Assert.IsFalse(responseContent.Contains(EventWidgetTests.BaseRepeatEventTitle), "The event with this title was found!");
            Assert.IsFalse(responseContent.Contains(EventWidgetTests.BaseUpcomingEventTitle), "The event with this title was found!");
            Assert.IsFalse(responseContent.Contains(EventWidgetTests.BasePastEventTitle), "The event with this title was found!");
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.FeatherTeam)]
        [Description("Add Event widget to a page and create events(past, current, upcoming, all day, repeat) with published and draft posts in order to verify that ")]
        public void EventWidget_DisplayAllPublishedEvents()
        {
            var mvcProxy = new MvcControllerProxy();
            mvcProxy.ControllerName = typeof(EventController).FullName;
            var eventController = new EventController();
            eventController.Model.SelectionMode = SelectionMode.AllItems;
            mvcProxy.Settings = new Telerik.Sitefinity.Mvc.Proxy.ControllerSettings(eventController);

            string responseContent = this.pageOperations.AddWidgetToPageAndRequest(mvcProxy);

            Assert.IsTrue(responseContent.Contains(EventWidgetTests.BaseEventTitle), "The event with this title was not found!");
            Assert.IsTrue(responseContent.Contains(EventWidgetTests.BaseAllDayEventTitle), "Add day event with this title was not found!");
            Assert.IsFalse(responseContent.Contains(EventWidgetTests.BaseDraftEventTitle), "Draft event with this title was found!");
            Assert.IsTrue(responseContent.Contains(EventWidgetTests.BaseRepeatEventTitle), "Repeat event with this title was not found!");
            Assert.IsTrue(responseContent.Contains(EventWidgetTests.BasePastEventTitle), "Past event with this title was not found!");
            Assert.IsTrue(responseContent.Contains(EventWidgetTests.BaseUpcomingEventTitle), "Upcoming event with this title was not found!");
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.FeatherTeam)]
        [Description("Ensure that event calendar color is retrieved correctly via the helper.")]
        public void EventHelperCalendarColor()
        {
            const string ExpectedColor = "FF0000";
            var ev = new Event() { Parent = new Calendar() { Color = ExpectedColor } };
            Assert.AreEqual(ExpectedColor, new ItemViewModel(ev).EventCalendarColour());
        }

        private FeatherWidgets.TestUtilities.CommonOperations.PagesOperations pageOperations;
        private const string BaseEventTitle = "TestEvent";
        private const string BasePastEventTitle = "PastTestEvent";
        private const string BaseUpcomingEventTitle = "UpcomingTestEvent";
        private const string BaseDraftEventTitle = "DraftTestEvent";
        private const string BaseAllDayEventTitle = "AllDayTestEvent";
        private const string BaseRepeatEventTitle = "RepeatTestEvent";
    }
}
