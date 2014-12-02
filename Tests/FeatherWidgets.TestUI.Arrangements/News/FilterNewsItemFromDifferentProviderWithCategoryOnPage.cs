using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FeatherWidgets.TestUtilities.CommonOperations;
using Telerik.Sitefinity.TestUI.Arrangements.Framework;
using Telerik.Sitefinity.TestUI.Arrangements.Framework.Attributes;
using Telerik.Sitefinity.TestUtilities.CommonOperations;

namespace FeatherWidgets.TestUI.Arrangements
{
    /// <summary>
    /// FilterNewsItemFromDifferentProviderWithCategoryOnPage arrangement class.
    /// </summary>
    public class FilterNewsItemFromDifferentProviderWithCategoryOnPage : ITestArrangement
    {
        /// <summary>
        /// Server side set up.
        /// </summary>
        [ServerSetUp]
        public void SetUp()
        {
            ServerOperations.News().CreateSecondDataProvider();
            Guid pageId = ServerOperations.Pages().CreatePage(PageName);
            ServerOperations.Taxonomies().CreateCategory(TaxonTitle + "0");

            for (int i = 1; i < 7; i++)
            {
                ServerOperations.Taxonomies().CreateCategory(TaxonTitle + i, TaxonTitle + (i - 1));
                ServerOperationsFeather.NewsOperations().CreatePublishedNewsItem(NewsTitle + (i - 1), NewsContent, "AuthorName", "SourceName", new List<string> { TaxonTitle + i }, null, SecondProviderName);
            }

            ServerOperationsFeather.Pages().AddNewsWidgetToPage(pageId);
        }

        /// <summary>
        /// Tears down.
        /// </summary>
        [ServerTearDown]
        public void TearDown()
        {
            ServerOperations.Pages().DeleteAllPages();
            ServerOperations.News().DeleteAllNews(SecondProviderName);
            ServerOperations.Taxonomies().ClearAllCategories(TaxonomiesConstants.CategoriesTaxonomyId);
            ServerOperations.News().RemoveSecondDataProvider();
        }

        private const string PageName = "News";
        private const string NewsContent = "News content";
        private const string NewsTitle = "NewsTitle";
        private const string TaxonTitle = "Category";
        private const string SecondProviderName = "NewsSecondDataProvider";
    }
}
