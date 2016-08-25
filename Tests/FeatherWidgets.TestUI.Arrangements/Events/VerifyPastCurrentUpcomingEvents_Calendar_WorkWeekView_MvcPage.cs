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
    /// Executes Server Side operations for VerifyPastCurrentUpcomingEvents_Calendar_WorkWeekView_MvcPage UI Test.
    /// </summary>
    public class VerifyPastCurrentUpcomingEvents_Calendar_WorkWeekView_MvcPage : TestArrangementBase
    {
        /// <summary>
        /// Creates an Event.
        /// Creates a Page with Events Widget.
        /// </summary>
        [ServerSetUp]
        public void OnBeforeTestsStarts()
        {
            var templateId = ServerOperations.Templates().GetTemplateIdByTitle(TemplateTitle);
            Guid pageId = ServerOperations.Pages().CreatePage(PageTitle, templateId);
            var pageNodeId = ServerOperations.Pages().GetPageNodeId(pageId);
            ServerOperationsFeather.Pages().AddCalendarWidgetToPage(pageNodeId, PlaceHolderId);

            ServerOperations.Events().CreateEvent(CurrentEventTitle, string.Empty, false, this.currentEventStartTime, this.currentEventEndTime);
            var currentEventItem = EventsManager.GetManager().GetEvents().Where<Event>(ni => ni.Status == Telerik.Sitefinity.GenericContent.Model.ContentLifecycleStatus.Live
               && ni.Title == CurrentEventTitle).FirstOrDefault();
            ServerArrangementContext.GetCurrent().Values.Add("currentEventId", currentEventItem.Id.ToString());

            ServerOperations.Events().CreateEvent(BasePastInOneDayEventTitle, string.Empty, false, this.basePastInOneDayEventStartTime, this.basePastInOneDayEventEndTime);
            var pastEventItem = EventsManager.GetManager().GetEvents().Where<Event>(ni => ni.Status == Telerik.Sitefinity.GenericContent.Model.ContentLifecycleStatus.Live
              && ni.Title == BasePastInOneDayEventTitle).FirstOrDefault();
            ServerArrangementContext.GetCurrent().Values.Add("pastEventId", pastEventItem.Id.ToString());

            ServerOperations.Events().CreateEvent(BaseUpcomingInOneDayEventTitle, string.Empty, false, this.baseUpcomingInOneDayEventStartTime, this.baseUpcomingInOneDayEventEndTime);
            var upcomingEventItem = EventsManager.GetManager().GetEvents().Where<Event>(ni => ni.Status == Telerik.Sitefinity.GenericContent.Model.ContentLifecycleStatus.Live
              && ni.Title == BaseUpcomingInOneDayEventTitle).FirstOrDefault();
            ServerArrangementContext.GetCurrent().Values.Add("upcomingEventId", upcomingEventItem.Id.ToString());

            ServerArrangementContext.GetCurrent().Values.Add("timezoneId", TimeZoneInfo.Utc.ToString());
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

        private const string CurrentEventTitle = "CurrentEvent_Title";
        private readonly DateTime currentEventStartTime = new DateTime(2016, 7, 12, 10, 0, 0);
        private readonly DateTime currentEventEndTime = new DateTime(2016, 7, 12, 11, 0, 0);
        private const string BasePastInOneDayEventTitle = "PastInOneDayEvent_Title";
        private readonly DateTime basePastInOneDayEventStartTime = new DateTime(2016, 7, 11, 10, 0, 0);
        private readonly DateTime basePastInOneDayEventEndTime = new DateTime(2016, 7, 11, 11, 0, 0);
        private const string BaseUpcomingInOneDayEventTitle = "UpcomingInOneDayEvent_Title";
        private readonly DateTime baseUpcomingInOneDayEventStartTime = new DateTime(2016, 7, 13, 10, 0, 0);
        private readonly DateTime baseUpcomingInOneDayEventEndTime = new DateTime(2016, 7, 13, 11, 0, 0);

        private const string PageTitle = "EventsPage";
        private const string TemplateTitle = "Bootstrap.default";
        private const string PlaceHolderId = "Contentplaceholder1";
    }
}
