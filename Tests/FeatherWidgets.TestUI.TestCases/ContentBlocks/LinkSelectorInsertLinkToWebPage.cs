using System;
using Feather.Widgets.TestUI.Framework;
using FeatherWidgets.TestUI.TestCases;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FeatherWidgets.TestUI
{
    /// <summary>
    /// LinkSelectorInsertLinkToWebPage test class.
    /// </summary>
    [TestClass]
    public class LinkSelectorInsertLinkToWebPage_ : FeatherTestCase
    {
        /// <summary>
        /// UI test LinkSelectorInsertLinkToWebPage
        /// </summary>
        [TestMethod,
        Owner("Sitefinity Team 7"),
        TestCategory(FeatherTestCategories.PagesAndContent)]
        public void LinkSelectorInsertLinkToWebPage()        
        {
            BAT.Macros().NavigateTo().Pages();
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(PageName);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(WidgetName);
            BATFeather.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().OpenLinkSelector();
            Assert.IsFalse(BATFeather.Wrappers().Backend().ContentBlocks().LinkSelectorWrapper().IsInsertLinkEnabled());
            BATFeather.Wrappers().Backend().ContentBlocks().LinkSelectorWrapper().VerifyTestThisLinkVisibility(false);

            BATFeather.Wrappers().Backend().ContentBlocks().LinkSelectorWrapper().EnterWebAddress(WebAddress);
            Assert.IsFalse(BATFeather.Wrappers().Backend().ContentBlocks().LinkSelectorWrapper().IsInsertLinkEnabled());
            BATFeather.Wrappers().Backend().ContentBlocks().LinkSelectorWrapper().VerifyTestThisLinkVisibility(false);

            BATFeather.Wrappers().Backend().ContentBlocks().LinkSelectorWrapper().EnterTextToDisplay(TextToDisplay);
            BATFeather.Wrappers().Backend().ContentBlocks().LinkSelectorWrapper().VerifyTestThisLinkVisibility(true);
            BATFeather.Wrappers().Backend().ContentBlocks().LinkSelectorWrapper().VerifyTestThisLinkAttributes(TextToDisplay, WebAddress);
            BATFeather.Wrappers().Backend().ContentBlocks().LinkSelectorWrapper().InsertLink();
            BATFeather.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().VerifyContentBlockTextDesignMode(TextToDisplay);
            BATFeather.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().SwitchToHtmlView();
            BATFeather.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().VerifyContentInHtmlEditableArea(HtmlContent);
            BATFeather.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().SaveChanges();
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().VerifyCreatedLink(TextToDisplay, WebAddress);
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();
            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower());
            BATFeather.Wrappers().Frontend().ContentBlock().ContentBlockWrapper().VerifyCreatedLink(TextToDisplay, WebAddress);
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
        private const string HtmlContent = "<a href=\"http://www.google.bg\">Test content</a>";
        private const string TextToDisplay = "Test content";
        private const string WebAddress = "http://www.google.bg";
    }
}