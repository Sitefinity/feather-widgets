using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FeatherWidgets.TestUtilities.CommonOperations;
using Telerik.Sitefinity.TestArrangementService.Attributes;
using Telerik.Sitefinity.TestUI.Arrangements.Framework;
using Telerik.Sitefinity.TestUtilities.CommonOperations;

namespace FeatherWidgets.TestUI.Arrangements
{
    /// <summary>
    /// VerifyLoginWidgetWhenOpenItemDetailViewAndForgotPassword arragement.
    /// /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1726:UsePreferredTerms", MessageId = "Login")]
    public class VerifyLoginWidgetWhenOpenItemDetailViewAndForgotPassword : TestArrangementBase
    {
        /// <summary>
        /// Server side set up.
        /// </summary>
        [ServerSetUp]
        public void SetUp()
        {
            AuthenticationHelper.AuthenticateUser(this.AdminEmail, this.AdminPass, true);
            var blogId = ServerOperations.Blogs().CreateBlog(BlogTitle);
            ServerOperations.Blogs().CreatePublishedBlogPost(PostTitle, blogId);

            var templateId = ServerOperations.Templates().GetTemplateIdByTitle(TemplateTitle);
            Guid pageId = Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.Pages().CreatePage(PageTitle, templateId);
            pageId = ServerOperations.Pages().GetPageNodeId(pageId);
            ServerOperationsFeather.Pages().AddBlogPostsWidgetToPage(pageId, PlaceHolderId);
            ServerOperationsFeather.Pages().AddLoginFormWidgetToPage(pageId, PlaceHolderId);
            ServerOperationsFeather.Pages().AddLoginStatusWidgetToPage(pageId, PlaceHolderId);
            ServerOperations.Configuration().EnableUserPasswordReset();
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
        private const string PlaceHolderId = "Contentplaceholder1";
        private const string TemplateTitle = "Bootstrap.default";
        private const string BlogTitle = "TestBlog";
        private const string PostTitle = "post1";
    }
}
