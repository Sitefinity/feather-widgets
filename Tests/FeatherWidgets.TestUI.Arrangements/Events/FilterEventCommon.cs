using System;
using FeatherWidgets.TestUI.Arrangements.Events;
using FeatherWidgets.TestUtilities.CommonOperations;
using Telerik.Sitefinity.TestArrangementService.Attributes;
using Telerik.Sitefinity.TestUtilities.CommonOperations;

namespace FeatherWidgets.TestUI.Arrangements
{
    /// <summary>
    /// FilterEventCommon arrangement class.
    /// </summary>
    public class FilterEventCommon : TestArrangementBase
    {
        /// <summary>
        /// Server side set up.
        /// </summary>
        [ServerSetUp]
        public void SetUp()
        {
            Guid pageId = ServerOperations.Pages().CreatePage(PageName);
            EventsTestsCommon.CreateEvents();
            ServerOperationsFeather.Pages().AddEventsWidgetToPage(pageId);
        }

        /// <summary>
        /// Tears down.
        /// </summary>
        [ServerTearDown]
        public void TearDown()
        {
            ServerOperations.Pages().DeletePage(PageName);
            ServerOperations.Events().DeleteAllEvents();
        }

        private const string PageName = "EventsPage";
    }
}
