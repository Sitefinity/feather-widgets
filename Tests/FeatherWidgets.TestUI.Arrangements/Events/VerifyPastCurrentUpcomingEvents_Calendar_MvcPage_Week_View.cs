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
    /// Executes Server Side operations for VerifyPastCurrentUpcomingEvents_Calendar_MvcPage_Week_View UI Test.
    /// </summary>
    public class VerifyPastCurrentUpcomingEvents_Calendar_MvcPage_Week_View : TestArrangementBase
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

            var basicCurrentEventStartTime = DateTime.Now;
            var currentEventStartTime = new DateTime(basicCurrentEventStartTime.Year, basicCurrentEventStartTime.Month, basicCurrentEventStartTime.Day, basicCurrentEventStartTime.Hour, basicCurrentEventStartTime.Minute, basicCurrentEventStartTime.Second);
            var basicCurrentEventEndTime = DateTime.Now.AddHours(1);
            var currentEventEndTime = new DateTime(basicCurrentEventEndTime.Year, basicCurrentEventEndTime.Month, basicCurrentEventEndTime.Day, basicCurrentEventEndTime.Hour, basicCurrentEventEndTime.Minute, basicCurrentEventEndTime.Second);

            ServerOperations.Events().CreateEvent(CurrentEventTitle, string.Empty, false, currentEventStartTime, currentEventEndTime);
            var currentEventExpectedStartTime = this.FormatTime(currentEventStartTime);
            var currentEventExpectedEndTime = this.FormatTime(currentEventEndTime);
            ServerArrangementContext.GetCurrent().Values.Add("currentEventStartTime", currentEventExpectedStartTime);
            ServerArrangementContext.GetCurrent().Values.Add("currentEventEndTime", currentEventExpectedEndTime);

            var currentEventItem = EventsManager.GetManager().GetEvents().Where<Event>(ni => ni.Status == Telerik.Sitefinity.GenericContent.Model.ContentLifecycleStatus.Live
               && ni.Title == CurrentEventTitle).FirstOrDefault();
            ServerArrangementContext.GetCurrent().Values.Add("currentEventId", currentEventItem.Id.ToString());

            var basicBasePastInOneDayEventStartTime = DateTime.Now.AddDays(-1);
            var basePastInOneDayEventStartTime = new DateTime(basicBasePastInOneDayEventStartTime.Year, basicBasePastInOneDayEventStartTime.Month, basicBasePastInOneDayEventStartTime.Day, basicBasePastInOneDayEventStartTime.Hour, basicBasePastInOneDayEventStartTime.Minute, basicBasePastInOneDayEventStartTime.Second);
            var basicBasePastInOneDayEventEndTime = DateTime.Now.AddDays(-1);
            var basePastInOneDayEventEndTime = new DateTime(basicBasePastInOneDayEventEndTime.Year, basicBasePastInOneDayEventEndTime.Month, basicBasePastInOneDayEventEndTime.Day, basicBasePastInOneDayEventEndTime.Hour, basicBasePastInOneDayEventEndTime.Minute, basicBasePastInOneDayEventEndTime.Second);

            ServerOperations.Events().CreateEvent(BasePastInOneDayEventTitle, string.Empty, false, basePastInOneDayEventStartTime, basePastInOneDayEventEndTime);
            var basePastInOneDayEventExpectedStartTime = this.FormatTime(basePastInOneDayEventStartTime);
            var basePastInOneDayEventExpectedEndTime = this.FormatTime(basePastInOneDayEventEndTime);
            ServerArrangementContext.GetCurrent().Values.Add("basePastInOneDayEventStartTime", basePastInOneDayEventExpectedStartTime);
            ServerArrangementContext.GetCurrent().Values.Add("basePastInOneDayEventEndTime", basePastInOneDayEventExpectedEndTime);

            var pastEventItem = EventsManager.GetManager().GetEvents().Where<Event>(ni => ni.Status == Telerik.Sitefinity.GenericContent.Model.ContentLifecycleStatus.Live
          && ni.Title == BasePastInOneDayEventTitle).FirstOrDefault();
            ServerArrangementContext.GetCurrent().Values.Add("pastEventId", pastEventItem.Id.ToString());

            var basicBaseUpcomingInOneDayEventStartTime = DateTime.Now.AddDays(1);
            var baseUpcomingInOneDayEventStartTime = new DateTime(basicBaseUpcomingInOneDayEventStartTime.Year, basicBaseUpcomingInOneDayEventStartTime.Month, basicBaseUpcomingInOneDayEventStartTime.Day, basicBaseUpcomingInOneDayEventStartTime.Hour, basicBaseUpcomingInOneDayEventStartTime.Minute, basicBaseUpcomingInOneDayEventStartTime.Second);
            var basicBaseUpcomingInOneDayEventEndTime = DateTime.Now.AddDays(1);
            var baseUpcomingInOneDayEventEndTime = new DateTime(basicBaseUpcomingInOneDayEventEndTime.Year, basicBaseUpcomingInOneDayEventEndTime.Month, basicBaseUpcomingInOneDayEventEndTime.Day, basicBaseUpcomingInOneDayEventEndTime.Hour, basicBaseUpcomingInOneDayEventEndTime.Minute, basicBaseUpcomingInOneDayEventEndTime.Second);

            ServerOperations.Events().CreateEvent(BaseUpcomingInOneDayEventTitle, string.Empty, false, baseUpcomingInOneDayEventStartTime, baseUpcomingInOneDayEventEndTime);
            var baseUpcomingInOneDayEventExpectedStartTime = this.FormatTime(baseUpcomingInOneDayEventStartTime);
            var baseUpcomingInOneDaytExpectedEndTime = this.FormatTime(baseUpcomingInOneDayEventEndTime);
            ServerArrangementContext.GetCurrent().Values.Add("baseUpcomingInOneDayEventStartTime", baseUpcomingInOneDayEventExpectedStartTime);
            ServerArrangementContext.GetCurrent().Values.Add("baseUpcomingInOneDayEventEndTime", baseUpcomingInOneDaytExpectedEndTime);

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

        /// <summary>
        /// Format event time
        /// </summary>
        /// <param name="eventTime"></param>
        /// <returns>Expected event time</returns>
        private string FormatTime(DateTime eventTime)
        {
            var formattedEventTime = string.Format(CultureInfo.InvariantCulture, "{0:ddd MMM dd yyyy HH:mm:ss}", eventTime);
            return formattedEventTime;
        }

        private const string CurrentEventTitle = "CurrentEvent_Title";
        private const string BasePastInOneDayEventTitle = "PastInOneDayEvent_Title";
        private const string BaseUpcomingInOneDayEventTitle = "UpcomingInOneDayEvent_Title";

        private const string PageTitle = "EventsPage";
        private const string TemplateTitle = "Bootstrap.default";
        private const string PlaceHolderId = "Contentplaceholder1";
    }
}
