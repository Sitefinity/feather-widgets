using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FeatherWidgets.TestUtilities.CommonOperations;
using MbUnit.Framework;
using Telerik.Sitefinity.Frontend.Blogs.Mvc.Controllers;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers;
using Telerik.Sitefinity.Frontend.TestUtilities;
using Telerik.Sitefinity.Mvc.Proxy;
using Telerik.Sitefinity.Taxonomies;
using Telerik.Sitefinity.Taxonomies.Model;
using Telerik.Sitefinity.TestUtilities.CommonOperations;

namespace FeatherWidgets.TestIntegration.BlogPosts
{
    /// <summary>
    /// This is a test fixture with tests for narrow selection options.
    /// </summary>
    [TestFixture]
    public class BlogPostsWidgetNarrowSelectionTests
    {
        [Test]
        [Category(TestCategories.Blogs)]
        [Author(FeatherTeams.FeatherTeam)]
        [Description("Verifies that blog posts controller is filtering blog posts by tags.")]
        public void BlogPostsWidget_FilterBlogPostsByTags()
        {
            int countOfItemsWithTag = 1;
            string blogTitle = "Blog";
            string blogPost1Title = "BlogPost_WithTag";
            string blogPost2Title = "BlogPost_NoTag";
            string tagTitle = "TestTag";
            BlogPostController blogPostsController = new BlogPostController();

            try
            {
                Guid blogId = ServerOperations.Blogs().CreateBlog(blogTitle);
                ServerOperations.Blogs().CreatePublishedBlogPost(blogPost2Title, blogId);

                var taxonId = ServerOperations.Taxonomies().CreateFlatTaxon(TaxonomiesConstants.TagsTaxonomyId, tagTitle);
                var blogPost1Id = ServerOperations.Blogs().CreatePublishedBlogPost(blogPost1Title, blogId);
                ServerOperations.Blogs().AssignTaxonToBlogPost(blogPost1Id, "Tags", taxonId);

                ITaxon taxonomy = TaxonomyManager.GetManager().GetTaxon(taxonId);
                var items = blogPostsController.Model.CreateListViewModel(taxonomy, 1).Items.ToArray();

                Assert.AreEqual(countOfItemsWithTag, items.Count());
                Assert.IsTrue(items[0].Fields.Title.Equals(blogPost1Title, StringComparison.CurrentCulture), "The blog post with this title was not found!");              
            }
            finally
            {
                ServerOperations.Taxonomies().DeleteTags(tagTitle);
                ServerOperations.Blogs().DeleteAllBlogs();
            }
        }

        [Test]
        [Category(TestCategories.Blogs)]
        [Author(FeatherTeams.FeatherTeam)]
        [Description("Verifies that blog posts controller is filtering blog posts by categories.")]
        public void BlogPostsWidget_FilterBlogPostsByCategories()
        {
            int countOfItemsWithCategories = 1;
            string blogTitle = "Blog";
            string blogPost1Title = "BlogPost_WithCategory";
            string blogPost2Title = "BlogPost_NoCategory";
            string categoryTitle = "Test_Category";
            BlogPostController blogPostsController = new BlogPostController();

            try
            {
                Guid blogId = ServerOperations.Blogs().CreateBlog(blogTitle);
                ServerOperations.Blogs().CreatePublishedBlogPost(blogPost2Title, blogId);

                var taxonId = ServerOperations.Taxonomies().CreateHierarchicalTaxon(TaxonomiesConstants.CategoriesTaxonomyId, categoryTitle);
                var blogPost1Id = ServerOperations.Blogs().CreatePublishedBlogPost(blogPost1Title, blogId);
                ServerOperations.Blogs().AssignTaxonToBlogPost(blogPost1Id, "Category", taxonId);

                ITaxon taxonomy = TaxonomyManager.GetManager().GetTaxon(taxonId);
                var items = blogPostsController.Model.CreateListViewModel(taxonomy, 1).Items.ToArray();

                Assert.AreEqual(countOfItemsWithCategories, items.Count());
                Assert.IsTrue(items[0].Fields.Title.Equals(blogPost1Title, StringComparison.CurrentCulture), "The blog post with this title was not found!");
            }
            finally
            {
                ServerOperations.Taxonomies().DeleteCategories(categoryTitle);
                ServerOperations.Blogs().DeleteAllBlogs();
            }
        }
    }
}
