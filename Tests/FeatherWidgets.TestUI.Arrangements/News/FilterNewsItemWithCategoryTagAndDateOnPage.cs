using System;
using System.Collections.Generic;
using System.Linq;
using FeatherWidgets.TestUtilities.CommonOperations;
using Telerik.Sitefinity.Modules.News;
using Telerik.Sitefinity.News.Model;
using Telerik.Sitefinity.TestUI.Arrangements.Framework;
using Telerik.Sitefinity.TestUI.Arrangements.Framework.Attributes;
using Telerik.Sitefinity.TestUtilities.CommonOperations;

namespace FeatherWidgets.TestUI.Arrangements
{
    /// <summary>
    /// FilterNewsItemWithCategoryTagAndDateOnPage arrangement class.
    /// </summary>
    public class FilterNewsItemWithCategoryTagAndDateOnPage : ITestArrangement
    {
        /// <summary>
        /// Server side set up.
        /// </summary>
        [ServerSetUp]
        public void SetUp()
        {
            Guid pageId = ServerOperations.Pages().CreatePage(PageName);
            ServerOperations.Taxonomies().CreateCategory(CategoryTitle + "0");

            for (int i = 1; i < 5; i++)
            {
                ServerOperations.Taxonomies().CreateCategory(CategoryTitle + i, CategoryTitle + (i - 1));
                ServerOperations.Taxonomies().CreateTag(TagTitle + i);
                ServerOperationsFeather.NewsOperations().CreatePublishedNewsItem(NewsTitle + (i - 1), NewsContent, "AuthorName", "SourceName", new List<string> { CategoryTitle + i }, new List<string> { TagTitle + i }, null);                           
            }

            DateTime publicationDate = DateTime.UtcNow.AddDays(-10);

            var newsManager = NewsManager.GetManager();
            NewsItem modified = newsManager.GetNewsItems().Where<NewsItem>(ni => ni.Status == Telerik.Sitefinity.GenericContent.Model.ContentLifecycleStatus.Master && ni.Title == "NewsTitle1").FirstOrDefault();
            newsManager.Lifecycle.PublishWithSpecificDate(modified, publicationDate);
            newsManager.SaveChanges();

            ServerOperationsFeather.Pages().AddNewsWidgetToPage(pageId);
        }

        /// <summary>
        /// Tears down.
        /// </summary>
        [ServerTearDown]
        public void TearDown()
        {
            ServerOperations.Pages().DeleteAllPages();
            ServerOperations.News().DeleteAllNews();
            ServerOperations.Taxonomies().ClearAllCategories(TaxonomiesConstants.CategoriesTaxonomyId);
            ServerOperations.Taxonomies().ClearAllTags(TaxonomiesConstants.TagsTaxonomyId);
        }

        private const string PageName = "News";
        private const string NewsContent = "News content";
        private const string NewsTitle = "NewsTitle";
        private const string CategoryTitle = "Category";
        private const string TagTitle = "Tag";
    }
}
