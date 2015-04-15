using System;
using Feather.Widgets.TestUI.Framework;
using FeatherWidgets.TestUI.TestCases;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telerik.Sitefinity.Frontend.TestUtilities;

namespace FeatherWidgets.TestUI.TestCases.ContentBlocks
{
    /// <summary>
    /// LinkSelectorInsertLinkToWebPageOverSelectedImage test class.
    /// </summary>
    [TestClass]
    public class LinkSelectorInsertLinkToWebPageOverSelectedImage_ : FeatherTestCase
    {
        /// <summary>
        /// UI test LinkSelectorInsertLinkToWebPageOverSelectedImage
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.Team7),
        TestCategory(FeatherTestCategories.PagesAndContent),
        TestCategory(FeatherTestCategories.ContentBlock),
        TestCategory(FeatherTestCategories.LinkSelector)]
        public void LinkSelectorInsertLinkToWebPageOverSelectedImage()
        { 
            BAT.Macros().NavigateTo().Pages();
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(PageName);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(WidgetName);
            BATFeather.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().SelectAllContentInEditableArea();
            BATFeather.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().OpenLinkSelector();
            Assert.IsFalse(BATFeather.Wrappers().Backend().ContentBlocks().LinkSelectorWrapper().IsInsertLinkButtonEnabled());
            BATFeather.Wrappers().Backend().ContentBlocks().LinkSelectorWrapper().VerifyCorrectWebAddress("http://");
            BATFeather.Wrappers().Backend().ContentBlocks().LinkSelectorWrapper().VerifyTextToDisplayIsNotVisible(TabIndex);
            BATFeather.Wrappers().Backend().ContentBlocks().LinkSelectorWrapper().EnterWebAddress(WebAddress);
            BATFeather.Wrappers().Backend().ContentBlocks().LinkSelectorWrapper().VerifyTestThisLinkVisibility(false);
            BATFeather.Wrappers().Backend().ContentBlocks().LinkSelectorWrapper().InsertLink();
            BATFeather.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().VerifyContentBlockImageDesignMode(ImageSrc);
            BATFeather.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().SwitchToHtmlView();
            BATFeather.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().VerifyContentInHtmlEditableArea(HtmlContent);
            BATFeather.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().SaveChanges();
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().VerifyCreatedImageLink(ImageSrc, WebAddress);

            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(WidgetName);
            BATFeather.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().SelectAllContentInEditableArea();
            BATFeather.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().OpenLinkSelector();
            BATFeather.Wrappers().Backend().ContentBlocks().LinkSelectorWrapper().VerifyCorrectWebAddress(WebAddress);
            BATFeather.Wrappers().Backend().ContentBlocks().LinkSelectorWrapper().VerifyTextToDisplayIsNotVisible(TabIndex);
            BATFeather.Wrappers().Backend().ContentBlocks().LinkSelectorWrapper().SelectOpenInNewWindowOption(TabIndex);
            BATFeather.Wrappers().Backend().ContentBlocks().LinkSelectorWrapper().InsertLink();
            BATFeather.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().PressFullScreenButton();
            BATFeather.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().VerifyFullScreenMode(true);
            BATFeather.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().SwitchToHtmlView();
            BATFeather.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().VerifyContentInHtmlEditableArea(NewHtmlContent);
            BATFeather.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().VerifyFullScreenMode(true);
            BATFeather.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().VerifyCountOfButtonsIsCorrect(2);
            BATFeather.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().PressFullScreenButton();
            BATFeather.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().VerifyFullScreenMode(false);
            BATFeather.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().SwitchToDesignView();
            BATFeather.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().VerifyCountOfButtonsIsCorrect(26);
            BATFeather.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().VerifyFullScreenMode(false);
            BATFeather.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().SaveChanges();
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();

            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower());
            BATFeather.Wrappers().Frontend().ContentBlock().ContentBlockWrapper().VerifyCreatedImageLink(ImageSrc, WebAddress, true);
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
        private const string HtmlContent = "<a href=\"http://www.google.bg\"><img src=\"https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTQOrSSvhefLVAXo3OOoMGYGS232bfHFnZyA9Jk24KeefYuau8c\" /></a>";
        private const string NewHtmlContent = "<a href=\"http://www.google.bg\" target=\"_blank\"><img src=\"https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTQOrSSvhefLVAXo3OOoMGYGS232bfHFnZyA9Jk24KeefYuau8c\" /></a>";
        private const string ImageSrc = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTQOrSSvhefLVAXo3OOoMGYGS232bfHFnZyA9Jk24KeefYuau8c";
        private const string WebAddress = "http://www.google.bg";
        private const int TabIndex = 1;
    }
}