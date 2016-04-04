using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FeatherWidgets.TestUtilities.CommonOperations;
using MbUnit.Framework;
using Telerik.Sitefinity.Frontend.Blogs.Mvc.Controllers;
using Telerik.Sitefinity.Frontend.Blogs.Mvc.Models.Blog;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers;
using Telerik.Sitefinity.Frontend.Mvc.Models;
using Telerik.Sitefinity.Frontend.TestUtilities;
using Telerik.Sitefinity.Mvc.Proxy;
using Telerik.Sitefinity.TestIntegration.Data.Content;
using Telerik.Sitefinity.TestUtilities.CommonOperations;
using Telerik.Sitefinity.Web;

namespace FeatherWidgets.TestIntegration.BlogsWidget
{
    /// <summary>
    /// This is a test class with test related to Blogs widget.
    /// </summary>
    [TestFixture]
    public class BlogsWidgetTests
    {       
        [Test]
        [Category(TestCategories.Blogs)]
        [Author(FeatherTeams.FeatherTeam)]
        [Description("Add Blogs widget to a page and display only selected blogs.")]
        public void BlogsWidget_DisplaySelectedBlogs()
        {
            string blog1Title = "Blog1";
            string blog2Title = "Blog2";
            string pageTitle = "PageWithBlogsWidget";

            try
            {
                Guid blog1Id = ServerOperations.Blogs().CreateBlog(blog1Title);
                
                ServerOperations.Blogs().CreateBlog(blog2Title);

                Guid pageId = ServerOperations.Pages().CreatePage(pageTitle);

                var blogsWidget = this.CreateBlogsMvcWidgetSelectionMode(SelectionMode.SelectedItems, blog1Id);

                var controls = new List<System.Web.UI.Control>();
                controls.Add(blogsWidget);

                PageContentGenerator.AddControlsToPage(pageId, controls);

                string url = UrlPath.ResolveAbsoluteUrl("~/" + pageTitle);
                string responseContent = PageInvoker.ExecuteWebRequest(url);

                Assert.IsTrue(responseContent.Contains(blog1Title), "The item with this title was NOT found " + blog1Title);
                Assert.IsFalse(responseContent.Contains(blog2Title), "The item with this title WAS found " + blog2Title);
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
        [Description("Add Blogs widget to a page and display only blogs with more than 0 posts.")]
        public void BlogsWidget_DisplayBlogsWithMoreThan_0_Posts()
        {
            string blog1Title = "Blog1";
            string blog2Title = "Blog2";
            string pageTitle = "PageWithBlogsWidget";
            string blog1PostTitle = "Blog1_PublishedPost";

            try
            {
                Guid blog1Id = ServerOperations.Blogs().CreateBlog(blog1Title);
                ServerOperations.Blogs().CreatePublishedBlogPost(blog1PostTitle, blog1Id);

                ServerOperations.Blogs().CreateBlog(blog2Title);

                Guid pageId = ServerOperations.Pages().CreatePage(pageTitle);

                var blogsWidget = this.CreateBlogsMvcWidgetFilteredSelectionMode(FilteredSelectionMode.MinPostsCount, minPostsCount: 0, maxPostsAge: 1);

                var controls = new List<System.Web.UI.Control>();
                controls.Add(blogsWidget);

                PageContentGenerator.AddControlsToPage(pageId, controls);

                string url = UrlPath.ResolveAbsoluteUrl("~/" + pageTitle);
                string responseContent = PageInvoker.ExecuteWebRequest(url);

                Assert.IsTrue(responseContent.Contains(blog1Title), "The item with this title was NOT found " + blog1Title);
                Assert.IsFalse(responseContent.Contains(blog2Title), "The item with this title WAS found " + blog2Title);
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
        [Description("Add Blogs widget to a page and display only blogs with more than 1 post.")]
        public void BlogsWidget_DisplayBlogsWithMoreThan_1_Post()
        {
            string blog1Title = "Blog1";
            string blog2Title = "Blog2";
            string pageTitle = "PageWithBlogsWidget";
            string blog1PostTitle = "Blog1_PublishedPost";
            string blog2Post1Title = "Blog2_PublishedPost1";
            string blog2Post2Title = "Blog2_PublishedPost2";

            try
            {
                Guid blog1Id = ServerOperations.Blogs().CreateBlog(blog1Title);
                ServerOperations.Blogs().CreatePublishedBlogPost(blog1PostTitle, blog1Id);

                Guid blog2Id = ServerOperations.Blogs().CreateBlog(blog2Title);
                ServerOperations.Blogs().CreatePublishedBlogPost(blog2Post1Title, blog2Id);
                ServerOperations.Blogs().CreatePublishedBlogPost(blog2Post2Title, blog2Id);

                Guid pageId = ServerOperations.Pages().CreatePage(pageTitle);

                var blogsWidget = this.CreateBlogsMvcWidgetFilteredSelectionMode(FilteredSelectionMode.MinPostsCount, minPostsCount: 1, maxPostsAge: 1);

                var controls = new List<System.Web.UI.Control>();
                controls.Add(blogsWidget);

                PageContentGenerator.AddControlsToPage(pageId, controls);

                string url = UrlPath.ResolveAbsoluteUrl("~/" + pageTitle);
                string responseContent = PageInvoker.ExecuteWebRequest(url);

                Assert.IsTrue(responseContent.Contains(blog2Title), "The item with this title was NOT found " + blog2Title);
                Assert.IsFalse(responseContent.Contains(blog1Title), "The item with this title WAS found " + blog1Title);
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
        [Description("Add Blogs widget to a page and display only blogs that have posts not older than 1 month.")]
        public void BlogsWidget_DisplayBlogsThatHavePostsNotOlderThan_1_Month()
        {
            string blog1Title = "Blog1";
            string blog2Title = "Blog2";
            string pageTitle = "PageWithBlogsWidget";
            string blog1PostTitle = "Blog1_PublishedPost_Past";
            string blog2Post1Title = "Blog2_PublishedPost";

            try
            {
                DateTime publicationDate = DateTime.UtcNow.AddMonths(-1);

                Guid blog1Id = ServerOperations.Blogs().CreateBlog(blog1Title);
                ServerOperationsFeather.Blogs().CreateBlogPostSpecificPublicationDate(blog1PostTitle, blog1Id, publicationDate);

                Guid blog2Id = ServerOperations.Blogs().CreateBlog(blog2Title);
                ServerOperations.Blogs().CreatePublishedBlogPost(blog2Post1Title, blog2Id);

                Guid pageId = ServerOperations.Pages().CreatePage(pageTitle);

                var blogsWidget = this.CreateBlogsMvcWidgetFilteredSelectionMode(FilteredSelectionMode.MaxPostsAge, minPostsCount: 0, maxPostsAge: 1);

                var controls = new List<System.Web.UI.Control>();
                controls.Add(blogsWidget);

                PageContentGenerator.AddControlsToPage(pageId, controls);

                string url = UrlPath.ResolveAbsoluteUrl("~/" + pageTitle);
                string responseContent = PageInvoker.ExecuteWebRequest(url);

                Assert.IsTrue(responseContent.Contains(blog2Title), "The item with this title was NOT found " + blog2Title);
                Assert.IsFalse(responseContent.Contains(blog1Title), "The item with this title WAS found " + blog1Title);
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
        [Description("Add Blogs widget to a page and display only blogs that have posts not older than 12 months.")]
        public void BlogsWidget_DisplayBlogsThatHavePostsNotOlderThan_1_Year()
        {
            string blog1Title = "Blog1";
            string blog2Title = "Blog2";
            string pageTitle = "PageWithBlogsWidget";
            string blog1PostTitle = "Blog1_PublishedPost_Past";
            string blog2Post1Title = "Blog2_PublishedPost";

            try
            {
                DateTime publicationDate = DateTime.UtcNow.AddYears(-1);

                Guid blog1Id = ServerOperations.Blogs().CreateBlog(blog1Title);
                ServerOperationsFeather.Blogs().CreateBlogPostSpecificPublicationDate(blog1PostTitle, blog1Id, publicationDate);

                Guid blog2Id = ServerOperations.Blogs().CreateBlog(blog2Title);
                ServerOperations.Blogs().CreatePublishedBlogPost(blog2Post1Title, blog2Id);

                Guid pageId = ServerOperations.Pages().CreatePage(pageTitle);

                var blogsWidget = this.CreateBlogsMvcWidgetFilteredSelectionMode(FilteredSelectionMode.MaxPostsAge, minPostsCount: 0, maxPostsAge: 12);

                var controls = new List<System.Web.UI.Control>();
                controls.Add(blogsWidget);

                PageContentGenerator.AddControlsToPage(pageId, controls);

                string url = UrlPath.ResolveAbsoluteUrl("~/" + pageTitle);
                string responseContent = PageInvoker.ExecuteWebRequest(url);

                Assert.IsTrue(responseContent.Contains(blog2Title), "The item with this title was NOT found " + blog2Title);
                Assert.IsFalse(responseContent.Contains(blog1Title), "The item with this title WAS found " + blog1Title);
            }
            finally
            {
                ServerOperations.Pages().DeleteAllPages();
                ServerOperations.Blogs().DeleteAllBlogs();
            }
        }

        private MvcWidgetProxy CreateBlogsMvcWidgetSelectionMode(SelectionMode selectionMode, Guid selectedItemId)
        {
            var mvcProxy = new MvcWidgetProxy();
            mvcProxy.ControllerName = typeof(BlogController).FullName;
            var controller = new BlogController();

            controller.Model.SelectionMode = selectionMode;

            if (selectedItemId != null)
            {
                controller.Model.SerializedSelectedItemsIds = "[" + selectedItemId.ToString() + "]";
            }

            mvcProxy.Settings = new ControllerSettings(controller);

            return mvcProxy;
        }

        private MvcWidgetProxy CreateBlogsMvcWidgetFilteredSelectionMode(FilteredSelectionMode mode, int minPostsCount, int maxPostsAge)
        {
            var mvcProxy = new MvcWidgetProxy();
            mvcProxy.ControllerName = typeof(BlogController).FullName;
            var controller = new BlogController();

            controller.Model.SelectionMode = SelectionMode.FilteredItems;
            controller.Model.FilteredSelectionMode = mode;
            controller.Model.MinPostsCount = minPostsCount;
            controller.Model.MaxPostsAge = maxPostsAge; 

            mvcProxy.Settings = new ControllerSettings(controller);

            return mvcProxy;
        }
    }
}
