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
    /// SelectedTagsOptionAndCloudList arrangement class.
    /// </summary>
    public class SelectedTagsOptionAndCloudList : ITestArrangement
    {
        /// <summary>
        /// Server side set up.
        /// </summary>
        [ServerSetUp]
        public void SetUp()
        {
            Guid templateId = Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.Templates().GetTemplateIdByTitle(PageTemplateName);
            Guid pageId = Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.Pages().CreatePage(PageName, templateId);
            pageId = ServerOperations.Pages().GetPageNodeId(pageId);
            ServerOperationsFeather.Pages().AddTagsWidgetToPage(pageId, PlaceHolderId);
            ServerOperationsFeather.Pages().AddNewsWidgetToPage(pageId, PlaceHolderId);

            for (int i = 1; i < 3; i++)
            {
                ServerOperations.Taxonomies().CreateTag(TaxonTitle + i);
                ServerOperationsFeather.NewsOperations().CreatePublishedNewsItem(NewsTitle + i, NewsContent, "AuthorName", "SourceName", null, new List<string> { TaxonTitle + i }, null);
            }

            ServerOperations.Taxonomies().CreateTag(TaxonTitle + 3);
            ServerOperationsFeather.NewsOperations().CreatePublishedNewsItem(NewsTitle + 3, NewsContent, "AuthorName", "SourceName", null, new List<string> { TaxonTitle + 2 }, null);
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

        private const string PageName = "TagsPage";
        private const string NewsContent = "News content";
        private const string NewsTitle = "NewsTitle";
        private const string TaxonTitle = "Tag";
        private const string PageTemplateName = "Bootstrap.default";
        private const string PlaceHolderId = "Contentplaceholder1";
    }
}
