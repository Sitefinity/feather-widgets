using ServiceStack.Text;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.DynamicModules;
using Telerik.Sitefinity.Frontend.Mvc.Models;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Taxonomies.Model;
using Telerik.Sitefinity.Web.Model;

namespace DynamicContent.Mvc.Model
{
    /// <summary>
    /// This class represents model used for DynamicContent widget.
    /// </summary>
    public class DynamicContentModel : IDynamicContentModel
    {
        #region Properties

        /// <inheritdoc />
        public string ContentType { get; set; }

        /// <inheritdoc />
        public IList<IDataItem> Items
        {
            get
            {
                return this.items;
            }

            private set
            {
                this.items = value;
            }
        }

        /// <inheritdoc />
        public string ListCssClass
        {
            get;
            set;
        }

        /// <inheritdoc />
        public string DetailCssClass
        {
            get;
            set;
        }

        /// <inheritdoc />
        public Guid SelectedItemId
        {
            get;
            set;
        }

        /// <inheritdoc />
        public dynamic DetailItem
        {
            get;
            set;
        }

        /// <inheritdoc />
        public bool EnableSocialSharing
        {
            get;
            set;
        }

        /// <inheritdoc />
        public string ProviderName
        {
            get;
            set;
        }

        /// <inheritdoc />
        public SelectionMode SelectionMode
        {
            get;
            set;
        }

        /// <inheritdoc />
        public ListDisplayMode DisplayMode
        {
            get;
            set;
        }

        /// <inheritdoc />
        public int? TotalPagesCount
        {
            get;
            set;
        }

        /// <inheritdoc />
        public int CurrentPage
        {
            get;
            set;
        }

        /// <inheritdoc />
        public int? ItemsPerPage
        {
            get
            {
                return this.itemsPerPage;
            }

            set
            {
                this.itemsPerPage = value;
            }
        }

        /// <inheritdoc />
        public string SortExpression
        {
            get
            {
                return this.sortExpression;
            }

            set
            {
                this.sortExpression = value;
            }
        }

        /// <inheritdoc />
        public string FilterExpression
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the query data used for filtering of the news items.
        /// </summary>
        /// <value>
        /// The additional filters.
        /// </value>
        [Browsable(false)]
        public QueryData AdditionalFilters
        {
            get;
            set;
        }

        /// <inheritdoc />
        public string SerializedAdditionalFilters
        {
            get
            {
                return this.serializedAdditionalFilters;
            }

            set
            {
                if (this.serializedAdditionalFilters != value)
                {
                    this.serializedAdditionalFilters = value;
                    if (!this.serializedAdditionalFilters.IsNullOrEmpty())
                    {
                        this.AdditionalFilters = JsonSerializer.DeserializeFromString<QueryData>(this.serializedAdditionalFilters);
                    }
                }
            }
        }

        #endregion

        #region Public methods

        /// <inheritdoc />
        public virtual void PopulateItems(ITaxon taxonFilter, string taxonField, int? page)
        {
            if (this.ContentType.IsNullOrEmpty())
                return;

            this.InitializeManager();

            if (this.manager == null)
                return;

            var items = this.manager.GetDataItems(Telerik.Sitefinity.Utilities.TypeConverters.TypeResolutionService.ResolveType(this.ContentType));

            if (taxonFilter != null && !taxonField.IsNullOrEmpty())
                items = items.Where(n => n.GetValue<IList<Guid>>(taxonField).Contains(taxonFilter.Id));

            this.ApplyListSettings(page, ref items);

            this.Items = items.ToArray();
        }

        /// <inheritdoc />
        public virtual string CompileFilterExpression()
        {
            var elements = new List<string>();

            if (this.SelectionMode == SelectionMode.FilteredItems)
            {
                if (this.AdditionalFilters != null)
                {
                    var queryExpression = Telerik.Sitefinity.Data.QueryBuilder.LinqTranslator.ToDynamicLinq(this.AdditionalFilters);
                    elements.Add(queryExpression);
                }
            }
            else if (this.SelectionMode == SelectionMode.SelectedItems)
            {
                var selectedItemsFilterExpression = this.GetSelectedItemsFilterExpression();
                if (!selectedItemsFilterExpression.IsNullOrEmpty())
                {
                    elements.Add(selectedItemsFilterExpression);
                }
            }

            if (!this.FilterExpression.IsNullOrEmpty())
            {
                elements.Add(this.FilterExpression);
            }

            return string.Join(" AND ", elements.Select(el => "(" + el + ")"));
        }

        /// <summary>
        /// Gets a collection of <see cref="CacheDependencyNotifiedObject"/>.
        ///     The <see cref="CacheDependencyNotifiedObject"/> represents a key for which cached items could be subscribed for
        ///     notification.
        ///     When notified, all cached objects with dependency on the provided keys will expire.
        /// </summary>
        /// <returns>
        /// The <see cref="IList"/>.
        /// </returns>
        public virtual IList<CacheDependencyKey> GetKeysOfDependentObjects()
        {
            if (!this.ContentType.IsNullOrEmpty())
            {
                var contentResolvedType = Telerik.Sitefinity.Utilities.TypeConverters.TypeResolutionService.ResolveType(this.ContentType);
                var result = new List<CacheDependencyKey>(1);
                if (this.DetailItem != null && this.DetailItem.Id != Guid.Empty)
                {
                    result.Add(new CacheDependencyKey { Key = this.DetailItem.Id.ToString(), Type = contentResolvedType });
                }
                else
                {
                    result.Add(new CacheDependencyKey { Key = null, Type = contentResolvedType });
                }

                return result;
            }
            else
            {
                return new List<CacheDependencyKey>(0);
            }
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Applies the list settings.
        /// </summary>
        /// <param name="page">The page.</param>
        /// <param name="items">The items.</param>
        private void ApplyListSettings(int? page, ref IQueryable<Telerik.Sitefinity.DynamicModules.Model.DynamicContent> items)
        {
            if (page == null || page < 1)
                page = 1;

            int? itemsToSkip = (page.Value - 1) * this.ItemsPerPage;
            itemsToSkip = this.DisplayMode == ListDisplayMode.Paging ? ((page.Value - 1) * this.ItemsPerPage) : null;
            int? totalCount = 0;
            int? itemsPerPage = this.DisplayMode == ListDisplayMode.All ? null : this.ItemsPerPage;

            var compiledFilterExpression = this.CompileFilterExpression();
            compiledFilterExpression = this.AddLiveFilterExpression(compiledFilterExpression);
            compiledFilterExpression = this.AdaptMultilingualFilterExpression(compiledFilterExpression);

            items = DataProviderBase.SetExpressions(
                items,
                compiledFilterExpression,
                this.SortExpression,
                itemsToSkip,
                itemsPerPage,
                ref totalCount);

            this.TotalPagesCount = (int)Math.Ceiling((double)(totalCount.Value / (double)this.ItemsPerPage.Value));
            this.TotalPagesCount = this.DisplayMode == ListDisplayMode.Paging ? this.TotalPagesCount : null;
            this.CurrentPage = page.Value;
        }

        private string AddLiveFilterExpression(string filterExpression)
        {
            if (filterExpression.IsNullOrEmpty())
            {
                filterExpression = "Visible = true AND Status = Live";
            }
            else
            {
                filterExpression = filterExpression + " AND Visible = true AND Status = Live";
            }

            return filterExpression;
        }

        /// <summary>
        /// Adapts a filter expression in multilingual.
        /// </summary>
        /// <param name="filterExpression">The filter expression.</param>
        /// <returns>Multilingual filter expression.</returns>
        private string AdaptMultilingualFilterExpression(string filterExpression)
        {
            CultureInfo uiCulture = null;

            if (SystemManager.CurrentContext.AppSettings.Multilingual)
            {
                uiCulture = System.Globalization.CultureInfo.CurrentUICulture;
            }

            // the filter is adapted to the implementation of ILifecycleDataItemGeneric, so the culture is taken in advance when filtering published items.
            return ContentHelper.AdaptMultilingualFilterExpressionRaw(filterExpression, uiCulture);
        }

        /// <summary>
        /// Initializes the manager.
        /// </summary>
        private void InitializeManager()
        {
            DynamicModuleManager dynamicManager;

            // try to resolve manager with control definition provider
            dynamicManager = this.ResolveManagerWithProvider(this.ProviderName);
            if (dynamicManager == null)
            {
                dynamicManager = this.ResolveManagerWithProvider(null);
            }

            this.manager = dynamicManager;
        }

        /// <summary>
        /// Resolves the manager with provider.
        /// </summary>
        /// <param name="providerName">Name of the provider.</param>
        /// <returns>A manager instance using a provider with the given name.</returns>
        private DynamicModuleManager ResolveManagerWithProvider(string providerName)
        {
            try
            {
                return DynamicModuleManager.GetManager(providerName);
            }
            catch (Exception)
            {
                // TODO: Do not catch general exception types
                return null;
            }
        }

        private string GetSelectedItemsFilterExpression()
        {
            var selectedItemIds = new List<Guid>() { this.SelectedItemId };

            var selectedItemsFilterExpression = string.Join(" OR ", selectedItemIds.Select(id => "Id = " + id));
            return selectedItemsFilterExpression;
        }

        #endregion

        #region Privte properties and constants

        private IList<IDataItem> items = new List<IDataItem>();
        private int? itemsPerPage = 20;
        private string sortExpression = "PublicationDate DESC";

        private DynamicModuleManager manager;
        private string serializedAdditionalFilters;
        private string serializedSelectedTaxonomies;
        private string serializedTaxonomyFilter;

        #endregion
    }
}
