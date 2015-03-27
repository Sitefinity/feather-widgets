using System;
using System.Linq;
using Telerik.Sitefinity.Frontend.Mvc.Models;
using Telerik.Sitefinity.Lists.Model;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.Lists;

namespace Telerik.Sitefinity.Frontend.Lists.Mvc.Models
{
    /// <summary>
    /// A model for the list items MVC widget.
    /// </summary>
    public class ListItemsViewModel : ContentModelBase
    {
        private readonly ListViewModel parentList;

        /// <inheritdoc />
        public override Type ContentType
        {
            get
            {
                return typeof(ListItem);
            }
            set
            {
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ListItemsViewModel" /> class.
        /// </summary>
        /// <param name="parentList">The parent list.</param>
        public ListItemsViewModel(ListViewModel parentList)
        {
            if (parentList == null)
            {
                throw new ArgumentException("parentList == null");
            }

            this.parentList = parentList;
        }

        /// <inheritdoc />
        protected override IQueryable<IDataItem> GetItemsQuery()
        {
            var manager = (ListsManager)this.GetManager();

            return manager.GetListItems().Where(l => l.Parent.Id == parentList.DataItem.Id);
        }
    }
}
