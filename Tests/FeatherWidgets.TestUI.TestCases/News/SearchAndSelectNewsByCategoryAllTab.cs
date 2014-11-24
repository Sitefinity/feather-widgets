using System;
using Feather.Widgets.TestUI.Framework;
using FeatherWidgets.TestUI.TestCases;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FeatherWidgets.TestUI
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
        Microsoft.VisualStudio.TestTools.UnitTesting.Owner("Sitefinity Team 7"),
        TestCategory(FeatherTestCategories.PagesAndContent),
        TestCategory(FeatherTestCategories.NewsSelectors)]
        
        public void SearchAndSelectNewsByCategoryAllTab()
        {
            BAT.Macros().NavigateTo().Pages();
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(PageName);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(WidgetName);
            BATFeather.Wrappers().Backend().News().NewsWidgetEditContentScreenWrapper().SelectWhichNewsToDisplay(WhichNewsToDisplay);
            BATFeather.Wrappers().Backend().News().NewsWidgetEditContentScreenWrapper().SelectCheckBox(TaxonomyName);
            BATFeather.Wrappers().Backend().News().NewsWidgetEditContentScreenWrapper().ClickSelectButton();
            BATFeather.Wrappers().Backend().News().NewsWidgetEditContentScreenWrapper().SearchItemByTitle("C");
            BATFeather.Wrappers().Backend().News().NewsWidgetEditContentScreenWrapper().WaitForItemsToAppear(24);

            BATFeather.Wrappers().Backend().News().NewsWidgetEditContentScreenWrapper().CheckBreadcrumbAfterSearchInHierarchicalSelector(breadcrumbName);

            BATFeather.Wrappers().Backend().News().NewsWidgetEditContentScreenWrapper().SelectItemInMultipleSelector(TaxonTitle1, TaxonTitle2);
            BATFeather.Wrappers().Backend().News().NewsWidgetEditContentScreenWrapper().DoneSelecting();

            BATFeather.Wrappers().Backend().News().NewsWidgetEditContentScreenWrapper().VerifySelectedItemInMultipleSelectors(new[] { "Category0 > Category1 > Category2 > Category3 > Category4 > Category5", "AnotherCategory0 > ... > AnotherCategory5 > AnotherCategory6" });

            BATFeather.Wrappers().Backend().News().NewsWidgetEditContentScreenWrapper().SaveChanges();
            foreach (var newsTitle in newsTitles)
            {
                BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().CheckWidgetContent(WidgetName, newsTitle);
            }
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();
            this.VerifyNewsOnTheFrontend();
        }

        /// <summary>
        /// Verify news widget on the frontend
        /// </summary>
        public void VerifyNewsOnTheFrontend()
        {
            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower());
            ActiveBrowser.WaitUntilReady();
            BATFeather.Wrappers().Frontend().News().NewsWrapper().VerifyNewsTitlesOnThePageFrontend(this.newsTitles);
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
        private const string breadcrumbName = "Under AnotherCategory0 > AnotherCategory1 > AnotherCategory2 > AnotherCategory3 > AnotherCategory4 > AnotherCategory5 > AnotherCategory6 > AnotherCategory7 > AnotherCategory8 > AnotherCategory9";
    }
}