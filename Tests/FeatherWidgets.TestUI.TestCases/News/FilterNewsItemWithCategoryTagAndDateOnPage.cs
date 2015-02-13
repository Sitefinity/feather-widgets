using System;
using Feather.Widgets.TestUI.Framework;
using FeatherWidgets.TestUI.TestCases;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FeatherWidgets.TestUI.TestCases.News
{
    /// <summary>
    /// FilterNewsItemWithCategoryTagAndDateOnPage test class.
    /// </summary>
    [TestClass]
    public class FilterNewsItemWithCategoryTagAndDateOnPage_ : FeatherTestCase
    {
        /// <summary>
        /// UI test FilterNewsItemWithCategoryTagAndDateOnPage
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.Team7),
        TestCategory(FeatherTestCategories.PagesAndContent),
        TestCategory(FeatherTestCategories.News), 
        TestCategory(FeatherTestCategories.Selectors)]
        public void FilterNewsItemWithCategoryTagAndDateOnPage()
        {
            BAT.Macros().NavigateTo().Pages();
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(PageName);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(WidgetName);
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SelectWhichItemsToDisplay(WhichNewsToDisplay);

            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SelectCheckBox(DateName);
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().ClickSelectButtonByDate();
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SelectDisplayItemsPublishedIn(DisplayItemsPublishedIn);
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SetFromDateByTyping(DayAgo);
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().AddHour("10", true);
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().AddMinute("2", true);
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SetToDateByDatePicker(DayForward);
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().AddHour("13", false);
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().AddMinute("4", false);
            BATFeather.Wrappers().Backend().Widgets().SelectorsWrapper().DoneSelecting();

            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SelectCheckBox(TaxonomyTags);
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().ClickSelectButton();
            BATFeather.Wrappers().Backend().Widgets().SelectorsWrapper().WaitForItemsToAppear(4);
            BATFeather.Wrappers().Backend().Widgets().SelectorsWrapper().SelectItemsInFlatSelector(TagTitle);
            BATFeather.Wrappers().Backend().Widgets().SelectorsWrapper().DoneSelecting();

            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SelectCheckBox(TaxonomyCategory);
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().ClickSelectButton();
            BATFeather.Wrappers().Backend().Widgets().SelectorsWrapper().WaitForItemsToAppear(1);
            BATFeather.Wrappers().Backend().Widgets().SelectorsWrapper().SelectItemsInHierarchicalSelector(CategoryTitle);
            BATFeather.Wrappers().Backend().Widgets().SelectorsWrapper().DoneSelecting();

            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SaveChanges();
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().CheckWidgetContent(WidgetName, NewsTitle);
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

        private const string DisplayItemsPublishedIn = "Custom range...";
        private const string WhichNewsToDisplay = "Narrow selection by...";
        private const string DateName = "dateInput";
        private const int DayAgo = -1;
        private const int DayForward = 1;
        private const string PageName = "News";
        private const string CategoryTitle = "Category3";
        private const string TagTitle = "Tag3";
        private const string NewsTitle = "NewsTitle2";
        private const string WidgetName = "News";
        private const string TaxonomyCategory = "Category";
        private const string TaxonomyTags = "Tags";
        private readonly string[] newsTitles = new string[] { NewsTitle };
    }
}