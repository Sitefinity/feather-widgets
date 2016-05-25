using System;
using FeatherWidgets.TestUtilities.CommonOperations;
using Telerik.Sitefinity.TestArrangementService.Attributes;
using Telerik.Sitefinity.TestUtilities.CommonOperations;

namespace FeatherWidgets.TestUI.Arrangements
{
    /// <summary>
    /// VerifyUseLimitListSettingsOnFrontendPageForEventsWidget arrangement class.
    /// </summary>
    public class VerifyUseLimitListSettingsOnFrontendPageForEventsWidget : TestArrangementBase
    {
        /// <summary>
        /// Server side set up.
        /// </summary>
        [ServerSetUp]
        public void SetUp()
        {
            Guid pageId = ServerOperations.Pages().CreatePage(PageName);

            for (int i = 1; i < 6; i++)
            {
                ServerOperations.Events().CreateEvent(EventsTitle + i);
            }
            
            ServerOperationsFeather.Pages().AddEventsWidgetToPage(pageId, "Body");
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

        private const string PageName = "EventsPage";
        private const string EventsTitle = "TestEvent";
    }
}
