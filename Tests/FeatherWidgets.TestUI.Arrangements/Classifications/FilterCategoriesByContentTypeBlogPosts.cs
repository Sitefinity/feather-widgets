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
    /// FilterCategoriesByContentTypeBlogPosts arrangement class.
    /// </summary>
    public class FilterCategoriesByContentTypeBlogPosts : TestArrangementBase
    {
        /// <summary>
        /// Server side set up.
        /// </summary>
        [ServerSetUp]
        public void SetUp()
        {
            Guid templateId = ServerOperations.Templates().GetTemplateIdByTitle(PageTemplateName);
            Guid pageId = ServerOperations.Pages().CreatePage(PageName, templateId);
            Guid pageNodeId = ServerOperations.Pages().GetPageNodeId(pageId);
            Guid blogId = ServerOperations.Blogs().CreateBlog(BlogTitle, pageId);            
            ServerOperations.Taxonomies().CreateCategory(this.taxonTitleBlogs);
            ServerOperations.Blogs().CreateBlogPostWithCategoryAndTag(BlogPostTitle, blogId);
            ServerOperations.Taxonomies().CreateCategory(this.taxonTitleNews + "0");

            for (int i = 1; i < 4; i++)
            {
                ServerOperations.Taxonomies().CreateCategory(this.taxonTitleNews + i, this.taxonTitleNews + (i - 1));
                ServerOperationsFeather.NewsOperations().CreatePublishedNewsItem(NewsTitle + i, NewsContent, AuthorName, SourceName, new List<string> { this.taxonTitleNews + i }, null, null);
            }

            ServerOperationsFeather.Pages().AddNewsWidgetToPage(pageNodeId, Placeholder);
            ServerOperationsFeather.Pages().AddBlogPostsWidgetToPage(pageNodeId, Placeholder);
        }

        /// <summary>
        /// Tears down.
        /// </summary>
        [ServerTearDown]
        public void TearDown()
        {
            ServerOperations.Pages().DeleteAllPages();
            ServerOperations.News().DeleteAllNews();
            ServerOperations.Taxonomies().DeleteCategories(this.taxonTitleBlogs);

            for (int i = 0; i <= 4; i++)
            {
                ServerOperations.Taxonomies().DeleteCategories(this.taxonTitleNews + i);
            }

            ServerOperations.Blogs().DeleteBlogPost(BlogPostTitle);
            ServerOperations.Blogs().DeleteAllBlogs();           
        }

        private const string PageName = "CategoriesPage";
        private const string PageTemplateName = "Bootstrap.default";
        private const string Placeholder = "Contentplaceholder1";
        private string taxonTitleNews = "CategoryNews";
        private string taxonTitleBlogs = "CategoryBlogs";
        private const string BlogTitle = "TestBlog";
        private const string BlogPostTitle = "TestPost";
        private const string NewsContent = "News content";
        private const string NewsTitle = "NewsTitle";
        private const string AuthorName = "AuthorName";
        private const string SourceName = "SourceName";
    }
}
