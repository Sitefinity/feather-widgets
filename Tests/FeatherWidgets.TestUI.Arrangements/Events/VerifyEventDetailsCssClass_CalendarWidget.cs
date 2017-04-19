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
    /// Executes Server Side operations for VerifyEventDetailsCssClass_CalendarWidget UI Test
    /// </summary>
    public class VerifyEventDetailsCssClass_CalendarWidget : TestArrangementBase
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

            var basicCurrentEventStartTime = DateTime.Now;
            var currentEventStartTime = new DateTime(basicCurrentEventStartTime.Year, basicCurrentEventStartTime.Month, basicCurrentEventStartTime.Day, basicCurrentEventStartTime.Hour, basicCurrentEventStartTime.Minute, basicCurrentEventStartTime.Second);
            var basicCurrentEventEndTime = DateTime.Now.AddHours(1);
            var currentEventEndTime = new DateTime(basicCurrentEventEndTime.Year, basicCurrentEventEndTime.Month, basicCurrentEventEndTime.Day, basicCurrentEventEndTime.Hour, basicCurrentEventEndTime.Minute, basicCurrentEventEndTime.Second);
            ServerOperations.Events().CreateEvent(EventTitle, string.Empty, false, currentEventStartTime, currentEventEndTime);
            var currentEventItem = EventsManager.GetManager()
                .GetEvents()
                .Where<Event>(ni => ni.Status == Telerik.Sitefinity.GenericContent.Model.ContentLifecycleStatus.Live && ni.Title == EventTitle)
                .FirstOrDefault();
            ServerArrangementContext.GetCurrent().Values.Add("currentEventId", currentEventItem.Id.ToString());
        }

        /// <summary>
        /// Deletes all Events and Pages
        /// </summary>
        [ServerTearDown]
        public void OnAfterTestCompletes()
        {
            ServerOperations.Pages().DeleteAllPages();
            ServerOperations.Events().DeleteAllEvents();
        }

        private const string PageTitle = "EventsPage";
        private const string TemplateTitle = "Bootstrap.default";
        private const string PlaceHolderId = "Contentplaceholder1";
        private const string EventTitle = "EventTitle";
    }
}
