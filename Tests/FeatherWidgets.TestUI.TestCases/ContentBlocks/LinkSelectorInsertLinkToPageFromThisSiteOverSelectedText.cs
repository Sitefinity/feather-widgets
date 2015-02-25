using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Feather.Widgets.TestUI.Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telerik.Sitefinity.Frontend.TestUtilities;

namespace FeatherWidgets.TestUI.TestCases.ContentBlocks
{
    /// <summary>
    /// This is a test class with tests related to Link selector Page from this site option over selected text.
    /// </summary>
    [TestClass]
    public class LinkSelectorInsertLinkToPageFromThisSiteOverSelectedText_ : FeatherTestCase
    {
        /// <summary>
        /// UI test LinkSelectorInsertLinkToPageFromThisSiteOverSelectedText
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.Team2),
        TestCategory(FeatherTestCategories.PagesAndContent),
        TestCategory(FeatherTestCategories.ContentBlock),
        TestCategory(FeatherTestCategories.LinkSelector)]
        public void LinkSelectorInsertLinkToPageFromThisSiteOverSelectedText()
        {
            BAT.Macros().NavigateTo().Pages();
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(LinkSelectorPageName);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(WidgetName);
            BATFeather.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().SelectAllContentInEditableArea();
            BATFeather.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().OpenLinkSelector();
            BATFeather.Wrappers().Backend().ContentBlocks().LinkSelectorWrapper().SwitchToSelectedTab(SelectedTabName);

            Assert.IsFalse(BATFeather.Wrappers().Backend().ContentBlocks().LinkSelectorWrapper().IsInsertLinkButtonEnabled());
            BATFeather.Wrappers().Backend().Widgets().SelectorsWrapper().VerifyPageStatusAndIcon(LinkSelectorPageName, "Locked");
            BATFeather.Wrappers().Backend().Widgets().SelectorsWrapper().VerifyPageStatusAndIcon(ParentPageName, "Published");

            BATFeather.Wrappers().Backend().Widgets().SelectorsWrapper().SearchItemByTitle(SearchText);
            BATFeather.Wrappers().Backend().Widgets().SelectorsWrapper().WaitForItemsToAppear(SearchResultsCount);
            BATFeather.Wrappers().Backend().Widgets().SelectorsWrapper().VerifyNoItemsFound();

            BATFeather.Wrappers().Backend().Widgets().SelectorsWrapper().SearchItemByTitle(NewSearchText);
            BATFeather.Wrappers().Backend().Widgets().SelectorsWrapper().WaitForItemsToAppear(NewSearchResultsCount);
            BATFeather.Wrappers().Backend().Widgets().SelectorsWrapper().VerifyPageStatusAndIconAfterSearch(NewSearchText, "Published");
            BATFeather.Wrappers().Backend().Widgets().SelectorsWrapper().CheckBreadcrumbAfterSearchInFlatSelector(NewSearchText, ResultPageBreadcrumb);
            BATFeather.Wrappers().Backend().Widgets().SelectorsWrapper().SelectItemsInFlatSelector(NewSearchText);

            BATFeather.Wrappers().Backend().ContentBlocks().LinkSelectorWrapper().VerifyCorrectTextToDisplay(TextToDisplay, TabIndex);
            BATFeather.Wrappers().Backend().ContentBlocks().LinkSelectorWrapper().InsertLink();

            BATFeather.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().VerifyContentBlockTextDesignMode(TextToDisplay);
            BATFeather.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().SwitchToHtmlView();
            BATFeather.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().VerifyContentInHtmlEditableArea(this.HtmlContent());
            BATFeather.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().SaveChanges();
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();
            BAT.Macros().NavigateTo().CustomPage("~/" + LinkSelectorPageName.ToLower());
            BATFeather.Wrappers().Frontend().CommonWrapper().VerifySelectedAnchorLink(TextToDisplay, ExpectedUrl);
        }

        /// <summary>
        /// Login admin user and creates page with content block widget.
        /// Creates a parent and child page in order to be used in the link selector.
        /// </summary>
        protected override void ServerSetup()
        {
            BAT.Macros().User().EnsureAdminLoggedIn();
            BAT.Arrange(this.TestName).ExecuteSetUp();
        }

        /// <summary>
        /// Deletes all pages.
        /// </summary>
        protected override void ServerCleanup()
        {
            BAT.Arrange(this.TestName).ExecuteTearDown();
        }

        private string HtmlContent()
        {
            string siteMapRootNodeId = BAT.Arrange(this.TestName).ExecuteArrangement("GetSiteMapRootNodeId").Result.Values["siteMapRootNodeId"];
            string childPageId = BAT.Arrange(this.TestName).ExecuteArrangement("GetChildPageId").Result.Values["childPageId"];

            string content = "<a href=\"/" + ParentPageName.ToLower() + 
                "/" + InsertedPageName.ToLower() + 
                "\" sfref=\"[" + siteMapRootNodeId + "]" + childPageId +
                "\">" + TextToDisplay + "</a>";

            return content;
        }

        private const string LinkSelectorPageName = "PageWithContentBlockAndLinkSelector";
        private const string WidgetName = "ContentBlock";
        private const string SelectedTabName = "Page from this site";
        private const string InsertedPageName = "ChildPage";
        private const string ParentPageName = "ParentPage";
        private const int TabIndex = 2;
        private const string ExpectedUrl = "parentpage/childpage";
        private const string TextToDisplay = "Test content";
        private const string SearchText = "FeatherPage";
        private const int SearchResultsCount = 0;
        private const string NewSearchText = "ChildPage";
        private const string ResultPageBreadcrumb = "Under ParentPage";
        private const int NewSearchResultsCount = 1;
    }
}
