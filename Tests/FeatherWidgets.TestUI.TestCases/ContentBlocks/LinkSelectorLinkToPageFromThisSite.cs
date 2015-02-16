using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Feather.Widgets.TestUI.Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FeatherWidgets.TestUI.TestCases.ContentBlocks
{
    /// <summary>
    /// This is a test class with tests related to Link selector Page from this site option.
    /// </summary>
    [TestClass]
    public class LinkSelectorLinkToPageFromThisSite : FeatherTestCase
    {
        /// <summary>
        /// UI test LinkSelectorInsertLinkToPageFromThisSite
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.Team2),
        TestCategory(FeatherTestCategories.PagesAndContent),
        TestCategory(FeatherTestCategories.ContentBlock),
        TestCategory(FeatherTestCategories.LinkSelector)]
        public void LinkSelectorInsertLinkToPageFromThisSite()
        {
            BAT.Macros().NavigateTo().Pages();
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(LinkSelectorPageName);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(WidgetName);
            BATFeather.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().OpenLinkSelector();
            BATFeather.Wrappers().Backend().ContentBlocks().LinkSelectorWrapper().SwitchToSelectedTab(SelectedTabName);

            Assert.IsFalse(BATFeather.Wrappers().Backend().ContentBlocks().LinkSelectorWrapper().IsInsertLinkButtonEnabled());
            BATFeather.Wrappers().Backend().Widgets().SelectorsWrapper().SelectItemsInHierarchicalSelector(this.selectedPageName);
            BATFeather.Wrappers().Backend().ContentBlocks().LinkSelectorWrapper().VerifyCorrectTextToDisplay(InsertedPageName, TabIndex);
            BATFeather.Wrappers().Backend().ContentBlocks().LinkSelectorWrapper().SelectOpenInNewWindowOption(TabIndex);
            BATFeather.Wrappers().Backend().ContentBlocks().LinkSelectorWrapper().InsertLink();

            BATFeather.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().VerifyContentBlockTextDesignMode(InsertedPageName);
            BATFeather.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().SwitchToHtmlView();
            BATFeather.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().VerifyContentInHtmlEditableArea(this.HtmlContent());
            BATFeather.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().SaveChanges();
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();
            BAT.Macros().NavigateTo().CustomPage("~/" + LinkSelectorPageName.ToLower());
            BATFeather.Wrappers().Frontend().CommonWrapper().VerifySelectedAnchorLink(InsertedPageName, ExpectedUrl, true);
        }

        /// <summary>
        /// Login admin user and creates page with content block widget.
        /// Creates a parent and child page in order to be used in the link selector.
        /// </summary>
        protected override void ServerSetup()
        {
            BAT.Macros().User().EnsureAdminLoggedIn();
            BAT.Arrange(ArrangementClassName).ExecuteSetUp();
        }

        /// <summary>
        /// Deletes all pages.
        /// </summary>
        protected override void ServerCleanup()
        {
            BAT.Arrange(ArrangementClassName).ExecuteTearDown();
        }

        private string HtmlContent()
        {
            string siteMapRootNodeId = BAT.Arrange(ArrangementClassName).ExecuteArrangement("GetSiteMapRootNodeId").Result.Values["siteMapRootNodeId"];
            string childPageId = BAT.Arrange(ArrangementClassName).ExecuteArrangement("GetChildPageId").Result.Values["childPageId"];

            string content = "<a href=\"/" + ParentPageName.ToLower() + 
                "/" + InsertedPageName.ToLower() + 
                "\" sfref=\"[" + siteMapRootNodeId + "]" + childPageId + 
                "\" target=\"_blank\">" + InsertedPageName + "</a>";

            return content;
        }

        private const string ArrangementClassName = "LinkSelectorLinkToPageFromThisSite";
        private const string LinkSelectorPageName = "PageWithContentBlockAndLinkSelector";
        private const string WidgetName = "ContentBlock";
        private const string SelectedTabName = "Page from this site";
        private const string InsertedPageName = "ChildPage";
        private const string ParentPageName = "ParentPage";
        private readonly string[] selectedPageName = new string[] { "ChildPage" };
        private const int TabIndex = 2;
        private const string ExpectedUrl = "parentpage/childpage";
    }
}
