using System;
using Feather.Widgets.TestUI.Framework;
using FeatherWidgets.TestUI.TestCases;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telerik.Sitefinity.Frontend.TestUtilities;

namespace FeatherWidgets.TestUI.TestCases.News
{
    /// <summary>
    /// SearchAndSelectNewsByCategoryAllTab test class.
    /// </summary>
    [TestClass]
    public class SearchAndSelectNewsByCategoryAllTab_ : FeatherTestCase
    {
        /// <summary>
        /// UI test SearchAndSelectNewsByCategoryAllTab
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.Team7),
        TestCategory(FeatherTestCategories.PagesAndContent),
        TestCategory(FeatherTestCategories.News),
        TestCategory(FeatherTestCategories.Selectors)]       
        public void SearchAndSelectNewsByCategoryAllTab()
        {
            BAT.Macros().NavigateTo().Pages();
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(PageName);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(WidgetName);
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SelectWhichItemsToDisplay(WhichNewsToDisplay);
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SelectCheckBox(TaxonomyName);
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().ClickSelectButton();
            BATFeather.Wrappers().Backend().Widgets().SelectorsWrapper().SearchItemByTitle("C");
            BATFeather.Wrappers().Backend().Widgets().SelectorsWrapper().WaitForItemsToAppear(24);

            BATFeather.Wrappers().Backend().Widgets().SelectorsWrapper().CheckBreadcrumbAfterSearchInHierarchicalSelector(BreadcrumbName, BreadcrumbFullName);

            BATFeather.Wrappers().Backend().Widgets().SelectorsWrapper().SelectItemsInFlatSelector(TaxonTitle1, TaxonTitle2);
            BATFeather.Wrappers().Backend().Widgets().SelectorsWrapper().DoneSelecting();

            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().VerifySelectedItemsFromHierarchicalSelector(new[] 
            { 
                "Category0 > Category1 > Category2 > Category3 > Category4 > Category5", 
                "AnotherCategory0 > AnotherCategory1 > AnotherCategory2 > AnotherCategory3 > AnotherCategory4 > AnotherCategory5 > AnotherCategory6" 
            });

            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SaveChanges();
            foreach (var newsTitle in this.newsTitles)
            {
                BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().CheckWidgetContent(WidgetName, newsTitle);
            }

            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();
            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower());
            Assert.IsTrue(BATFeather.Wrappers().Frontend().News().NewsWrapper().IsNewsTitlesPresentOnThePageFrontend(this.newsTitles));
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

        private const string PageName = "News";
        private const string TaxonTitle1 = "Category5";
        private const string TaxonTitle2 = "AnotherCategory6";
        private const string NewsTitle1 = "NewsTitle5";
        private const string NewsTitle2 = "NewsTitle18";
        private readonly string[] newsTitles = new string[] { NewsTitle2, NewsTitle1 };
        private const string WidgetName = "News";
        private const string WhichNewsToDisplay = "Narrow selection by...";
        private const string TaxonomyName = "Category";
        private const string BreadcrumbName = "Under AnotherCategory0 > ... > AnotherCategory9";
        private const string BreadcrumbFullName = "Under AnotherCategory0 > AnotherCategory1 > AnotherCategory2 > AnotherCategory3 > AnotherCategory4 > AnotherCategory5 > AnotherCategory6 > AnotherCategory7 > AnotherCategory8 > AnotherCategory9";
    }
}