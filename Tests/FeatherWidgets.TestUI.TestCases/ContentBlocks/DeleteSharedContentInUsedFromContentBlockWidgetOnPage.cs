using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Feather.Widgets.TestUI.Framework;
using FeatherWidgets.TestUI.TestCases;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FeatherWidgets.TestUI
{
    /// <summary>
    /// DeleteSharedContentInUsedFromContentBlockWidgetOnPage test class.
    /// </summary>
    [TestClass]
    public class DeleteSharedContentInUsedFromContentBlockWidgetOnPage_ : FeatherTestCase
    {
         /// <summary>
        /// UI test DeleteSharedContentInUsedFromContentBlockWidgetOnPage
        /// </summary>
        [TestMethod,
       Microsoft.VisualStudio.TestTools.UnitTesting.Owner("Feather team"),
       TestCategory(FeatherTestCategories.PagesAndContent)]
        public void DeleteSharedContentInUsedFromContentBlockWidgetOnPage()
        {
            this.VerifyPageBackend(PageName, WidgetName, ContentBlockContent);
            BAT.Macros().NavigateTo().Modules().ContentBlocks();
            BAT.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().DeleteContentByTitle(ContentBlockTitle);
            this.VerifyPageBackend(PageName, WidgetName, CreateContent);
            this.NavigatePageOnTheFrontend(PageName);
            BATFeather.Wrappers().Frontend().ContentBlock().ContentBlockWrapper().VerifyContentOfContentBlockOnThePageFrontend(ExpectedContent);
        }

        /// <summary>
        /// Verify page backend
        /// </summary>
        /// <param name="pageName">Page name</param>
        /// <param name="widgetName">Widget name</param>
        /// <param name="widgetContent">Widget content</param>
        public void VerifyPageBackend(string pageName, string widgetName, string widgetContent)
        {
            BAT.Macros().NavigateTo().Pages();
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(pageName);
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().CheckWidgetContent(widgetName, widgetContent);
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();
        }

        /// <summary>
        /// Navigate page on the front end
        /// </summary>
        /// <param name="pageName">Page name</param>
        public void NavigatePageOnTheFrontend(string pageName)
        {
            BAT.Macros().NavigateTo().CustomPage("~/" + pageName.ToLower());
            ActiveBrowser.WaitUntilReady();
        }

        /// <summary>
        /// Performs Server Setup and prepare the system with needed data.
        /// </summary>
        protected override void ServerSetup()
        {
            BAT.Macros().User().EnsureAdminLoggedIn();
            BAT.Arrange(this.TestName).ExecuteSetUp();
        }

        /// <summary>
        /// Performs clean up and clears all data created by the test.
        /// </summary>
        protected override void ServerCleanup()
        {
            BAT.Arrange(this.TestName).ExecuteTearDown();
        }

        private const string PageName = "ContentBlock";
        private const string WidgetName = "ContentBlock";
        private const string ExpectedContent = "";
        private const string ContentBlockTitle = "ContentBlockTitle";
        private const string CreateContent = "Create Content";
        private const string ContentBlockContent = "Test content";
    }
}
