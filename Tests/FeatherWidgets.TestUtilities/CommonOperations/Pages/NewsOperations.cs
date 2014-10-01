using System;
using System.Linq;
using Telerik.Sitefinity;
using Telerik.Sitefinity.Modules.News;
using Telerik.Sitefinity.News.Model;
using Telerik.Sitefinity.Taxonomies;
using Telerik.Sitefinity.Taxonomies.Model;

namespace FeatherWidgets.TestUtilities.CommonOperations.Pages
{
    public class NewsOperations
    {
        /// <summary>
        /// Creates news item with tag.
        /// </summary>
        /// <param name="newsTitle">The news title.</param>
        public void AddCustomTaxonomyToNews(string taxonomyName)
        {
            var newsManager = NewsManager.GetManager();
            if (newsManager != null)
            {
                var items = newsManager.GetNewsItems();

                if (items.Count() > 0)
                {
                    foreach (NewsItem item in items)
                    {
                        TaxonomyManager taxonomyManager = TaxonomyManager.GetManager();
                        var taxon = taxonomyManager.GetTaxa<FlatTaxon>().Where(t => t.Taxonomy.Name == taxonomyName).FirstOrDefault();
                        if (taxon != null)
                        {
                            item.Organizer.AddTaxa(taxonomyName, taxon.Id);                           
                        }
                    }

                    newsManager.SaveChanges();
                }
            }
        }
    }
}