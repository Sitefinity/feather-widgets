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
    /// Executes Server Side operations for CalendarWidgetVerifyEventItemInDifferentPageInPreviewModeAndFrontendPage UI Test
    /// </summary>
    public class CalendarWidgetVerifyEventItemInDifferentPageInPreviewModeAndFrontendPage : TestArrangementBase
    {
            /// <summary>
            /// Creates an Event.
            /// Creates a Page with Events Widget.
            /// </summary>
            [ServerSetUp]
            public void OnBeforeTestsStarts()
            {
                var templateId = ServerOperations.Templates().GetTemplateIdByTitle(TemplateTitle);

                Guid page1Id = ServerOperations.Pages().CreatePage(Page1Title, templateId);
                var page1NodeId = ServerOperations.Pages().GetPageNodeId(page1Id);
                ServerOperationsFeather.Pages().AddCalendarWidgetToPage(page1NodeId, PlaceHolderId);

                Guid page2Id = ServerOperations.Pages().CreatePage(Page2Title, templateId);
                var page2NodeId = ServerOperations.Pages().GetPageNodeId(page2Id);
                ServerOperationsFeather.Pages().AddCalendarWidgetToPage(page2NodeId, PlaceHolderId);

                Guid page3Id = ServerOperations.Pages().CreatePage(Page3Title, templateId);
                var page3NodeId = ServerOperations.Pages().GetPageNodeId(page3Id);
                ServerOperationsFeather.Pages().AddEventsWidgetToPage(page3NodeId, PlaceHolderId);

                Guid page4Id = ServerOperations.Pages().CreatePage(Page4Title, templateId);
                var page4NodeId = ServerOperations.Pages().GetPageNodeId(page4Id);
                ServerOperationsFeather.Pages().AddCalendarWidgetToPage(page4NodeId, PlaceHolderId);

                var basicCurrentEventStartTime = DateTime.Now;
                var currentEventStartTime = new DateTime(basicCurrentEventStartTime.Year, basicCurrentEventStartTime.Month, basicCurrentEventStartTime.Day, basicCurrentEventStartTime.Hour, basicCurrentEventStartTime.Minute, basicCurrentEventStartTime.Second);
                var basicCurrentEventEndTime = DateTime.Now.AddHours(1);
                var currentEventEndTime = new DateTime(basicCurrentEventEndTime.Year, basicCurrentEventEndTime.Month, basicCurrentEventEndTime.Day, basicCurrentEventEndTime.Hour, basicCurrentEventEndTime.Minute, basicCurrentEventEndTime.Second);
                ServerOperations.Events().CreateEvent(EventTitle, EventContent, false, currentEventStartTime, currentEventEndTime);
                var currentEventExpectedStartTime = this.FormatTime(currentEventStartTime);
                ServerArrangementContext.GetCurrent().Values.Add("currentEventStartTime", currentEventExpectedStartTime);
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

            /// <summary>
            /// Format event time
            /// </summary>
            /// <param name="eventTime"></param>
            /// <returns>Expected event time</returns>
            private string FormatTime(DateTime eventTime)
            {
                var formattedEventTime = string.Format(CultureInfo.InvariantCulture, "{0:yyyy/MM/dd}", eventTime);
                return formattedEventTime;
            }

            private const string Page1Title = "PrimaryPage_CalendarWidget";
            private const string Page2Title = "SecondaryPage_CalendarWidget";
            private const string Page3Title = "PrimaryPage_EventsWidget";
            private const string Page4Title = "SecondaryPage_EventsWidget";
            private const string EventTitle = "EventTitle";
            private const string EventContent = "This is a test content";
            private const string TemplateTitle = "Bootstrap.default";
            private const string PlaceHolderId = "Contentplaceholder1";
    }
}