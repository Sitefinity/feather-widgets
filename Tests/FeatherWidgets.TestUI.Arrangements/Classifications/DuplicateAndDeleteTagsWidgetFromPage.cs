﻿using System;
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
    /// DuplicateAndDeleteTagsWidgetFromPage arrangement class.
    /// </summary>
    public class DuplicateAndDeleteTagsWidgetFromPage : TestArrangementBase
    {
        /// <summary>
        /// Server side set up.
        /// </summary>
        [ServerSetUp]
        public void SetUp()
        {
            Guid pageId = ServerOperations.Pages().CreatePage(PageName);
            Guid blogId = ServerOperations.Blogs().CreateBlog(BlogTitle, pageId);
            this.taxonomies.CreateTag(TaxonTitleBlogs);
            ServerOperations.Blogs().CreateBlogPostWithCategoryAndTag(BlogPostTitle, blogId);

            this.taxonomies.CreateTag(TaxonTitleNews);
            ServerOperationsFeather.NewsOperations().CreatePublishedNewsItem(NewsTitle, NewsContent, AuthorName, SourceName, null, new List<string> { TaxonTitleNews }, null);
            
            ServerOperationsFeather.Pages().AddTagsWidgetToPage(pageId);
        }

        /// <summary>
        /// Tears down.
        /// </summary>
        [ServerTearDown]
        public void TearDown()
        {
            ServerOperations.Pages().DeleteAllPages();
            ServerOperations.News().DeleteAllNews();
            ServerOperations.Blogs().DeleteBlogPost(BlogPostTitle);
            ServerOperations.Blogs().DeleteAllBlogs();
            this.taxonomies.ClearAllTags(TaxonomiesConstants.TagsTaxonomyId);
        }

        private const string PageName = "TagsPage";
        private const string NewsContent = "News content";
        private const string NewsTitle = "NewsTitle";
        private const string TaxonTitleNews = "NewsTag";
        private const string TaxonTitleBlogs = "BlogTag";
        private const string BlogTitle = "TestBlog";
        private const string BlogPostTitle = "TestBlogPost";
        private const string AuthorName = "AuthorName";
        private const string SourceName = "SourceName";
        private readonly TaxonomiesOperations taxonomies = ServerOperations.Taxonomies();
    }
}