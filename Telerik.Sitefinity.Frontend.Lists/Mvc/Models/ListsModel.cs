using System;
using System.Collections.Generic;
using System.Linq;
using ServiceStack.Text;
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
            if (string.IsNullOrEmpty(location.ProviderName))
            {
                location.ProviderName = this.GetManager().Provider.Name;
            }

            string listsFilterExpression;
            switch (this.ContentViewDisplayMode)
            {
                case Telerik.Sitefinity.Web.UI.ContentUI.Enums.ContentViewDisplayMode.Detail:
                    location.Filters.Add(this.CompileSingleItemFilterExpression(location.ContentType));

                    return new[] { location };
                case Telerik.Sitefinity.Web.UI.ContentUI.Enums.ContentViewDisplayMode.Automatic:
                    listsFilterExpression = this.CompileFilterExpression(ListItemFilterExpression);
                    break;
                default:
                    return null;
            }
            
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

        /// <summary>
        /// Populates the list ViewModel.
        /// </summary>
        /// <param name="page">The current page.</param>
        /// <param name="query">The query.</param>
        /// <param name="viewModel">The view model.</param>
        protected override void PopulateListViewModel(int page, IQueryable<IDataItem> query, ContentListViewModel viewModel)
        {
            if (viewModel == null)
            {
                throw new ArgumentNullException("Paremter viewModel cannot be null.");
            }

            int? totalPages = null;
            if (string.IsNullOrEmpty(this.GetSelectedItemsFilterExpression()))
            {
                viewModel.Items = Enumerable.Empty<ItemViewModel>();
            }
            else
            {
                viewModel.Items = this.ApplyListSettings(page, query, out totalPages);
            }

            this.SetViewModelProperties(viewModel, page, totalPages);
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
            viewModel.UrlKeyPrefix = this.UrlKeyPrefix;
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
                    SelectionGroupLogicalOperator = this.SelectionGroupLogicalOperator,
                    ProviderName = this.ProviderName,
                    ItemsPerPage = null,

                    UrlKeyPrefix = this.UrlKeyPrefix
                };

                listModel.ListItemViewModel = listItemModel.CreateListViewModel(taxonFilter, page);
            }

            return viewModel;
        }

        /// <inheritdoc />
        public override ContentDetailsViewModel CreateDetailsViewModel(IDataItem item)
        {
            var viewModel = base.CreateDetailsViewModel(item);

            var listItemModel = new ListItemModel((ListViewModel)viewModel.Item);

            ((ListViewModel)viewModel.Item).ListItemViewModel = listItemModel.CreateListViewModel(null, 1);

            return viewModel;
        }

        /// <summary>
        /// Gets a collection of <see cref="CacheDependencyNotifiedObject"/>.
        ///     The <see cref="CacheDependencyNotifiedObject"/> represents a key for which cached items could be subscribed for
        ///     notification.
        ///     When notified, all cached objects with dependency on the provided keys will expire.
        /// </summary>
        /// <param name="viewModel">View model that will be used for displaying the data.</param>
        /// <returns>
        /// The <see cref="IList"/>.
        /// </returns>
        public override IList<CacheDependencyKey> GetKeysOfDependentObjects(ContentListViewModel viewModel)
        {
            var result = base.GetKeysOfDependentObjects(viewModel);
            var manager = this.GetManager();
            string applicationName = manager != null && manager.Provider != null ? manager.Provider.ApplicationName : string.Empty;

            result.Add(new CacheDependencyKey { Key = string.Concat(ContentLifecycleStatus.Live.ToString(), applicationName), Type = typeof(ListItem) });

            return result;
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
            var selectedItemGuids = this.selectedItemsIds.Select(id => new Guid(id));
            var masterIds = this.GetItemsQuery()
                                .OfType<Content>()
                                .Where(c => selectedItemGuids.Contains(c.Id) || selectedItemGuids.Contains(c.OriginalContentId))
                                .Select(n => n.OriginalContentId != Guid.Empty ? n.OriginalContentId : n.Id)
                                .Distinct();

            var selectedItemConditions = masterIds.Select(id => filterExpression.Arrange(id.ToString("D")));
            var selectedItemsFilterExpression = string.Join(" OR ", selectedItemConditions);

            return selectedItemsFilterExpression;
        }

        protected override string GetSelectedItemsFilterExpression()
        {
            return this.GetSelectedItemsFilterExpression(ListItemFilterExpression);
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
