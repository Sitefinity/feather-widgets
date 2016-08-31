using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FeatherWidgets.TestUtilities.CommonOperations;
using Telerik.Sitefinity.Events.Model;
using Telerik.Sitefinity.Modules.Events;
using Telerik.Sitefinity.TestArrangementService.Attributes;
using Telerik.Sitefinity.TestUI.Arrangements.Framework.Server;
using Telerik.Sitefinity.TestUtilities.CommonOperations;

namespace FeatherWidgets.TestUI.Arrangements
{
    /// <summary>
    /// Executes Server Side operations for VerifyWeekStartDay_CalendarView UI Test
    /// </summary>
    public class VerifyWeekStartDay_CalendarView
    {
        /// <summary>
            /// Creates an Event.
            /// Creates a Page with Events Widget.
            /// </summary>
            [ServerSetUp]
            public void OnBeforeTestsStarts()
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
            public void OnAfterTestCompletes()
            {
                ServerOperations.Pages().DeleteAllPages();
            }

            private const string PageTitle = "EventsPage";
            private const string TemplateTitle = "Bootstrap.default";
            private const string PlaceHolderId = "Contentplaceholder1";
    }
}
