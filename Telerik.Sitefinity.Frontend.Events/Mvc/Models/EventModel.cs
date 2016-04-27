using System;
using System.Linq;
using ServiceStack.Text;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Events.Model;
using Telerik.Sitefinity.Frontend.Mvc.Models;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.Events;
using Telerik.Sitefinity.Taxonomies.Model;
using Telerik.Sitefinity.Web.Model;

namespace Telerik.Sitefinity.Frontend.Events.Mvc.Models
{
    /// <summary>
    /// Provides API for working with <see cref="Telerik.Sitefinity.Events.Model.Event"/> items.
    /// </summary>
    public class EventModel : ContentModelBase, IEventModel
    {
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
        /// Gets or sets the selected mode for Narrow selection. That is the filtering by taxonomies and calendars.
        /// </summary>
        /// <value>
        /// The narrow selection mode.
        /// </value>
        public virtual SelectionMode NarrowSelectionMode { get; set; }

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
        public bool AllowCalendarExport
        {
            get { return this.allowCalendarExport; }
            set { this.allowCalendarExport = value; }
        }

        /// <summary>
        /// Creates a view model for use in list views.
        /// </summary>
        /// <param name="taxonFilter">The taxon filter.</param>
        /// <param name="page">The page.</param>
        /// <returns>
        /// A view model for use in list views.
        /// </returns>
        /// <exception cref="System.ArgumentException">'page' argument has to be at least 1.;page</exception>
        public ContentListViewModel CreateListViewModel(ITaxon taxonFilter, int page)
        {
            return base.CreateListViewModel(taxonFilter, page);
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
                    var narrowFilters = JsonSerializer.DeserializeFromString<QueryData>(this.SerializedNarrowSelectionFilters);
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

        private const string DefaultSortExpression = "EventStart ASC";

        private string sortExpression = DefaultSortExpression;
        private bool allowCalendarExport = true;
    }
}
