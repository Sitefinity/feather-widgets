using System;
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

namespace News.Mvc.Models
{
    /// <summary>
    /// This class represents model used for News widget.
    /// </summary>
    public class NewsModel : INewsModel
    {
        #region Properties

        /// <inheritdoc />
        public IList<NewsItem> News
        {
            get
            {
                return this.news;
            }

            private set
            {
                this.news = value;
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
        public Guid SelectedNewsId
        {
            get;
            set;
        }

        /// <inheritdoc />
        public NewsItem DetailNews
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
        /// Gets or sets the taxonomy filter.
        /// </summary>
        /// <value>
        /// The taxonomy filter.
        /// </value>
        [Browsable(false)]
        public Dictionary<string, IList<Guid>> TaxonomyFilter
        {
            get;
            set;
        }

        /// <inheritdoc />
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
                    if (!this.serializedSelectedTaxonomies.IsNullOrEmpty())
                    {
                        this.selectedTaxonomies = JsonSerializer.DeserializeFromString<IList<string>>(this.serializedSelectedTaxonomies);
                    }
                }
            }
        }

        #endregion 

        #region Public methods

        /// <inheritdoc />
        public virtual void PopulateNews(ITaxon taxonFilter, string taxonField, int? page)
        {
            this.InitializeManager();

            if (this.manager == null)
                return;

            var newsItems = this.manager.GetNewsItems();

            if (taxonFilter != null && !taxonField.IsNullOrEmpty())
                newsItems = newsItems.Where(n => n.GetValue<IList<Guid>>(taxonField).Contains(taxonFilter.Id));

            this.ApplyListSettings(page, ref newsItems);

            this.News = newsItems.ToArray();
        }

        /// <inheritdoc />
        public virtual string CompileFilterExpression()
        {
            var elements = new List<string>();

            if (this.SelectionMode == NewsSelectionMode.FilteredNews)
            {
                var taxonomyFilterExpression = this.GetTaxonomyFilterExpression();
                if (!taxonomyFilterExpression.IsNullOrEmpty())
                {
                    elements.Add(taxonomyFilterExpression);
                }
            }
            else if (this.SelectionMode == NewsSelectionMode.SelectedNews)
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
            var result = new List<CacheDependencyKey>(1);
            if (this.DetailNews != null && this.DetailNews.Id != Guid.Empty)
            {
                result.Add(new CacheDependencyKey { Key = this.DetailNews.Id.ToString(), Type = typeof(NewsItem) });
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

            newsItems = DataProviderBase.SetExpressions(
                newsItems,
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

        private string GetTaxonomyFilterExpression()
        {
            var taxonomyFilterExpression = string.Join(
                " AND ",
                this.TaxonomyFilter
                    .Where(tf => (tf.Value.Count > 0 && this.selectedTaxonomies.Contains(tf.Key)))
                    .Select(tf => "(" + string.Join(" OR ", tf.Value.Select(id => "{0}.Contains(({1}))".Arrange(tf.Key, id))) + ")"));

            return taxonomyFilterExpression;
        }

        private string GetSelectedItemsFilterExpression()
        {
            var selectedItemIds = new List<Guid>() { this.SelectedNewsId };

            var selectedItemsFilterExpression = string.Join(" OR ", selectedItemIds.Select(id => "Id = " + id));
            return selectedItemsFilterExpression;
        }

        #endregion 

        #region Privte properties and constants

        private IList<NewsItem> news = new List<NewsItem>();
        private int? itemsPerPage = 20;
        private string sortExpression = "PublicationDate DESC";

        private NewsManager manager;
        private string serializedTaxonomyFilter;
        private string serializedSelectedTaxonomies;
        private IList<string> selectedTaxonomies = new List<string>();

        #endregion
    }
}