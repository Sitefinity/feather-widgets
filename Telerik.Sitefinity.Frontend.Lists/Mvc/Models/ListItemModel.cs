using System;
using System.Linq;
using Telerik.Sitefinity.Frontend.Mvc.Models;
using Telerik.Sitefinity.Lists.Model;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.Lists;

namespace Telerik.Sitefinity.Frontend.Lists.Mvc.Models
{
    /// <summary>
    /// This class represents model for list item.
    /// </summary>
    public class ListItemModel : ContentModelBase
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
        /// Initializes a new instance of the <see cref="ListItemModel" /> class.
        /// </summary>
        /// <param name="parentList">The parent list.</param>
        public ListItemModel(ListViewModel parentList)
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
