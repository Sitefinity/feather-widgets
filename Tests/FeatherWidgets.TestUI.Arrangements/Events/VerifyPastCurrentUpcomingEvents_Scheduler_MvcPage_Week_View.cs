using System;
using System.Globalization;
using FeatherWidgets.TestUtilities.CommonOperations;
using Telerik.Sitefinity.TestArrangementService.Attributes;
using Telerik.Sitefinity.TestUI.Arrangements.Framework.Server;
using Telerik.Sitefinity.TestUtilities.CommonOperations;

namespace FeatherWidgets.TestUI.Arrangements
{
    /// <summary>
    /// Executes Server Side operations for VerifyPastCurrentUpcomingEvents_Scheduler_MvcPage_Week_View UI Test.
    /// </summary>
    public class VerifyPastCurrentUpcomingEvents_Scheduler_MvcPage_Week_View : TestArrangementBase
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
            ServerOperationsFeather.Pages().AddEventsWidgetToPage(pageNodeId, PlaceHolderId);

            var currentEventStartTime = DateTime.Now;
            var currentEventEndTime = DateTime.Now.AddHours(1);

            ServerOperations.Events().CreateEvent(CurrentEventTitle, string.Empty, false, currentEventStartTime, currentEventEndTime);
            var currentEventExpectedStartTime = this.FormatTime(currentEventStartTime);
            var currentEventExpectedEndTime = this.FormatTime(currentEventEndTime);
            ServerArrangementContext.GetCurrent().Values.Add("currentEventStartTime", currentEventExpectedStartTime);
            ServerArrangementContext.GetCurrent().Values.Add("currentEventEndTime", currentEventExpectedEndTime);

            var basePastInOneDayEventStartTime = DateTime.Now.AddDays(-1);
            var basePastInOneDayEventEndTime = DateTime.Now.AddDays(-1);

            ServerOperations.Events().CreateEvent(BasePastInOneDayEventTitle, string.Empty, false, basePastInOneDayEventStartTime, basePastInOneDayEventEndTime);
            var basePastInOneDayEventExpectedStartTime = this.FormatTime(basePastInOneDayEventStartTime);
            var basePastInOneDayEventExpectedEndTime = this.FormatTime(basePastInOneDayEventEndTime);
            ServerArrangementContext.GetCurrent().Values.Add("basePastInOneDayEventStartTime", basePastInOneDayEventExpectedStartTime);
            ServerArrangementContext.GetCurrent().Values.Add("basePastInOneDayEventEndTime", basePastInOneDayEventExpectedEndTime);

            var baseUpcomingInOneDayEventStartTime = DateTime.Now.AddDays(1);
            var baseUpcomingInOneDayEventEndTime = DateTime.Now.AddDays(1);

            ServerOperations.Events().CreateEvent(BaseUpcomingInOneDayEventTitle, string.Empty, false, baseUpcomingInOneDayEventStartTime, baseUpcomingInOneDayEventEndTime);
            var baseUpcomingInOneDayEventExpectedStartTime = this.FormatTime(baseUpcomingInOneDayEventStartTime);
            var baseUpcomingInOneDaytExpectedEndTime = this.FormatTime(baseUpcomingInOneDayEventEndTime);
            ServerArrangementContext.GetCurrent().Values.Add("baseUpcomingInOneDayEventStartTime", baseUpcomingInOneDayEventExpectedStartTime);
            ServerArrangementContext.GetCurrent().Values.Add("baseUpcomingInOneDayEventEndTime", baseUpcomingInOneDaytExpectedEndTime);
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
        private const string TemplateTitle = "default";
        private const string PlaceHolderId = "Contentplaceholder1";
    }
}
