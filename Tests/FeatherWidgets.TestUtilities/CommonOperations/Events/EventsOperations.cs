using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Telerik.Sitefinity.Events.Model;
using Telerik.Sitefinity.Modules.Events;
using Telerik.Sitefinity.RecurrentRules;

namespace FeatherWidgets.TestUtilities.CommonOperations.Events
{
    /// <summary>
    /// This class provides access to events common server operations
    /// </summary>
    public class EventsOperations
    {
        public Guid CreateEvent(string eventTitle, DateTime startDateTime, bool isAllDayEvent)
        {
            EventsManager manager = EventsManager.GetManager();

            bool isExiting = manager.GetEvents().Where(i => i.Title == eventTitle).FirstOrDefault() != null;
            if (isExiting)
            {
                throw new ArgumentException("Event " + eventTitle + " already exists");
            }

            Guid id = Guid.NewGuid();
            Event nonrecurrentEvent = manager.CreateEvent(id);

            nonrecurrentEvent.Title = eventTitle;
            ////nonrecurrentEvent.Content = eventContent;
            nonrecurrentEvent.EventStart = startDateTime;
            ////nonrecurrentEvent.TimeZoneId = timeZoneId;
            nonrecurrentEvent.IsRecurrent = false;
            nonrecurrentEvent.AllDayEvent = isAllDayEvent;
            nonrecurrentEvent.UrlName = Regex.Replace(eventTitle.ToLower(), @"[^\w\-\!\$\'\(\)\=\@\d_]+", "-");

            manager.Lifecycle.Publish(nonrecurrentEvent);
            manager.SaveChanges();

            return id;
        }

        public Guid CreateDailyRecurrentEvent(string eventTitle, DateTime eventStart, DateTime eventEnd, int minutesDuration, int occurrencesCount, int occurrenceIntervalInDays, string timeZoneId)
        {
            var manager = EventsManager.GetManager();
            bool isExiting = manager.GetEvents().Where(i => i.Title == eventTitle).FirstOrDefault() != null;

            if (isExiting)
            {
                throw new ArgumentException("Event " + eventTitle + " already exists");
            }

            var eventGuid = Guid.NewGuid();
            var eventToCreate = manager.CreateEvent(eventGuid);
            eventToCreate.Title = eventTitle;
            eventToCreate.EventStart = eventStart;
            eventToCreate.EventEnd = eventEnd;
            eventToCreate.TimeZoneId = timeZoneId;
            eventToCreate.IsRecurrent = true;
            eventToCreate.ApprovalWorkflowState = "Published";
            eventToCreate.UrlName = Regex.Replace(eventTitle.ToLower(), @"[^\w\-\!\$\'\(\)\=\@\d_]+", "-");

            var recurrenceBuilder = new RecurrenceRuleBuilder();
            eventToCreate.RecurrenceExpression = recurrenceBuilder.CreateDailyRecurrenceExpression(eventStart, TimeSpan.FromMinutes(minutesDuration), eventEnd, occurrencesCount, occurrenceIntervalInDays, timeZoneId);

            manager.Lifecycle.Publish(eventToCreate);
            manager.SaveChanges();

            return eventGuid;
        }
    }
}
