using System.Linq;
using FeatherWidgets.TestUtilities.CommonOperations;
using MbUnit.Framework;
using Telerik.Sitefinity.Events.Model;
using Telerik.Sitefinity.Frontend.Events.Mvc.Controllers;
using Telerik.Sitefinity.Frontend.TestUtilities;
using Telerik.Sitefinity.Mvc.Proxy;
using Telerik.Sitefinity.Web;

namespace FeatherWidgets.TestIntegration.Events
{
    /// <summary>
    /// This is class contains test for event widget details functionality.
    /// </summary>
    [TestFixture]
    [Description("This is class contains test for event widget details functionality.")]
    public class EventWidgetDetailSettingsTests
    {
        /// <summary>
        /// The setup.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            this.pageOperations = new PagesOperations();

            for (int i = 1; i <= EventWidgetDetailSettingsTests.EventsCount; i++)
                Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.Events().CreateEvent(EventWidgetDetailSettingsTests.BaseEventTitle + i);
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

        /// <summary>
        /// Event widget - test whether single item view is displayed in the same page.
        /// </summary>
        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.FeatherTeam)]
        [Description("Verifies that open single item in the same page functionality resolves the correct page.")]
        public void EventWidget_VerifyOpenSingleItemInSamePage()
        {
            string testName = System.Reflection.MethodInfo.GetCurrentMethod().Name;
            string pageNamePrefix = testName + "EventsPage";
            string pageTitlePrefix = testName + "Events Page";
            string urlNamePrefix = testName + "events-page";
            int pageIndex = 1;
            string pageUrl = UrlPath.ResolveAbsoluteUrl("~/" + urlNamePrefix + pageIndex);

            var mvcProxy = new MvcControllerProxy();
            mvcProxy.ControllerName = typeof(EventController).FullName;
            var eventController = new EventController();
            eventController.OpenInSamePage = true;
            mvcProxy.Settings = new ControllerSettings(eventController);

            this.pageOperations.CreatePageWithControl(mvcProxy, pageNamePrefix, pageTitlePrefix, urlNamePrefix, pageIndex);

            var items = eventController.Model.CreateListViewModel(1).Items.ToArray();

            Assert.AreEqual(EventWidgetDetailSettingsTests.EventsCount, items.Length, "The count of the events is not as expected");

            var expectedDetailEvent = (Event)items[0].DataItem;
            string expectedDetailEventUrl = pageUrl + expectedDetailEvent.ItemDefaultUrl;

            string responseContent = PageInvoker.ExecuteWebRequest(pageUrl);
            Assert.IsTrue(responseContent.Contains(expectedDetailEventUrl), "The expected details event url was not found!");
        }

        /// <summary>
        /// Events widget - verify open single item in custom page functionality.
        /// </summary>
        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.FeatherTeam)]
        [Description("Verifies that open single item in the existing page functionality resolves the correct page.")]
        public void EventWidget_VerifyOpenSingleItemInCustomPage()
        {
            string testName = System.Reflection.MethodInfo.GetCurrentMethod().Name;
            string pageNamePrefix = testName + "EventsPage";
            string pageTitlePrefix = testName + "Events Page";
            string urlNamePrefix = testName + "events-page";
            int firstPageIndex = 1;
            int secondPageIndex = 2;
            string firstPageUrl = UrlPath.ResolveAbsoluteUrl("~/" + urlNamePrefix + firstPageIndex);
            string secondPageUrl = UrlPath.ResolveAbsoluteUrl("~/" + urlNamePrefix + secondPageIndex);

            var secondPageMvcProxy = new MvcControllerProxy();
            secondPageMvcProxy.ControllerName = typeof(EventController).FullName;
            var secondPageId = this.pageOperations.CreatePageWithControl(secondPageMvcProxy, pageNamePrefix, pageTitlePrefix, urlNamePrefix, secondPageIndex);

            var firstPageMvcProxy = new MvcControllerProxy();
            firstPageMvcProxy.ControllerName = typeof(EventController).FullName;
            var eventController = new EventController();
            eventController.OpenInSamePage = false;
            eventController.DetailsPageId = secondPageId;
            firstPageMvcProxy.Settings = new ControllerSettings(eventController);
            this.pageOperations.CreatePageWithControl(firstPageMvcProxy, pageNamePrefix, pageTitlePrefix, urlNamePrefix, firstPageIndex);

            var items = eventController.Model.CreateListViewModel(1).Items.ToArray();
            Assert.AreEqual(EventWidgetDetailSettingsTests.EventsCount, items.Length, "The count of the events is not as expected");

            var expectedDetailEvent = (Event)items[0].DataItem;
            string expectedDetailEventUrl = secondPageUrl + expectedDetailEvent.ItemDefaultUrl;

            string responseContent = PageInvoker.ExecuteWebRequest(firstPageUrl);
            Assert.IsTrue(responseContent.Contains(expectedDetailEventUrl), "The expected details event url was not found!");
        }

        /// <summary>
        /// Verifies that selected templates is used in Details mode.
        /// </summary>
        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.FeatherTeam)]
        [Description("Verifies that selected templates is used in Details mode.")]
        public void EventWidget_SelectDetailTemplate()
        {            
        }

        /// <summary>
        /// Verifies that links for exporting events are available when export is enabled.
        /// </summary>
        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.FeatherTeam)]
        [Description("Verifies that links for exporting events are available when export is enabled.")]
        public void EventWidget_AllowCalendarExport()
        {
        }

        private PagesOperations pageOperations;
        private const int EventsCount = 3;
        private const string BaseEventTitle = "TestEvent";
    }
}
