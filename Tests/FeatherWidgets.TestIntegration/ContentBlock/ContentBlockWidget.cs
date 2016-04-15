using System;
using System.Linq;
using FeatherWidgets.TestUtilities.CommonOperations;
using MbUnit.Framework;
using Telerik.Sitefinity;
using Telerik.Sitefinity.Frontend.ContentBlock.Mvc.Controllers;
using Telerik.Sitefinity.Frontend.TestUtilities;
using Telerik.Sitefinity.Modules.GenericContent;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Mvc.Proxy;
using Telerik.Sitefinity.TestIntegration.Data.Content;
using Telerik.Sitefinity.Web;

namespace FeatherWidgets.TestIntegration.ContentBlock
{
    /// <summary>
    /// This is a class with Content block tests - Advanced settings.
    /// </summary>
    [TestFixture]
    [Description("This is a class with Content block tests for Advanced settings.")]
    public class ContentBlockWidget
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
        [Author(FeatherTeams.FeatherTeam)]
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
            contentBlockController.WrapperCssClass = ContentBlockWrapperCssClass;
            mvcProxy.Settings = new ControllerSettings(contentBlockController);

            this.pageOperations.CreatePageWithControl(mvcProxy, pageNamePrefix, pageTitlePrefix, urlNamePrefix, pageIndex);

            string responseContent = PageInvoker.ExecuteWebRequest(url);

            Assert.IsTrue(responseContent.Contains(ContentBlockContent), "The content block with this title was not found!");
            Assert.IsTrue(responseContent.Contains(ContentBlockWrapperCssClass), "WrapperCssClass with name '" + ContentBlockWrapperCssClass + "' was not found");
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling"), Test]
        [Category(TestCategories.ContentBlock)]
        [Author(FeatherTeams.FeatherTeam)]
        [Description("Verifies that when edit shared content block the changes are applied in content block widget on the frontend.")]
        public void ContentBlockWidget_EditSharedContent()
        {
            string testName = "ContentBlockWidgetEditSharedContent";
            string pageNamePrefix = testName + "ContentBlockPage";
            string pageTitlePrefix = testName + "Content Block";
            string urlNamePrefix = testName + "content-block";
            int pageIndex = 1;
            string url = UrlPath.ResolveAbsoluteUrl("~/" + urlNamePrefix + pageIndex);

            var content = App.WorkWith().ContentItems()
                           .Where(c => c.Title == ContentBlockTitle && c.Status == Telerik.Sitefinity.GenericContent.Model.ContentLifecycleStatus.Master)
                           .Get().Single();

            var mvcProxy = new MvcControllerProxy();
            mvcProxy.ControllerName = typeof(ContentBlockController).FullName;
            var contentBlockController = new ContentBlockController();
            contentBlockController.SharedContentID = content.Id;
            mvcProxy.Settings = new ControllerSettings(contentBlockController);

            this.pageOperations.CreatePageWithControl(mvcProxy, pageNamePrefix, pageTitlePrefix, urlNamePrefix, pageIndex);

            App.WorkWith().ContentItem(content.Id).CheckOut().Do(cI =>
            {
                cI.Content = ContentBlockContentEdited;
                cI.LastModified = DateTime.UtcNow;
            })
                .CheckIn().Publish().SaveChanges();

            string responseContent = PageInvoker.ExecuteWebRequest(url);
            Assert.IsTrue(responseContent.Contains(ContentBlockContentEdited), "The content block with this title was not found!");
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling"), Test]
        [Category(TestCategories.ContentBlock)]
        [Author(FeatherTeams.FeatherTeam)]
        [Description("Verifies text of content block widget in preview mode.")]
        public void ContentBlockWidget_ValidatePreviewMode()
        {
            string testName = "ContentBlockWidgetEditSharedContent";
            string pageNamePrefix = testName + "ContentBlockPage";
            string pageTitlePrefix = testName + "Content Block";
            string urlNamePrefix = testName + "content-block";
            int pageIndex = 1;
            string previewUrl = UrlPath.ResolveAbsoluteUrl("~/" + urlNamePrefix + pageIndex) + "/Action/Preview";

            var content = App.WorkWith().ContentItems()
                           .Where(c => c.Title == ContentBlockTitle && c.Status == Telerik.Sitefinity.GenericContent.Model.ContentLifecycleStatus.Master)
                           .Get().Single();

            var mvcProxy = new MvcControllerProxy();
            mvcProxy.ControllerName = typeof(ContentBlockController).FullName;
            var contentBlockController = new ContentBlockController();
            contentBlockController.SharedContentID = content.Id;
            mvcProxy.Settings = new ControllerSettings(contentBlockController);

            this.pageOperations.CreatePageWithControl(mvcProxy, pageNamePrefix, pageTitlePrefix, urlNamePrefix, pageIndex);

            App.WorkWith().ContentItem(content.Id).CheckOut().Do(cI =>
            {
                cI.Content = ContentBlockContentEdited;
                cI.LastModified = DateTime.UtcNow;
            })
                .CheckIn().Publish().SaveChanges();

            string responseContent = PageInvoker.ExecuteWebRequest(previewUrl);
            Assert.IsTrue(responseContent.Contains(ContentBlockContentEdited), "The content block with this text was not found in preview mode!");
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling"), Test]
        [Category(TestCategories.ContentBlock)]
        [Author(FeatherTeams.FeatherTeam)]
        [Description("Verifies text of the content block widget when edit page.")]
        public void ContentBlockWidget_ValidateOnEditPage()
        {
            string testName = "ContentBlockWidgetEditSharedContent";
            string pageNamePrefix = testName + "ContentBlockPage";
            string pageTitlePrefix = testName + "Content Block";
            string urlNamePrefix = testName + "content-block";
            int pageIndex = 1;

            var content = App.WorkWith().ContentItems()
                           .Where(c => c.Title == ContentBlockTitle && c.Status == Telerik.Sitefinity.GenericContent.Model.ContentLifecycleStatus.Master)
                           .Get().Single();

            var mvcProxy = new MvcControllerProxy();
            mvcProxy.ControllerName = typeof(ContentBlockController).FullName;
            var contentBlockController = new ContentBlockController();
            contentBlockController.SharedContentID = content.Id;
            mvcProxy.Settings = new ControllerSettings(contentBlockController);

            var pageId = this.pageOperations.CreatePageWithControl(mvcProxy, pageNamePrefix, pageTitlePrefix, urlNamePrefix, pageIndex);

            PageManager manager = PageManager.GetManager();
            var page = manager.GetPageNode(pageId);
            var pageUrl = page.GetFullUrl();
            pageUrl = RouteHelper.GetAbsoluteUrl(pageUrl) + "/Action/Edit";

            string responseContent = PageInvoker.ExecuteWebRequest(pageUrl);
            Assert.IsTrue(responseContent.Contains(ContentBlockContent), "The content block with this text was not found!");
        }

        [Test]
        [Category(TestCategories.ContentBlock)]
        [Author(FeatherTeams.FeatherTeam)]
        public void ContentBlockWidget_SocialShareButtonsFunctionality()
        {
            string pageNamePrefix = "ContentBlockPage";
            string pageTitlePrefix = "Content Block";
            string urlNamePrefix = "content-block";
            int pageIndex = 1;
            string url = UrlPath.ResolveAbsoluteUrl("~/" + urlNamePrefix + pageIndex);
            string socialShare = "list-inline sf-social-share";

            var mvcProxy = new MvcControllerProxy();
            mvcProxy.ControllerName = typeof(ContentBlockController).FullName;
            var contentBlockController = new ContentBlockController();
            contentBlockController.Content = ContentBlockContent;
            contentBlockController.EnableSocialSharing = true;
            mvcProxy.Settings = new ControllerSettings(contentBlockController);

            this.pageOperations.CreatePageWithControl(mvcProxy, pageNamePrefix, pageTitlePrefix, urlNamePrefix, pageIndex);

            string responseContent = PageInvoker.ExecuteWebRequest(url);

            Assert.IsTrue(responseContent.Contains(socialShare), "Social share button was not found!");
        }

        [Test]
        [Category(TestCategories.ContentBlock)]
        [Author(FeatherTeams.FeatherTeam)]
        [Description("Verifies that set shared content id to content block widget results in correct page statistics.")]
        public void ContentBlockWidget_ShareFunctionality()
        {
            string pageNamePrefix = "ContentBlockPage";
            string pageTitlePrefix = "Content Block";
            string urlNamePrefix = "content-block";
            int pageIndex = 1;

            var contentManager = ContentManager.GetManager();
            var contentItem = contentManager.GetContent()
                .Single(c => c.Status == Telerik.Sitefinity.GenericContent.Model.ContentLifecycleStatus.Live && c.Title == ContentBlockTitle);

            var mvcProxy = new MvcControllerProxy();
            mvcProxy.ControllerName = typeof(ContentBlockController).FullName;
            var contentBlockController = new ContentBlockController();
            contentBlockController.Content = ContentBlockContent;
            contentBlockController.SharedContentID = contentItem.Id;
            mvcProxy.Settings = new ControllerSettings(contentBlockController);

            this.pageOperations.CreatePageWithControl(mvcProxy, pageNamePrefix, pageTitlePrefix, urlNamePrefix, pageIndex);

            Assert.AreEqual(1, contentManager.GetCountOfPagesThatUseContent(contentItem.Id));
        }

        [Test]
        [Category(TestCategories.ContentBlock)]
        [Author(FeatherTeams.FeatherTeam)]
        [Description("Verifies that updated shared content id to content block widget results in correct page statistics.")]
        public void ContentBlockWidget_UnshareFunctionality()
        {
            string pageNamePrefix = "ContentBlockPage";
            string pageTitlePrefix = "Content Block";
            string urlNamePrefix = "content-block";
            int pageIndex = 1;
            var contentManager = ContentManager.GetManager();
            var contentItem = contentManager.GetContent()
                .Single(c => c.Status == Telerik.Sitefinity.GenericContent.Model.ContentLifecycleStatus.Live && c.Title == ContentBlockTitle);

            var mvcProxy = new MvcControllerProxy();
            mvcProxy.ControllerName = typeof(ContentBlockController).FullName;
            var contentBlockController = new ContentBlockController();
            contentBlockController.Content = ContentBlockContent;
            contentBlockController.SharedContentID = contentItem.Id;
            mvcProxy.Settings = new ControllerSettings(contentBlockController);

            var pageId = this.pageOperations.CreatePageWithControl(mvcProxy, pageNamePrefix, pageTitlePrefix, urlNamePrefix, pageIndex);

            PageContentGenerator.ModifyPageControl(
               pageId,
               c => c.ObjectType == mvcProxy.GetType().FullName,
               (c) =>
               {
                   var manager = PageManager.GetManager();
                   mvcProxy = manager.LoadControl(c) as MvcControllerProxy;
                   contentBlockController = (ContentBlockController)mvcProxy.Controller;
                   contentBlockController.SharedContentID = Guid.Empty;
                   mvcProxy.Settings = new ControllerSettings(contentBlockController);
                   manager.ReadProperties(mvcProxy, c);
               });

            Assert.AreEqual(0, contentManager.GetCountOfPagesThatUseContent(contentItem.Id));
        }

        #region Fields and constants

        private PagesOperations pageOperations;
        private const string ContentBlockTitle = "This is content block title";
        private const string ContentBlockContent = "This is content block content";
        private const string ContentBlockContentEdited = "This is content block content edited";
        private const string ContentBlockWrapperCssClass = "testCSSclass";

        #endregion
    }
}
