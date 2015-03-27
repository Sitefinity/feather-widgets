using System;
using System.Collections.Generic;
using System.Linq;
using Telerik.Sitefinity.Frontend.Mvc.Models;
using Telerik.Sitefinity.Model;

namespace Telerik.Sitefinity.Frontend.Lists.Mvc.Models
{
    public class ListViewModel : ItemViewModel
    {
        public ListViewModel(IDataItem listItem)
            : base(listItem)
        {
        }

        /// <summary>
        /// Gets or sets the list items view model.
        /// </summary>
        /// <value>The list items.</value>
        public IEnumerable<ItemViewModel> ListItemsViewModel { get; set; }
    }
}
