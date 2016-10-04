using System;
using FeatherWidgets.TestUtilities.CommonOperations;
using Telerik.Sitefinity.TestArrangementService.Attributes;
using Telerik.Sitefinity.TestUtilities.CommonOperations;

namespace FeatherWidgets.TestUI.Arrangements
{
    /// <summary>
    /// This is a class with arrangement methods related to UI test BreadcrumbDetailEventItemVirtualNodes
    /// </summary>
    public class BreadcrumbDetailEventItemVirtualNodes : TestArrangementBase
    {
        /// <summary>
        /// Server side set up.
        /// </summary>
        [ServerSetUp]
        public void SetUp()
        {
            Guid pageId = ServerOperations.Pages().CreatePage(PageName);
            ServerOperations.Events().CreateEvent(EventsTitle);

            ServerOperationsFeather.Pages().AddEventsWidgetToPage(pageId, PlaceHolderId);
            ServerOperationsFeather.Pages().AddBreadcrumbWidgetToPage(pageId, PlaceHolderId);
        }

        /// <summary>
        /// Tears down.
        /// </summary>
        [ServerTearDown]
        public void TearDown()
        {
            ServerOperations.Events().DeleteAllEvents();
            ServerOperations.Pages().DeleteAllPages();
        }

        private const string PageName = "TestPageWithEventsWidget";
        private const string PlaceHolderId = "Body";
        private const string EventsTitle = "TestEvent1";
    }
}
