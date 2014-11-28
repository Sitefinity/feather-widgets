using System;
using Feather.Widgets.TestUI.Framework;
using FeatherWidgets.TestUI.TestCases;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FeatherWidgets.TestUI
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
        Microsoft.VisualStudio.TestTools.UnitTesting.Owner("Sitefinity Team 7"),
        TestCategory(FeatherTestCategories.PagesAndContent),
        TestCategory(FeatherTestCategories.NewsSelectors)]

        public void FilterNewsItemWithCategoryTagAndDateOnPage()
        {
            BAT.Macros().NavigateTo().Pages();
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(PageName);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(WidgetName);
            BATFeather.Wrappers().Backend().News().NewsWidgetEditContentScreenWrapper().SelectWhichNewsToDisplay(WhichNewsToDisplay);

            BATFeather.Wrappers().Backend().News().NewsWidgetEditContentScreenWrapper().SelectCheckBox(DateName);
            BATFeather.Wrappers().Backend().News().NewsWidgetEditContentScreenWrapper().ClickSelectButtonByDate();
            BATFeather.Wrappers().Backend().News().NewsWidgetEditContentScreenWrapper().SelectDisplayItemsPublishedIn(DisplayItemsPublishedIn);
            BATFeather.Wrappers().Backend().News().NewsWidgetEditContentScreenWrapper().SetFromDateByTyping(DayAgo);
            BATFeather.Wrappers().Backend().News().NewsWidgetEditContentScreenWrapper().AddHour("10", true);
            BATFeather.Wrappers().Backend().News().NewsWidgetEditContentScreenWrapper().AddMinute("2", true);
            BATFeather.Wrappers().Backend().News().NewsWidgetEditContentScreenWrapper().SetToDateByDatePicker(DayForward);
            BATFeather.Wrappers().Backend().News().NewsWidgetEditContentScreenWrapper().AddHour("13", false);
            BATFeather.Wrappers().Backend().News().NewsWidgetEditContentScreenWrapper().AddMinute("4", false);
            BATFeather.Wrappers().Backend().News().NewsWidgetEditContentScreenWrapper().DoneSelecting();

            BATFeather.Wrappers().Backend().News().NewsWidgetEditContentScreenWrapper().SelectCheckBox(TaxonomyTags);
            BATFeather.Wrappers().Backend().News().NewsWidgetEditContentScreenWrapper().ClickSelectButton();
            BATFeather.Wrappers().Backend().News().NewsWidgetEditContentScreenWrapper().WaitForItemsToAppear(4);
            BATFeather.Wrappers().Backend().News().NewsWidgetEditContentScreenWrapper().SelectItemsInFlatSelector(TagTitle);
            BATFeather.Wrappers().Backend().News().NewsWidgetEditContentScreenWrapper().DoneSelecting();

            BATFeather.Wrappers().Backend().News().NewsWidgetEditContentScreenWrapper().SelectCheckBox(TaxonomyCategory);
            BATFeather.Wrappers().Backend().News().NewsWidgetEditContentScreenWrapper().ClickSelectButton();
            BATFeather.Wrappers().Backend().News().NewsWidgetEditContentScreenWrapper().WaitForItemsToAppear(1);
            BATFeather.Wrappers().Backend().News().NewsWidgetEditContentScreenWrapper().SelectItemsInHierarchicalSelector(CategoryTitle);
            BATFeather.Wrappers().Backend().News().NewsWidgetEditContentScreenWrapper().DoneSelecting();

            BATFeather.Wrappers().Backend().News().NewsWidgetEditContentScreenWrapper().SaveChanges();
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().CheckWidgetContent(WidgetName, NewsTitle);
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
        private string[] newsTitles = new string[] { NewsTitle };
    }
}