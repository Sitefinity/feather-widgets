using System;
using System.Collections.Generic;
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

namespace FeatherWidgets.TestIntegration.BlogsWidget
{
    /// <summary>
    /// This is a test class with test related to Blogs widget List settings.
    /// </summary>
    [TestFixture]
    public class BlogsWidgetListSettingsTests
    {
        [Test]
        [Category(TestCategories.Blogs)]
        [Author(FeatherTeams.SitefinityTeam7)]
        [Description("Add Blogs widget to a page and display blogs in pages.")]
        public void BlogsWidget_UsePaging_OneItemPerPage()
        {
            string blog1Title = "Blog1";
            string blog2Title = "Blog2";
            string pageTitle = "PageWithBlogsWidget";
            int firstPageIndex = 1;
            int secondPageIndex = 2;
            Guid pageId = Guid.Empty;
            Guid blog1Id = Guid.Empty;
            Guid blog2Id = Guid.Empty;

            try
            {
                blog1Id = ServerOperations.Blogs().CreateBlog(blog1Title);
                blog2Id = ServerOperations.Blogs().CreateBlog(blog2Title);
                pageId = ServerOperations.Pages().CreatePage(pageTitle);

                var blogsWidget = this.CreateBlogsMvcWidget(ListDisplayMode.Paging, itemsCount: 1);

                var controls = new List<System.Web.UI.Control>();
                controls.Add(blogsWidget);

                PageContentGenerator.AddControlsToPage(pageId, controls);

                string url = UrlPath.ResolveAbsoluteUrl("~/" + pageTitle + "/" + firstPageIndex);
                string responseContent = PageInvoker.ExecuteWebRequest(url);

                Assert.IsTrue(responseContent.Contains(blog2Title), "The item with this title was NOT found " + blog2Title);
                Assert.IsFalse(responseContent.Contains(blog1Title), "The item with this title WAS found " + blog1Title);

                url = UrlPath.ResolveAbsoluteUrl("~/" + pageTitle + "/" + secondPageIndex);
                responseContent = PageInvoker.ExecuteWebRequest(url);

                Assert.IsTrue(responseContent.Contains(blog1Title), "The item with this title was NOT found " + blog1Title);
                Assert.IsFalse(responseContent.Contains(blog2Title), "The item with this title WAS found " + blog2Title);
            }
            finally
            {
                if (pageId != Guid.Empty)
                {
                    ServerOperations.Pages().DeletePage(pageId);
                }

                if (blog1Id != Guid.Empty)
                {
                    ServerOperations.Blogs().DeleteBlog(blog1Id);
                }

                if (blog2Id != Guid.Empty)
                {
                    ServerOperations.Blogs().DeleteBlog(blog2Id);
                }
            }
        }

        [Test]
        [Category(TestCategories.Blogs)]
        [Author(FeatherTeams.SitefinityTeam7)]
        [Description("Add Blogs widget to a page and display blogs limited.")]
        public void BlogsWidget_UseLimit_OneItem()
        {
            string blog1Title = "Blog1";
            string blog2Title = "Blog2";
            string pageTitle = "PageWithBlogsWidget";
            Guid pageId = Guid.Empty;
            Guid blog1Id = Guid.Empty;
            Guid blog2Id = Guid.Empty;

            try
            {
                blog1Id = ServerOperations.Blogs().CreateBlog(blog1Title);
                blog2Id = ServerOperations.Blogs().CreateBlog(blog2Title);
                pageId = ServerOperations.Pages().CreatePage(pageTitle);

                var blogsWidget = this.CreateBlogsMvcWidget(ListDisplayMode.Limit, itemsCount: 1);

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
                if (pageId != Guid.Empty)
                {
                    ServerOperations.Pages().DeletePage(pageId);
                }

                if (blog1Id != Guid.Empty)
                {
                    ServerOperations.Blogs().DeleteBlog(blog1Id);
                }

                if (blog2Id != Guid.Empty)
                {
                    ServerOperations.Blogs().DeleteBlog(blog2Id);
                }
            }
        }

        [Test]
        [Category(TestCategories.Blogs)]
        [Author(FeatherTeams.SitefinityTeam7)]
        [Description("Add Blogs widget to a page and display blogs with no limit and paging.")]
        public void BlogsWidget_NoLimitAndPaging_AllItems()
        {
            string blog1Title = "Blog1";
            string blog2Title = "Blog2";
            string pageTitle = "PageWithBlogsWidget";
            Guid pageId = Guid.Empty;
            Guid blog1Id = Guid.Empty;
            Guid blog2Id = Guid.Empty;

            try
            {
                blog1Id = ServerOperations.Blogs().CreateBlog(blog1Title);
                blog2Id = ServerOperations.Blogs().CreateBlog(blog2Title);
                pageId = ServerOperations.Pages().CreatePage(pageTitle);

                var blogsWidget = this.CreateBlogsMvcWidget(ListDisplayMode.All, itemsCount: 1);

                var controls = new List<System.Web.UI.Control>();
                controls.Add(blogsWidget);

                PageContentGenerator.AddControlsToPage(pageId, controls);

                string url = UrlPath.ResolveAbsoluteUrl("~/" + pageTitle);
                string responseContent = PageInvoker.ExecuteWebRequest(url);

                Assert.IsTrue(responseContent.Contains(blog2Title), "The item with this title was NOT found " + blog2Title);
                Assert.IsTrue(responseContent.Contains(blog1Title), "The item with this title was NOT found " + blog1Title);
            }
            finally
            {
                if (pageId != Guid.Empty)
                {
                    ServerOperations.Pages().DeletePage(pageId);
                }

                if (blog1Id != Guid.Empty)
                {
                    ServerOperations.Blogs().DeleteBlog(blog1Id);
                }

                if (blog2Id != Guid.Empty)
                {
                    ServerOperations.Blogs().DeleteBlog(blog2Id);
                }
            }
        }

        [Test]
        [Category(TestCategories.Blogs)]
        [Author(FeatherTeams.SitefinityTeam7)]
        [Description("Add Blogs widget to a page and display blogs sorted by title from A to Z")]
        public void BlogsWidget_SortBlogs_ByTitle_AZ()
        {
            string blog1Title = "A_Blog";
            string blog2Title = "Z_Blog";
            string pageTitle = "PageWithBlogsWidget";
            string sortExpression = "Title ASC";
            int limitCount = 1;
            Guid pageId = Guid.Empty;
            Guid blog1Id = Guid.Empty;
            Guid blog2Id = Guid.Empty;

            try
            {
                blog1Id = ServerOperations.Blogs().CreateBlog(blog1Title);
                blog2Id = ServerOperations.Blogs().CreateBlog(blog2Title);
                pageId = ServerOperations.Pages().CreatePage(pageTitle);

                var blogsWidget = this.CreateBlogsMvcWidget(ListDisplayMode.Limit, limitCount, sortExpression);

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
                if (pageId != Guid.Empty)
                {
                    ServerOperations.Pages().DeletePage(pageId);
                }

                if (blog1Id != Guid.Empty)
                {
                    ServerOperations.Blogs().DeleteBlog(blog1Id);
                }

                if (blog2Id != Guid.Empty)
                {
                    ServerOperations.Blogs().DeleteBlog(blog2Id);
                }
            }
        }

        [Test]
        [Category(TestCategories.Blogs)]
        [Author(FeatherTeams.SitefinityTeam7)]
        [Description("Add Blogs widget to a page and display blogs sorted by title from A to Z")]
        public void BlogsWidget_SortBlogs_ByTitle_ZA()
        {
            string blog1Title = "A_Blog";
            string blog2Title = "Z_Blog";
            string blog3Title = "B_Blog";
            string pageTitle = "PageWithBlogsWidget";
            string sortExpression = "Title DESC";
            int limitCount = 1;
            Guid pageId = Guid.Empty;
            Guid blog1Id = Guid.Empty;
            Guid blog2Id = Guid.Empty;
            Guid blog3Id = Guid.Empty;

            try
            {
                blog1Id = ServerOperations.Blogs().CreateBlog(blog1Title);
                blog2Id = ServerOperations.Blogs().CreateBlog(blog2Title);
                blog3Id = ServerOperations.Blogs().CreateBlog(blog3Title);
                pageId = ServerOperations.Pages().CreatePage(pageTitle);

                var blogsWidget = this.CreateBlogsMvcWidget(ListDisplayMode.Limit, limitCount, sortExpression);

                var controls = new List<System.Web.UI.Control>();
                controls.Add(blogsWidget);

                PageContentGenerator.AddControlsToPage(pageId, controls);

                string url = UrlPath.ResolveAbsoluteUrl("~/" + pageTitle);
                string responseContent = PageInvoker.ExecuteWebRequest(url);

                Assert.IsTrue(responseContent.Contains(blog2Title), "The item with this title was NOT found " + blog2Title);
                Assert.IsFalse(responseContent.Contains(blog3Title), "The item with this title WAS found " + blog3Title);
            }
            finally
            {
                if (pageId != Guid.Empty)
                {
                    ServerOperations.Pages().DeletePage(pageId);
                }

                if (blog1Id != Guid.Empty)
                {
                    ServerOperations.Blogs().DeleteBlog(blog1Id);
                }

                if (blog2Id != Guid.Empty)
                {
                    ServerOperations.Blogs().DeleteBlog(blog2Id);
                }

                if (blog3Id != Guid.Empty)
                {
                    ServerOperations.Blogs().DeleteBlog(blog3Id);
                }
            }
        }

        private MvcWidgetProxy CreateBlogsMvcWidget(ListDisplayMode displayMode, int itemsCount, string sortExpression)
        {
            var mvcProxy = new MvcWidgetProxy();
            mvcProxy.ControllerName = typeof(BlogController).FullName;
            var controller = new BlogController();

            controller.Model.DisplayMode = displayMode;
            controller.Model.ItemsPerPage = itemsCount;
            controller.Model.LimitCount = itemsCount;
            controller.Model.SortExpression = sortExpression;

            mvcProxy.Settings = new ControllerSettings(controller);

            return mvcProxy;
        }

        private MvcWidgetProxy CreateBlogsMvcWidget(ListDisplayMode displayMode, int itemsCount)
        {
            string defaultSortExpr = "PublicationDate DESC";

            var mvcProxy = this.CreateBlogsMvcWidget(displayMode, itemsCount, defaultSortExpr);

            return mvcProxy;
        }
    }
}
