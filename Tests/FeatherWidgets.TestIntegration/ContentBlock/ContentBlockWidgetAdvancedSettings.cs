using System.Linq;
using ContentBlock.Mvc.Controllers;
using FeatherWidgets.TestUtilities.CommonOperations;
using MbUnit.Framework;
using Telerik.Sitefinity;
using Telerik.Sitefinity.Mvc.Proxy;
using Telerik.Sitefinity.TestUtilities;
using Telerik.Sitefinity.Web;

namespace FeatherWidgets.TestIntegration.ContentBlock
{
    /// <summary>
    /// This is a class with Content block tests - Advanced settings.
    /// </summary>
    [TestFixture]
    [Description("This is a class with Content block tests for Advanced settings.")]
    public class ContentBlockWidgetAdvancedSettings
    {
        /// <summary>
        /// Set up method
        /// </summary>
        [SetUp]
        public void Setup()
        {
            this.pageOperations = new PagesOperations();
            Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.ContentBlocks().CreateContentBlock(ContentBlockTitle, ContentBlockContent);
        }

        /// <summary>
        /// Tears down.
        /// </summary>
        [TearDown]
        public void TearDown()
        {
            this.pageOperations.DeletePages();
            Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.ContentBlocks().DeleteAllContentBlocks();
        }

        [Test]
        [Category(TestCategories.ContentBlock)]
        [Author(TestAuthor.Team2)]
        [Description("Verifies that set shared content id to content block widget and verify on the frontend.")]
        public void ContentBlockWidget_AdvancedSettings()
        {
            string testName = "ContentBlockWidgetAdvancedSettings";
            string pageNamePrefix = testName + "ContentBlockPage";
            string pageTitlePrefix = testName + "Content Block";
            string urlNamePrefix = testName + "content-block";
            int pageIndex = 1;
            string url = UrlPath.ResolveAbsoluteUrl("~/" + urlNamePrefix + pageIndex);

            var content = App.WorkWith().ContentItems()
                           .Published()
                           .Where(c => c.Title == ContentBlockTitle)
                           .Get().Single();

            var mvcProxy = new MvcControllerProxy();
            mvcProxy.ControllerName = typeof(ContentBlockController).FullName;
            var contentBlockController = new ContentBlockController();
            contentBlockController.SharedContentID = content.Id;
            mvcProxy.Settings = new ControllerSettings(contentBlockController);

            this.pageOperations.CreatePageWithControl(mvcProxy, pageNamePrefix, pageTitlePrefix, urlNamePrefix, pageIndex);

            string responseContent = PageInvoker.ExecuteWebRequest(url);

            Assert.IsTrue(responseContent.Contains(ContentBlockContent), "The content block with this title was not found!");
        }

        #region Fields and constants

        private PagesOperations pageOperations;
        private const string ContentBlockTitle = "This is content block title";
        private const string ContentBlockContent = "This is content block content";

        #endregion
    }
}
