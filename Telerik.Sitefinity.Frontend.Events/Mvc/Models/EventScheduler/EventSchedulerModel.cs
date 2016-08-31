using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Newtonsoft.Json;
using ServiceStack.Text;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Data.Linq.Dynamic;
using Telerik.Sitefinity.Events.Model;
using Telerik.Sitefinity.Frontend.Mvc.Models;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules;
using Telerik.Sitefinity.Modules.Events;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Taxonomies.Model;
using Telerik.Sitefinity.Web.Model;

namespace Telerik.Sitefinity.Frontend.Events.Mvc.Models.EventScheduler
{
    /// <summary>
    /// The model of the scheduler event widget.
    /// </summary>
    public class EventSchedulerModel : ContentModelBase, IEventSchedulerModel
    {
        #region Properties

        /// <summary>
        /// Gets or sets the start date.
        /// </summary>
        /// <returns>The start date</returns>
        [JsonIgnore]
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
        [JsonIgnore]
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
        [JsonIgnore]
        public virtual Guid[] CalendarList { get; set; }

        /// <summary>
        /// Gets or sets the ui culture.
        /// </summary>
        /// <returns>The ui culture</returns>
        [JsonIgnore]
        public virtual CultureInfo UiCulture
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
        /// Gets or sets the type of the content.
        /// </summary>
        /// <value>
        /// The type of the content.
        /// </value>
        public override Type ContentType
        {
            get
            {
                return typeof(Event);
            }

            set
            {
            }
        }

        /// <summary>
        /// Gets or sets the sort expression.
        /// </summary>
        /// <value>
        /// The sort expression.
        /// </value>
        public override string SortExpression
        {
            get
            {
                return this.sortExpression;
            }

            set
            {
                this.sortExpression = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether to divide items in the list.
        /// </summary>
        /// <value>
        /// The display mode.
        /// </value>
        public override ListDisplayMode DisplayMode
        {
            get
            {
                return this.listDisplayMode;
            }

            set
            {
                this.listDisplayMode = value;
            }
        }

        /// <summary>
        /// Gets or sets the selected mode for Narrow selection. That is the filtering by taxonomies and calendars.
        /// </summary>
        /// <value>
        /// The narrow selection mode.
        /// </value>
        public virtual SelectionMode NarrowSelectionMode { get; set; }

        /// <summary>
        /// Gets or sets the selected mode for scheduler view.
        /// </summary>
        /// <value>
        /// The selection mode.
        /// </value>
        public virtual EventSchedulerViewMode EventSchedulerViewMode { get; set; }

        /// <summary>
        /// Gets or sets the week start day for scheduler view.
        /// </summary>
        /// <value>
        /// The week start day.
        /// </value>
        public virtual WeekStartDay WeekStartDay { get; set; }

        /// <summary>
        /// Gets or sets the serialized narrow selection filters. Contains information about taxonomies and calendar filters as serialized QueryData.
        /// </summary>
        /// <value>
        /// The serialized narrow selection filters.
        /// </value>
        public virtual string SerializedNarrowSelectionFilters { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the widget allows calendar export.
        /// </summary>
        /// <value>
        ///   <c>true</c> if [allow calendar export]; otherwise, <c>false</c>.
        /// </value>
        public virtual bool AllowCalendarExport
        {
            get { return this.allowCalendarExport; }
            set { this.allowCalendarExport = value; }
        }

        /// <summary>
        /// Gets or sets the allow change for scheduler view.
        /// </summary>
        /// <value>
        ///   <c>true</c> if [allow change view]; otherwise, <c>false</c>.
        /// </value>
        public virtual bool AllowChangeCalendarView
        {
            get { return this.allowChangeCalendarView; }
            set { this.allowChangeCalendarView = value; }
        }

        /// <summary>
        /// Gets or sets the allow event calendar filter.
        /// </summary>
        /// <value>
        ///   <c>true</c> if [allow event calendar filter]; otherwise, <c>false</c>.
        /// </value>
        public virtual bool AllowCalendarFilter 
        {
            get { return this.allowCalendarFilter; }
            set { this.allowCalendarFilter = value; }
        }

        #endregion Properties

        /// <summary>
        /// Gets the scheduler events.
        /// </summary>
        /// <returns>List view model</returns>
        public virtual IList<EventOccurrenceViewModel> GetSchedulerEvents()
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

            var eventOccurrences = allOccurrences.Select(e => new EventOccurrenceViewModel(e, this.UiCulture));
            return eventOccurrences.ToList();
        }

        /// <summary>
        /// Gets the calendars.
        /// </summary>
        /// <returns>List view model</returns>
        public virtual IList<EventCalendarViewModel> GetCalendars()
        {
            var viewModel = this.CreateListViewModel(null, 1);
            var manager = (EventsManager)this.GetManager();
            var eventCalendarsIds = viewModel.Items.Select(i => i.DataItem as Event).Where(p => p != null)
                .GroupBy(p => p.Parent.Id).Select(p => p.Key);

            var eventCalendars = manager.GetCalendars().Where(p => eventCalendarsIds.Contains(p.Id));
            var calendars = eventCalendars.Select(e => new EventCalendarViewModel(e, this.UiCulture)).OrderBy(p => p.Title, StringComparer.Create(this.UiCulture, true));

            return calendars.ToList();
        }

        /// <summary>
        /// Gets a manager instance for the model.
        /// </summary>
        /// <returns>The manager.</returns>
        protected override IManager GetManager()
        {
            return EventsManager.GetManager(this.ProviderName);
        }

        /// <summary>
        /// Gets an active query for all items.
        /// </summary>
        /// <returns>The query.</returns>
        protected override IQueryable<IDataItem> GetItemsQuery()
        {
            var manager = (EventsManager)this.GetManager();
            return manager.GetEvents();
        }

        /// <summary>
        /// Compiles a filter expression based on the widget settings.
        /// </summary>
        /// <returns>Filter expression that will be applied on the query.</returns>
        protected override string CompileFilterExpression()
        {
            var filterExpression = base.CompileFilterExpression();

            filterExpression = filterExpression.Replace("EventEnd>=(DateTime.UtcNow)", "( (EventEnd>=(DateTime.UtcNow)) OR  (NULL==EventEnd))");

            if (this.NarrowSelectionMode == SelectionMode.FilteredItems)
            {
                if (!this.SerializedNarrowSelectionFilters.IsNullOrEmpty())
                {
                    var narrowFilters = ServiceStack.Text.JsonSerializer.DeserializeFromString<QueryData>(this.SerializedNarrowSelectionFilters);
                    if (narrowFilters.QueryItems != null && narrowFilters.QueryItems.Length > 0)
                    {
                        var queryExpression = Telerik.Sitefinity.Data.QueryBuilder.LinqTranslator.ToDynamicLinq(narrowFilters);

                        if (!filterExpression.IsNullOrEmpty())
                            filterExpression += " AND (" + queryExpression + ")";
                        else
                            filterExpression = "(" + queryExpression + ")";
                    }
                }
            }

            return filterExpression;
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
                    var additionalFilters = ServiceStack.Text.JsonSerializer.DeserializeFromString<QueryData>(this.SerializedAdditionalFilters);
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

        private DateTime start;
        private DateTime end;
        private CultureInfo uiCulture;
        private const string DefaultSortExpression = "EventStart ASC";
        private string sortExpression = DefaultSortExpression;
        private ListDisplayMode listDisplayMode = ListDisplayMode.All;
        private bool allowCalendarExport = true;
        private bool allowChangeCalendarView = true;
        private bool allowCalendarFilter = true;
    }
}