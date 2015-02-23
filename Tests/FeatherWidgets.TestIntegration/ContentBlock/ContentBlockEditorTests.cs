using FeatherWidgets.TestUtilities.CommonOperations;
using MbUnit.Framework;
using Telerik.Sitefinity.Frontend.ContentBlock.Mvc.Controllers;
using Telerik.Sitefinity.Mvc.Proxy;
using Telerik.Sitefinity.Web;

namespace FeatherWidgets.TestIntegration.ContentBlock
{
    /// <summary>
    /// This is a class with Content block tests - Editor.
    /// </summary>
    [TestFixture]
    [Description("This is a class with Content block tests for editor.")]
    public class ContentBlockEditorTests
    {
        /// <summary>
        /// Set up method
        /// </summary>
        [SetUp]
        public void Setup()
        {
            this.pageOperations = new PagesOperations();
        }

        /// <summary>
        /// Tears down.
        /// </summary>
        [TearDown]
        public void TearDown()
        {
            this.pageOperations.DeletePages();
        }

        [Test]
        [Category(TestCategories.ContentBlock)]
        [Author(FeatherTeams.Team2)]
        public void ContentBlockWidget_AddTableToContentBlock()
        {
            string pageNamePrefix = "ContentBlockPage";
            string pageTitlePrefix = "Content Block";
            string urlNamePrefix = "content-block";
            int pageIndex = 1;
            string contentBlockContent = "<table><tbody><tr><td>a</td><td>b</td></tr><tr><td>c</td><td>d</td></tr></tbody></table>";
            string url = UrlPath.ResolveAbsoluteUrl("~/" + urlNamePrefix + pageIndex);

            var mvcProxy = new MvcControllerProxy();
            mvcProxy.ControllerName = typeof(ContentBlockController).FullName;
            var contentBlockController = new ContentBlockController();
            contentBlockController.Content = contentBlockContent;
            mvcProxy.Settings = new ControllerSettings(contentBlockController);

            this.pageOperations.CreatePageWithControl(mvcProxy, pageNamePrefix, pageTitlePrefix, urlNamePrefix, pageIndex);
            string responseContent = PageInvoker.ExecuteWebRequest(url);

            Assert.IsTrue(responseContent.Contains(contentBlockContent), "Table was not found!");
        }

        [Test]
        [Category(TestCategories.ContentBlock)]
        [Author(FeatherTeams.Team2)]
        public void ContentBlockWidget_InsertHyperlinkToContentBlock()
        {
            string pageNamePrefix = "ContentBlockPage";
            string pageTitlePrefix = "Content Block";
            string urlNamePrefix = "content-block";
            int pageIndex = 1;
            string contentBlockContent = "<a href=\"http://wheather.com\" title=\"wheather\">Wheather site</a>";
            string url = UrlPath.ResolveAbsoluteUrl("~/" + urlNamePrefix + pageIndex);

            var mvcProxy = new MvcControllerProxy();
            mvcProxy.ControllerName = typeof(ContentBlockController).FullName;
            var contentBlockController = new ContentBlockController();
            contentBlockController.Content = contentBlockContent;
            mvcProxy.Settings = new ControllerSettings(contentBlockController);

            this.pageOperations.CreatePageWithControl(mvcProxy, pageNamePrefix, pageTitlePrefix, urlNamePrefix, pageIndex);
            string responseContent = PageInvoker.ExecuteWebRequest(url);

            Assert.IsTrue(responseContent.Contains(contentBlockContent), "Hyperlink was not found!");
        }

        [Test]
        [Category(TestCategories.ContentBlock)]
        [Author(FeatherTeams.Team2)]
        public void ContentBlockWidget_FormatContentViaBoldFunctionalityToContentBlock()
        {
            string pageNamePrefix = "ContentBlockPage";
            string pageTitlePrefix = "Content Block";
            string urlNamePrefix = "content-block";
            int pageIndex = 1;
            string contentBlockContent = "<div><strong>Bold text</strong></div>";
            string url = UrlPath.ResolveAbsoluteUrl("~/" + urlNamePrefix + pageIndex);

            var mvcProxy = new MvcControllerProxy();
            mvcProxy.ControllerName = typeof(ContentBlockController).FullName;
            var contentBlockController = new ContentBlockController();
            contentBlockController.Content = contentBlockContent;
            mvcProxy.Settings = new ControllerSettings(contentBlockController);

            this.pageOperations.CreatePageWithControl(mvcProxy, pageNamePrefix, pageTitlePrefix, urlNamePrefix, pageIndex);
            string responseContent = PageInvoker.ExecuteWebRequest(url);

            Assert.IsTrue(responseContent.Contains(contentBlockContent), "Content was not bold!");
        }

        [Test]
        [Category(TestCategories.ContentBlock)]
        [Author(FeatherTeams.Team2)]
        public void ContentBlockWidget_FormatContentViaItalicFunctionalityToContentBlock()
        {
            string pageNamePrefix = "ContentBlockPage";
            string pageTitlePrefix = "Content Block";
            string urlNamePrefix = "content-block";
            int pageIndex = 1;
            string contentBlockContent = "<em>Italic text</em>";
            string url = UrlPath.ResolveAbsoluteUrl("~/" + urlNamePrefix + pageIndex);

            var mvcProxy = new MvcControllerProxy();
            mvcProxy.ControllerName = typeof(ContentBlockController).FullName;
            var contentBlockController = new ContentBlockController();
            contentBlockController.Content = contentBlockContent;
            mvcProxy.Settings = new ControllerSettings(contentBlockController);

            this.pageOperations.CreatePageWithControl(mvcProxy, pageNamePrefix, pageTitlePrefix, urlNamePrefix, pageIndex);
            string responseContent = PageInvoker.ExecuteWebRequest(url);

            Assert.IsTrue(responseContent.Contains(contentBlockContent), "Content was not italic!");
        }

        [Test]
        [Category(TestCategories.ContentBlock)]
        [Author(FeatherTeams.Team2)]
        public void ContentBlockWidget_FormatContentViaUnderlineFunctionalityToContentBlock()
        {
            string pageNamePrefix = "ContentBlockPage";
            string pageTitlePrefix = "Content Block";
            string urlNamePrefix = "content-block";
            int pageIndex = 1;
            string contentBlockContent = "<span style=\"text-decoration:underline;\">Underline text</span>";
            string url = UrlPath.ResolveAbsoluteUrl("~/" + urlNamePrefix + pageIndex);

            var mvcProxy = new MvcControllerProxy();
            mvcProxy.ControllerName = typeof(ContentBlockController).FullName;
            var contentBlockController = new ContentBlockController();
            contentBlockController.Content = contentBlockContent;
            mvcProxy.Settings = new ControllerSettings(contentBlockController);

            this.pageOperations.CreatePageWithControl(mvcProxy, pageNamePrefix, pageTitlePrefix, urlNamePrefix, pageIndex);
            string responseContent = PageInvoker.ExecuteWebRequest(url);

            Assert.IsTrue(responseContent.Contains(contentBlockContent), "Content was not underlined!");
        }

        [Test]
        [Category(TestCategories.ContentBlock)]
        [Author(FeatherTeams.Team2)]
        public void ContentBlockWidget_FormatContentViaAlignTextLeftFunctionalityToContentBlock()
        {
            string pageNamePrefix = "ContentBlockPage";
            string pageTitlePrefix = "Content Block";
            string urlNamePrefix = "content-block";
            int pageIndex = 1;
            string contentBlockContent = "<div style=\"text-align:left;\">Align text left</div>";
            string url = UrlPath.ResolveAbsoluteUrl("~/" + urlNamePrefix + pageIndex);

            var mvcProxy = new MvcControllerProxy();
            mvcProxy.ControllerName = typeof(ContentBlockController).FullName;
            var contentBlockController = new ContentBlockController();
            contentBlockController.Content = contentBlockContent;
            mvcProxy.Settings = new ControllerSettings(contentBlockController);

            this.pageOperations.CreatePageWithControl(mvcProxy, pageNamePrefix, pageTitlePrefix, urlNamePrefix, pageIndex);
            string responseContent = PageInvoker.ExecuteWebRequest(url);

            Assert.IsTrue(responseContent.Contains(contentBlockContent), "Content was not aligned left!");
        }

        [Test]
        [Category(TestCategories.ContentBlock)]
        [Author(FeatherTeams.Team2)]
        public void ContentBlockWidget_FormatContentViaCenterTextFunctionalityToContentBlock()
        {
            string pageNamePrefix = "ContentBlockPage";
            string pageTitlePrefix = "Content Block";
            string urlNamePrefix = "content-block";
            int pageIndex = 1;
            string contentBlockContent = "<div style=\"text-align:center;\">Center text</div>";
            string url = UrlPath.ResolveAbsoluteUrl("~/" + urlNamePrefix + pageIndex);

            var mvcProxy = new MvcControllerProxy();
            mvcProxy.ControllerName = typeof(ContentBlockController).FullName;
            var contentBlockController = new ContentBlockController();
            contentBlockController.Content = contentBlockContent;
            mvcProxy.Settings = new ControllerSettings(contentBlockController);

            this.pageOperations.CreatePageWithControl(mvcProxy, pageNamePrefix, pageTitlePrefix, urlNamePrefix, pageIndex);
            string responseContent = PageInvoker.ExecuteWebRequest(url);

            Assert.IsTrue(responseContent.Contains(contentBlockContent), "Content was not centered!");
        }

        [Test]
        [Category(TestCategories.ContentBlock)]
        [Author(FeatherTeams.Team2)]
        public void ContentBlockWidget_FormatContentViaAlignTextRightFunctionalityToContentBlock()
        {
            string pageNamePrefix = "ContentBlockPage";
            string pageTitlePrefix = "Content Block";
            string urlNamePrefix = "content-block";
            int pageIndex = 1;
            string contentBlockContent = "<div style=\"text-align:right;\">Align text right</div>";
            string url = UrlPath.ResolveAbsoluteUrl("~/" + urlNamePrefix + pageIndex);

            var mvcProxy = new MvcControllerProxy();
            mvcProxy.ControllerName = typeof(ContentBlockController).FullName;
            var contentBlockController = new ContentBlockController();
            contentBlockController.Content = contentBlockContent;
            mvcProxy.Settings = new ControllerSettings(contentBlockController);

            this.pageOperations.CreatePageWithControl(mvcProxy, pageNamePrefix, pageTitlePrefix, urlNamePrefix, pageIndex);
            string responseContent = PageInvoker.ExecuteWebRequest(url);

            Assert.IsTrue(responseContent.Contains(contentBlockContent), "Content was not aligned right!");
        }

        [Test]
        [Category(TestCategories.ContentBlock)]
        [Author(FeatherTeams.Team2)]
        public void ContentBlockWidget_InsertImageToContentBlock()
        {
            string pageNamePrefix = "ContentBlockPage";
            string pageTitlePrefix = "Content Block";
            string urlNamePrefix = "content-block";
            int pageIndex = 1;
            string imageTitle = "One";
            string imageExtension = ".jpg";
            string imageName = "1.jpg";
            string contentBlockContentPart1 = "<img alt=\"\" src=\"";
            string contentBlockContentPart2 = "\" />";
            string url = UrlPath.ResolveAbsoluteUrl("~/" + urlNamePrefix + pageIndex);

            try
            {
                var imageId = Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.Images().CreateImage(imageTitle, imageExtension, imageName);
                var imageUrl = Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.Images().GetImageUrl(imageId);

                string contentBlockContent = string.Concat(contentBlockContentPart1, imageUrl, contentBlockContentPart2);

                var mvcProxy = new MvcControllerProxy();
                mvcProxy.ControllerName = typeof(ContentBlockController).FullName;
                var contentBlockController = new ContentBlockController();
                contentBlockController.Content = contentBlockContent;
                mvcProxy.Settings = new ControllerSettings(contentBlockController);

                this.pageOperations.CreatePageWithControl(mvcProxy, pageNamePrefix, pageTitlePrefix, urlNamePrefix, pageIndex);
                string responseContent = PageInvoker.ExecuteWebRequest(url);

                Assert.IsTrue(responseContent.Contains(contentBlockContent), "Image was not found!");
            }
            finally
            {
                Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.Images().DeleteAllImages(Telerik.Sitefinity.TestUtilities.CommonOperations.ContentLifecycleStatus.Master);
            }
        }

        #region Fields and constants

        private PagesOperations pageOperations;
      
        #endregion
    }
}
