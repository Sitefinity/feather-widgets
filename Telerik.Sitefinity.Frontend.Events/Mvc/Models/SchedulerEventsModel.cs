using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using ServiceStack.Text;
using Telerik.Sitefinity.Data.Linq.Dynamic;
using Telerik.Sitefinity.Events.Model;
using Telerik.Sitefinity.Frontend.Mvc.Models;
using Telerik.Sitefinity.Modules;
using Telerik.Sitefinity.Modules.Events;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web.Model;

namespace Telerik.Sitefinity.Frontend.Events.Mvc.Models
{
    /// <summary>
    /// The model of the scheduler event widget.
    /// </summary>
    public class SchedulerEventsModel : EventModel
    {
        /// <summary>
        /// Gets or sets the start date.
        /// </summary>
        /// <returns>The start date</returns>
        public DateTime StartDate { get; set; }
        
        /// <summary>
        /// Gets or sets the end date.
        /// </summary>
        /// <returns>The end date</returns>
        public DateTime EndDate { get; set; }

        /// <summary>
        /// Gets or sets the calendar id list.
        /// </summary>
        /// <returns>The calendar list</returns>
        public Guid[] CalendarList { get; set; }

        /// <summary>
        /// Gets or sets the ui culture.
        /// </summary>
        /// <returns>The ui culture</returns>
        public CultureInfo UiCulture
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
        /// Gets the scheduler events.
        /// </summary>
        /// <returns>List view model</returns>
        public IList<SchedulerEventViewModel> GetSchedulerEvents()
        {
            var viewModel = this.CreateListViewModel(null, 1);
            var events = viewModel.Items.Select(i => i.DataItem as Event);

            if (this.CalendarList != null && this.CalendarList.Length > 0)
            {
                events = events.Where(p => p.Parent != null && this.CalendarList.Contains(p.Parent.Id));
            }

            var manager = (EventsManager)this.GetManager();

            // get event occurrences based on widget selected view
            var allOccurrences = manager.GetEventsOccurrences(events, this.StartDate, this.EndDate.AddDays(1)).AsQueryable<EventOccurrence>();

            // get filter expression used for events
            string filterExpression = this.CompileEventsOccurrencesFilterExpression();

            if (!string.IsNullOrWhiteSpace(filterExpression))
            {
                var eventEnd = this.GetPropertyInfo<Event>(e => e.EventEnd);
                var occurrenceEnd = this.GetPropertyInfo<EventOccurrence>(eo => eo.EndDate);
                var eventStart = this.GetPropertyInfo<Event>(e => e.EventStart);
                var occurrenceStart = this.GetPropertyInfo<EventOccurrence>(eo => eo.StartDate);

                // replace Event properties with EventOccurrence properties in filter expression
                filterExpression = filterExpression.Replace(eventEnd.Name, occurrenceEnd.Name).Replace(eventStart.Name, occurrenceStart.Name);

                allOccurrences = allOccurrences.Where(filterExpression);
            }
         
            var schedulerEvents = allOccurrences.Select(e => new SchedulerEventViewModel(e, this.UiCulture));
            return schedulerEvents.ToList();
        }

        /// <summary>
        /// Gets the calendars.
        /// </summary>
        /// <returns>List view model</returns>
        public IList<CalendarViewModel> GetCalendars()
        {
            var viewModel = this.CreateListViewModel(null, 1);
            var manager = (EventsManager)this.GetManager();
            var eventCalendarsIds = viewModel.Items.Select(i => i.DataItem as Event).Where(p => p != null)
                .GroupBy(p => p.Parent.Id).Select(p => p.Key);

            var eventCalendars = manager.GetCalendars().Where(p => eventCalendarsIds.Contains(p.Id));
            var calendars = eventCalendars.Select(e => new CalendarViewModel(e, this.UiCulture));

            return calendars.ToList();
        }

        /// <summary>
        /// Compiles a filter expression based on the widget settings.
        /// </summary>
        /// <returns>Filter expression that will be applied on the query.</returns>
        protected virtual string CompileEventsOccurrencesFilterExpression()
        {
            var elements = new List<string>();

            if (this.SelectionMode == SelectionMode.FilteredItems)
            {
                if (!this.SerializedAdditionalFilters.IsNullOrEmpty())
                {
                    var additionalFilters = JsonSerializer.DeserializeFromString<QueryData>(this.SerializedAdditionalFilters);
                    var currentFilter = additionalFilters.QueryItems.Where(p => !string.IsNullOrWhiteSpace(p.Name) && p.Name.Equals("Current", StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();
                    if (currentFilter == null)
                    {
                        if (additionalFilters.QueryItems != null && additionalFilters.QueryItems.Length > 0)
                        {
                            var queryExpression = Telerik.Sitefinity.Data.QueryBuilder.LinqTranslator.ToDynamicLinq(additionalFilters);
                            elements.Add(queryExpression);
                        }
                    }
                }
            }

            return string.Join(" AND ", elements.Select(el => "(" + el + ")"));
        }

        /// <summary>
        /// Adapts a filter expression in multilingual.
        /// </summary>
        /// <param name="filterExpression">The filter expression.</param>
        /// <returns>Multilingual filter expression.</returns>
        protected override string AdaptMultilingualFilterExpression(string filterExpression)
        {
            CultureInfo uiCulture;
            if (SystemManager.CurrentContext.AppSettings.Multilingual)
            {
                if (this.UiCulture == null)
                {
                    uiCulture = System.Globalization.CultureInfo.CurrentUICulture;
                }
                else
                {
                    uiCulture = this.UiCulture;
                }
            }
            else
            {
                uiCulture = null;
            }

            // the filter is adapted to the implementation of ILifecycleDataItemGeneric, so the culture is taken in advance when filtering published items.
            return ContentHelper.AdaptMultilingualFilterExpressionRaw(filterExpression, uiCulture);
        }

        /// <summary>
        /// Pull out a <see cref="PropertyInfo"/> object for the get method from an expression of the form
        /// x => x.SomeProperty
        /// </summary>
        /// <param name="expression">Expression describing the property for which the get method is to be extracted.</param>
        /// <returns>Corresponding <see cref="PropertyInfo"/>.</returns>
        private PropertyInfo GetPropertyInfo<T>(Expression<Func<T, object>> exp)
        {
            PropertyInfo info = null;
            MemberExpression body = exp.Body as MemberExpression;

            if (body == null)
            {
                UnaryExpression ubody = exp.Body as UnaryExpression;

                if (ubody != null)
                {
                    body = ubody.Operand as MemberExpression;
                }
            }

            if (body != null)
            {
                info = body.Member as PropertyInfo;
            }

            return info;
        }

        private CultureInfo uiCulture;
    }
}