using System;
using Feather.Widgets.TestUI.Framework;
using FeatherWidgets.TestUI.TestCases;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telerik.Sitefinity.Frontend.TestUtilities;

namespace FeatherWidgets.TestUI.TestCases.ContentBlocks
{
    /// <summary>
    /// LinkSelectorInsertAnAnchor test class.
    /// </summary>
    [TestClass]
    public class LinkSelectorInsertAnAnchor_ : FeatherTestCase
    {
        /// <summary>
        /// UI test LinkSelectorInsertAnAnchor
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.Team7),
        TestCategory(FeatherTestCategories.PagesAndContent),
        TestCategory(FeatherTestCategories.ContentBlock),
        TestCategory(FeatherTestCategories.LinkSelector)]
        public void LinkSelectorInsertAnAnchor()        
        {
            BAT.Macros().NavigateTo().Pages();
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(PageName);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(WidgetName);
            BATFeather.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().OpenLinkSelector();
            BATFeather.Wrappers().Backend().ContentBlocks().LinkSelectorWrapper().SwitchToSelectedTab(SelectedTabName);
            Assert.IsFalse(BATFeather.Wrappers().Backend().ContentBlocks().LinkSelectorWrapper().IsInsertLinkButtonEnabled());
            BATFeather.Wrappers().Backend().ContentBlocks().LinkSelectorWrapper().VerifySelectedValueInAnchorDropdown(DefaultAnchorDropdownValue);
            BATFeather.Wrappers().Backend().ContentBlocks().LinkSelectorWrapper().VerifyThatHowToInsertAnAnchorLinkIsVisible();

            BATFeather.Wrappers().Backend().ContentBlocks().LinkSelectorWrapper().EnterTextToDisplay(TextToDisplay, TabIndex);
            Assert.IsFalse(BATFeather.Wrappers().Backend().ContentBlocks().LinkSelectorWrapper().IsInsertLinkButtonEnabled());
            BATFeather.Wrappers().Backend().ContentBlocks().LinkSelectorWrapper().SelectAnchor(AnchorName);

            BATFeather.Wrappers().Backend().ContentBlocks().LinkSelectorWrapper().InsertLink();
            BATFeather.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().VerifyContentBlockTextDesignMode("Test1" + "Test2" + TextToDisplay);

            BATFeather.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().SelectLinkInEditableArea(TextToDisplay);
            BATFeather.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().OpenLinkSelector();

            BATFeather.Wrappers().Backend().ContentBlocks().LinkSelectorWrapper().VerifySelectedValueInAnchorDropdown(AnchorName);
            BATFeather.Wrappers().Backend().ContentBlocks().LinkSelectorWrapper().VerifyCorrectTextToDisplay(TextToDisplay, TabIndex);
            BATFeather.Wrappers().Backend().ContentBlocks().LinkSelectorWrapper().CancelEditingLinkSelector();

            BATFeather.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().SwitchToHtmlView();
            BATFeather.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().VerifyContentInHtmlEditableArea(HtmlContent);
            BATFeather.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().SaveChanges();
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().VerifyCreatedLink(TextToDisplay, Anchor);
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();
            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower());
            BATFeather.Wrappers().Frontend().ContentBlock().ContentBlockWrapper().VerifyCreatedLink(TextToDisplay, Anchor);

            BAT.Macros().NavigateTo().Pages();
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(PageName);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(WidgetName);
            BATFeather.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().DeleteAllContentInEditableArea();
            BATFeather.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().OpenLinkSelector();
            BATFeather.Wrappers().Backend().ContentBlocks().LinkSelectorWrapper().SwitchToSelectedTab(SelectedTabName);
            Assert.IsFalse(BATFeather.Wrappers().Backend().ContentBlocks().LinkSelectorWrapper().IsInsertLinkButtonEnabled());
            BATFeather.Wrappers().Backend().ContentBlocks().LinkSelectorWrapper().VerifyAnchorDropdownIsNotVisible();
            BATFeather.Wrappers().Backend().ContentBlocks().LinkSelectorWrapper().VerifyTextToDisplayIsNotVisible(TabIndex);
            BATFeather.Wrappers().Backend().ContentBlocks().LinkSelectorWrapper().VerifyNoAnchorsHaveBeenInsertedDialog();
            BATFeather.Wrappers().Backend().ContentBlocks().LinkSelectorWrapper().CancelEditingLinkSelector();
            BATFeather.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().SaveChanges();
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();
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
        private const string SelectedTabName = "Anchor";
        private const string WidgetName = "ContentBlock";
        private const string HtmlContent = "<div id=\"test1\">Test1</div><p></p><div id=\"test2\">Test2</div><p></p><a href=\"#test2\">Test content</a>";
        private const string TextToDisplay = "Test content";
        private const string AnchorName = "test2";
        private const string Anchor = "#test2";
        private const string DefaultAnchorDropdownValue = "- Select -";
        private const int TabIndex = 3;
    }
}