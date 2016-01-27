using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FeatherWidgets.TestUtilities.CommonOperations;
using Telerik.Sitefinity.TestArrangementService.Attributes;
using Telerik.Sitefinity.TestUI.Arrangements.Framework;
using Telerik.Sitefinity.TestUtilities.CommonOperations;

namespace FeatherWidgets.TestUI.Arrangements
{
    /// <summary>
    /// SelectAllCategoriesOptionAndFilterByCategory arrangement class.
    /// </summary>
    public class SelectAllCategoriesOptionAndFilterByCategory : TestArrangementBase
    {
        /// <summary>
        /// Server side set up.
        /// </summary>
        [ServerSetUp]
        public void SetUp()
        {
            Guid pageId = ServerOperations.Pages().CreatePage(PageName);

            for (int i = 1; i < 4; i++)
            {
                ServerOperations.Taxonomies().CreateCategory(this.categoryTitle + i);
            }
                
            ServerOperationsFeather.NewsOperations().CreatePublishedNewsItem(NewsTitle + 1, NewsContent, AuthorName, SourceName, new List<string> { this.categoryTitle + 1 }, null, null);
            ServerOperationsFeather.NewsOperations().CreatePublishedNewsItem(NewsTitle + 2, NewsContent, AuthorName, SourceName, new List<string> { this.categoryTitle + 2 }, null, null);
            ServerOperationsFeather.NewsOperations().CreatePublishedNewsItem(NewsTitle + 3, NewsContent, AuthorName, SourceName, null, null, null);
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
            ServerOperations.Taxonomies().DeleteCategories();
        }

        private const string PageName = "CategoriesPage";
        private string categoryTitle = "Category";
        private const string NewsContent = "News content";
        private const string NewsTitle = "NewsTitle";
        private const string AuthorName = "AuthorName";
        private const string SourceName = "SourceName";
    }
}
