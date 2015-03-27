using System;
using System.Collections.Generic;
using System.Linq;
using Telerik.Sitefinity.Frontend.Mvc.Models;
using Telerik.Sitefinity.Lists.Model;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.Lists;
using Telerik.Sitefinity.Taxonomies.Model;

namespace Telerik.Sitefinity.Frontend.Lists.Mvc.Models
{
    /// <summary>
    /// A model for the lists MVC widget.
    /// </summary>
    public class ListsModel : ContentModelBase, IListsModel
    {
        public override Type ContentType
        {
            get
            {
                return typeof(List);
            }
            set
            {

            }
        }

        /// <inheritdoc />
        public override ContentListViewModel CreateListViewModel(ITaxon taxonFilter, int page)
        {
            if (page < 1)
                throw new ArgumentException("'page' argument has to be at least 1.", "page");

            var query = this.GetItemsQuery();
            if (query == null)
                return this.CreateListViewModelInstance();

            var viewModel = this.CreateListViewModelInstance();
            this.PopulateListViewModel(page, query, viewModel);

            var listItemsViewModel = new ListItemsViewModel { SortExpression = this.SortExpression };
            var listItemViewModel = listItemsViewModel.CreateListViewModel(taxonFilter, page);

            foreach (var listModel in viewModel.Items.Cast<ListViewModel>())
            {
                listModel.ListItemsViewModel = listItemViewModel.Items.Where(l => ((ListItem)l.DataItem).Parent.Id == listModel.DataItem.Id);
            }

            return viewModel;
        }

        protected override ItemViewModel CreateItemViewModelInstance(IDataItem item)
        {
            return new ListViewModel(item);
        }

        /// <inheritdoc />
        protected override IEnumerable<ItemViewModel> ApplyListSettings(int page, IQueryable<IDataItem> query, out int? totalPages)
        {
            totalPages = 0;

            IList<ItemViewModel> result = new List<ItemViewModel>();

            var queryResult = query.ToArray<IDataItem>();

            foreach (var item in queryResult)
            {
                result.Add(this.CreateItemViewModelInstance(item));
            }

            return result;
        }

        /// <inheritdoc />
        protected override IQueryable<IDataItem> GetItemsQuery()
        {
            var manager = (ListsManager)this.GetManager();

            return manager.GetLists();
        }
    }
}
