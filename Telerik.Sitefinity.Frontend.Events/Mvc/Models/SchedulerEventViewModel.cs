using System;
using System.Globalization;
using Telerik.Sitefinity.Modules.Events;

namespace Telerik.Sitefinity.Frontend.Events.Mvc.Models
{
    /// <summary>
    /// This is the view model required by Kendo scheduler.
    /// </summary>
    public class SchedulerEventViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SchedulerEventViewModel"/> class.
        /// </summary>
        /// <param name="item">The item.</param>
        public SchedulerEventViewModel(EventOccurrence item, CultureInfo uiCulture)
        {
            this.Id = item.Event.Id;
            this.Title = item.Event.Title[uiCulture.Name];
            this.Description = item.Event.Description[uiCulture.Name];
            this.Start = item.Event.AllDayEvent ? item.StartDate.Add(new TimeSpan(item.StartDate.Ticks - item.StartDate.ToSitefinityUITime().Ticks)) : item.StartDate;
            this.End = item.EndDate.HasValue ? item.Event.AllDayEvent ? item.EndDate.Value.Add(new TimeSpan(item.EndDate.Value.Ticks - item.EndDate.Value.ToSitefinityUITime().Ticks)).AddDays(-1) : item.EndDate.Value : DateTime.MaxValue;
            this.RecurrenceID = item.IsRecurrent ? item.Event.Id : (Guid?)null;
            this.IsAllDay = item.Event.AllDayEvent;
            this.CalendarId = item.Event.ParentId;
            this.EventUrl = item.Event.ItemDefaultUrl[uiCulture.Name];
            this.City = item.Event.City[uiCulture.Name];
            this.Country = item.Event.Country[uiCulture.Name];
        }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>
        /// The title.
        /// </value>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the start timezone.
        /// </summary>
        /// <value>
        /// The start timezone.
        /// </value>
        public string StartTimezone { get; set; }

        /// <summary>
        /// Gets or sets the start.
        /// </summary>
        /// <value>
        /// The start.
        /// </value>
        public DateTime Start
        {
            get
            {
                return this.start;
            }

            set
            {
                this.start = value.ToUniversalTime();
            }
        }

        /// <summary>
        /// Gets or sets the end.
        /// </summary>
        /// <value>
        /// The end.
        /// </value>
        public DateTime End
        {
            get
            {
                return this.end;
            }

            set
            {
                this.end = value.ToUniversalTime();
            }
        }

        /// <summary>
        /// Gets or sets the end timezone.
        /// </summary>
        /// <value>
        /// The end timezone.
        /// </value>
        public string EndTimezone { get; set; }

        /// <summary>
        /// Gets or sets the recurrence rule.
        /// </summary>
        /// <value>
        /// The recurrence rule.
        /// </value>
        public string RecurrenceRule { get; set; }

        /// <summary>
        /// Gets or sets the recurrence identifier.
        /// </summary>
        /// <value>
        /// The recurrence identifier.
        /// </value>
        public Guid? RecurrenceID { get; set; }

        /// <summary>
        /// Gets or sets the recurrence exception.
        /// </summary>
        /// <value>
        /// The recurrence exception.
        /// </value>
        public string RecurrenceException { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [is all day].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [is all day]; otherwise, <c>false</c>.
        /// </value>
        public bool IsAllDay { get; set; }

        /// <summary>
        /// Gets or sets the calendar identifier.
        /// </summary>
        /// <value>
        /// The calendar identifier.
        /// </value>
        public Guid CalendarId { get; set; }

        /// <summary>
        /// Gets or sets the EventUrl.
        /// </summary>
        /// <value>
        /// The event url.
        /// </value>
        public string EventUrl { get; set; }

        /// <summary>
        /// Gets or sets the city of the event.
        /// </summary>
        /// <value>
        /// The event city.
        /// </value>
        public string City { get; set; }

        /// <summary>
        //  Gets or sets the country of the event.
        /// </summary>
        /// <value>
        /// The event country.
        /// </value>
        public string Country { get; set; }

        private DateTime start;
        private DateTime end;
    }
}