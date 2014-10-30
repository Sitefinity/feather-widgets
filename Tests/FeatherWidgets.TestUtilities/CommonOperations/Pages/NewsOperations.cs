using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Telerik.Sitefinity;
using Telerik.Sitefinity.Modules.News;
using Telerik.Sitefinity.News.Model;
using Telerik.Sitefinity.SitefinityExceptions;
using Telerik.Sitefinity.Taxonomies;
using Telerik.Sitefinity.Taxonomies.Model;
using Telerik.Sitefinity.TestUtilities.Utilities;

namespace FeatherWidgets.TestUtilities.CommonOperations.Pages
{
    public class NewsOperations
    {
        /// <summary>
        /// Creates news item with tag.
        /// </summary>
        /// <param name="newsTitle">The news title.</param>
        public void AddCustomTaxonomyToNews(Guid newsItemId, string taxonomyName, IEnumerable<string> taxonNames)
        {
            var newsManager = Telerik.Sitefinity.Modules.News.NewsManager.GetManager();
            NewsItem newsItem = newsManager.GetNewsItem(newsItemId);
            if (newsItem == null)
            {
                throw new ItemNotFoundException(string.Format(CultureInfo.CurrentCulture, "News item with id {0} was not found.", newsItemId));
            }
           
            TaxonomyManager taxonomyManager = TaxonomyManager.GetManager();
            foreach (var taxonName in taxonNames)
            {
                var taxon = taxonomyManager.GetTaxa<FlatTaxon>().Where(t => t.Title == taxonName).FirstOrDefault();
                if (taxon != null)
                {
                    newsItem.Organizer.AddTaxa(taxonomyName, taxon.Id);
                }
            }

            newsManager.SaveChanges();        
        }

        /// <summary>
        /// Creates published news item
        /// </summary>
        /// <param name="title">news title</param>
        /// <param name="content">news content</param>
        /// <param name="author">news author</param>
        /// <param name="sourceName">news source name</param>
        /// <param name="categories">news categories</param>
        /// <param name="tags">news tags</param>
        /// <param name="providerTitle">provider name</param>
        /// <returns>news master id</returns>
        public Guid CreatePublishedNewsItem(string title, string content, string author, string sourceName, IEnumerable<string> categories, IEnumerable<string> tags, string providerTitle)
        {
            NewsManager newsManager = null;
            if (providerTitle.IsNullOrEmpty())
            {
                newsManager = NewsManager.GetManager();
            }
            else
            {
                newsManager = NewsManager.GetManager();
                var provider = newsManager.ProviderInfos.SingleOrDefault(p => providerTitle.Equals(p.ProviderTitle, StringComparison.OrdinalIgnoreCase));
                newsManager = NewsManager.GetManager(provider.ProviderName);
            }

            var newsItem = this.CreateNewsWithBasicProperties(newsManager, title, content, author, sourceName);

            if (providerTitle.IsNullOrEmpty())
            {
                this.AddTaxonomiesToNews(newsItem.Id, categories, tags);
            }

            newsItem.SetWorkflowStatus(newsManager.Provider.ApplicationName, "Published");
            newsManager.Publish(newsItem);
            newsManager.SaveChanges();

            return newsItem.Id;
        }

        public Guid CreatePublishedNewsItemWithCustomTaxonomy(string title, string content, string author, string sourceName, string taxonomyName, IEnumerable<string> taxonNames, string providerTitle)
        {
            NewsManager newsManager = null;
            if (providerTitle.IsNullOrEmpty())
            {
                newsManager = NewsManager.GetManager();
            }
            else
            {
                newsManager = NewsManager.GetManager();
                var provider = newsManager.ProviderInfos.SingleOrDefault(p => providerTitle.Equals(p.ProviderTitle, StringComparison.OrdinalIgnoreCase));
                newsManager = NewsManager.GetManager(provider.ProviderName);
            }

            var newsItem = this.CreateNewsWithBasicProperties(newsManager, title, content, author, sourceName);

            if (providerTitle.IsNullOrEmpty())
            {
                this.AddCustomTaxonomyToNews(newsItem.Id, taxonomyName, taxonNames);
            }

            newsItem.SetWorkflowStatus(newsManager.Provider.ApplicationName, "Published");
            newsManager.Publish(newsItem);
            newsManager.SaveChanges();

            return newsItem.Id;
        }

        /// <summary>
        /// Adds the taxonomies to news.
        /// </summary>
        /// <param name="newsItemId">The news item id.</param>
        /// <param name="categories">The categories.</param>
        /// <param name="tags">The tags.</param>
        public void AddTaxonomiesToNews(Guid newsItemId, IEnumerable<string> categories, IEnumerable<string> tags)
        {
            var newsManager = Telerik.Sitefinity.Modules.News.NewsManager.GetManager();
            NewsItem newsItem = newsManager.GetNewsItem(newsItemId);
            if (newsItem == null)
            {
                throw new ItemNotFoundException(string.Format(CultureInfo.CurrentCulture, "News item with id {0} was not found.", newsItemId));
            }

            var taxonomyManager = TaxonomyManager.GetManager();
            if (categories != null)
            {
                if (categories.Count() > 0)
                {
                    HierarchicalTaxon category = null;
                    foreach (var c in categories)
                    {
                        category = taxonomyManager.GetTaxa<HierarchicalTaxon>().Single(t => t.Title == c);
                        newsItem.Organizer.AddTaxa("Category", category.Id);
                    }
                }
            }

            if (tags != null)
            {
                if (tags.Count() > 0)
                {
                    FlatTaxon tag = null;
                    foreach (var tg in tags)
                    {
                        tag = taxonomyManager.GetTaxa<FlatTaxon>().Single(t => t.Title == tg);
                        newsItem.Organizer.AddTaxa("Tags", tag.Id);
                    }
                }
            }

            newsManager.SaveChanges();
        }

        private NewsItem CreateNewsWithBasicProperties(NewsManager manager, string title, string content, string author, string sourceName)
        {
            var newsItem = manager.CreateNewsItem();
            newsItem.Title = title;
            newsItem.DateCreated = DateTime.UtcNow;
            newsItem.PublicationDate = DateTime.UtcNow.AddDays(1);
            newsItem.ExpirationDate = DateTime.UtcNow.AddDays(30);
            newsItem.Author = author;
            newsItem.Content = content;
            newsItem.SourceName = sourceName;
            newsItem.UrlName = ServerArrangementUtilities.GetFormatedUrlName(title);

            return newsItem;
        }
    }
}