using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FeatherWidgets.TestUtilities.CommonOperations;
using Telerik.Sitefinity.TestArrangementService.Attributes;
using Telerik.Sitefinity.TestUI.Arrangements.Framework;
using Telerik.Sitefinity.TestUtilities.CommonOperations;
using Telerik.Sitefinity.TestUtilities.TestConfig;

namespace FeatherWidgets.TestUI.Arrangements
{
    /// <summary>
    /// CreateSplitPageWithPersonalizedContentBlock arrangement class.
    /// </summary>
    public class CreateSplitPageWithPersonalizedContentBlock : TestArrangementBase
    {
        /// <summary>
        /// Server side set up. 
        /// </summary>
        [ServerSetUp]
        public void SetUp()
        {
            var blogId = ServerOperations.Blogs().CreateBlog(BlogTitle);
            ServerOperations.Blogs().CreatePublishedBlogPost(PostTitle, blogId);

            Guid templateId = Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.Templates().GetTemplateIdByTitle(PageTemplateName);

            this.pageId = ServerOperations.Multilingual().Pages().CreatePageMultilingual(this.pageId, PageName + "EN", false, "en", pageTemplateId: templateId);
           
            ServerOperationsFeather.Pages().AddContentBlockWidgetToPage(this.pageId, string.Empty, PlaceHolderId);
            ServerOperationsFeather.Pages().AddBlogPostsWidgetToPage(this.pageId, PlaceHolderId);
            ServerOperations.Personalization().CreateRoleSegment(AdministratorRole);     
        }

        /// <summary>
        /// Tears down.
        /// </summary>
        [ServerTearDown]
        public void TearDown()
        {
            ServerOperations.Pages().DeleteAllPages();
            ServerOperations.Personalization().DeleteAllSegments();
            ServerOperations.Blogs().DeleteAllBlogs();
        }

        private const string PageName = "ContentBlock";
        private const string PageTemplateName = "Bootstrap.default";
        private const string PlaceHolderId = "Contentplaceholder1";
        private const string BlogTitle = "TestBlog";
        private const string PostTitle = "post1";
        private const string AdministratorRole = "Administrators";
        private Guid pageId = Guid.NewGuid();
    }
}
