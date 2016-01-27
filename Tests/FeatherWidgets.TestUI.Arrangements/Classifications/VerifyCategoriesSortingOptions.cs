using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FeatherWidgets.TestUtilities.CommonOperations;
using Telerik.Sitefinity.Taxonomies;
using Telerik.Sitefinity.Taxonomies.Model;
using Telerik.Sitefinity.TestArrangementService.Attributes;
using Telerik.Sitefinity.TestUI.Arrangements.Framework;
using Telerik.Sitefinity.TestUtilities.CommonOperations;

namespace FeatherWidgets.TestUI.Arrangements
{
    /// <summary>
    /// VerifyCategoriesSortingOptions arrangement class.
    /// </summary>
    public class VerifyCategoriesSortingOptions : TestArrangementBase
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
                ServerOperations.Taxonomies().CreateCategory(i + this.taxonTitleNews);
                ServerOperationsFeather.NewsOperations().CreatePublishedNewsItem(NewsTitle + i, NewsContent, AuthorName, SourceName, new List<string> { i + this.taxonTitleNews }, null, null);
            }

            ServerOperationsFeather.Pages().AddCategoriesWidgetToPage(pageId);
        }

        /// <summary>
        /// Tears down.
        /// </summary>
        [ServerTearDown]
        public void TearDown()
        {
            ServerOperations.Pages().DeleteAllPages();
            ServerOperations.Taxonomies().DeleteCategories();
            ServerOperations.News().DeleteAllNews();
        }

        private const string PageName = "CategoriesPage";
        private string taxonTitleNews = "CategoryNews";
        private const string ItemsTitle = "Title";
        private const string NewsContent = "News content";
        private const string NewsTitle = "NewsTitle";
        private const string AuthorName = "AuthorName";
        private const string SourceName = "SourceName";
    }
}
