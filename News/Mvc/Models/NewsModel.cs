using System;
using System.Collections.Generic;
using System.Linq;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Modules.News;
using Telerik.Sitefinity.News.Model;

namespace News.Mvc.Models
{
    /// <summary>
    /// This class represents model used for News widget.
    /// </summary>
    public class NewsModel : INewsModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NewsModel"/> class.
        /// </summary>
        public NewsModel()
        {
            this.InitializeManager();
        }

        #region Properties

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

        public string ListCssClass
        {
            get;
            set;
        }

        public string DetailCssClass
        {
            get;
            set;
        }

        public IList<NewsItem> SelectedNews
        {
            get 
            {
                return this.selectedNews;
            }
            private set 
            {
                this.selectedNews = value;
            }
        }

        public NewsItem DetailNews
        {
            get;
            set;
        }

        public bool EnableSocialSharing 
        { 
            get; 
            set; 
        }

        public string ProviderName 
        { 
            get;
            set; 
        }


        /// <summary>
        /// Gets or sets which news to be displayed in the list view.
        /// </summary>
        /// <value>The page display mode.</value>
        public NewsSelectionMode SelectionMode
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether paging should be enabled.
        /// </summary>
        /// <value>
        ///   <c>true</c> when should enable paging; otherwise, <c>false</c>.
        /// </value>
        public bool EnablePaging
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the items count per page.
        /// </summary>
        /// <value>
        /// The items per page.
        /// </value>
        public int ItemsPerPage
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

        #endregion 

        #region Public methods

        public void PopulateNews(int? page)
        {
            this.EnablePaging = true;

            IQueryable<NewsItem> newsItems = null; 

            if (this.SelectionMode == NewsSelectionMode.SelectedNews)
            {
                newsItems = this.SelectedNews.AsQueryable<NewsItem>();
            }
            else
            {
                newsItems = this.manager.GetNewsItems()
                    .Where(n => n.Status == ContentLifecycleStatus.Live && n.Visible == true);
            }
            if (page == null || page < 1)
                page = 1;

            if (this.EnablePaging)
            {
                var itemsToSkip = (page.Value - 1) * this.ItemsPerPage;
                newsItems = newsItems.Skip(itemsToSkip).Take(this.ItemsPerPage);
            }

            this.News = newsItems.ToArray();
        }

        #endregion

        #region Private methods

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

        private NewsManager ResolveManagerWithProvider(string providerName)
        {
            try
            {
                return NewsManager.GetManager(providerName);
            }
            catch (Exception)
            {
                // DynamicModuleManager with this provider cannot be resolved 
                return null;
            }
        }

        #endregion 

        #region Privte properties and constants

        private IList<NewsItem> selectedNews = new List<NewsItem>();
        private IList<NewsItem> news = new List<NewsItem>();
        private int itemsPerPage = 2;

        private NewsManager manager;

        #endregion
    }
}