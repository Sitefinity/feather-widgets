using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FeatherWidgets.TestUtilities.CommonOperations;
using Telerik.Sitefinity;
using Telerik.Sitefinity.Events.Model;
using Telerik.Sitefinity.Modules.Events;
using Telerik.Sitefinity.TestArrangementService.Attributes;
using Telerik.Sitefinity.TestUI.Arrangements.Framework.Server;
using Telerik.Sitefinity.TestUtilities.CommonOperations;

namespace FeatherWidgets.TestUI.Arrangements
{
    /// <summary>
    /// Executes Server Side operations for VerifyAllDayEventSwitchingDaylightSavingTime_CalendarView UI Test.
    /// </summary>
    public class VerifyAllDayEventSwitchingDaylightSavingTime_CalendarView : TestArrangementBase
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

            var event1End = this.event1Start.AddMonths(3).AddDays(1);

            ServerOperations.Events().CreateAllDayEvent(Event1Title, string.Empty, this.event1Start, event1End);
            var event1Item = EventsManager.GetManager().GetEvents().Where<Event>(ni => ni.Status == Telerik.Sitefinity.GenericContent.Model.ContentLifecycleStatus.Live
                && ni.Title == Event1Title).FirstOrDefault();
            ServerArrangementContext.GetCurrent().Values.Add("event1Id", event1Item.Id.ToString());
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

        private const string Event1Title = "Event1Title";
        private readonly DateTime event1Start = new DateTime(2016, 1, 10, 0, 0, 0, DateTimeKind.Utc);

        private const string PageTitle = "EventsPage";
        private const string TemplateTitle = "Bootstrap.default";
        private const string PlaceHolderId = "Contentplaceholder1";
    }
}
