using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArtOfTest.WebAii.Core;
using Feather.Widgets.TestUI.Framework;
using FeatherWidgets.TestUI.TestCases;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telerik.Sitefinity.Frontend.TestUtilities;

namespace FeatherWidgets.TestUI.TestCases.ContentBlocks
{
    /// <summary>
    /// UpdateSharedContentInUsedFromOldContentBlockWidget test class.
    /// </summary>
    [TestClass]
    public class UpdateSharedContentInUsedFromOldContentBlockWidget_ : FeatherTestCase
    {
        /// <summary>
        /// UI test UpdateSharedContentInUsedFromOldContentBlockWidget
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.FeatherTeam),
        TestCategory(FeatherTestCategories.ContentBlock)]
        public void UpdateSharedContentInUsedFromOldContentBlockWidget()
        {
            BAT.Macros().NavigateTo().Pages();
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(PageName);
            this.EditOldContentBlockWidget();
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().CheckWidgetContent(NewContentBlockWidget, ExpectedContent);
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().CheckWidgetContent(OldContentBlockWidget, ExpectedContent);
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();
            BAT.Macros().NavigateTo().Modules().ContentBlocks();
            this.VerifyContentBlockInContentModule();
            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower());
            BATFeather.Wrappers().Frontend().ContentBlock().ContentBlockWrapper().VerifyContentOfContentBlockOnThePageFrontend(ExpectedContent);
            BAT.Wrappers().Frontend().ContentBlock().ContentBlockWrapper().VerifyContentOfContentBlockOnThePageFrontend(ExpectedContent);
        }

        /// <summary>
        /// Edit shared content block from widget
        /// </summary>
        public void EditOldContentBlockWidget()
        {
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(OldContentBlockWidget);
            BAT.Wrappers().Backend().Pages().WidgetDesigners().ContentBlockDesigner().ClickEditThisContentLink();
            ActiveBrowser.WaitForAsyncRequests();
            BAT.Wrappers().Backend().Pages().WidgetDesigners().ContentBlockDesigner().SharedContentRadEditor.Html = ExpectedContent;
            BAT.Wrappers().Backend().Pages().WidgetDesigners().ContentBlockDesigner().SaveEditedWidgetChanges();
            BAT.Wrappers().Backend().Pages().WidgetDesigners().ContentBlockDesigner().ConfirmSharedContentEdit();
            ActiveBrowser.WaitForAsyncRequests();
        }

        /// <summary>
        /// Verify edited content in content module
        /// </summary>
        public void VerifyContentBlockInContentModule()
        {
            BAT.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().ClickOnContentBlockTitle(ContentBlockTitle);            
            string content = BAT.Wrappers().Backend().ContentBlocks().ContentBlocksEditWrapper().GetContent();
            Assert.AreEqual(ExpectedContent, content, "Content was not modified.");
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
        private const string NewContentBlockWidget = "ContentBlock";
        private const string OldContentBlockWidget = "Content block";
        private const string EditContent = " edited";
        private const string ExpectedContent = "Test content edited";
        private const string ContentBlockTitle = "ContentBlockTitle";
    }
}
