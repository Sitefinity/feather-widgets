using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Events.Model;
using Telerik.Sitefinity.Frontend.Mvc.Models;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.Events;

namespace Telerik.Sitefinity.Frontend.Events.Mvc.Models
{
    /// <summary>
    /// Provides API for working with <see cref="Telerik.Sitefinity.Events.Model.Event"/> items.
    /// </summary>
    public class EventModel : ContentModelBase, IEventModel
    {
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
        public override string SortExpression { get; set; }

        /// <summary>
        /// Creates a view model for use in list views.
        /// </summary>
        /// <param name="page">The page.</param>
        /// <returns>A view model for use in list views.</returns>
        /// <exception cref="System.ArgumentException">'page' argument has to be at least 1.;page</exception>
        public ContentListViewModel CreateListViewModel(int page)
        {
            return base.CreateListViewModel(null, page);
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
    }
}
