using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.Sitefinity.Frontend.Mvc.Models;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.Lists;

namespace Telerik.Sitefinity.Frontend.Lists.Mvc.Models
{
    public class ListsModel : ContentModelBase, IListsModel
    {
        /// <inheritdoc />
        protected override IQueryable<IDataItem> GetItemsQuery()
        {
            var manager = (ListsManager)this.GetManager();

            return manager.GetListItems();
        }
    }
}
