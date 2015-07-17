using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Feather.Widgets.TestUI.Framework;
using FeatherWidgets.TestUI.TestCases;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telerik.Sitefinity.Frontend.TestUtilities;

namespace FeatherWidgets.TestUI.TestCases.DynamicWidgets
{
    /// <summary>
    /// This is test class for SearchForSingleItemPageAndVerifyBreadcrumb.
    /// </summary>
    [TestClass]
    public class SearchInDynamicWidgetForSingleItemPageAndVerifyBreadcrumb_ : FeatherTestCase
    {
        /// <summary>
        /// UI test SearchInDynamicWidgetForSingleItemPageAndVerifyBreadcrumb
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.FeatherTeam),
        TestCategory(FeatherTestCategories.DynamicWidgets)]
        public void SearchInDynamicWidgetForSingleItemPageAndVerifyBreadcrumb()
        {
            BAT.Macros().NavigateTo().Pages();
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(PageName);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().AddMvcWidgetHybridModePage(WidgetName);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(WidgetName);
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SwitchToSingleItemSettingsTab();
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SelectExistingPage();
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().ClickSelectButton();

            //// Search for not existing page
            BATFeather.Wrappers().Backend().Widgets().SelectorsWrapper().SearchItemByTitle(SearchNotExistingText);
            BATFeather.Wrappers().Backend().Widgets().SelectorsWrapper().WaitForItemsToAppear(NotExistingSearchResultsCount);
            BATFeather.Wrappers().Backend().Widgets().SelectorsWrapper().VerifyNoItemsFound();

            BATFeather.Wrappers().Backend().Widgets().SelectorsWrapper().SearchItemByTitle(SearchText);
            BATFeather.Wrappers().Backend().Widgets().SelectorsWrapper().WaitForItemsToAppear(SearchResultsCount);
            BATFeather.Wrappers().Backend().Widgets().SelectorsWrapper().VerifyPageStatusAndIconAfterSearch(SearchText, "Published");
            BATFeather.Wrappers().Backend().Widgets().SelectorsWrapper().CheckBreadcrumbAfterSearchInFlatSelector(SearchText, ResultPageBreadcrumb);
            BATFeather.Wrappers().Backend().Widgets().SelectorsWrapper().SelectItemsInFlatSelector(SearchText);
            BATFeather.Wrappers().Backend().Widgets().SelectorsWrapper().DoneSelecting();
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().VerifySelectedItemsFromHierarchicalSelector(this.selectedItemsFromPageSelector);

            //// save changes and reopen widget designer to ensure selection is saved
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SaveChanges();
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(WidgetName);
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SwitchToSingleItemSettingsTab();
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().VerifySelectedItemsFromHierarchicalSelector(this.selectedItemsFromPageSelector);
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SaveChanges();
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

        private const string PageName = "TestPage";
        private const string WidgetName = "Press Articles";
        private const string SearchNotExistingText = "FeatherPage";
        private const int NotExistingSearchResultsCount = 0;
        private const string SearchText = "ChildPage6";
        private const string ResultPageBreadcrumb = "Under TestPage > ChildPage0 > ChildPage1 > ChildPage2 > ChildPage3 > ChildPage4 > ChildPage5";
        private const int SearchResultsCount = 1;
        private readonly string[] selectedItemsFromPageSelector = new string[] { "TestPage > ChildPage0 > ChildPage1 > ChildPage2 > ChildPage3 > ChildPage4 > ChildPage5 > ChildPage6" };
    }
}
