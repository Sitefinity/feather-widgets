using System;
using System.Globalization;

namespace Telerik.Sitefinity.Frontend.Events.Mvc.Models.EventScheduler
{
    /// <summary>
    /// This interface defines API for working with scheduler event widget for filtering.
    /// </summary>
    public interface IEventsFilter
    {
        /// <summary>
        /// Gets or sets the widget id.
        /// </summary>
        Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the ui culture.
        /// </summary>
        /// <returns>The ui culture</returns>
        CultureInfo UICulture { get; set; }

        /// <summary>
        /// Gets or sets the start date.
        /// </summary>
        /// <returns>The start date</returns>
        DateTime StartDate { get; set; }

        /// <summary>
        /// Gets or sets the end date.
        /// </summary>
        /// <returns>The end date</returns>
        DateTime EndDate { get; set; }

        /// <summary>
        /// Gets or sets the calendar id list.
        /// </summary>
        /// <returns>The calendar list</returns>
        Guid[] CalendarList { get; set; }

        /// <summary>
        /// Gets or sets the selected mode for scheduler view.
        /// </summary>
        /// <value>
        /// The selection mode.
        /// </value>
        EventSchedulerViewMode EventSchedulerViewMode { get; set; }
    }
}