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
    /// ContentBlockSwitchBetweenHtmlAndDesignViews test class.
    /// </summary>
    [TestClass]
    public class ContentBlockSwitchBetweenHtmlAndDesignViews_ : FeatherTestCase
    {
        /// <summary>
        /// UI test ContentBlockSwitchBetweenHtmlAndDesignViews
        /// </summary>
        [TestMethod,
       Microsoft.VisualStudio.TestTools.UnitTesting.Owner("Sitefinity Team 7"),
       TestCategory(FeatherTestCategories.PagesAndContent), Ignore]
        public void ContentBlockSwitchBetweenHtmlAndDesignViews()
        {
            BAT.Macros().NavigateTo().Pages();
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(PageName);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(WidgetName);
            BATFeather.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().OpenLinkSelector();
            BATFeather.Wrappers().Backend().ContentBlocks().LinkSelectorWrapper().InsertLink(false);
            BATFeather.Wrappers().Backend().ContentBlocks().LinkSelectorWrapper().VerifyTestThisLinkVisibility(false);

            BATFeather.Wrappers().Backend().ContentBlocks().LinkSelectorWrapper().EnterWebAddress(WebAddress);
            BATFeather.Wrappers().Backend().ContentBlocks().LinkSelectorWrapper().InsertLink(false);
            BATFeather.Wrappers().Backend().ContentBlocks().LinkSelectorWrapper().VerifyTestThisLinkVisibility(false);

            BATFeather.Wrappers().Backend().ContentBlocks().LinkSelectorWrapper().EnterTextToDisplay(Content);
            BATFeather.Wrappers().Backend().ContentBlocks().LinkSelectorWrapper().VerifyTestThisLinkVisibility(true);
            BATFeather.Wrappers().Backend().ContentBlocks().LinkSelectorWrapper().VerifyTestThisLinkProperties(Content, WebAddress);
            BATFeather.Wrappers().Backend().ContentBlocks().LinkSelectorWrapper().InsertLink();
            BATFeather.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().VerifyCreatedLink(Content, WebAddress);
            BATFeather.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().SelectContentInEditableArea();
            BATFeather.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().SwitchToHtmlView();
            BATFeather.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().VerifyContentInHtmlEditableArea(HtmlContent);
            BATFeather.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().SwitchToDesignView();
            BATFeather.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().SaveChanges();
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();
            this.NavigatePageOnTheFrontend(PageName);
            BATFeather.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().VerifyCreatedLink(Content, WebAddress);

            BAT.Macros().NavigateTo().Pages();
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(PageName);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(WidgetName);
            BATFeather.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().SelectContentInEditableArea();
            BATFeather.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().OpenLinkSelector();
            BATFeather.Wrappers().Backend().ContentBlocks().LinkSelectorWrapper().VerifyCorrectWebAddress(WebAddress);
            BATFeather.Wrappers().Backend().ContentBlocks().LinkSelectorWrapper().VerifyCorrectTextToDisplay(Content);
            BATFeather.Wrappers().Backend().ContentBlocks().LinkSelectorWrapper().PressCancel();

            BATFeather.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().SelectContentInEditableArea();
            BATFeather.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().PressSpecificButton("Bold");
            BATFeather.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().OpenLinkSelector();
            BATFeather.Wrappers().Backend().ContentBlocks().LinkSelectorWrapper().VerifyCorrectWebAddress(WebAddress);
            BATFeather.Wrappers().Backend().ContentBlocks().LinkSelectorWrapper().VerifyTextToDisplayIsNotVisible();
            BATFeather.Wrappers().Backend().ContentBlocks().LinkSelectorWrapper().OpenInNewWindow();
            BATFeather.Wrappers().Backend().ContentBlocks().LinkSelectorWrapper().InsertLink();
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
        private const string HtmlContent = "<a href=\"http://google.bg\">Test content</a>";
        private const string Content = "Test content";
        private const string WebAddress = "www.google.bg";
    }
}
