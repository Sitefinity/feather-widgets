using System;
using Feather.Widgets.TestUI.Framework;
using FeatherWidgets.TestUI.TestCases;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FeatherWidgets.TestUI
{
    /// <summary>
    /// SelectNewsItemInNewsWidgetFromPage_ test class.
    /// </summary>
    [TestClass]
    public class Load200TagsAndSearch_ : FeatherTestCase
    {
        /// <summary>
        /// UI test FilterNewsItemWithCustomTaxonomyOnPage
        /// </summary>
        [TestMethod,
       Microsoft.VisualStudio.TestTools.UnitTesting.Owner("Sitefinity Team 7"),
       TestCategory(FeatherTestCategories.PagesAndContent)]
        public void Load200TagsAndSearch()
        {
            BAT.Macros().NavigateTo().Pages();
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(PageName);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(WidgetName);
            BATFeather.Wrappers().Backend().News().NewsWidgetEditContentScreenWrapper().SelectWhichNewsToDisplay(WhichNewsToDisplay);
            BATFeather.Wrappers().Backend().News().NewsWidgetEditContentScreenWrapper().SelectTaxonomy(TaxonomyName);
            BATFeather.Wrappers().Backend().News().NewsWidgetEditContentScreenWrapper().SelectContent();
            BATFeather.Wrappers().Backend().News().NewsWidgetEditContentScreenWrapper().WaitForItemsToAppear(200);

            BATFeather.Wrappers().Backend().News().NewsWidgetEditContentScreenWrapper().SearchItemByTitle(TaxonTitle);
            BATFeather.Wrappers().Backend().News().NewsWidgetEditContentScreenWrapper().WaitForItemsToAppear(0);
            BATFeather.Wrappers().Backend().News().NewsWidgetEditContentScreenWrapper().NoItemsFound();

            BATFeather.Wrappers().Backend().News().NewsWidgetEditContentScreenWrapper().ChangeSearchText(TaxonTitle1);
            BATFeather.Wrappers().Backend().News().NewsWidgetEditContentScreenWrapper().WaitForItemsToAppear(1);

            BATFeather.Wrappers().Backend().News().NewsWidgetEditContentScreenWrapper().SelectItem(TaxonTitle1);
            BATFeather.Wrappers().Backend().News().NewsWidgetEditContentScreenWrapper().DoneSelecting();
            BATFeather.Wrappers().Backend().News().NewsWidgetEditContentScreenWrapper().VerifySelectedItemInMultipleSelectors(new[] { TaxonTitle1 });
            BATFeather.Wrappers().Backend().News().NewsWidgetEditContentScreenWrapper().SaveChanges();
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().CheckWidgetContent(WidgetName, NewsTitle1);

            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(WidgetName);
            BATFeather.Wrappers().Backend().News().NewsWidgetEditContentScreenWrapper().VerifySelectedItemInMultipleSelectors(new [] {TaxonTitle1});
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
        private const string TaxonTitle1 = "Tag125";
        private const string TaxonTitle = "Tag200";
        private const string NewsTitle1 = "NewsTitle125";
        private const string WidgetName = "News";
        private const string WhichNewsToDisplay = "Narrow selection by...";
        private const string TaxonomyName = "Tags";
    }
}
