using System;
using System.Collections.Generic;
using Feather.Widgets.TestUI.Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telerik.Sitefinity.Frontend.TestUtilities;

namespace FeatherWidgets.TestUI.TestCases.Navigation
{
    /// <summary>
    /// Test for NavigationWidgetCustomSelectionOfPagesWithExternalUrls
    /// </summary>
    [TestClass]
    public class NavigationWidgetCustomSelectionOfPagesWithExternalUrls_ : FeatherTestCase
    {
        /// <summary>
        /// UI test NavigationWidgetCustomSelectionOfPagesWithExternalUrls in Bootstrap template
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.FeatherTeam),
        TestCategory(FeatherTestCategories.PagesAndContent),
        TestCategory(FeatherTestCategories.Navigation),
        TestCategory(FeatherTestCategories.Bootstrap)]
        public void NavigationWidgetCustomSelectionOfPagesWithExternalUrls()
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
            BATFeather.Wrappers().Backend().Widgets().SelectorsWrapper().OpenExternalUrlsTab();
            BATFeather.Wrappers().Backend().Widgets().SelectorsWrapper().PressAddExternalUrlsButton();
            BATFeather.Wrappers().Backend().Widgets().SelectorsWrapper().EnterExternalUrlsInfo(FirstExternalPageTitle, FirstExternalPageUrl);
            BATFeather.Wrappers().Backend().Widgets().SelectorsWrapper().PressAddExternalUrlsButton();
            BATFeather.Wrappers().Backend().Widgets().SelectorsWrapper().EnterExternalUrlsInfo(SecondExternalPageTitle, SecondExternalPageUrl, false);
            BATFeather.Wrappers().Backend().Widgets().SelectorsWrapper().CheckNotificationInSelectedTab(2);
            BATFeather.Wrappers().Backend().Widgets().SelectorsWrapper().DoneSelecting();
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().VerifySelectedItemsFromHierarchicalSelector(this.selectedPages);

            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SaveChanges();
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();
            BAT.Macros().NavigateTo().CustomPage("~/" + NavigationPage.ToLower());
            BATFeather.Wrappers().Frontend().Navigation().NavigationWrapper().VerifyNavigationOnThePageFrontend(navTemplateClass, this.selectedPages);

            BAT.Macros().NavigateTo().Pages();
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(NavigationPage);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(WidgetName);
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().ClickSelectButton();
            BATFeather.Wrappers().Backend().Widgets().SelectorsWrapper().CheckNotificationInSelectedTab(2);
            BATFeather.Wrappers().Backend().Widgets().SelectorsWrapper().OpenExternalUrlsTab();
            BATFeather.Wrappers().Backend().Widgets().SelectorsWrapper().RemoveExternalUrl();
            BATFeather.Wrappers().Backend().Widgets().SelectorsWrapper().SelectOpenInNewWindowOption();
            BATFeather.Wrappers().Backend().Widgets().SelectorsWrapper().CheckNotificationInSelectedTab(1);
            BATFeather.Wrappers().Backend().Widgets().SelectorsWrapper().DoneSelecting();
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().VerifySelectedItemsFromHierarchicalSelector(new string[] { SecondExternalPageTitle });
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SaveChanges();
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();

            BAT.Macros().NavigateTo().CustomPage("~/" + NavigationPage.ToLower());
            BATFeather.Wrappers().Frontend().Navigation().NavigationWrapper().VerifyNavigationOnThePageFrontend(navTemplateClass, new string[] { SecondExternalPageTitle });
            BATFeather.Wrappers().Frontend().Navigation().NavigationWrapper().VerifyCreatedPageWithExternalUrl(SecondExternalPageTitle, SecondExternalPageUrl, true);
        }

        /// <summary>
        /// Performs clean up and clears all data created by the test.
        /// </summary>
        protected override void ServerCleanup()
        {
            BAT.Arrange(ArrangementClass).ExecuteTearDown();
        }

        private const string ArrangementClass = "NavigationWidgetCustomSelectionOfPagesWithExternalUrls";
        private const string ArrangementMethod = "SetUp";
        private const string NavigationPage = "PageWithNavigationWidget";
        private const string WidgetName = "Navigation";
        private const string NavWidgetDisplayMode = "Custom selection of pages...";
        private const string FirstExternalPageTitle = "FirstExternalPage";
        private const string SecondExternalPageTitle = "SecondExternalPage";
        private const string FirstExternalPageUrl = "http://www.google.com";
        private const string SecondExternalPageUrl = "http://www.weather.com";
        private readonly string[] selectedPages = new string[] { FirstExternalPageTitle, SecondExternalPageTitle };        
        private readonly string[] pagesOrderFrontend = new string[] { SecondExternalPageTitle, FirstExternalPageTitle };
        private const int SelectedItemsCount = 2;

        private readonly Dictionary<int, int> reorderedIndexMapping = new Dictionary<int, int>()
        {
            { 1, 0 }
        };
    }
}