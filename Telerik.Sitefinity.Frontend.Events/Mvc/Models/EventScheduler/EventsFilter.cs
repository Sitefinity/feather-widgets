using System;
using System.Globalization;

namespace Telerik.Sitefinity.Frontend.Events.Mvc.Models.EventScheduler
{
    /// <summary>
    /// The model of the scheduler event widget for filtering.
    /// </summary>
    public class EventsFilter : IEventsFilter
    {
        /// <summary>
        /// Gets or sets the widget id.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the ui culture.
        /// </summary>
        /// <returns>The ui culture</returns>
        public virtual CultureInfo UICulture
        {
            get
            {
                if (this.uiCulture == null)
                {
                    this.uiCulture = CultureInfo.CurrentUICulture;
                }

                return this.uiCulture;
            }

            set
            {
                this.uiCulture = value;
            }
        }

        /// <summary>
        /// Gets or sets the current page identifier.
        /// </summary>
        /// <value>
        /// The current page identifier.
        /// </value>
        public Guid CurrentPageId { get; set; }

        /// <summary>
        /// Gets or sets the start date.
        /// </summary>
        /// <returns>The start date</returns>
        public virtual DateTime StartDate
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
        /// Gets or sets the end date.
        /// </summary>
        /// <returns>The end date</returns>
        public virtual DateTime EndDate
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
        /// Gets or sets the calendar id list.
        /// </summary>
        /// <returns>The calendar list</returns>
        public virtual Guid[] CalendarList { get; set; }

        /// <summary>
        /// Gets or sets the selected mode for scheduler view.
        /// </summary>
        /// <value>
        /// The selection mode.
        /// </value>
        public virtual EventSchedulerViewMode EventSchedulerViewMode { get; set; }

        private DateTime start;
        private DateTime end;
        private CultureInfo uiCulture;
    }
}