using ServiceStack.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using Telerik.Sitefinity.Frontend.Mvc.Models;
using Telerik.Sitefinity.GenericContent.Model;
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

            foreach (var listModel in viewModel.Items.Cast<ListViewModel>())
            {
                var listItemsViewModel = new ListItemsViewModel(listModel)
                {
                    SortExpression = this.SortExpression,
                    FilterExpression = this.FilterExpression,
                    SerializedAdditionalFilters = this.SerializedAdditionalFilters,
                    // We need only filter list items.
                    SelectionMode = SelectionMode.FilteredItems
                };
                var listItemViewModel = listItemsViewModel.CreateListViewModel(taxonFilter, page);

                listModel.ListItemsViewModel = listItemViewModel.Items;
            }

            return viewModel;
        }

        protected override ItemViewModel CreateItemViewModelInstance(IDataItem item)
        {
            return new ListViewModel(item);
        }

        protected override string CompileFilterExpression()
        {
            var elements = new List<string>();

            elements.Add(this.GetSelectedItemsFilterExpression());

            return string.Join(" AND ", elements.Select(el => "(" + el + ")"));
        }

        protected override IQueryable<IDataItem> UpdateExpression(IQueryable<IDataItem> query, int? skip, int? take, ref int? totalCount)
        {
            var compiledFilterExpression = this.CompileFilterExpression();

            query = this.SetExpression(
                         query,
                         compiledFilterExpression,
                         this.SortExpression,
                         skip,
                         take,
                         ref totalCount);

            return query;
        }

        private string GetSelectedItemsFilterExpression()
        {
            var selectedItems = JsonSerializer.DeserializeFromString<IList<string>>(this.SerializedSelectedItemsIds);

            var selectedItemGuids = selectedItems.Select(id => new Guid(id));
            var masterIds = this.GetItemsQuery()
                                .OfType<Content>()
                                .Where(c => selectedItemGuids.Contains(c.Id) || selectedItemGuids.Contains(c.OriginalContentId))
                                .Select(n => n.OriginalContentId != Guid.Empty ? n.OriginalContentId : n.Id)
                                .Distinct();

            var selectedItemConditions = masterIds.Select(id => "Id = {0} OR OriginalContentId = {0}".Arrange(id.ToString("D")));
            var selectedItemsFilterExpression = string.Join(" OR ", selectedItemConditions);

            return selectedItemsFilterExpression;
        }

        /// <inheritdoc />
        protected override IQueryable<IDataItem> GetItemsQuery()
        {
            var manager = (ListsManager)this.GetManager();

            return manager.GetLists();
        }
    }
}
