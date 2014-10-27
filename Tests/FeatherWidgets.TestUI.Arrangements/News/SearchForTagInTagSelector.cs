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
    /// DeleteTagAndEditNewsWidget arrangement class.
    /// </summary>
    public class SearchForTagInTagSelector : ITestArrangement
    {
        /// <summary>
        /// Server side set up.
        /// </summary>
        [ServerSetUp]
        public void SetUp()
        {
            Guid pageId = ServerOperations.Pages().CreatePage(PageName);

            for (int i = 0; i < 100; i++)
            {
                ServerOperations.Taxonomies().CreateTag(TaxonTitle + i);                
            }

            //// var tag = new List<string> { "Tag125" };
            //// ServerOperationsFeather.NewsOperations().CreatePublishedNewsItem(NewsTitle + 1, NewsContent, "AuthorName", "SourceName", null, tag, null);
            for (int i = 0; i < 100; i++)
            {
                ServerOperationsFeather.NewsOperations().CreatePublishedNewsItem(NewsTitle + i, NewsContent, "AuthorName", "SourceName", null, null, null);
            }

            ServerOperationsFeather.Pages().AddNewsWidgetToPage(pageId);
        }

        [ServerArrangement]
        public void DeleteTag()
        {
            ServerOperations.Taxonomies().DeleteTags("Tag1");
        }

        /// <summary>
        /// Tears down.
        /// </summary>
        [ServerTearDown]
        public void TearDown()
        {
            ServerOperations.Pages().DeleteAllPages();
            ServerOperations.News().DeleteAllNews();
            ServerOperations.Taxonomies().ClearAllTags(TaxonomiesConstants.TagsTaxonomyId);
        }

        private const string PageName = "News";
        private const string NewsContent = "News content";
        private const string NewsTitle = "NewsTitle";
        private const string TaxonTitle = "Tag";
    }
}