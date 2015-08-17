using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FeatherWidgets.TestUtilities.CommonOperations;
using MbUnit.Framework;
using Telerik.Sitefinity.Frontend.Blogs.Mvc.Controllers;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers;
using Telerik.Sitefinity.Frontend.Mvc.Models;
using Telerik.Sitefinity.Frontend.TestUtilities;
using Telerik.Sitefinity.Mvc.Proxy;
using Telerik.Sitefinity.TestIntegration.Data.Content;
using Telerik.Sitefinity.TestUtilities.CommonOperations;
using Telerik.Sitefinity.Web;

namespace FeatherWidgets.TestIntegration.BlogPosts
{
    /// <summary>
    /// A class with tests related to blog posts widget list settings
    /// </summary>
    [TestFixture]
    [Description("This is a class with Blog posts widget list settings tests.")]
    public class BlogPostsWidgetListSettingsTests
    {
        [Test]
        [Category(TestCategories.Blogs)]
        [Author(FeatherTeams.FeatherTeam)]
        [Description("Add Blog posts widget to a page and display posts in paging - one item per page")]
        public void BlogPostsWidget_UsePaging_OneItemPerPage()
        {
            string blog1Title = "Blog1";
            string post1Title = "Blog1Post1";
            string post2Title = "Blog1Post2";
            string pageTitle = "PageWithBlogPostsWidget";
            int page1Index = 1;
            int page2Index = 2;

            try
            {
                Guid blog1Id = ServerOperations.Blogs().CreateBlog(blog1Title);
                ServerOperations.Blogs().CreatePublishedBlogPost(post1Title, blog1Id);
                ServerOperations.Blogs().CreatePublishedBlogPost(post2Title, blog1Id);

                Guid pageId = ServerOperations.Pages().CreatePage(pageTitle);

                var blogPostsWidget = this.CreateBlogPostsMvcWidget(ListDisplayMode.Paging, itemsPerPage: 1);

                var controls = new List<System.Web.UI.Control>();
                controls.Add(blogPostsWidget);

                PageContentGenerator.AddControlsToPage(pageId, controls);

                string url = UrlPath.ResolveAbsoluteUrl("~/" + pageTitle + "/" + page1Index);
                string responseContent = PageInvoker.ExecuteWebRequest(url);

                Assert.IsTrue(responseContent.Contains(post2Title), "The item with this title was NOT found " + post2Title);
                Assert.IsFalse(responseContent.Contains(post1Title), "The item with this title WAS found " + post1Title);

                url = UrlPath.ResolveAbsoluteUrl("~/" + pageTitle + "/" + page2Index);
                responseContent = PageInvoker.ExecuteWebRequest(url);

                Assert.IsTrue(responseContent.Contains(post1Title), "The item with this title was NOT found " + post1Title);
                Assert.IsFalse(responseContent.Contains(post2Title), "The item with this title WAS found " + post2Title);
            }
            finally
            {
                ServerOperations.Pages().DeleteAllPages();
                ServerOperations.Blogs().DeleteAllBlogs();
            }
        }

        [Test]
        [Category(TestCategories.Blogs)]
        [Author(FeatherTeams.FeatherTeam)]
        [Description("Add Blog posts widget to a page and display limited posts.")]
        public void BlogPostsWidget_UseLimit_OneItem()
        {
            string blog1Title = "Blog1";
            string post1Title = "Blog1Post1";
            string post2Title = "Blog1Post2";
            string pageTitle = "PageWithBlogPostsWidget";

            try
            {
                Guid blog1Id = ServerOperations.Blogs().CreateBlog(blog1Title);
                ServerOperations.Blogs().CreatePublishedBlogPost(post1Title, blog1Id);
                ServerOperations.Blogs().CreatePublishedBlogPost(post2Title, blog1Id);

                Guid pageId = ServerOperations.Pages().CreatePage(pageTitle);

                var blogPostsWidget = this.CreateBlogPostsMvcWidget(ListDisplayMode.Limit, itemsPerPage: 1);

                var controls = new List<System.Web.UI.Control>();
                controls.Add(blogPostsWidget);

                PageContentGenerator.AddControlsToPage(pageId, controls);

                string url = UrlPath.ResolveAbsoluteUrl("~/" + pageTitle);
                string responseContent = PageInvoker.ExecuteWebRequest(url);

                Assert.IsTrue(responseContent.Contains(post2Title), "The item with this title was NOT found " + post2Title);
                Assert.IsFalse(responseContent.Contains(post1Title), "The item with this title WAS found " + post1Title);
            }
            finally
            {
                ServerOperations.Pages().DeleteAllPages();
                ServerOperations.Blogs().DeleteAllBlogs();
            }
        }

        [Test]
        [Category(TestCategories.Blogs)]
        [Author(FeatherTeams.FeatherTeam)]
        [Description("Add Blog posts widget to a page and display posts with no limit and paging.")]
        public void BlogPostsWidget_NoLimitAndPaging()
        {
            string blog1Title = "Blog1";
            string post1Title = "Blog1Post1";
            string post2Title = "Blog1Post2";
            string pageTitle = "PageWithBlogPostsWidget";

            try
            {
                Guid blog1Id = ServerOperations.Blogs().CreateBlog(blog1Title);
                ServerOperations.Blogs().CreatePublishedBlogPost(post1Title, blog1Id);
                ServerOperations.Blogs().CreatePublishedBlogPost(post2Title, blog1Id);

                Guid pageId = ServerOperations.Pages().CreatePage(pageTitle);

                var blogPostsWidget = this.CreateBlogPostsMvcWidget(ListDisplayMode.All, itemsPerPage: 1);

                var controls = new List<System.Web.UI.Control>();
                controls.Add(blogPostsWidget);

                PageContentGenerator.AddControlsToPage(pageId, controls);

                string url = UrlPath.ResolveAbsoluteUrl("~/" + pageTitle);
                string responseContent = PageInvoker.ExecuteWebRequest(url);

                Assert.IsTrue(responseContent.Contains(post2Title), "The item with this title was NOT found " + post2Title);
                Assert.IsTrue(responseContent.Contains(post1Title), "The item with this title was NOT found " + post1Title);
            }
            finally
            {
                ServerOperations.Pages().DeleteAllPages();
                ServerOperations.Blogs().DeleteAllBlogs();
            }
        }

        private MvcWidgetProxy CreateBlogPostsMvcWidget(ListDisplayMode displayMode, int itemsPerPage, string sortExpression)
        {
            var mvcProxy = new MvcWidgetProxy();
            mvcProxy.ControllerName = typeof(BlogPostController).FullName;
            var controller = new BlogPostController();

            controller.Model.DisplayMode = displayMode;
            controller.Model.ItemsPerPage = itemsPerPage;
            controller.Model.SortExpression = sortExpression;

            mvcProxy.Settings = new ControllerSettings(controller);

            return mvcProxy;
        }

        private MvcWidgetProxy CreateBlogPostsMvcWidget(ListDisplayMode displayMode, int itemsPerPage)
        {
            string defaultSortExpr = "PublicationDate DESC";

            var mvcProxy = this.CreateBlogPostsMvcWidget(displayMode, itemsPerPage, defaultSortExpr);

            return mvcProxy;
        }
    }
}
