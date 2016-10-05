using System;
using FeatherWidgets.TestUtilities.CommonOperations;
using Telerik.Sitefinity.TestArrangementService.Attributes;
using Telerik.Sitefinity.TestUtilities.CommonOperations;

namespace FeatherWidgets.TestUI.Arrangements
{
    /// <summary>
    /// VerifyUseLimitListSettingsOnFrontendPageForBlogPostsWidget arrangement class.
    /// </summary>
    public class VerifyUseLimitListSettingsOnFrontendPageForBlogPostsWidget : TestArrangementBase
    {
        /// <summary>
        /// Server side set up.
        /// </summary>
        [ServerSetUp]
        public void SetUp()
        {
            Guid pageId = ServerOperations.Pages().CreatePage(PageName);
            var blogId = ServerOperations.Blogs().CreateBlog(BlogTitle);

            for (int i = 1; i < 6; i++)
            {
                ServerOperations.Blogs().CreatePublishedBlogPost(PostTitle + i, blogId);
            }
            
            var blogId1 = ServerOperations.Blogs().CreateBlog(BlogTitle1);
            
            for (int j = 1; j < 3; j++)
            {
                ServerOperations.Blogs().CreatePublishedBlogPost(PostTitle1 + j, blogId1);
            }

            ServerOperationsFeather.Pages().AddBlogPostsWidgetToPage(pageId, "Body");
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

        private const string PageName = "BlogsPage";
        private const string BlogTitle = "TestBlog";
        private const string BlogTitle1 = "TestBlogNew";
        private const string PostTitle = "Post";
        private const string PostTitle1 = "PostNew";
    }
}
