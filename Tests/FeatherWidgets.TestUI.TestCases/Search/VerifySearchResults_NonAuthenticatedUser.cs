using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Feather.Widgets.TestUI.Framework;
using FeatherWidgets.TestUI.TestCases;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FeatherWidgets.TestUI.TestCases.Search
{
    /// <summary>
    /// VerifySearchResults_NonAuthenticatedUser_ test class.
    /// </summary>
    [TestClass]
    public class VerifySearchResults_NonAuthenticatedUser_ : FeatherTestCase
    {
        /// <summary>
        /// UI test VerifySearchResults_NonAuthenticatedUser
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.Team7),
        TestCategory(FeatherTestCategories.PagesAndContent), 
        TestCategory(FeatherTestCategories.Search)]
        public void VerifySearchResults_NonAuthenticatedUser()
        {
            BAT.Macros().NavigateTo().Pages();
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(SearchPage);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().AddMvcWidgetHybridModePage(SearchBoxWidget);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().AddMvcWidgetHybridModePage(SearchResultsWidget);

            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(SearchBoxWidget);
            BATFeather.Wrappers().Backend().Search().SearchBoxWrapper().SelectSearchIndex(SearchIndexName);
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().ClickSelectButton();
            BATFeather.Wrappers().Backend().Widgets().SelectorsWrapper().WaitForItemsToAppear(2);
            BATFeather.Wrappers().Backend().Widgets().SelectorsWrapper().SelectItemsInFlatSelector(SearchPage);
            BATFeather.Wrappers().Backend().Widgets().SelectorsWrapper().DoneSelecting();
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().VerifySelectedItemsFromFlatSelector(new string[] { SearchPage });
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SaveChanges();
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();

            BAT.Macros().NavigateTo().CustomPage("~/" + SearchPage.ToLower());
            BATFeather.Wrappers().Frontend().Search().SearchWrapper().EnterSearchText(NoResultsSearchText);
            BATFeather.Wrappers().Frontend().Search().SearchWrapper().ClickSearchButton();
            BATFeather.Wrappers().Frontend().Search().SearchWrapper().VerifySearchResultsLabel(0, NoResultsSearchText);
            BATFeather.Wrappers().Frontend().Search().SearchWrapper().EnterSearchText(SearchText1);
            BATFeather.Wrappers().Frontend().Search().SearchWrapper().ClickSearchButton();
            BATFeather.Wrappers().Frontend().Search().SearchWrapper().VerifySearchResultsLabel(1, SearchText1);
            BATFeather.Wrappers().Frontend().Search().SearchWrapper().VerifySearchResultsList(NewsTitle1);

            BAT.Macros().User().LogOut();
            BAT.Macros().NavigateTo().CustomPage("~/" + SearchPage.ToLower());
            BATFeather.Wrappers().Frontend().Search().SearchWrapper().EnterSearchText(NoResultsSearchText);
            BATFeather.Wrappers().Frontend().Search().SearchWrapper().PressEnter();
            BATFeather.Wrappers().Frontend().Search().SearchWrapper().VerifySearchResultsLabel(0, NoResultsSearchText);
            BATFeather.Wrappers().Frontend().Search().SearchWrapper().EnterSearchText(SearchText2);
            BATFeather.Wrappers().Frontend().Search().SearchWrapper().PressEnter();
            BATFeather.Wrappers().Frontend().Search().SearchWrapper().VerifySearchResultsLabel(1, SearchText2);
            BATFeather.Wrappers().Frontend().Search().SearchWrapper().VerifySearchResultsList(NewsTitle2);
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
        private const string SearchBoxWidget = "Search box";
        private const string SearchResultsWidget = "Search results";
        private const string SearchIndexName = "news index";

        private const string NoResultsSearchText = "events";
        private const string SearchText1 = "test";
        private const string SearchText2 = "another";
        private const string NewsTitle1 = "test news";
        private const string NewsTitle2 = "another news";
    }
}
