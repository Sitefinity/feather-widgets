using System;
using Feather.Widgets.TestUI.Framework;
using FeatherWidgets.TestUI.TestCases;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FeatherWidgets.TestUI.TestCases.DynamicWidgets
{
    /// <summary>
    /// SearchAndSelectDynamicItemsByCategoryAllTab test class.
    /// </summary>
    [TestClass]
    public class SearchAndSelectDynamicItemsByCategoryAllTab_ : FeatherTestCase
    {
        /// <summary>
        /// UI test SearchAndSelectDynamicItemsByCategoryAllTab
        /// </summary>
        [TestMethod,
        Owner("Sitefinity Team 7"),
        TestCategory(FeatherTestCategories.DynamicWidgets)]       
        public void SearchAndSelectDynamicItemsByCategoryAllTab()
        {
            BAT.Macros().NavigateTo().Pages();
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(PageName);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(WidgetName);
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerContentScreenWrapper().SelectWhichItemsToDisplay(WhichNewsToDisplay);
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerContentScreenWrapper().SelectCheckBox(TaxonomyName);
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerContentScreenWrapper().ClickSelectButton();
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerContentScreenWrapper().SearchItemByTitle("C");
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerContentScreenWrapper().WaitForItemsToAppear(24);

            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerContentScreenWrapper().CheckBreadcrumbAfterSearchInHierarchicalSelector(BreadcrumbName, BreadcrumbFullName);

            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerContentScreenWrapper().SelectItemsInFlatSelector(TaxonTitle1, TaxonTitle2);
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerContentScreenWrapper().DoneSelecting();

            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerContentScreenWrapper().VerifySelectedItemsFromHierarchicalSelector(new[] { "Category0 > Category1 > Category2 > Category3 > Category4 > Category5", 
                "AnotherCategory0 > AnotherCategory1 > AnotherCategory2 > AnotherCategory3 > AnotherCategory4 > AnotherCategory5 > AnotherCategory6" });

            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerContentScreenWrapper().SaveChanges();
            foreach (var newsTitle in itemsTitles)
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
            BATFeather.Wrappers().Frontend().News().NewsWrapper().VerifyNewsTitlesOnThePageFrontend(this.itemsTitles);
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
        private const string WidgetName = "Press Articles MVC";     
        private const string TaxonTitle1 = "Category5";
        private const string TaxonTitle2 = "AnotherCategory6";
        private const string ItemsTitle1 = "Title5";
        private const string ItemsTitle2 = "Title18";
        private readonly string[] itemsTitles = new string[] { ItemsTitle2, ItemsTitle1 };
        private const string WhichNewsToDisplay = "Narrow selection by...";
        private const string TaxonomyName = "Category";
        private const string BreadcrumbName = "Under AnotherCategory0 > ... > AnotherCategory9";
        private const string BreadcrumbFullName = "Under AnotherCategory0 > AnotherCategory1 > AnotherCategory2 > AnotherCategory3 > AnotherCategory4 > AnotherCategory5 > AnotherCategory6 > AnotherCategory7 > AnotherCategory8 > AnotherCategory9";
    }
}