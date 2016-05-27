using System;
using ArtOfTest.WebAii.Core;
using Feather.Widgets.TestUI.Framework;
using Feather.Widgets.TestUI.Framework.Framework.Wrappers.Backend.Widgets;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telerik.Sitefinity.TestUI.Framework.Utilities;

namespace FeatherWidgets.TestUI.TestCases.Search
{
    /// <summary>
    /// VerifyNoLimitAndPaginationListSettingsOnFrontendPageForSearchResultsWidget_ test class.
    /// </summary>
    [TestClass]
    public class VerifyNoLimitAndPaginationListSettingsOnFrontendPageForSearchResultsWidget_ : FeatherTestCase
    {
        /// <summary>
        /// UI test VerifyNoLimitAndPaginationListSettingsOnFrontendPageForSearchResultsWidget
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.SitefinityTeam4),
        TestCategory(FeatherTestCategories.PagesAndContent),
        TestCategory(FeatherTestCategories.Search)]
        public void VerifyNoLimitAndPaginationListSettingsOnFrontendPageForSearchResultsWidget()
        {
            RuntimeSettingsModificator.ExecuteWithClientTimeout(800000, () => BAT.Macros().NavigateTo().CustomPage("~/sitefinity/pages", true, null, new HtmlFindExpression("class=~sfMain")));
            BAT.Macros().NavigateTo().Pages(this.Culture);
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(SearchPage);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().AddMvcWidgetHybridModePage(SearchBoxWidget);

            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(SearchBoxWidget);
            BATFeather.Wrappers().Backend().Search().SearchBoxWrapper().SelectSearchIndex(SearchIndexName);
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().ClickSelectButton();
            BATFeather.Wrappers().Backend().Widgets().SelectorsWrapper().WaitForItemsToAppear(3);
            BATFeather.Wrappers().Backend().Widgets().SelectorsWrapper().SelectItemsInFlatSelector(ResultsPage);
            BATFeather.Wrappers().Backend().Widgets().SelectorsWrapper().DoneSelecting();
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().VerifySelectedItemsFromFlatSelector(new string[] { ResultsPage });
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SaveChanges();
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();

            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(ResultsPage);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().AddMvcWidgetHybridModePage(SearchResultsWidget);

            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(SearchResultsWidget);
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().VerifyPageValue("20", "Paging");
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().VerifyPageValue("20", "Limit");
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().ChangePagingOrLimitValue("2", "Paging");
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().VerifyPageValue("2", "Paging");
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().VerifyPageValue("20", "Limit");
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SaveChanges();
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(SearchResultsWidget);
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().VerifyPageValue("2", "Paging");
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().VerifyPageValue("20", "Limit");
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SelectRadioButtonOption(WidgetDesignerRadioButtonIds.allItems);
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().VerifyCheckedRadioButtonOption(WidgetDesignerRadioButtonIds.allItems);
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SaveChanges();
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(SearchResultsWidget);
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().VerifyCheckedRadioButtonOption(WidgetDesignerRadioButtonIds.allItems);
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().VerifyPageValue("2", "Paging");
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().VerifyPageValue("20", "Limit");
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().PressCancelButton();
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();

            BAT.Macros().NavigateTo().CustomPage("~/" + SearchPage.ToLower(), true, this.Culture);
            BATFeather.Wrappers().Frontend().Search().SearchWrapper().EnterSearchText(SearchText);
            BATFeather.Wrappers().Frontend().Search().SearchWrapper().ClickSearchButton(ResultsPage.ToLower());
            BATFeather.Wrappers().Frontend().Search().SearchWrapper().VerifySearchResultsLabel(5, SearchText);
            BATFeather.Wrappers().Frontend().Search().SearchWrapper().VerifySearchResultsList(NewsTitle5, NewsTitle4, NewsTitle3, NewsTitle2, NewsTitle1);
            BAT.Macros().NavigateTo().Pages(this.Culture);
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
        /// 
        protected override void ServerCleanup()
        {
            BAT.Arrange(this.TestName).ExecuteTearDown();
        }

        private const string SearchPage = "SearchPage";
        private const string ResultsPage = "ResultsPage";
        private const string SearchBoxWidget = "Search box";
        private const string SearchResultsWidget = "Search results";
        private const string SearchIndexName = "VerifySearchResults";

        private const string SearchText = "NewsTitle";
        private const string NewsTitle1 = "NewsTitle 1";
        private const string NewsTitle2 = "NewsTitle 2";
        private const string NewsTitle3 = "NewsTitle 3";
        private const string NewsTitle4 = "NewsTitle 4";
        private const string NewsTitle5 = "NewsTitle 5";
    }
}
