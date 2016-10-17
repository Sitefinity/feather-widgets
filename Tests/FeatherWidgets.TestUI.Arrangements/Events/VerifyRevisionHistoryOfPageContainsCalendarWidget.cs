﻿using System;
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
    public class VerifyRevisionHistoryOfPageContainsCalendarWidget : TestArrangementBase
    {
        /// <summary>
        /// Creates 2 Events.
        /// Creates pure MVC page template with Calendat Widget
        /// Creates a Page based on created page template.
        /// </summary>
        [ServerSetUp]
        public void OnBeforeTestsStarts()
        {
            ServerOperations.Events().CreateEvent(Event1Title, string.Empty, false, this.eventStart, this.eventEnd);
            var event1Item = EventsManager.GetManager()
                .GetEvents()
                .Where<Event>(ni => ni.Status == Telerik.Sitefinity.GenericContent.Model.ContentLifecycleStatus.Live && ni.Title == Event1Title)
                .FirstOrDefault();
            ServerArrangementContext.GetCurrent().Values.Add("event1Id", event1Item.Id.ToString());

            ServerOperations.Events().CreateEvent(Event2Title, string.Empty, false, this.eventStart, this.eventEnd);
            var event2Item = EventsManager.GetManager()
                .GetEvents().Where<Event>(ni => ni.Status == Telerik.Sitefinity.GenericContent.Model.ContentLifecycleStatus.Live && ni.Title == Event2Title)
                .FirstOrDefault();
            ServerArrangementContext.GetCurrent().Values.Add("event2Id", event2Item.Id.ToString());

            ServerOperations.Pages().CreatePage(PageTitle);
        }

        /// <summary>
        /// Deletes all Events, Pages and Templates
        /// </summary>
        [ServerTearDown]
        public void OnAfterTestCompletes()
        {
            ServerOperations.Pages().DeleteAllPages();
            ServerOperations.Events().DeleteAllEvents();
        }

        private const string PageTitle = "CalendarPage";
        private const string Event1Title = "Event1Title";
        private const string Event2Title = "Event2Title";

        private const bool IsAllDay = false;
        private readonly DateTime eventStart = DateTime.Now;
        private readonly DateTime eventEnd = DateTime.Now.AddHours(1.0);
    }
}
