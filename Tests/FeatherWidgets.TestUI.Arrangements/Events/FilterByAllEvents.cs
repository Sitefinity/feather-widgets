using FeatherWidgets.TestUI.Arrangements.Events;
using Telerik.Sitefinity.TestArrangementService.Attributes;
using Telerik.Sitefinity.TestUtilities.CommonOperations;

namespace FeatherWidgets.TestUI.Arrangements
{
    /// <summary>
    /// FilterByAllEvents arrangement class.
    /// </summary>
    public class FilterByAllEvents
    {
        /// <summary>
        /// Server side set up.
        /// </summary>
        [ServerSetUp]
        public void SetUp()
        {
            ServerOperations.Pages().CreatePage(PageName);
            EventsTestsCommon.CreateEvents();
        }

        /// <summary>
        /// Tears down.
        /// </summary>
        [ServerTearDown]
        public void TearDown()
        {
            ServerOperations.Events().DeleteAllEvents();
            ServerOperations.Pages().DeletePage(PageName);
        }

        private const string PageName = "FilterByAllEvents";
    }
}
