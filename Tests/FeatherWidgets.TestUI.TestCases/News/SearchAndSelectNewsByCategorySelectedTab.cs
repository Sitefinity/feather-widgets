using Feather.Widgets.TestUI.Framework;
using FeatherWidgets.TestUI.TestCases;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeatherWidgets.TestUI
{
    /// <summary>
    /// SearchAndSelectNewsByCategorySelectedTab test class.
    /// </summary>
    [TestClass]
    public class SearchAndSelectNewsByCategorySelectedTab_ : FeatherTestCase
    {
        /// <summary>
        /// UI test SearchAndSelectNewsByCategory
        /// </summary>
        [TestMethod,
       Microsoft.VisualStudio.TestTools.UnitTesting.Owner("Feather team"),
       TestCategory(FeatherTestCategories.PagesAndContent)]
        public void SearchAndSelectNewsByCategorySelectedTab()
        {
            BAT.Macros().NavigateTo().Pages();
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(PageName);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(WidgetName);
            BATFeather.Wrappers().Backend().News().NewsWidgetEditContentScreenWrapper().SelectWhichNewsToDisplay(WhichNewsToDisplay);
            BATFeather.Wrappers().Backend().News().NewsWidgetEditContentScreenWrapper().SelectCheckBox(TaxonomyName);
            BATFeather.Wrappers().Backend().News().NewsWidgetEditContentScreenWrapper().ClickSelectButton();
            BATFeather.Wrappers().Backend().News().NewsWidgetEditContentScreenWrapper().SelectItemInMultipleHierarchicalSelector(selectedTaxonTitles);
            var countOfSelectedItems = selectedTaxonTitles.Count();
            BATFeather.Wrappers().Backend().News().NewsWidgetEditContentScreenWrapper().CheckNotificationInSelectedTab(countOfSelectedItems);
            BATFeather.Wrappers().Backend().News().NewsWidgetEditContentScreenWrapper().OpenSelectedTab();
            BATFeather.Wrappers().Backend().News().NewsWidgetEditContentScreenWrapper().SearchItemByTitle("C");
            BATFeather.Wrappers().Backend().News().NewsWidgetEditContentScreenWrapper().WaitForItemsToAppear(4);

            BATFeather.Wrappers().Backend().News().NewsWidgetEditContentScreenWrapper().SelectItemInMultipleSelector("AnotherCategory6");
            BATFeather.Wrappers().Backend().News().NewsWidgetEditContentScreenWrapper().CheckNotificationInSelectedTab(countOfSelectedItems - 1);

            BATFeather.Wrappers().Backend().News().NewsWidgetEditContentScreenWrapper().DoneSelecting();
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
        private const string NewsTitle1 = "NewsTitle5";
        private const string NewsTitle2 = "NewsTitle18";
        private readonly string[] selectedTaxonTitles = { "Category1", "Category5", "AnotherCategory6", "AnotherCategory10" };
        private readonly string[] newsTitles = new string[] { "NewsTitle1", "NewsTitle5", "NewsTitle22" };
        private const string WidgetName = "News";
        private const string WhichNewsToDisplay = "Narrow selection by...";
        private const string TaxonomyName = "Category";
    }
}
