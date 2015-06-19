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
    /// SelectAllTagsOptionAndCssClasses arrangement class.
    /// </summary>
    public class SelectAllTagsOptionAndCssClasses : ITestArrangement
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
                this.taxonomies.CreateTag(TaxonTitle + i);
            }

            ServerOperationsFeather.NewsOperations().CreatePublishedNewsItem(NewsTitle + 1, NewsContent, AuthorName, SourceName, null, new List<string> { TaxonTitle + 1 }, null);
            ServerOperationsFeather.NewsOperations().CreatePublishedNewsItem(NewsTitle + 2, NewsContent, AuthorName, SourceName, null, new List<string> { TaxonTitle + 2 }, null);
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
            this.taxonomies.ClearAllTags(TaxonomiesConstants.TagsTaxonomyId);
        }

        private const string PageName = "TagsPage";
        private const string NewsContent = "News content";
        private const string NewsTitle = "NewsTitle";
        private const string TaxonTitle = "Tag";
        private const string AuthorName = "AuthorName";
        private const string SourceName = "SourceName";
        private readonly TaxonomiesOperations taxonomies = ServerOperations.Taxonomies();
    }
}
