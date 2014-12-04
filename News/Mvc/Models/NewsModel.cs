﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;

using ServiceStack.Text;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules;
using Telerik.Sitefinity.Modules.News;
using Telerik.Sitefinity.News.Model;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Taxonomies.Model;
using Telerik.Sitefinity.Web.Model;

namespace News.Mvc.Models
{
    /// <summary>
    /// This class represents model used for News widget.
    /// </summary>
    public class NewsModel : INewsModel
    {
        #region Properties

        /// <inheritdoc />
        public IList<NewsItem> Items
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
        public string SerializedSelectedItemsIds
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
        public NewsItem DetailItem
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
        public NewsSelectionMode SelectionMode
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

        /// <summary>
        /// Gets or sets the taxonomy filter.
        /// </summary>
        /// <value>
        /// The taxonomy filter.
        /// </value>
        [Browsable(false)]
        [Obsolete("Use SerializedAdditionalFilters instead.")]
        public Dictionary<string, IList<Guid>> TaxonomyFilter
        {
            get;
            set;
        }

        /// <inheritdoc />
        [Obsolete("Use SerializedAdditionalFilters instead.")]
        public string SerializedTaxonomyFilter
        {
            get
            {
                return this.serializedTaxonomyFilter;
            }

            set
            {
                if (this.serializedTaxonomyFilter != value)
                {
                    this.serializedTaxonomyFilter = value;
                    if (!this.serializedTaxonomyFilter.IsNullOrEmpty())
                    {
                        this.TaxonomyFilter = JsonSerializer.DeserializeFromString<Dictionary<string, IList<Guid>>>(this.serializedTaxonomyFilter);
                    }
                }
            }
        }

        /// <inheritdoc />
        [Obsolete("Use SerializedAdditionalFilters instead.")]
        public string SerializedSelectedTaxonomies
        {
            get
            {
                return this.serializedSelectedTaxonomies;
            }

            set
            {
                if (this.serializedSelectedTaxonomies != value)
                {
                    this.serializedSelectedTaxonomies = value;
                }
            }
        }

        #endregion 

        #region Public methods

        /// <inheritdoc />
        public virtual void PopulateItems(ITaxon taxonFilter, string taxonField, int? page)
        {
            this.InitializeManager();

            if (this.manager == null)
                return;

            if (this.SelectionMode == NewsSelectionMode.SelectedItems && this.selectedItemsIds.Count == 0)
                return;

            var newsItems = this.manager.GetNewsItems();

            if (taxonFilter != null && !taxonField.IsNullOrEmpty())
                newsItems = newsItems.Where(n => n.GetValue<IList<Guid>>(taxonField).Contains(taxonFilter.Id));

            this.ApplyListSettings(page, ref newsItems);

            this.Items = newsItems.ToArray();
        }

        /// <inheritdoc />
        public virtual string CompileFilterExpression()
        {
            var elements = new List<string>();

            if (this.SelectionMode == NewsSelectionMode.FilteredItems)
            {
                if (this.AdditionalFilters != null)
                {
                    var queryExpression = Telerik.Sitefinity.Data.QueryBuilder.LinqTranslator.ToDynamicLinq(this.AdditionalFilters);

                    if (!string.IsNullOrEmpty(queryExpression))
                    elements.Add(queryExpression);
                }
            }
            else if (this.SelectionMode == NewsSelectionMode.SelectedItems)
            {
                var selectedItemsFilterExpression = this.GetSelectedItemsFilterExpression();

                if (!string.IsNullOrEmpty(selectedItemsFilterExpression))
                    elements.Add(selectedItemsFilterExpression);
            }

            if (!string.IsNullOrEmpty(this.FilterExpression))
                elements.Add(this.FilterExpression);

            var compiledExpression = string.Join(" AND ", elements.Select(el => "(" + el + ")"));

            return compiledExpression;
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
            var result = new List<CacheDependencyKey>(1);
            if (this.DetailItem != null && this.DetailItem.Id != Guid.Empty)
            {
                result.Add(new CacheDependencyKey { Key = this.DetailItem.Id.ToString(), Type = typeof(NewsItem) });
            }
            else
            {
                result.Add(new CacheDependencyKey { Key = null, Type = typeof(NewsItem) });
            }

            return result;
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Applies the list settings.
        /// </summary>
        /// <param name="page">The page.</param>
        /// <param name="newsItems">The news items.</param>
        private void ApplyListSettings(int? page, ref IQueryable<NewsItem> newsItems)
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

            newsItems = this.SetExpressions(newsItems, compiledFilterExpression, this.SortExpression, itemsToSkip, itemsPerPage, ref totalCount);

            this.TotalPagesCount = (int)Math.Ceiling((double)(totalCount.Value / (double)this.ItemsPerPage.Value));
            this.TotalPagesCount = this.DisplayMode == ListDisplayMode.Paging ? this.TotalPagesCount : null;
            this.CurrentPage = page.Value;
        }

        private IQueryable<NewsItem> SetExpressions(IQueryable<NewsItem> newsItems, string filterExpression, string sortExpr, int? itemsToSkip, int? itemsToTake, ref int? totalCount)
        {
            if (this.SelectionMode == NewsSelectionMode.SelectedItems)
            {
                newsItems = DataProviderBase.SetExpressions(
                    newsItems,
                    filterExpression,
                    string.Empty, 
                    null, 
                    null,
                    ref totalCount);

                newsItems = newsItems.Select(x => new 
                    {
                        newsItem = x,
                        orderIndex = this.selectedItemsIds.IndexOf(x.Id.ToString())
                    })
                    .OrderBy(x => x.orderIndex)
                    .Select(x => x.newsItem);

                if (itemsToSkip.HasValue && itemsToSkip.Value > 0)
                {
                    newsItems = newsItems.Skip(itemsToSkip.Value);
                }

                if (itemsToTake.HasValue && itemsToTake.Value > 0)
                {
                    newsItems = newsItems.Take(itemsToTake.Value);
                }
            }
            else
            {
                newsItems = DataProviderBase.SetExpressions(
                    newsItems,
                    filterExpression,
                    sortExpr,
                    itemsToSkip,
                    itemsToTake,
                    ref totalCount);
            }

            return newsItems;
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
            NewsManager newsManager;

            // try to resolve manager with control definition provider
            newsManager = this.ResolveManagerWithProvider(this.ProviderName);
            if (newsManager == null)
            {
                newsManager = this.ResolveManagerWithProvider(null);
            }

            this.manager = newsManager;
        }

        /// <summary>
        /// Resolves the manager with provider.
        /// </summary>
        /// <param name="providerName">Name of the provider.</param>
        /// <returns></returns>
        private NewsManager ResolveManagerWithProvider(string providerName)
        {
            try
            {
                return NewsManager.GetManager(providerName);
            }
            catch (Exception)
            {
                // TODO: Do not catch general exception types
                return null;
            }
        }

        private string GetSelectedItemsFilterExpression()
        {
            var selectedItemGuids = this.selectedItemsIds.Select(s => new Guid(s)).ToArray();

            var newsManager = this.ResolveManagerWithProvider(this.ProviderName);
            var masterIds = newsManager.GetNewsItems().Where(n => selectedItemGuids.Contains(n.Id) && n.OriginalContentId != Guid.Empty).Select(n => n.OriginalContentId.ToString("D"));

            var selectedItemConditions = this.selectedItemsIds.Select(id => "Id = " + id.Trim()).ToList();
            selectedItemConditions.AddRange(masterIds.Select(id => "OriginalContentId = " + id.Trim()));

            var selectedItemsFilterExpression = string.Join(" OR ", selectedItemConditions);
            return selectedItemsFilterExpression;
        }

        #endregion 

        #region Private properties and constants

        private IList<NewsItem> items = new List<NewsItem>();
        private int? itemsPerPage = 20;
        private string sortExpression = "PublicationDate DESC";

        private NewsManager manager;
        private string serializedAdditionalFilters;
        private string serializedSelectedTaxonomies;
        private string serializedTaxonomyFilter;
        private string serializedSelectedItemsIds;
        private IList<string> selectedItemsIds = new List<string>();

        #endregion
    }
}