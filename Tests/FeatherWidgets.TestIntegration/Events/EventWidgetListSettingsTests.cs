using FeatherWidgets.TestUtilities.CommonOperations;
using MbUnit.Framework;
using Telerik.Sitefinity.Frontend.Events.Mvc.Controllers;
using Telerik.Sitefinity.Frontend.TestUtilities;
using Telerik.Sitefinity.Mvc.Proxy;
using Telerik.Sitefinity.Web;

namespace FeatherWidgets.TestIntegration.Events
{
    /// <summary>
    /// A class with tests related to Event widget list settings.
    /// </summary>
    [TestFixture]
    [Description("This is a class with Event widget list settings tests.")]
    public class EventWidgetListSettingsTests
    {
        /// <summary>
        /// Fixture setup.
        /// </summary>
        [FixtureSetUp]
        public void Setup()
        {
            this.pageOperations = new PagesOperations();

            for (int i = 1; i <= EventWidgetListSettingsTests.EventsCount; i++)
                Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.Events().CreateEvent(EventWidgetListSettingsTests.BaseEventTitle);
        }

        /// <summary>
        /// Fixture tear down.
        /// </summary>
        [FixtureTearDown]
        public void FixtureTearDown()
        {
            Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.Pages().DeleteAllPages();
            Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.Events().DeleteAllEvents();
        }

        /// <summary>
        /// Add Event widget to a page and display events in paging - two item per page
        /// </summary>
        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.FeatherTeam)]
        [Description("Add Event widget to a page and display events in paging - two item per page")]
        public void EventWidget_UsePaging_TwoItemPerPage()
        {
            string testName = System.Reflection.MethodInfo.GetCurrentMethod().Name;
            string pageNamePrefix = testName + "EventPage";
            string pageTitlePrefix = testName + "Event Page";
            string urlNamePrefix = testName + "Event-page";
            int index = 1;
            string index2 = "/2";
            int itemsPerPage = 2;
            string url = UrlPath.ResolveAbsoluteUrl("~/" + urlNamePrefix + index);
            string url2 = UrlPath.ResolveAbsoluteUrl("~/" + urlNamePrefix + index + index2);

            var mvcProxy = new MvcControllerProxy();
            mvcProxy.ControllerName = typeof(EventController).FullName;
            var eventController = new EventController();
            eventController.Model.ItemsPerPage = itemsPerPage;
            eventController.Model.DisplayMode = Telerik.Sitefinity.Frontend.Mvc.Models.ListDisplayMode.Paging;
            mvcProxy.Settings = new Telerik.Sitefinity.Mvc.Proxy.ControllerSettings(eventController);

            this.pageOperations.CreatePageWithControl(mvcProxy, pageNamePrefix, pageTitlePrefix, urlNamePrefix, index);

            string responseContent = PageInvoker.ExecuteWebRequest(url);
            string responseContent2 = PageInvoker.ExecuteWebRequest(url2);

            for (int i = 1; i <= EventWidgetListSettingsTests.EventsCount; i++)
            {
                if (i <= 2)
                {
                    Assert.IsFalse(responseContent.Contains(EventWidgetListSettingsTests.BaseEventTitle + i), "The event with this title was found!");
                    Assert.IsTrue(responseContent2.Contains(EventWidgetListSettingsTests.BaseEventTitle + i), "The event with this title was not found!");
                }
                else
                {
                    Assert.IsTrue(responseContent.Contains(EventWidgetListSettingsTests.BaseEventTitle + i), "The event with this title was not found!");
                    Assert.IsFalse(responseContent2.Contains(EventWidgetListSettingsTests.BaseEventTitle + i), "The event with this title was found!");
                }
            }
        }

        /// <summary>
        /// Add Event widget to a page and display limited events.
        /// </summary>
        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.FeatherTeam)]
        [Description("Add Event widget to a page and display limited events.")]
        public void EventWidget_UseLimit_TwoItem()
        {
            string testName = System.Reflection.MethodInfo.GetCurrentMethod().Name;
            string pageNamePrefix = testName + "EventPage";
            string pageTitlePrefix = testName + "Event Page";
            string urlNamePrefix = testName + "Event-page";
            int index = 1;
            int itemsPerPage = 2;
            string url = UrlPath.ResolveAbsoluteUrl("~/" + urlNamePrefix + index);

            var mvcProxy = new MvcControllerProxy();
            mvcProxy.ControllerName = typeof(EventController).FullName;
            var eventController = new EventController();
            eventController.Model.ItemsPerPage = itemsPerPage;
            eventController.Model.DisplayMode = Telerik.Sitefinity.Frontend.Mvc.Models.ListDisplayMode.Limit;
            mvcProxy.Settings = new Telerik.Sitefinity.Mvc.Proxy.ControllerSettings(eventController);

            this.pageOperations.CreatePageWithControl(mvcProxy, pageNamePrefix, pageTitlePrefix, urlNamePrefix, index);

            string responseContent = PageInvoker.ExecuteWebRequest(url);

            for (int i = 1; i <= EventWidgetListSettingsTests.EventsCount; i++)
            {
                if (i <= 2)
                {
                    Assert.IsFalse(responseContent.Contains(EventWidgetListSettingsTests.BaseEventTitle + i), "The event with this title was found!");
                }
                else
                {
                    Assert.IsTrue(responseContent.Contains(EventWidgetListSettingsTests.BaseEventTitle + i), "The event with this title was not found!");
                }
            }
        }

        /// <summary>
        /// Add Event widget to a page and display events with no limit and paging.
        /// </summary>
        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.FeatherTeam)]
        [Description("Add Event widget to a page and display events with no limit and paging.")]
        public void EventWidget_NoLimitAndPaging()
        {
            string testName = System.Reflection.MethodInfo.GetCurrentMethod().Name;
            string pageNamePrefix = testName + "EventPage";
            string pageTitlePrefix = testName + "Event Page";
            string urlNamePrefix = testName + "Event-page";
            int index = 1;
            int itemsPerPage = 2;
            string url = UrlPath.ResolveAbsoluteUrl("~/" + urlNamePrefix + index);

            var mvcProxy = new MvcControllerProxy();
            mvcProxy.ControllerName = typeof(EventController).FullName;
            var eventController = new EventController();
            eventController.Model.ItemsPerPage = itemsPerPage;
            eventController.Model.DisplayMode = Telerik.Sitefinity.Frontend.Mvc.Models.ListDisplayMode.All;
            mvcProxy.Settings = new Telerik.Sitefinity.Mvc.Proxy.ControllerSettings(eventController);

            this.pageOperations.CreatePageWithControl(mvcProxy, pageNamePrefix, pageTitlePrefix, urlNamePrefix, index);

            string responseContent = PageInvoker.ExecuteWebRequest(url);

            for (int i = 1; i <= EventWidgetListSettingsTests.EventsCount; i++)
            {
                Assert.IsTrue(responseContent.Contains(EventWidgetListSettingsTests.BaseEventTitle + i), "The event with this title was not found!");
            }
        }

        /// <summary>
        /// Add Event widget to a page and display sorted events.
        /// </summary>
        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.FeatherTeam)]
        [Description("Add Event widget to a page and display sorted events.")]
        public void EventWidget_ItemsAreSorted()
        {
        }

        private PagesOperations pageOperations;
        private const int EventsCount = 5;
        private const string BaseEventTitle = "TestEvent";
    }
}
