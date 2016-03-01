﻿using System;
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
    /// VerifySearchResults_SortingOnFrontend_ test class.
    /// </summary>
    [TestClass]
    public class VerifySearchResults_SortingOnFrontend_ : FeatherTestCase
    {
        /// <summary>
        /// UI test VerifySearchResults_SortingOnFrontend
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.FeatherTeam),
        TestCategory(FeatherTestCategories.PagesAndContent),
        TestCategory(FeatherTestCategories.Search)]
        public void VerifySearchResults_SortingOnFrontend()
        {
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
            BATFeather.Wrappers().Backend().Search().SearchResultsWrapper().SelectSortingOption(SortingOrder);
            BATFeather.Wrappers().Backend().Search().SearchResultsWrapper().AllowUsersToSortResults();
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SaveChanges();
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();

            BAT.Macros().NavigateTo().CustomPage("~/" + SearchPage.ToLower(), true, this.Culture);
            BATFeather.Wrappers().Frontend().Search().SearchWrapper().EnterSearchText(SearchText);
            BATFeather.Wrappers().Frontend().Search().SearchWrapper().ClickSearchButton(ResultsPage.ToLower());
            BATFeather.Wrappers().Frontend().Search().SearchWrapper().VerifySearchResultsLabel(2, SearchText);
            BATFeather.Wrappers().Frontend().Search().SearchWrapper().VerifySearchResultsList(NewsTitle2, NewsTitle1);

            BATFeather.Wrappers().Frontend().Search().SearchWrapper().SelectSortingOption(NewSortingOrder);
            BATFeather.Wrappers().Frontend().Search().SearchWrapper().VerifySearchResultsLabel(2, SearchText);
            BATFeather.Wrappers().Frontend().Search().SearchWrapper().VerifySearchResultsList(NewsTitle1, NewsTitle2);
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
        private const string SearchIndexName = "VerifySearchResults_SortingOnFrontend";

        private const string SearchText = "news";
        private const string NewsTitle1 = "test news";
        private const string NewsTitle2 = "another news";
        private const string SortingOrder = "Newest first";
        private const string NewSortingOrder = "Oldest first";
    }
}
