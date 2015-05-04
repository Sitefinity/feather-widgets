using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FeatherWidgets.TestUtilities.CommonOperations;
using MbUnit.Framework;
using Telerik.Sitefinity.Frontend.Blogs.Mvc.Controllers;
using Telerik.Sitefinity.Frontend.Blogs.Mvc.Models.BlogPost;
using Telerik.Sitefinity.Frontend.DynamicContent.Mvc.Controllers;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers;
using Telerik.Sitefinity.Frontend.Mvc.Models;
using Telerik.Sitefinity.Frontend.TestUtilities;
using Telerik.Sitefinity.Mvc.Proxy;
using Telerik.Sitefinity.TestIntegration.Data.Content;
using Telerik.Sitefinity.TestUtilities.CommonOperations;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.Web;

namespace FeatherWidgets.TestIntegration.BlogPosts
{
    /// <summary>
    /// This is a class with Blog posts widget tests.
    /// </summary>
    [TestFixture]
    [Description("This is a class with Blog posts widget tests.")]
    public class BlogPostsWidgetTests
    {       
        [Test]
        [Category(TestCategories.Blogs)]
        [Author(FeatherTeams.Team2)]
        [Description("Add Blog posts widget to a page and display posts from selected blogs only.")]
        public void BlogPostsWidget_DisplayPostsFromSelectedBlogsOnly()
        {
            string blog1Title = "Blog1";
            string blog2Title = "Blog2";
            string blog1PostTitle = "Blog1Post1";
            string blog2PostTitle = "Blog2Post1";
            string pageTitle = "PageWithBlogPostsWidget";

            Guid blog1Id = ServerOperations.Blogs().CreateBlog(blog1Title);
            ServerOperations.Blogs().CreatePublishedBlogPost(blog1PostTitle, blog1Id);

            Guid blog2Id = ServerOperations.Blogs().CreateBlog(blog2Title);
            ServerOperations.Blogs().CreatePublishedBlogPost(blog2PostTitle, blog2Id);

            Guid pageId = ServerOperations.Pages().CreatePage(pageTitle);

            try
            {
                var blogPostsWidget = this.CreateBlogPostsMvcWidget(blog1Id, ParentFilterMode.Selected, SelectionMode.AllItems, default(Guid));

                var controls = new List<System.Web.UI.Control>();
                controls.Add(blogPostsWidget);

                PageContentGenerator.AddControlsToPage(pageId, controls);

                string url = UrlPath.ResolveAbsoluteUrl("~/" + pageTitle);
                string responseContent = PageInvoker.ExecuteWebRequest(url);

                Assert.IsTrue(responseContent.Contains(blog1PostTitle), "The item with this title was NOT found " + blog1PostTitle);
                Assert.IsFalse(responseContent.Contains(blog2PostTitle), "The item with this title WAS found " + blog2PostTitle);
            }
            finally
            {
                ServerOperations.Pages().DeleteAllPages();
                ServerOperations.Blogs().DeleteAllBlogs();
            }
        }

        [Test]
        [Category(TestCategories.Blogs)]
        [Author(FeatherTeams.Team2)]
        [Description("Add Blog posts widget to a page and display posts from currently opened blog.")]
        public void BlogPostsWidget_DisplayPostsFromCurrentlyOpenedBlog()
        {
            string blog1Title = "Blog1";
            string blog2Title = "Blog2";
            string blog1PostTitle = "Blog1Post1";
            string blog2PostTitle = "Blog2Post1";
            string pageTitle = "PageWithBlogPostsWidget";

            Guid blog1Id = ServerOperations.Blogs().CreateBlog(blog1Title);
            ServerOperations.Blogs().CreatePublishedBlogPost(blog1PostTitle, blog1Id);

            Guid blog2Id = ServerOperations.Blogs().CreateBlog(blog2Title);
            ServerOperations.Blogs().CreatePublishedBlogPost(blog2PostTitle, blog2Id);

            Guid pageId = ServerOperations.Pages().CreatePage(pageTitle);

            try
            {
                var blogPostsWidget = this.CreateBlogPostsMvcWidget(blog2Id, ParentFilterMode.CurrentlyOpen, SelectionMode.AllItems, default(Guid));

                var controls = new List<System.Web.UI.Control>();
                controls.Add(blogPostsWidget);

                PageContentGenerator.AddControlsToPage(pageId, controls);

                string url = UrlPath.ResolveAbsoluteUrl("~/" + pageTitle + "/" + blog2Title);
                string responseContent = PageInvoker.ExecuteWebRequest(url);

                Assert.IsTrue(responseContent.Contains(blog2PostTitle), "The item with this title was NOT found " + blog2PostTitle);
                Assert.IsFalse(responseContent.Contains(blog1PostTitle), "The item with this title WAS found " + blog1PostTitle);
            }
            finally
            {
                ServerOperations.Pages().DeleteAllPages();
                ServerOperations.Blogs().DeleteAllBlogs();
            }
        }

        [Test]
        [Category(TestCategories.Blogs)]
        [Author(FeatherTeams.Team2)]
        [Description("Add Blog posts widget to a page and display selected posts only")]
        public void BlogPostsWidget_DisplaySelectedBlogPosts()
        {
            string blogTitle = "Blog";
            string blogPost1Title = "BlogPost1";
            string blogPost2Title = "BlogPost2";
            string pageTitle = "PageWithBlogPostsWidget";

            Guid blogId = ServerOperations.Blogs().CreateBlog(blogTitle);
            Guid post1Id = ServerOperations.Blogs().CreatePublishedBlogPost(blogPost1Title, blogId);

            Guid pageId = ServerOperations.Pages().CreatePage(pageTitle);

            try
            {
                var blogPostsWidget = this.CreateBlogPostsMvcWidget(default(Guid), ParentFilterMode.All, SelectionMode.SelectedItems, post1Id);

                var controls = new List<System.Web.UI.Control>();
                controls.Add(blogPostsWidget);

                PageContentGenerator.AddControlsToPage(pageId, controls);

                string url = UrlPath.ResolveAbsoluteUrl("~/" + pageTitle);
                string responseContent = PageInvoker.ExecuteWebRequest(url);

                Assert.IsTrue(responseContent.Contains(blogPost1Title), "The item with this title was NOT found " + blogPost1Title);
                Assert.IsFalse(responseContent.Contains(blogPost2Title), "The item with this title WAS found " + blogPost2Title);
            }
            finally
            {
                ServerOperations.Pages().DeleteAllPages();
                ServerOperations.Blogs().DeleteAllBlogs();
            }
        }

        private MvcWidgetProxy CreateBlogPostsMvcWidget(Guid selectedParentId, ParentFilterMode parentMode, SelectionMode selectionMode, Guid selectedItemId)
        {
            var mvcProxy = new MvcWidgetProxy();
            mvcProxy.ControllerName = typeof(BlogPostController).FullName;
            var controller = new BlogPostController();

            controller.Model.ParentFilterMode = parentMode;
            controller.Model.SelectionMode = selectionMode;

            if (selectedParentId != null)
            {
                controller.Model.SerializedSelectedParentsIds = "[" + selectedParentId.ToString() + "]";
            }

            if (selectedItemId != null)
            {
                controller.Model.SerializedSelectedItemsIds = "[" + selectedItemId.ToString() + "]";
            }

            mvcProxy.Settings = new ControllerSettings(controller);

            return mvcProxy;
        }
    }
}
