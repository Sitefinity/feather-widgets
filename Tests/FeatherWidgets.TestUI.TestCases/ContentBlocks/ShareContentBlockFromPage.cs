using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArtOfTest.WebAii.Controls.HtmlControls;
using Feather.Widgets.TestUI.Framework;
using FeatherWidgets.TestUI.TestCases;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telerik.Sitefinity.Frontend.TestUtilities;

namespace FeatherWidgets.TestUI.TestCases.ContentBlocks
{
    /// <summary>
    /// ShareContentBlockFromPage test class.
    /// </summary>
    [TestClass]
    public class ShareContentBlockFromPage_ : FeatherTestCase
    {
        /// <summary>
        /// UI test ShareContentBlockFromPage
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.FeatherTeam),
        TestCategory(FeatherTestCategories.ContentBlock)]
        public void ShareContentBlockFromPage()
        {
            BAT.Macros().NavigateTo().Pages();
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(PageName);
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().SelectExtraOptionForWidget(OperationName);
            BATFeather.Wrappers().Backend().ContentBlocks().ContentBlocksShareWrapper().FillContentBlockTitle(ContentBlockName);
            BATFeather.Wrappers().Backend().ContentBlocks().ContentBlocksShareWrapper().ShareButton();
            this.VerifyIfSharedLabelExist();
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();
            BAT.Macros().NavigateTo().Modules().ContentBlocks();
            this.VerifyIfContentBlockExist(ContentBlockName);
            this.VerifyLinkUsedOfContentBlockOnPage();
            this.ClickLinkUsedOfContentBlockOnPage();
            BAT.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().VerifyUsedPagesMessage(ExpectedCountMessage);
            this.VerifyPagesThatUseSharedContentBlock();     
        }

        /// <summary>
        /// Verify if shared label exist
        /// </summary>
        public void VerifyIfSharedLabelExist()
        {
            bool isExist = BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().VerifyContentBlockWidgetSharedLabel();
            Assert.IsTrue(isExist, "Shared label doesn't exist!");
        }

        /// <summary>
        /// Verify if content block exist
        /// </summary>
        /// <param name="contentBlockName">Content block name</param>
        public void VerifyIfContentBlockExist(string contentBlockName)
        {
           BAT.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().GetContentBlockRowByTitle(contentBlockName);
        }

        /// <summary>
        /// Verify link in used
        /// </summary>
        public void VerifyLinkUsedOfContentBlockOnPage()
        {
            HtmlTableCell cellUsedOnPage = BAT.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().FindUsedOnPageCell(ContentBlockName);
            BAT.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().VerifyTheLinkUsedOfContentBlockOnPage(cellUsedOnPage, ExpectedCount);
        }

        /// <summary>
        /// Click link in used of content block
        /// </summary>
        public void ClickLinkUsedOfContentBlockOnPage()
        {
            HtmlTableCell cellUsedOnPage = BAT.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().FindUsedOnPageCell(ContentBlockName);
            BAT.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().ClickTheLinkUsedOfContentBlockOnPage(cellUsedOnPage);
        }

        /// <summary>
        /// Verify pages that use shared content block
        /// </summary>
        public void VerifyPagesThatUseSharedContentBlock()
        {
            Dictionary<string, string> pages = new Dictionary<string, string>()
            {
                {
                    PageName, PageStatus
                }
            };

            BAT.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().VerifyPagesWithStatusesThatUseSharedContentBlock(pages);
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
        private const string ContentBlockName = "Content block title";
        private const string WidgetName = "ContentBlock";
        private const string OperationName = "Share";
        private const string PageStatus = "Published";
        private const string ExpectedCount = "1 page";
        private const string ExpectedCountMessage = "1 page uses this group"; 
    }
}
