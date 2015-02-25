using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Feather.Widgets.TestUI.Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telerik.Sitefinity.Frontend.TestUtilities;

namespace FeatherWidgets.TestUI.TestCases.Search
{
    /// <summary>
    /// VerifySearchResults_ApplyCssClass_ test class.
    /// </summary>
    [TestClass]
    public class VerifySearchResults_ApplyCssClass_ : FeatherTestCase
    {
        /// <summary>
        /// UI test VerifySearchResults_ApplyCssClass
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.Team7),
        TestCategory(FeatherTestCategories.PagesAndContent),
        TestCategory(FeatherTestCategories.Search)]
        public void VerifySearchResults_ApplyCssClass()
        {
            BAT.Macros().NavigateTo().Pages();
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(SearchPage);

            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().AddMvcWidgetHybridModePage(SearchBoxWidget);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().AddMvcWidgetHybridModePage(SearchResultsWidget);

            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(SearchBoxWidget);
            //// BATFeather.Wrappers().Backend().Search().SearchBoxWrapper().VerifyWhatsThis();
            BATFeather.Wrappers().Backend().Search().SearchBoxWrapper().SelectSearchIndex(SearchIndexName);
            BATFeather.Wrappers().Backend().Search().SearchBoxWrapper().VerifyWhereToDisplaySearchResultsLabel();
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().ClickSelectButton();
            BATFeather.Wrappers().Backend().Widgets().SelectorsWrapper().WaitForItemsToAppear(2);
            BATFeather.Wrappers().Backend().Widgets().SelectorsWrapper().SelectItemsInFlatSelector(SearchPage);
            BATFeather.Wrappers().Backend().Widgets().SelectorsWrapper().DoneSelecting();
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().VerifySelectedItemsFromFlatSelector(new string[] { SearchPage });
            BATFeather.Wrappers().Backend().Search().SearchBoxWrapper().SelectTemplate(SearchBoxTemplate);

            BATFeather.Wrappers().Backend().Search().SearchBoxWrapper().ExpandMoreOptions();
            BATFeather.Wrappers().Backend().Search().SearchBoxWrapper().ApplyCssClasses(SearchBoxCssClassesToApply);
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SaveChanges();

            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(SearchResultsWidget);
            BATFeather.Wrappers().Backend().Search().SearchResultsWrapper().VerifyUsePaging();
            BATFeather.Wrappers().Backend().Search().SearchResultsWrapper().VerifyUseLimit();
            BATFeather.Wrappers().Backend().Search().SearchResultsWrapper().VerifyNoLimit();
            BATFeather.Wrappers().Backend().Search().SearchResultsWrapper().SelectSortingOption(SortResultsOption);
            BATFeather.Wrappers().Backend().Search().SearchResultsWrapper().AllowUsersToSortResults();
            BATFeather.Wrappers().Backend().Search().SearchResultsWrapper().SelectTemplate(SearchResultsTemplate);

            BATFeather.Wrappers().Backend().Search().SearchBoxWrapper().ExpandMoreOptions();
            BATFeather.Wrappers().Backend().Search().SearchBoxWrapper().ApplyCssClasses(SearchResultsCssClassesToApply);
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SaveChanges();

            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();

            BAT.Macros().NavigateTo().CustomPage("~/" + SearchPage.ToLower());
            BATFeather.Wrappers().Frontend().Search().SearchWrapper().VerifySearchBoxCssClass(SearchBoxCssClassesToApply);
            BATFeather.Wrappers().Frontend().Search().SearchWrapper().EnterSearchText(SearchText);
            BATFeather.Wrappers().Frontend().Search().SearchWrapper().ClickSearchButton();
            BATFeather.Wrappers().Frontend().Search().SearchWrapper().VerifySearchResultsCssClass(SearchResultsCssClassesToApply);
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
        private const string SearchText = "test";

        private const string SearchBoxCssClassesToApply = "testSearchBoxClass";
        private const string SearchResultsCssClassesToApply = "testSearchResultsClass";
        private const string SearchBoxTemplate = "SearchBox";
        private const string SearchResultsTemplate = "SearchResults";
        private const string SortResultsOption = "Most relevant results on top";
    }
}
