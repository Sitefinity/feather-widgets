﻿using System;
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
    /// DeleteTagAndEditNewsWidget arrangement class.
    /// </summary>
    public class DeleteTagAndEditNewsWidget : TestArrangementBase
    {
        /// <summary>
        /// Server side set up.
        /// </summary>
        [ServerSetUp]
        public void SetUp()
        {
            Guid pageId = ServerOperations.Pages().CreatePage(PageName);
            var tags = new List<string> { "Tag1", "Tag2" };
            int index = 1;
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
    }
}