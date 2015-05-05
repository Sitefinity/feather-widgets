using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FeatherWidgets.TestUtilities.CommonOperations;
using Telerik.Sitefinity.TestUI.Arrangements.Framework;
using Telerik.Sitefinity.TestUI.Arrangements.Framework.Attributes;
using Telerik.Sitefinity.TestUtilities.CommonOperations;

namespace FeatherWidgets.TestUI.Arrangements
{
    /// <summary>
    /// This is a class with arrangement methods related to UI test AddBlogPostsWidgetToPageOpenSingleItemInExistingPage
    /// </summary>
    public class AddBlogPostsWidgetToPageOpenSingleItemInExistingPage : ITestArrangement
    {
        /// <summary>
        /// Server side set up.
        /// </summary>
        [ServerSetUp]
        public void SetUp()
        {
            var blogId = ServerOperations.Blogs().CreateBlog(BlogTitle);
            ServerOperations.Blogs().CreatePublishedBlogPost(PostTitle, blogId);

            var templateId = ServerOperations.Templates().GetTemplateIdByTitle(TemplateTitle);
            ServerOperations.Pages().CreatePage(PageTitle, templateId);

            var pageId = ServerOperations.Pages().CreatePage(DetailPageTitle, templateId);
            var pageNodeId = ServerOperations.Pages().GetPageNodeId(pageId);
            ServerOperationsFeather.Pages().AddBlogPostsWidgetToPage(pageNodeId, PlaceHolderId);
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

        private const string PageTitle = "TestPageWithBlogPostsWidget";
        private const string DetailPageTitle = "DetailPage";
        private const string PlaceHolderId = "Contentplaceholder1";
        private const string TemplateTitle = "Bootstrap.default";
        private const string BlogTitle = "TestBlog";
        private const string PostTitle = "post1";
    }
}
