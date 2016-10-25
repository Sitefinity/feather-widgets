using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FeatherWidgets.TestUtilities.CommonOperations;
using Telerik.Sitefinity.Events.Model;
using Telerik.Sitefinity.Frontend.Events.Mvc.Controllers;
using Telerik.Sitefinity.Modules.Events;
using Telerik.Sitefinity.Mvc.Proxy;
using Telerik.Sitefinity.TestArrangementService.Attributes;
using Telerik.Sitefinity.TestUI.Arrangements.Framework.Server;
using Telerik.Sitefinity.TestUtilities.CommonOperations;

namespace FeatherWidgets.TestUI.Arrangements
{
    public class VerifyModifiedEditableCaledarWidgetOnHybridPage : TestArrangementBase
    {
        /// <summary>
        /// Creates 2 Events.
        /// Creates Hybrid page template.
        /// </summary>
        [ServerSetUp]
        public void OnBeforeTestsStarts()
        {
            var templateId = ServerOperations.Templates().CreateHybridMVCPageTemplate(CaptionCalendar);

            ServerOperations.Events().CreateCalendar(this.calendar1Guid, Calendar1Title);
            ServerOperations.Events().CreateEvent(Event1Title, string.Empty, IsAllDay, this.eventStart, this.eventEnd, this.calendar1Guid);
            var event1Item = EventsManager.GetManager()
                .GetEvents()
                .Where<Event>(ni => ni.Status == Telerik.Sitefinity.GenericContent.Model.ContentLifecycleStatus.Live && ni.Title == Event1Title)
                .FirstOrDefault();
            ServerArrangementContext.GetCurrent().Values.Add("event1Id", event1Item.Id.ToString());

            ServerOperations.Events().CreateCalendar(this.calendar2Guid, Calendar2Title);
            ServerOperations.Events().CreateEvent(Event2Title, string.Empty, IsAllDay, this.eventStart, this.eventEnd, this.calendar2Guid);
            var event2Item = EventsManager.GetManager()
                .GetEvents().Where<Event>(ni => ni.Status == Telerik.Sitefinity.GenericContent.Model.ContentLifecycleStatus.Live && ni.Title == Event2Title)
                .FirstOrDefault();
            ServerArrangementContext.GetCurrent().Values.Add("event2Id", event2Item.Id.ToString());
        }

        /// <summary>
        /// Deletes all Events, Pages and Templates
        /// </summary>
        [ServerTearDown]
        public void OnAfterTestCompletes()
        {
            ServerOperations.Pages().DeleteAllPages();
            ServerOperations.Events().DeleteAllCalendarsExceptDefaultOne();
            ServerOperations.Events().DeleteAllEvents();
            ServerOperations.Templates().DeletePageTemplate(CaptionCalendar);
        }

        private const string CaptionCalendar = "Calendar";
        private const string PlaceHolderId = "Body";

        private readonly Guid calendar1Guid = Guid.NewGuid();
        private const string Calendar1Title = "Calendar1";
        private const string Event1Title = "Event1Title";

        private readonly Guid calendar2Guid = Guid.NewGuid();
        private const string Calendar2Title = "Calendar2";
        private const string Event2Title = "Event2Title";

        private const bool IsAllDay = false;
        private readonly DateTime eventStart = DateTime.Now;
        private readonly DateTime eventEnd = DateTime.Now.AddHours(1.0);
    }
}
