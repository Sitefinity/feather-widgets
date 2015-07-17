using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FeatherWidgets.TestUtilities.CommonOperations;
using Telerik.Sitefinity.Frontend.Blogs.Mvc.Models.BlogPost;
using Telerik.Sitefinity.TestUI.Arrangements.Framework;
using Telerik.Sitefinity.TestUI.Arrangements.Framework.Attributes;
using Telerik.Sitefinity.TestUtilities.CommonOperations;

namespace FeatherWidgets.TestUI.Arrangements
{
    /// <summary>
    /// This is a test class with arrangement methods related to UI test DragAndDropBlogsWidgetToPage
    /// </summary>
    public class DragAndDropBlogsWidgetToPage : ITestArrangement
    {
        /// <summary>
        /// Server side set up.
        /// </summary>
        [ServerSetUp]
        public void SetUp()
        {
            AuthenticationHelper.AuthenticateUser(AdminUserName, AdminPass, true);
            var templateId = ServerOperations.Templates().GetTemplateIdByTitle(TemplateTitle);
            var pageId = ServerOperations.Pages().CreatePage(DefaultPageTitle, templateId);
            var pageNodeId = ServerOperations.Pages().GetPageNodeId(pageId);

            ServerOperationsFeather.Pages().AddBlogPostsWidgetToPage(pageNodeId, PlaceHolderId, ParentFilterMode.CurrentlyOpen);

            var blogId = ServerOperations.Blogs().CreateBlog(BlogTitle, pageNodeId);
            ServerOperations.Blogs().CreatePublishedBlogPost(PostTitle, blogId);
            
            ServerOperations.Pages().CreatePage(PageTitle, templateId);          
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

        private const string AdminUserName = "admin";
        private const string AdminPass = "admin@2";
        private const string PageTitle = "PageWithBlogsWidget";
        private const string DefaultPageTitle = "BlogsDefaultPage";
        private const string PlaceHolderId = "Contentplaceholder1";
        private const string TemplateTitle = "Bootstrap.default";
        private const string BlogTitle = "TestBlog";
        private const string PostTitle = "post1";
    }
}
