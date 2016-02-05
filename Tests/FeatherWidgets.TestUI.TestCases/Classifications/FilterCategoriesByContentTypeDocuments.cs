using System;
using Feather.Widgets.TestUI.Framework;
using Feather.Widgets.TestUI.Framework.Framework.Wrappers.Backend.Classifications;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FeatherWidgets.TestUI.TestCases.Classifications
{
    /// <summary>
    /// FilterCategoriesByContentTypeDocuments test class.
    /// </summary>
    [TestClass]
    public class FilterCategoriesByContentTypeDocuments_ : FeatherTestCase
    {
        /// <summary>
        /// UI test verifying the filtering of categories by content type in hybrid page
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.FeatherTeam),
        TestCategory(FeatherTestCategories.PagesAndContent)]
        public void FilterCategoriesByContentTypeDocuments()
        {
            BAT.Macros().NavigateTo().Pages(this.Culture);
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(PageName);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(WidgetName);
            BATFeather.Wrappers().Backend().Classifications().CategoriesWrapper().SelectRadioButtonOption(CategoriesRadioButtonIds.ContentCategories);
            BATFeather.Wrappers().Backend().Classifications().CategoriesWrapper().SelectUsedByContentTypeOption(DocumentsOption);
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SaveChanges();
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().CheckWidgetContent(WidgetName, taxonTitleDocuments);
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();
            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower(), false, this.Culture);
            Assert.IsTrue(BATFeather.Wrappers().Frontend().Classifications().ClassificationsWrapper().IsCategoriesTitlesPresentOnTheFrontendPage(new string[] { taxonTitleDocuments }));
            Assert.IsFalse(BATFeather.Wrappers().Frontend().Classifications().ClassificationsWrapper().IsCategoriesTitlesPresentOnTheFrontendPage(new string[] { taxonTitleNews + 1, taxonTitleNews + 2, taxonTitleNews + 3 }));
            Assert.IsTrue(BATFeather.Wrappers().Frontend().News().NewsWrapper().IsNewsTitlesPresentOnThePageFrontend(new string[] { NewsTitle + 1, NewsTitle + 2, NewsTitle + 3 }));

            BATFeather.Wrappers().Frontend().Classifications().ClassificationsWrapper().ClickCategoryTitle(taxonTitleDocuments);
            Assert.IsFalse(BATFeather.Wrappers().Frontend().News().NewsWrapper().IsNewsTitlesPresentOnThePageFrontend(new string[] { NewsTitle + 1, NewsTitle + 2, NewsTitle + 3 }));
            Assert.IsTrue(BATFeather.Wrappers().Frontend().DocumentsList().DocumentsListWrapper().IsDocumentTitlePresentOnDetailMasterPage(DocumentTitle));
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

        private const string PageName = "CategoriesPage";
        private const string WidgetName = "Categories";
        private string taxonTitleNews = "CategoryNews";
        private string taxonTitleDocuments = "CategoryDocuments";
        private const string NewsTitle = "NewsTitle";
        private const string DocumentsOption = "Documents & files";
        private const string DocumentTitle = "TestDocument";
    }
}