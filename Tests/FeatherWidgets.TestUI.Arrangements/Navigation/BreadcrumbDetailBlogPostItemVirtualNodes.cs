using System;
using FeatherWidgets.TestUtilities.CommonOperations;
using Telerik.Sitefinity.TestArrangementService.Attributes;
using Telerik.Sitefinity.TestUtilities.CommonOperations;

namespace FeatherWidgets.TestUI.Arrangements
{
    /// <summary>
    /// This is a class with arrangement methods related to UI test Breadcrumb_DetailBlogPostItem_VirtualNodes
    /// </summary>
    public class BreadcrumbDetailBlogPostItemVirtualNodes : TestArrangementBase
    {
        /// <summary>
        /// Server side set up.
        /// </summary>
        [ServerSetUp]
        public void SetUp()
        {
            Guid pageId = ServerOperations.Pages().CreatePage(PageName);
            var blogId = ServerOperations.Blogs().CreateBlog(BlogTitle);
            ServerOperations.Blogs().CreatePublishedBlogPost(PostTitle, blogId);

            ServerOperationsFeather.Pages().AddBlogPostsWidgetToPage(pageId, PlaceHolderId);
            ServerOperationsFeather.Pages().AddBreadcrumbWidgetToPage(pageId, PlaceHolderId);
        }

        /// <summary>
        /// Tears down.
        /// </summary>
        [ServerTearDown]
        public void TearDown()
        {
            ServerOperations.Pages().DeleteAllPages();
            ServerOperations.Blogs().DeleteAllBlogs();
        }

        private const string PageName = "TestPageWithBlogPostsWidget";
        private const string PlaceHolderId = "Body";
        private const string BlogTitle = "TestBlog";
        private const string PostTitle = "post1";
    }
}
