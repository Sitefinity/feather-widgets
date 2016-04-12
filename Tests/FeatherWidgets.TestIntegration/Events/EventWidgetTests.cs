using System;
using System.Linq;
using FeatherWidgets.TestUtilities.CommonOperations;
using MbUnit.Framework;
using Telerik.Sitefinity.Frontend.Events.Mvc.Controllers;
using Telerik.Sitefinity.Frontend.Mvc.Models;
using Telerik.Sitefinity.Frontend.TestUtilities;
using Telerik.Sitefinity.Modules.Events;
using Telerik.Sitefinity.Mvc.Proxy;
using Telerik.Sitefinity.Web;

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
            this.pageOperations = new PagesOperations();

            for (int i = 1; i <= EventWidgetTests.EventsCount; i++)
                Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.Events().CreateEvent(EventWidgetTests.BaseEventTitle + i);
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
           var selectedEventTitle = EventWidgetTests.BaseEventTitle + 1;

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

            for (var i = 2; i <= EventsCount; i++) 
            {
                Assert.IsFalse(responseContent.Contains(EventWidgetTests.BaseEventTitle + i), "The event with this title was not found!");
            }
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.FeatherTeam)]
        [Description("Add Event widget to a page and create events(past, current, upcoming, all day, repeat) with published and draft posts in order to verify that ")]
        public void EventWidget_DisplayAllPublishedEvents()
        {
        }

        private PagesOperations pageOperations;
        private const int EventsCount = 3;
        private const string BaseEventTitle = "TestEvent";
    }
}
