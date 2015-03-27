using System;
using System.Linq;
using Telerik.Sitefinity.Frontend.Mvc.Models;
using Telerik.Sitefinity.Lists.Model;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.Lists;

namespace Telerik.Sitefinity.Frontend.Lists.Mvc.Models
{
    public class ListItemsViewModel : ContentModelBase
    {
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

        public ListItemsViewModel()
        {

        }

        protected override IQueryable<IDataItem> GetItemsQuery()
        {
            var manager = (ListsManager)this.GetManager();

            return manager.GetListItems();
        }
    }
}
