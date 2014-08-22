using System;
using System.Collections.Generic;
using System.Linq;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.News;
using Telerik.Sitefinity.News.Model;
using Telerik.Sitefinity.Taxonomies.Model;
using Telerik.Sitefinity.Data.Linq.Dynamic;
using System.Globalization;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Modules;
using Telerik.Sitefinity.Data;

namespace News.Mvc.Models
{
    /// <summary>
    /// This class represents model used for News widget.
    /// </summary>
    public class NewsModel : INewsModel
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="NewsModel"/> class.
        /// </summary>
        public NewsModel()
        {
            this.InitializeManager();
        }

        #endregion

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

        public Dictionary<string, IList<Guid>> TaxonomyFilter
        {
            get;
            set;
        }

        #endregion 

        #region Public methods

        /// <inheritdoc />
        public void PopulateNews(ITaxon taxonFilter, int? page)
        {
            IQueryable<NewsItem> newsItems = this.GetNewsItems();

            if (taxonFilter != null)
                newsItems = newsItems.Where(n => n.GetValue<IList<Guid>>(taxonFilter.Taxonomy.Name).Contains(taxonFilter.Id));

            this.AdaptMultilingualFilterExpression();

            this.ApplyListSettings(page, ref newsItems);

            this.News = newsItems.ToArray();
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Gets the news items depending on the Content section of the property editor.
        /// </summary>
        /// <returns></returns>
        private IQueryable<NewsItem> GetNewsItems()
        {
            IQueryable<NewsItem> newsItems;

            if (this.SelectionMode == NewsSelectionMode.SelectedNews)
            {
                var selectedItems = new List<NewsItem>() { this.manager.GetNewsItem(this.SelectedNewsId) };
                newsItems = selectedItems.AsQueryable<NewsItem>();
            }
            else if (this.SelectionMode == NewsSelectionMode.AllNews)
            {
                newsItems = this.manager.GetNewsItems()
                    .Where(n => n.Status == ContentLifecycleStatus.Live && n.Visible == true);
            }
            else 
            {
                newsItems = this.manager.GetNewsItems()
                    .Where(n => n.Status == ContentLifecycleStatus.Live && n.Visible == true);

                if (this.TaxonomyFilter!=null)
                {
                    foreach(var taxonFilter in this.TaxonomyFilter)
                    {
                        newsItems = newsItems.Where(n => n.GetValue<IList<Guid>>(taxonFilter.Key).Intersect(taxonFilter.Value).Any());
                    }
                }
            }

            return newsItems;
        }

        /// <summary>
        /// Applies the list settings.
        /// </summary>
        /// <param name="page">The page.</param>
        /// <param name="newsItems">The news items.</param>
        private void ApplyListSettings(int? page, ref IQueryable<NewsItem> newsItems)
        {
            if (page == null || page < 1)
                page = 1;

            int? itemsToSkip = ((page.Value - 1) * this.ItemsPerPage);
            itemsToSkip = this.DisplayMode==ListDisplayMode.Paging? ((page.Value - 1) * this.ItemsPerPage) : null ;
            int? totalCount = 0;
            int? itemsPerPage = this.DisplayMode == ListDisplayMode.All ?  null: this.ItemsPerPage;

            newsItems = DataProviderBase.SetExpressions(
                newsItems,
                this.FilterExpression,
                this.SortExpression,
                itemsToSkip,
                itemsPerPage,
                ref totalCount);

            this.TotalPagesCount = (int)Math.Ceiling((double)(totalCount.Value / (double)this.ItemsPerPage.Value));
            this.TotalPagesCount = this.DisplayMode == ListDisplayMode.Paging ? this.TotalPagesCount : null;
            this.CurrentPage = page.Value;
        }

        /// <summary>
        /// Adapts the filter expression in multilingual.
        /// </summary>
        private void AdaptMultilingualFilterExpression()
        {
            CultureInfo uiCulture = null;

            if (SystemManager.CurrentContext.AppSettings.Multilingual)
            {
                uiCulture = System.Globalization.CultureInfo.CurrentUICulture;
            }

            //the filter is adapted to the implementation of ILifecycleDataItemGeneric, so the culture is taken in advance when filtering published items.
            this.FilterExpression = ContentHelper.AdaptMultilingualFilterExpressionRaw(this.FilterExpression, uiCulture);
        }

        /// <summary>
        /// Initializes the manager.
        /// </summary>
        private void InitializeManager()
        {
            NewsManager manager;

            // try to resolve manager with control definition provider
            manager = this.ResolveManagerWithProvider(this.ProviderName);
            if (manager == null)
            {
                manager = this.ResolveManagerWithProvider(null);
            }

            this.manager = manager;
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
                return null;
            }
        }

        #endregion 

        #region Privte properties and constants

        private IList<NewsItem> news = new List<NewsItem>();
        private int? itemsPerPage = 2;
        private string sortExpression = "PublicationDate DESC";

        private NewsManager manager;

        #endregion

    }
}