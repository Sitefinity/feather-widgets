using System;
using Telerik.Sitefinity.Events.Model;

namespace Telerik.Sitefinity.Frontend.Events.Mvc.Models.EventScheduler
{
    /// <summary>
    /// This is the view model used for displaying calendars.
    /// </summary>
    public class EventCalendarViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CalendarViewModel"/> class.
        /// </summary>
        /// <param name="calendar">The calendar.</param>
        public EventCalendarViewModel(Calendar calendar, System.Globalization.CultureInfo uiCulture) 
        {
            this.CalendarId = calendar.Id;
            this.Color = calendar.Color;
            this.Title = calendar.Title[uiCulture.Name];
        }

        /// <summary>
        /// Gets or sets the calendar identifier.
        /// </summary>
        /// <value>
        /// The calendar identifier.
        /// </value>
        public Guid CalendarId { get; set; }

        /// <summary>
        /// Gets or sets the color.
        /// </summary>
        /// <value>
        /// The color.
        /// </value>
        public string Color { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>
        /// The calendar title.
        /// </value>
        public string Title { get; set; }
    }
}