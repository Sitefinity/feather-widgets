﻿using ServiceStack.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using Telerik.Sitefinity.ContentLocations;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Frontend.Mvc.Models;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Lists.Model;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.Lists;
using Telerik.Sitefinity.Taxonomies.Model;

namespace Telerik.Sitefinity.Frontend.Lists.Mvc.Models
{
    /// <summary>
    /// This class is used as a model for Lists widget.
    /// </summary>
    public class ListsModel : ContentModelBase, IListsModel
    {
        public ListsModel()
        {
            this.SortExpression = "Ordinal ASC";
        }

        /// <inheritdoc />
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
        public override string SerializedSelectedItemsIds
        {
            get
            {
                return this.serializedSelectedItemsIds;
            }
            set
            {
                if (this.serializedSelectedItemsIds != value)
                {
                    this.serializedSelectedItemsIds = value;
                    if (!this.serializedSelectedItemsIds.IsNullOrEmpty())
                    {
                        this.selectedItemsIds = JsonSerializer.DeserializeFromString<IList<string>>(this.serializedSelectedItemsIds);
                    }
                }
            }
        }

        /// <inheritdoc />
        public bool IsEmpty
        {
            get
            {
                return this.selectedItemsIds.Count == 0;
            }
        }

        /// <inheritdoc />
        public override IEnumerable<ContentLocations.IContentLocationInfo> GetLocations()
        {
            var location = new ContentLocationInfo();

            location.ContentType = typeof(ListItem);
            location.ProviderName = this.ProviderName;

            var listsFilterExpression = this.CompileFilterExpression(ListItemFilterExpression);
            if (!string.IsNullOrEmpty(listsFilterExpression))
            {
                location.Filters.Add(new BasicContentLocationFilter(listsFilterExpression));
            }

            var listItemModel = new ListItemModel()
            {
                FilterExpression = this.FilterExpression,
                SerializedAdditionalFilters = this.SerializedAdditionalFilters,
                // We need only filter list items.
                SelectionMode = SelectionMode.FilteredItems
            };

            var listItemsFilterExpression = listItemModel.GetFilterExpression();

            if (!string.IsNullOrEmpty(listItemsFilterExpression))
            {
                location.Filters.Add(new BasicContentLocationFilter(listItemsFilterExpression));
            }

            return new[] { location };
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
                var listItemModel = new ListItemModel(listModel)
                {
                    SortExpression = this.SortExpression,
                    FilterExpression = this.FilterExpression,
                    SerializedAdditionalFilters = this.SerializedAdditionalFilters,
                    // We need only filter list items.
                    SelectionMode = SelectionMode.FilteredItems,
                    ProviderName = this.ProviderName,
                    ItemsPerPage = null
                };

                listModel.ListItemViewModel = listItemModel.CreateListViewModel(taxonFilter, page);
            }

            return viewModel;
        }

        /// <inheritdoc />
        public override ContentDetailsViewModel CreateDetailsViewModel(IDataItem item)
        {
            var viewModel =  base.CreateDetailsViewModel(item);

            var listItemModel = new ListItemModel((ListViewModel)viewModel.Item);

            ((ListViewModel)(viewModel.Item)).ListItemViewModel = listItemModel.CreateListViewModel(null, 1);

            return viewModel;
        }

        /// <inheritdoc />
        protected override ItemViewModel CreateItemViewModelInstance(IDataItem item)
        {
            return new ListViewModel(item);
        }

        /// <inheritdoc />
        protected override string CompileFilterExpression()
        {
            return this.CompileFilterExpression(ListFilterExpression);
        }

        /// <summary>
        /// Compiles a filter expression based on the widget settings.
        /// </summary>
        /// <returns>Filter expression that will be applied on the query.</returns>
        protected string CompileFilterExpression(string filterExpression)
        {
            var elements = new List<string>();

            string filter = this.GetSelectedItemsFilterExpression(filterExpression);

            if (string.IsNullOrWhiteSpace(filter))
            {
                return string.Empty;
            }

            elements.Add(filter);

            return string.Join(" AND ", elements.Select(el => "(" + el + ")"));
        }

        /// <inheritdoc />
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

        private string GetSelectedItemsFilterExpression(string filterExpression)
        {
            var selectedItemGuids = selectedItemsIds.Select(id => new Guid(id));
            var masterIds = this.GetItemsQuery()
                                .OfType<Content>()
                                .Where(c => selectedItemGuids.Contains(c.Id) || selectedItemGuids.Contains(c.OriginalContentId))
                                .Select(n => n.OriginalContentId != Guid.Empty ? n.OriginalContentId : n.Id)
                                .Distinct();

            var selectedItemConditions = masterIds.Select(id => filterExpression.Arrange(id.ToString("D")));
            var selectedItemsFilterExpression = string.Join(" OR ", selectedItemConditions);

            return selectedItemsFilterExpression;
        }

        /// <inheritdoc />
        protected override IQueryable<IDataItem> GetItemsQuery()
        {
            var manager = (ListsManager)this.GetManager();

            return manager.GetLists();
        }

        /// <inheritdoc />
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Expr"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "5#")]
        protected override IQueryable<TItem> SetExpression<TItem>(IQueryable<TItem> query, string filterExpression, string sortExpr, int? itemsToSkip, int? itemsToTake, ref int? totalCount)
        {
            try
            {
                query = DataProviderBase.SetExpressions(
                                                  query,
                                                  filterExpression,
                                                  sortExpr,
                                                  itemsToSkip,
                                                  itemsToTake,
                                                  ref totalCount);
            }
            catch (MemberAccessException)
            {
                query = DataProviderBase.SetExpressions(
                                                  query,
                                                  filterExpression,
                                                  ListsModel.DefaultSortExpression,
                                                  itemsToSkip,
                                                  itemsToTake,
                                                  ref totalCount);
            }

            return query;
        }

        #region Private fields and constants

        private const string DefaultSortExpression = "PublicationDate DESC";

        private IList<string> selectedItemsIds = new List<string>();
        private string serializedSelectedItemsIds;

        private const string ListFilterExpression = "Id = {0} OR OriginalContentId = {0}";
        private const string ListItemFilterExpression = "Parent.Id = {0} OR Parent.OriginalContentId = {0}";

        #endregion
    }
}
