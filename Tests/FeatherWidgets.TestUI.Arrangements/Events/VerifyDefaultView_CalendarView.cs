using System;
using FeatherWidgets.TestUtilities.CommonOperations;
using Telerik.Sitefinity.TestArrangementService.Attributes;
using Telerik.Sitefinity.TestUtilities.CommonOperations;

namespace FeatherWidgets.TestUI.Arrangements
{
    /// <summary>
    /// Executes Server Side operations for VerifyDefaultView_CalendarView UI Test
    /// </summary>
    public class VerifyDefaultView_CalendarView : TestArrangementBase
    {
        /// <summary>
            /// Creates an Event.
            /// Creates a Page with Events Widget.
            /// </summary>
            [ServerSetUp]
            public void SetUp()
            {
                var templateId = ServerOperations.Templates().GetTemplateIdByTitle(TemplateTitle);
                Guid page1Id = ServerOperations.Pages().CreatePage(PageTitle, templateId);
                var page1NodeId = ServerOperations.Pages().GetPageNodeId(page1Id);
                ServerOperationsFeather.Pages().AddCalendarWidgetToPage(page1NodeId, PlaceHolderId);
            }

            /// <summary>
            /// Deletes all Events and Pages
            /// </summary>
            [ServerTearDown]
            public void TearDown()
            {
                ServerOperations.Pages().DeleteAllPages();
            }

            private const string PageTitle = "EventsPage";
            private const string TemplateTitle = "Bootstrap.default";
            private const string PlaceHolderId = "Contentplaceholder1";
    }
}
