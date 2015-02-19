using System;
using Feather.Widgets.TestUI.Framework;
using FeatherWidgets.TestUI.TestCases;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FeatherWidgets.TestUI
{
    /// <summary>
    /// LinkSelectorInsertLinkToEmailAddress test class.
    /// </summary>
    [TestClass]
    public class LinkSelectorInsertLinkToEmailAddress_ : FeatherTestCase
    {
        /// <summary>
        /// UI test LinkSelectorInsertLinkToEmailAddress
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.Team7),
        TestCategory(FeatherTestCategories.PagesAndContent),
        TestCategory(FeatherTestCategories.ContentBlock),
        TestCategory(FeatherTestCategories.LinkSelector)]
        public void LinkSelectorInsertLinkToEmailAddress()        
        {
            BAT.Macros().NavigateTo().Pages();
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(PageName);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(WidgetName);
            BATFeather.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().OpenLinkSelector();
            BATFeather.Wrappers().Backend().ContentBlocks().LinkSelectorWrapper().SwitchToSelectedTab(SelectedTabName);
            Assert.IsFalse(BATFeather.Wrappers().Backend().ContentBlocks().LinkSelectorWrapper().IsInsertLinkButtonEnabled());

            BATFeather.Wrappers().Backend().ContentBlocks().LinkSelectorWrapper().EnterEmail(InvalidEmailAddress);
            BATFeather.Wrappers().Backend().ContentBlocks().LinkSelectorWrapper().VerifyInvalidEmailMessage(true);
            BATFeather.Wrappers().Backend().ContentBlocks().LinkSelectorWrapper().EnterTextToDisplay(TextToDisplay, TabIndex);
            Assert.IsFalse(BATFeather.Wrappers().Backend().ContentBlocks().LinkSelectorWrapper().IsInsertLinkButtonEnabled());

            BATFeather.Wrappers().Backend().ContentBlocks().LinkSelectorWrapper().EnterEmail(ValidEmailAddress);
            BATFeather.Wrappers().Backend().ContentBlocks().LinkSelectorWrapper().VerifyInvalidEmailMessage(false);
            BATFeather.Wrappers().Backend().ContentBlocks().LinkSelectorWrapper().InsertLink();
            BATFeather.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().VerifyContentBlockTextDesignMode(TextToDisplay);

            BATFeather.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().SelectTextInEditableArea(TextToDisplay);
            BATFeather.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().OpenLinkSelector();

            BATFeather.Wrappers().Backend().ContentBlocks().LinkSelectorWrapper().VerifyCorrectEmailAddress(ValidEmailAddress);
            BATFeather.Wrappers().Backend().ContentBlocks().LinkSelectorWrapper().VerifyCorrectTextToDisplay(TextToDisplay, TabIndex);
            BATFeather.Wrappers().Backend().ContentBlocks().LinkSelectorWrapper().CancelEditingLinkSelector();

            BATFeather.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().SwitchToHtmlView();
            BATFeather.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().VerifyContentInHtmlEditableArea(HtmlContent);
            BATFeather.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().SaveChanges();
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().VerifyCreatedLink(TextToDisplay, Href);
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();
            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower());
            BATFeather.Wrappers().Frontend().ContentBlock().ContentBlockWrapper().VerifyCreatedLink(TextToDisplay, Href);
        }

        /// <summary>
        /// Performs Server Setup and prepare the system with needed data.
        /// </summary>
        protected override void ServerSetup()
        {
            BAT.Macros().User().EnsureAdminLoggedIn();
            BAT.Arrange(ArrangementClassName).ExecuteSetUp();
        }

        /// <summary>
        /// Performs clean up and clears all data created by the test.
        /// </summary>
        protected override void ServerCleanup()
        {
            BAT.Arrange(ArrangementClassName).ExecuteTearDown();
        }

        private const string ArrangementClassName = "LinkSelectorInsertLink";
        private const string SelectedTabName = "Email";
        private const string PageName = "ContentBlock";
        private const string WidgetName = "ContentBlock";
        private const string HtmlContent = "<a href=\"mailto:test@abv.bg\">Test content</a>";
        private const string TextToDisplay = "Test content";
        private const string InvalidEmailAddress = "test";
        private const string ValidEmailAddress = "test@abv.bg";
        private const string Href = "mailto:test@abv.bg";
        private const int TabIndex = 4;
    }
}