﻿using System;
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
    /// Executes Server Side operations for VerifyRecurrentAllDayEventsWithNoEndDate_CalendarView UI Test.
    /// </summary>
    public class VerifyRecurrentAllDayEventsWithNoEndDate_CalendarView : TestArrangementBase
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

            var еventStartTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0, DateTimeKind.Utc);
            var еventEndTime = DateTime.Now.AddYears(5);
            ServerOperations.Events().CreаteAllDayRecurrentEvent(Event1Title, string.Empty, еventStartTime, еventEndTime, 1440, this.occurenceCount, 1, true);
            var event1Item = EventsManager.GetManager().GetEvents().Where<Event>(ni => ni.Status == Telerik.Sitefinity.GenericContent.Model.ContentLifecycleStatus.Live
               && ni.Title == Event1Title).FirstOrDefault();
            ServerArrangementContext.GetCurrent().Values.Add("event1Id", event1Item.Id.ToString());
            var еventExpectedStartTime = this.FormatTime(еventStartTime);
            ServerArrangementContext.GetCurrent().Values.Add("еventStartTime", еventExpectedStartTime);
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
            var formattedEventTime = string.Format(CultureInfo.InvariantCulture, "{0:yyyy}", eventTime);
            return formattedEventTime;
        }

        private const string PageTitle = "EventsPage";
        private const string Event1Title = "Event1Title";
        private const string TemplateTitle = "Bootstrap.default";
        private const string PlaceHolderId = "Contentplaceholder1";
        private readonly int occurenceCount = int.MaxValue;
    }
}
