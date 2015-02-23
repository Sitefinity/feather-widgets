using System;
using Feather.Widgets.TestUI.Framework;
using FeatherWidgets.TestUI.TestCases;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telerik.Sitefinity.Frontend.TestUtilities;

namespace FeatherWidgets.TestUI.TestCases.News
{
    /// <summary>
    /// FilterNewsItemFromDifferentProviderWithCategoryOnPage test class.
    /// </summary>
    [TestClass]
    public class FilterNewsItemFromDifferentProviderWithCategoryOnPage_ : FeatherTestCase
    {
        /// <summary>
        /// UI test FilterNewsItemFromDifferentProviderWithCategoryOnPage
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.Team7),
        TestCategory(FeatherTestCategories.PagesAndContent),
        TestCategory(FeatherTestCategories.News),
        TestCategory(FeatherTestCategories.Selectors)]
        public void FilterNewsItemFromDifferentProviderWithCategoryOnPage()
        {
            BAT.Macros().NavigateTo().Pages();
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(PageName);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(WidgetName);
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SelectProvider(SecondProviderName);
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SelectWhichItemsToDisplay(WhichNewsToDisplay);
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SelectCheckBox(TaxonomyName);
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().ClickSelectButton();
            BATFeather.Wrappers().Backend().Widgets().SelectorsWrapper().WaitForItemsToAppear(1);
            BATFeather.Wrappers().Backend().Widgets().SelectorsWrapper().SelectItemsInHierarchicalSelector(TaxonTitle1, TaxonTitle2);
            BATFeather.Wrappers().Backend().Widgets().SelectorsWrapper().DoneSelecting();
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().VerifySelectedItemsFromHierarchicalSelector(new[] { "Category0 > Category1 > Category2 > Category3", "Category0 > Category1 > Category2 > Category3 > Category4" });
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SaveChanges();
            for (int i = 0; i < 7; i++)
            {      
                if (i >= 2 && i < 4)
                {
                    BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().CheckWidgetContent(WidgetName, "NewsTitle" + i);
                }
                else
                {
                    BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().CheckWidgetContentForNotExistingContent(WidgetName, "NewsTitle" + i);
                }
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
        private const string TaxonTitle1 = "Category3";
        private const string TaxonTitle2 = "Category4";
        private const string NewsTitle2 = "NewsTitle2";
        private const string NewsTitle3 = "NewsTitle3";
        private const string WidgetName = "News";
        private const string WhichNewsToDisplay = "Narrow selection by...";
        private const string TaxonomyName = "Category";
        private readonly string[] newsTitles = new string[] { NewsTitle3, NewsTitle2 };
        private const string SecondProviderName = "NewsSecondDataProvider";
    }
}