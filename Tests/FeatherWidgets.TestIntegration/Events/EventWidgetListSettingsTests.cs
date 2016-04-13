using System.IO;
using System.Linq;
using FeatherWidgets.TestUtilities.CommonOperations;
using FeatherWidgets.TestUtilities.CommonOperations.Templates;
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
        /// The setup.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            this.pageOperations = new PagesOperations();

            for (int i = 1; i <= EventWidgetListSettingsTests.EventsCount; i++)
                Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.Events().CreateEvent(EventWidgetListSettingsTests.BaseEventTitle + i);
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
        /// Add Event widget to a page and display events in paging - two items per page
        /// </summary>
        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.FeatherTeam)]
        [Description("Add Event widget to a page and display events in paging - two items per page")]
        public void EventWidget_UsePaging_TwoItemsPerPage()
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
            eventController.Model.SortExpression = "Title";
            eventController.Model.DisplayMode = Telerik.Sitefinity.Frontend.Mvc.Models.ListDisplayMode.Paging;
            mvcProxy.Settings = new Telerik.Sitefinity.Mvc.Proxy.ControllerSettings(eventController);

            this.pageOperations.CreatePageWithControl(mvcProxy, pageNamePrefix, pageTitlePrefix, urlNamePrefix, index);

            string responseContent = PageInvoker.ExecuteWebRequest(url);
            string responseContent2 = PageInvoker.ExecuteWebRequest(url2);

            for (int i = 1; i <= EventWidgetListSettingsTests.EventsCount; i++)
            {
                if (i <= 2)
                {
                    Assert.IsTrue(responseContent.Contains(EventWidgetListSettingsTests.BaseEventTitle + i), "The event with this title was not found!");
                    Assert.IsFalse(responseContent2.Contains(EventWidgetListSettingsTests.BaseEventTitle + i), "The event with this title was found!");
                }
                else if (i > 2 && i <= 4)
                {
                    Assert.IsFalse(responseContent.Contains(EventWidgetListSettingsTests.BaseEventTitle + i), "The event with this title was found!");
                    Assert.IsTrue(responseContent2.Contains(EventWidgetListSettingsTests.BaseEventTitle + i), "The event with this title was not found!");
                }
                else
                {
                    Assert.IsFalse(responseContent.Contains(EventWidgetListSettingsTests.BaseEventTitle + i), "The event with this title was found!");
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
        public void EventWidget_UseLimit_TwoItems()
        {
            int itemsPerPage = 2;

            var mvcProxy = new MvcControllerProxy();
            mvcProxy.ControllerName = typeof(EventController).FullName;
            var eventController = new EventController();
            eventController.Model.ItemsPerPage = itemsPerPage;
            eventController.Model.DisplayMode = Telerik.Sitefinity.Frontend.Mvc.Models.ListDisplayMode.Limit;
            mvcProxy.Settings = new Telerik.Sitefinity.Mvc.Proxy.ControllerSettings(eventController);

            string responseContent = this.pageOperations.AddWidgetToPageAndRequest(mvcProxy);

            for (int i = 1; i <= EventWidgetListSettingsTests.EventsCount; i++)
            {
                if (i > 2)
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
            int itemsPerPage = 2;

            var mvcProxy = new MvcControllerProxy();
            mvcProxy.ControllerName = typeof(EventController).FullName;
            var eventController = new EventController();
            eventController.Model.ItemsPerPage = itemsPerPage;
            eventController.Model.DisplayMode = Telerik.Sitefinity.Frontend.Mvc.Models.ListDisplayMode.All;
            mvcProxy.Settings = new Telerik.Sitefinity.Mvc.Proxy.ControllerSettings(eventController);

            string responseContent = this.pageOperations.AddWidgetToPageAndRequest(mvcProxy);

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
            var eventController = new EventController();
            eventController.Model.SortExpression = "Title DESC";
            eventController.Model.DisplayMode = Telerik.Sitefinity.Frontend.Mvc.Models.ListDisplayMode.All;

            var items = eventController.Model.CreateListViewModel(1).Items.ToArray();

            for (int i = 1; i <= EventWidgetListSettingsTests.EventsCount; i++)
            {
                Assert.AreEqual(EventWidgetListSettingsTests.BaseEventTitle + i, (string)items[EventWidgetListSettingsTests.EventsCount - i].Fields.Title, "The event with this title was not found!");
            }
        }

        /// <summary>
        /// Verifies that selected templates is used in List mode.
        /// </summary>
        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.FeatherTeam)]
        [Description("Verifies that selected templates is used in List mode.")]
        public void EventWidget_SelectListTemplate()
        {
            string textEdited = "<p> Test paragraph </p>";
            string paragraphText = "Test paragraph";

            string listTemplate = "ListPageNew";
            var templateOperation = new TemplateOperations();
            var listTemplatePath = Path.Combine(templateOperation.SfPath, "ResourcePackages", "Bootstrap", "MVC", "Views", "Event", "List.Default.cshtml");
            var newListTemplatePath = Path.Combine(templateOperation.SfPath, "MVC", "Views", "Shared", "List.ListPageNew.cshtml");

            try
            {
                File.Copy(listTemplatePath, newListTemplatePath);
                this.EditFile(newListTemplatePath, textEdited);

                var mvcProxy = new MvcControllerProxy();
                mvcProxy.ControllerName = typeof(EventController).FullName;
                var eventController = new EventController();
                eventController.ListTemplateName = listTemplate;
                mvcProxy.Settings = new ControllerSettings(eventController);

                string responseContent = this.pageOperations.AddWidgetToPageAndRequest(mvcProxy);

                Assert.IsTrue(responseContent.Contains(BaseEventTitle + 1), "The event with this title was not found!");
                Assert.IsTrue(responseContent.Contains(paragraphText), "The event with this template was not found!");
            }
            finally
            {
                File.Delete(newListTemplatePath);
            }
        }

        #region Helper methods

        private void EditFile(string newListTemplatePath, string textEdited)
        {
            using (StreamWriter output = File.AppendText(newListTemplatePath))
            {
                output.WriteLine(textEdited);
            }
        }

        #endregion

        private PagesOperations pageOperations;
        private const int EventsCount = 5;
        private const string BaseEventTitle = "TestEvent";
    }
}
