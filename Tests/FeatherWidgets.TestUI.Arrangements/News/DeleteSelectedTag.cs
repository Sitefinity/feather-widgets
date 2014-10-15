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
    /// DeleteSelectedTag arrangement class.
    /// </summary>
    public class DeleteSelectedTag : ITestArrangement
    {
        /// <summary>
        /// Server side set up.
        /// </summary>
        [ServerSetUp]
        public void SetUp()
        {
            Guid pageId = ServerOperations.Pages().CreatePage(PageName);
            List<string> tags = new List<string>();

            for (int i = 0; i < 100; i++)
            {
                tags.Add(TaxonTitle + i);
            }

            int index = 0;
            foreach (var tg in tags)
            {
                ServerOperations.Taxonomies().CreateTag(tg);
                var tag = new List<string> { tg };
                ServerOperationsFeather.NewsOperations().CreatePublishedNewsItem(NewsTitle + index, NewsContent, "AuthorName", "SourceName", null, tag, null);
                index++;
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