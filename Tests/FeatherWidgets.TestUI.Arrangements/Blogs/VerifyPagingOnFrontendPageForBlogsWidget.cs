using System;
using FeatherWidgets.TestUtilities.CommonOperations;
using Telerik.Sitefinity.TestArrangementService.Attributes;
using Telerik.Sitefinity.TestUtilities.CommonOperations;

namespace FeatherWidgets.TestUI.Arrangements
{
    /// <summary>
    /// VerifyPagingOnFrontendPageForBlogsWidget arrangement class.
    /// </summary>
    public class VerifyPagingOnFrontendPageForBlogsWidget : TestArrangementBase
    {
        /// <summary>
        /// Server side set up.
        /// </summary>
        [ServerSetUp]
        public void SetUp()
        {
            Guid pageId = ServerOperations.Pages().CreatePage(PageName);

            for (int i = 1; i < 6; i++)
            {
                ServerOperations.Blogs().CreateBlog(BlogTitle + i, null, null, null);
            }
            
            ServerOperationsFeather.Pages().AddBlogsWidgetToPage(pageId, "Body");
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
    }
}
