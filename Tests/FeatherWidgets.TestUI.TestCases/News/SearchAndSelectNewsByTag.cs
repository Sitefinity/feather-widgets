using System;
using Feather.Widgets.TestUI.Framework;
using FeatherWidgets.TestUI.TestCases;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FeatherWidgets.TestUI.TestCases.News
{
    /// <summary>
    /// SearchAndSelectNewsByTag test class.
    /// </summary>
    [TestClass]
    public class SearchAndSelectNewsByTag_ : FeatherTestCase
    {
        /// <summary>
        /// UI test SearchAndSelectNewsByTag
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.Team7),
        TestCategory(FeatherTestCategories.PagesAndContent),
        TestCategory(FeatherTestCategories.News),
        TestCategory(FeatherTestCategories.Selectors)]
        public void SearchAndSelectNewsByTag()
        {
            BAT.Macros().NavigateTo().Pages();
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(PageName);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(WidgetName);
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SelectWhichItemsToDisplay(WhichNewsToDisplay);
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SelectCheckBox(TaxonomyName);
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().ClickSelectButton();
            BATFeather.Wrappers().Backend().Widgets().SelectorsWrapper().SearchItemByTitle(TaxonTitle1);
            BATFeather.Wrappers().Backend().Widgets().SelectorsWrapper().SelectItemsInFlatSelector(TaxonTitle1);
            BATFeather.Wrappers().Backend().Widgets().SelectorsWrapper().SearchItemByTitle(TaxonTitle2);
            BATFeather.Wrappers().Backend().Widgets().SelectorsWrapper().SelectItemsInFlatSelector(TaxonTitle2);
            BATFeather.Wrappers().Backend().Widgets().SelectorsWrapper().DoneSelecting();
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().VerifySelectedItemsFromFlatSelector(new[] { TaxonTitle1, TaxonTitle2 });
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SaveChanges();
            this.VerifyNewsOnBackend(); 

            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();
            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower());
            Assert.IsTrue(BATFeather.Wrappers().Frontend().News().NewsWrapper().IsNewsTitlesPresentOnThePageFrontend(new string[] { NewsTitle + 2, NewsTitle + 1 }));
            Assert.IsFalse(BATFeather.Wrappers().Frontend().News().NewsWrapper().IsNewsTitlesPresentOnThePageFrontend(new string[] { NewsTitle + 0, NewsTitle + 3, NewsTitle + 4 }));
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

        private void VerifyNewsOnBackend()
        {
            for (int i = 0; i < 5; i++)
            {
                if (i > 0 && i <= 2)
                {
                    BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().CheckWidgetContent(WidgetName, NewsTitle + i);
                }
                else
                {
                    BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().CheckWidgetContentForNotExistingContent(WidgetName, NewsTitle + i);
                }
            }
        }

        private const string PageName = "News";
        private const string TaxonTitle1 = "Tag1";
        private const string TaxonTitle2 = "Tag2";
        private const string NewsTitle = "NewsTitle";
        private const string WidgetName = "News";
        private const string WhichNewsToDisplay = "Narrow selection by...";
        private const string TaxonomyName = "Tags";
    }
}