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
    /// EditSharedContentBlockFromPage test class.
    /// </summary>
    [TestClass]
    public class EditSharedContentBlockFromPage_ : FeatherTestCase
    { 
        /// <summary>
        /// UI test EditSharedContentBlockFromPage
        /// </summary>
        [TestMethod,
       Microsoft.VisualStudio.TestTools.UnitTesting.Owner("Feather team"),
       TestCategory(FeatherTestCategories.PagesAndContent)]
        public void EditSharedContentBlockFromPage()
        {
            BAT.Macros().NavigateTo().Pages();
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(PageName);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(WidgetName);
            BATFeather.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().FillContentToContentBlockWidget(EditContent);
            BATFeather.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().SaveChanges();
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage(); 
            this.VerifyIfSharedContentIsModified(ExpectedContent, ContentBlockTitle);
            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower());
            BATFeather.Wrappers().Frontend().ContentBlock().ContentBlockWrapper().VerifyContentOfContentBlockOnThePageFrontend(ExpectedContent);
        }

        /// <summary>
        /// Verify if shared content is modified
        /// </summary>
        /// <param name="newContentBlockContent">New content</param>
        /// <param name="contentBlockTitle">Content block title</param>
        public void VerifyIfSharedContentIsModified(string newContentBlockContent, string contentBlockTitle)
        {
            BAT.Macros().NavigateTo().Modules().ContentBlocks();
            BAT.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().ClickOnContentBlockTitle(contentBlockTitle);
            string content = BAT.Wrappers().Backend().ContentBlocks().ContentBlocksEditWrapper().GetContent();
            Assert.AreEqual(newContentBlockContent, content, "Content was not modified. ");
            BAT.Wrappers().Backend().ContentBlocks().ContentBlocksEditWrapper().SaveChanges();
            BAT.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().ClickOkButtonOnScreenContentSuccessfullyUpdated();
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
        private const string EditContent = " edited";
        private const string ExpectedContent = "Test content edited";
        private const string ContentBlockTitle = "ContentBlockTitle";
    }
}
