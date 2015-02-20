using System;
using System.Collections.Generic;
using Feather.Widgets.TestUI.Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FeatherWidgets.TestUI.TestCases.Navigation
{
    [TestClass]
    public class NavigationWidgetCustomSelectionOfPages : FeatherTestCase
    {
        /// <summary>
        /// UI test NavigationWidgetCustomSelectionOfPages in Bootstrap template
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.Team2),
        TestCategory(FeatherTestCategories.Navigation),
        TestCategory(FeatherTestCategories.Bootstrap)]
        public void NavigationWidgetCustomSelectionOfPagesPageBootstrap()
        {
            string pageTemplateName = "Bootstrap.default";
            string navTemplateClass = "nav navbar-nav";

            BAT.Macros().User().EnsureAdminLoggedIn();
            BAT.Arrange(ArrangementClass).AddParameter("templateName", pageTemplateName).ExecuteArrangement(ArrangementMethod);
            BAT.Macros().NavigateTo().Pages();
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(NavigationPage);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(WidgetName);
            BATFeather.Wrappers().Backend().Navigation().NavigationWidgetEditWrapper().SelectNavigationWidgetDisplayMode(NavWidgetDisplayMode);
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().ClickSelectButton();
            BATFeather.Wrappers().Backend().Widgets().SelectorsWrapper().SelectItemsInHierarchicalSelector(this.pagesToSelect);
            BATFeather.Wrappers().Backend().Widgets().SelectorsWrapper().DoneSelecting();
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().VerifySelectedItemsFromHierarchicalSelector(this.selectedPages);

            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().ClickSelectButton();
            BATFeather.Wrappers().Backend().Widgets().SelectorsWrapper().WaitForItemsToAppear(SelectedItemsCount);
            BATFeather.Wrappers().Backend().Widgets().SelectorsWrapper().ReorderSelectedItems(this.expectedOrderOfItems, this.selectedPages, this.reorderedIndexMapping);
            BATFeather.Wrappers().Backend().Widgets().SelectorsWrapper().DoneSelecting();
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().VerifySelectedItemsFromHierarchicalSelector(this.expectedOrderOfItems);

            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SaveChanges();
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();

            BAT.Macros().NavigateTo().CustomPage("~/" + NavigationPage.ToLower());
            BATFeather.Wrappers().Frontend().Navigation().NavigationWrapper().VerifyNavigationOnThePageFrontend(navTemplateClass, this.pagesOrderFrontend);
            BATFeather.Wrappers().Frontend().Navigation().NavigationWrapper().VerifyPagesNotPresentFrontEndNavigation(navTemplateClass, this.pagesNotPresentOnFrontend);
        }

        /// <summary>
        /// Performs clean up and clears all data created by the test.
        /// </summary>
        protected override void ServerCleanup()
        {
            BAT.Arrange(ArrangementClass).ExecuteTearDown();
        }

        private const string ArrangementClass = "NavigationWidgetCustomSelectionOfPages";
        private const string ArrangementMethod = "SetUp";
        private const string NavigationPage = "PageWithNavigationWidget";
        private const string WidgetName = "Navigation";
        private const string NavWidgetDisplayMode = "Custom selection of pages...";
        private readonly string[] pagesToSelect = new string[] { "ChildPage1", "RootPage2" };
        private readonly string[] selectedPages = new string[] { "RootPage1 > ChildPage1", "RootPage2" };
        private readonly string[] pagesNotPresentOnFrontend = new string[] { "RootPage1" };
        private readonly string[] expectedOrderOfItems = new string[] { "RootPage2", "RootPage1 > ChildPage1" };
        private readonly string[] pagesOrderFrontend = new string[] { "RootPage2", "ChildPage1" };
        private const int SelectedItemsCount = 2;

        private readonly Dictionary<int, int> reorderedIndexMapping = new Dictionary<int, int>()
        {
            { 1, 0 }
        };
    }
}
