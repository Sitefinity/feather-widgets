using ArtOfTest.WebAii.Controls.HtmlControls;
using Feather.Widgets.TestUI.Framework;
using FeatherWidgets.TestUI.TestCases;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeatherWidgets.TestUI
{
    /// <summary>
    /// This is a sample test class.
    /// </summary>
    [TestClass]
    public class UseSharedContentBlockFromPage_ : FeatherTestCase
    {
        // <summary>
        /// Pefroms Server Setup and prepare the system with needed data.
        /// </summary>
        protected override void ServerSetup()
        {
            BAT.Macros().User().EnsureAdminLoggedIn();
        //    BAT.Arrange(this.TestName).ExecuteSetUp();
        }

        /// <summary>
        /// Performs clean up and clears all data created by the test.
        /// </summary>
        protected override void ServerCleanup()
        {
         //   BAT.Arrange(this.TestName).ExecuteTearDown();
        }

        [TestMethod,
       Microsoft.VisualStudio.TestTools.UnitTesting.Owner("Feather team"),
       TestCategory(FeatherTestCategories.PagesAndContent)]
        public void UseSharedContentBlockFromPage()
        {
            BAT.Macros().NavigateTo().Pages();
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(PageName);
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().SelectExtraOptionForWidget(OperationName);
            BATFeather.Wrappers().Backend().ContentBlocks().ContentBlocksShareWrapper().SelectContentBlock(ContentBlockTitle);
            this.VerifyIfSharedLabelExist();
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();
            BAT.Macros().NavigateTo().Modules().ContentBlocks();
            this.VerifyIfContentBlockExist(ContentBlockTitle);
            this.VerifyLinkUsedOfContentBlockOnPage();
            this.ClickLinkUsedOfContentBlockOnPage();
            BAT.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().VerifyUsedPagesMessage(ExpectedCount);
            this.VerifyPagesThatUseSharedContentBlock();
            this.NavigatePageOnTheFrontend(PageName);
            BATFeather.Wrappers().Frontend().ContentBlock().ContentBlockWrapper().VerifyContentOnTheFrontend(ContentBlockContent);
        }

        private void VerifyIfSharedLabelExist()
        {
            bool isExist = BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().VerifyContentBlockWidgetSharedLabel();
            Assert.IsTrue(isExist, "Shared label doesn't exist!");
        }

        public void VerifyIfContentBlockExist(string contentBlockName)
        {
            BAT.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().GetContentBlockRowByTitle(contentBlockName);
        }

        public void VerifyLinkUsedOfContentBlockOnPage()
        {
            HtmlTableCell cellUsedOnPage = BAT.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().FindUsedOnPageCell(ContentBlockTitle);
            BAT.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().VerifyTheLinkUsedOfContentBlockOnPage(cellUsedOnPage, ExpectedCount);
        }

        public void ClickLinkUsedOfContentBlockOnPage()
        {
            HtmlTableCell cellUsedOnPage = BAT.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().FindUsedOnPageCell(ContentBlockTitle);
            BAT.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().ClickTheLinkUsedOfContentBlockOnPage(cellUsedOnPage);
        }

        public void VerifyPagesThatUseSharedContentBlock()
        {
            Dictionary<string, string> pages = new Dictionary<string, string>()
            {
                {PageName, PageStatus}
            };

            BAT.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().VerifyPagesWithStatusesThatUseSharedContentBlock(pages);
        }

        public void NavigatePageOnTheFrontend(string pageName)
        {
            BAT.Macros().NavigateTo().CustomPage("~/" + pageName.ToLower());
            ActiveBrowser.WaitUntilReady();
        }

        private const string PageName = "ContentBlock";
        private const string OperationName = "Use shared";
        private const string ContentBlockTitle = "ContentBlockTitle";
        private const string PageStatus = "Published";
        private const string ExpectedCount = "1 page";
        private const string ContentBlockContent = "Test content";
    }
}
